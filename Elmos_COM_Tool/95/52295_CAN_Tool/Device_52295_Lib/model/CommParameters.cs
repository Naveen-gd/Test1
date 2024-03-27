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
        private const string LABEL_BITRATE = "COMM_BITRATE";
        private const string LABEL_SECURE_M = "COMM_SECURE_M";
        private const string LABEL_SECURE_S = "COMM_SECURE_S"; 
        private const string LABEL_TYPE_M_W3= "COMM_TYPE_M_W3";
        private const string LABEL_TYPE_M_W = "COMM_TYPE_M_W"; 
        private const string LABEL_TYPE_M_R = "COMM_TYPE_M_R";
        private const string LABEL_TYPE_S_R = "COMM_TYPE_S_R";

        public static Dictionary<CanCommBitrate, string> bitrateLabels;

        public CanCommBitrate bitrate = CanCommBitrate.BITRATE_500_SP80_500_SP80;
        public CanCommAdapter adapter = CanCommAdapter.NONE;

        public bool defaultConfig = true;

        public byte frameType_M_W = 0x06;
        public byte frameType_M_R = 0x04;
        public byte frameType_M_W3 = 0x02;
        public byte frameType_S_R = 0x00;

        public byte secureByte_M = 0x42;
        public byte secureByte_S = 0x23;

        public CommParameters()
        {
            // bitrate labels
            bitrateLabels = new Dictionary<CanCommBitrate, string>();
            bitrateLabels[CanCommBitrate.BITRATE_500_SP80_500_SP80] = "A: 500k 80% + D: 500k 80%";
            bitrateLabels[CanCommBitrate.BITRATE_500_SP80_1000_SP70] = "A: 500k 80% + D:   1M 70%";
            bitrateLabels[CanCommBitrate.BITRATE_500_SP60_2000_SP60] = "A: 500k 60% + D:   2M 60%";
            bitrateLabels[CanCommBitrate.BITRATE_500_SP70_2000_SP70] = "A: 500k 70% + D:   2M 70%";
            bitrateLabels[CanCommBitrate.BITRATE_500_SP80_2000_SP60] = "A: 500k 80% + D:   2M 60%";
            bitrateLabels[CanCommBitrate.BITRATE_500_SP80_2000_SP70] = "A: 500k 80% + D:   2M 70%";
            bitrateLabels[CanCommBitrate.BITRATE_500_SP80_4000_SP70] = "A: 500k 80% + D:   4M 70%";
            bitrateLabels[CanCommBitrate.BITRATE_1000_SP70_2000_SP70] = "A:   1M 70% + D:   2M 70%";
            
            // same default for master and gui
            setDefaultConfig();

            // Vector as Default ONCE
            adapter = CanCommAdapter.VECTOR;
        }

        private string getShortBitrateLabel(CanCommBitrate bitrate)
        {
            string ret = bitrateLabels[bitrate];
            ret = Regex.Replace(ret, ":\\s+", ":");
            ret = Regex.Replace(ret, "\\s+\\+\\s+", "+");
            ret = Regex.Replace(ret, "\\s+", "_");
            return ret;
        }

        public void apply(CommParameters commParameters)
        {
            defaultConfig = commParameters.defaultConfig;
            bitrate = commParameters.bitrate;
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

            bitrate = CanCommBitrate.BITRATE_500_SP80_500_SP80;

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

            if (settingsFile.parameterExists(LABEL_BITRATE)){
                foreach (KeyValuePair<CanCommBitrate,string> kvp in bitrateLabels){
                    if (getShortBitrateLabel(kvp.Key) == settingsFile.getStringParameter(LABEL_BITRATE)){
                        bitrate = kvp.Key;
                    }
                }
            }
            
            if (settingsFile.parameterExists(LABEL_SECURE_M)) secureByte_M = settingsFile.getByteParameter(LABEL_SECURE_M);
            if (settingsFile.parameterExists(LABEL_SECURE_S)) secureByte_S = settingsFile.getByteParameter(LABEL_SECURE_S);

            if (settingsFile.parameterExists(LABEL_TYPE_M_W3)) frameType_M_W3 = settingsFile.getByteParameter(LABEL_TYPE_M_W3);
            if (settingsFile.parameterExists(LABEL_TYPE_M_W)) frameType_M_W = settingsFile.getByteParameter(LABEL_TYPE_M_W);
            if (settingsFile.parameterExists(LABEL_SECURE_M)) frameType_M_R = settingsFile.getByteParameter(LABEL_TYPE_M_R);
            if (settingsFile.parameterExists(LABEL_SECURE_M)) frameType_S_R = settingsFile.getByteParameter(LABEL_TYPE_S_R);

            if (defaultConfig) setDefaultConfig();
        }

        public void setSettingsFile(SettingsFile settingsFile)
        {
            settingsFile.setParameter(LABEL_DEFAULT, defaultConfig);

            switch (adapter)
            {
                case CanCommAdapter.NONE: settingsFile.setParameter(LABEL_ADAPTER, ""); break;
                case CanCommAdapter.PEAK: settingsFile.setParameter(LABEL_ADAPTER, "Peak"); break;
                case CanCommAdapter.VECTOR: settingsFile.setParameter(LABEL_ADAPTER, "Vector"); break;
            }

            settingsFile.setParameter(LABEL_BITRATE, getShortBitrateLabel(bitrate));
            
            settingsFile.setParameter(LABEL_SECURE_M, secureByte_M);
            settingsFile.setParameter(LABEL_SECURE_S, secureByte_S);

            settingsFile.setParameter(LABEL_TYPE_M_W3, frameType_M_W3);
            settingsFile.setParameter(LABEL_TYPE_M_W, frameType_M_W);
            settingsFile.setParameter(LABEL_TYPE_M_R, frameType_M_R);
            settingsFile.setParameter(LABEL_TYPE_S_R, frameType_S_R);
        }
    }

}
