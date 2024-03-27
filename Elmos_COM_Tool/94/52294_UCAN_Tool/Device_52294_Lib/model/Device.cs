using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Linq;

namespace Device_52294_Lib
{

    public class Device
    {
        public enum PageSel
        {
            STANDALONE_0 = 0,       // 0x000 ... 0x07E
            STANDALONE_1 = 1,       // 0x080 ... 0x0FE
            STANDALONE_2 = 2,       // 0x100 ... 0x17E
            BUS_DEFAULT = 3,
            STANDALONE_EXT = 4
        };

        internal const byte MAPPING_PAGE_WORDS = 0x40;
        internal const byte MAPPING_PAGE_BYTES = MAPPING_PAGE_WORDS * 2;

        internal const ushort ADDR_BUS_CONFIG_CMD = 0x000;
        internal const ushort ADDR_BUS_CONFIG_IMM = 0x080;
        internal const ushort ADDR_BUS_STATUS = 0x100;
        internal const ushort ADDR_MAPPING = 0x180;

        public const double LSB_VLED = 36;
        public const double LSB_VDIF = 36;
        public const double LSB_ILED = 10;
        public const double LSB_VSUP = 25;
        public const double LSB_VDD5 = 142;

        public BusConfig busConfig;
        public BusStatus busStatus;

        public DeviceParameters parameters;

        public Device()
		{
            busConfig = new BusConfig("BUS_CONFIG", false);
            busStatus = new BusStatus();

            parameters = new DeviceParameters();
        }
    }

    public class DeviceParameters
    {
        public const string FILE_FILTER_SAVE = "Parameter files (*.txt)|*.txt";
        public const string FILE_FILTER_OPEN = FILE_FILTER_SAVE + "|Wizard files (*.cfg)|*.cfg";

        public Standalone standalone;
        public BusConfig busDefConfig;
        public StandaloneExt standaloneExt;

        public DeviceParameters()
        {
            standalone = new Standalone();
            busDefConfig = new BusConfig("BUS_DEF_CONFIG", true);
            standaloneExt = new StandaloneExt();
        }

        public void ClearAllModified()
        {
            standalone.ClearAllModified();
            busDefConfig.ClearAllModified();
            standaloneExt.ClearAllModified();
        }

        public void saveToFile(String path)
        {
            standalone.saveToFile(path, false);
            busDefConfig.saveToFile(path, true);
            standaloneExt.saveToFile(path, true);
        }

        public void loadFromFile(String path)
        {
            standalone.loadFromFile(path);
            busDefConfig.loadFromFile(path);
            standaloneExt.loadFromFile(path);
        }
    }

}
