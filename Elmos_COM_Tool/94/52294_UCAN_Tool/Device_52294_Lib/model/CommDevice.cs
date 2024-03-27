using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Linq;

using FtdiLib;
using MemLib;
using Extensions;
using UcanCommLib;

namespace Device_52294_Lib
{
    public class CommBroadcast : CommDevice
    {
        public CommBroadcast(UcanComm ucanCommRef, UcanCommParameters commParamtersRef)
            : base(ucanCommRef, commParamtersRef, 0)
        {
        }
    }

    public class CommDevice
    {
        private UcanComm _ucanCommRef;
        private Device _device;
        private BitFlag _commError;
        public BitFlag _verifyError;
        private byte _node;

        private bool _SetCommError()
        {
            commError.SetValue(true);
            return false;
        }

        private bool _ClearCommError()
        {
            commError.SetValue(false);
            return true;
        }

        private void _SetVerifyError()
        {
            verifyError.SetValue(true);
            verifyError.SetValue(false);
        }


        public bool WriteDataHandleCommError(ushort addr, ushort[] data)
        {
            if (!_ucanCommRef.WriteData(_node, addr, data))
                return _SetCommError();
            else
                return _ClearCommError();
        }

        public bool ReadDataHandleCommError(ushort addr, ref ushort[] data)
        {
            if (!_ucanCommRef.ReadData(_node, addr, ref data))
                return _SetCommError();
            else
                return _ClearCommError();
        }

        public ushort[] VerifyData(ushort addr, ushort words, ushort mask, ushort expected)
        {
            ushort[] rdata = new ushort[words];
            bool pass = ReadDataHandleCommError(addr, ref rdata);
            if (pass)
            {
                for (int i = 0; i < words; i += 1)
                {
                    if ((rdata[i] & mask) != expected)
                    {
                        pass = false;
                        break;
                    }
                }
            }
            if (pass)
            {
                return new ushort[0];
            }
            else
            {
                _SetVerifyError();
                return rdata;
            }
        }

        public BitFlag commError
        {
            get { return _commError; }
        }

        public BitFlag verifyError
        {
            get { return _verifyError; }
        }

        public Device deviceRef
        {
            get { return _device; }
        }

        public byte node
        {
            set { _node = value; }
            get { return _node; }
        }

        public CommDevice(UcanComm ucanCommRef, UcanCommParameters commParamtersRef, byte node = 1)
        {
            _ucanCommRef = ucanCommRef;
            _device = new Device();
            _commError = new BitFlag(false);
            _verifyError = new BitFlag(false);
            _node = node;
        }

        #region "BUS CONFIG"

        public void WriteBusConfigPwmData(bool immediate = true)
        {
            ushort addr = immediate ? Device.ADDR_BUS_CONFIG_IMM : Device.ADDR_BUS_CONFIG_CMD;

            ushort[] pulses = new ushort[16];
            for (byte i = 0; i < 16; i += 1) pulses[i] = _device.busConfig.GetPulse(i);
            WriteDataHandleCommError((ushort) (addr + BusConfig.ADDR_PULSE_AREA), pulses);
            for (byte i = 0; i < 16; i += 1) _device.busConfig[(UInt32)(BusConfig.ADDR_PULSE_AREA + 2*i)].modified = false;
        }

        public void ReadBusConfigPwmData()
        {
            ushort[] temp_pulses = new ushort[16];
            if (ReadDataHandleCommError(Device.ADDR_BUS_CONFIG_IMM + BusConfig.ADDR_PULSE_AREA, ref temp_pulses))
            {
                for (byte i = 0; i < 16; i += 1) _device.busConfig.SetPulse(i, temp_pulses[i]);
            }
        }

        public void WriteBusConfigCurrentsData(bool immediate = true)
        {
            ushort addr = immediate ? Device.ADDR_BUS_CONFIG_IMM : Device.ADDR_BUS_CONFIG_CMD;

            ushort[] currents = new ushort[16];
            for (byte i = 0; i < 16; i += 1) currents[i] = _device.busConfig.GetCurrent(i);
            WriteDataHandleCommError((ushort) (addr + BusConfig.ADDR_CURRENT_AREA), currents);
            for (byte i = 0; i < 16; i += 1) _device.busConfig[(UInt32)(BusConfig.ADDR_CURRENT_AREA + 2*i)].modified = false;
        }

        public void ReadBusConfigCurrentsData()
        {
            ushort[] temp_currents = new ushort[16];
            if (ReadDataHandleCommError(Device.ADDR_BUS_CONFIG_IMM + BusConfig.ADDR_CURRENT_AREA, ref temp_currents))
            {
                for (byte i = 0; i < 16; i += 1) _device.busConfig.SetCurrent(i, temp_currents[i]);
            }
        }

        public void SendImmCommands(List<MemLocation> memLocs, bool do_wait = true)
        {
            // ASSUMPTION: memLocs are all modifed, TODO: check
            // ASSUMPTION: memLocs are all sorted and neighbored addresses, TODO: check and split into multiple commands for super function

            ushort[] data = new ushort[memLocs.Count];

            int i;
            for (i = 0; i < memLocs.Count; i += 1){
                data[i] = (ushort)(memLocs.ElementAt(i).data); // command
            }

            WriteDataHandleCommError((ushort)(Device.ADDR_BUS_CONFIG_IMM + memLocs.ElementAt(0).addr), data);

            for (i = 0; i < memLocs.Count; i += 1)
            {
                memLocs.ElementAt(i).SetDataClearModified(0);
            }

            if (do_wait)
                Thread.Sleep(10);
        }

        public void SendImmCommandModified(MemLocation memLoc, bool do_wait = true)
        {
            // only modified values will be written!
            if (memLoc.modified)
            {
                ushort[] data = new ushort[1];
                data[0] = (ushort)memLoc.data; // command

                WriteDataHandleCommError((ushort) (Device.ADDR_BUS_CONFIG_IMM + memLoc.addr), data);

                memLoc.SetDataClearModified(0);

                if (do_wait)
                    Thread.Sleep(10);
            }
        }

        public void SendImmCommandReset()
        {
            _device.busConfig[BusConfig.ADDR_CMD_RESET].SetDataSetModified(0x0259);
            SendImmCommandModified(_device.busConfig[BusConfig.ADDR_CMD_RESET]);
            _ucanCommRef.Reset();
            Thread.Sleep(100);
        }

        public void SendImmCommandUpdate(bool do_wait = true)
        {
            _device.busConfig[BusConfig.ADDR_CMD_UPDATE].SetDataSetModified(0x026A);
            SendImmCommandModified(_device.busConfig[BusConfig.ADDR_CMD_UPDATE], do_wait);
        }

        public void SendImmCommandSleep()
        {
            _device.busConfig[BusConfig.ADDR_CMD_SLEEP].SetDataSetModified(0x0295);
            SendImmCommandModified(_device.busConfig[BusConfig.ADDR_CMD_SLEEP]);
        }

        public void SendImmCommandWakeupAck()
        {
            _device.busConfig[BusConfig.ADDR_CMD_WAKEUP_ACK].SetDataSetModified(0x0296);
            SendImmCommandModified(_device.busConfig[BusConfig.ADDR_CMD_WAKEUP_ACK]);
        }

        public void SendImmCommandLedEnable(bool do_wait = true)
        {
            _device.busConfig[BusConfig.ADDR_LED_ENABLE_0_7].SetDataSetModified(0xFF);
            _device.busConfig[BusConfig.ADDR_LED_ENABLE_8_15].SetDataSetModified(0xFF);

            List<MemLocation> cmdList = new List<MemLocation>(0);
            cmdList.Add(_device.busConfig[BusConfig.ADDR_LED_ENABLE_0_7]);
            cmdList.Add(_device.busConfig[BusConfig.ADDR_LED_ENABLE_8_15]);
            SendImmCommands(cmdList, do_wait);
        }

        public void SendImmPulseAll(uint value, bool do_wait = true)
        {
            _device.busConfig[BusConfig.ADDR_BUS_PULSE_ALL].SetDataSetModified(value);
            SendImmCommandModified(_device.busConfig[BusConfig.ADDR_BUS_PULSE_ALL], do_wait);
        }

        public void SendImmCurrentAll(uint value, bool do_wait = true)
        {
            _device.busConfig[BusConfig.ADDR_BUS_CURRENT_ALL].SetDataSetModified(value);
            SendImmCommandModified(_device.busConfig[BusConfig.ADDR_BUS_CURRENT_ALL], do_wait);
        }

        public void SendImmPulseCurrentAll(uint pulseAll, uint currentAll, bool do_wait = true)
        {
            _device.busConfig[BusConfig.ADDR_BUS_PULSE_ALL].SetDataSetModified(pulseAll);
            _device.busConfig[BusConfig.ADDR_BUS_CURRENT_ALL].SetDataSetModified(currentAll);

            List<MemLocation> cmdList = new List<MemLocation>(0);
            cmdList.Add(_device.busConfig[BusConfig.ADDR_BUS_PULSE_ALL]);
            cmdList.Add(_device.busConfig[BusConfig.ADDR_BUS_CURRENT_ALL]);
            SendImmCommands(cmdList, do_wait);
        }

        public void ReadBusConfig()
        {
            ushort[] rdata = new ushort[_device.busConfig.Count];
            if (ReadDataHandleCommError(Device.ADDR_BUS_CONFIG_IMM, ref rdata))
            {
                for (byte i = 0; i < rdata.Length; i += 1) _device.busConfig[(UInt32)(2*i)].SetDataClearModified(rdata[i]);
            }
        }

        public void WriteBusConfig(bool only_modified = true)
        {
            // direct write except commands
            ushort[] wdata = new ushort[1];
            for (ushort addr = 0; addr <= BusConfig.ADDR_BUS_DERATE_GAIN; addr += 2)
            {
                if (_device.busConfig[(UInt32)addr].modified || !only_modified)
                {
                    wdata[0] = (ushort)_device.busConfig[(UInt32)addr].data;
                    WriteDataHandleCommError((ushort) (Device.ADDR_BUS_CONFIG_IMM + _device.busConfig[(UInt32)addr].addr), wdata);
                    _device.busConfig[(UInt32)addr].modified = false;
                }
            }

            // send commands with special handling
            for (ushort i = BusConfig.ADDR_CMD_SLEEP; i <= BusConfig.ADDR_CMD_UPDATE; i += 2) 
                SendImmCommandModified(_device.busConfig[(UInt32)i]);
        }

        #endregion

        #region "BUS STATUS"

        public void ReadBusStatusVleds()
        {
            ushort[] rdata = new ushort[16];
            if (ReadDataHandleCommError(Device.ADDR_BUS_STATUS + BusStatus.ADDR_VLED_AREA, ref rdata))
            {
                for (byte i = 0; i < rdata.Length; i++) _device.busStatus[(UInt32)(BusStatus.ADDR_VLED_AREA + 2 * i)].SetDataClearModified(rdata[i]);
            }
        }

        public void ReadBusStatusVdifs()
        {
            ushort[] rdata = new ushort[16];
            if (ReadDataHandleCommError(Device.ADDR_BUS_STATUS + BusStatus.ADDR_VDIF_AREA, ref rdata))
            {
                for (byte i = 0; i < rdata.Length; i++) _device.busStatus[(UInt32)(BusStatus.ADDR_VDIF_AREA + 2 * i)].SetDataClearModified(rdata[i]);
            }
        }

        public void ReadBusStatusIleds()
        {
            ushort[] rdata = new ushort[16];
            if (ReadDataHandleCommError(Device.ADDR_BUS_STATUS + BusStatus.ADDR_ILED_AREA, ref rdata))
            {
                for (byte i = 0; i < rdata.Length; i++) _device.busStatus[(UInt32)(BusStatus.ADDR_ILED_AREA + 2 * i)].SetDataClearModified(rdata[i]);
            }
        }

        public void ReadBusStatusDiag()
        {
            ushort[] rdata = new ushort[16];
            if (ReadDataHandleCommError(Device.ADDR_BUS_STATUS + BusStatus.ADDR_DIAG_AREA, ref rdata))
            {
                for (byte i = 0; i < rdata.Length; i++) _device.busStatus[(UInt32)(BusStatus.ADDR_DIAG_AREA + 2 * i)].SetDataClearModified(rdata[i]);
            }
        }

        public void ReadBusStatus()
        {
            ushort[] rdata = new ushort[_device.busStatus.Count];
            if (ReadDataHandleCommError(Device.ADDR_BUS_STATUS, ref rdata))
            {
                for (byte i = 0; i < rdata.Length; i += 1) _device.busStatus[(UInt32)(2*i)].SetDataClearModified(rdata[i]);
            }
        }

        #endregion

        #region "Parameters"

        public bool SelectPage(Device.PageSel sel)
        {
            _device.busConfig.SelectPage(sel);
            SendImmCommandModified(_device.busConfig[BusConfig.ADDR_PAGE_BASE_ADDR]);
            Thread.Sleep(20);
            ReadBusStatusDiag();
            if ((uint) sel == _device.busStatus.prog_base_addr_sel) return true;

            return _SetCommError();
        }

        public bool SetSramSel(bool sram_otp_n)
        {
            _device.busConfig.SetSramSel(sram_otp_n);
            SendImmCommandModified(_device.busConfig[BusConfig.ADDR_SRAM_SEL]);
            Thread.Sleep(20);
            ReadBusStatusDiag();
            if (sram_otp_n == _device.busStatus.prog_sram_sel) return true;

            return _SetCommError();
        }

        public void ReadParameters()
        {
            foreach (Device.PageSel page in Enum.GetValues(typeof(Device.PageSel)))
            {
                UInt32 words = 0x40;
                UInt32 word_offset = (UInt32)page * 2 * words;
                Memory mem = _device.parameters.standalone;
                if (page == Device.PageSel.BUS_DEFAULT) { mem = _device.parameters.busDefConfig; word_offset = 0; }
                if (page == Device.PageSel.STANDALONE_EXT) { mem = _device.parameters.standaloneExt; word_offset = 0; words = (UInt32)mem.Count; }

                if (SelectPage(page))
                {
                    ushort[] rdata = new ushort[words];
                    if (ReadDataHandleCommError(Device.ADDR_MAPPING, ref rdata))
                    {
                        for (byte i = 0; i < rdata.Length; i += 1) mem[(UInt32)(word_offset + 2 * i)].SetDataClearModified(rdata[i]);
                    }
                }
                else
                {
                    _SetCommError();
                }
            }
        } 

        public BoolString WriteParameters(bool only_modified = true)
        {
            bool prog_error = false;
            bool timeout = false;
            bool prog_needed = false;

            foreach (Device.PageSel page in Enum.GetValues(typeof(Device.PageSel)))
            {
                UInt32 words = 0x40;
                UInt32 page_offset = (UInt32)page * 2 * words;
                UInt32 word_offset = page_offset;
                Memory mem = _device.parameters.standalone;
                if (page == Device.PageSel.BUS_DEFAULT) { mem = _device.parameters.busDefConfig; word_offset = 0; }
                if (page == Device.PageSel.STANDALONE_EXT) { mem = _device.parameters.standaloneExt; word_offset = 0; words = (UInt32)mem.Count; }

                if (SelectPage(page))
                {
                    for (byte i = 0; i < words; i += 1)
                    {
                        UInt32 mapping_addr = (UInt32)(word_offset + 2 * i);
                        if (mem[mapping_addr].modified || !only_modified)
                        {
                            prog_needed = true;
                            if (!prog_error && !timeout)
                            {
                                ushort waddr = (ushort)(Device.ADDR_MAPPING + 2 * i);
                                ushort[] wdata = new ushort[1];
                                wdata[0] = (ushort) mem[mapping_addr].data;
                                WriteDataHandleCommError(waddr, wdata);

                                // poll prog status

                                int timeout_ms = 10;    // max elapsedMs to wait for busy
                                do
                                {
                                    ReadBusStatusDiag();
                                    System.Threading.Thread.Sleep(1);
                                    timeout_ms--;
                                } while (!_device.busStatus.prog_busy && (timeout_ms > 0));

                                timeout_ms = 40;    // max elapsedMs to wait for busy done
                                do
                                {
                                    ReadBusStatusDiag();
                                    System.Threading.Thread.Sleep(1);
                                    timeout_ms--;
                                } while (_device.busStatus.prog_busy && (timeout_ms > 0));

                                if (_device.busStatus.prog_error) prog_error = true;
                                if (timeout_ms == 0) timeout = true;

                                System.Threading.Thread.Sleep(20);  // wait after write command
                            }
                        }
                    }
                }
                else
                {
                    _SetCommError();
                }
            }

            BoolString ret;
            ret.bval = false;
            ret.sval = "Internal Error Occurred!";

            if (prog_needed)
            {
                if (prog_error)
                {
                    ret.sval = "Prog Error occurred!";
                }
                else if (timeout)
                {
                    ret.sval = "Prog Timeout!";
                }
                else if (VerifyParameters())
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
                ret.bval = true;
                ret.sval = "Nothing to program!";
            }

            return ret;
        }

        public bool VerifyParameters()
        {
            foreach (Device.PageSel page in Enum.GetValues(typeof(Device.PageSel)))
            {
                UInt32 words = 0x40;
                UInt32 word_offset = (UInt32)page * 2 * words;
                Memory mem = _device.parameters.standalone;
                if (page == Device.PageSel.BUS_DEFAULT) { mem = _device.parameters.busDefConfig; word_offset = 0; }
                if (page == Device.PageSel.STANDALONE_EXT) { mem = _device.parameters.standaloneExt; word_offset = 0; words = (UInt32)mem.Count; }

                if (SelectPage(page))
                {
                    ushort[] rdata = new ushort[words];
                    if (ReadDataHandleCommError(Device.ADDR_MAPPING, ref rdata))
                    {
                        for (byte i = 0; i < rdata.Length; i += 1)
                        {
                            if (rdata[i] != mem[(UInt32)(word_offset + 2 * i)].data){
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    _SetCommError();
                    return false;
                }
            }
            return true;
        }

        public void ReadDeviceInfo()
        {
            byte page = (byte)(Standalone.ADDR_DEVICE_INFO / Device.MAPPING_PAGE_BYTES);
            Device.PageSel pageSel = (Device.PageSel) (page);
            SelectPage(pageSel);

            ushort pageOffset = (ushort)(Standalone.ADDR_DEVICE_INFO - (page - 1) * Device.MAPPING_PAGE_BYTES);

            ushort[] rdata = new ushort[1];
            if (ReadDataHandleCommError((ushort) (Device.ADDR_MAPPING + pageOffset), ref rdata))
            {
                _device.parameters.standalone[Standalone.ADDR_DEVICE_INFO].SetDataClearModified(rdata[0]);
            }

        }

        #endregion
    }

}
