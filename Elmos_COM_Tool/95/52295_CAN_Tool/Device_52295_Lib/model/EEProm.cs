using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace Device_52295_Lib
{
    public class EEProm : Memory
    {
        private String LABEL_VALID_COOKIE = "VALID_COOKIE";
        private String LABEL_VREG_REGULATION_FACTOR = "VREG_REGULATION_FACTOR";
        private String LABEL_VREG_TARGET_VOLTAGE = "VREG_TARGET_VOLTAGE";
        private String LABEL_VREG_TAKEOVER_THRESHOLD = "VREG_TAKEOVER_THRESHOLD";
        private String LABEL_GPIO_FUNC = "GPIO_FUNC";
        private String LABEL_VREG_MODE = "VREG_MODE";
        private String LABEL_W3_STARTUP_FACTOR = "W3_STARTUP_FACTOR";
        private String LABEL_W3_TIMEOUT_FACTOR = "W3_TIMEOUT_FACTOR";
        private String LABEL_W3_ERROR_THD = "W3_ERROR_THD";
        private String LABEL_W3_TIMEOUT = "W3_TIMEOUT";
        private String LABEL_W3_FIRST_FACTOR = "W3_FIRST_FACTOR";
        private String LABEL_W3_ERROR_RESET = "W3_ERROR_RESET";
        private String LABEL_W3_VALID_THD                        = "W3_VALID_THD";
        private String LABEL_W3_T_STARTUP                        = "W3_T_STARTUP";
        private String LABEL_CAN_TX_BRS                          = "CAN_TX_BRS";
        private String LABEL_A_BRP                               = "A_BRP";
        private String LABEL_A_SEG1                              = "A_SEG1";
        private String LABEL_A_SEG2_SJW                          = "A_SEG2_SJW";
        private String LABEL_FD_BRP                              = "FD_BRP";
        private String LABEL_FD_SEG1                             = "FD_SEG1";
        private String LABEL_FD_SEG2_SJW                         = "FD_SEG2_SJW";
        private String LABEL_TDC_EN                              = "TDC_EN";
        private String LABEL_DIAG_TIMEOUT                        = "DIAG_TIMEOUT";
        private String LABEL_DIAG0_IE                            = "DIAG0_IE";
        private String LABEL_DIAG1_IE                            = "DIAG1_IE";
        private String LABEL_DIAG2_IE                            = "DIAG2_IE";
        private String LABEL_DIAG0_SEL                           = "DIAG0_SEL";
        private String LABEL_DIAG1_SEL                           = "DIAG1_SEL";
        private String LABEL_DIAG2_SEL                           = "DIAG2_SEL";
        private String LABEL_DIAG0_SLM                           = "DIAG0_SLM";
        private String LABEL_DIAG1_SLM                           = "DIAG1_SLM";
        private String LABEL_DIAG2_SLM                           = "DIAG2_SLM";
        private String LABEL_DIAG0_BUS_FAILURE                   = "DIAG0_BUS_FAILURE";
        private String LABEL_DIAG1_BUS_FAILURE                   = "DIAG1_BUS_FAILURE";
        private String LABEL_DIAG2_BUS_FAILURE                   = "DIAG2_BUS_FAILURE";
        private String LABEL_DIAG_CNT_THD                        = "DIAG_CNT_THD";
        private String LABEL_VS_TOO_HIGH                         = "VS_TOO_HIGH";      
        private String LABEL_VS_TOO_LOW                          = "VS_TOO_LOW";          
        private String LABEL_VT_CRITICAL                         = "VT_CRITICAL";         
        private String LABEL_VT_TOO_HIGH                         = "VT_TOO_HIGH";         
        private String LABEL_DIAG_RETRY_PAUSE                    = "DIAG_RETRY_PAUSE";
        private String LABEL_DIAG_RETRY_BURST = "DIAG_RETRY_BURST";
        private String LABEL_DIAG_VS_DIAG_DISABLE_THRESHOLD = "VS_DIAG_DISABLE_THRESHOLD";
        private String LABEL_DIAG_COMB_RETRY_ALL = "COMB_RETRY_ALL";
        private String LABEL_PROTECT_CURRENT = "PROTECT_CURRENT";
        private String LABEL_CRYSTAL_SEL = "CRYSTAL_SEL";
        private String LABEL_REFCLK_MASTER = "REFCLK_MASTER";
        private String LABEL_RECOVERY_RETRIES    = "RECOVERY_RETRIES";
        private String LABEL_RECOVERY_PAUSE      = "RECOVERY_PAUSE";
        private String LABEL_DERATE_SRC          = "DERATE_SRC";
        private String LABEL_VT_DERATE           = "VT_DERATE";
        private String LABEL_VS_DERATE           = "VS_DERATE";
        private String LABEL_START               = "START";
        private String LABEL_STOP                = "STOP";
        private String LABEL_GAIN                = "GAIN";
        private String LABEL_PERIOD              = "PERIOD";
        private String LABEL_PRESCALER           = "PRESCALER";
        private String LABEL_PWMIN_MID_ALT       = "PWMIN_MID_ALT";
        private String LABEL_PWMIN_ACC           = "PWMIN_ACC";
        private String LABEL_PWMIN_FREQ          = "PWMIN_FREQ";
        private String LABEL_PWMIN_INVALID_ALT   = "PWMIN_INVALID_ALT";
        private String LABEL_ANALOG              = "ANALOG";
        private String LABEL_COMBINE_PRI         = "COMBINE_PRI";
        private String LABEL_COMBINE_SEC         = "COMBINE_SEC";
        private String LABEL_BIN_LEVEL_0         = "BIN_LEVEL_0";
        private String LABEL_BIN_LEVEL_1         = "BIN_LEVEL_1";
        private String LABEL_BIN_LEVEL_2         = "BIN_LEVEL_2";
        private String LABEL_BIN_LEVEL_3         = "BIN_LEVEL_3";
        private String LABEL_BIN_GAIN_0          = "BIN_GAIN_0";
        private String LABEL_BIN_GAIN_1          = "BIN_GAIN_1";
        private String LABEL_BIN_GAIN_2          = "BIN_GAIN_2";
        private String LABEL_BIN_GAIN_3          = "BIN_GAIN_3";
        private String LABEL_BIN_GAIN_4          = "BIN_GAIN_4";
        private String LABEL_LED0                = "LED0";
        private String LABEL_LED1                = "LED1";
        private String LABEL_LED2                = "LED2";
        private String LABEL_LED3                = "LED3";
        private String LABEL_LED4                = "LED4";
        private String LABEL_LED5                = "LED5";
        private String LABEL_LED6                = "LED6";
        private String LABEL_LED7                = "LED7";
        private String LABEL_LED8                = "LED8";
        private String LABEL_LED9                = "LED9";
        private String LABEL_LED10               = "LED10";
        private String LABEL_LED11               = "LED11";
        private String LABEL_LED12               = "LED12";
        private String LABEL_LED13               = "LED13";
        private String LABEL_LED14               = "LED14";
        private String LABEL_LED15               = "LED15";
        private String LABEL_LED_ENABLE          = "LED_ENABLE";
        private String LABEL_SCALE_GAIN1         = "SCALE_GAIN1";
        private String LABEL_SCALE_GAIN2         = "SCALE_GAIN2";
        private String LABEL_SCALE_GAIN3         = "SCALE_GAIN3";
        private String LABEL_PWM_ALT_SCALE       = "PWM_ALT_SCALE";
        private String LABEL_BIN_FACTOR          = "BIN_FACTOR";
        private String LABEL_BIN_CONFIG          = "BIN_CONFIG";
        private String LABEL_SUPPLY_SRC          = "SUPPLY_SRC";
        private String LABEL_DIAG_GROUP          = "DIAG_GROUP";
        private String LABEL_LISTEN_TO_PWMIN     = "LISTEN_TO_PWMIN";
        private String LABEL_VREG_GPIO           = "VREG_GPIO";
        private String LABEL_VREG_ENABLE         = "VREG_ENABLE";
        private String LABEL_OPEN_MIN_THD        = "OPEN_MIN_THD";
        private String LABEL_SHORT_THD           = "SHORT_THD";
        private String LABEL_OPEN_THD            = "OPEN_THD";

        internal const ushort SIZE_CUSTOMER_AREA = 0xB0;
        internal const ushort SIZE_PARAMETER_AREA = 0xD0;

        internal const ushort ADDR_CUSTOMER_AREA = 0x00;
        internal const ushort ADDR_PARAMETER_AREA = SIZE_CUSTOMER_AREA;

        internal const ushort ADDR_PARAMETER_PWM_CONFIG = ADDR_PARAMETER_AREA + 0x88;
        internal const ushort ADDR_PARAMETER_CAN_CONFIG_1 = ADDR_PARAMETER_AREA + 0xAC;
        internal const ushort ADDR_PARAMETER_CAN_CONFIG_2 = ADDR_PARAMETER_AREA + 0xB0;
        internal const ushort ADDR_PARAMETER_CAN_CONFIG_3 = ADDR_PARAMETER_AREA + 0xB4;
        internal const ushort ADDR_PARAMETER_EEPROM_KEYS = ADDR_PARAMETER_AREA + 0xCC;

        internal byte eeprom_key
        {
            get { return (byte)this[ADDR_PARAMETER_EEPROM_KEYS].GetBitfield("EEPROM_KEY").GetData(); }
        }

        internal ushort pwm_period
        {
            get { 
                ushort temp = (ushort)this[ADDR_PARAMETER_PWM_CONFIG].GetBitfield("PERIOD").GetData();
                temp <<= 2;
                temp |= 3;
                return temp;
            }
        }

        internal ushort canTimeout
        {
            get
            {
                uint temp = this[ADDR_PARAMETER_CAN_CONFIG_2].GetBitfield("W3_TIMEOUT").GetData();

                if (DeviceType.IsE52295A)
                {
                    temp *= (this[ADDR_PARAMETER_CAN_CONFIG_3].GetBitfield("W3_TIMEOUT_FACTOR").GetData() + 1);
                }

                return (ushort)(temp);
            }
        }

        internal ushort canStartupTimeout
        {
            get
            {
                uint temp = this[ADDR_PARAMETER_CAN_CONFIG_1].GetBitfield("W3_T_STARTUP").GetData();

                if (DeviceType.IsE52295A) { 
                    temp *= (this[ADDR_PARAMETER_CAN_CONFIG_3].GetBitfield("W3_STARTUP_FACTOR").GetData() + 1);
                }

                return (ushort)(temp);
            }
        }


        private String _GroupBinFactorDesc(byte groupNo, MemBitfield bitfield)
        {
            return String.Format("Group {0:D} specific binning gain factor = {1:F} %", groupNo, (Convert.ToDouble(bitfield.GetData()) + 1.0) / 256.0 * 100.0);
        }

        private String _GroupPwmAltScaleDesc(byte groupNo, MemBitfield bitfield)
        {
            return String.Format("Group {0:D} specific PWM scaling factor for alternative mode = {1:F} %", groupNo, (Convert.ToDouble(bitfield.GetData()) + 1.0) / 256.0 * 100.0);
        }

        private String _GroupScaleGainDesc(byte groupNo, byte scaleNo, MemBitfield bitfield)
        {
            return String.Format("Group {0:D} Specific Current Scaling Factor {1:D} = {2:F} %", groupNo, scaleNo, (Convert.ToDouble(bitfield.GetData()) + 1.0) / 256.0 * 100.0);
        }

        private String _GroupBinConfigDesc(byte groupNo, MemBitfield bitfield)
        {
            switch (bitfield.GetData())
            {
                case 1: return String.Format("Group {0:D} binning enabled with GPIO 0", groupNo);
                case 3: return String.Format("Group {0:D} binning enabled with GPIO 1", groupNo);
            }
            return String.Format("Group {0:D} binning disabled", groupNo);
        }

        private String _GroupVregConfigDesc(byte groupNo, MemBitfield bitfield)
        {
            MemLocation memLoc = bitfield.GetMemLocation();
            MemBitfield memBfEnable = memLoc.GetBitfield("VREG_ENABLE");
            MemBitfield memBfGpio = memLoc.GetBitfield("VREG_GPIO");

            if (memBfEnable.GetData() == 1)
                return String.Format("Group {0:D} GPIO Voltage Regulation enabled with GPIO {1:D}", groupNo, memBfGpio.GetData());
            else
                return String.Format("Group {0:D} GPIO Voltage Regulation disabled!", groupNo);
        }

        private String _GroupSupplySrcDesc(byte groupNo, MemBitfield bitfield)
        {
            switch (bitfield.GetData())
            {
                case 0: return String.Format("Group {0:D} Supply = VS", groupNo);
                case 1: return String.Format("Group {0:D} Supply = SENSE0", groupNo);
                case 2: return String.Format("Group {0:D} Supply = SENSE1", groupNo);
            }
            return "reserved, do not use!";
        }

        private String _GroupDiagGroupDesc(byte groupNo, MemBitfield bitfield)
        {
            switch (bitfield.GetData())
            {
                case 0: return String.Format("Assign Group {0:D} to DIAG pin 0.", groupNo);
                case 1: return String.Format("Assign Group {0:D} to DIAG pin 1.", groupNo);
                case 2: return String.Format("Assign Group {0:D} to DIAG pin 2.", groupNo);
            }
            return String.Format("Group {0:D} diagnosis off!", groupNo);
        }

        private String _GroupOpenMinThdDesc(byte groupNo, MemBitfield bitfield)
        {
            if (bitfield.GetData() == 0)
                return String.Format("Group {0:D} minimum supply voltage detection disabled!", groupNo);
            else
                return String.Format("Group {0:D} minimum supply voltage for open detection = {1:F} V", groupNo, Convert.ToDouble(bitfield.GetData()) * 4.0 / Device.LSB_VSUP);
        }

        private String _GroupShortThdDesc(byte groupNo, MemBitfield bitfield)
        {
            if (bitfield.GetData() == 0)
                return String.Format("Group {0:D} SHORT detection disabled!", groupNo);
            else
                return String.Format("Group {0:D} SHORT detection threshold level = {1:F} V", groupNo, Convert.ToDouble(bitfield.GetData()) * 4.0 / Device.LSB_VDIF);
        }

        private String _GroupOpenThdDesc(byte groupNo, MemBitfield bitfield)
        {
            if (bitfield.GetData() == 0)
                return String.Format("Group {0:D} OPEN detection disabled!", groupNo);
            else
                return String.Format("Group {0:D} OPEN detection threshold level = {1:F} V", groupNo, Convert.ToDouble(bitfield.GetData()) * 4.0 / Device.LSB_VLED);
        }

        private String _GroupListenToPwmInDesc(byte groupNo, MemBitfield bitfield)
        {
            if (bitfield.GetData() == 1)
                return String.Format("Group {0:D} listens to PWMIN interface in SAFE state", groupNo);
            else
                return String.Format("Group {0:D} ignores PWMIN interface in SAFE state", groupNo);
        }

        private String _LedGroupSelDesc(byte ledNo, MemBitfield bitfield)
        {
            return String.Format("LED {0:D} assigned to Group {1:D}", ledNo, bitfield.GetData());
        }

        private String _LedEnableDesc(MemBitfield bitfield)
        {
            if (bitfield.GetData() == 0x0000) return "ALL Channels disabled!";
            if (bitfield.GetData() == 0xFFFF) return "ALL Channels enabled.";
            String ret = "Disabled Led Channels =";
            for (int i = 0; i < 16; i += 1)
            {
                if (((bitfield.GetData() >> i) & 1) == 0)
                {
                    ret += String.Format(" {0:D}", i);
                }
            }
            return ret;
        }

        private String _BinLevelDesc(byte levelNo, MemBitfield bitfield)
        {
            return String.Format("GPIO Bin Class {0:D} Compare Level = {1:F} V", levelNo, (Convert.ToDouble(bitfield.GetData()) * 4.0) / Device.LSB_GPIO);
        }

        private String _BinGainDesc(byte levelNo, MemBitfield bitfield)
        {
            return String.Format("GPIO Bin Class {0:D} Gain = {1:F} %", levelNo, (Convert.ToDouble(bitfield.GetData()) + 1.0) / 256.0 * 100.0);
        }

        private String _PwmAnalogBundlingDesc(MemBitfield bitfield)
        {
            if (bitfield.GetData() == 0x0000) return "No Analog Bundling configured!";
            String ret = "Analog Bundled Channels =";
            for (int i = 0; i < 8; i += 1)
            {
                if (((bitfield.GetData() >> i) & 1) == 1)
                {
                    ret += String.Format(" {0:D}+{1:D} ", 2 * i, 2 * i+1);
                }
            }
            return ret;
        }

        private String _PwmCombineDesc(MemBitfield bitfield)
        {
            MemLocation memLoc = bitfield.GetMemLocation();
            MemBitfield memBfPri = memLoc.GetBitfield("COMBINE_PRI");
            MemBitfield memBfSec = memLoc.GetBitfield("COMBINE_SEC");

            if ((memBfPri.GetData() == 0) && (memBfSec.GetData() == 0))
                return "No Channels combined!";

            String ret = "Combined Channels = ";
            bool in_grp = false;
            for (int i = 0; i < 16; i += 1)
            {
                uint val;
                if ((i & 1) == 1)
                    val = (uint) (memBfSec.GetData() >> (i >> 1)) & 1;
                else
                    val = (uint)(memBfPri.GetData() >> (i >> 1)) & 1;

                if (val == 1)
                {
                    if (!in_grp)
                        ret += " ";
                    ret += String.Format("{0:D}+", i);
                    in_grp = true;
                }
                else
                {
                    if (in_grp)
                        ret += String.Format("{0:D} ", i);
                    in_grp = false;
                }
            }
            return ret;
        }

        private String _PwmInFreqDesc(MemBitfield bitfield)
        {
            switch (bitfield.GetData())
            {
                case 4: return "PWMIN frequency = 100 Hz";
                case 5: return "PWMIN frequency = 100 Hz";
                case 6: return "PWMIN frequency = 100 Hz";
                case 7: return "PWMIN frequency = 125 Hz";
                case 0: return "PWMIN frequency = 200 Hz";
                case 1: return "PWMIN frequency = 250 Hz";
                case 2: return "PWMIN frequency = 500 Hz";
                case 3: return "PWMIN frequency = 1000 Hz";
            }
            return "";
        }

        private String _PwmInAccDesc(MemBitfield bitfield)
        {
            uint temp = bitfield.GetData();
            if (temp == 0) temp = 15;
            return String.Format("Allowed PWMIN frequency accuracy = {0:D} %", temp);
        }

        private String _PwmInMidAltDesc(MemBitfield bitfield)
        {
            if (bitfield.GetData() == 1)
                return "PWMIN MID range output = Alternate Setting";
            else
                return "PWMIN MID range output = ZERO";
        }

        private String _PwmInInvalidAltDesc(MemBitfield bitfield)
        {
            if (bitfield.GetData() == 1)
                return "PWMIN INVALID range output = Alternate Setting";
            else
                return "PWMIN INVALID range output = ZERO";
        }

        private String _PwmFreqDesc(MemBitfield bitfield)
        {
            MemLocation memLoc = bitfield.GetMemLocation();
            MemBitfield memBfPeriod = memLoc.GetBitfield("PERIOD");
            MemBitfield memBfPrescaler = memLoc.GetBitfield("PRESCALER");

            return String.Format("PWM Frequency = {0:F} Hz", 10000000.0 / ((4.0 * Convert.ToDouble(memBfPeriod.GetData()) + 3.0) * (1.0 + Convert.ToDouble(memBfPrescaler.GetData()))));
        }

        private String _DerateVtStartDesc(MemBitfield bitfield)
        {
            MemLocation memLoc = bitfield.GetMemLocation();
            MemBitfield memBfStart = memLoc.GetBitfield("START");
            MemBitfield memBfStop = memLoc.GetBitfield("STOP");
            if (memBfStart.GetData() == 0)
                return "Temperate Derating Disabled!";
            if (memBfStop.GetData() == 0)
                return "Temperate Derating Disabled!";

            return String.Format("Temperate Derating Start Level = {0:D} °C", Device.GetTemperatureFrom8bit((byte)bitfield.GetData()));
        }

        private String _DerateVtStopDesc(MemBitfield bitfield)
        {
            MemLocation memLoc = bitfield.GetMemLocation();
            MemBitfield memBfStart = memLoc.GetBitfield("START");
            MemBitfield memBfStop = memLoc.GetBitfield("STOP");
            if (memBfStart.GetData() == 0)
                return "Temperate Derating Disabled!";
            if (memBfStop.GetData() == 0)
                return "Temperate Derating Disabled!";

            return String.Format("Temperate Derating Stop Level = {0:D} °C", Device.GetTemperatureFrom8bit((byte)bitfield.GetData()));
        }

        private String _DerateVtGainDesc(MemBitfield bitfield)
        {
            MemLocation memLoc = bitfield.GetMemLocation();
            MemBitfield memBfStart = memLoc.GetBitfield("START");
            MemBitfield memBfStop = memLoc.GetBitfield("STOP");
            if (memBfStart.GetData() == 0)
                return "Temperate Derating Disabled!";
            if (memBfStop.GetData() == 0)
                return "Temperate Derating Disabled!";

            return String.Format("Temperate Derating Gain = {0:F4} %/K", Convert.ToDouble(bitfield.GetData()) / 4096.0 * 100.0);
        }

        private String _DerateVsStartDesc(MemBitfield bitfield)
        {
            MemLocation memLoc = bitfield.GetMemLocation();
            MemBitfield memBfStart = memLoc.GetBitfield("START");
            MemBitfield memBfStop = memLoc.GetBitfield("STOP");
            if (memBfStart.GetData() == 0)
                return "VS Derating Disabled!";
            if (memBfStop.GetData() == 0)
                return "VS Derating Disabled!";

            return String.Format("VS Derating Start Level = {0:F} V", Convert.ToDouble(bitfield.GetData()) * 4.0 / Device.LSB_VSUP);
        }

        private String _DerateVsStopDesc(MemBitfield bitfield)
        {
            MemLocation memLoc = bitfield.GetMemLocation();
            MemBitfield memBfStart = memLoc.GetBitfield("START");
            MemBitfield memBfStop = memLoc.GetBitfield("STOP");
            if (memBfStart.GetData() == 0)
                return "VS Derating Disabled!";
            if (memBfStop.GetData() == 0)
                return "VS Derating Disabled!";

            return String.Format("VS Derating Start Level = {0:F} V", Convert.ToDouble(bitfield.GetData()) * 4.0 / Device.LSB_VSUP);
        }

        private String _DerateVsGainDesc(MemBitfield bitfield)
        {
            MemLocation memLoc = bitfield.GetMemLocation();
            MemBitfield memBfStart = memLoc.GetBitfield("START");
            MemBitfield memBfStop = memLoc.GetBitfield("STOP");
            if (memBfStart.GetData() == 0)
                return "VS Derating Disabled!";
            if (memBfStop.GetData() == 0)
                return "VS Derating Disabled!";

            return String.Format("VS Derating Gain = {0:F4} %/V", Convert.ToDouble(bitfield.GetData()) / 8192.0 * Device.LSB_VSUP * 100.0);
        }

        private String _DerateSupplySrcDesc(MemBitfield bitfield)
        {
            switch (bitfield.GetData())
            {
                case 0: return "Supply Source for Derating = VS";
                case 1: return "Supply Source for Derating = SENSE0";
                case 2: return "Supply Source for Derating = SENSE1";
            }
            return "reserved, do not use!";
        }

        private String _CanArbBitrateDesc(MemBitfield bitfield)
        {
            MemLocation memLoc = bitfield.GetMemLocation();
            MemBitfield memBfBrp = memLoc.GetBitfield("A_BRP");
            MemBitfield memBfSeg1 = memLoc.GetBitfield("A_SEG1");
            MemBitfield memBfSeg2 = memLoc.GetBitfield("A_SEG2_SJW");

            double bitrate = 40000000.0 / (Convert.ToDouble(memBfBrp.GetData()) + 1.0) / ((Convert.ToDouble(memBfSeg1.GetData()) + 2.0) + (Convert.ToDouble(memBfSeg2.GetData()) + 1.0));
            double sp = (Convert.ToDouble(memBfSeg1.GetData()) + 2.0) / ((Convert.ToDouble(memBfSeg1.GetData()) + 2.0) + (Convert.ToDouble(memBfSeg2.GetData()) + 1.0)) * 100.0;

            return String.Format("CAN Arbitration Phase Bitrate = {0:F} (SP = {1:F} %)", bitrate, sp);
        }

        private String _CanFdBitrateDesc(MemBitfield bitfield)
        {
            MemLocation memLoc = bitfield.GetMemLocation();
            MemBitfield memBfBrp = memLoc.GetBitfield("FD_BRP");
            MemBitfield memBfSeg1 = memLoc.GetBitfield("FD_SEG1");
            MemBitfield memBfSeg2 = memLoc.GetBitfield("FD_SEG2_SJW");

            double bitrate = 40000000.0 / (Convert.ToDouble(memBfBrp.GetData()) + 1.0) / ((Convert.ToDouble(memBfSeg1.GetData()) + 2.0) + (Convert.ToDouble(memBfSeg2.GetData()) + 1.0));
            double sp = (Convert.ToDouble(memBfSeg1.GetData()) + 2.0) / ((Convert.ToDouble(memBfSeg1.GetData()) + 2.0) + (Convert.ToDouble(memBfSeg2.GetData()) + 1.0)) * 100.0;

            return String.Format("CAN FD Data Phase Bitrate = {0:F} (SP = {1:F} %)", bitrate, sp);
        }

        private String _CanTdcEnDesc(MemBitfield bitfield)
        {
            if (bitfield.GetData() == 1)
                return "CAN FD Transmitter Delay Compensation enabled.";
            else
                return "CAN FD Transmitter Delay Compensation disabled!";
        }

        private String _CanTxBrsDesc(MemBitfield bitfield)
        {
            if (bitfield.GetData() == 1)
                return "Switch to FD bitrate in Read Response Data Phase.";
            else
                return "Send Read Response Data Phase with Arbitration Bitrate.";
        }

        private String _RecoveryRetriesPauseDesc(MemBitfield bitfield)
        {
            MemLocation memLoc = bitfield.GetMemLocation();
            MemBitfield memBfRetries = memLoc.GetBitfield("RECOVERY_RETRIES");
            MemBitfield memBfPause = memLoc.GetBitfield("RECOVERY_PAUSE");

            String ret = "";

            if (memBfPause.GetData() == 0)
                return "Startup Pause disabled";

            if (memBfRetries.GetData() == 0)
                ret += "Immediate Pause of ";

            switch (memBfPause.GetData())
            {
                case 1: ret += "typ. 100ms"; break;
                case 2: ret += "typ. 200ms"; break;
                case 3: ret += "typ. 300ms"; break;
                case 4: ret += "typ. 500ms"; break;
                case 5: ret += "typ. 1s"; break;
                case 6: ret += "typ. 2s"; break;
                case 7: ret += "typ. 4s"; break;
                case 8: ret += "typ. 10s"; break;
            }

            if (memBfRetries.GetData() == 0)
                ret += " after internal Error Reset.";
            else
                ret += String.Format(" Wait Time after {0:D} unsuccessful startup retries.", memBfRetries.GetData());

            return ret;
        }

        private String _DiagRetryBurstPauseDesc(MemBitfield bitfield)
        {
            MemLocation memLoc = bitfield.GetMemLocation();
            MemBitfield memBfBurst = memLoc.GetBitfield("DIAG_RETRY_BURST");
            MemBitfield memBfPause = memLoc.GetBitfield("DIAG_RETRY_PAUSE");

            if (memBfBurst.GetData() == 0)
                return "Diag Retries disabled!";

            if (memBfPause.GetData() == 0)
                return "No Retry Pause configured!";

            return String.Format("{0:D} PWM cycles with retries followed by {1:D} PWM cycles with a pause", memBfBurst.GetData(), memBfPause.GetData());
        }

        private String _DiagVsDiagDisabledThresholdDesc(MemBitfield bitfield)
        {
            if (bitfield.GetData() > 0)
                return String.Format("Diagnosis disabled when VS < {0:F} V.", Convert.ToDouble(bitfield.GetData()) / Device.LSB_VSUP);
            else 
                return String.Format("Diag Disabled by VS disabled.");
        }

        private String _DiagCombRetryAllDesc(MemBitfield bitfield)
        {
            if (bitfield.GetBool())
                return "Retries on all channels that are combined with the failing channel(s).";
            else
                return "Retries only on the failing channel(s).";
        }

        private String _DiagVtTooHighDesc(MemBitfield bitfield)
        {
            return String.Format("BUS_STATUS.vt_too_high flag will be set when {0:D} °C are exceeded.", Device.GetTemperatureFrom8bit((byte)bitfield.GetData()));
        }

        private String _DiagVsTooHighDesc(MemBitfield bitfield)
        {
            return String.Format("BUS_STATUS.vs_too_high flag will be set when {0:F} V are exceeded.", Convert.ToDouble(bitfield.GetData()) * 4.0 / Device.LSB_VSUP);
        }

        private String _DiagVsTooLowDesc(MemBitfield bitfield)
        {
            return String.Format("BUS_STATUS.vs_too_low flag will be set when {0:F} V are undershot.", Convert.ToDouble(bitfield.GetData()) * 4.0 / Device.LSB_VSUP);
        }

        private String _DiagTimeoutDesc(MemBitfield bitfield)
        {
            if (bitfield.GetData() == 0)
                return "Diag Unknown Timeout disabled!";

            return String.Format("Diag result switches after {0:D} PWM Periods without measurement to UNKNWON.", 5 * bitfield.GetData());
        }

        private String _DiagSelDesc(byte ledNo, MemBitfield bitfield)
        {
            if (bitfield.GetData() == 1)
                return String.Format("LED {0:D} Pin used as DIAG output!", ledNo);
            else
                return String.Format("LED {0:D} Pin is PWM output.", ledNo);
        }

        private String _DiagIeDesc(byte ledNo, MemBitfield bitfield)
        {
            if (bitfield.GetData() == 1)
                return String.Format("LED {0:D} Pin used as DIAG input!", ledNo);
            else
                return String.Format("LED {0:D} Pin is no Diagnosis input.", ledNo);
        }

        private String _DiagSlmDesc(byte diagNo, MemBitfield bitfield)
        {
            if (bitfield.GetData() == 1)
                return String.Format("Diagnosis Group {0:D} in Single-Lamp-Mode.", diagNo);
            else
                return String.Format("Diagnosis Group {0:D} in Multi-Lamp-Mode.", diagNo);
        }

        private String _DiagBusFailureDesc(byte diagNo, MemBitfield bitfield)
        {
            if (bitfield.GetData() == 1)
                return String.Format("Diagnosis Group {0:D} will signal failure mode (SAFESTATE).", diagNo);
            else
                return String.Format("Diagnosis Group {0:D} will NOT signal failure mode (SAFESTATE).", diagNo);
        }

        private String _DiagFilterThdDesc(MemBitfield bitfield)
        {
            if (bitfield.GetData() == 0)
                return "Diagnosis DISABLED!";
            else
                return String.Format("Diagnosis Filter Threshold configured to {0:D}", bitfield.GetData());
        }

        private String _CanProtectCurrentDesc(MemBitfield bitfield)
        {
            if (bitfield.GetData() == 1)
                return "Bus Current locked (only Default Currents are used)!";
            else
                return "Bus Current writeable";
        }

        private String _CanCrystalSelDesc(MemBitfield bitfield)
        {
            if (bitfield.GetData() == 1)
                return "16 MHz selected as external crystal.";
            else
                return "8 MHz selected as external crystal.";
        }

        private String _CanRefclkMasterDesc(MemBitfield bitfield)
        {
            if (bitfield.GetData() == 1)
                return "External quartz oscillator is reference clock for FLL. CLKREF pin provides reference clock.";
            else
                return "CLKREF pin used as reference clock.";
        }

        private String _CanValidThdDesc(MemBitfield bitfield)
        {
            if (bitfield.GetData() == 0)
                return "INVALID value!";
            else
                return String.Format("{0:D} consecutive valid frames are required to transition to NORMAL state.", bitfield.GetData());
        }

        private String _CanErrorThdDesc(MemBitfield bitfield)
        {
            if (bitfield.GetData() == 0)
                return "INVALID value!";
            else
                return String.Format("{0:D} errors in frames are required to transition to SAFE state.", bitfield.GetData());
        }

        private String _CanTimeoutDesc(MemBitfield bitfield)
        {
            if (canTimeout == 0)
                return "Basic Protection message Timeout DISABLED!";
            else
                return String.Format("Basic Protection message Timeout = {0:D} ms.", canTimeout);
        }

        private String _CanStartupTimeoutDesc(MemBitfield bitfield)
        {
            if (canStartupTimeout == 0)
                return "Basic Protection Startup Timeout DISABLED!";
            else
                return String.Format("Basic Protection Startup Timeout = {0:D} ms.", canStartupTimeout);
        }

        private String _CanFirstFactorDesc(MemBitfield bitfield)
        {
            double timeout = Convert.ToDouble(canTimeout) * (1.0 + Convert.ToDouble(bitfield.GetData()) / 8.0);

            return String.Format("Basic Protection First Message Timeout = {0:F} ms.",  timeout);
        }

        private String _CanErrorResetThdDesc(MemBitfield bitfield)
        {
            return String.Format("Basic Protection Error Counter is reset after {0:D} consecutive valid frames in NORMAL state.", bitfield.GetData());
        }

        private String _GpioFuncDesc(MemBitfield bitfield)
        {
            switch(bitfield.GetData()){
                case 0: return "GPIO is digital input";
                case 1: return "GPIO is analog input";
                case 2: return "GPIO is used for binning";
                case 3: return "GPIO is used for voltage regulation";
            }
            return "GPIO is high-ohmic";
        }

        private String _GpioVregModeDesc(MemBitfield bitfield)
        {
            MemLocation memLoc = bitfield.GetMemLocation();
            MemBitfield memBfGpioFunc = memLoc.GetBitfield("GPIO_FUNC");
            if (memBfGpioFunc.GetData() != 3)
                return "GPIO not configured for Voltage Regulation!";

            switch(bitfield.GetData()){
                case 0: return "GPIO Voltage Regulation with Current Source (Stand-alone)";
                case 1: return "GPIO Voltage Regulation with Current Sink (Stand-alone)";
                case 2: return "GPIO Voltage Regulation with Voltage (Multi-chip)";
            }
            return "reserved, do not use!";
        }

        private String _GpioVregTakeoverThdDesc(MemBitfield bitfield)
        {
            MemLocation memLoc = bitfield.GetMemLocation();
            MemBitfield memBfGpioFunc = memLoc.GetBitfield("GPIO_FUNC");
            if (memBfGpioFunc.GetData() != 3)
                return "GPIO not configured for voltage regulation!";

            return String.Format("GPIO Voltage Regulation with {0:F} V Takeover Threshold (recommended 0.6 V).", Convert.ToDouble(bitfield.GetData()) * 0.1);
        }

        private String _GpioVregTargetVoltDesc(MemBitfield bitfield)
        {
            MemLocation memLoc = bitfield.GetMemLocation();
            MemBitfield memBfGpioFunc = memLoc.GetBitfield("GPIO_FUNC");
            if (memBfGpioFunc.GetData() != 3)
                return "GPIO not configured for voltage regulation!";

            return String.Format("GPIO Voltage Regulation with {0:F4} V Target Voltage (recommended 1.2V).", Convert.ToDouble(bitfield.GetData()) * 0.025);
        }

        private String _GpioVregRegulationFactorDesc(MemBitfield bitfield)
        {
            MemLocation memLoc = bitfield.GetMemLocation();
            MemBitfield memBfGpioFunc = memLoc.GetBitfield("GPIO_FUNC");
            if (memBfGpioFunc.GetData() != 3)
                return "GPIO not configured for voltage regulation!";

            return "Regulation Factor for GPIO voltage regulation. Select for stability.";
        }

        private String _ValidKeyDesc(MemBitfield bitfield)
        {
            if (bitfield.GetData() == 0xAE)
                return "EEPROM Configuration marked as valid.";
            else
                return "Use value of 0xAE to mark EEPROM Configuration as valid.";
        }

        public override String Description(MemBitfield bitfield)
        {
            MemLocation memLoc = bitfield.GetMemLocation();

            if (bitfield.name == LABEL_VALID_COOKIE) return _ValidKeyDesc(bitfield);
            if (bitfield.name == LABEL_VREG_REGULATION_FACTOR) return _GpioVregRegulationFactorDesc(bitfield);
            if (bitfield.name == LABEL_VREG_TARGET_VOLTAGE) return _GpioVregTargetVoltDesc(bitfield);
            if (bitfield.name == LABEL_VREG_TAKEOVER_THRESHOLD) return _GpioVregTakeoverThdDesc(bitfield);
            if (bitfield.name == LABEL_GPIO_FUNC) return _GpioFuncDesc(bitfield);
            if (bitfield.name == LABEL_VREG_MODE) return _GpioVregModeDesc(bitfield);
            if (bitfield.name == LABEL_W3_STARTUP_FACTOR) return _CanStartupTimeoutDesc(bitfield);
            if (bitfield.name == LABEL_W3_TIMEOUT_FACTOR) return _CanTimeoutDesc(bitfield);
            if (bitfield.name == LABEL_W3_ERROR_THD) return _CanErrorThdDesc(bitfield);
            if (bitfield.name == LABEL_W3_TIMEOUT) return _CanTimeoutDesc(bitfield);
            if (bitfield.name == LABEL_W3_FIRST_FACTOR) return _CanFirstFactorDesc(bitfield);
            if (bitfield.name == LABEL_W3_ERROR_RESET) return _CanErrorResetThdDesc(bitfield);
            if (bitfield.name == LABEL_W3_VALID_THD) return _CanValidThdDesc(bitfield);
            if (bitfield.name == LABEL_W3_T_STARTUP) return _CanStartupTimeoutDesc(bitfield);
            if (bitfield.name == LABEL_CAN_TX_BRS) return _CanTxBrsDesc(bitfield);
            if (bitfield.name == LABEL_A_BRP) return _CanArbBitrateDesc(bitfield);
            if (bitfield.name == LABEL_A_SEG1) return _CanArbBitrateDesc(bitfield);           
            if (bitfield.name == LABEL_A_SEG2_SJW) return _CanArbBitrateDesc(bitfield);
            if (bitfield.name == LABEL_FD_BRP) return _CanFdBitrateDesc(bitfield);
            if (bitfield.name == LABEL_FD_SEG1) return _CanFdBitrateDesc(bitfield);
            if (bitfield.name == LABEL_FD_SEG2_SJW) return _CanFdBitrateDesc(bitfield);
            if (bitfield.name == LABEL_TDC_EN) return _CanTdcEnDesc(bitfield);
            if (bitfield.name == LABEL_DIAG_TIMEOUT) return _DiagTimeoutDesc(bitfield);
            if (bitfield.name == LABEL_DIAG0_IE) return _DiagIeDesc(0, bitfield);
            if (bitfield.name == LABEL_DIAG1_IE) return _DiagIeDesc(1, bitfield);         
            if (bitfield.name == LABEL_DIAG2_IE) return _DiagIeDesc(2, bitfield);
            if (bitfield.name == LABEL_DIAG0_SEL) return _DiagSelDesc(0, bitfield);        
            if (bitfield.name == LABEL_DIAG1_SEL) return _DiagSelDesc(1, bitfield);        
            if (bitfield.name == LABEL_DIAG2_SEL) return _DiagSelDesc(2, bitfield);        
            if (bitfield.name == LABEL_DIAG0_SLM) return _DiagSlmDesc(0, bitfield);
            if (bitfield.name == LABEL_DIAG1_SLM) return _DiagSlmDesc(1, bitfield);        
            if (bitfield.name == LABEL_DIAG2_SLM) return _DiagSlmDesc(2, bitfield);
            if (bitfield.name == LABEL_DIAG0_BUS_FAILURE) return _DiagBusFailureDesc(0, bitfield);
            if (bitfield.name == LABEL_DIAG1_BUS_FAILURE) return _DiagBusFailureDesc(1, bitfield);
            if (bitfield.name == LABEL_DIAG2_BUS_FAILURE) return _DiagBusFailureDesc(2, bitfield);
            if (bitfield.name == LABEL_DIAG_CNT_THD) return _DiagFilterThdDesc(bitfield);
            if (bitfield.name == LABEL_VS_TOO_HIGH) return _DiagVsTooHighDesc(bitfield);
            if (bitfield.name == LABEL_VS_TOO_LOW) return _DiagVsTooLowDesc(bitfield);
            if (bitfield.name == LABEL_VT_CRITICAL) return _DiagVtTooHighDesc(bitfield);      
            if (bitfield.name == LABEL_VT_TOO_HIGH) return _DiagVtTooHighDesc(bitfield);      
            if (bitfield.name == LABEL_DIAG_RETRY_PAUSE) return _DiagRetryBurstPauseDesc(bitfield);
            if (bitfield.name == LABEL_DIAG_RETRY_BURST) return _DiagRetryBurstPauseDesc(bitfield);
            if (bitfield.name == LABEL_DIAG_VS_DIAG_DISABLE_THRESHOLD) return _DiagVsDiagDisabledThresholdDesc(bitfield);
            if (bitfield.name == LABEL_DIAG_COMB_RETRY_ALL) return _DiagCombRetryAllDesc(bitfield);
            if (bitfield.name == LABEL_PROTECT_CURRENT) return _CanProtectCurrentDesc(bitfield);
            if (bitfield.name == LABEL_CRYSTAL_SEL) return _CanCrystalSelDesc(bitfield);
            if (bitfield.name == LABEL_REFCLK_MASTER) return _CanRefclkMasterDesc(bitfield);
            if (bitfield.name == LABEL_RECOVERY_RETRIES) return _RecoveryRetriesPauseDesc(bitfield);
            if (bitfield.name == LABEL_RECOVERY_PAUSE) return _RecoveryRetriesPauseDesc(bitfield);
            if (bitfield.name == LABEL_DERATE_SRC) return _DerateSupplySrcDesc(bitfield);
            if (memLoc.name == LABEL_VT_DERATE){
                if (bitfield.name == LABEL_START) return _DerateVtStartDesc(bitfield);              
                if (bitfield.name == LABEL_STOP) return _DerateVtStopDesc(bitfield);           
                if (bitfield.name == LABEL_GAIN) return _DerateVtGainDesc(bitfield);
            }
            if (memLoc.name == LABEL_VS_DERATE){
                if (bitfield.name == LABEL_START) return _DerateVsStartDesc(bitfield);              
                if (bitfield.name == LABEL_STOP) return _DerateVsStopDesc(bitfield);           
                if (bitfield.name == LABEL_GAIN) return _DerateVsGainDesc(bitfield);
            }

            if (bitfield.name == LABEL_PERIOD) return _PwmFreqDesc(bitfield);
            if (bitfield.name == LABEL_PRESCALER) return _PwmFreqDesc(bitfield);
            if (bitfield.name == LABEL_PWMIN_MID_ALT) return _PwmInMidAltDesc(bitfield);
            if (bitfield.name == LABEL_PWMIN_ACC) return _PwmInAccDesc(bitfield);
            if (bitfield.name == LABEL_PWMIN_FREQ) return _PwmInFreqDesc(bitfield);
            if (bitfield.name == LABEL_PWMIN_INVALID_ALT) return _PwmInInvalidAltDesc(bitfield);
            if (bitfield.name == LABEL_ANALOG) return _PwmAnalogBundlingDesc(bitfield);
            if (bitfield.name == LABEL_COMBINE_PRI) return _PwmCombineDesc(bitfield);
            if (bitfield.name == LABEL_COMBINE_SEC) return _PwmCombineDesc(bitfield);
            if (bitfield.name == LABEL_BIN_LEVEL_0) return _BinLevelDesc(0, bitfield);
            if (bitfield.name == LABEL_BIN_LEVEL_1) return _BinLevelDesc(1, bitfield);
            if (bitfield.name == LABEL_BIN_LEVEL_2) return _BinLevelDesc(2, bitfield);
            if (bitfield.name == LABEL_BIN_LEVEL_3) return _BinLevelDesc(3, bitfield);
            if (bitfield.name == LABEL_BIN_GAIN_0) return _BinGainDesc(0, bitfield);        
            if (bitfield.name == LABEL_BIN_GAIN_1) return _BinGainDesc(1, bitfield);       
            if (bitfield.name == LABEL_BIN_GAIN_2) return _BinGainDesc(2, bitfield);        
            if (bitfield.name == LABEL_BIN_GAIN_3) return _BinGainDesc(3, bitfield);
            if (bitfield.name == LABEL_BIN_GAIN_4) return _BinGainDesc(4, bitfield);
            if (bitfield.name == LABEL_LED0) return _LedGroupSelDesc(0, bitfield);
            if (bitfield.name == LABEL_LED1) return _LedGroupSelDesc(1, bitfield);               
            if (bitfield.name == LABEL_LED2) return _LedGroupSelDesc(2, bitfield);               
            if (bitfield.name == LABEL_LED3) return _LedGroupSelDesc(3, bitfield);               
            if (bitfield.name == LABEL_LED4) return _LedGroupSelDesc(4, bitfield);               
            if (bitfield.name == LABEL_LED5) return _LedGroupSelDesc(5, bitfield);               
            if (bitfield.name == LABEL_LED6) return _LedGroupSelDesc(6, bitfield);               
            if (bitfield.name == LABEL_LED7) return _LedGroupSelDesc(7, bitfield);               
            if (bitfield.name == LABEL_LED8) return _LedGroupSelDesc(8, bitfield);               
            if (bitfield.name == LABEL_LED9) return _LedGroupSelDesc(9, bitfield);               
            if (bitfield.name == LABEL_LED10) return _LedGroupSelDesc(10, bitfield);
            if (bitfield.name == LABEL_LED11) return _LedGroupSelDesc(11, bitfield);
            if (bitfield.name == LABEL_LED12) return _LedGroupSelDesc(12, bitfield);
            if (bitfield.name == LABEL_LED13) return _LedGroupSelDesc(13, bitfield);
            if (bitfield.name == LABEL_LED14) return _LedGroupSelDesc(14, bitfield);
            if (bitfield.name == LABEL_LED15) return _LedGroupSelDesc(15, bitfield);
            if (bitfield.name == LABEL_LED_ENABLE) return _LedEnableDesc(bitfield);


            MatchCollection matches = Regex.Matches(memLoc.name, "^GROUP_([0-3]+)");
            if ((matches.Count == 1) && (matches[0].Groups.Count == 2))
            {
                String groupStr = (matches[0].Groups[1].ToString());
                byte group = Convert.ToByte(groupStr);

                if (bitfield.name == LABEL_SCALE_GAIN1) return _GroupScaleGainDesc(group, 1, bitfield);
                if (bitfield.name == LABEL_SCALE_GAIN2) return _GroupScaleGainDesc(group, 2, bitfield);
                if (bitfield.name == LABEL_SCALE_GAIN3) return _GroupScaleGainDesc(group, 3, bitfield);
                if (bitfield.name == LABEL_PWM_ALT_SCALE) return _GroupPwmAltScaleDesc(group, bitfield);
                if (bitfield.name == LABEL_BIN_FACTOR) return _GroupBinFactorDesc(group, bitfield);
                if (bitfield.name == LABEL_BIN_CONFIG) return _GroupBinConfigDesc(group, bitfield);
                if (bitfield.name == LABEL_SUPPLY_SRC) return _GroupSupplySrcDesc(group, bitfield);
                if (bitfield.name == LABEL_DIAG_GROUP) return _GroupDiagGroupDesc(group, bitfield);
                if (bitfield.name == LABEL_LISTEN_TO_PWMIN) return _GroupListenToPwmInDesc(group, bitfield);
                if (bitfield.name == LABEL_VREG_GPIO) return _GroupVregConfigDesc(group, bitfield);
                if (bitfield.name == LABEL_VREG_ENABLE) return _GroupVregConfigDesc(group, bitfield);
                if (bitfield.name == LABEL_OPEN_MIN_THD) return _GroupOpenMinThdDesc(group, bitfield);
                if (bitfield.name == LABEL_SHORT_THD) return _GroupShortThdDesc(group, bitfield);
                if (bitfield.name == LABEL_OPEN_THD) return _GroupOpenThdDesc(group, bitfield);
            }

            return bitfield.Description();
        }

        public EEProm()
            : base(32, "EEPROM", false)
        {
            ushort addr;

            for (byte no = 0; no < 44; no += 1)
            {
                addr = (ushort)(4 * no);
                this.Add(new MemLocation(String.Format("CUSTOMER_AREA_{0:D}", no), addr));
            }

            MemLocation memLoc;

            for (byte group = 0; group < 4; group += 1)
            {
                memLoc = new MemLocation(String.Format("GROUP_{0:D}_SCALE_GAIN", group), (ushort)(ADDR_PARAMETER_AREA + group * 0xC + 0x0));
                memLoc.AddBitfield(new MemBitfield(LABEL_SCALE_GAIN1, 8, 0));
                memLoc.AddBitfield(new MemBitfield(LABEL_SCALE_GAIN2, 8, 8));
                memLoc.AddBitfield(new MemBitfield(LABEL_SCALE_GAIN3, 8, 16));
                this.Add(memLoc);

                memLoc = new MemLocation(String.Format("GROUP_{0:D}_CONFIG", group), (ushort)(ADDR_PARAMETER_AREA + group * 0xC + 0x4));
                memLoc.AddBitfield(new MemBitfield(LABEL_PWM_ALT_SCALE, 8, 0));
                memLoc.AddBitfield(new MemBitfield(LABEL_BIN_FACTOR, 8, 8));
                memLoc.AddBitfield(new MemBitfield(LABEL_BIN_CONFIG, 2, 16));
                memLoc.AddBitfield(new MemBitfield(LABEL_SUPPLY_SRC, 2, 18));
                memLoc.AddBitfield(new MemBitfield(LABEL_DIAG_GROUP, 2, 20));
                memLoc.AddBitfield(new MemBitfield(LABEL_LISTEN_TO_PWMIN, 1, 22));
                memLoc.AddBitfield(new MemBitfield(LABEL_VREG_GPIO, 1, 23));
                memLoc.AddBitfield(new MemBitfield(LABEL_VREG_ENABLE, 1, 24));
                this.Add(memLoc);

                memLoc = new MemLocation(String.Format("GROUP_{0:D}_OPEN_SHORT", group), (ushort)(ADDR_PARAMETER_AREA + group * 0xC + 0x8));
                memLoc.AddBitfield(new MemBitfield(LABEL_OPEN_MIN_THD, 8, 0));
                memLoc.AddBitfield(new MemBitfield(LABEL_SHORT_THD, 8, 8));
                memLoc.AddBitfield(new MemBitfield(LABEL_OPEN_THD, 8, 16));
                this.Add(memLoc);
            }

            memLoc = new MemLocation("LED_GROUP_SEL_0_11", ADDR_PARAMETER_AREA + 0x30);
            memLoc.AddBitfield(new MemBitfield(LABEL_LED0, 2, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_LED1, 2, 2));
            memLoc.AddBitfield(new MemBitfield(LABEL_LED2, 2, 4));
            memLoc.AddBitfield(new MemBitfield(LABEL_LED3, 2, 6));
            memLoc.AddBitfield(new MemBitfield(LABEL_LED4, 2, 8));
            memLoc.AddBitfield(new MemBitfield(LABEL_LED5, 2, 10));
            memLoc.AddBitfield(new MemBitfield(LABEL_LED6, 2, 12));
            memLoc.AddBitfield(new MemBitfield(LABEL_LED7, 2, 14));
            memLoc.AddBitfield(new MemBitfield(LABEL_LED8, 2, 16));
            memLoc.AddBitfield(new MemBitfield(LABEL_LED9, 2, 18));
            memLoc.AddBitfield(new MemBitfield(LABEL_LED10, 2, 20));
            memLoc.AddBitfield(new MemBitfield(LABEL_LED11, 2, 22));
            this.Add(memLoc);

            memLoc = new MemLocation("LED_GROUP_SEL_12_15_ENABLE", ADDR_PARAMETER_AREA + 0x34);
            memLoc.AddBitfield(new MemBitfield(LABEL_LED12, 2, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_LED13, 2, 2));
            memLoc.AddBitfield(new MemBitfield(LABEL_LED14, 2, 4));
            memLoc.AddBitfield(new MemBitfield(LABEL_LED15, 2, 6));
            memLoc.AddBitfield(new MemBitfield(LABEL_LED_ENABLE, 16, 8));
            this.Add(memLoc);

            for (byte led = 0; led < 16; led += 1)
            {
                memLoc = new MemLocation(String.Format("PULSE_CURRENT_DEFAULTS_LED_{0:D}", led), (ushort)(ADDR_PARAMETER_AREA + 0x38 + led * 0x4));
                memLoc.AddBitfield(new MemBitfield("CURR_BUS", 8, 0, "Bus Default Current"));
                memLoc.AddBitfield(new MemBitfield("CURR_ALT", 8, 8, "Alternative Current"));
                memLoc.AddBitfield(new MemBitfield("PULSE_ALT", 8, 16, "Alternative Pulse"));
                this.Add(memLoc);
            }

            memLoc = new MemLocation("BIN_CLASS_LEVEL_0_2", ADDR_PARAMETER_AREA + 0x78);
            memLoc.AddBitfield(new MemBitfield(LABEL_BIN_LEVEL_0, 8, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_BIN_LEVEL_1, 8, 8));
            memLoc.AddBitfield(new MemBitfield(LABEL_BIN_LEVEL_2, 8, 16));
            this.Add(memLoc);

            memLoc = new MemLocation("BIN_CLASS_LEVEL_3_GAIN_0_1", ADDR_PARAMETER_AREA + 0x7C);
            memLoc.AddBitfield(new MemBitfield(LABEL_BIN_LEVEL_3, 8, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_BIN_GAIN_0, 8, 8));
            memLoc.AddBitfield(new MemBitfield(LABEL_BIN_GAIN_1, 8, 16));
            this.Add(memLoc);

            memLoc = new MemLocation("BIN_CLASS_GAIN_2_4", ADDR_PARAMETER_AREA + 0x80);
            memLoc.AddBitfield(new MemBitfield(LABEL_BIN_GAIN_2, 8, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_BIN_GAIN_3, 8, 8));
            memLoc.AddBitfield(new MemBitfield(LABEL_BIN_GAIN_4, 8, 16));
            this.Add(memLoc);

            memLoc = new MemLocation("PWM_BUNDLING", ADDR_PARAMETER_AREA + 0x84);
            memLoc.AddBitfield(new MemBitfield(LABEL_ANALOG, 8, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_COMBINE_PRI, 8, 8));
            memLoc.AddBitfield(new MemBitfield(LABEL_COMBINE_SEC, 7, 16));
            if (DeviceType.IsE52295A) memLoc.AddBitfield(new MemBitfield(LABEL_PWMIN_INVALID_ALT, 1, 23));
            if (DeviceType.IsM52295A) memLoc.AddBitfield(new MemBitfield(LABEL_REFCLK_MASTER, 1, 24));
            this.Add(memLoc);

            memLoc = new MemLocation("PWM_CONFIG", ADDR_PARAMETER_PWM_CONFIG);
            memLoc.AddBitfield(new MemBitfield(LABEL_PWMIN_ACC, 4, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_PWMIN_FREQ, 3, 4));
            if (DeviceType.IsE52295A) memLoc.AddBitfield(new MemBitfield(LABEL_PWMIN_MID_ALT, 1, 7));
            memLoc.AddBitfield(new MemBitfield(LABEL_PERIOD, 8, 8));
            memLoc.AddBitfield(new MemBitfield(LABEL_PRESCALER, 7, 16));
            memLoc.AddBitfield(new MemBitfield("SLEW", 2, 23, "Driver Slew Rate Setting"));
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_VS_DERATE, ADDR_PARAMETER_AREA + 0x8C);
            memLoc.AddBitfield(new MemBitfield(LABEL_START, 8, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_STOP, 8, 8));
            memLoc.AddBitfield(new MemBitfield(LABEL_GAIN, 8, 16));
            this.Add(memLoc);

            memLoc = new MemLocation(LABEL_VT_DERATE, ADDR_PARAMETER_AREA + 0x90);
            memLoc.AddBitfield(new MemBitfield(LABEL_START, 8, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_STOP, 8, 8));
            memLoc.AddBitfield(new MemBitfield(LABEL_GAIN, 8, 16));
            this.Add(memLoc);

            if (DeviceType.IsM52295A) memLoc = new MemLocation("DERATE_CONFIG", ADDR_PARAMETER_AREA + 0x94);
            if (DeviceType.IsE52295A) memLoc = new MemLocation("DERATE_RECOVERY_CONFIG", ADDR_PARAMETER_AREA + 0x94);
            memLoc.AddBitfield(new MemBitfield(LABEL_DERATE_SRC, 2, 0));
            if (DeviceType.IsE52295A) memLoc.AddBitfield(new MemBitfield(LABEL_RECOVERY_RETRIES, 3, 2));
            if (DeviceType.IsE52295A) memLoc.AddBitfield(new MemBitfield(LABEL_RECOVERY_PAUSE, 4, 5));
            this.Add(memLoc);

            memLoc = new MemLocation("DIAG_RETRY_CONFIG", ADDR_PARAMETER_AREA + 0x98);
            memLoc.AddBitfield(new MemBitfield(LABEL_DIAG_RETRY_PAUSE, 8, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_DIAG_RETRY_BURST, 6, 8));
            if (DeviceType.IsE52295A) memLoc.AddBitfield(new MemBitfield(LABEL_DIAG_VS_DIAG_DISABLE_THRESHOLD, 10, 14));
            if (DeviceType.IsE52295A) memLoc.AddBitfield(new MemBitfield(LABEL_DIAG_COMB_RETRY_ALL, 1, 24));
            this.Add(memLoc);

            memLoc = new MemLocation("DIAG_THRESHOLDS", ADDR_PARAMETER_AREA + 0x9C);
            if (DeviceType.IsM52295A) memLoc.AddBitfield(new MemBitfield(LABEL_VT_CRITICAL, 8, 0));
            if (DeviceType.IsE52295A) memLoc.AddBitfield(new MemBitfield(LABEL_VT_TOO_HIGH, 8, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_VS_TOO_HIGH, 8, 8));
            memLoc.AddBitfield(new MemBitfield(LABEL_VS_TOO_LOW, 8, 16));
            this.Add(memLoc);

            memLoc = new MemLocation("DIAG_CONFIG", ADDR_PARAMETER_AREA + 0xA0);
            memLoc.AddBitfield(new MemBitfield(LABEL_DIAG_TIMEOUT, 4, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_DIAG0_IE, 1, 4));
            memLoc.AddBitfield(new MemBitfield(LABEL_DIAG1_IE, 1, 5));
            memLoc.AddBitfield(new MemBitfield(LABEL_DIAG2_IE, 1, 6));
            memLoc.AddBitfield(new MemBitfield(LABEL_DIAG0_SEL, 1, 7));
            memLoc.AddBitfield(new MemBitfield(LABEL_DIAG1_SEL, 1, 8));
            memLoc.AddBitfield(new MemBitfield(LABEL_DIAG2_SEL, 1, 9));
            memLoc.AddBitfield(new MemBitfield(LABEL_DIAG0_SLM, 1, 10));
            memLoc.AddBitfield(new MemBitfield(LABEL_DIAG1_SLM, 1, 11));
            memLoc.AddBitfield(new MemBitfield(LABEL_DIAG2_SLM, 1, 12));
            memLoc.AddBitfield(new MemBitfield(LABEL_DIAG0_BUS_FAILURE, 1, 13));
            memLoc.AddBitfield(new MemBitfield(LABEL_DIAG1_BUS_FAILURE, 1, 14));
            memLoc.AddBitfield(new MemBitfield(LABEL_DIAG2_BUS_FAILURE, 1, 15));
            memLoc.AddBitfield(new MemBitfield(LABEL_DIAG_CNT_THD, 5, 16));
            this.Add(memLoc);

            memLoc = new MemLocation("CAN_TIMINGS", ADDR_PARAMETER_AREA + 0xA4);
            memLoc.AddBitfield(new MemBitfield(LABEL_A_BRP, 2, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_A_SEG1, 6, 2));
            memLoc.AddBitfield(new MemBitfield(LABEL_A_SEG2_SJW, 5, 8));
            memLoc.AddBitfield(new MemBitfield(LABEL_FD_BRP, 2, 13));
            memLoc.AddBitfield(new MemBitfield(LABEL_FD_SEG1, 4, 15));
            memLoc.AddBitfield(new MemBitfield(LABEL_FD_SEG2_SJW, 3, 19));
            memLoc.AddBitfield(new MemBitfield(LABEL_TDC_EN, 1, 22));
            this.Add(memLoc);

            memLoc = new MemLocation("CAN_CONFIG_0", ADDR_PARAMETER_AREA + 0xA8);
            memLoc.AddBitfield(new MemBitfield("CAN_NODE", 8, 0, "CAN Identifier Node"));
            memLoc.AddBitfield(new MemBitfield("CAN_GROUP", 8, 8, "CAN Identifier Multi-Write Group"));
            memLoc.AddBitfield(new MemBitfield("CAN_INDEX", 2, 16, "CAN Identifier Multi-Write Index (0..2)"));
            memLoc.AddBitfield(new MemBitfield(LABEL_CAN_TX_BRS, 1, 18));
            if (DeviceType.IsE52295A) memLoc.AddBitfield(new MemBitfield(LABEL_PROTECT_CURRENT, 1, 19));
            if (DeviceType.IsE52295A) memLoc.AddBitfield(new MemBitfield(LABEL_CRYSTAL_SEL, 1, 23));
            if (DeviceType.IsE52295A) memLoc.AddBitfield(new MemBitfield(LABEL_REFCLK_MASTER, 1, 24));
            this.Add(memLoc);

            memLoc = new MemLocation("CAN_CONFIG_1", ADDR_PARAMETER_AREA + 0xAC);
            memLoc.AddBitfield(new MemBitfield(LABEL_W3_T_STARTUP, 8, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_W3_VALID_THD, 8, 8));
            memLoc.AddBitfield(new MemBitfield("CAN_TYPE_M_W", 3, 16, "CAN Identifier Write Frame"));
            memLoc.AddBitfield(new MemBitfield("CAN_TYPE_M_R", 3, 19, "CAN Identifier Read-Request Frame"));
            memLoc.AddBitfield(new MemBitfield("CAN_TYPE_S_R", 3, 22, "CAN Identifier Read-Response Frame"));
            this.Add(memLoc);

            memLoc = new MemLocation("CAN_CONFIG_2", ADDR_PARAMETER_AREA + 0xB0);
            memLoc.AddBitfield(new MemBitfield(LABEL_W3_ERROR_THD, 8, 0));
            memLoc.AddBitfield(new MemBitfield(LABEL_W3_TIMEOUT, 8, 8));
            memLoc.AddBitfield(new MemBitfield(LABEL_W3_FIRST_FACTOR, 3, 16));
            memLoc.AddBitfield(new MemBitfield(LABEL_W3_ERROR_RESET, 2, 19));
            memLoc.AddBitfield(new MemBitfield("CAN_TYPE_M_W3", 3, 22, "CAN Identifier Multi-Write Frame"));
            this.Add(memLoc);

            memLoc = new MemLocation("CAN_CONFIG_3", ADDR_PARAMETER_AREA + 0xB4);
            memLoc.AddBitfield(new MemBitfield("SECURE_BYTE_S", 8, 0, "Basic Protection Secure Byte for Slave Response frames"));
            memLoc.AddBitfield(new MemBitfield("SECURE_BYTE_M", 8, 8, "Basic Protection Secure Byte for Master frames"));
            if (DeviceType.IsE52295A) memLoc.AddBitfield(new MemBitfield(LABEL_W3_STARTUP_FACTOR, 3, 16));
            if (DeviceType.IsE52295A) memLoc.AddBitfield(new MemBitfield(LABEL_W3_TIMEOUT_FACTOR, 3, 19));
            this.Add(memLoc);

            for (byte gpio = 0; gpio < 2; gpio += 1)
            {
                memLoc = new MemLocation(String.Format("GPIO{0:D}_CONFIG", gpio), (ushort)(ADDR_PARAMETER_AREA + 0xB8 + gpio * 0x4));
                memLoc.AddBitfield(new MemBitfield(LABEL_VREG_TARGET_VOLTAGE, 8, 0));
                memLoc.AddBitfield(new MemBitfield(LABEL_VREG_TAKEOVER_THRESHOLD, 4, 8));
                memLoc.AddBitfield(new MemBitfield(LABEL_GPIO_FUNC, 3, 12));
                memLoc.AddBitfield(new MemBitfield(LABEL_VREG_MODE, 2, 15));
                memLoc.AddBitfield(new MemBitfield(LABEL_VREG_REGULATION_FACTOR, 8, 17));
                this.Add(memLoc);
            }

            memLoc = new MemLocation("GPIO_VREG_CONFIG", ADDR_PARAMETER_AREA + 0xC0);
            memLoc.AddBitfield(new MemBitfield("VREG_TRACKING_THRESHOLD", 8, 0, "Threshold for voltage regulation in DAC LSB (recommended 10)"));
            this.Add(memLoc);

            memLoc = new MemLocation("EEPROM_KEYS", ADDR_PARAMETER_EEPROM_KEYS);
            memLoc.AddBitfield(new MemBitfield("EEPROM_KEY", 8, 0, "EEPROM write access key for BUS programming."));
            memLoc.AddBitfield(new MemBitfield(LABEL_VALID_COOKIE, 8, 8));
            this.Add(memLoc);

            // add some reserved fields
            for (ushort a = 0x174; a < 0x17C; a += 4) this.Add(new MemLocation("RESERVED", a, true));

            this.Verify();
        }
    }

}
