using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace Device_52295_Lib
{
    public static class Extensions
    {
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    
        public static uint ParseAsUInt(this string data)
        {
            if ((data.Length >= 3) && (data.Substring(0, 2) == "0x")) 
                return Convert.ToUInt32(data, 16);
            return Convert.ToUInt32(data, 10);
        }

        public static byte ParseAsByte(this string data)
        {
            if ((data.Length >= 3) && (data.Length <= 4) && (data.Substring(0, 2) == "0x")) 
                return Convert.ToByte(data, 16);
            if (data.Length <= 3) 
                return Convert.ToByte(data, 10);
            throw new FormatException();
        }

        public static String ToString(this bool data)
        {
            return String.Format("{0:D1}", data ? 1 : 0);
        }

        public static String ToHexString(this byte data)
        {
            return String.Format("0x{0:X2}", data);
        }

        public static String ToHexString(this uint data, int bits)
        {
            String dataStr;
            if (bits == 1)
                dataStr = String.Format("{0:D}", data);
            else if (bits > 31) dataStr = String.Format("0x{0:X8}", data);
            else if (bits > 12) dataStr = String.Format("0x{0:X4}", data);
            else if (bits > 8) dataStr = String.Format("0x{0:X3}", data);
            else if (bits > 4) dataStr = String.Format("0x{0:X2}", data);
            else dataStr = String.Format("0x{0:X1}", data);
            return dataStr;
        }

        public static uint Pow(this uint bas, uint exp)
        {
            uint result = 1;
            for (uint i = 0; i < exp; i++)
                result *= bas;
            return result;
        }
    }

}
