using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Linq;


namespace Device_52295_Lib
{

    public class Device
    {
        internal const ushort ADDR_BUS_CONFIG = 0x00;
        internal const ushort ADDR_BUS_STATUS = 0x30;
        internal const ushort ADDR_EEPROM = 0xB0;

        public const double LSB_VLED = 36;
        public const double LSB_VDIF = 76;
        public const double LSB_ILED = 10;
        public const double LSB_VSUP = 25;
        public const double LSB_GPIO = 569;

        public BusConfig busConfig;
        public BusStatus busStatus;
        public EEProm eeprom;

        public BitFlag readFail;

        public static int GetTemperatureFrom8bit(byte temp)
        {
            return ((int)temp - 60);
        }

        public bool GotReadFail()
        {
            readFail.SetValue(true);
            return true;
        }

        public Device()
		{
            busConfig = new BusConfig();
            busStatus = new BusStatus();
            eeprom = new EEProm();
            readFail = new BitFlag(false);
        }

    }

}
