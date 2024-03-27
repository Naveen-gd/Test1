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

        #region "BUS CONFIG"

        public void WriteBusConfigPwmData()
        {
            byte[] pulses = new byte[BusConfig.SIZE_PULSE_AREA];
            for (byte i = 0; i < 16; i += 1) pulses[i] = _device.busConfig.GetPulse(i);
            WriteData(Device.ADDR_BUS_CONFIG + BusConfig.ADDR_PULSE_AREA, pulses);
            for (byte i = 0; i < 16; i += 1) _device.busConfig[(UInt32)(BusConfig.ADDR_PULSE_AREA + i)].modified = false;
        }

        public void ReadBusConfigPwmData()
        {
            byte[] temp_pulses = new byte[BusConfig.SIZE_PULSE_AREA];
            if (ReadData(Device.ADDR_BUS_CONFIG + BusConfig.ADDR_PULSE_AREA, ref temp_pulses))
            {
                for (byte i = 0; i < 16; i += 1) _device.busConfig.SetPulse(i, temp_pulses[i]);
            }
        }

        public void WriteBusConfigCurrentsData()
        {
            byte[] currents = new byte[BusConfig.SIZE_CURRENT_AREA];
            for (byte i = 0; i < 16; i += 1) currents[i] = _device.busConfig.GetCurrent(i);
            WriteData(Device.ADDR_BUS_CONFIG + BusConfig.ADDR_CURRENT_AREA, currents);
            for (byte i = 0; i < 16; i += 1) _device.busConfig[(UInt32)(BusConfig.ADDR_CURRENT_AREA + i)].modified = false;
        }

        public void ReadBusConfigCurrentsData()
        {
            byte[] temp_currents = new byte[BusConfig.SIZE_CURRENT_AREA];
            if (ReadData(Device.ADDR_BUS_CONFIG + BusConfig.ADDR_CURRENT_AREA, ref temp_currents))
            {
                for (byte i = 0; i < 16; i += 1) _device.busConfig.SetCurrent(i, temp_currents[i]);
            }
        }

        public void SendCommandModified(MemLocation memLoc)
        {
            // only modified values will be written!
            if (memLoc.modified)
            {
                byte[] data = new byte[1];
                data[0] = (byte)memLoc.data; // command

                WriteData(memLoc.addr, data);

                memLoc.SetDataClearModified(0);

                Thread.Sleep(10);
            }
        }

        public void SendCommandReset()
        {
            _device.busConfig[BusConfig.ADDR_CMD_RESET].SetDataSetModified(0x59);
            SendCommandModified(_device.busConfig[BusConfig.ADDR_CMD_RESET]);
            _canCommRef.Reset();
            Thread.Sleep(100);

            _ResetComm();
        }

        public void SendCommandUnlockEeprom()
        {
            // read from device
            byte cmd = ReadDirectEepromUnlockKey();
            // send correct command sequence
            _device.busConfig[BusConfig.ADDR_SET_EEPROM_KEY].SetDataSetModified(cmd);
            SendCommandModified(_device.busConfig[BusConfig.ADDR_SET_EEPROM_KEY]);
            cmd = (byte)(~cmd & 0xFF);
            _device.busConfig[BusConfig.ADDR_SET_EEPROM_KEY].SetDataSetModified(cmd);
            SendCommandModified(_device.busConfig[BusConfig.ADDR_SET_EEPROM_KEY]);
        }

        public void ReadBusConfig()
        {
            byte[] rdata = new byte[_device.busConfig.Count];
            if (ReadData(Device.ADDR_BUS_CONFIG, ref rdata))
            {
                for (byte i = 0; i < rdata.Length; i += 1) _device.busConfig[(UInt32)i].SetDataClearModified(rdata[i]);
            }
        }

        public void WriteBusConfig(bool only_modified = true)
        {
            // direct write except commands
            byte[] wdata = new byte[1];
            for (byte i = 0; i < BusConfig.ADDR_SET_EEPROM_KEY; i += 1)
            {
                if (_device.busConfig[(UInt32)i].modified || !only_modified)
                {
                    wdata[0] = (byte)_device.busConfig[(UInt32)i].data;
                    WriteData(_device.busConfig[(UInt32)i].addr, wdata);
                    _device.busConfig[(UInt32)i].modified = false;
                }
            }

            // send commands with special handling
            for (ushort i = BusConfig.ADDR_SET_EEPROM_KEY; i <= BusConfig.ADDR_CMD_CLR_BUS_STATUS; i += 1) SendCommandModified(_device.busConfig[(UInt32)i]);
        }

        #endregion


        #region "BUS STATUS"

        public void ReadBusStatusVleds()
        {
            byte[] rdata = new byte[2 * 16]; // LW + HW
            if (ReadData(Device.ADDR_BUS_STATUS + BusStatus.ADDR_VLED_AREA, ref rdata))
            {
                for (byte i = 0; i < 32; i++) _device.busStatus[(UInt32)(BusStatus.ADDR_VLED_AREA + i)].SetDataClearModified(rdata[i]);
            }
        }

        public void ReadBusStatusVdifs()
        {
            byte[] rdata = new byte[2 * 16]; // LW + HW
            if (ReadData(Device.ADDR_BUS_STATUS + BusStatus.ADDR_VDIF_AREA, ref rdata))
            {
                for (byte i = 0; i < 32; i++) _device.busStatus[(UInt32)(BusStatus.ADDR_VDIF_AREA + i)].SetDataClearModified(rdata[i]);
            }
        }

        public void ReadBusStatusIleds()
        {
            byte[] rdata = new byte[2 * 16]; // LW + HW
            if (ReadData(Device.ADDR_BUS_STATUS + BusStatus.ADDR_ILED_AREA, ref rdata))
            {
                for (byte i = 0; i < 32; i++) _device.busStatus[(UInt32)(BusStatus.ADDR_ILED_AREA + i)].SetDataClearModified(rdata[i]);
            }
        }

        public void ReadBusStatusDiag()
        {
            byte[] rdata = new byte[20];
            if (ReadData(Device.ADDR_BUS_STATUS + BusStatus.ADDR_DIAG_AREA, ref rdata))
            {
                for (byte i = 0; i < 20; i++) _device.busStatus[(UInt32)(BusStatus.ADDR_DIAG_AREA + i)].SetDataClearModified(rdata[i]);
            }
        }

        public void ReadBusStatusMisc()
        {
            byte[] rdata = new byte[12];
            if (ReadData(Device.ADDR_BUS_STATUS + BusStatus.ADDR_MISC_AREA, ref rdata))
            {
                for (byte i = 0; i < 8; i++) _device.busStatus[(UInt32)(BusStatus.ADDR_MISC_AREA + i)].SetDataClearModified(rdata[i]);
                // there are some reserved bytes in between
                _device.busStatus[BusStatus.ADDR_ERROR_CODE].SetDataClearModified(rdata[11]);
            }
        }

        public void ReadBusStatus()
        {
            byte[] rdata = new byte[_device.busStatus.Count];
            if (ReadData(Device.ADDR_BUS_STATUS, ref rdata))
            {
                for (byte i = 0; i < rdata.Length; i += 1) _device.busStatus[(UInt32)i].SetDataClearModified(rdata[i]);
            }
        }

        #endregion

        #region "EEPROM"

        public byte ReadDirectEepromUnlockKey()
        {
            ushort addr = Device.ADDR_EEPROM + EEProm.ADDR_PARAMETER_EEPROM_KEYS;
            byte[] rdata = new byte[1];
            if (ReadData(addr, ref rdata))
            {
                return (byte)(rdata[0] & 0xFF);
            }
            return 0;
        }

        public void ReadEeprom()
        {
            ushort bytes = (EEProm.SIZE_CUSTOMER_AREA + EEProm.SIZE_PARAMETER_AREA);

            byte[] rdata = new byte[bytes];
            if (ReadData(Device.ADDR_EEPROM, ref rdata))
            {

                ushort words = (ushort)(bytes >> 2);

                for (ushort i = 0; i < words; i += 1)
                {
                    UInt32 temp;
                    temp = (UInt32)rdata[4 * i + 3]; temp <<= 8;
                    temp += (UInt32)rdata[4 * i + 2]; temp <<= 8;
                    temp += (UInt32)rdata[4 * i + 1]; temp <<= 8;
                    temp += (UInt32)rdata[4 * i + 0];
                    // limit to 25 bits
                    temp &= 0x1FFFFFF;

                    _device.eeprom[(UInt32)(4 * i)].SetDataClearModified(temp);
                }
            }
        }

        public BoolString WriteEeprom(bool only_modified = true)
        {
            bool prog_error = false;
            bool prog_access = false;
            bool timeout = false;
            bool prog_needed = false;

            for (ushort word = 0; word < _device.eeprom.Count; word += 1)
            {
                if (_device.eeprom.ElementAt(word).Value.modified || !only_modified)
                {
                    prog_needed = true;
                    if (!prog_error && !timeout)
                    {
                        byte[] wdata = new byte[4];
                        wdata[0] = (byte)((_device.eeprom.ElementAt(word).Value.data >> 0) & 0xFF);
                        wdata[1] = (byte)((_device.eeprom.ElementAt(word).Value.data >> 8) & 0xFF);
                        wdata[2] = (byte)((_device.eeprom.ElementAt(word).Value.data >> 16) & 0xFF);
                        wdata[3] = (byte)((_device.eeprom.ElementAt(word).Value.data >> 24) & 0xFF);

                        ushort waddr = (ushort)(Device.ADDR_EEPROM + _device.eeprom.ElementAt(word).Value.addr);
                        WriteData(waddr, wdata);

                        // poll prog status
                        int timeout_ms = TIMEOUT_EE_MS;
                        do
                        {
                            ReadBusStatusMisc();
                            System.Threading.Thread.Sleep(1);
                            timeout_ms--;
                        } while (!_device.busStatus.prog_done && (timeout_ms > 0));

                        if (_device.busStatus.prog_error) prog_error = true;
                        if (_device.busStatus.prog_access) prog_access = true;
                        if (timeout_ms == 0) timeout = true;

                        if (!prog_error && !timeout && !prog_access)
                        {
                            _device.eeprom.ElementAt(word).Value.modified = false;
                        }
                    }
                }
            }

            BoolString ret;
            ret.bval = false;
            ret.sval = "Internal Error occurred!";

            if (prog_needed)
            {
                if (prog_access)
                {
                    ret.sval = "Access Error occurred!";
                }
                else if (prog_error)
                {
                    ret.sval = "Prog Error occurred!";
                }
                else if (timeout)
                {
                    ret.sval = "Prog Timeout!";
                }
                else if (VerifyEeprom())
                {
                    ret.bval = true;
                    ret.sval = "Programming successful!";
                }
                else
                {
                    ret.sval = "Verify Error!";
                }
            }
            else
            {
                if (VerifyEeprom())
                {
                    ret.bval = true;
                    ret.sval = "Verify ok!";
                }
                else
                {
                    ret.sval = "Verify Error!";
                }
            }

            return ret;
        }

        public bool VerifyEeprom()
        {
            bool pass = false;
            ushort bytes = (EEProm.SIZE_CUSTOMER_AREA + EEProm.SIZE_PARAMETER_AREA);
            byte[] rdata = new byte[bytes];
            if (ReadData(Device.ADDR_EEPROM, ref rdata))
            {
                pass = true;
                ushort words = (ushort)(bytes >> 2);

                for (ushort i = 0; i < words; i += 1)
                {
                    UInt32 temp;
                    temp = (UInt32)rdata[4 * i + 3]; temp <<= 8;
                    temp += (UInt32)rdata[4 * i + 2]; temp <<= 8;
                    temp += (UInt32)rdata[4 * i + 1]; temp <<= 8;
                    temp += (UInt32)rdata[4 * i + 0];
                    // limit to 25 bits
                    temp &= 0x1FFFFFF;

                    MemLocation memLoc = _device.eeprom.FirstOrDefault(x => x.Value.addr == (4 * i)).Value;
                    if (memLoc != null)
                    {
                        if ((memLoc.data & 0x1FFFFFF) != temp)
                        {
                            pass = false;
                        }
                    }
                }
            }
            return pass;
        }

        #endregion
    }

}
