using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

using vxlapi_NET;

namespace Can_Comm_Lib
{
    internal class VectorComm : CanInterfaceBase
    {
        // DLL Import for RX events
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int WaitForSingleObject(int handle, int timeOut);
        
        // Driver access through XLDriver (wrapper)
        private XLDriver _xlDriver = new XLDriver();
        private String _appName = "";

        // Driver configuration
        private XLClass.xl_driver_config _xlDriverConfig;

        // Variables required by XLDriver
        private XLDefine.XL_HardwareType hwType = XLDefine.XL_HardwareType.XL_HWTYPE_NONE;
        private uint hwIndex = 0;
        private uint hwChannel = 0;
        private int portHandle = -1;
        private int eventHandle = -1;
        private UInt64 accessMask = 0;
        private UInt64 permissionMask = 0;
        private UInt64 channelMask = 0;
        private int channelIndex = 0;

        private uint canFdModeNoIso = 0;      // Global CAN FD ISO (default) / no ISO mode flag

        private bool _connected = false;

        private bool GetAppChannelAndTestIsOk(uint appChIdx, ref UInt64 chMask, ref int chIdx)
        {
            XLDefine.XL_Status status = _xlDriver.XL_GetApplConfig(_appName, appChIdx, ref hwType, ref hwIndex, ref hwChannel, XLDefine.XL_BusTypes.XL_BUS_TYPE_CAN);
            if (status != XLDefine.XL_Status.XL_SUCCESS) return false;

            chMask = _xlDriver.XL_GetChannelMask(hwType, (int)hwIndex, (int)hwChannel);
            chIdx = _xlDriver.XL_GetChannelIndex(hwType, (int)hwIndex, (int)hwChannel);
            if (chIdx < 0 || chIdx >= _xlDriverConfig.channelCount)
            {
                // the (hwType, hwIndex, hwChannel) triplet stored in the application configuration does not refer to any available channel.
                return false;
            }

            if ((_xlDriverConfig.channel[chIdx].channelBusCapabilities & XLDefine.XL_BusCapabilities.XL_BUS_ACTIVE_CAP_CAN) == 0)
            {
                throw new System.Exception("Vector CAN is not available on this channel");
            }

            if (canFdModeNoIso > 0)
            {
                if ((_xlDriverConfig.channel[chIdx].channelCapabilities & XLDefine.XL_ChannelCapabilities.XL_CHANNEL_FLAG_CANFD_BOSCH_SUPPORT) == 0)
                {
                    //Console.WriteLine("{0} ({1}) does not support CAN FD NO-ISO", driverConfig.channel[chIdx].name.TrimEnd(' ', '\0'),
                    //    driverConfig.channel[chIdx].transceiverName.TrimEnd(' ', '\0'));
                    return false;
                }
            }
            else
            {
                if ((_xlDriverConfig.channel[chIdx].channelCapabilities & XLDefine.XL_ChannelCapabilities.XL_CHANNEL_FLAG_CANFD_ISO_SUPPORT) == 0)
                {
                    //Console.WriteLine("{0} ({1}) does not support CAN FD ISO", driverConfig.channel[chIdx].name.TrimEnd(' ', '\0'),
                    //    driverConfig.channel[chIdx].transceiverName.TrimEnd(' ', '\0'));
                    return false;
                }
            }

            return true;
        }

        private XLDefine.XL_CANFD_DLC Convert_CAN_DLC_to_XL_DLC(CanCommDlc dlc)
        {
            switch (dlc)
            {
                case CanCommDlc.DLC_Bytes_0: return XLDefine.XL_CANFD_DLC.DLC_CAN_CANFD_0_BYTES;
                case CanCommDlc.DLC_Bytes_1: return XLDefine.XL_CANFD_DLC.DLC_CAN_CANFD_1_BYTES;
                case CanCommDlc.DLC_Bytes_2: return XLDefine.XL_CANFD_DLC.DLC_CAN_CANFD_2_BYTES;
                case CanCommDlc.DLC_Bytes_3: return XLDefine.XL_CANFD_DLC.DLC_CAN_CANFD_3_BYTES;
                case CanCommDlc.DLC_Bytes_4: return XLDefine.XL_CANFD_DLC.DLC_CAN_CANFD_4_BYTES;
                case CanCommDlc.DLC_Bytes_5: return XLDefine.XL_CANFD_DLC.DLC_CAN_CANFD_5_BYTES;
                case CanCommDlc.DLC_Bytes_6: return XLDefine.XL_CANFD_DLC.DLC_CAN_CANFD_6_BYTES;
                case CanCommDlc.DLC_Bytes_7: return XLDefine.XL_CANFD_DLC.DLC_CAN_CANFD_7_BYTES;
                case CanCommDlc.DLC_Bytes_8: return XLDefine.XL_CANFD_DLC.DLC_CAN_CANFD_8_BYTES;
                case CanCommDlc.DLC_Bytes_FD_12: return XLDefine.XL_CANFD_DLC.DLC_CANFD_12_BYTES;
                case CanCommDlc.DLC_Bytes_FD_16: return XLDefine.XL_CANFD_DLC.DLC_CANFD_16_BYTES;
                case CanCommDlc.DLC_Bytes_FD_20: return XLDefine.XL_CANFD_DLC.DLC_CANFD_20_BYTES;
                case CanCommDlc.DLC_Bytes_FD_24: return XLDefine.XL_CANFD_DLC.DLC_CANFD_24_BYTES;
                case CanCommDlc.DLC_Bytes_FD_32: return XLDefine.XL_CANFD_DLC.DLC_CANFD_32_BYTES;
                case CanCommDlc.DLC_Bytes_FD_48: return XLDefine.XL_CANFD_DLC.DLC_CANFD_48_BYTES;
                case CanCommDlc.DLC_Bytes_FD_64: return XLDefine.XL_CANFD_DLC.DLC_CANFD_64_BYTES;
            }

            return 0;
        }

        private CanCommDlc Convert_XL_DLC_to_CAN_DLC(XLDefine.XL_CANFD_DLC dlc)
        {
            switch (dlc)
            {
                case XLDefine.XL_CANFD_DLC.DLC_CAN_CANFD_0_BYTES: return CanCommDlc.DLC_Bytes_0;
                case XLDefine.XL_CANFD_DLC.DLC_CAN_CANFD_1_BYTES: return CanCommDlc.DLC_Bytes_1;
                case XLDefine.XL_CANFD_DLC.DLC_CAN_CANFD_2_BYTES: return CanCommDlc.DLC_Bytes_2;
                case XLDefine.XL_CANFD_DLC.DLC_CAN_CANFD_3_BYTES: return CanCommDlc.DLC_Bytes_3;
                case XLDefine.XL_CANFD_DLC.DLC_CAN_CANFD_4_BYTES: return CanCommDlc.DLC_Bytes_4;
                case XLDefine.XL_CANFD_DLC.DLC_CAN_CANFD_5_BYTES: return CanCommDlc.DLC_Bytes_5;
                case XLDefine.XL_CANFD_DLC.DLC_CAN_CANFD_6_BYTES: return CanCommDlc.DLC_Bytes_6;
                case XLDefine.XL_CANFD_DLC.DLC_CAN_CANFD_7_BYTES: return CanCommDlc.DLC_Bytes_7;
                case XLDefine.XL_CANFD_DLC.DLC_CAN_CANFD_8_BYTES: return CanCommDlc.DLC_Bytes_8;
                case XLDefine.XL_CANFD_DLC.DLC_CANFD_12_BYTES: return CanCommDlc.DLC_Bytes_FD_12;
                case XLDefine.XL_CANFD_DLC.DLC_CANFD_16_BYTES: return CanCommDlc.DLC_Bytes_FD_16;
                case XLDefine.XL_CANFD_DLC.DLC_CANFD_20_BYTES: return CanCommDlc.DLC_Bytes_FD_20;
                case XLDefine.XL_CANFD_DLC.DLC_CANFD_24_BYTES: return CanCommDlc.DLC_Bytes_FD_24;
                case XLDefine.XL_CANFD_DLC.DLC_CANFD_32_BYTES: return CanCommDlc.DLC_Bytes_FD_32;
                case XLDefine.XL_CANFD_DLC.DLC_CANFD_48_BYTES: return CanCommDlc.DLC_Bytes_FD_48;
                case XLDefine.XL_CANFD_DLC.DLC_CANFD_64_BYTES: return CanCommDlc.DLC_Bytes_FD_64;
            }

            return 0;
        }

        public VectorComm(string appName)
        {
            _appName = appName;

            _xlDriverConfig = new XLClass.xl_driver_config();
        }

        public override bool Connected()
        {
            return _connected;
        }

        public override void Reset()
        {
            //_xlDriver.XL_CanFlushTransmitQueue(portHandle, accessMask);
            _xlDriver.XL_FlushReceiveQueue(portHandle);
        }

        public static CanCommBitrateConfig GetBitrateConfigForBitrate(CanCommBitrate bitrate)
        {
            CanCommBitrateConfig bitrateConfig = new CanCommBitrateConfig();

            bitrateConfig.f_clock_mhz = 80;
            bitrateConfig.allowBrp = false;
            bitrateConfig.arbBrp = 1;
            bitrateConfig.dataBrp = 1;

            switch (bitrate)
            {
                case CanCommBitrate.BITRATE_500_SP80_500_SP80:
                    bitrateConfig.arbTseg1 = 127; bitrateConfig.arbTseg2 = 32;
                    bitrateConfig.dataTseg1 = 127; bitrateConfig.dataTseg2 = 32;
                    break;
                case CanCommBitrate.BITRATE_500_SP80_1000_SP70:
                    bitrateConfig.arbTseg1 = 127; bitrateConfig.arbTseg2 = 32;
                    bitrateConfig.dataTseg1 = 55; bitrateConfig.dataTseg2 = 24;
                    break;
                case CanCommBitrate.BITRATE_500_SP60_2000_SP60:
                    bitrateConfig.arbTseg1 = 95; bitrateConfig.arbTseg2 = 64;
                    bitrateConfig.dataTseg1 = 23; bitrateConfig.dataTseg2 = 16;
                    break;
                case CanCommBitrate.BITRATE_500_SP70_2000_SP70:
                    bitrateConfig.arbTseg1 = 111; bitrateConfig.arbTseg2 = 48;
                    bitrateConfig.dataTseg1 = 27; bitrateConfig.dataTseg2 = 12;
                    break;
                case CanCommBitrate.BITRATE_500_SP80_2000_SP60:
                    bitrateConfig.arbTseg1 = 127; bitrateConfig.arbTseg2 = 32;
                    bitrateConfig.dataTseg1 = 23; bitrateConfig.dataTseg2 = 16;
                    break;
                case CanCommBitrate.BITRATE_500_SP80_2000_SP70:
                    bitrateConfig.arbTseg1 = 127; bitrateConfig.arbTseg2 = 32;
                    bitrateConfig.dataTseg1 = 27; bitrateConfig.dataTseg2 = 12;
                    break;
                case CanCommBitrate.BITRATE_500_SP80_4000_SP70:
                    bitrateConfig.arbTseg1 = 127; bitrateConfig.arbTseg2 = 32;
                    bitrateConfig.dataTseg1 = 13; bitrateConfig.dataTseg2 = 6;
                    break;
                case CanCommBitrate.BITRATE_1000_SP70_2000_SP70:
                    bitrateConfig.arbTseg1 = 55; bitrateConfig.arbTseg2 = 24;
                    bitrateConfig.dataTseg1 = 27; bitrateConfig.dataTseg2 = 12;
                    break;
            }

            ValidateBitrateConfig(ref bitrateConfig);

            return bitrateConfig;
        }

        public static bool ValidateBitrateConfig(ref CanCommBitrateConfig bitrateConfig)
        {
            bitrateConfig.validated = true;

            if (bitrateConfig.f_clock_mhz != 80) 
                bitrateConfig.validated = false;
            if (bitrateConfig.arbBrp != 1) 
                bitrateConfig.validated = false;
            if (bitrateConfig.dataBrp != 1) 
                bitrateConfig.validated = false;

            bitrateConfig.arbBitrate = Convert.ToUInt32(Convert.ToDouble(bitrateConfig.f_clock_mhz * 1000000) / Convert.ToDouble(bitrateConfig.arbTseg1 + bitrateConfig.arbTseg2 + 1));
            bitrateConfig.dataBitrate = Convert.ToUInt32(Convert.ToDouble(bitrateConfig.f_clock_mhz * 1000000) / Convert.ToDouble(bitrateConfig.dataTseg1 + bitrateConfig.dataTseg2 + 1));

            bitrateConfig.arbSP = Convert.ToDouble(bitrateConfig.arbTseg1 + 1) / Convert.ToDouble(bitrateConfig.arbTseg1 + bitrateConfig.arbTseg2 + 1) * 100.0;
            bitrateConfig.dataSP = Convert.ToDouble(bitrateConfig.dataTseg1 + 1) / Convert.ToDouble(bitrateConfig.dataTseg1 + bitrateConfig.dataTseg2 + 1) * 100.0;

            return bitrateConfig.validated;
        }

        public override void Open(CanCommBitrateConfig bitrateConfig)
        {
            XLDefine.XL_Status status;

            // Open XL Driver
            status = _xlDriver.XL_OpenDriver();
            if (status != XLDefine.XL_Status.XL_SUCCESS)
            {
                throw new System.Exception("Vector XL_OpenDriver not OK: " + status);
            }

            // Get XL Driver configuration
            status = _xlDriver.XL_GetDriverConfig(ref _xlDriverConfig);
            if (status != XLDefine.XL_Status.XL_SUCCESS)
            {
                throw new System.Exception("Vector XL_GetDriverConfig not OK: " + status);
            }

            bool do_show_hw_config = false;

            // If the application name cannot be found in VCANCONF...
            if (_xlDriver.XL_GetApplConfig(_appName, 0, ref hwType, ref hwIndex, ref hwChannel, XLDefine.XL_BusTypes.XL_BUS_TYPE_CAN) != XLDefine.XL_Status.XL_SUCCESS)
            {
                // ... create the item
                _xlDriver.XL_SetApplConfig(_appName, 0, XLDefine.XL_HardwareType.XL_HWTYPE_NONE, 0, 0, XLDefine.XL_BusTypes.XL_BUS_TYPE_CAN);
                do_show_hw_config = true;
            }

            // Request the user to assign channel
            if (!GetAppChannelAndTestIsOk(0, ref channelMask, ref channelIndex))
            {
                do_show_hw_config = true;
            }

            if (do_show_hw_config)
            {
                _xlDriver.XL_PopupHwConfig();
                return;
            }

            accessMask = channelMask;
            permissionMask = accessMask;

            // Open port
            status = _xlDriver.XL_OpenPort(ref portHandle, _appName, accessMask, ref permissionMask, 8192, XLDefine.XL_InterfaceVersion.XL_INTERFACE_VERSION_V4, XLDefine.XL_BusTypes.XL_BUS_TYPE_CAN);
            if (status != XLDefine.XL_Status.XL_SUCCESS)
            {
                throw new System.Exception("Vector XL_OpenPort not OK: " + status);
            }

            // Set CAN FD config and bitrate
            if (!ValidateBitrateConfig(ref bitrateConfig))
            {
                throw new System.Exception("Vector Bitrate Config wrong!");
            }

            XLClass.XLcanFdConf canFdConf = new XLClass.XLcanFdConf();

            canFdConf.arbitrationBitRate = bitrateConfig.arbBitrate;
            canFdConf.tseg1Abr = bitrateConfig.arbTseg1;
            canFdConf.tseg2Abr = bitrateConfig.arbTseg2;
            canFdConf.sjwAbr = canFdConf.tseg2Abr;

            canFdConf.dataBitRate = bitrateConfig.dataBitrate;
            canFdConf.tseg1Dbr = bitrateConfig.dataTseg1;
            canFdConf.tseg2Dbr = bitrateConfig.dataTseg2;
            canFdConf.sjwDbr = canFdConf.tseg2Dbr;

            if (canFdModeNoIso > 0) canFdConf.options = (byte)XLDefine.XL_CANFD_ConfigOptions.XL_CANFD_CONFOPT_NO_ISO;
            else canFdConf.options = 0;

            status = _xlDriver.XL_CanFdSetConfiguration(portHandle, accessMask, canFdConf);
            if (status != XLDefine.XL_Status.XL_SUCCESS)
            {
                throw new System.Exception("Vector XL_CanFdSetConfiguration not OK: " + status);
            }

            // Get RX event handle
            status = _xlDriver.XL_SetNotification(portHandle, ref eventHandle, 1);
            if (status != XLDefine.XL_Status.XL_SUCCESS)
            {
                throw new System.Exception("Vector XL_SetNotification not OK: " + status);
            }

            // no receipt for transmitting
            _xlDriver.XL_CanSetChannelMode(portHandle, accessMask, 0, 0);
            if (status != XLDefine.XL_Status.XL_SUCCESS)
            {
                throw new System.Exception("Vector XL_CanSetChannelMode not OK: " + status);
            }

            // Activate channel - with reset clock
            status = _xlDriver.XL_ActivateChannel(portHandle, accessMask, XLDefine.XL_BusTypes.XL_BUS_TYPE_CAN, XLDefine.XL_AC_Flags.XL_ACTIVATE_RESET_CLOCK);
            if (status != XLDefine.XL_Status.XL_SUCCESS)
            {
                throw new System.Exception("Vector XL_ActivateChannel not OK: " + status);
            }

            // Get XL Driver configuration to get the actual setup parameter
            status = _xlDriver.XL_GetDriverConfig(ref _xlDriverConfig);
            if (status != XLDefine.XL_Status.XL_SUCCESS)
            {
                throw new System.Exception("Vector XL_GetDriverConfig not OK: " + status);
            }

            _connected = true;
        }

        public override void Close()
        {
            // deactivate channel
            _xlDriver.XL_DeactivateChannel(portHandle, accessMask);

            // close the Port
            _xlDriver.XL_ClosePort(portHandle);

            _connected = false;
        }

        public override void SendMsg(uint id, CanCommDlc dlc, byte[] data)
        {
            XLClass.xl_canfd_event_collection xlEventCollection  = new XLClass.xl_canfd_event_collection(1);

            xlEventCollection.xlCANFDEvent[0].tag = XLDefine.XL_CANFD_TX_EventTags.XL_CAN_EV_TAG_TX_MSG;
            xlEventCollection.xlCANFDEvent[0].tagData.msgFlags = XLDefine.XL_CANFD_TX_MessageFlags.XL_CAN_TXMSG_FLAG_BRS | XLDefine.XL_CANFD_TX_MessageFlags.XL_CAN_TXMSG_FLAG_EDL;
            xlEventCollection.xlCANFDEvent[0].tagData.canId = id;
            xlEventCollection.xlCANFDEvent[0].tagData.dlc = Convert_CAN_DLC_to_XL_DLC(dlc);
            xlEventCollection.xlCANFDEvent[0].tagData.data = data;

            XLDefine.XL_Status status;
            uint messageCounterSent = 0;
            status = _xlDriver.XL_CanTransmitEx(portHandle, channelMask, ref messageCounterSent, xlEventCollection);
            if (status != XLDefine.XL_Status.XL_SUCCESS)
            {
                throw new System.Exception("Vector XL_CanTransmitEx not OK: " + status);
            }
        }

        public override bool ReceiveMsg(ref uint id, ref CanCommDlc dlc, ref byte[] data)
        {
            Stopwatch stopwatch = new Stopwatch();
            XLDefine.WaitResults waitResult = new XLDefine.WaitResults();
            stopwatch.Start();

            bool received = false;
            long elapsedMS = 0;
            do
            {
                waitResult = (XLDefine.WaitResults)WaitForSingleObject(eventHandle, canTimeoutMs);

                elapsedMS = stopwatch.ElapsedMilliseconds;

                if (waitResult == XLDefine.WaitResults.WAIT_OBJECT_0)
                {
                    XLDefine.XL_Status status;
                    XLClass.XLcanRxEvent receivedEvent = new XLClass.XLcanRxEvent();

                    status = _xlDriver.XL_CanReceive(portHandle, ref receivedEvent);

                    if (status == XLDefine.XL_Status.XL_SUCCESS)
                    {
                        if (receivedEvent.tag == XLDefine.XL_CANFD_RX_EventTags.XL_CAN_EV_TAG_RX_OK)
                        {
                            id = receivedEvent.tagData.canRxOkMsg.canId;
                            dlc = Convert_XL_DLC_to_CAN_DLC(receivedEvent.tagData.canRxOkMsg.dlc);
                            receivedEvent.tagData.canRxOkMsg.data.CopyTo(data, 0);
                            received = true;
                        }
                    }
                }
            } while (!received && (elapsedMS < canTimeoutMs));

            if (!received)
            {
            }

            return received;
        }

    }
}
