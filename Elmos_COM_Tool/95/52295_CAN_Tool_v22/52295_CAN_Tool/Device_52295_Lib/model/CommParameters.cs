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

    public class CommParameters
    {
        private const string LABEL_DEFAULT = "COMM_DEFAULT";
        private const string LABEL_ADAPTER = "COMM_ADAPTER";
        private const string LABEL_ARB_BRP = "COMM_ARB_BRP";
        private const string LABEL_ARB_TSEG_1 = "COMM_ARB_TSEG_1";
        private const string LABEL_ARB_TSEG_2 = "COMM_ARB_TSEG_2";
        private const string LABEL_DATA_BRP = "COMM_DATA_BRP";
        private const string LABEL_DATA_TSEG_1 = "COMM_DATA_TSEG_1";
        private const string LABEL_DATA_TSEG_2 = "COMM_DATA_TSEG_2";
        private const string LABEL_SECURE_M = "COMM_SECURE_M";
        private const string LABEL_SECURE_S = "COMM_SECURE_S"; 
        private const string LABEL_TYPE_M_W3= "COMM_TYPE_M_W3";
        private const string LABEL_TYPE_M_W = "COMM_TYPE_M_W"; 
        private const string LABEL_TYPE_M_R = "COMM_TYPE_M_R";
        private const string LABEL_TYPE_S_R = "COMM_TYPE_S_R";

        public const CanCommBitrate DEFAULT_BITRATE = CanCommBitrate.BITRATE_500_SP80_500_SP80;

        public static Dictionary<CanCommBitrate, string> BITRATE_LABELS = new Dictionary<CanCommBitrate,string>
        {
            {CanCommBitrate.BITRATE_500_SP80_500_SP80, "A: 500k 80% + D: 500k 80%"},
            {CanCommBitrate.BITRATE_500_SP80_1000_SP70, "A: 500k 80% + D:   1M 70%"},
            {CanCommBitrate.BITRATE_500_SP60_2000_SP60, "A: 500k 60% + D:   2M 60%"},
            {CanCommBitrate.BITRATE_500_SP70_2000_SP70, "A: 500k 70% + D:   2M 70%"},
            {CanCommBitrate.BITRATE_500_SP80_2000_SP60, "A: 500k 80% + D:   2M 60%"},
            {CanCommBitrate.BITRATE_500_SP80_2000_SP70, "A: 500k 80% + D:   2M 70%"},
            {CanCommBitrate.BITRATE_500_SP80_4000_SP70, "A: 500k 80% + D:   4M 70%"},
            {CanCommBitrate.BITRATE_1000_SP70_2000_SP70, "A:   1M 70% + D:   2M 70%"}
        };

        public CanCommAdapter adapter = CanCommAdapter.VECTOR;

        public bool defaultConfig = true;

        public CanCommBitrateConfig bitrateConfig;

        public byte frameType_M_W = 0x06;
        public byte frameType_M_R = 0x04;
        public byte frameType_M_W3 = 0x02;
        public byte frameType_S_R = 0x00;

        public byte secureByte_M = 0x42;
        public byte secureByte_S = 0x23;

        public CommParameters()
        {
            // same default for master and gui
            setDefaultConfig();
        }

        public void patchAdapterSpecificSettings(){
            // patch some adapter specific stuff
            CanCommBitrateConfig defBrConf = CanComm.GetBitrateConfigForBitrate(adapter, CommParameters.DEFAULT_BITRATE);
            bitrateConfig.f_clock_mhz = defBrConf.f_clock_mhz;
            bitrateConfig.allowBrp = defBrConf.allowBrp;
            if (!bitrateConfig.allowBrp)
            {
                bitrateConfig.arbBrp = defBrConf.arbBrp;
                bitrateConfig.dataBrp = defBrConf.dataBrp;
            }
        }

        private string getShortBitrateLabel(CanCommBitrate bitrate)
        {
            string ret = BITRATE_LABELS[bitrate];
            ret = Regex.Replace(ret, ":\\s+", ":");
            ret = Regex.Replace(ret, "\\s+\\+\\s+", "+");
            ret = Regex.Replace(ret, "\\s+", "_");
            return ret;
        }

        public void apply(CommParameters commParameters)
        {
            defaultConfig = commParameters.defaultConfig;
            //TOOD: bitrate = commParameters.bitrate;
            bitrateConfig = commParameters.bitrateConfig;
            adapter = commParameters.adapter;

            secureByte_M = commParameters.secureByte_M;
            secureByte_S = commParameters.secureByte_S;

            frameType_M_W3 = commParameters.frameType_M_W3;
            frameType_M_W = commParameters.frameType_M_W;
            frameType_M_R = commParameters.frameType_M_R;
            frameType_S_R = commParameters.frameType_S_R;
        }

        public void setDefaultConfig()
        {
            // set everything except CanCommAdapter
            defaultConfig = true;

            // depends on adapter
            bitrateConfig = CanComm.GetBitrateConfigForBitrate(adapter, DEFAULT_BITRATE);

            frameType_M_W = 0x06;
            frameType_M_R = 0x04;
            frameType_M_W3 = 0x02;
            frameType_S_R = 0x00;

            secureByte_M = 0x00;
            secureByte_S = 0x00;
        }

        public void getFromSettingsFile(SettingsFile settingsFile)
        {
            if (settingsFile.parameterExists(LABEL_DEFAULT)) defaultConfig = settingsFile.getBoolParameter(LABEL_DEFAULT);
            if (settingsFile.parameterExists(LABEL_ADAPTER) && (settingsFile.getStringParameter(LABEL_ADAPTER) == "Peak")) adapter = CanCommAdapter.PEAK;
            if (settingsFile.parameterExists(LABEL_ADAPTER) && (settingsFile.getStringParameter(LABEL_ADAPTER) == "Vector")) adapter = CanCommAdapter.VECTOR;
            
            if (settingsFile.parameterExists(LABEL_ARB_BRP)) bitrateConfig.arbBrp = settingsFile.getByteParameter(LABEL_ARB_BRP);
            if (settingsFile.parameterExists(LABEL_ARB_TSEG_1)) bitrateConfig.arbTseg1 = settingsFile.getByteParameter(LABEL_ARB_TSEG_1);
            if (settingsFile.parameterExists(LABEL_ARB_TSEG_2)) bitrateConfig.arbTseg2 = settingsFile.getByteParameter(LABEL_ARB_TSEG_2);

            if (settingsFile.parameterExists(LABEL_DATA_BRP)) bitrateConfig.dataBrp = settingsFile.getByteParameter(LABEL_DATA_BRP);
            if (settingsFile.parameterExists(LABEL_DATA_TSEG_1)) bitrateConfig.dataTseg1 = settingsFile.getByteParameter(LABEL_DATA_TSEG_1);
            if (settingsFile.parameterExists(LABEL_DATA_TSEG_2)) bitrateConfig.dataTseg2 = settingsFile.getByteParameter(LABEL_DATA_TSEG_2);

            if (settingsFile.parameterExists(LABEL_SECURE_M)) secureByte_M = settingsFile.getByteParameter(LABEL_SECURE_M);
            if (settingsFile.parameterExists(LABEL_SECURE_S)) secureByte_S = settingsFile.getByteParameter(LABEL_SECURE_S);

            if (settingsFile.parameterExists(LABEL_TYPE_M_W3)) frameType_M_W3 = settingsFile.getByteParameter(LABEL_TYPE_M_W3);
            if (settingsFile.parameterExists(LABEL_TYPE_M_W)) frameType_M_W = settingsFile.getByteParameter(LABEL_TYPE_M_W);
            if (settingsFile.parameterExists(LABEL_SECURE_M)) frameType_M_R = settingsFile.getByteParameter(LABEL_TYPE_M_R);
            if (settingsFile.parameterExists(LABEL_SECURE_M)) frameType_S_R = settingsFile.getByteParameter(LABEL_TYPE_S_R);

            patchAdapterSpecificSettings();

            if (defaultConfig) setDefaultConfig();
        }

        public void setToSettingsFile(SettingsFile settingsFile)
        {
            settingsFile.setParameter(LABEL_DEFAULT, defaultConfig);

            switch (adapter)
            {
                case CanCommAdapter.PEAK: settingsFile.setParameter(LABEL_ADAPTER, "Peak"); break;
                case CanCommAdapter.VECTOR: settingsFile.setParameter(LABEL_ADAPTER, "Vector"); break;
            }

            settingsFile.setParameter(LABEL_ARB_BRP, bitrateConfig.arbBrp);
            settingsFile.setParameter(LABEL_ARB_TSEG_1, bitrateConfig.arbTseg1);
            settingsFile.setParameter(LABEL_ARB_TSEG_2, bitrateConfig.arbTseg2);

            settingsFile.setParameter(LABEL_DATA_BRP, bitrateConfig.dataBrp);
            settingsFile.setParameter(LABEL_DATA_TSEG_1, bitrateConfig.dataTseg1);
            settingsFile.setParameter(LABEL_DATA_TSEG_2, bitrateConfig.dataTseg2);
            
            settingsFile.setParameter(LABEL_SECURE_M, secureByte_M);
            settingsFile.setParameter(LABEL_SECURE_S, secureByte_S);

            settingsFile.setParameter(LABEL_TYPE_M_W3, frameType_M_W3);
            settingsFile.setParameter(LABEL_TYPE_M_W, frameType_M_W);
            settingsFile.setParameter(LABEL_TYPE_M_R, frameType_M_R);
            settingsFile.setParameter(LABEL_TYPE_S_R, frameType_S_R);
        }
    }

}
