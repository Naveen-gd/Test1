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

    public class CommDeviceGroup
    {
        private CanComm _canCommRef;
        private CommParameters _commParametersRef;
        private List<CommDevice> _commDevicesRef;

        private byte _bz_M_W3 = 16;
        private byte _group;

        public byte group
        {
            get { return _group; }
        }

        private byte[] _CreateW3Subframe(ushort addr, byte[] data)
        {
            int dataLength = data.Length;
            if (dataLength > CommDevice.CAN_SUB_FRAME_MAX_LENGTH_M_W3)
            {
                throw new System.ArgumentException("Array contains to much elements", "data");
            }

            byte[] subframe = new byte[dataLength + 2];       // Addr, length + data itself

            // Insert address & length
            subframe[0] = (byte)(addr & 0x00FF);
            subframe[1] = (byte)((dataLength & 0x1F) << 3 | ((addr >> 8) & 0x3));

            // Insert data
            for (int dataPos = 0, frameIdx = 2; dataPos < dataLength; dataPos++, frameIdx++)
            {
                subframe[frameIdx] = data[dataPos];
            }

            return subframe;
        }

        private void _WriteW3Group(ushort addr0, byte[] data0, ushort addr1 = 0, byte[] data1 = null, ushort addr2 = 0, byte[] data2 = null)
        {
            const int cSubframe0Addr = 02;      // Startaddress of subframe 0 in the message
            const int cSubframe1Addr = 22;      // Startaddress of subframe 1 in the message
            const int cSubframe2Addr = 42;      // Startaddress of subframe 2 in the message

            uint msg_id = (uint)((_commParametersRef.frameType_M_W3 << 8) | _group);
            CanCommDlc msg_dlc = CanCommDlc.DLC_Bytes_FD_64;
            byte[] msg_data = new byte[64];

            if (data0 == null)
            {
                throw new System.ArgumentException("Parameter must not be null", "data0");
            }
            if (data1 == null)
            {
                throw new System.ArgumentException("Parameter must not be null", "data1");
            }
            if (data2 == null)
            {
                throw new System.ArgumentException("Parameter must not be null", "data2");
            }

            // Carebear only: Make shure that the data array is zero'ed
            for (int dataPos = 0; dataPos < msg_data.Length; dataPos++)
            {
                msg_data[dataPos] = 0;
            }

            // Setup the subframes
            byte[] subframe0Data = _CreateW3Subframe(addr0, data0);
            byte[] subframe1Data = _CreateW3Subframe(addr1, data1);
            byte[] subframe2Data = _CreateW3Subframe(addr2, data2);

            int iLength = CanComm.GetBytesFromDLC(msg_dlc);

            // increment BZ
            _bz_M_W3 = (byte)(((int)_bz_M_W3 + 1) & 0xF);

            // fill header
            msg_data[0] = 0x00;
            msg_data[1] = _bz_M_W3;


            // Merge the subframes into the msg
            Array.Copy(subframe0Data, 0, msg_data, cSubframe0Addr, subframe0Data.Length);
            Array.Copy(subframe1Data, 0, msg_data, cSubframe1Addr, subframe1Data.Length);
            Array.Copy(subframe2Data, 0, msg_data, cSubframe2Addr, subframe2Data.Length);

            // Calculate CRC
            msg_data[0] = CommE2ECRC.calc(msg_data, 63, _commParametersRef.secureByte_M);

            try
            {
                _canCommRef.SendMsg(msg_id, msg_dlc, msg_data);
            }
            catch (Exception x) { }
        }

        public CommDeviceGroup(byte group, CanComm canCommRef, CommParameters commParametersRef)
        {
            _group = group;
            _canCommRef = canCommRef;
            _commParametersRef = commParametersRef;

            _commDevicesRef = new List<CommDevice>(3);

            ResetComm();
        }

        public void ResetComm()
        {
            _bz_M_W3 = 16;
        }

        public void RemoveAllCommDevices()
        {
            _commDevicesRef.Clear();
        }

        public void AddCommDeviceRef(CommDevice commDeviceRef)
        {
            _commDevicesRef.Add(commDeviceRef);
        }

        public void WritePwmData()
        {
            byte[] zeroPwm = new byte[0];

            byte[] pwm_0 = zeroPwm;
            byte[] pwm_1 = zeroPwm;
            byte[] pwm_2 = zeroPwm;

            foreach (CommDevice d in _commDevicesRef)
            {
                Debug.Assert(d.canGroup == _group);

                if (d.canIndex == 0) pwm_0 = new byte[BusConfig.SIZE_PULSE_AREA];
                if (d.canIndex == 1) pwm_1 = new byte[BusConfig.SIZE_PULSE_AREA];
                if (d.canIndex == 2) pwm_2 = new byte[BusConfig.SIZE_PULSE_AREA];

                for (byte i = 0; i < BusConfig.SIZE_PULSE_AREA; i += 1)
                {
                    if (d.canIndex == 0) pwm_0[i] = d.deviceRef.busConfig.GetPulse(i);
                    if (d.canIndex == 1) pwm_1[i] = d.deviceRef.busConfig.GetPulse(i);
                    if (d.canIndex == 2) pwm_2[i] = d.deviceRef.busConfig.GetPulse(i);
                }
            }

            _WriteW3Group(BusConfig.ADDR_PULSE_AREA, pwm_0, BusConfig.ADDR_PULSE_AREA, pwm_1, BusConfig.ADDR_PULSE_AREA, pwm_2);
        }
    }
}
