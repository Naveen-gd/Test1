using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Linq;
using System.Text.RegularExpressions;

using Can_Comm_Lib;

namespace Device_52295_Lib
{

    public class CommDevice
    {
        internal const int TIMEOUT_EE_MS = 100;
        internal const uint CAN_SUB_FRAME_MAX_LENGTH_M_W3 = 18;

        private CanComm _canCommRef;
        private CommParameters _commParametersRef;

        private Device _device;

        private byte _canNode;
        private byte _canGroup;
        private byte _canIndex;

        private byte _bz_M_W = 16;
        private byte _bz_M_R = 16;
        private byte _bz_S_R = 16;

        private void _ResetComm()
        {
            _bz_M_W = 16;
            _bz_M_R = 16;
            _bz_S_R = 16;
            _device.readFail.SetValue(false);
        }

        public Device deviceRef
        {
            get { return _device; }
        }

        public byte canNode
        {
            set { _canNode = value; }
            get { return _canNode;  }
        }

        public byte canGroup
        {
            set { _canGroup = value; }
            get { return _canGroup;  }
        }

        public byte canIndex
        {
            set { _canIndex = value; }
            get { return _canIndex;  }
        }

        public CommDevice(CanComm canCommRef, CommParameters commParametersRef)
        {
            _canCommRef = canCommRef;
            _commParametersRef = commParametersRef;

            _device = new Device(); ;

            _canNode = 0;
            _canGroup = 0;
            _canIndex = 0;

            _ResetComm();
        }

        public int WriteData(ushort addr, byte[] data)
        {
            int bytes = data.Length;
            int receive_bytes;
            int data_index = 0;

            uint msg_id = (uint)((_commParametersRef.frameType_M_W << 8) | _canNode);
            CanCommDlc msg_dlc = CanCommDlc.DLC_Bytes_FD_24;
            byte[] msg_data = new byte[64];

            _canCommRef.Reset();

            while (bytes > 0)
            {
                if (bytes >= 20) receive_bytes = 20;
                else receive_bytes = bytes;

                int iLength = CanComm.GetBytesFromDLC(msg_dlc);

                // increment BZ
                _bz_M_W = (byte)(((int)_bz_M_W + 1) & 0xF);

                // fill header
                msg_data[0] = 0x00;
                msg_data[1] = _bz_M_W;
                msg_data[2] = (byte)(addr & 0x00FF);
                msg_data[3] = (byte)((receive_bytes & 0x1F) << 3 | ((addr >> 8) & 0x3));

                // fill data
                for (int i = 0; i < receive_bytes; i++)
                {
                    msg_data[i + 4] = data[data_index++];
                }
                // fill rest with zero
                for (int i = receive_bytes + 4; i < iLength; i++)
                {
                    msg_data[i] = 0;
                }

                // Calculate CRC
                msg_data[0] = CommE2ECRC.calc(msg_data, 23, _commParametersRef.secureByte_M);

                _canCommRef.SendMsg(msg_id, msg_dlc, msg_data);

                addr += (ushort)receive_bytes; bytes -= receive_bytes;
            }
            return 0;
        }

        public bool ReadData(ushort addr, ref byte[] data)
        {
            try
            {
                int bytes = data.Length;
                int receive_bytes;
                int data_index = 0;

                uint msg_req_id = (uint)((_commParametersRef.frameType_M_R << 8) | _canNode);
                CanCommDlc msg_req_dlc = CanCommDlc.DLC_Bytes_4;
                byte[] msg_req_data = new byte[64];

                uint msg_rsp_exp_id = (uint)((_commParametersRef.frameType_S_R << 8) | _canNode);
                CanCommDlc msg_rsp_exp_dlc = CanCommDlc.DLC_Bytes_FD_24;

                uint msg_rsp_id = 0;
                CanCommDlc msg_rsp_dlc = CanCommDlc.DLC_Bytes_0;
                byte[] msg_rsp_data = new byte[64];

                _canCommRef.Reset();

                while (bytes > 0)
                {
                    if (bytes >= 20) receive_bytes = 20;
                    else receive_bytes = bytes;

                    // increment BZ
                    _bz_M_R = (byte)(((int)_bz_M_R + 1) & 0xF);

                    // fill header
                    msg_req_data[0] = 0x00;
                    msg_req_data[1] = _bz_M_R;
                    msg_req_data[2] = (byte)(addr & 0x00FF);
                    msg_req_data[3] = (byte)((receive_bytes & 0x1F) << 3 | ((addr >> 8) & 0x3));
                    //msg_req_data[4] = 0;

                    // Calculate CRC
                    msg_req_data[0] = CommE2ECRC.calc(msg_req_data, 3, _commParametersRef.secureByte_M);

                    _canCommRef.SendMsg(msg_req_id, msg_req_dlc, msg_req_data);

                    // receive Message
                    bool received = _canCommRef.ReceiveMsg(ref msg_rsp_id, ref msg_rsp_dlc, ref msg_rsp_data);

                    byte crc = CommE2ECRC.calc(msg_rsp_data, 23, _commParametersRef.secureByte_S);

                    if (!received) return _device.GotReadFail();

                    if (msg_rsp_id != msg_rsp_exp_id) return _device.GotReadFail();
                    if (msg_rsp_dlc != msg_rsp_exp_dlc) return _device.GotReadFail();
                    if (crc != msg_rsp_data[0]) return _device.GotReadFail();
                    if ((msg_rsp_data[1] & 0x0F) == _bz_S_R) return _device.GotReadFail();

                    _bz_S_R = (byte)(msg_rsp_data[1] & 0x0F);

                    for (int i = 0; i < receive_bytes; i++)
                    {
                        data[data_index++] = msg_rsp_data[i + 4];
                    }

                    addr += (ushort)receive_bytes; bytes -= receive_bytes;
                }
                // only okay
                _device.readFail.SetValue(false);
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception Information: \n\t" + e.Message);
            }
            return _device.GotReadFail();
        }

    }

}
