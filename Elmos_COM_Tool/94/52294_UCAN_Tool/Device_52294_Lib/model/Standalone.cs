using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using MemLib;

namespace Device_52294_Lib
{
    public class Standalone : Memory
    {
        private string LABEL_BIN_CLASS_CONFIG = "BIN_CLASS_CONFIG";
        private string LABEL_PWM_PRESCALER = "PWM_PRESCALER";
        private string LABEL_PWM_PERIOD = "PWM_PERIOD";
        private string LABEL_PWM_CONFIG = "PWM_CONFIG";
        private string LABEL_PWM_COMBINE_PRI = "PWM_COMBINE_PRI";
        private string LABEL_PWM_COMBINE_SEC = "PWM_COMBINE_SEC";
        private string LABEL_PWMIN_TIMING = "PWMIN_TIMING";
        private string LABEL_PWMIN_CONFIG = "PWMIN_CONFIG";
        private string LABEL_VS_TOO_LOW = "VS_TOO_LOW";
        private string LABEL_VS_CRITICAL = "VS_CRITICAL";
        private string LABEL_VT_CRITICAL = "VT_CRITICAL";
        private string LABEL_ILED_OPEN_THR = "ILED_OPEN_THR";
        private string LABEL_COM_TIMEOUT = "COM_TIMEOUT";
        private string LABEL_UART_CONFIG = "UART_CONFIG";
        private string LABEL_RESET_BEHAVIOR = "RESET_BEHAVIOR";
        private string LABEL_UART_BAUDRATE = "UART_BAUDRATE";
        private string LABEL_ISINK_CONFIG = "ISINK_CONFIG";
        private string LABEL_DEVICE_INFO = "DEVICE_INFO ";
        private string LABEL_VS_DERATE_RANGE = "VS_DERATE_RANGE";
        private string LABEL_DERATE_GAIN = "DERATE_GAIN";
        private string LABEL_IO_CONFIG = "IO_CONFIG";
        private string LABEL_DIAG_CONFIG = "DIAG_CONFIG";
        private string LABEL_VT_DERATE_START = "VT_DERATE_START";
        private string LABEL_VT_DERATE_STOP = "VT_DERATE_STOP";
        private string LABEL_DIAG_CURRENT = "DIAG_CURRENT";
        private string LABEL_ISINK_BUNDLE = "ISINK_BUNDLE";
        
        private string LABEL_IC_VERSION = "ic_version";
        private string LABEL_START = "start";
        private string LABEL_VS_GAIN = "vs_gain";
        private string LABEL_VT_GAIN = "vt_gain";
        private string LABEL_OE_INV = "oe_inv";
        private string LABEL_PWMIN_LP_INV = "pwmin_lp_inv";
        private string LABEL_PWMIN_HP_INV = "pwmin_hp_inv";
        private string LABEL_LEVEL = "level";
        private string LABEL_DIAG0_SEL = "diag0_sel";
        private string LABEL_DIAG1_SEL = "diag1_sel";
        private string LABEL_SLM0 = "slm0";
        private string LABEL_SLM1 = "slm1";
        private string LABEL_SLEW = "slew";
        private string LABEL_LP_DIRECT = "lp_direct";
        private string LABEL_HP_DIRECT = "hp_direct";
        private string LABEL_LPFB_HPFB = "lpfb_hpfb";
        private string LABEL_DERATE_SUP = "derate_sup";
        private string LABEL_NUMBER_RETRIES = "number_retries";
        private string LABEL_UNRESPONSIVE_TIME = "unresponsive_time";
        private string LABEL_FORMAT = "format";
        private string LABEL_TURN_AROUND = "turn_around";
        private string LABEL_PARITY = "parity";
        private string LABEL_STOP = "stop";
        private string LABEL_BREAK_SEL = "break_sel";
        private string LABEL_AUTO_BAUD = "auto_baud";
        private string LABEL_VAL = "val";
        private string LABEL_TIMEBASE = "timebase";
        private string LABEL_DIAG0_E = "diag0_e";
        private string LABEL_DIAG1_E = "diag1_e";
        private string LABEL_PIN_SEL = "pin_sel";
        private string LABEL_ENABLE = "enable";
        private string LABEL_CURRENT_SEL = "current_sel";
        private string LABEL_OE_MASK_BUST = "oe_mask_bust";
        private string LABEL_TIMING = "timing";
        private string LABEL_MIN = "min";
        private string LABEL_MAX = "max";
        private string LABEL_BUST_PRIO_SCHEME = "bust_prio_scheme";

        internal const ushort ADDR_STANDALONE_DIAG0_CONFIG_0_7 = 0x152;
        internal const ushort ADDR_STANDALONE_DIAG0_CONFIG_8_15 = 0x154;
        internal const ushort ADDR_STANDALONE_DIAG1_CONFIG_0_7 = 0x156;
        internal const ushort ADDR_STANDALONE_DIAG1_CONFIG_8_15 = 0x158;
        internal const ushort ADDR_STANDALONE_PWM_COMBINE_PRI = 0xBE;
        internal const ushort ADDR_STANDALONE_PWM_COMBINE_SEC = 0xC0;
        internal const ushort ADDR_STANDALONE_PWM_PRESCALER = 0xB8;
        internal const ushort ADDR_STANDALONE_PWM_PERIOD = 0xBA;
        internal const ushort ADDR_STANDALONE_BIN_CLASS_ENABLE_0_7 = 0xA2;
        internal const ushort ADDR_STANDALONE_BIN_CLASS_ENABLE_8_15 = 0xA4;
        public const ushort ADDR_DEVICE_INFO = 0x17A; // TODO internal

        public uint ic_version
        {
            get { return this[ADDR_DEVICE_INFO].GetBitfield("ic_version").GetData(); }
        }

        public uint fw_version
        {
            get { return this[ADDR_DEVICE_INFO].GetBitfield("fw_version").GetData(); }
        }

        internal uint pwm_prescaler
        {
            get { 
                uint ret = this[ADDR_STANDALONE_PWM_PRESCALER].data;
                if (ret == 0)
                    ret = 25;
                return ret;
            }
        }

        public uint pwm_period
        {
            get
            {
                uint ret = this[ADDR_STANDALONE_PWM_PERIOD].data;
                if (ret == 0)
                    ret = 1023; 
                return ret;
            }
        }

        internal uint bin_class_enables
        {
            get
            {
                uint temp = this[ADDR_STANDALONE_BIN_CLASS_ENABLE_8_15].data;
                temp <<= 8;
                temp += this[ADDR_STANDALONE_BIN_CLASS_ENABLE_0_7].data;
                return temp;
            }
        }

        internal uint diag0_config
        {
            get
            {
                uint temp = this[ADDR_STANDALONE_DIAG0_CONFIG_8_15].data;
                temp <<= 8;
                temp += this[ADDR_STANDALONE_DIAG0_CONFIG_0_7].data;
                return temp;
            }
        }

        internal uint diag1_config
        {
            get
            {
                uint temp = this[ADDR_STANDALONE_DIAG1_CONFIG_8_15].data;
                temp <<= 8;
                temp += this[ADDR_STANDALONE_DIAG1_CONFIG_0_7].data;
                return temp;
            }
        }

        internal uint pwm_combine_pri
        {
            get { return this[ADDR_STANDALONE_PWM_COMBINE_PRI].data; }
        }

        internal uint pwm_combine_sec
        {
            get { return this[ADDR_STANDALONE_PWM_COMBINE_SEC].data; }
        }

        private String _LpfbPulseDesc(byte led, MemLocation location)
        {
            return String.Format("LED {0:D} Low Priority Fallback Pulse", led);
        }

        private String _HpfbPulseDesc(byte led, MemLocation location)
        {
            return String.Format("LED {0:D} High Priority Fallback Pulse", led);
        }

        private String _BusTPulseDesc(byte led, MemLocation location)
        {
            return String.Format("LED {0:D} Bus Timeout Pulse", led);
        }

        private String _LpfbCurrentDesc(byte led, MemLocation location)
        {
            return String.Format("LED {0:D} Low Priority Fallback Current = {1:F} mA", led, Convert.ToDouble(location.data) * 0.1);
        }

        private String _HpfbCurrentDesc(byte led, MemLocation location)
        {
            return String.Format("LED {0:D} High Priority Fallback Current = {1:F} mA", led, Convert.ToDouble(location.data) * 0.1);
        }

        private String _BusTCurrentDesc(byte led, MemLocation location)
        {
            return String.Format("LED {0:D} Bus Timeout Current = {1:F} mA", led, Convert.ToDouble(location.data) * 0.1);
        }

        private String _LedBinGainDesc(byte led, MemLocation location)
        {
            double gain = 1.0;
            if (location.data > 0)
            {
                gain = Convert.ToDouble(location.data) / Convert.ToDouble(0x200);
            }

            return String.Format("LED {0:D} Binning Gain Factor = {1:F}", led, gain);
        }

        private String _ClassBinLevelDesc(byte binClass, MemLocation location)
        {
            return String.Format("Binning Group 0+1 Class {0:D}/{1:D} Level = {2:F} V", binClass, binClass+1, Convert.ToDouble(location.data) / Device.LSB_VLED);
        }

        private String _ClassBinPinSelDesc(MemBitfield bitfield)
        {
            return String.Format("Binning Group 0 Class determined by LED {0:D} pin.", bitfield.GetData());
        }

        private String _ClassBinEnableDesc(MemBitfield bitfield)
        {
            if (bitfield.GetBool())
                return "Binning Group 0 Class evaluation enabled.";
            else
                return "Binning Group 0 Class evaluation disabled!";
        }

        private String _ClassBinCurrentSelDesc(MemBitfield bitfield)
        {
            return String.Format("Binning Group 0 Class evaluation current = {0:F} mA", Convert.ToDouble(bitfield.GetData()) * 3.2);
        }

        private String _ClassBinEnablesDesc()
        {
            if (bin_class_enables == 0)
                return "Binning Group 0 Class not applied to any channels!";

            String ret = "Binning Group 0 Class applied to Channels = ";
            bool first = true;
            for (int i = 0; i < 16; i += 1)
            {
                if (((uint)(bin_class_enables >> i) & 1) == 1)
                {
                    if (!first)
                        ret += "+";
                    ret += String.Format("{0:D}", i);
                    first = false;
                }
            }
            return ret;
        }

        private String _ClassBinGainDesc(byte binClass, MemLocation location)
        {
            double gain = Convert.ToDouble(location.data) / Convert.ToDouble(0x200);
            return String.Format("Binning Group 0 Class {0:D} Gain Factor = {1:F}", binClass, gain);
        }

        private String _PwmFreqDesc()
        {
            double freq = 8000000.0 / (Convert.ToDouble(pwm_period) * (1.0 + Convert.ToDouble(pwm_prescaler)));
            return String.Format("PWM Frequency = {0:F} Hz", freq);
        }

        private String _PwmTimingDesc(MemBitfield bitfield)
        {
            if (bitfield.GetBool())
                return "Equidistant distribution of PWM channel starting point over PWM period.";
            else
                return "No PWM inter channel start delay. All channel pulses start at the same time.";
        }

        private String _PwmOeMaskBusTDesc(MemBitfield bitfield)
        {
            if (bitfield.GetBool())
                return "OE masks LED channels, which are in bus timeout mode.";
            else
                return "OE has no influence in bus timeout mode LED channels.";
        }

        private String _PwmCombineDesc()
        {
            if ((pwm_combine_pri == 0) && (pwm_combine_sec == 0))
                return "No Channels combined!";

            String ret = "Combined Channels = ";
            bool in_grp = false;
            for (int i = 0; i < 16; i += 1)
            {
                uint val;
                if ((i & 1) == 1)
                    val = (uint)(pwm_combine_sec >> (i >> 1)) & 1;
                else
                    val = (uint)(pwm_combine_pri >> (i >> 1)) & 1;

                if (val == 1)
                {
                    if (!in_grp)
                        ret += " ";
                    ret += String.Format("{0:D}+", i);
                    in_grp = true;
                }
                else
                {
                    if (in_grp)
                        ret += String.Format("{0:D} ", i);
                    in_grp = false;
                }
            }
            return ret;
        }

        private String _DiagOpenThrDesc(byte sel, MemLocation location)
        {
            double thr = Convert.ToDouble(location.data) / Convert.ToDouble(Device.LSB_VLED);
            return String.Format("Diag LED OPEN Threshold {0:D} level = {1:F} V", sel, thr);
        }

        private String _DiagShortThrDesc(byte sel, MemLocation location)
        {
            double thr = Convert.ToDouble(location.data) / Convert.ToDouble(Device.LSB_VLED);
            return String.Format("Diag LED SHORT Threshold {0:D} level = {1:F} V", sel, thr);
        }

        private String _LedIdacRefSelDesc(byte led, MemBitfield bitfield)
        {
            if (bitfield.GetBool())
                return String.Format("LED {0:D} driver configured to lower range", led);
            else
                return String.Format("LED {0:D} driver configured to upper range", led);
        }

        private String _LedSupplySelDesc(byte led, MemBitfield bitfield)
        {
            switch (bitfield.GetData())
            {
                case 0: return String.Format("VS selected as LED {0:D} supply", led);
                case 1: return String.Format("PWMIN_LP selected as LED {0:D} supply", led);
                case 2: return String.Format("PWMIN_HP selected as LED {0:D} supply", led);
            }
            return "Reserved, do not use !";
        }

        private String _LedDiagOpenSelDesc(byte led, MemBitfield bitfield)
        {
            switch (bitfield.GetData())
            {
                case 0: return String.Format("OPEN detection disabled for LED {0:D}!", led);
                case 1: return String.Format("LED_OPEN_THR_1 level is used for LED {0:D} OPEN detection", led);
                case 2: return String.Format("LED_OPEN_THR_2 level is used for LED {0:D} OPEN detection", led);
                case 3: return String.Format("LED_OPEN_THR_3 level is used for LED {0:D} OPEN detection", led);
            }
            return "Reserved, do not use !";
        }

        private String _LedDiagShortSelDesc(byte led, MemBitfield bitfield)
        {
            switch (bitfield.GetData())
            {
                case 0: return String.Format("SHORT detection disabled for LED {0:D}!", led);
                case 1: return String.Format("LED_SHORT_THR_1 level is used for LED {0:D} SHORT detection", led);
                case 2: return String.Format("LED_SHORT_THR_2 level is used for LED {0:D} SHORT detection", led);
                case 3: return String.Format("LED_SHORT_THR_3 level is used for LED {0:D} SHORT detection", led);
            }
            return "Reserved, do not use !";
        }

        private String _PwmInTimingDesc(MemBitfield bitfield)
        {
            MemLocation memLoc = bitfield.GetMemLocation();
            MemBitfield memBfMin = memLoc.GetBitfield(LABEL_MIN);
            MemBitfield memBfMax = memLoc.GetBitfield(LABEL_MAX);

            uint minPeriod_us = memBfMin.GetData() * 256;
            uint maxPeriod_us = memBfMax.GetData() * 256;

            if (minPeriod_us >= maxPeriod_us)
                return "Invalid configuration!";

            return String.Format("Accepted PWMIN Period = {0:D} ... {1:D} us.", minPeriod_us, maxPeriod_us);
        }

        private String _PwmInLedLpEnableDesc(byte led, MemBitfield bitfield)
        {
            if (bitfield.GetBool())
                return String.Format("LED {0:D} uses PWMIN_LP signal.", led);
            else
                return String.Format("LED {0:D} does not use PWMIN_LP signal.", led);
        }

        private String _PwmInLedHpEnableDesc(byte led, MemBitfield bitfield)
        {
            if (bitfield.GetBool())
                return String.Format("LED {0:D} uses PWMIN_HP signal.", led);
            else
                return String.Format("LED {0:D} does not use PWMIN_HP signal.", led);
        }

        private String _PwmInBustTPrioSchemeDesc(MemBitfield bitfield)
        {
            if (bitfield.GetBool())
                return "PWMIN pins are only considered when the device is in bus timeout mode.";
            else
                return "PWMIN pins have higher priority than the bus configuration and a bus timeout.";
        }

        private String _DiagVsTooLowDesc(MemLocation location)
        {
            if (location.data == 0)
                return "\"VS too low\" evaluation is disabled!";
            else
                return String.Format("\"VS too low\" flag will be set when VS is measured lower than {0:F} V.", Convert.ToDouble(location.data) / Device.LSB_VSUP);
        }

        private String _DiagVsCriticalDesc(MemLocation location)
        {
            if (location.data == 0)
                return "\"VS critical\" evaluation is disabled!";
            else
                return String.Format("\"VS critical\" flag will be set when VS is measured higher than {0:F} V.", Convert.ToDouble(location.data) / Device.LSB_VSUP);
        }

        private String _DiagVtCriticalDesc(MemLocation location)
        {
            int temp_C = Convert.ToInt32(location.data) - 273;
            if (location.data == 0)
                return "\"VT critical\" evaluation is disabled!";
            else
                return String.Format("\"VT critical\" flag will be set when {0:D} K ({1:D} °C) are exceeded.", location.data, temp_C);
        }

        private String _DiagIledOpenDesc(MemLocation location)
        {
            if (location.data == 0)
                return "ILED open evaluation is disabled!";
            else
                return String.Format("\"LED open\" flag will be set when LED current higher than {0:F} mA.", Convert.ToDouble(location.data) / 10.0);
        }

        private String _ComTimeoutDesc(MemBitfield bitfield)
        {
            MemLocation memLoc = bitfield.GetMemLocation();
            MemBitfield memBfBase = memLoc.GetBitfield(LABEL_TIMEBASE);
            MemBitfield memBfVal = memLoc.GetBitfield(LABEL_VAL);

            uint time = 1;                          // 1ms
            if (memBfBase.GetBool()) time *= 8;     // 8ms

            time *= (memBfVal.GetData() + 1);

            if (memBfVal.GetData() == 0)
                return "Bus Timeout disabled!";
            else
                return String.Format("Bus Timeout after {0:D} ms.", time);
        }

        private String _ComTimeoutDiagDesc(byte sel, MemBitfield bitfield)
        {
            if (bitfield.GetBool())
                return String.Format("A bus communication timeout asserts DIAG{0:D}.", sel);
            else
                return String.Format("A bus communication timeout has no effect on DIAG{0:D}", sel);
        }

        private String _UartConfigFormatDesc(MemBitfield bitfield)
        {
            if (bitfield.GetBool())
                return "UART protocol uses 4 byte header format (CRC-8 variant)";
            else
                return "UART protocol uses 3 byte header format (CRC-6 variant)";
        }

        private String _UartConfigTurnAroundDesc(MemBitfield bitfield)
        {
            if (bitfield.GetData() == 0)
                return "UART protocol uses no additional turn-around time (only with auto_baud=0)";
            else
                return String.Format("UART protocol uses additional turn around time at change of transmit direction = {0:F}", Convert.ToDouble(bitfield.GetData()) * 0.5);
        }

        private String _UartConfigParityDesc(MemBitfield bitfield)
        {
            switch (bitfield.GetData())
            {
                case 0: return "UART bytes with Even Parity bit";
                case 1: return "UART bytes with Odd Parity bit";
                case 2: return "UART bytes with Zero Parity bit (always '0')";
                case 3: return "UART bytes with No Parity bit";
            }
         return "Invalid.";
        }

        private String _UartConfigStopBitsDesc(MemBitfield bitfield)
        {
            if (bitfield.GetBool())
                return "UART bytes with 2 stop bits";
            else
                return "UART bytes with 1 stop bit";
        }

        private String _UartConfigBreakSelDesc(MemBitfield bitfield)
        {
            if (bitfield.GetBool())
                return "UART uses 11 Break Bits";
            else
                return "UART uses 9.5 Break Bits (not with Parity bit)";
        }

        private String _UartConfigAutoBaudDesc(MemBitfield bitfield)
        {
            if (bitfield.GetBool())
                return "UART uses automatic baudrate adaption from SYNC byte";
            else
                return "UART uses fixed baudrate";
        }

        private String _UartBaudrateDesc(MemLocation memLoc)
        {
            double baudrate = 48000 / Convert.ToDouble(memLoc.data) / 2;

            if (memLoc.data == 0)
                baudrate = 500;

            return String.Format("UART interface baud_rate = {0:F} kbit/s", baudrate);
        }

        private String _ResetRetriesDesc(MemBitfield bitfield)
        {
            switch (bitfield.GetData())
            {
                case 0: return "3 times reset retry, after next reset go to unresponsive time";
                case 1: return "1 time reset retry, after next reset go to unresponsive time";
                case 2: return "2 times reset retry, after next reset go to unresponsive time";
                case 3: return "3 times reset retry, after next reset go to unresponsive time";
                case 4: return "4 times reset retry, after next reset go to unresponsive time";
                case 5: return "5 times reset retry, after next reset go to unresponsive time";
                case 6: return "6 times reset retry, after next reset go to unresponsive time";
                case 7: return "7 times reset retry, after next reset go to unresponsive time";
                case 8: return "8 times reset retry, after next reset go to unresponsive time";
                case 9: return "9 times reset retry, after next reset go to unresponsive time";
                case 10: return "10 times reset retry, after next reset go to unresponsive time";
                case 11: return "11 times reset retry, after next reset go to unresponsive time";
                case 12: return "12 times reset retry, after next reset go to unresponsive time";
                case 13: return "13 times reset retry, after next reset go to unresponsive time";
                case 14: return "14 times reset retry, after next reset go to unresponsive time";
                case 15: return "reset retry loop, no unresponsive time, no matter how RESET_BEHAVIOR.unresponsive_time is configured";
            }
            return "reserved";
        }

        private String _ResetUnresponsiveDesc(MemBitfield bitfield)
        {
            switch (bitfield.GetData())
            {
                case 0: return "infinite unresponsive time, can only be left by power cycle";
                case 1: return "~100ms unresponsive time, after that start up again";
                case 2: return "~200ms unresponsive time, after that start up again";
                case 3: return "~300ms unresponsive time, after that start up again";
                case 4: return "~500ms unresponsive time, after that start up again";
                case 5: return "~1s unresponsive time, after that start up again";
                case 6: return "~2s unresponsive time, after that start up again";
                case 7: return "~4s unresponsive time, after that start up again";
                case 8: return "~10s unresponsive time, after that start up again";
            }
            return "reserved";
        }

        private String _DerateVtStartDesc(MemLocation location)
        {
            int temp_C = Convert.ToInt32(location.data) - 273;
            if (location.data == 0)
                return "Temperature derating is disabled!";
            else
                return String.Format("Temperature derating starts at {0:D} K ({1:D} °C).", location.data, temp_C);
        }

        private String _DerateVtStopDesc(MemLocation location)
        {
            int temp_C = Convert.ToInt32(location.data) - 273;
            if (location.data == 0)
                return "Temperature derating is disabled!";
            else
                return String.Format("Temperature derating stops at {0:D} K ({1:D} °C).", location.data, temp_C);
        }

        private String _DerateVtSGainDesc(MemBitfield bitfield)
        {
            if (bitfield.GetData() == 0)
                return "Temperature derating is disabled!";
            else
                return String.Format("Temperature derating gain factor = {0:F} %/K.", Convert.ToDouble(bitfield.GetData()) / 256.0 * 100.0);
        }

        private String _DerateVsStartDesc(MemBitfield bitfield)
        {
            if (bitfield.GetData() == 0)
                return "Supply derating is disabled!";
            else
                return String.Format("Supply derating starts at {0:D} V.", bitfield.GetData());
        }

        private String _DerateVsStopDesc(MemBitfield bitfield)
        {
            if (bitfield.GetData() == 0)
                return "Supply derating is disabled!";
            else
                return String.Format("Supply derating stops at {0:D} V.", bitfield.GetData());
        }

        private String _DerateVsSGainDesc(MemBitfield bitfield)
        {
            if (bitfield.GetData() == 0)
                return "Supply derating is disabled!";
            else
                return String.Format("Supply derating gain factor = {0:F} %/V.", Convert.ToDouble(bitfield.GetData()) * Convert.ToDouble(Device.LSB_VSUP) / (16 * 256) * 100.0);
        }

        private String _DiagCurrent(MemLocation location)
        {
            return String.Format("DIAG pin driver current = {0:F} mA.", Convert.ToDouble(location.data) * 0.1 );
        }

        private String _IsinkAnaBundle(MemLocation location)
        {
            if (location.data == 0x0000) return "No Analog Bundling configured!";
            String ret = "Analog Bundled Channels =";
            for (int i = 0; i < 8; i += 1)
            {
                if (((location.data >> i) & 1) == 1)
                {
                    ret += String.Format(" {0:D}+{1:D} ", 2 * i, 2 * i + 1);
                }
            }
            return ret;
        }

        private String _IsinkConfigLpDirectDesc(MemBitfield bitfield)
        {
            switch (bitfield.GetData())
            {
                case 0: return "low priority fallback (LPFB) data is used in low priority direct mode";
                case 1: return "high priority fallback (HPFB) data is used in low priority direct mode";
                case 2: return "bus configuration data is used in low priority direct mode";
                case 3: return "bus timeout configuration data is used in low priority direct mode";
            }
            return "reserved";
        }

        private String _IsinkConfigHpDirectDesc(MemBitfield bitfield)
        {
            switch (bitfield.GetData())
            {
                case 0: return "high priority fallback (HPFB) data is used in high priority direct mode";
                case 1: return "low priority fallback (LPFB) data is used in high priority direct mode";
                case 2: return "bus configuration data is used in high priority direct mode";
                case 3: return "bus timeout configuration data is used in high priority direct mode";
            }
            return "reserved";
        }

        private String _IsinkConfigLpfbHpfbDesc(MemBitfield bitfield)
        {
            switch (bitfield.GetData())
            {
                case 0: return "high priority fallback (HPFB) data is used when both PWMIN_LP and PWMIN_HP signal fallback";
                case 1: return "low priority fallback (LPFB) data is used when both PWMIN_LP and PWMIN_HP signal fallback";
                case 2: return "bus configuration data is used when both PWMIN_LP and PWMIN_HP signal fallback";
                case 3: return "bus timeout configuration data is used when both PWMIN_LP and PWMIN_HP signal fallback";
            }
            return "reserved";
        }

        private String _IsinkConfigDerateSupDesc(MemBitfield bitfield)
        {
            switch (bitfield.GetData())
            {
                case 0: return "VS supply is used for derating evaluation";
                case 1: return "VS supply is used for derating evaluation";
                case 2: return "PWMIN_LP supply is used for derating evaluation";
                case 3: return "PWMIN_HP supply is used for derating evaluation";
            }
            return "reserved";
        }

        private String _IsinkDeviceInfoIcVersionDesc(MemBitfield bitfield)
        {
            switch (bitfield.GetData())
            {
                case 0: return "Device is M522.94A / M522.94B";
                case 1: return "Device is E522.94A";
                case 2: return "Device is E522.94B";
            }
            return "reserved";
        }

        private String _IoConfigOeInvDesc(MemBitfield bitfield)
        {
            if (bitfield.GetBool())
                return "OE pin inverted and pull-up";
            else
                return "OE pin not inverted and pull-down";
        }

        private String _IoConfigPwmInLpInvDesc(MemBitfield bitfield)
        {
            if (bitfield.GetBool())
                return "PWMIN_LP pin inverted and pull-up";
            else
                return "PWMIN_LP pin not inverted and pull-down";
        }

        private String _IoConfigPwmInHpInvDesc(MemBitfield bitfield)
        {
            if (bitfield.GetBool())
                return "PWMIN_HP pin inverted and pull-up";
            else
                return "PWMIN_HP pin not inverted and pull-down";
        }

        private String _DiagConfigLevelDesc(MemBitfield bitfield)
        {
            if (bitfield.GetData() == 0)
                return "Diagnosis disabled!";
            else
                return "DIAG error counter level";
        }

        private String _DiagConfigDiagSelDesc(byte sel, MemBitfield bitfield)
        {
            if (bitfield.GetBool())
                return String.Format("LED{0:D} pin used as DIAG{0:D} pin.", sel);
            else
                return String.Format("LED{0:D} pin used as LED pin.", sel);
        }

        private String _DiagConfigSlmDesc(byte sel, MemBitfield bitfield)
        {
            if (bitfield.GetBool())
                return String.Format("Diagnosis Group {0:D} is in single-lamp-mode (SLM).", sel);
            else
                return String.Format("Diagnosis Group {0:D} is in multi-lamp-mode (MLM).", sel);
        }

        private String _DiagConfigDesc(byte sel)
        {
            uint diag_config = (sel==1) ? diag1_config : diag0_config;

            if (diag_config == 0)
                return String.Format("DIAG{0:D} diagnosis is not enabled for any channels!", sel);

            String ret = String.Format("DIAG{0:D} diagnosis is enabled for channels = ", sel);
            bool first = true;
            for (int i = 0; i < 16; i += 1)
            {
                if (((uint)(diag_config >> i) & 1) == 1)
                {
                    if (!first)
                        ret += "+";
                    ret += String.Format("{0:D}", i);
                    first = false;
                }
            }
            return ret;
        }


        public override String Description(MemLocation memLoc)
        {
            if (memLoc.name == LABEL_PWM_PRESCALER) return _PwmFreqDesc();
            if (memLoc.name == LABEL_PWM_PERIOD) return _PwmFreqDesc();
            if (memLoc.name == LABEL_PWM_COMBINE_PRI) return _PwmCombineDesc();
            if (memLoc.name == LABEL_PWM_COMBINE_SEC) return _PwmCombineDesc();
            if (memLoc.name == LABEL_VS_TOO_LOW) return _DiagVsTooLowDesc(memLoc);
            if (memLoc.name == LABEL_VS_CRITICAL) return _DiagVsCriticalDesc(memLoc);
            if (memLoc.name == LABEL_VT_CRITICAL) return _DiagVtCriticalDesc(memLoc);
            if (memLoc.name == LABEL_ILED_OPEN_THR) return _DiagIledOpenDesc(memLoc);
            if (memLoc.name == LABEL_UART_BAUDRATE) return _UartBaudrateDesc(memLoc);
            if (memLoc.name == LABEL_VT_DERATE_START) return _DerateVtStartDesc(memLoc);
            if (memLoc.name == LABEL_VT_DERATE_STOP) return _DerateVtStopDesc(memLoc);
            if (memLoc.name == LABEL_DIAG_CURRENT) return _DiagCurrent(memLoc);
            if (memLoc.name == LABEL_ISINK_BUNDLE) return _IsinkAnaBundle(memLoc);

            MatchCollection matches;

            matches = Regex.Matches(memLoc.name, "^LPFB_PULSE_([0-9]+)");
            if ((matches.Count == 1) && (matches[0].Groups.Count == 2))
            {
                String matchStr = (matches[0].Groups[1].ToString());
                byte led = Convert.ToByte(matchStr);
                return _LpfbPulseDesc(led, memLoc);
            }

            matches = Regex.Matches(memLoc.name, "^HPFB_PULSE_([0-9]+)");
            if ((matches.Count == 1) && (matches[0].Groups.Count == 2))
            {
                String matchStr = (matches[0].Groups[1].ToString());
                byte led = Convert.ToByte(matchStr);
                return _HpfbPulseDesc(led, memLoc);
            }

            matches = Regex.Matches(memLoc.name, "BUST_PULSE_([0-9]+)");
            if ((matches.Count == 1) && (matches[0].Groups.Count == 2))
            {
                String matchStr = (matches[0].Groups[1].ToString());
                byte led = Convert.ToByte(matchStr);
                return _BusTPulseDesc(led, memLoc);
            }

            matches = Regex.Matches(memLoc.name, "^LPFB_CURRENT_([0-9]+)");
            if ((matches.Count == 1) && (matches[0].Groups.Count == 2))
            {
                String matchStr = (matches[0].Groups[1].ToString());
                byte led = Convert.ToByte(matchStr);
                return _LpfbCurrentDesc(led, memLoc);
            }

            matches = Regex.Matches(memLoc.name, "^HPFB_CURRENT_([0-9]+)");
            if ((matches.Count == 1) && (matches[0].Groups.Count == 2))
            {
                String matchStr = (matches[0].Groups[1].ToString());
                byte led = Convert.ToByte(matchStr);
                return _HpfbCurrentDesc(led, memLoc);
            }

            matches = Regex.Matches(memLoc.name, "^BUST_CURRENT_([0-9]+)");
            if ((matches.Count == 1) && (matches[0].Groups.Count == 2))
            {
                String matchStr = (matches[0].Groups[1].ToString());
                byte led = Convert.ToByte(matchStr);
                return _BusTCurrentDesc(led, memLoc);
            }

            matches = Regex.Matches(memLoc.name, "^BIN_GAIN_([0-9]+)");
            if ((matches.Count == 1) && (matches[0].Groups.Count == 2))
            {
                String matchStr = (matches[0].Groups[1].ToString());
                byte led = Convert.ToByte(matchStr);
                return _LedBinGainDesc(led, memLoc);
            }

            matches = Regex.Matches(memLoc.name, "^BIN_CLASS_ENABLE_"); 
            if (matches.Count == 1) return _ClassBinEnablesDesc();

            matches = Regex.Matches(memLoc.name, "^BIN_CLASS_LEVEL_([0-9]+)");
            if ((matches.Count == 1) && (matches[0].Groups.Count == 2))
            {
                String matchStr = (matches[0].Groups[1].ToString());
                byte binClass = Convert.ToByte(matchStr);
                return _ClassBinLevelDesc(binClass, memLoc);
            }

            matches = Regex.Matches(memLoc.name, "^BIN_CLASS_GAIN_([0-9]+)");
            if ((matches.Count == 1) && (matches[0].Groups.Count == 2))
            {
                String matchStr = (matches[0].Groups[1].ToString());
                byte binClass = Convert.ToByte(matchStr);
                return _ClassBinGainDesc(binClass, memLoc);
            }

            matches = Regex.Matches(memLoc.name, "^LED_OPEN_THR_([0-9]+)");
            if ((matches.Count == 1) && (matches[0].Groups.Count == 2))
            {
                String matchStr = (matches[0].Groups[1].ToString());
                byte sel = Convert.ToByte(matchStr);
                return _DiagOpenThrDesc(sel, memLoc);
            }

            matches = Regex.Matches(memLoc.name, "^LED_SHORT_THR_([0-9]+)");
            if ((matches.Count == 1) && (matches[0].Groups.Count == 2))
            {
                String matchStr = (matches[0].Groups[1].ToString());
                byte sel = Convert.ToByte(matchStr);
                return _DiagShortThrDesc(sel, memLoc);
            }

            matches = Regex.Matches(memLoc.name, "^DIAG([0-9]+)_CONFIG_");
            if ((matches.Count == 1) && (matches[0].Groups.Count == 2))
            {
                String matchStr = (matches[0].Groups[1].ToString());
                byte sel = Convert.ToByte(matchStr);
                return _DiagConfigDesc(sel);
            }

            return memLoc.Description();
        }

        public override String Description(MemBitfield bitfield)
        {
            MemLocation memLoc = bitfield.GetMemLocation();

            if ((memLoc.name == LABEL_BIN_CLASS_CONFIG) && (bitfield.name == LABEL_PIN_SEL)) return _ClassBinPinSelDesc(bitfield);
            if ((memLoc.name == LABEL_BIN_CLASS_CONFIG) && (bitfield.name == LABEL_ENABLE)) return _ClassBinEnableDesc(bitfield);
            if ((memLoc.name == LABEL_BIN_CLASS_CONFIG) && (bitfield.name == LABEL_CURRENT_SEL)) return _ClassBinCurrentSelDesc(bitfield);

            if ((memLoc.name == LABEL_PWM_CONFIG) && (bitfield.name == LABEL_OE_MASK_BUST)) return _PwmOeMaskBusTDesc(bitfield);
            if ((memLoc.name == LABEL_PWM_CONFIG) && (bitfield.name == LABEL_TIMING)) return _PwmTimingDesc(bitfield);

            if (memLoc.name == LABEL_PWMIN_TIMING) return _PwmInTimingDesc(bitfield);

            if ((memLoc.name == LABEL_PWMIN_CONFIG) && (bitfield.name == LABEL_BUST_PRIO_SCHEME)) return _PwmInBustTPrioSchemeDesc(bitfield);

            if ((memLoc.name == LABEL_COM_TIMEOUT) && (bitfield.name == LABEL_VAL)) return _ComTimeoutDesc(bitfield);
            if ((memLoc.name == LABEL_COM_TIMEOUT) && (bitfield.name == LABEL_TIMEBASE)) return _ComTimeoutDesc(bitfield);
            if ((memLoc.name == LABEL_COM_TIMEOUT) && (bitfield.name == LABEL_DIAG0_E)) return _ComTimeoutDiagDesc(0, bitfield);
            if ((memLoc.name == LABEL_COM_TIMEOUT) && (bitfield.name == LABEL_DIAG1_E)) return _ComTimeoutDiagDesc(1, bitfield);

            if ((memLoc.name == LABEL_UART_CONFIG) && (bitfield.name == LABEL_FORMAT)) return _UartConfigFormatDesc(bitfield);
            if ((memLoc.name == LABEL_UART_CONFIG) && (bitfield.name == LABEL_TURN_AROUND)) return _UartConfigTurnAroundDesc(bitfield);
            if ((memLoc.name == LABEL_UART_CONFIG) && (bitfield.name == LABEL_PARITY)) return _UartConfigParityDesc(bitfield);
            if ((memLoc.name == LABEL_UART_CONFIG) && (bitfield.name == LABEL_STOP)) return _UartConfigStopBitsDesc(bitfield);
            if ((memLoc.name == LABEL_UART_CONFIG) && (bitfield.name == LABEL_BREAK_SEL)) return _UartConfigBreakSelDesc(bitfield);
            if ((memLoc.name == LABEL_UART_CONFIG) && (bitfield.name == LABEL_AUTO_BAUD)) return _UartConfigAutoBaudDesc(bitfield);

            if ((memLoc.name == LABEL_RESET_BEHAVIOR) && (bitfield.name == LABEL_NUMBER_RETRIES)) return _ResetRetriesDesc(bitfield);
            if ((memLoc.name == LABEL_RESET_BEHAVIOR) && (bitfield.name == LABEL_UNRESPONSIVE_TIME)) return _ResetUnresponsiveDesc(bitfield);

            if ((memLoc.name == LABEL_VS_DERATE_RANGE) && (bitfield.name == LABEL_START)) return _DerateVsStartDesc(bitfield);
            if ((memLoc.name == LABEL_VS_DERATE_RANGE) && (bitfield.name == LABEL_STOP)) return _DerateVsStopDesc(bitfield);

            if ((memLoc.name == LABEL_DERATE_GAIN) && (bitfield.name == LABEL_VT_GAIN)) return _DerateVtSGainDesc(bitfield);
            if ((memLoc.name == LABEL_DERATE_GAIN) && (bitfield.name == LABEL_VS_GAIN)) return _DerateVsSGainDesc(bitfield);

            if ((memLoc.name == LABEL_ISINK_CONFIG) && (bitfield.name == LABEL_LP_DIRECT)) return _IsinkConfigLpDirectDesc(bitfield);
            if ((memLoc.name == LABEL_ISINK_CONFIG) && (bitfield.name == LABEL_HP_DIRECT)) return _IsinkConfigHpDirectDesc(bitfield);
            if ((memLoc.name == LABEL_ISINK_CONFIG) && (bitfield.name == LABEL_LPFB_HPFB)) return _IsinkConfigLpfbHpfbDesc(bitfield);
            if ((memLoc.name == LABEL_ISINK_CONFIG) && (bitfield.name == LABEL_DERATE_SUP)) return _IsinkConfigDerateSupDesc(bitfield);

            if ((memLoc.name == LABEL_DEVICE_INFO) && (bitfield.name == LABEL_IC_VERSION)) return _IsinkDeviceInfoIcVersionDesc(bitfield);

            if ((memLoc.name == LABEL_IO_CONFIG) && (bitfield.name == LABEL_OE_INV)) return _IoConfigOeInvDesc(bitfield);
            if ((memLoc.name == LABEL_IO_CONFIG) && (bitfield.name == LABEL_PWMIN_LP_INV)) return _IoConfigPwmInLpInvDesc(bitfield);
            if ((memLoc.name == LABEL_IO_CONFIG) && (bitfield.name == LABEL_PWMIN_HP_INV)) return _IoConfigPwmInHpInvDesc(bitfield);

            if ((memLoc.name == LABEL_DIAG_CONFIG) && (bitfield.name == LABEL_LEVEL)) return _DiagConfigLevelDesc(bitfield);
            if ((memLoc.name == LABEL_DIAG_CONFIG) && (bitfield.name == LABEL_DIAG0_SEL)) return _DiagConfigDiagSelDesc(0, bitfield);
            if ((memLoc.name == LABEL_DIAG_CONFIG) && (bitfield.name == LABEL_DIAG1_SEL)) return _DiagConfigDiagSelDesc(1, bitfield);
            if ((memLoc.name == LABEL_DIAG_CONFIG) && (bitfield.name == LABEL_SLM0)) return _DiagConfigSlmDesc(0, bitfield);
            if ((memLoc.name == LABEL_DIAG_CONFIG) && (bitfield.name == LABEL_SLM1)) return _DiagConfigSlmDesc(1, bitfield);

            MatchCollection matches;

            matches = Regex.Matches(memLoc.name, "^IDAC_REF_SEL_");
            if (matches.Count == 1)
            {
                matches = Regex.Matches(bitfield.name, "^led([0-9]+)");
                if ((matches.Count == 1) && (matches[0].Groups.Count == 2))
                {
                    String matchStr = (matches[0].Groups[1].ToString());
                    byte led = Convert.ToByte(matchStr);
                    return _LedIdacRefSelDesc(led, bitfield);
                }
            }

            matches = Regex.Matches(memLoc.name, "^LED_SUPPLY_SEL_");
            if (matches.Count == 1)
            {
                matches = Regex.Matches(bitfield.name, "^led([0-9]+)");
                if ((matches.Count == 1) && (matches[0].Groups.Count == 2))
                {
                    String matchStr = (matches[0].Groups[1].ToString());
                    byte led = Convert.ToByte(matchStr);
                    return _LedSupplySelDesc(led, bitfield);
                }
            }

            matches = Regex.Matches(memLoc.name, "^LED_OPEN_SEL_");
            if (matches.Count == 1)
            {
                matches = Regex.Matches(bitfield.name, "^led([0-9]+)");
                if ((matches.Count == 1) && (matches[0].Groups.Count == 2))
                {
                    String matchStr = (matches[0].Groups[1].ToString());
                    byte led = Convert.ToByte(matchStr);
                    return _LedDiagOpenSelDesc(led, bitfield);
                }
            }

            matches = Regex.Matches(memLoc.name, "^LED_SHORT_SEL_");
            if (matches.Count == 1)
            {
                matches = Regex.Matches(bitfield.name, "^led([0-9]+)");
                if ((matches.Count == 1) && (matches[0].Groups.Count == 2))
                {
                    String matchStr = (matches[0].Groups[1].ToString());
                    byte led = Convert.ToByte(matchStr);
                    return _LedDiagShortSelDesc(led, bitfield);
                }
            }

            matches = Regex.Matches(memLoc.name, "^PWMIN_ENABLE_");
            if (matches.Count == 1)
            {
                matches = Regex.Matches(bitfield.name, "^led([0-9]+)_lp_e");
                if ((matches.Count == 1) && (matches[0].Groups.Count == 2))
                {
                    String matchStr = (matches[0].Groups[1].ToString());
                    byte led = Convert.ToByte(matchStr);
                    return _PwmInLedLpEnableDesc(led, bitfield);
                }
                matches = Regex.Matches(bitfield.name, "^led([0-9]+)_hp_e");
                if ((matches.Count == 1) && (matches[0].Groups.Count == 2))
                {
                    String matchStr = (matches[0].Groups[1].ToString());
                    byte led = Convert.ToByte(matchStr);
                    return _PwmInLedHpEnableDesc(led, bitfield);
                }
            }

            return bitfield.Description();
        }

        public Standalone()
            : base(10, "STANDALONE", false, true)
        {
            MemLocation memLoc;
            ushort addr = 0;

            for (byte i = 0; i < 16; i += 1)
            {
                this.Add(new MemLocation(String.Format("LPFB_PULSE_{0:D}", i), addr)); addr += 2;
            }
            for (byte i = 0; i < 16; i += 1)
            {
                this.Add(new MemLocation(String.Format("HPFB_PULSE_{0:D}", i), addr)); addr += 2;
            }
            for (byte i = 0; i < 16; i += 1)
            {
                this.Add(new MemLocation(String.Format("LPFB_CURRENT_{0:D}", i), addr)); addr += 2;
            }
            for (byte i = 0; i < 16; i += 1)
            {
                this.Add(new MemLocation(String.Format("HPFB_CURRENT_{0:D}", i), addr)); addr += 2;
            }
            for (byte i = 0; i < 16; i += 1)
            {
                this.Add(new MemLocation(String.Format("BIN_GAIN_{0:D}", i), addr)); addr += 2;
            }

            memLoc = new MemLocation(LABEL_BIN_CLASS_CONFIG, addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield(LABEL_PIN_SEL, 4, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_ENABLE, 1, 4));
            memLoc.AddBitfield(new MemBitfield(LABEL_CURRENT_SEL, 5, 5));
            this.Add(memLoc);

            memLoc = new MemLocation("BIN_CLASS_ENABLE_0_7", addr); addr += 2;
            this.Add(memLoc);

            memLoc = new MemLocation("BIN_CLASS_ENABLE_8_15", addr); addr += 2;
            this.Add(memLoc);

            for (byte i = 0; i < 4; i += 1)
            {
                this.Add(new MemLocation(String.Format("BIN_CLASS_LEVEL_{0:D}", i), addr)); addr += 2;
            }

            for (byte i = 0; i < 5; i += 1)
            {
                this.Add(new MemLocation(String.Format("BIN_CLASS_GAIN_{0:D}", i), addr)); addr += 2;
            }

            memLoc = new MemLocation(LABEL_PWM_PRESCALER, addr); addr += 2;
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_PWM_PERIOD, addr); addr += 2;
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_PWM_CONFIG, addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield(LABEL_TIMING, 1, 0));
            // reserved1
            memLoc.AddBitfield(new MemBitfield(LABEL_OE_MASK_BUST, 1, 2));
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_PWM_COMBINE_PRI, addr); addr += 2;
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_PWM_COMBINE_SEC, addr); addr += 2;
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_PWMIN_TIMING, addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield(LABEL_MIN, 5, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_MAX, 5, 5));
            this.Add(memLoc);

            memLoc = new MemLocation("PWMIN_ENABLE_0_3", addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield("led0_lp_e", 1, 0));
            memLoc.AddBitfield(new MemBitfield("led0_hp_e", 1, 1));
            memLoc.AddBitfield(new MemBitfield("led1_lp_e", 1, 2));
            memLoc.AddBitfield(new MemBitfield("led1_hp_e", 1, 3));
            memLoc.AddBitfield(new MemBitfield("led2_lp_e", 1, 4));
            memLoc.AddBitfield(new MemBitfield("led2_hp_e", 1, 5));
            memLoc.AddBitfield(new MemBitfield("led3_lp_e", 1, 6));
            memLoc.AddBitfield(new MemBitfield("led3_hp_e", 1, 7));
            this.Add(memLoc);

            memLoc = new MemLocation("PWMIN_ENABLE_4_7", addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield("led4_lp_e", 1, 0));
            memLoc.AddBitfield(new MemBitfield("led4_hp_e", 1, 1));
            memLoc.AddBitfield(new MemBitfield("led5_lp_e", 1, 2));
            memLoc.AddBitfield(new MemBitfield("led5_hp_e", 1, 3));
            memLoc.AddBitfield(new MemBitfield("led6_lp_e", 1, 4));
            memLoc.AddBitfield(new MemBitfield("led6_hp_e", 1, 5));
            memLoc.AddBitfield(new MemBitfield("led7_lp_e", 1, 6));
            memLoc.AddBitfield(new MemBitfield("led7_hp_e", 1, 7));
            this.Add(memLoc);

            memLoc = new MemLocation("PWMIN_ENABLE_8_11", addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield("led8_lp_e", 1, 0));
            memLoc.AddBitfield(new MemBitfield("led8_hp_e", 1, 1));
            memLoc.AddBitfield(new MemBitfield("led9_lp_e", 1, 2));
            memLoc.AddBitfield(new MemBitfield("led9_hp_e", 1, 3));
            memLoc.AddBitfield(new MemBitfield("led10_lp_e", 1, 4));
            memLoc.AddBitfield(new MemBitfield("led10_hp_e", 1, 5));
            memLoc.AddBitfield(new MemBitfield("led11_lp_e", 1, 6));
            memLoc.AddBitfield(new MemBitfield("led11_hp_e", 1, 7));
            this.Add(memLoc);

            memLoc = new MemLocation("PWMIN_ENABLE_12_15", addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield("led12_lp_e", 1, 0));
            memLoc.AddBitfield(new MemBitfield("led12_hp_e", 1, 1));
            memLoc.AddBitfield(new MemBitfield("led13_lp_e", 1, 2));
            memLoc.AddBitfield(new MemBitfield("led13_hp_e", 1, 3));
            memLoc.AddBitfield(new MemBitfield("led14_lp_e", 1, 4));
            memLoc.AddBitfield(new MemBitfield("led14_hp_e", 1, 5));
            memLoc.AddBitfield(new MemBitfield("led15_lp_e", 1, 6));
            memLoc.AddBitfield(new MemBitfield("led15_hp_e", 1, 7));
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_PWMIN_CONFIG, addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield(LABEL_BUST_PRIO_SCHEME, 1, 0));
            this.Add(memLoc);

            this.Add(new MemLocation("RESERVED", addr, true)); addr += 2;

            for (byte i = 1; i <= 3; i += 1)
            {
                memLoc = new MemLocation(String.Format("LED_OPEN_THR_{0:D}", i), addr); addr += 2;
                this.Add(memLoc);
            }

            memLoc = new MemLocation("LED_OPEN_SEL_0_3", addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield("led0", 2, 0));
            memLoc.AddBitfield(new MemBitfield("led1", 2, 2));
            memLoc.AddBitfield(new MemBitfield("led2", 2, 4));
            memLoc.AddBitfield(new MemBitfield("led3", 2, 6));
            this.Add(memLoc);

            memLoc = new MemLocation("LED_OPEN_SEL_4_7", addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield("led4", 2, 0));
            memLoc.AddBitfield(new MemBitfield("led5", 2, 2));
            memLoc.AddBitfield(new MemBitfield("led6", 2, 4));
            memLoc.AddBitfield(new MemBitfield("led7", 2, 6));
            this.Add(memLoc);

            memLoc = new MemLocation("LED_OPEN_SEL_8_11", addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield("led8", 2, 0));
            memLoc.AddBitfield(new MemBitfield("led9", 2, 2));
            memLoc.AddBitfield(new MemBitfield("led10", 2, 4));
            memLoc.AddBitfield(new MemBitfield("led11", 2, 6));
            this.Add(memLoc);

            memLoc = new MemLocation("LED_OPEN_SEL_12_15", addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield("led12", 2, 0));
            memLoc.AddBitfield(new MemBitfield("led13", 2, 2));
            memLoc.AddBitfield(new MemBitfield("led14", 2, 4));
            memLoc.AddBitfield(new MemBitfield("led15", 2, 6));
            this.Add(memLoc);

            for (byte i = 1; i <= 3; i += 1)
            {
                memLoc = new MemLocation(String.Format("LED_SHORT_THR_{0:D}", i), addr); addr += 2;
                this.Add(memLoc);
            }

            memLoc = new MemLocation("LED_SHORT_SEL_0_3", addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield("led0", 2, 0));
            memLoc.AddBitfield(new MemBitfield("led1", 2, 2));
            memLoc.AddBitfield(new MemBitfield("led2", 2, 4));
            memLoc.AddBitfield(new MemBitfield("led3", 2, 6));
            this.Add(memLoc);

            memLoc = new MemLocation("LED_SHORT_SEL_4_7", addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield("led4", 2, 0));
            memLoc.AddBitfield(new MemBitfield("led5", 2, 2));
            memLoc.AddBitfield(new MemBitfield("led6", 2, 4));
            memLoc.AddBitfield(new MemBitfield("led7", 2, 6));
            this.Add(memLoc);

            memLoc = new MemLocation("LED_SHORT_SEL_8_11", addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield("led8", 2, 0));
            memLoc.AddBitfield(new MemBitfield("led9", 2, 2));
            memLoc.AddBitfield(new MemBitfield("led10", 2, 4));
            memLoc.AddBitfield(new MemBitfield("led11", 2, 6));
            this.Add(memLoc);

            memLoc = new MemLocation("LED_SHORT_SEL_12_15", addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield("led12", 2, 0));
            memLoc.AddBitfield(new MemBitfield("led13", 2, 2));
            memLoc.AddBitfield(new MemBitfield("led14", 2, 4));
            memLoc.AddBitfield(new MemBitfield("led15", 2, 6));
            this.Add(memLoc);

            this.Add(new MemLocation("RESERVED", addr, true)); addr += 2;

            memLoc = new MemLocation(LABEL_ILED_OPEN_THR, addr); addr += 2;
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_VS_TOO_LOW, addr); addr += 2;
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_VS_CRITICAL, addr); addr += 2;
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_VT_CRITICAL, addr); addr += 2;
            this.Add(memLoc);

            memLoc = new MemLocation("COM_DEV_ADDR", addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield("addr", 5, 0, "Device Address"));
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_COM_TIMEOUT, addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield(LABEL_VAL, 7, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_TIMEBASE, 1, 7));
            memLoc.AddBitfield(new MemBitfield(LABEL_DIAG0_E, 1, 8));
            memLoc.AddBitfield(new MemBitfield(LABEL_DIAG1_E, 1, 9));
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_UART_CONFIG, addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield(LABEL_FORMAT, 1, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_TURN_AROUND, 3, 1));
            memLoc.AddBitfield(new MemBitfield(LABEL_PARITY, 2, 4));
            memLoc.AddBitfield(new MemBitfield(LABEL_STOP, 1, 6));
            memLoc.AddBitfield(new MemBitfield(LABEL_BREAK_SEL, 1, 7));
            memLoc.AddBitfield(new MemBitfield(LABEL_AUTO_BAUD, 1, 8));
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_UART_BAUDRATE, addr); addr += 2;
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_RESET_BEHAVIOR, addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield(LABEL_NUMBER_RETRIES, 4, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_UNRESPONSIVE_TIME, 4, 4));
            this.Add(memLoc);

            for (byte i = 0; i < 16; i += 1)
            {
                this.Add(new MemLocation(String.Format("BUST_PULSE_{0:D}", i), addr)); addr += 2;
            }

            for (byte i = 0; i < 16; i += 1)
            {
                this.Add(new MemLocation(String.Format("BUST_CURRENT_{0:D}", i), addr)); addr += 2;
            }

            memLoc = new MemLocation("IDAC_REF_SEL_0_7", addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield("led0", 1, 0));
            memLoc.AddBitfield(new MemBitfield("led1", 1, 1));
            memLoc.AddBitfield(new MemBitfield("led2", 1, 2));
            memLoc.AddBitfield(new MemBitfield("led3", 1, 3));
            memLoc.AddBitfield(new MemBitfield("led4", 1, 4));
            memLoc.AddBitfield(new MemBitfield("led5", 1, 5));
            memLoc.AddBitfield(new MemBitfield("led6", 1, 6));
            memLoc.AddBitfield(new MemBitfield("led7", 1, 7));
            this.Add(memLoc);

            memLoc = new MemLocation("IDAC_REF_SEL_8_15", addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield("led8", 1, 0));
            memLoc.AddBitfield(new MemBitfield("led9", 1, 1));
            memLoc.AddBitfield(new MemBitfield("led10", 1, 2));
            memLoc.AddBitfield(new MemBitfield("led11", 1, 3));
            memLoc.AddBitfield(new MemBitfield("led12", 1, 4));
            memLoc.AddBitfield(new MemBitfield("led13", 1, 5));
            memLoc.AddBitfield(new MemBitfield("led14", 1, 6));
            memLoc.AddBitfield(new MemBitfield("led15", 1, 7));
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_ISINK_CONFIG, addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield(LABEL_SLEW, 2, 0, "Driver Slew Rate Configuration"));
            memLoc.AddBitfield(new MemBitfield(LABEL_LP_DIRECT, 2, 2));
            memLoc.AddBitfield(new MemBitfield(LABEL_HP_DIRECT, 2, 4));
            memLoc.AddBitfield(new MemBitfield(LABEL_LPFB_HPFB, 2, 6));
            memLoc.AddBitfield(new MemBitfield(LABEL_DERATE_SUP, 2, 8));
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_VS_DERATE_RANGE, addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield(LABEL_START, 5, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_STOP, 5, 5));
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_VT_DERATE_START, addr); addr += 2;
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_VT_DERATE_STOP, addr); addr += 2;
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_DERATE_GAIN, addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield(LABEL_VS_GAIN, 5, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_VT_GAIN, 5, 5));
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_IO_CONFIG, addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield(LABEL_OE_INV, 1, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_PWMIN_LP_INV, 1, 1));
            memLoc.AddBitfield(new MemBitfield(LABEL_PWMIN_HP_INV, 1, 2));
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_DIAG_CONFIG, addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield(LABEL_LEVEL, 5, 0));
            // reserved5
            memLoc.AddBitfield(new MemBitfield(LABEL_DIAG0_SEL, 1, 6));
            memLoc.AddBitfield(new MemBitfield(LABEL_DIAG1_SEL, 1, 7));
            memLoc.AddBitfield(new MemBitfield(LABEL_SLM0, 1, 8));
            memLoc.AddBitfield(new MemBitfield(LABEL_SLM1, 1, 9));
            this.Add(memLoc);

            memLoc = new MemLocation("DIAG0_CONFIG_0_7", addr); addr += 2;
            this.Add(memLoc);

            memLoc = new MemLocation("DIAG0_CONFIG_8_15", addr); addr += 2;
            this.Add(memLoc);

            memLoc = new MemLocation("DIAG1_CONFIG_0_7", addr); addr += 2;
            this.Add(memLoc);

            memLoc = new MemLocation("DIAG1_CONFIG_8_15", addr); addr += 2;
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_DIAG_CURRENT, addr); addr += 2;
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_ISINK_BUNDLE, addr); addr += 2;
            this.Add(memLoc);

            this.Add(new MemLocation("RESERVED", addr, true)); addr += 2;

            memLoc = new MemLocation("LED_SUPPLY_SEL_0_3", addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield("led0", 2, 0));
            memLoc.AddBitfield(new MemBitfield("led1", 2, 2));
            memLoc.AddBitfield(new MemBitfield("led2", 2, 4));
            memLoc.AddBitfield(new MemBitfield("led3", 2, 6));
            this.Add(memLoc);

            memLoc = new MemLocation("LED_SUPPLY_SEL_4_7", addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield("led4", 2, 0));
            memLoc.AddBitfield(new MemBitfield("led5", 2, 2));
            memLoc.AddBitfield(new MemBitfield("led6", 2, 4));
            memLoc.AddBitfield(new MemBitfield("led7", 2, 6));
            this.Add(memLoc);

            memLoc = new MemLocation("LED_SUPPLY_SEL_8_11", addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield("led8", 2, 0));
            memLoc.AddBitfield(new MemBitfield("led9", 2, 2));
            memLoc.AddBitfield(new MemBitfield("led10", 2, 4));
            memLoc.AddBitfield(new MemBitfield("led11", 2, 6));
            this.Add(memLoc);

            memLoc = new MemLocation("LED_SUPPLY_SEL_12_15", addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield("led12", 2, 0));
            memLoc.AddBitfield(new MemBitfield("led13", 2, 2));
            memLoc.AddBitfield(new MemBitfield("led14", 2, 4));
            memLoc.AddBitfield(new MemBitfield("led15", 2, 6));
            this.Add(memLoc);

            for (byte i = 0; i < 9; i += 1)
            {
                this.Add(new MemLocation("RESERVED", addr, true)); addr += 2;
            }

            memLoc = new MemLocation(LABEL_DEVICE_INFO, addr, true); addr += 2;
            memLoc.AddBitfield(new MemBitfield("fw_version", 5, 0, "Firmware Version"));
            memLoc.AddBitfield(new MemBitfield(LABEL_IC_VERSION, 5, 5));
            this.Add(memLoc);

            memLoc = new MemLocation("FOR_CUSTOMER_USE_0", addr, false, false, "For Customer Use."); addr += 2;
            this.Add(memLoc);

            memLoc = new MemLocation("FOR_CUSTOMER_USE_1", addr, false, false, "For Customer Use."); addr += 2;
            this.Add(memLoc);

            this.Verify();
        }
    }

}
