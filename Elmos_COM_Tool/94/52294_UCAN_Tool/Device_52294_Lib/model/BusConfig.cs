using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using MemLib;

namespace Device_52294_Lib
{
    public class BusConfig : Memory
    {
        private string LABEL_BUS_DERATE_GAIN = "BUS_DERATE_GAIN";
        private string LABEL_PAGE_BASE_ADDR = "PAGE_BASE_ADDR";
        private string LABEL_SRAM_SEL = "SRAM_SEL";

        private string LABEL_SEL = "sel";
        private string LABEL_GAIN = "gain";

        internal const ushort ADDR_PULSE_AREA = 0x00;
        internal const ushort ADDR_CURRENT_AREA = 0x20;

        internal const ushort ADDR_LED_ENABLE_0_7 = 0x44;
        internal const ushort ADDR_LED_ENABLE_8_15 = 0x46;

        internal const ushort ADDR_PAGE_BASE_ADDR = 0x60;
        internal const ushort ADDR_ASSERT_DIAG = 0x62;
        internal const ushort ADDR_SRAM_SEL = 0x64;

        internal const ushort ADDR_BUS_DERATE_GAIN = 0x70;

        internal const ushort ADDR_CMD_SLEEP = 0x72;
        internal const ushort ADDR_CMD_WAKEUP_ACK = 0x74;

        internal const ushort ADDR_BUS_PULSE_ALL = 0x78;
        internal const ushort ADDR_BUS_CURRENT_ALL = 0x7A;
        internal const ushort ADDR_CMD_RESET = 0x7C;
        internal const ushort ADDR_CMD_UPDATE = 0x7E;

        public uint led_enables
        {
            get
            {
                uint temp = this[ADDR_LED_ENABLE_8_15].data;
                temp <<= 8;
                temp += this[ADDR_LED_ENABLE_0_7].data;
                return temp;
            }
        }

        public ushort GetPulse(byte index)
        {
            return (ushort)this[(UInt32)(ADDR_PULSE_AREA + 2 * index)].data;
        }

        public void SetPulse(byte index, ushort value)
        {
            this[(UInt32) (ADDR_PULSE_AREA + 2*index)].SetDataClearModified(value);
        }

        public ushort GetCurrent(byte index)
        {
            return (ushort)this[(UInt32)(ADDR_CURRENT_AREA + 2 * index)].data;
        }

        public void SetCurrent(byte index, ushort value)
        {
            this[(UInt32) (ADDR_CURRENT_AREA + 2*index)].SetDataClearModified(value);
        }

        public void SelectPage(Device.PageSel sel)
        {
            this[(UInt32)(ADDR_PAGE_BASE_ADDR)].SetDataSetModified((uint)sel);
        }

        public void SetSramSel(bool sram_otp_n)
        {
            this[(UInt32)(ADDR_SRAM_SEL)].SetDataSetModified((uint)(sram_otp_n ? 1 : 0));
        }

        private String _PulseDesc(byte led, MemLocation location)
        {
            return String.Format("LED {0:D} Pulse", led);
        }

        private String _CurrentDesc(byte led, MemLocation location)
        {
            return String.Format("LED {0:D} Current = {1:F} mA", led, Convert.ToDouble(location.data) * 0.1);
        }

        private String _LedEnableDesc()
        {
            if (led_enables == 0x0000) return "ALL Channels disabled!";
            if (led_enables == 0xFFFF) return "ALL Channels enabled.";
            String ret = "Disabled Led Channels =";
            for (int i = 0; i < 16; i += 1)
            {
                if (((led_enables >> i) & 1) == 0)
                {
                    ret += String.Format(" {0:D}", i);
                }
            }
            return ret;
        }

        private String _BusDerateGainDesc(MemBitfield bitfield)
        {
            return String.Format("Bus Derating factor = {0:F}", 1 - (Convert.ToDouble(bitfield.GetData()) / 256.0));
        }

        private String _PageBaseAddrSelDesc(MemBitfield bitfield)
        {
            /*
            0 : selects standalone page 0 for access (0x000 ... 0x07E)
            1 : selects standalone page 1 for access (0x080 ... 0x0FE)
            2 : selects standalone page 2 for access (0x100 ... 0x17E)
            3 : selects bus default config page for access
            4 : selects standalone_ext page for access (0x200 ... 0x27E)
             */
            return ""; // TODO
        }

        private String _SramSelDesc(MemBitfield bitfield)
        {
            //0: selects the OTP for access
            //1: selects the SRAM for access
            return ""; // TODO
        }

        public override String Description(MemLocation memLoc)
        {
            MatchCollection matches;

            matches = Regex.Matches(memLoc.name, "^PULSE_([0-9]+)");
            if ((matches.Count == 1) && (matches[0].Groups.Count == 2))
            {
                String matchStr = (matches[0].Groups[1].ToString());
                byte led = Convert.ToByte(matchStr);
                return _PulseDesc(led, memLoc);
            }

            matches = Regex.Matches(memLoc.name, "^CURRENT_([0-9]+)");
            if ((matches.Count == 1) && (matches[0].Groups.Count == 2))
            {
                String matchStr = (matches[0].Groups[1].ToString());
                byte led = Convert.ToByte(matchStr);
                return _CurrentDesc(led, memLoc);
            }

            return memLoc.Description();
        }

        public override String Description(MemBitfield bitfield)
        {
            MemLocation memLoc = bitfield.GetMemLocation();

            if ((memLoc.name == LABEL_PAGE_BASE_ADDR) && (bitfield.name == LABEL_SEL)) return _PageBaseAddrSelDesc(bitfield);
            if ((memLoc.name == LABEL_SRAM_SEL) && (bitfield.name == LABEL_SEL)) return _SramSelDesc(bitfield);
            if ((memLoc.name == LABEL_BUS_DERATE_GAIN) && (bitfield.name == LABEL_GAIN)) return _BusDerateGainDesc(bitfield);


            MatchCollection matches;

            matches = Regex.Matches(memLoc.name, "^LED_ENABLE_");
            if (matches.Count == 1)
            {
                return _LedEnableDesc();
            }

            return memLoc.Description();
        }

        public BusConfig(String name, bool default_config = false)
            : base(10, name, false, default_config)
        {
            MemLocation memLoc;

            for (byte i = 0; i < 16; i++)
            {
                this.Add(new MemLocation(String.Format("PULSE_{0:D}", i), (ushort)(ADDR_PULSE_AREA + 2 * i)));
            }
            for (byte i = 0; i < 16; i++)
            {
                this.Add(new MemLocation(String.Format("CURRENT_{0:D}", i), (ushort)(ADDR_CURRENT_AREA + 2 * i)));
            }

            this.Add(new MemLocation("RESERVED", 0x40, true));
            this.Add(new MemLocation("RESERVED", 0x42, true));

            memLoc = new MemLocation("LED_ENABLE_0_7", ADDR_LED_ENABLE_0_7);
            memLoc.AddBitfield(new MemBitfield("enable", 8, 0));
            this.Add(memLoc);

            memLoc = new MemLocation("LED_ENABLE_8_15", ADDR_LED_ENABLE_8_15);
            memLoc.AddBitfield(new MemBitfield("enable", 8, 0));
            this.Add(memLoc);

            for (ushort a = 0x48; a < 0x60; a += 2) this.Add(new MemLocation("RESERVED", a, true));

            memLoc = new MemLocation(LABEL_PAGE_BASE_ADDR, ADDR_PAGE_BASE_ADDR, default_config);
            memLoc.AddBitfield(new MemBitfield(LABEL_SEL, 3, 0));
            this.Add(memLoc);

            memLoc = new MemLocation("ASSERT_DIAG", ADDR_ASSERT_DIAG, default_config);
            memLoc.AddBitfield(new MemBitfield("assert_diag0", 1, 0));
            memLoc.AddBitfield(new MemBitfield("assert_diag1", 1, 1));
            memLoc.AddBitfield(new MemBitfield("mask_diag0_in", 1, 2));
            memLoc.AddBitfield(new MemBitfield("mask_diag1_in", 1, 3));
            memLoc.AddBitfield(new MemBitfield("pass", 6, 4));
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_SRAM_SEL, ADDR_SRAM_SEL, default_config);
            memLoc.AddBitfield(new MemBitfield(LABEL_SEL, 1, 0));
            this.Add(memLoc);

            for (ushort a = 0x66; a < 0x70; a += 2) this.Add(new MemLocation("RESERVED", a, true));

            memLoc = new MemLocation(LABEL_BUS_DERATE_GAIN, ADDR_BUS_DERATE_GAIN, default_config);
            memLoc.AddBitfield(new MemBitfield(LABEL_GAIN, 8, 0));
            this.Add(memLoc);

            if (!default_config)
            {
                this.Add(new MemLocation("CMD_SLEEP", ADDR_CMD_SLEEP));
                this.Add(new MemLocation("CMD_WAKEUP_ACK", ADDR_CMD_WAKEUP_ACK));
                this.Add(new MemLocation("RESERVED", 0x76, true));
                this.Add(new MemLocation("BUS_PULSE_ALL", ADDR_BUS_PULSE_ALL));
                this.Add(new MemLocation("BUS_CURRENT_ALL", ADDR_BUS_CURRENT_ALL));
                this.Add(new MemLocation("CMD_RESET", ADDR_CMD_RESET));
                this.Add(new MemLocation("CMD_UPDATE", ADDR_CMD_UPDATE));
            }
            else
            {
                for (ushort a = ADDR_CMD_SLEEP; a <= ADDR_CMD_UPDATE; a += 2) this.Add(new MemLocation("RESERVED", a, true));
            }
        }

    }
}
