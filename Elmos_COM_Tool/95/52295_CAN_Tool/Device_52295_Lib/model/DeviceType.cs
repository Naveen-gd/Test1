using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml.Linq;
using System.Xml;
using System.IO;

namespace Device_52295_Lib
{
    public static class DeviceType
    {
        private enum Type
        {
            E52295A = 0,
            M52295A = 1
        };

        private static Type type = Type.E52295A;

        public static void SetM52295A()
        {
            type = Type.M52295A;
        }

        public static void SetE52295A()
        {
            type = Type.E52295A;
        }

        public static bool IsM52295A
        {
            get
            {
                return ((type == Type.M52295A) ? true : false);
            }
        }

        public static bool IsE52295A
        {
            get
            {
                return ((type == Type.E52295A) ? true : false);
            }
        }
    }
}
