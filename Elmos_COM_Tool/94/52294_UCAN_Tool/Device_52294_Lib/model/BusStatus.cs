using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MemLib;

namespace Device_52294_Lib
{
    public class BusStatus : Memory
    {
        internal const ushort SIZE_VLED_AREA = 0x20;
        internal const ushort SIZE_VDIF_AREA = 0x20;
        internal const ushort SIZE_ILED_AREA = 0x20;

        internal const ushort ADDR_VLED_AREA = 0x00;
        internal const ushort ADDR_VDIF_AREA = 0x20;
        internal const ushort ADDR_ILED_AREA = 0x40;

        internal const ushort ADDR_DIAG_AREA = 0x60;
        internal const ushort ADDR_VT = 0x60;
        internal const ushort ADDR_VSUP = 0x62;
        internal const ushort ADDR_VDD5 = 0x64;
        internal const ushort ADDR_RUN_STATUS = 0x6E;
        internal const ushort ADDR_LED_OPEN_0_7 = 0x70;
        internal const ushort ADDR_LED_OPEN_8_15 = 0x72;
        internal const ushort ADDR_LED_SHORT_0_7 = 0x74;
        internal const ushort ADDR_LED_SHORT_8_15 = 0x76;
        internal const ushort ADDR_EVENT_STATUS = 0x78;
        internal const ushort ADDR_PWMIN_STATUS = 0x7A;
        internal const ushort ADDR_DIAG_STATUS = 0x7C;
        internal const ushort ADDR_PROG_STATUS = 0x7E;

        public ushort GetVLED(byte index)
        {
            return (ushort)(this[(UInt32) (ADDR_VLED_AREA + 2*index)].data);
        }

        public ushort GetVDIF(byte index)
        {
            return (ushort)(this[(UInt32)(ADDR_VDIF_AREA + 2 * index)].data);
        }

        public ushort GetILED(byte index)
        {
            return (ushort)(this[(UInt32)(ADDR_ILED_AREA + 2 * index)].data);
        }

        public ushort vt
        {
            get { return (ushort)(this[ADDR_VT].data); }
        }

        public ushort vsup
        {
            get { return (ushort)(this[ADDR_VSUP].data); }
        }

        public ushort vdd5
        {
            get { return (ushort)(this[ADDR_VDD5].data); }
        }

        // run status
        public bool run_parity
        {
            get { return this[ADDR_RUN_STATUS].GetBitfield("parity").bitFlag.GetClearWasIs1(); }
        }
        public bool run_por
        {
            get { return this[ADDR_RUN_STATUS].GetBitfield("por").bitFlag.GetClearWasIs1(); }
        }
        public bool run_oe
        {
            get { return this[ADDR_RUN_STATUS].GetBitfield("oe").bitFlag.GetClearWasIs1(); }
        }
        public bool run_wakeupsymbol
        {
            get { return this[ADDR_RUN_STATUS].GetBitfield("wakeupsymbol").bitFlag.GetClearWasIs1(); }
        }
        public bool run_wait_wake_ack
        {
            get { return this[ADDR_RUN_STATUS].GetBitfield("wait_wake_ack").bitFlag.GetClearWasIs1(); }
        }
        public bool run_active
        {
            get { return this[ADDR_RUN_STATUS].GetBitfield("active").bitFlag.GetClearWasIs1(); }
        }
        public bool run_ram_debugging
        {
            get { return this[ADDR_RUN_STATUS].GetBitfield("ram_debugging").bitFlag.GetClearWasIs1(); }
        }

        public bool GetOpen(byte index)
        {
            bool ret = false;
            UInt32 addr;
            if (index < 8) addr = ADDR_LED_OPEN_0_7;
            else addr = ADDR_LED_OPEN_8_15;
            MemLocation memLoc = this[addr];
            if (index > 7) index -= 8;
            if (((memLoc.data >> index) & 1) == 1) ret = true;
            else ret = false;
            return ret;
        }

        public bool GetShort(byte index)
        {
            bool ret = false;
            UInt32 addr;
            if (index < 8) addr = ADDR_LED_SHORT_0_7;
            else addr = ADDR_LED_SHORT_8_15;
            MemLocation memLoc = this[addr];
            if (index > 7) index -= 8;
            if (((memLoc.data >> index) & 1) == 1) ret = true;
            else ret = false;
            return ret;
        }

        // event_status
        public bool reset
        {
            get { return this[ADDR_EVENT_STATUS].GetBitfield("reset").bitFlag.GetClearWasIs1(); }
        }

        public bool timeout
        {
            get { return this[ADDR_EVENT_STATUS].GetBitfield("timeout").bitFlag.GetClearWasIs1(); }
        }

        public bool derating
        {
            get { return this[ADDR_EVENT_STATUS].GetBitfield("derating").bitFlag.GetClearWasIs1(); }
        }

        public bool vs_too_low
        {
            get { return this[ADDR_EVENT_STATUS].GetBitfield("vs_too_low").bitFlag.GetClearWasIs1(); }
        }

        public bool vs_too_high
        {
            get { return this[ADDR_EVENT_STATUS].GetBitfield("vs_too_high").bitFlag.GetClearWasIs1(); }
        }

        public bool vt_too_high
        {
            get { return this[ADDR_EVENT_STATUS].GetBitfield("vt_too_high").bitFlag.GetClearWasIs1(); }
        }

        public bool led_short
        {
            get { return this[ADDR_EVENT_STATUS].GetBitfield("led_short").bitFlag.GetClearWasIs1(); }
        }

        public bool led_open
        {
            get { return this[ADDR_EVENT_STATUS].GetBitfield("led_open").bitFlag.GetClearWasIs1(); }
        }
        public bool meas_error
        {
            get { return this[ADDR_EVENT_STATUS].GetBitfield("meas_error").bitFlag.GetClearWasIs1(); }
        }
        public bool bus_error
        {
            get { return this[ADDR_EVENT_STATUS].GetBitfield("bus_error").bitFlag.GetClearWasIs1(); }
        }

        // pwmin_status
        public bool pwmin_lp
        {
            get { return this[ADDR_PWMIN_STATUS].GetBitfield("pwmin_lp").GetBool(); }
        }

        public bool pwmin_hp
        {
            get { return this[ADDR_PWMIN_STATUS].GetBitfield("pwmin_hp").GetBool(); }
        }

        public bool pwmin_lp_state_bus
        {
            get { return this[ADDR_PWMIN_STATUS].GetBitfield("lp_state_bus").GetBool(); }
        }

        public bool pwmin_hp_state_bus
        {
            get { return this[ADDR_PWMIN_STATUS].GetBitfield("hp_state_bus").GetBool(); }
        }

        public bool pwmin_lp_state_fallback
        {
            get { return this[ADDR_PWMIN_STATUS].GetBitfield("lp_state_fallback").bitFlag.GetClearWasIs1(); }
        }

        public bool pwmin_hp_state_fallback
        {
            get { return this[ADDR_PWMIN_STATUS].GetBitfield("hp_state_fallback").bitFlag.GetClearWasIs1(); }
        }

        public bool pwmin_lp_state_direct
        {
            get { return this[ADDR_PWMIN_STATUS].GetBitfield("lp_state_direct").bitFlag.GetClearWasIs1(); }
        }

        public bool pwmin_hp_state_direct
        {
            get { return this[ADDR_PWMIN_STATUS].GetBitfield("hp_state_direct").bitFlag.GetClearWasIs1(); }
        }

        public bool pwmin_oe_state
        {
            get { return this[ADDR_PWMIN_STATUS].GetBitfield("oe_state").bitFlag.GetClearWasIs1(); }
        }

        // diag_status
        public bool diag0_out
        {
            get { return this[ADDR_DIAG_STATUS].GetBitfield("diag0_out").GetBool(); }
        }

        public bool diag1_out
        {
            get { return this[ADDR_DIAG_STATUS].GetBitfield("diag1_out").GetBool(); }
        }

        public bool diag2_out
        {
            get { return this[ADDR_DIAG_STATUS].GetBitfield("diag2_out").GetBool(); }
        }

        public bool diag3_out
        {
            get { return this[ADDR_DIAG_STATUS].GetBitfield("diag3_out").GetBool(); }
        }

        public bool diag0_in
        {
            get { return this[ADDR_DIAG_STATUS].GetBitfield("diag0_in").GetBool(); }
        }

        public bool diag1_in
        {
            get { return this[ADDR_DIAG_STATUS].GetBitfield("diag1_in").GetBool(); }
        }

        // PROG_STATUS
        public bool prog_busy
        {
            get { return this[ADDR_PROG_STATUS].GetBitfield("busy").bitFlag.GetClearWasIs1(); }
        }

        public bool prog_error
        {
            get { return this[ADDR_PROG_STATUS].GetBitfield("error").bitFlag.GetClearWasIs1(); }
        }

        public uint prog_base_addr_sel
        {
            get { return this[ADDR_PROG_STATUS].GetBitfield("base_addr_sel").GetData(); }
        }

        public bool prog_sram_sel
        {
            get { return this[ADDR_PROG_STATUS].GetBitfield("sram_sel").bitFlag.GetClearWasIs1(); }
        }

        public BusStatus() : base(10, "BUS_STATUS", true)
        {
            MemLocation memLoc;

            for (byte i = 0; i < (SIZE_VLED_AREA/2); i++)
            {
                this.Add(new MemLocation(String.Format("VLED_{0:D}", i), (ushort)(ADDR_VLED_AREA + 2 * i)));
            }
            for (byte i = 0; i < (SIZE_VDIF_AREA/2); i++) {
                this.Add(new MemLocation(String.Format("VDIF_{0:D}", i), (ushort)(ADDR_VDIF_AREA + 2 * i)));
            }
            for (byte i = 0; i < (SIZE_ILED_AREA/2); i++) {
                this.Add(new MemLocation(String.Format("ILED_{0:D}", i), (ushort)(ADDR_ILED_AREA + 2 * i)));
            }

            this.Add(new MemLocation("VT", ADDR_VT));
            this.Add(new MemLocation("VSUP_VS", ADDR_VSUP));
            this.Add(new MemLocation("VDD5", ADDR_VDD5));

            this.Add(new MemLocation("RESERVED", 0x66, true));
            this.Add(new MemLocation("RESERVED", 0x68, true));
            this.Add(new MemLocation("RESERVED", 0x6A, true));
            this.Add(new MemLocation("RESERVED", 0x6C, true));

            memLoc = new MemLocation("RUN_STATUS", ADDR_RUN_STATUS);
            memLoc.AddBitfield(new MemBitfield("parity", 1, 0));
            memLoc.AddBitfield(new MemBitfield("por", 1, 1));
            memLoc.AddBitfield(new MemBitfield("oe", 1, 2));
            memLoc.AddBitfield(new MemBitfield("wakeupsymbol", 1, 3));
            memLoc.AddBitfield(new MemBitfield("wait_wake_ack", 1, 4));
            memLoc.AddBitfield(new MemBitfield("active", 1, 5));
            memLoc.AddBitfield(new MemBitfield("ram_debugging", 1, 6));
            this.Add(memLoc);

            this.Add(new MemLocation("LED_OPEN_0_7", ADDR_LED_OPEN_0_7));
            this.Add(new MemLocation("LED_OPEN_8_15", ADDR_LED_OPEN_8_15));
            this.Add(new MemLocation("LED_SHORT_0_7", ADDR_LED_SHORT_0_7));
            this.Add(new MemLocation("LED_SHORT_8_15", ADDR_LED_SHORT_8_15));

            memLoc = new MemLocation("EVENT_STATUS", ADDR_EVENT_STATUS);
            memLoc.AddBitfield(new MemBitfield("reset", 1, 0));
            memLoc.AddBitfield(new MemBitfield("timeout", 1, 1));
            memLoc.AddBitfield(new MemBitfield("derating", 1, 2));
            memLoc.AddBitfield(new MemBitfield("vs_too_low", 1, 3));
            memLoc.AddBitfield(new MemBitfield("vs_too_high", 1, 4));
            memLoc.AddBitfield(new MemBitfield("vt_too_high", 1, 5));
            memLoc.AddBitfield(new MemBitfield("led_short", 1, 6));
            memLoc.AddBitfield(new MemBitfield("led_open", 1, 7));
            memLoc.AddBitfield(new MemBitfield("meas_error", 1, 8));
            memLoc.AddBitfield(new MemBitfield("bus_error", 1, 9));
            this.Add(memLoc);

            memLoc = new MemLocation("PWMIN_STATUS", ADDR_PWMIN_STATUS);
            memLoc.AddBitfield(new MemBitfield("pwmin_lp", 1, 0));
            memLoc.AddBitfield(new MemBitfield("pwmin_hp", 1, 1));
            memLoc.AddBitfield(new MemBitfield("lp_state_bus", 1, 2));
            memLoc.AddBitfield(new MemBitfield("hp_state_bus", 1, 3));
            memLoc.AddBitfield(new MemBitfield("lp_state_fallback", 1, 4));
            memLoc.AddBitfield(new MemBitfield("hp_state_fallback", 1, 5));
            memLoc.AddBitfield(new MemBitfield("lp_state_direct", 1, 6));
            memLoc.AddBitfield(new MemBitfield("hp_state_direct", 1, 7));
            // reserved8
            memLoc.AddBitfield(new MemBitfield("oe_state", 1, 9));
            this.Add(memLoc);

            memLoc = new MemLocation("DIAG_STATUS", ADDR_DIAG_STATUS);
            memLoc.AddBitfield(new MemBitfield("diag0_out", 1, 0));
            memLoc.AddBitfield(new MemBitfield("diag1_out", 1, 1));
            memLoc.AddBitfield(new MemBitfield("diag0_in", 1, 2));
            memLoc.AddBitfield(new MemBitfield("diag1_in", 1, 3));
            memLoc.AddBitfield(new MemBitfield("diag2_out", 1, 4));
            memLoc.AddBitfield(new MemBitfield("diag3_out", 1, 5));
            this.Add(memLoc);

            memLoc = new MemLocation("PROG_STATUS", ADDR_PROG_STATUS);
            memLoc.AddBitfield(new MemBitfield("busy", 1, 0));
            memLoc.AddBitfield(new MemBitfield("error", 1, 1));
            memLoc.AddBitfield(new MemBitfield("base_addr_sel", 3, 2));
            memLoc.AddBitfield(new MemBitfield("sram_sel", 1, 5));
            this.Add(memLoc);
        }

        public String getLedDiagStateString(byte led)
        {
            if (GetOpen(led) && GetShort(led)) return "OPENSHORT";
            if (GetOpen(led)) return "OPEN";
            if (GetShort(led)) return "SHORT";
            return "OK";
        }

        public String getPwminLpStateString()
        {
            if (pwmin_lp_state_bus && !pwmin_lp_state_direct && !pwmin_lp_state_fallback) return "BUS";
            if (!pwmin_lp_state_bus && pwmin_lp_state_direct && !pwmin_lp_state_fallback) return "DIRECT";
            if (!pwmin_lp_state_bus && !pwmin_lp_state_direct && pwmin_lp_state_fallback) return "FALLBACK";
            return "UNKNOWN";
        }

        public String getPwminHpStateString()
        {
            if (pwmin_hp_state_bus && !pwmin_hp_state_direct && !pwmin_hp_state_fallback) return "BUS";
            if (!pwmin_hp_state_bus && pwmin_hp_state_direct && !pwmin_hp_state_fallback) return "DIRECT";
            if (!pwmin_hp_state_bus && !pwmin_hp_state_direct && pwmin_hp_state_fallback) return "FALLBACK";
            return "UNKNOWN";
        }
    }

}
