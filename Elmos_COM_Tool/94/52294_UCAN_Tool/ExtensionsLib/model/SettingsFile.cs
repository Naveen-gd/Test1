using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Windows.Forms;

namespace Extensions
{
    public class SettingsFile
    {
        public Dictionary<string, string> dict;

        private string _fileName;

        public SettingsFile(string fileName = null)
        {
            _fileName = fileName;

            dict = new Dictionary<string,string>();
        }

        public bool parameterExists(string name)
        {
            return dict.ContainsKey(name);
        }

        public string getStringParameter(string name)
        {
            if (parameterExists(name)) return dict[name];
            return "";
        }

        public byte getByteParameter(string name)
        {
            try
            {
                if (parameterExists(name))
                    return Convert.ToByte(dict[name], 16);
            }
            catch
            {
            }
            return 0;
        }

        public bool getBoolParameter(string name)
        {
            if (parameterExists(name)) return ((Convert.ToByte(dict[name]) == 0) ? false : true);
            return false;
        }

        public void setParameter(string name, string value)
        {
            dict[name] = value;
        }

        public void setParameter(string name, byte value)
        {
            dict[name] = value.ToHexString();
        }

        public void setParameter(string name, bool value)
        {
            dict[name] = value.ToString();
        }

        public void saveToFile(string path = null)
        {
            if (path == null)
            {
                path = _fileName;
            }

            try
            {
                System.IO.StreamWriter writer = new StreamWriter(path);

                foreach (KeyValuePair<string, string> entry in dict)
                {
                    writer.WriteLine(entry.Key + ";" + entry.Value);
                }
                writer.Close();
            }
            catch
            {
            }
        }

        public void loadFromFile(string path = null)
        {
            if (path == null)
            {
                path = _fileName;
            }

            try
            {
                System.IO.StreamReader reader = new StreamReader(path);
                while (!reader.EndOfStream)
                {
                    String line = reader.ReadLine();
                    String[] values = line.Split(';');

                    if (values.Length == 2)
                    {
                        String key = values[0];
                        String value = values[1];
                        dict[key] = value;
                    }
                }
                reader.Close();
            }
            catch
            {
            }
        }
    
    }
}
