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

        public static CanCommBitrateConfig GetBitrateConfigForBitrate(CanCommBitrate bitrate)
        {
            CanCommBitrateConfig bitrateConfig = new CanCommBitrateConfig();

            bitrateConfig.f_clock_mhz = 40;
            bitrateConfig.allowBrp = true;

            switch (bitrate)
            {
                case CanCommBitrate.BITRATE_500_SP80_500_SP80:
                    bitrateConfig.arbBrp = 1; bitrateConfig.arbTseg1 = 63; bitrateConfig.arbTseg2 = 16; 
                    bitrateConfig.dataBrp = 2; bitrateConfig.dataTseg1 = 31; bitrateConfig.dataTseg2 = 8;
                    break;
                case CanCommBitrate.BITRATE_500_SP80_1000_SP70:
                    bitrateConfig.arbBrp = 1; bitrateConfig.arbTseg1 = 63; bitrateConfig.arbTseg2 = 16; 
                    bitrateConfig.dataBrp = 1; bitrateConfig.dataTseg1 = 27; bitrateConfig.dataTseg2 = 12;
                    break;
                case CanCommBitrate.BITRATE_500_SP60_2000_SP60:
                    bitrateConfig.arbBrp = 1; bitrateConfig.arbTseg1 = 47; bitrateConfig.arbTseg2 = 32; 
                    bitrateConfig.dataBrp = 1; bitrateConfig.dataTseg1 = 11; bitrateConfig.dataTseg2 = 8;
                    break;
                case CanCommBitrate.BITRATE_500_SP70_2000_SP70:
                    bitrateConfig.arbBrp = 1; bitrateConfig.arbTseg1 = 55; bitrateConfig.arbTseg2 = 24; 
                    bitrateConfig.dataBrp = 1; bitrateConfig.dataTseg1 = 13; bitrateConfig.dataTseg2 = 6;
                    break;
                case CanCommBitrate.BITRATE_500_SP80_2000_SP60:
                    bitrateConfig.arbBrp = 1; bitrateConfig.arbTseg1 = 63; bitrateConfig.arbTseg2 = 16; 
                    bitrateConfig.dataBrp = 1; bitrateConfig.dataTseg1 = 11; bitrateConfig.dataTseg2 = 8;
                    break;
                case CanCommBitrate.BITRATE_500_SP80_2000_SP70:
                    bitrateConfig.arbBrp = 1; bitrateConfig.arbTseg1 = 63; bitrateConfig.arbTseg2 = 16; 
                    bitrateConfig.dataBrp = 1; bitrateConfig.dataTseg1 = 13; bitrateConfig.dataTseg2 = 6;
                    break;
                case CanCommBitrate.BITRATE_500_SP80_4000_SP70:
                    bitrateConfig.arbBrp = 1; bitrateConfig.arbTseg1 = 63; bitrateConfig.arbTseg2 = 16; 
                    bitrateConfig.dataBrp = 1; bitrateConfig.dataTseg1 = 6; bitrateConfig.dataTseg2 = 3;
                    break;
                case CanCommBitrate.BITRATE_1000_SP70_2000_SP70:
                    bitrateConfig.arbBrp = 1; bitrateConfig.arbTseg1 = 27; bitrateConfig.arbTseg2 = 12; 
                    bitrateConfig.dataBrp = 1; bitrateConfig.dataTseg1 = 13; bitrateConfig.dataTseg2 = 6;
                    break;
            }

            ValidateBitrateConfig(ref bitrateConfig);

            return bitrateConfig;
        }

        public static bool ValidateBitrateConfig(ref CanCommBitrateConfig bitrateConfig)
        {
            bitrateConfig.validated = true;

            if (bitrateConfig.f_clock_mhz != 40)
                bitrateConfig.validated = false;

            bitrateConfig.arbBitrate = Convert.ToUInt32(Convert.ToDouble(bitrateConfig.f_clock_mhz * 1000000) / Convert.ToDouble(bitrateConfig.arbBrp * (bitrateConfig.arbTseg1 + bitrateConfig.arbTseg2 + 1)));
            bitrateConfig.dataBitrate = Convert.ToUInt32(Convert.ToDouble(bitrateConfig.f_clock_mhz * 1000000) / Convert.ToDouble(bitrateConfig.dataBrp * (bitrateConfig.dataTseg1 + bitrateConfig.dataTseg2 + 1)));

            bitrateConfig.arbSP = Convert.ToDouble(bitrateConfig.arbTseg1 + 1) / Convert.ToDouble(bitrateConfig.arbTseg1 + bitrateConfig.arbTseg2 + 1) * 100.0;
            bitrateConfig.dataSP = Convert.ToDouble(bitrateConfig.dataTseg1 + 1) / Convert.ToDouble(bitrateConfig.dataTseg1 + bitrateConfig.dataTseg2 + 1) * 100.0;

            return bitrateConfig.validated;
        }

        public override void Open(CanCommBitrateConfig bitrateConfig)
        {
            _connected = false;
            TPCANStatus status;
            pCANHandle = PCANBasic.PCAN_USBBUS1;

            // get values from struct
            if (!ValidateBitrateConfig(ref bitrateConfig))
            {
                throw new System.Exception("PCANBasic Bitrate Config wrong!");
            }

            UInt16 f_clock_mhz = bitrateConfig.f_clock_mhz;

            UInt16 nom_brp = bitrateConfig.arbBrp;
            UInt16 nom_tseg1 = bitrateConfig.arbTseg1;
            UInt16 nom_tseg2 = bitrateConfig.arbTseg2;
            UInt16 nom_sjw = nom_tseg2;

            UInt16 data_brp = bitrateConfig.dataBrp;
            UInt16 data_tseg1 = bitrateConfig.dataTseg1;
            UInt16 data_tseg2 = bitrateConfig.dataTseg2;
            UInt16 data_sjw = data_tseg2;

            // generic bitrate to Peak String
            string bitrateString = String.Format("f_clock_mhz={0:D}, nom_brp={1:D}, nom_tseg1={2:D}, nom_tseg2={3:D}, nom_sjw={4:D}, data_brp={5:D}, data_tseg1={6:D},  data_tseg2={7:D}, data_sjw={8:D}",
                f_clock_mhz, nom_brp, nom_tseg1, nom_tseg2, nom_sjw, data_brp, data_tseg1, data_tseg2, data_sjw);

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
