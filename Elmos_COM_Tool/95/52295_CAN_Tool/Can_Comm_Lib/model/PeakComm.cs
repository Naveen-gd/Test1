using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using PCANBasic_NET;

namespace Can_Comm_Lib
{
    internal class PeakComm : CanInterfaceBase
    {
        private const byte CAN_DLC_Bytes_0 = 0;
        private const byte CAN_DLC_Bytes_1 = 1;
        private const byte CAN_DLC_Bytes_2 = 2;
        private const byte CAN_DLC_Bytes_3 = 3;
        private const byte CAN_DLC_Bytes_4 = 4;
        private const byte CAN_DLC_Bytes_5 = 5;
        private const byte CAN_DLC_Bytes_6 = 6;
        private const byte CAN_DLC_Bytes_7 = 7;
        private const byte CAN_DLC_Bytes_8 = 8;
        private const byte CAN_DLC_Bytes_FD_12 = 9; // only CAN-FD
        private const byte CAN_DLC_Bytes_FD_16 = 10; // only CAN-FD
        private const byte CAN_DLC_Bytes_FD_20 = 11; // only CAN-FD
        private const byte CAN_DLC_Bytes_FD_24 = 12; // only CAN-FD
        private const byte CAN_DLC_Bytes_FD_32 = 13; // only CAN-FD
        private const byte CAN_DLC_Bytes_FD_48 = 14; // only CAN-FD
        private const byte CAN_DLC_Bytes_FD_64 = 15; // only CAN-FD

        private bool _connected = false;

        private ushort pCANHandle;

        private byte Convert_CAN_DLC_to_byte(CanCommDlc dlc)
        {
            switch (dlc)
            {
                case CanCommDlc.DLC_Bytes_0: return CAN_DLC_Bytes_0;
                case CanCommDlc.DLC_Bytes_1: return CAN_DLC_Bytes_1;
                case CanCommDlc.DLC_Bytes_2: return CAN_DLC_Bytes_2;
                case CanCommDlc.DLC_Bytes_3: return CAN_DLC_Bytes_3;
                case CanCommDlc.DLC_Bytes_4: return CAN_DLC_Bytes_4;
                case CanCommDlc.DLC_Bytes_5: return CAN_DLC_Bytes_5;
                case CanCommDlc.DLC_Bytes_6: return CAN_DLC_Bytes_6;
                case CanCommDlc.DLC_Bytes_7: return CAN_DLC_Bytes_7;
                case CanCommDlc.DLC_Bytes_8: return CAN_DLC_Bytes_8;
                case CanCommDlc.DLC_Bytes_FD_12: return CAN_DLC_Bytes_FD_12;
                case CanCommDlc.DLC_Bytes_FD_16: return CAN_DLC_Bytes_FD_16;
                case CanCommDlc.DLC_Bytes_FD_20: return CAN_DLC_Bytes_FD_20;
                case CanCommDlc.DLC_Bytes_FD_24: return CAN_DLC_Bytes_FD_24;
                case CanCommDlc.DLC_Bytes_FD_32: return CAN_DLC_Bytes_FD_32;
                case CanCommDlc.DLC_Bytes_FD_48: return CAN_DLC_Bytes_FD_48;
                case CanCommDlc.DLC_Bytes_FD_64: return CAN_DLC_Bytes_FD_64;
            }

            return 0;
        }

        private CanCommDlc Convert_byte_to_CAN_DLC(byte dlc)
        {
            switch (dlc)
            {
                case CAN_DLC_Bytes_0: return CanCommDlc.DLC_Bytes_0;
                case CAN_DLC_Bytes_1: return CanCommDlc.DLC_Bytes_1;
                case CAN_DLC_Bytes_2: return CanCommDlc.DLC_Bytes_2;
                case CAN_DLC_Bytes_3: return CanCommDlc.DLC_Bytes_3;
                case CAN_DLC_Bytes_4: return CanCommDlc.DLC_Bytes_4;
                case CAN_DLC_Bytes_5: return CanCommDlc.DLC_Bytes_5;
                case CAN_DLC_Bytes_6: return CanCommDlc.DLC_Bytes_6;
                case CAN_DLC_Bytes_7: return CanCommDlc.DLC_Bytes_7;
                case CAN_DLC_Bytes_8: return CanCommDlc.DLC_Bytes_8;
                case CAN_DLC_Bytes_FD_12: return CanCommDlc.DLC_Bytes_FD_12;
                case CAN_DLC_Bytes_FD_16: return CanCommDlc.DLC_Bytes_FD_16;
                case CAN_DLC_Bytes_FD_20: return CanCommDlc.DLC_Bytes_FD_20;
                case CAN_DLC_Bytes_FD_24: return CanCommDlc.DLC_Bytes_FD_24;
                case CAN_DLC_Bytes_FD_32: return CanCommDlc.DLC_Bytes_FD_32;
                case CAN_DLC_Bytes_FD_48: return CanCommDlc.DLC_Bytes_FD_48;
                case CAN_DLC_Bytes_FD_64: return CanCommDlc.DLC_Bytes_FD_64;
            }

            return 0;
        }

        public override bool Connected()
        {
            return _connected;
        }

        public override void Reset()
        {
            PCANBasic.Reset(pCANHandle);
        }

        public override void Open(CanCommBitrate bitrate)
        {
            _connected = false;
            TPCANStatus status;
            pCANHandle = PCANBasic.PCAN_USBBUS1;

            // generic bitrate to Peak String
            string bitrateString = "";
            switch (bitrate)
            {
                case CanCommBitrate.BITRATE_500_SP80_500_SP80: 
                    bitrateString = "f_clock_mhz=40, nom_brp=1, nom_tseg1=63, nom_tseg2=16, nom_sjw=16, data_brp=2, data_tseg1=31, data_tseg2=8, data_sjw=8"; 
                    break;
                case CanCommBitrate.BITRATE_500_SP80_1000_SP70: 
                    bitrateString = "f_clock_mhz=40, nom_brp=1, nom_tseg1=63, nom_tseg2=16, nom_sjw=16, data_brp=1, data_tseg1=27, data_tseg2=12, data_sjw=12"; 
                    break;
                case CanCommBitrate.BITRATE_500_SP60_2000_SP60:
                    bitrateString = "f_clock_mhz=40, nom_brp=1, nom_tseg1=47, nom_tseg2=32, nom_sjw=32, data_brp=1, data_tseg1=11, data_tseg2=8, data_sjw=8";
                    break;
                case CanCommBitrate.BITRATE_500_SP70_2000_SP70:
                    bitrateString = "f_clock_mhz=40, nom_brp=1, nom_tseg1=55, nom_tseg2=24, nom_sjw=24, data_brp=1, data_tseg1=13, data_tseg2=6, data_sjw=6";
                    break;
                case CanCommBitrate.BITRATE_500_SP80_2000_SP60:
                    bitrateString = "f_clock_mhz=40, nom_brp=1, nom_tseg1=63, nom_tseg2=16, nom_sjw=16, data_brp=1, data_tseg1=11, data_tseg2=8, data_sjw=8";
                    break;
                case CanCommBitrate.BITRATE_500_SP80_2000_SP70:
                    bitrateString = "f_clock_mhz=40, nom_brp=1, nom_tseg1=63, nom_tseg2=16, nom_sjw=16, data_brp=1, data_tseg1=13, data_tseg2=6, data_sjw=6";
                    break;
                case CanCommBitrate.BITRATE_500_SP80_4000_SP70:
                    bitrateString = "f_clock_mhz=40, nom_brp=1, nom_tseg1=63, nom_tseg2=16, nom_sjw=16, data_brp=1, data_tseg1=6,  data_tseg2=3, data_sjw=3";
                    break;
                case CanCommBitrate.BITRATE_1000_SP70_2000_SP70:
                    bitrateString = "f_clock_mhz=40, nom_brp=1, nom_tseg1=27, nom_tseg2=12, nom_sjw=12, data_brp=1, data_tseg1=13,  data_tseg2=6, data_sjw=6";
                    break;
            }

            status = PCANBasic.InitializeFD(pCANHandle, bitrateString);

            if (status != TPCANStatus.PCAN_ERROR_OK)
            {
                throw new System.Exception("PCANBasic.InitializeFD not OK: " + status);
            }

            // clear status information
            TPCANMsgFD CANMsg;
            ulong CANTimeStamp;
            do
            {
                status = PCANBasic.ReadFD(pCANHandle, out CANMsg, out CANTimeStamp);
            }
            while (CANMsg.MSGTYPE == TPCANMessageType.PCAN_MESSAGE_STATUS);
            _connected = true;
        }

        public override void Close()
        {
            PCANBasic.Uninitialize(pCANHandle);
            _connected = false;
        }

        public override void SendMsg(uint id, CanCommDlc dlc, byte[] data)
        {
            TPCANMsgFD CANMsg = new TPCANMsgFD();
            CANMsg.ID = id;
            CANMsg.DLC = Convert_CAN_DLC_to_byte(dlc);
            CANMsg.MSGTYPE = TPCANMessageType.PCAN_MESSAGE_STANDARD;
            CANMsg.MSGTYPE |= TPCANMessageType.PCAN_MESSAGE_FD;
            CANMsg.MSGTYPE |= TPCANMessageType.PCAN_MESSAGE_BRS;
            CANMsg.DATA = data;

            TPCANStatus status;

            do
            {
                status = PCANBasic.WriteFD(pCANHandle, ref CANMsg);
            } while (status == TPCANStatus.PCAN_ERROR_QXMTFULL);

            if (status != TPCANStatus.PCAN_ERROR_OK)
            {
                throw new System.Exception("PCANBasic.WriteFD not OK: " + status);
            }
        }

        public override bool ReceiveMsg(ref uint id, ref CanCommDlc dlc, ref byte[] data)
        {
            TPCANStatus status;
            TPCANMsgFD CANMsgResponse;
            ulong CANTimeStamp;

            // Wait until something received
            int timeout = canTimeoutMs;
            do
            {
                status = PCANBasic.ReadFD(pCANHandle, out CANMsgResponse, out CANTimeStamp);
                if (status != TPCANStatus.PCAN_ERROR_OK)
                {
                    System.Threading.Thread.Sleep(1);
                    timeout--;
                }
                if (timeout == 0)
                {
                    throw new System.Exception("PCANBasic.ReadFD Timeout: " + status);
                }
            }
            while (status == TPCANStatus.PCAN_ERROR_QRCVEMPTY);

            if (status != TPCANStatus.PCAN_ERROR_OK)
            {
                throw new System.Exception("PCANBasic.ReadFD not OK: " + status);
            }

            id = CANMsgResponse.ID;
            dlc = Convert_byte_to_CAN_DLC(CANMsgResponse.DLC);
            CANMsgResponse.DATA.CopyTo(data, 0);

            return true;
        }

    }
}
