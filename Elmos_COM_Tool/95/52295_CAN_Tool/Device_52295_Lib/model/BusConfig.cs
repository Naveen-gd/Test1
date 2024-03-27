using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device_52295_Lib
{
    public class BusConfig : Memory
    {
        internal const ushort SIZE_PULSE_AREA = 16;
        internal const ushort SIZE_CURRENT_AREA = 16;

        internal const ushort ADDR_PULSE_AREA = 0x00;
        internal const ushort ADDR_CURRENT_AREA = 0x14;

        internal const ushort ADDR_CURR_GROUP_SEL = 0x10;
        internal const ushort ADDR_BUS_DERATE_GAIN = 0x11;

        // commands
        internal const ushort ADDR_SET_EEPROM_KEY = 0x29;
        internal const ushort ADDR_BUS_PULSE_ALL = 0x2A;
        internal const ushort ADDR_BUS_CURRENT_ALL = 0x2B;
        internal const ushort ADDR_ASSERT_DIAG = 0x2C;
        internal const ushort ADDR_MASK_DIAG = 0x2D;
        internal const ushort ADDR_CMD_RESET = 0x2E;
        internal const ushort ADDR_CMD_CLR_BUS_STATUS = 0x2F;

        public byte GetPulse(byte index)
        {
            return (byte)this[(UInt32) (ADDR_PULSE_AREA + index)].data;
        }

        public void SetPulse(byte index, byte value)
        {
            this[(UInt32) (ADDR_PULSE_AREA + index)].SetDataClearModified(value);
        }

        public byte GetCurrent(byte index)
        {
            return (byte)this[(UInt32) (ADDR_CURRENT_AREA + index)].data;
        }

        public void SetCurrent(byte index, byte value)
        {
            this[(UInt32) (ADDR_CURRENT_AREA + index)].SetDataClearModified(value);
        }

        public BusConfig() : base(8, "CONFIG", false)
        {
            MemLocation memLoc;

            for (byte i = 0; i < SIZE_PULSE_AREA; i++) this.Add(new MemLocation(String.Format("PULSE_{0:D}", i), (ushort) (ADDR_PULSE_AREA + i)));
            for (byte i = 0; i < SIZE_CURRENT_AREA; i++) this.Add(new MemLocation(String.Format("CURRENT_{0:D}", i), (ushort)(ADDR_CURRENT_AREA + i)));

            memLoc = new MemLocation("CURR_GROUP_SEL", ADDR_CURR_GROUP_SEL);
            memLoc.AddBitfield(new MemBitfield("group0", 2, 0));
            memLoc.AddBitfield(new MemBitfield("group1", 2, 2));
            memLoc.AddBitfield(new MemBitfield("group2", 2, 4));
            memLoc.AddBitfield(new MemBitfield("group3", 2, 6));
            this.Add(memLoc);

            this.Add(new MemLocation("BUS_DERATE_GAIN", ADDR_BUS_DERATE_GAIN));

            // add some reserved fields
            for (ushort a = 0x12; a < 0x14; a += 1) this.Add(new MemLocation("RESERVED", a, true));
            for (ushort a = 0x24; a < 0x29; a += 1) this.Add(new MemLocation("RESERVED", a, true));

            // commands
            this.Add(new MemLocation("SET_EEPROM_KEY", ADDR_SET_EEPROM_KEY, false, true));
            this.Add(new MemLocation("BUS_PULSE_ALL", ADDR_BUS_PULSE_ALL, false, true));
            this.Add(new MemLocation("BUS_CURRENT_ALL", ADDR_BUS_CURRENT_ALL, false, true));
            this.Add(new MemLocation("CMD_RESET", ADDR_CMD_RESET, false, true));
            this.Add(new MemLocation("CMD_CLR_BUS_STATUS", ADDR_CMD_CLR_BUS_STATUS, false, true));

            memLoc = new MemLocation("ASSERT_DIAG", ADDR_ASSERT_DIAG, false, true);
            memLoc.AddBitfield(new MemBitfield("assert_diag0", 1, 0));
            memLoc.AddBitfield(new MemBitfield("assert_diag1", 1, 1));
            memLoc.AddBitfield(new MemBitfield("assert_diag2", 1, 2));
            memLoc.AddBitfield(new MemBitfield("pass", 5, 3));
            this.Add(memLoc);

            memLoc = new MemLocation("MASK_DIAG", ADDR_MASK_DIAG, false, true);
            memLoc.AddBitfield(new MemBitfield("mask_diag0_in", 1, 0));
            memLoc.AddBitfield(new MemBitfield("mask_diag1_in", 1, 1));
            memLoc.AddBitfield(new MemBitfield("mask_diag2_in", 1, 2));
            memLoc.AddBitfield(new MemBitfield("pass", 5, 3));
            this.Add(memLoc);


            this.Verify();
        }
    }
}
