using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Linq;
using System.Text.RegularExpressions;

using Can_Comm_Lib;

namespace Device_52295_Lib
{
    internal static class CommE2ECRC
    {
        public static byte calc(byte[] data, int len, byte kennungsfolge)
        {
            byte crc_sum = 0xFF; // INIT
            byte crc_byte;
            int i;
            int b;

            for (i = 0; i < len + 1; i += 1) // +1 for Kennungsfolge
            {
                if (i == len) crc_byte = kennungsfolge;
                else crc_byte = data[i + 1];

                crc_sum ^= crc_byte;
                for (b = 0; b < 8; b += 1)
                {
                    if ((crc_sum & 0x80) == 0x80)
                    {
                        crc_sum = (byte)(((int)crc_sum << 1 ^ 0x2F) & 0xFF);
                    }
                    else crc_sum = (byte)((int)crc_sum << 1);
                }
            }
            return (byte)((~crc_sum) & 0xFF);
        }
    }


}
