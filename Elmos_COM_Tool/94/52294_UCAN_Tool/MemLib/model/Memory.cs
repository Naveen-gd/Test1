using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace MemLib
{
    public class Memory : SortedList<UInt32, MemLocation>
    {
        private byte _data_bits;
        private string _area;
        private bool _readOnly;
        private bool _changeZeroOnly;

        public byte data_bits
        {
            get { return _data_bits; }
        }

        public byte data_bytes
        {
            get {
                byte ret = (byte) (_data_bits / 8);
                if ((_data_bits % 8) > 0)
                    ret += 1;
                return ret; 
            }
        }

        public string area
        {
            get { return _area; }
        }

        public bool readOnly
        {
            get { return _readOnly; }
        }

        public bool changeZeroOnly
        {
            get { return _changeZeroOnly; }
        }

        public Memory(byte data_bits, string area, bool readOnly = false, bool changeZeroOnly = false)
        {
            _data_bits = data_bits;
            _area = area;
            _readOnly = readOnly;
            _changeZeroOnly = changeZeroOnly;
        }

        public void Add(MemLocation memLoc)
        {
            this.Add(memLoc.addr, memLoc);
        }

        public void ClearAllModified()
        {
            for (int r = 0; r < this.Count; r += 1)
            {
                this.ElementAt(r).Value.modified = false;
            }
        }

        public void SetAllModified()
        {
            for (int r = 0; r < this.Count; r += 1)
            {
                this.ElementAt(r).Value.modified = true;
            }
        }

        public void ClearAllBitfieldBitFlags()
        {
            for (int r = 0; r < this.Count; r += 1)
            {
                this.ElementAt(r).Value.ClearAllBitfieldBitFlags();
            }
        }

        public void Verify()
        {
            for (int r = 0; r < this.Count; r += 1)
            {
                Debug.Assert(this.ElementAt(r).Value.addr == (r * this.data_bytes));
            }
        }

        public void saveToFile(String path, bool append = false)
        {
            System.IO.StreamWriter writer = new StreamWriter(path, append);

            String dataStr;
            uint data;

            for (int r = 0; r < Count; r += 1)
            {
                MemLocation memLoc = this.ElementAt(r).Value;
                if (!memLoc.readOnly)
                {
                    data = memLoc.data;
                    dataStr = data.ToHexString(8 * data_bytes);
                    writer.WriteLine(this.ElementAt(r).Value.name + ";" + dataStr);
                }
            }
            writer.Close();
        }

        public void loadFromFile(String path)
        {
            System.IO.StreamReader reader = new StreamReader(path);
            while (!reader.EndOfStream)
            {
                String line = reader.ReadLine();

                String[] values = line.Split(';');  // new format
                if (values.Length != 2){
                    values = line.Split('=');       // old format
                    
                    if (values.Length == 2){        // change name
                        values[0] = values[0].Replace("OTP_PAGE0_", "");
                        values[0] = values[0].Replace("OTP_PAGE1_", "");
                        values[0] = values[0].Replace("OTP_PAGE2_", "");
                        values[0] = values[0].Replace("OTP_PAGE3_", "");
                    }
                }

                if (values.Length == 2)
                {
                    String name = values[0];
                    MemLocation memLoc = this.FirstOrDefault(x => x.Value.name == name).Value;
                    if (memLoc != null)
                    {
                        uint newData = values[1].ParseAsUInt();
                        if (memLoc.data != newData)
                        {
                            if (!changeZeroOnly || (memLoc.data == 0) || memLoc.modified)
                            {
                                memLoc.SetDataSetModified(newData);
                            }
                        }
                    }
                }
            }
            reader.Close();
        }

        public virtual String Description(MemLocation location)
        {
            return location.Description();
        }

        public virtual String Description(MemBitfield bitfield)
        {
            return bitfield.Description();
        }
    }

}
