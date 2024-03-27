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
    public class StandaloneExt : Memory
    {
        private string LABEL_DIAG23_CONFIG = "DIAG23_CONFIG";
        private string LABEL_BIN_CLASS_1_CONFIG = "BIN_CLASS_1_CONFIG";
        private string LABEL_SLEEP_CONFIG = "SLEEP_CONFIG";
        private string LABEL_WAKEUP_ACK_TIMEOUT = "WAKEUP_ACK_TIMEOUT";
        private string LABEL_UART_CONFIG_EXT = "UART_CONFIG_EXT";
        private string LABEL_UART_DEBOUNCE_EXT = "UART_DEBOUNCE_EXT";
        private string LABEL_UART_SAMPLING_EXT = "UART_SAMPLING_EXT";
        
        private string LABEL_BUS = "bus";
        private string LABEL_OE = "oe";
        private string LABEL_TIMEOUT = "timeout";
        private string LABEL_USE = "use";
        private string LABEL_BREAK_SEL = "break_sel";
        private string LABEL_SYNC32 = "sync32";
        private string LABEL_SYNC_AVG = "sync_avg";
        private string LABEL_SYNC_CHECK = "sync_check";
        private string LABEL_OFFSET = "offset";
        private string LABEL_SLM2 = "slm2";
        private string LABEL_SLM3 = "slm3";
        private string LABEL_PIN_SEL = "pin_sel";
        private string LABEL_ENABLE = "enable";
        private string LABEL_CURRENT_SEL = "current_sel";

        internal const ushort ADDR_STANDALONE_EXT_BIN_CLASS_1_ENABLE_0_7           = 0x02;
        internal const ushort ADDR_STANDALONE_EXT_BIN_CLASS_1_ENABLE_8_15          = 0x04;
        internal const ushort ADDR_STANDALONE_EXT_DIAG2_CONFIG_0_7                 = 0x12;
        internal const ushort ADDR_STANDALONE_EXT_DIAG2_CONFIG_8_15                = 0x14;
        internal const ushort ADDR_STANDALONE_EXT_DIAG3_CONFIG_0_7                 = 0x16;
        internal const ushort ADDR_STANDALONE_EXT_DIAG3_CONFIG_8_15 = 0x18;
        internal const ushort ADDR_STANDALONE_EXT_UART_CONFIG_EXT                =  0x30;

        internal uint bin_class_enables
        {
            get
            {
                uint temp = this[ADDR_STANDALONE_EXT_BIN_CLASS_1_ENABLE_8_15].data;
                temp <<= 8;
                temp += this[ADDR_STANDALONE_EXT_BIN_CLASS_1_ENABLE_0_7].data;
                return temp;
            }
        }

        internal uint diag2_config
        {
            get
            {
                uint temp = this[ADDR_STANDALONE_EXT_DIAG2_CONFIG_8_15].data;
                temp <<= 8;
                temp += this[ADDR_STANDALONE_EXT_DIAG2_CONFIG_0_7].data;
                return temp;
            }
        }

        internal uint diag3_config
        {
            get
            {
                uint temp = this[ADDR_STANDALONE_EXT_DIAG3_CONFIG_8_15].data;
                temp <<= 8;
                temp += this[ADDR_STANDALONE_EXT_DIAG3_CONFIG_0_7].data;
                return temp;
            }
        }

        internal bool uart_use_ext
        {
            get { return this[ADDR_STANDALONE_EXT_UART_CONFIG_EXT].GetBitfield(LABEL_USE).GetBool(); }
        }

        private String _ClassBinPinSelDesc(MemBitfield bitfield)
        {
            return String.Format("Binning Group 1 Class determined by LED {0:D} pin.", bitfield.GetData());
        }

        private String _ClassBinEnableDesc(MemBitfield bitfield)
        {
            if (bitfield.GetBool())
                return "Binning Group 1 Class evaluation enabled.";
            else
                return "Binning Group 1 Class evaluation disabled!";
        }

        private String _ClassBinCurrentSelDesc(MemBitfield bitfield)
        {
            return String.Format("Binning Group 1 Class evaluation current = {0:F} mA", Convert.ToDouble(bitfield.GetData()) * 3.2);
        }

        private String _ClassBinEnablesDesc()
        {
            if (bin_class_enables == 0)
                return "Binning Group 1 Class not applied to any channels!";

            String ret = "Binning Group 1 Class applied to Channels = ";
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
            return String.Format("Binning Group 1 Class {0:D} Gain Factor = {1:F}", binClass, gain);
        }

        private String _DiagConfigDesc(byte sel)
        {
            uint diag_config = (sel == 3) ? diag3_config : diag2_config;

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

        private String _DiagConfigSlmDesc(byte sel, MemBitfield bitfield)
        {
            if (bitfield.GetBool())
                return String.Format("Diagnosis Group {0:D} is in single-lamp-mode (SLM).", sel);
            else
                return String.Format("Diagnosis Group {0:D} is in multi-lamp-mode (MLM).", sel);
        }

        private String _SleepConfigBusDesc(MemBitfield bitfield)
        {
            if (bitfield.GetBool())
                return "Entering and leaving sleep mode via bus is enabled.";
            else
                return "Bus is ignored for Sleep Feature.";
        }

        private String _SleepConfigOeDesc(MemBitfield bitfield)
        {
            if (bitfield.GetBool())
                return "Entering and leaving sleep mode via OE pin is enabled.";
            else
                return "OE pin is ignored for Sleep Feature.";
        }

        private String _WakeupAckTimeoutDesc(MemBitfield bitfield)
        {
            if (bitfield.GetData() == 0) 
                return "Unlimited wait time for Wakeup Acknowledge.";
            else
                return String.Format("Wait time for Wakeup Acknowledge = {0:D} ms", bitfield.GetData() * 100);
        }

        private String _UartDebounceExtDesc(MemLocation memLoc)
        {
            if (uart_use_ext)
                return String.Format("Uart Ext Configuration active: Debounce Filter time = {0:F} us.", Convert.ToDouble(memLoc.data) / 48.0);
            else
                return String.Format("Uart Ext Configuration disabled. Default of {0:F} us is used.", 32.0 / 48.0);
        }

        private String _UartConfigExtUseDesc()
        {
            if (uart_use_ext)
                return String.Format("Uart Ext Configuration enabled.");
            else
                return String.Format("Uart Ext Configuration disabled.");
        }

        private String _UartConfigExtBreakSelDesc(MemBitfield bitfield)
        {
            double break_length = 9.5;
            if (bitfield.GetData() > 0)
                break_length = 10.5 + 0.5 * bitfield.GetData();

            if (uart_use_ext)
                return String.Format("Uart Ext Configuration active: Break Length {0:F} bits.", break_length);
            else
                return String.Format("Uart Ext Configuration disabled.");
        }

        private String _UartConfigExtSync32Desc(MemBitfield bitfield)
        {
            if (uart_use_ext)
            {
                if (bitfield.GetBool())
                    return String.Format("Uart Ext Configuration active: Sync Length = 32 bits (only without Partity bit).");
                else
                    return String.Format("Uart Ext Configuration active: Sync Length = 8 bits.");
            }
            else
                return String.Format("Uart Ext Configuration disabled.");
        }

        private String _UartConfigExtSyncAvgDesc(MemBitfield bitfield)
        {
            if (uart_use_ext)
            {
                switch (bitfield.GetData())
                {
                    case 0: return String.Format("Uart Ext Configuration active: no Sync averaging.");
                    case 1: return String.Format("Uart Ext Configuration active: up to 2 SYNCS are used (only with auto_baud=1).");
                    case 2: return String.Format("Uart Ext Configuration active: up to 4 SYNCS are used (only with auto_baud=1).");
                    case 3: return String.Format("Uart Ext Configuration active: up to 8 SYNCS are used (only with auto_baud=1).");
                }
                return "reserved";
            }
            else
                return String.Format("Uart Ext Configuration disabled.");
        }

        private String _UartConfigExtSyncCheckDesc(MemBitfield bitfield)
        {
            if (uart_use_ext)
            {
                switch (bitfield.GetData())
                {
                    case 0: return String.Format("Uart Ext Configuration active: SYNC check disabled.");
                    case 1: return String.Format("Uart Ext Configuration active: SYNC check with +-50% for each bit length.");
                    case 2: return String.Format("Uart Ext Configuration active: SYNC check with +-25% for each bit length.");
                    case 3: return String.Format("Uart Ext Configuration active: SYNC check with +-12.5% for each bit length.");
                }
                return "reserved";
            }
            else
                return String.Format("Uart Ext Configuration disabled.");
        }

        private String _UartSamplingExtOffsetDesc(MemBitfield bitfield)
        {
                switch (bitfield.GetData())
                {
                    case 0x1B: return "Uart Sampling Points @  2/16 +  3/16 +  4/16 of the bit time.";
                    case 0x1C: return "Uart Sampling Points @  3/16 +  4/16 +  5/16 of the bit time.";
                    case 0x1D: return "Uart Sampling Points @  4/16 +  5/16 +  6/16 of the bit time.";
                    case 0x1E: return "Uart Sampling Points @  5/16 +  6/16 +  7/16 of the bit time.";
                    case 0x1F: return "Uart Sampling Points @  6/16 +  7/16 +  8/16 of the bit time.";
                    case 0x00: return "Uart Sampling Points @  7/16 +  8/16 +  9/16 of the bit time.";
                    case 0x01: return "Uart Sampling Points @  8/16 +  9/16 + 10/16 of the bit time.";
                    case 0x02: return "Uart Sampling Points @  9/16 + 10/16 + 11/16 of the bit time.";
                    case 0x03: return "Uart Sampling Points @ 10/16 + 11/16 + 12/16 of the bit time.";
                    case 0x04: return "Uart Sampling Points @ 11/16 + 12/16 + 13/16 of the bit time.";
                    case 0x05: return "Uart Sampling Points @ 12/16 + 13/16 + 14/16 of the bit time.";
                }
                return "invalid!";
        }

        public override String Description(MemLocation memLoc)
        {
            if (memLoc.name == LABEL_UART_DEBOUNCE_EXT) return _UartDebounceExtDesc(memLoc);
        
            MatchCollection matches;

            matches = Regex.Matches(memLoc.name, "^BIN_CLASS_1_ENABLE_");
            if (matches.Count == 1) return _ClassBinEnablesDesc();

            matches = Regex.Matches(memLoc.name, "^DIAG([0-9]+)_CONFIG_");
            if ((matches.Count == 1) && (matches[0].Groups.Count == 2))
            {
                String matchStr = (matches[0].Groups[1].ToString());
                byte sel = Convert.ToByte(matchStr);
                return _DiagConfigDesc(sel);
            }

            matches = Regex.Matches(memLoc.name, "^BIN_CLASS_1_GAIN_([0-9]+)");
            if ((matches.Count == 1) && (matches[0].Groups.Count == 2))
            {
                String matchStr = (matches[0].Groups[1].ToString());
                byte binClass = Convert.ToByte(matchStr);
                return _ClassBinGainDesc(binClass, memLoc);
            }

            return memLoc.Description();
        }

        public override String Description(MemBitfield bitfield)
        {
            MemLocation memLoc = bitfield.GetMemLocation();

            if ((memLoc.name == LABEL_DIAG23_CONFIG) && (bitfield.name == LABEL_SLM2)) return _DiagConfigSlmDesc(2, bitfield);
            if ((memLoc.name == LABEL_DIAG23_CONFIG) && (bitfield.name == LABEL_SLM3)) return _DiagConfigSlmDesc(3, bitfield);

            if ((memLoc.name == LABEL_BIN_CLASS_1_CONFIG) && (bitfield.name == LABEL_PIN_SEL)) return _ClassBinPinSelDesc(bitfield);
            if ((memLoc.name == LABEL_BIN_CLASS_1_CONFIG) && (bitfield.name == LABEL_ENABLE)) return _ClassBinEnableDesc(bitfield);
            if ((memLoc.name == LABEL_BIN_CLASS_1_CONFIG) && (bitfield.name == LABEL_CURRENT_SEL)) return _ClassBinCurrentSelDesc(bitfield);

            if ((memLoc.name == LABEL_SLEEP_CONFIG) && (bitfield.name == LABEL_BUS)) return _SleepConfigBusDesc(bitfield);
            if ((memLoc.name == LABEL_SLEEP_CONFIG) && (bitfield.name == LABEL_OE)) return _SleepConfigOeDesc(bitfield);

            if ((memLoc.name == LABEL_WAKEUP_ACK_TIMEOUT) && (bitfield.name == LABEL_TIMEOUT)) return _WakeupAckTimeoutDesc(bitfield);

            if ((memLoc.name == LABEL_UART_CONFIG_EXT) && (bitfield.name == LABEL_USE)) return _UartConfigExtUseDesc();
            if ((memLoc.name == LABEL_UART_CONFIG_EXT) && (bitfield.name == LABEL_BREAK_SEL)) return _UartConfigExtBreakSelDesc(bitfield);
            if ((memLoc.name == LABEL_UART_CONFIG_EXT) && (bitfield.name == LABEL_SYNC32)) return _UartConfigExtSync32Desc(bitfield);
            if ((memLoc.name == LABEL_UART_CONFIG_EXT) && (bitfield.name == LABEL_SYNC_AVG)) return _UartConfigExtSyncAvgDesc(bitfield);
            if ((memLoc.name == LABEL_UART_CONFIG_EXT) && (bitfield.name == LABEL_SYNC_CHECK)) return _UartConfigExtSyncCheckDesc(bitfield);

            if ((memLoc.name == LABEL_UART_SAMPLING_EXT) && (bitfield.name == LABEL_OFFSET)) return _UartSamplingExtOffsetDesc(bitfield);

            return bitfield.Description();
        }

        public StandaloneExt()
            : base(10, "STANDALONE_EXT", false, true)
        {
            MemLocation memLoc;
            ushort addr = 0;

            memLoc = new MemLocation(LABEL_BIN_CLASS_1_CONFIG, addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield(LABEL_PIN_SEL, 4, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_ENABLE, 1, 4));
            memLoc.AddBitfield(new MemBitfield(LABEL_CURRENT_SEL, 5, 5));
            this.Add(memLoc);

            memLoc = new MemLocation("BIN_CLASS_1_ENABLE_0_7", addr); addr += 2;
            this.Add(memLoc);

            memLoc = new MemLocation("BIN_CLASS_1_ENABLE_8_15", addr); addr += 2;
            this.Add(memLoc);

            for (byte i = 0; i <= 4; i += 1)
            {
                memLoc = new MemLocation(String.Format("BIN_CLASS_1_GAIN_{0:D}", i), addr); addr += 2;
                this.Add(memLoc);
            }

            memLoc = new MemLocation(LABEL_DIAG23_CONFIG, addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield(LABEL_SLM2, 1, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_SLM3, 1, 1));
            this.Add(memLoc);

            memLoc = new MemLocation("DIAG2_CONFIG_0_7", addr); addr += 2;
            this.Add(memLoc);

            memLoc = new MemLocation("DIAG2_CONFIG_8_15", addr); addr += 2;
            this.Add(memLoc);

            memLoc = new MemLocation("DIAG3_CONFIG_0_7", addr); addr += 2;
            this.Add(memLoc);

            memLoc = new MemLocation("DIAG3_CONFIG_8_15", addr); addr += 2;
            this.Add(memLoc);

            for (byte i = 0; i < 3; i += 1)
            {
                this.Add(new MemLocation("RESERVED", addr, true)); addr += 2;
            }

            memLoc = new MemLocation(LABEL_SLEEP_CONFIG, addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield(LABEL_BUS, 1, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_OE, 1, 1));
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_WAKEUP_ACK_TIMEOUT, addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield(LABEL_TIMEOUT, 5, 0));
            this.Add(memLoc);

            for (byte i = 0; i < 6; i += 1)
            {
                this.Add(new MemLocation("RESERVED", addr, true)); addr += 2;
            }

            memLoc = new MemLocation(LABEL_UART_CONFIG_EXT, addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield(LABEL_USE, 1, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_BREAK_SEL, 3, 1));
            memLoc.AddBitfield(new MemBitfield(LABEL_SYNC32, 1, 4));
            memLoc.AddBitfield(new MemBitfield(LABEL_SYNC_AVG, 2, 5));
            memLoc.AddBitfield(new MemBitfield(LABEL_SYNC_CHECK, 3, 7));
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_UART_DEBOUNCE_EXT, addr); addr += 2;
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_UART_SAMPLING_EXT, addr); addr += 2;
            memLoc.AddBitfield(new MemBitfield(LABEL_OFFSET, 5, 0));
            this.Add(memLoc);

            this.Verify();
        }
    }

}
