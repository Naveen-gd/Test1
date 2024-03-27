using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace MemLib
{
    public class MemLocation
    {
        private Dictionary<string, MemBitfield> _bitfields;
        private UInt32 _data;
        private String _desc; 

        public string name;
        public ushort addr;
        public bool modified;
        public bool readOnly;
        public bool writeOnly;

        public void AddBitfield(MemBitfield bitfield)
        {
            bitfield.SetMemLocation(this);
            _bitfields.Add(bitfield.name, bitfield);
        }

        public MemBitfield GetBitfield(string name)
        {
            return _bitfields[name];
        }

        public List<MemBitfield> GetAllBitfields()
        {
            return _bitfields.Values.ToList();
        }

        public UInt32 data
        {
            get { return _data; }
        }

        public void SetData(UInt32 value)
        {
            _data = value;
            foreach (KeyValuePair<string, MemBitfield> kvp in _bitfields)
            {
                kvp.Value.UpdateBitFlag();
            }
        }

        public void SetDataClearModified(UInt32 value)
        {
            SetData(value);
            modified = false;
        }

        public void SetDataSetModified(UInt32 value)
        {
            SetData(value);
            modified = true;
        }

        public void ClearAllBitfieldBitFlags()
        {
            foreach (KeyValuePair<string, MemBitfield> kvp in _bitfields)
            {
                kvp.Value.bitFlag.Clear();
            }
        }

        public String Description()
        {
            return _desc;
        }

        public MemLocation(string name, ushort addr, bool readOnly = false, bool writeOnly = false, String desc = "")
        {
            _data = 0;
            _bitfields = new Dictionary<string, MemBitfield>();
            _desc = desc;

            this.name = name;
            this.addr = addr;
            this.readOnly = readOnly;
            this.writeOnly = writeOnly;
            modified = false;
        }
    }

}
