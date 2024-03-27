using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml.Linq;
using System.Xml;
using System.IO;

namespace Device_52294_Lib
{
    public static class DeviceType
    {
        private enum Type
        {
            E52294A = 0,
            M52294A = 1
        };

        private static Type type = Type.E52294A;

        public static void SetM52294A()
        {
            type = Type.M52294A;
        }

        public static void SetE52294A()
        {
            type = Type.E52294A;
        }

        public static bool IsM52294A
        {
            get
            {
                return ((type == Type.M52294A) ? true : false);
            }
        }

        public static bool IsE52294A
        {
            get
            {
                return ((type == Type.E52294A) ? true : false);
            }
        }
    }
}
