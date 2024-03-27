using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

using static ELMOS_521._38_UART_Eval.ApplicationData;

namespace ELMOS_521._38_UART_Eval
{
    
    public class CommunicationStateMachine : StateIterator
    {
        private E52138ChipAPI parent;
        private ApplicationData data;
        private StatusInformationStateMachine statusInformationStateMachine;
        
        public CommunicationStateMachine(E52138ChipAPI parent, ApplicationData data, double runTimer = 0.0) : base(runTimer)
        {
            this.parent = parent;
            this.data = data;
            statusInformationStateMachine = new StatusInformationStateMachine(parent);
        }

        public void Close()
        {
            Stop();
            statusInformationStateMachine.Close();
            parent = null;
        }

        [Transition]
        public void UpdatePWM()
        {
            parent?.UpdatePWMvalues();
        }

        [Transition]
        public void SetCurrentBoost()
        {
            parent?.SetCurrentBoost();
        }

        [Transition]
        public void GatherStatusInformation()
        {
            if (data.StatusUpdates && data.ActiveChip == parent.DeviceAddress)
            {
                /* Make one status update transition per update cycle
                 * per default every 20ms
                 */
                statusInformationStateMachine.Execute();
            }
        }

        public new void Interval(double interval)
        {
            // 3 is the number of transistions
            base.Interval(interval / 3);
        }
    };

    public class StatusInformationStateMachine : StateIterator
    {
        private E52138ChipAPI parent;
        
        public StatusInformationStateMachine(E52138ChipAPI parent)
        {
            this.parent = parent;
        }

        public void Close()
        {
            Stop();
            parent = null;
        }

        [Transition]
        public void GetVLED_0_5()
        {
            parent?.ReadVLED(0);
        }

        [Transition]
        public void GetVLED_6_11()
        {
            parent?.ReadVLED(6);
        }

        [Transition]
        public void GetVLED_12_17()
        {
            parent?.ReadVLED(12);
        }
        [Transition]
        public void GetVDIF_0_5()
        {
            parent?.ReadVDIF(0);
        }

        [Transition]
        public void GetVDIF_6_11()
        {
            parent?.ReadVDIF(6);
        }

        [Transition]
        public void GetVDIF_12_17()
        {
            parent?.ReadVDIF(12);
        }

        [Transition]
        public void GetCurrentBoost()
        {
            parent?.GetCurrentBoost();
        }

        [Transition]
        public void GetComperatorBIST()
        {
            parent?.GetComperatorBIST();
        }

        [Transition]
        public void GetLEDShort()
        {
            parent?.GetLEDShort();
        }

        [Transition]
        public void GetLEDOpen()
        {
            parent?.GetLEDOpen();
        }
    };

    public class DeviceInformationStateMachine : StateIterator
    {
        private E52138ChipAPI parent;

        public DeviceInformationStateMachine(E52138ChipAPI parent, double runTimer = 0.0) : base(runTimer)
        {
            this.parent = parent;
        }

        public void Close()
        {
            Stop();
            parent = null;
        }

        [Transition]
        public void GetFWVersion()
        {
            parent?.GetFWVersion();
        }

        [Transition]
        public void GetFWVariant()
        {
            parent?.GetFWVariant();
        }

        [Transition]
        public void GetHWVersion()
        {
            parent?.GetHWVersion();
        }

        [Transition]
        public void GetDeviceAddress()
        {
            parent?.GetDeviceAddress();
        }

        [Transition]
        public void IsMultiPlexingActive()
        {
            parent?.IsMultiplexingActive();
        }
    };

    public partial class E52138ChipAPI : INotifyPropertyChanged, IDisposable
    {
        private bool isDisposed = false;
        private bool hwRevisionErrorLogged;

        public class ErrorEntry
        {
            public ErrorEntry(DateTime time, string cmd, int code, string msg)
            {
                this.datetime = time;
                this.command = cmd;
                this.code = code.ToString();
                this.interpretation = msg;
            }
            public DateTime datetime;
            public string command;
            public string code;
            public string interpretation;
        }
        private List<ErrorEntry> errorLog;

        private void ReportError(string cmd, int code, string msg)
        {
            // Limit log length to 200 entries
            if (errorLog.Count >= 200)
            {
                errorLog.RemoveAt(0);
            }

            errorLog.Add(new ErrorEntry(DateTime.Now, cmd, code, msg));
            OnPropertyChanged("errorLog");
        }

        public void ExportCSVErrorLog(Stream saveFile)
        {
            string line;
            StreamWriter saveFileWriter = new StreamWriter(saveFile);

            line = "Date;Time;Error Code;Action;Error Interpretation\n";
            saveFile.Write(Encoding.UTF8.GetBytes(line), 0, line.Length);

            foreach (ErrorEntry error in errorLog)
            {
                line =  error.datetime.ToLongDateString() + ";" +
                        error.datetime.ToLongTimeString() + ";" +
                        error.code + ";" +
                        error.command + ";" +
                        error.interpretation + "\n";
                saveFile.Write(Encoding.UTF8.GetBytes(line), 0, line.Length);
            }
            
        }

         public class ComperatorBISTStatusType
        {
            public bool VDDD_uv = false;
            public bool VDDD_ov = false;
            public bool VDDA_uv = false;
            public bool VDDA_ov = false;
            public bool VS_uv = false;
            public bool VS_ov = false;
            public bool IREF_vbg1_err = false;
            public bool IREF_vbg2_err = false;
            public bool IREF_low = false;
            public bool IREF_high = false;
            public bool OVT = false;
            public bool VS_crit = false;
        }
        private ComperatorBISTStatusType comperatorBISTStatus;
        public ComperatorBISTStatusType ComperatorBISTStatus { get => comperatorBISTStatus; set { comperatorBISTStatus = value; OnPropertyChanged("comperatorBISTStatus"); } }
        public LEDStatusDataTable LEDStatusTable;
        private int[] pWMValues;
        public int[] PWMValues {
            get { return pWMValues; }
            set {
                OverrideAndTrigger(ref pWMValues, value, "pWMValues");
                if (data != null && data.SendPWM) writePWMValues = true;
            }
        }
        private bool[] boostStatus;
        public bool[] BoostStatus {
            get { return boostStatus; }
            set {
                boostStatus = value; OnPropertyChanged("BoostStatus");
            }
        }
        private bool[] boostEnable;
        public bool[] BoostEnable {
            get { return boostEnable; }
            set {
                OverrideAndTrigger(ref boostEnable, value, "boostValues");
                if (data != null && data.SendPWM) writeBoostValues = true;
            }
        }
        private bool[] shortStatus;
        public bool[] ShortStatus { get { return shortStatus; } set { shortStatus = value; UpdateStatusTable(); OnPropertyChanged("shortStatus"); } }
        private bool[] openStatus;
        public bool[] OpenStatus { get { return openStatus; } set { openStatus = value; UpdateStatusTable(); OnPropertyChanged("openStatus"); } }
        private bool anyShort;
        public bool AnyShort { get { return anyShort; } set { anyShort = value; OnPropertyChanged("anyShort"); } }
        private bool anyOpen;
        public bool AnyOpen { get { return anyOpen; } set { anyOpen = value; OnPropertyChanged("anyOpen"); } }


        private void UpdateStatusTable()
        {
            bool anyShort = false;
            bool anyOpen = false;

            for (int i = 0; i < Properties.Settings.Default.Channels; i++)
            {
                if (shortStatus[i] == true)
                {
                    LEDStatusTable[i]["Status"] = "short";
                    anyShort = true;
                }
                else if (openStatus[i] == true)
                {
                    LEDStatusTable[i]["Status"] = "open";
                }
                else
                {
                    LEDStatusTable[i]["Status"] = "ok";
                }
                if (openStatus[i] == true)
                {
                    anyOpen = true;
                }
            }

            AnyShort = anyShort;
            AnyOpen = anyOpen;
        }

        private void OverrideAndTrigger<T>(ref T[] a, T[] b, string property) where T : IComparable
        {
            bool write = false;
            if (a == null)
            {
                a = b;
                OnPropertyChanged(property);
            }
            else
            {
                for (int i = 0; i < Math.Min(a.Length, b.Length); i++)
                {
                    if (!EqualityComparer<T>.Default.Equals(a[i], b[i]))
                    {
                        write = true;
                        break;
                    }
                }
            }

            if (write)
            {
                b.CopyTo(a, 0);
                OnPropertyChanged(property);
            }
            
        }
        private void OverrideAndTrigger<T>(ref T a, T b, string property) where T : IComparable
        {
            if (!EqualityComparer<T>.Default.Equals(a, b))
            {
                a = b;
                OnPropertyChanged(property);
            }
        }

        private bool multiplexing = false;
        public bool Multiplexing { get => multiplexing; set { OverrideAndTrigger(ref multiplexing, value, "multiplexing"); } }

        private int deviceAddress = 1;
        public int DeviceAddress { get => deviceAddress; set { OverrideAndTrigger(ref deviceAddress, value, "deviceInfo"); } }

        private string hardwareVersion = "";
        public string HardwareVersion { get => hardwareVersion; set { OverrideAndTrigger(ref hardwareVersion, value, "deviceInfo"); } }
        public int HardwareRevision { get; set; }
        public int HardwarePatch { get; set; }

        private string firmwareVersion = "";
        public string FirmwareVersion { get => firmwareVersion; set { OverrideAndTrigger(ref firmwareVersion, value, "deviceInfo"); } }

        private string firmwareVariant = "";
        public string FirmwareVariant { get => firmwareVariant; set { OverrideAndTrigger(ref firmwareVariant, value, "deviceInfo"); } }

        private bool writePWMValues = false;
        public bool WritePWMValues { get => writePWMValues; set { writePWMValues = value; OnPropertyChanged("writePWMValues"); } }

        private bool writeBoostValues = false;
        public bool WriteBoostValues { get => writeBoostValues; set { writeBoostValues = value; OnPropertyChanged("writeBoostValues"); } }

        private bool readPWMValues;
        public bool ReadPWMValues { get => readPWMValues; set { readPWMValues = value; OnPropertyChanged("readPWMValues"); } }

        public int[,] Matrix;


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public enum ComMode
        {
            Off,
            DeviceInfo,
            Started
        }
        private ComMode comMode;

        private dynamic dut;
        private ApplicationData data;

        private DeviceInformationStateMachine deviceInformationStateMachine;
        private CommunicationStateMachine communicationStateMachine;

        public E52138ChipAPI(ApplicationData data, int deviceAddress)
        {
            this.data = data;
            dut = data.E52138py(api: data.API, device_address: deviceAddress);

            InitializeData();

            data.PropertyChanged += DataChanged;
            this.deviceAddress = deviceAddress;
            
            // initialize state machines
            communicationStateMachine = new CommunicationStateMachine(this, data, data.SendPWMUpdateInterval);
            deviceInformationStateMachine = new DeviceInformationStateMachine(this, 5000);

            this.GetFWVariant();
            comMode = ComMode.DeviceInfo;
        }

        ~E52138ChipAPI()
        {
            Dispose(false);
            Console.WriteLine("Chip " + deviceAddress + " removed");
        }

        private void InitializeData()
        {
            // initialize failure log
            errorLog = new List<ErrorEntry>();
            hwRevisionErrorLogged = false;

            // initialize data
            comperatorBISTStatus = new ComperatorBISTStatusType();
            LEDStatusTable = new LEDStatusDataTable();
            PWMValues = new int[Properties.Settings.Default.Channels];
            boostStatus = new bool[Properties.Settings.Default.Channels];
            BoostEnable = new bool[Properties.Settings.Default.Channels];
            shortStatus = new bool[Properties.Settings.Default.Channels];
            openStatus = new bool[Properties.Settings.Default.Channels];

            Matrix = new int[6, 3];
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 3; j++)
                    Matrix[i, j] = -1;
        }

        public void Close()
        {
            data.PropertyChanged -= DataChanged;
            SetMode(ComMode.Off);

            communicationStateMachine.Close();
            deviceInformationStateMachine.Close();
        }
 
        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                LEDStatusTable.Dispose();
                communicationStateMachine.Dispose();
                deviceInformationStateMachine.Dispose();
                // dispose managed resources
                Close();
            }

            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private static void ApiExceptionWrapper(Action f)
        {
            try
            {
                f();
            }
            catch (System.Exception e) when (e.Message.StartsWith("Transmission") || e.Message.StartsWith("No API"))
            {
                Console.WriteLine(e.Message);
            }
        }

        private void DataChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "sendPWMUpdateInterval")
            {
                communicationStateMachine.Interval(data.SendPWMUpdateInterval);
            }
            else if (e.PropertyName == "sendPWM")
            {
                if (data.SendPWM)
                {
                    writePWMValues = true;
                    writeBoostValues = true;
                }
            }
            else if (e.PropertyName == "aPI")
            {
                dut.set_api(data.API);
                SetMode(comMode);
            }
        }

        public void SetDiagnoseMode(bool value, bool allChips = false)
        {
            if (allChips)
            {
                foreach (E52138ChipAPI chip in data.chips.Values)
                {
                    chip.SetDiagnoseMode(value);
                }
            }
            else
            {
                dut.diag = value;
            }
        }

        public void SetMode(ComMode mode)
        {
            comMode = mode;
            if (data.API != null)
            {
                switch (comMode)
                {
                    case ComMode.Started:
                        communicationStateMachine.Start();
                        deviceInformationStateMachine.Start();
                        break;
                    case ComMode.DeviceInfo:
                        communicationStateMachine.Stop();
                        deviceInformationStateMachine.Start();
                        break;
                    default:
                        communicationStateMachine.Stop();
                        deviceInformationStateMachine.Stop();
                        break;
                }
            }
            else
            {
                communicationStateMachine.Stop();
                deviceInformationStateMachine.Stop();
            }
        }

        public void SetDeviceAddress(int address)
        {
            communicationStateMachine.Stop();
            deviceInformationStateMachine.Stop();

            // DeviceAddress = DeviceAddress = string.Format("{0:000}", address);
            firmwareVersion = "";
            firmwareVariant = "";
            hardwareVersion = "";
            deviceAddress = address;
            dut.device_address = address;

            communicationStateMachine.Start();
            deviceInformationStateMachine.Start();

        }

        public void SendRaw(byte[] payload)
        {
            if (!(payload.Length >= 1)) return;

            ApiExceptionWrapper(() =>
            {
                IronPython.Runtime.Bytes body = new IronPython.Runtime.Bytes(payload.Skip(1).ToList());
                
                int command = payload[0];
                dut._send_publish(command, body, diagnose: true);
            });
        }

        public void ReadBytes(int number)
        {
            ApiExceptionWrapper(() =>
            {
                dut._subscribe(number, diagnose: true);
            });
        }

        public void UpdatePWMvalues()
        {
            if (ReadPWMValues)
            {
                IronPython.Runtime.List readPWMValues = null;

                if (Multiplexing)
                {
                    ApiExceptionWrapper(() =>
                    {
                        readPWMValues = dut.get_pwm_channel(0, Properties.Settings.Default.Channels);
                    });
                }
                else
                {
                    ApiExceptionWrapper(() =>
                    {
                        readPWMValues = dut.get_pwm_channel(0, Properties.Settings.Default.StandardChannels);
                    });
                }
                if (readPWMValues != null)
                {
                    OverrideAndTrigger<int>(ref pWMValues, readPWMValues.Cast<int>().ToArray(), "pWMValues");
                    ReadPWMValues = false;
                }
            }
            // Write values (automatically or once)
            else if (WritePWMValues)
            {
                if (Multiplexing)
                {
                    ApiExceptionWrapper(() =>
                    {
                        IronPython.Runtime.List pWMValuesPy = new IronPython.Runtime.List();
                        for (int i = 0; i < Properties.Settings.Default.Channels / 2; i++)
                            pWMValuesPy.Add(PWMValues[i]);

                        dut.send_pwm_channel(0, pWMValuesPy);
                        pWMValuesPy.Clear();
                        for (int i = Properties.Settings.Default.Channels / 2; i < Properties.Settings.Default.Channels; i++)
                            pWMValuesPy.Add(PWMValues[i]);

                        dut.send_pwm_channel(Properties.Settings.Default.Channels / 2, pWMValuesPy);
                    });
                }
                else
                {
                    ApiExceptionWrapper(() =>
                    {
                        IronPython.Runtime.List pWMValuesPy = new IronPython.Runtime.List();
                        for (int i = 0; i < Properties.Settings.Default.StandardChannels; i++)
                            pWMValuesPy.Add(PWMValues[i]);

                        dut.send_pwm_channel(0, pWMValuesPy);
                    });
                }

                WritePWMValues = false;
            }
        }

        public void ReadVLED(int channel)
        {
            ApiExceptionWrapper(() =>
            {
                IronPython.Runtime.PythonTuple getVledValues = dut.get_vled(channel, 6, true);
                IronPython.Runtime.List vledValues = (IronPython.Runtime.List) getVledValues[0];
                IronPython.Runtime.List vledValidFlags = (IronPython.Runtime.List) getVledValues[1];
                
                for (int value = 0; value < Math.Min(vledValues.Count(), LEDStatusTable.Rows.Count - channel); value++)
                {
                    LEDStatusTable.Rows[value + channel]["VLED [V]"] = vledValues[value];

                    if (vledValidFlags[value].Equals(true))
                    {
                        LEDStatusTable.Rows[value + channel]["vled_valid"] = true;
                    }
                    else
                    {
                        LEDStatusTable.Rows[value + channel]["vled_valid"] = false;
                    }
                }
            });
        }

        public void ReadVDIF(int channel)
        {
            ApiExceptionWrapper(() =>
            {
                IronPython.Runtime.PythonTuple getVdifValues = dut.get_vdif(channel, 6, true);
                IronPython.Runtime.List vdifValues = (IronPython.Runtime.List)getVdifValues[0];
                IronPython.Runtime.List vdifValidFlags = (IronPython.Runtime.List)getVdifValues[1];

                for (int value = 0; value < Math.Min(vdifValues.Count(), LEDStatusTable.Rows.Count - channel); value++)
                {
                    LEDStatusTable.Rows[value + channel]["VDIF [V]"] = vdifValues[value];

                    // VLED and VDIF status updates are deactivated in this version
                    //if (vdifValidFlags[value].Equals(true))
                    //{
                    //    LEDStatusTable.Rows[value + channel]["vdif_valid"] = true;
                    //}
                    //else
                    //{
                    //    LEDStatusTable.Rows[value + channel]["vdif_valid"] = false;
                    //}
                }
            });
        }

        private double meanOf5outof6(double[] measurements)
        {
            if (measurements.Length < 6)
                return 0.0;

            double sum = 0.0;
            for (int i = 0; i < 6; i++)
            {
                sum += measurements[i];
            }
            double avg = sum / 6.0;

            int idx = 0;
            double maxdiff = 0.0;
            for (int i = 0; i < 6; i++)
            {
                if (Math.Abs(measurements[i] - avg) < maxdiff)
                {
                    maxdiff = Math.Abs(measurements[i] - avg);
                    idx = i;
                }
            }

            sum = 0.0;
            for (int i = 0; i < 6; i++)
            {
                if (i != idx)
                    sum += measurements[i];
            }
            avg = sum / 5.0;

            return avg;
        }

        public void SetCurrentBoost()
        {
            if (writeBoostValues)
            {
                ApiExceptionWrapper(() =>
                {
                    dut.set_current_boost(BoostEnable);
                });

                writeBoostValues = false;
            }
            
        }
        public void GetCurrentBoost()
        {
            ApiExceptionWrapper(() =>
            {
                IronPython.Runtime.List currentBoostRaw = dut.get_current_boost();
                int[] currentBoostInt = currentBoostRaw.Cast<int>().ToArray();
                
                for (int i = 0; i < BoostStatus.Count(); i++)
                {
                    boostStatus[i] = currentBoostInt[i] == 1;
                }
                OnPropertyChanged("boostStatus");
            });
        }

        public void GetComperatorBIST()
        {
            ApiExceptionWrapper(() =>
            {
                var BISTStatus = ComperatorBISTStatus;
                dynamic comperatorBISTresults = dut.get_comperator_bist_results();
                BISTStatus.VDDA_uv = 1 == comperatorBISTresults.vdda_uv;
                BISTStatus.VDDA_ov = 1 == comperatorBISTresults.vdda_ov;
                BISTStatus.VDDD_uv = 1 == comperatorBISTresults.vddd_uv;
                BISTStatus.VDDD_ov = 1 == comperatorBISTresults.vddd_ov;
                BISTStatus.VS_uv = 1 == comperatorBISTresults.vs_uv;
                BISTStatus.VS_ov = 1 == comperatorBISTresults.vs_ov;
                BISTStatus.IREF_vbg1_err = 1 == comperatorBISTresults.iref_vbg1_err;
                BISTStatus.IREF_vbg2_err = 1 == comperatorBISTresults.iref_vbg2_err;
                BISTStatus.IREF_low = 1 == comperatorBISTresults.iref_low;
                BISTStatus.IREF_high = 1 == comperatorBISTresults.iref_high;
                BISTStatus.OVT = 1 == comperatorBISTresults.ovt;
                BISTStatus.VS_crit = 1 == comperatorBISTresults.vs_crit;

                ComperatorBISTStatus = BISTStatus;
            });
        }

        public void GetLEDShort()
        {
            ApiExceptionWrapper(() =>
            {
                dynamic shortStatusResult = dut.get_short_status().to_list();
                bool[] shortStatus = new bool[Properties.Settings.Default.Channels];
                for (int i = 0; i < shortStatus.Count(); i++)
                {
                    shortStatus[i] = shortStatusResult[i] == 1;
                }

                ShortStatus = shortStatus;
            });            
        }

        public void GetLEDOpen()
        {
            ApiExceptionWrapper(() =>
            {
                dynamic openStatusResult = dut.get_open_status().to_list();
                bool[] openStatus = new bool[Properties.Settings.Default.Channels];
                for (int i = 0; i < openStatus.Count(); i++)
                {
                    openStatus[i] = openStatusResult[i] == 1;
                }

                OpenStatus = openStatus;
            });
        }

        public void GetFWVersion()
        {
            ApiExceptionWrapper(() =>
            {
                dynamic x = dut.get_fw_version();
                FirmwareVersion = x;
            });
        }

        public void GetFWVariant()
        {
            ApiExceptionWrapper(() =>
            {
                FirmwareVariant = dut.get_fw_variant();
            });
        }

        public int GetHWVersion()
        {
            int r19 = 0;

            ApiExceptionWrapper(() =>
            {
                int hwRevision = dut.get_HW_Revision();
                int hwPatch = dut.get_HW_Patch();

                dynamic hwVersionResult = dut.get_hw_version();
                r19 = hwVersionResult[2];

                if (hwRevision < 0 || hwPatch < 0)
                {
                    if (!hwRevisionErrorLogged)
                    {
                        if (hwRevision == -8 || hwPatch == -8)
                        {
                            ReportError("Get HW Version", -8, dut.decode_guard_error(-8) + " (R19=" + r19.ToString() + " Ohm) (Error source GUI)");
                            hwRevisionErrorLogged = true;
                        }
                        else
                        {
                            ReportError("Get HW Version", hwRevision, dut.decode_guard_error(hwRevision) + " (R19=" + r19.ToString() + " Ohm) (Error source FW)");
                            hwRevisionErrorLogged = true;
                        }
                    }


                    string hwVersion;
                    if (0 < r19 && r19 < 5)
                    {
                        HardwareRevision = 1;
                        HardwarePatch = 0;
                        hwVersion = "v1.0/v1.1 (invalid)";
                    }
                    else if (42 < r19 && r19 < 52)
                    {
                        HardwareRevision = 1;
                        HardwarePatch = 2;
                        hwVersion = "v1.2 (invalid)";
                    }
                    else if (57 < r19 && r19 < 67)
                    {
                        HardwareRevision = 2;
                        HardwarePatch = 1;
                        hwVersion = "v2.1 (invalid)";
                    }
                    else if (90 < r19 && r19 < 110)
                    {
                        HardwareRevision = 2;
                        HardwarePatch = 2;
                        hwVersion = "v2.2 (invalid)";
                    }
                    else
                    {
                        HardwareRevision = 0;
                        HardwarePatch = 0;
                        hwVersion = "unsupported";
                    }

                    HardwareVersion = hwVersion;
                }
                else
                {
                    HardwareRevision = hwRevision;
                    HardwarePatch = hwPatch;
                    HardwareVersion = "v" + hwRevision.ToString() + "." + hwPatch.ToString() + " (valid)";
                }
            });

            return r19;
        }

        public void GetDeviceAddress()
        {
            ApiExceptionWrapper(() =>
            {
                int address = dut.device_address;
                DeviceAddress = address;
            });
        }

        public void SendPatchMatrix()
        {
            ApiExceptionWrapper(() =>
            {
                IronPython.Runtime.List channelMatrices = new IronPython.Runtime.List();
                for (int i = 0; i < 3; i++)
                {
                    channelMatrices.Add(dut.ChannelMatrix(i, Matrix[i, 0], Matrix[i, 1], Matrix[i, 2]));
                }
                dut.set_channel_matrix(channelMatrices);

                channelMatrices.Clear();
                for (int i = 3; i < 6; i++)
                {
                    channelMatrices.Add(dut.ChannelMatrix(i, Matrix[i, 0], Matrix[i, 1], Matrix[i, 2]));
                }
                dut.set_channel_matrix(channelMatrices);
            });
        }

        public void IsMultiplexingActive()
        {
            ApiExceptionWrapper(() =>
            {
                Multiplexing = (bool) dut.is_multiplexing_active();
            });
        }
    }
}
