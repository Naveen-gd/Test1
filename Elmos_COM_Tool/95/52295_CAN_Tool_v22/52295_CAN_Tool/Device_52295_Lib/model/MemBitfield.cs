using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Device_52295_Lib
{
    public class MemBitfield
    {
        private MemLocation _memLocationRef;
        private String _desc; 

        public BitFlag bitFlag;
        public string name;
        public byte bits;
        public byte lsb;

        public void SetMemLocation(MemLocation memLoc)
        {
            if (_memLocationRef == null)
                _memLocationRef = memLoc;
        }

        public MemLocation GetMemLocation()
        {
            return _memLocationRef;
        }

        public UInt32 GetData()
        {
            UInt32 mask = (UInt32)((1 << bits) - 1);
            return (_memLocationRef.data >> lsb) & mask;
        }

        public UInt32 MaxValue()
        {
            UInt64 ret = (UInt64)(((UInt64)1 << bits) - 1);
            return (UInt32)ret;
        }

        public bool SetData(UInt32 value)
        {
            if (value <= MaxValue())
            {
                UInt32 mask = MaxValue();
                _memLocationRef.SetData(_memLocationRef.data & ~(mask << lsb));
                _memLocationRef.SetData(_memLocationRef.data | (value & mask) << lsb);
                UpdateBitFlag();
                return true;
            }
            return false;
        }

        public void SetDataSetModified(UInt32 value)
        {
            _memLocationRef.modified = SetData(value);
        }

        public bool GetBool()
        {
            if (GetData() > 0) return true;
            else return false;
        }

        public void UpdateBitFlag()
        {
            bitFlag.SetValue(GetBool());
        }

        public String Description()
        {
            return _desc;
        }

        public MemBitfield(string name, byte bits, byte lsb, String desc = "")
        {
            _memLocationRef = null;
            _desc = desc;
            this.bitFlag = new BitFlag();
            this.name = name;
            this.bits = bits;
            this.lsb = lsb;
        }
    }

}
