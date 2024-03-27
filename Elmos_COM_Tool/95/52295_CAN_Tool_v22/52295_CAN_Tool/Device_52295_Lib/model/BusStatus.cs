using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device_52295_Lib
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
        internal const ushort ADDR_VSUP_VS = 0x61;
        internal const ushort ADDR_VSUP_SENSE0 = 0x62;
        internal const ushort ADDR_VSUP_SENSE1 = 0x63;
        internal const ushort ADDR_LED_OPEN_SHORT_0_1 = 0x64;
        internal const ushort ADDR_EVENT_STATUS_L = 0x6C;
        internal const ushort ADDR_EVENT_STATUS_H = 0x6D;
        internal const ushort ADDR_PWMIN_STATUS = 0x6E;
        internal const ushort ADDR_DIAG_STATUS = 0x6F;
        internal const ushort ADDR_CAN_STATUS = 0x70;
        internal const ushort ADDR_CAN_COUNTER = 0x71;
        internal const ushort ADDR_GPIO_STATUS_0 = 0x72;
        internal const ushort ADDR_GPIO_STATUS_1 = 0x73;

        internal const ushort ADDR_MISC_AREA = 0x74;
        internal const ushort ADDR_HW_VERSION_MAJOR = 0x74;
        internal const ushort ADDR_HW_VERSION_MINOR = 0x75;
        internal const ushort ADDR_FW_VERSION_DD = 0x76;
        internal const ushort ADDR_FW_VERSION_MM = 0x77;
        internal const ushort ADDR_FW_VERSION_YYL = 0x78;
        internal const ushort ADDR_FW_VERSION_YYH = 0x79;
        internal const ushort ADDR_PROG_STATUS = 0x7A;
        internal const ushort ADDR_FW_VERSION_NUM = 0x7B;
        internal const ushort ADDR_PARAMETER_CRC_L = 0x7C;
        internal const ushort ADDR_PARAMETER_CRC_H = 0x7D;
        internal const ushort ADDR_ERROR_CODE = 0x7F;

        public ushort GetVLED(byte index)
        {
            ushort temp = (ushort)(this[(UInt32) (ADDR_VLED_AREA + 2*index+1)].data); 
            temp <<= 8;
            temp |= (ushort)(this[(UInt32) (ADDR_VLED_AREA + 2*index+0)].data);
            return temp;
        }

        public ushort GetVDIF(byte index)
        {
            ushort temp = (ushort)(this[(UInt32) (ADDR_VDIF_AREA + 2 * index + 1)].data);
            temp <<= 8;
            temp |= (ushort)(this[(UInt32) (ADDR_VDIF_AREA + 2 * index + 0)].data);
            return temp;
        }

        public ushort GetILED(byte index)
        {
            ushort temp = (ushort)(this[(UInt32) (ADDR_ILED_AREA + 2 * index + 1)].data);
            temp <<= 8;
            temp |= (ushort)(this[(UInt32) (ADDR_ILED_AREA + 2 * index + 0)].data);
            return temp;
        }

        public bool GetDiagEn(byte index)
        {
            MemLocation memLoc = this[(UInt32)(ADDR_LED_OPEN_SHORT_0_1 + (index / 2))];
            bool ret;
            if ((index % 2) == 1) ret = memLoc.GetBitfield("upper_enabled").GetBool();
            else ret = memLoc.GetBitfield("lower_enabled").GetBool();
            return ret;
        }

        public bool GetOpen(byte index)
        {
            MemLocation memLoc = this[(UInt32)(ADDR_LED_OPEN_SHORT_0_1 + (index / 2))];
            bool ret;
            if ((index % 2) == 1) ret = memLoc.GetBitfield("upper_open").GetBool();
            else ret = memLoc.GetBitfield("lower_open").GetBool();
            return ret;
        }

        public bool GetShort(byte index)
        {
            MemLocation memLoc = this[(UInt32)(ADDR_LED_OPEN_SHORT_0_1 + (index / 2))];
            bool ret;
            if ((index % 2) == 1) ret = memLoc.GetBitfield("upper_short").GetBool();
            else ret = memLoc.GetBitfield("lower_short").GetBool();
            return ret;
        }

        public bool GetUnknown(byte index)
        {
            MemLocation memLoc = this[(UInt32)(ADDR_LED_OPEN_SHORT_0_1 + (index / 2))];
            bool ret;
            if ((index % 2) == 1) ret = memLoc.GetBitfield("upper_unknown").GetBool();
            else ret = memLoc.GetBitfield("lower_unknown").GetBool();
            return ret;
        }

        public byte vt
        {
            get { return (byte)(this[ADDR_VT].data); }
        }

        public byte vsup_vs
        {
            get { return (byte)(this[ADDR_VSUP_VS].data); }
        }

        public byte vsup_sense0
        {
            get { return (byte)(this[ADDR_VSUP_SENSE0].data); }
        }

        public byte vsup_sense1
        {
            get { return (byte)(this[ADDR_VSUP_SENSE1].data); }
        }

        public byte can_counter
        {
            get { return (byte)(this[ADDR_CAN_COUNTER].data); }
        }

        public byte gpio_status_0
        {
            get { return (byte)(this[ADDR_GPIO_STATUS_0].data); }
        }

        public byte gpio_status_1
        {
            get { return (byte)(this[ADDR_GPIO_STATUS_1].data); }
        }

        public char hw_version_major
        {
            get { return (char)(this[ADDR_HW_VERSION_MAJOR].data); }
        }

        public char hw_version_minor
        {
            get { return (char)(this[ADDR_HW_VERSION_MINOR].data); }
        }

        public byte fw_version_dd
        {
            get { return (byte)(this[ADDR_FW_VERSION_DD].data); }
        }

        public byte fw_version_mm
        {
            get { return (byte)(this[ADDR_FW_VERSION_MM].data); }
        }

        public ushort parameter_crc{
            get
            {
                if (DeviceType.IsE52295A)
                {
                    ushort temp = 0;
                    temp |= (byte)(this[ADDR_PARAMETER_CRC_H].data);
                    temp <<= 8;
                    temp |= (byte)(this[ADDR_PARAMETER_CRC_L].data);
                    return temp;
                }
                else
                {
                    return 0;
                }
            }
        }

        internal byte error_code
        {
            get { return (byte)(this[ADDR_ERROR_CODE].data); }
        }

        public byte fw_version_num
        {
            get { return (byte)(this[ADDR_FW_VERSION_NUM].data); }
        }

        public ushort fw_version_yyyy
        {
            get
            {
                ushort temp = (ushort)(this[ADDR_FW_VERSION_YYL].data);
                temp <<= 8;
                temp |= (ushort)(this[ADDR_FW_VERSION_YYH].data);
                return temp;
            }
        }

        // event_status_l
        public bool reset
        {
            get { return this[ADDR_EVENT_STATUS_L].GetBitfield("reset").bitFlag.GetClearWasIs1(); }
        }

        public bool vt_derating
        {
            get { return this[ADDR_EVENT_STATUS_L].GetBitfield("vt_derating").bitFlag.GetClearWasIs1(); }
        }

        public bool vs_derating
        {
            get { return this[ADDR_EVENT_STATUS_L].GetBitfield("vs_derating").bitFlag.GetClearWasIs1(); }
        }

        public bool vs_too_low
        {
            get { return this[ADDR_EVENT_STATUS_L].GetBitfield("vs_too_low").bitFlag.GetClearWasIs1(); }
        }

        public bool vs_too_high
        {
            get { return this[ADDR_EVENT_STATUS_L].GetBitfield("vs_too_high").bitFlag.GetClearWasIs1(); }
        }

        public bool vt_too_high
        {
            get { return this[ADDR_EVENT_STATUS_L].GetBitfield("vt_too_high").bitFlag.GetClearWasIs1(); }
        }

        public bool led_short
        {
            get { return this[ADDR_EVENT_STATUS_L].GetBitfield("led_short").bitFlag.GetClearWasIs1(); }
        }

        public bool led_open
        {
            get { return this[ADDR_EVENT_STATUS_L].GetBitfield("led_open").bitFlag.GetClearWasIs1(); }
        }

        // event_status_h
        public bool clk_accuracy_low
        {
            get { return this[ADDR_EVENT_STATUS_H].GetBitfield("clk_accuracy_low").bitFlag.GetClearWasIs1(); }
        }

        public bool bus_error
        {
            get { return this[ADDR_EVENT_STATUS_H].GetBitfield("bus_error").bitFlag.GetClearWasIs1(); }
        }

        public bool led_current_mismatch
        {
            get { return this[ADDR_EVENT_STATUS_H].GetBitfield("led_current_mismatch").bitFlag.GetClearWasIs1(); }
        }

        public bool led_pwm_error
        {
            get { return this[ADDR_EVENT_STATUS_H].GetBitfield("led_pwm_error").bitFlag.GetClearWasIs1(); }
        }

        public bool gpio_curr_dac_error
        {
            get { return this[ADDR_EVENT_STATUS_H].GetBitfield("gpio_curr_dac_error").bitFlag.GetClearWasIs1(); }
        }

        public bool supply_too_low
        {
            get { return this[ADDR_EVENT_STATUS_H].GetBitfield("supply_too_low").bitFlag.GetClearWasIs1(); }
        }

        public bool meas_error
        {
            get { return this[ADDR_EVENT_STATUS_H].GetBitfield("meas_error").bitFlag.GetClearWasIs1(); }
        }

        public bool gpio_binning_error
        {
            get {
                if (DeviceType.IsE52295A)
                    return this[ADDR_EVENT_STATUS_H].GetBitfield("gpio_binning_error").bitFlag.GetClearWasIs1();
                else
                    return false;
            }
        }

        // pwmin_status
        public bool pwmin_low
        {
            get { return this[ADDR_PWMIN_STATUS].GetBitfield("pwmin_low").GetBool(); }
        }

        public bool pwmin_mid
        {
            get { return this[ADDR_PWMIN_STATUS].GetBitfield("pwmin_mid").GetBool(); }
        }

        public bool pwmin_high
        {
            get { return this[ADDR_PWMIN_STATUS].GetBitfield("pwmin_high").GetBool(); }
        }

        public bool pwmin_invalid
        {
            get { return this[ADDR_PWMIN_STATUS].GetBitfield("pwmin_invalid").GetBool(); }
        }

        public bool bus_failsafe
        {
            get { return this[ADDR_PWMIN_STATUS].GetBitfield("bus_failsafe").bitFlag.GetClearWasIs1(); }
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

        public bool diag0_in
        {
            get { return this[ADDR_DIAG_STATUS].GetBitfield("diag0_in").GetBool(); }
        }

        public bool diag1_in
        {
            get { return this[ADDR_DIAG_STATUS].GetBitfield("diag1_in").GetBool(); }
        }

        public bool diag2_in
        {
            get { return this[ADDR_DIAG_STATUS].GetBitfield("diag2_in").GetBool(); }
        }

        public bool assert_active
        {
            get { return this[ADDR_DIAG_STATUS].GetBitfield("assert_active").GetBool(); }
        }

        public bool mask_active
        {
            get { return this[ADDR_DIAG_STATUS].GetBitfield("mask_active").GetBool(); }
        }

        // can_status
        public byte com_state
        {
            get { return (byte) this[ADDR_CAN_STATUS].GetBitfield("com_state").GetData(); }
        }

        public bool dlc_error_comb
        {
            get { return this[ADDR_CAN_STATUS].GetBitfield("dlc_error_comb").bitFlag.GetClearWasIs1(); }
        }

        public bool len_error_comb
        {
            get { return this[ADDR_CAN_STATUS].GetBitfield("len_error_comb").bitFlag.GetClearWasIs1(); }
        }

        public bool crc_error_comb
        {
            get { return this[ADDR_CAN_STATUS].GetBitfield("crc_error_comb").bitFlag.GetClearWasIs1(); }
        }

        public bool bz_error_comb
        {
            get { return this[ADDR_CAN_STATUS].GetBitfield("bz_error_comb").bitFlag.GetClearWasIs1(); }
        }

        public bool timeout
        {
            get { return this[ADDR_CAN_STATUS].GetBitfield("timeout").bitFlag.GetClearWasIs1(); }
        }

        // PROG_STATUS
        public bool prog_done
        {
            get { return this[ADDR_PROG_STATUS].GetBitfield("prog_done").bitFlag.GetClearWasIs1(); }
        }

        public bool prog_error
        {
            get { return this[ADDR_PROG_STATUS].GetBitfield("prog_error").bitFlag.GetClearWasIs1(); }
        }

        public bool prog_access
        {
            get { return this[ADDR_PROG_STATUS].GetBitfield("prog_access").bitFlag.GetClearWasIs1(); }
        }

        public BusStatus()
            : base(8, "STATUS", true)
        {
            MemLocation memLoc;

            for (byte i = 0; i < (SIZE_VLED_AREA/2); i++)
            {
                this.Add(new MemLocation(String.Format("VLED_L_{0:D}", i), (ushort)(ADDR_VLED_AREA + 2 * i + 0)));
                this.Add(new MemLocation(String.Format("VLED_H_{0:D}", i), (ushort)(ADDR_VLED_AREA + 2 * i + 1)));
            }
            for (byte i = 0; i < (SIZE_VDIF_AREA/2); i++) {
                this.Add(new MemLocation(String.Format("VDIF_L_{0:D}", i), (ushort)(ADDR_VDIF_AREA + 2 * i + 0)));
                this.Add(new MemLocation(String.Format("VDIF_H_{0:D}", i), (ushort)(ADDR_VDIF_AREA + 2 * i + 1)));
            }
            for (byte i = 0; i < (SIZE_ILED_AREA/2); i++) {
                this.Add(new MemLocation(String.Format("ILED_L_{0:D}", i), (ushort)(ADDR_ILED_AREA + 2 * i + 0)));
                this.Add(new MemLocation(String.Format("ILED_H_{0:D}", i), (ushort)(ADDR_ILED_AREA + 2 * i + 1)));
            }

            this.Add(new MemLocation("VT", ADDR_VT));
            this.Add(new MemLocation("VSUP_VS", ADDR_VSUP_VS));
            this.Add(new MemLocation("VSUP_SENSE0", ADDR_VSUP_SENSE0));
            this.Add(new MemLocation("VSUP_SENSE1", ADDR_VSUP_SENSE1));
            this.Add(new MemLocation("CAN_COUNTER", ADDR_CAN_COUNTER));
            this.Add(new MemLocation("HW_VERSION_MAJOR", ADDR_HW_VERSION_MAJOR));
            this.Add(new MemLocation("HW_VERSION_MINOR", ADDR_HW_VERSION_MINOR));
            this.Add(new MemLocation("FW_VERSION_DD", ADDR_FW_VERSION_DD));
            this.Add(new MemLocation("FW_VERSION_MM", ADDR_FW_VERSION_MM));
            this.Add(new MemLocation("FW_VERSION_YYL", ADDR_FW_VERSION_YYL));
            this.Add(new MemLocation("FW_VERSION_YYH", ADDR_FW_VERSION_YYH));
            this.Add(new MemLocation("FW_VERSION_NUM", ADDR_FW_VERSION_NUM));

            for (byte i = 0; i < 8; i++) {
                memLoc = new MemLocation(String.Format("LED_OPEN_SHORT_{0:D}_{1:D}", 2 * i + 0, 2 * i + 1), (ushort)(ADDR_LED_OPEN_SHORT_0_1 + i));
                memLoc.AddBitfield(new MemBitfield("lower_enabled", 1, 0));
                memLoc.AddBitfield(new MemBitfield("lower_short", 1, 1));
                memLoc.AddBitfield(new MemBitfield("lower_open", 1, 2));
                memLoc.AddBitfield(new MemBitfield("lower_unknown", 1, 3));
                memLoc.AddBitfield(new MemBitfield("upper_enabled", 1, 4));
                memLoc.AddBitfield(new MemBitfield("upper_short", 1, 5));
                memLoc.AddBitfield(new MemBitfield("upper_open", 1, 6));
                memLoc.AddBitfield(new MemBitfield("upper_unknown", 1, 7));
                this.Add(memLoc);
            }

            memLoc = new MemLocation("GPIO_STATUS_0", ADDR_GPIO_STATUS_0);
            memLoc.AddBitfield(new MemBitfield("dig_val", 1, 0));
            memLoc.AddBitfield(new MemBitfield("ana_val", 8, 0));
            if (DeviceType.IsE52295A) memLoc.AddBitfield(new MemBitfield("bin_short", 1, 0));
            if (DeviceType.IsE52295A) memLoc.AddBitfield(new MemBitfield("bin_open", 1, 1));
            if (DeviceType.IsE52295A) memLoc.AddBitfield(new MemBitfield("bin_val", 6, 2));
            this.Add(memLoc);
            memLoc = new MemLocation("GPIO_STATUS_1", ADDR_GPIO_STATUS_1);
            memLoc.AddBitfield(new MemBitfield("dig_val", 1, 0));
            memLoc.AddBitfield(new MemBitfield("ana_val", 8, 0));
            if (DeviceType.IsE52295A) memLoc.AddBitfield(new MemBitfield("bin_short", 1, 0));
            if (DeviceType.IsE52295A) memLoc.AddBitfield(new MemBitfield("bin_open", 1, 1));
            if (DeviceType.IsE52295A) memLoc.AddBitfield(new MemBitfield("bin_val", 6, 2));
            this.Add(memLoc);

            memLoc = new MemLocation("EVENT_STATUS_L", ADDR_EVENT_STATUS_L);
            memLoc.AddBitfield(new MemBitfield("reset", 1, 0));
            memLoc.AddBitfield(new MemBitfield("vt_derating", 1, 1));
            memLoc.AddBitfield(new MemBitfield("vs_derating", 1, 2));
            memLoc.AddBitfield(new MemBitfield("vs_too_low", 1, 3));
            memLoc.AddBitfield(new MemBitfield("vs_too_high", 1, 4));
            memLoc.AddBitfield(new MemBitfield("vt_too_high", 1, 5));
            memLoc.AddBitfield(new MemBitfield("led_short", 1, 6));
            memLoc.AddBitfield(new MemBitfield("led_open", 1, 7));
            this.Add(memLoc);

            memLoc = new MemLocation("EVENT_STATUS_H", ADDR_EVENT_STATUS_H);
            memLoc.AddBitfield(new MemBitfield("clk_accuracy_low", 1, 0));
            memLoc.AddBitfield(new MemBitfield("bus_error", 1, 1));
            memLoc.AddBitfield(new MemBitfield("led_current_mismatch", 1, 2));
            memLoc.AddBitfield(new MemBitfield("led_pwm_error", 1, 3));
            memLoc.AddBitfield(new MemBitfield("gpio_curr_dac_error", 1, 4));
            memLoc.AddBitfield(new MemBitfield("supply_too_low", 1, 5));
            memLoc.AddBitfield(new MemBitfield("meas_error", 1, 6));
            if (DeviceType.IsE52295A) memLoc.AddBitfield(new MemBitfield("gpio_binning_error", 1, 7));
            this.Add(memLoc);

            memLoc = new MemLocation("PWMIN_STATUS", ADDR_PWMIN_STATUS);
            memLoc.AddBitfield(new MemBitfield("pwmin_low", 1, 0));
            memLoc.AddBitfield(new MemBitfield("pwmin_high", 1, 1));
            memLoc.AddBitfield(new MemBitfield("pwmin_invalid", 1, 2));
            memLoc.AddBitfield(new MemBitfield("bus_failsafe", 1, 3));
            memLoc.AddBitfield(new MemBitfield("pwmin_mid", 1, 4));
            this.Add(memLoc);

            memLoc = new MemLocation("DIAG_STATUS", ADDR_DIAG_STATUS);
            memLoc.AddBitfield(new MemBitfield("diag0_out", 1, 0));
            memLoc.AddBitfield(new MemBitfield("diag1_out", 1, 1));
            memLoc.AddBitfield(new MemBitfield("diag2_out", 1, 2));
            memLoc.AddBitfield(new MemBitfield("diag0_in", 1, 3));
            memLoc.AddBitfield(new MemBitfield("diag1_in", 1, 4));
            memLoc.AddBitfield(new MemBitfield("diag2_in", 1, 5));
            memLoc.AddBitfield(new MemBitfield("assert_active", 1, 6));
            memLoc.AddBitfield(new MemBitfield("mask_active", 1, 7));
            this.Add(memLoc);
            
            memLoc = new MemLocation("CAN_STATUS", ADDR_CAN_STATUS);
            memLoc.AddBitfield(new MemBitfield("com_state", 2, 0));
            memLoc.AddBitfield(new MemBitfield("dlc_error_comb", 1, 2));
            memLoc.AddBitfield(new MemBitfield("len_error_comb", 1, 3));
            memLoc.AddBitfield(new MemBitfield("crc_error_comb", 1, 4));
            memLoc.AddBitfield(new MemBitfield("bz_error_comb", 1, 5));
            memLoc.AddBitfield(new MemBitfield("timeout", 1, 6));
            this.Add(memLoc);

            memLoc = new MemLocation("PROG_STATUS", ADDR_PROG_STATUS);
            memLoc.AddBitfield(new MemBitfield("prog_done", 1, 0));
            memLoc.AddBitfield(new MemBitfield("prog_error", 1, 1));
            memLoc.AddBitfield(new MemBitfield("prog_access", 1, 2));
            this.Add(memLoc);

            if (DeviceType.IsE52295A) 
                this.Add(new MemLocation("PARAMETER_CRC_L", ADDR_PARAMETER_CRC_L));
            else
                this.Add(new MemLocation("RESERVED", ADDR_PARAMETER_CRC_L, true));
            if (DeviceType.IsE52295A) 
                this.Add(new MemLocation("PARAMETER_CRC_H", ADDR_PARAMETER_CRC_H));
            else
                this.Add(new MemLocation("RESERVED", ADDR_PARAMETER_CRC_H, true));

            // reserved
            this.Add(new MemLocation("RESERVED", 0x7E, true));

            this.Add(new MemLocation("ERROR_CODE", ADDR_ERROR_CODE));

            this.Verify();
        }

        public String getCommStateString()
        {
            switch (com_state)
            {
                case 0: return "NO_MON";
                case 1: return "INIT";
                case 2: return "SAFE";
                case 3: return "NORMAL";
            }
            return "n/a";
        }

        public String getLedDiagStateString(byte led)
        {
            if (!GetDiagEn(led)) return "DISABLED";
            if (GetOpen(led)) return "OPEN";
            if (GetShort(led)) return "SHORT";
            if (GetUnknown(led)) return "UNKNOWN";
            return "OK";
        }
    }

}
