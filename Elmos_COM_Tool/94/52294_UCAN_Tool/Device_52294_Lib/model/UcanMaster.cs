using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.IO;
using System.Threading;
using System.Diagnostics;

using FtdiLib;
using Extensions;
using UcanCommLib;

namespace Device_52294_Lib
{
    public class UcanMaster
    {
        public delegate void NodeAddrChangeCallback(byte devId, byte addr);
        public delegate void AutoNodeEnabledChangeCallback(bool enabled);

        public const ushort DEFAULT_AUTO_INTERVAL_MS = 10;
        public const ushort MAX_THREAD_EXEC_TIME_SAMPLES = 16;

        // ---------------------------------------------------------------------------
        // VARIABLES WITH LOCK

        static readonly object _token = new object();

        private UcanComm _ucanComm;

        private UcanCommParameters _commParameters;
        private List<CommDevice> _commDevices;
        private CommBroadcast _commBroadcast;

        private bool _autoNodeEnabled;
        private byte _autoNodeId;
        private ushort _autoNodeWriteAddr;
        private ushort _autoNodeWriteWords;
        private ushort _autoNodeWriteData;
        private ushort _autoNodeVerifyAddr;
        private ushort _autoNodeVerifyWords;
        private ushort _autoNodeVerifyMask;
        private ushort _autoNodeVerifyExpected;

        private ushort _autoIntervalMs;
        private bool _autoWritePwm;
        private bool _autoWriteCurrents;

        private bool _autoReadDiagStatus;
        private bool _autoReadFullStatus;
        private byte _autoReadDeviceId;

        private bool _autoImmPulse0All;
        private bool _autoImmCurrent0All;
        private bool _autoImmEnablesAll;

        private bool _animationEnabled;
        private int _animationDelay;
        private MemoryStream _animationMemory;
        private StreamReader _animationReader;

        private ushort _threadExecTimesMeanMs;
        private ushort _threadExecTimesMaxMs;

        // ---------------------------------------------------------------------------
        // VARIABLES WITHOUT LOCK OR COPIES

        private Thread _thread;
        private Queue<ushort> _threadExecTimesMsNoLock;
        private bool _lastPwmWrittenNoLock;
        private byte _autoReadCycleNoLock;

        private NodeAddrChangeCallback _nodeAddrChangeCallback;
        private AutoNodeEnabledChangeCallback _autoNodeEnabledChangeCallback;

        // ---------------------------------------------------------------------------

        public UcanComm ucanCommRef
        {
            get { return _ucanComm; }
        }

        public NodeAddrChangeCallback nodeAddrChangeCallback
        {
            set { _nodeAddrChangeCallback = value; }
        }

        public AutoNodeEnabledChangeCallback autoNodeEnabledChangeCallback
        {
            set { _autoNodeEnabledChangeCallback = value; }
        }


        #region "Comm"

        public List<FtdiBase.DeviceListEntry> GetUCanCommDevices()
        {
            lock (_token) { return _ucanComm.GetDevices(); }
        }

        public bool GetConnected(){
            lock (_token) { return _ucanComm.Connected(); }
        }

        public void OpenChannelByDevice(FtdiBase.DeviceListEntry device, uint channel = 0)
        {
            lock (_token)
            {
                _ucanComm.OpenChannelByDevice(device, channel);
            }
        }

        public void OpenChannelBySerialNumber(String serialNumber, uint channel = 0)
        {
            lock (_token)
            {
                _ucanComm.OpenChannelBySerialNumber(serialNumber, channel);
            }
        }

        public void Close()
        {
            lock (_token)
            {
                    _ucanComm.Close();
            }
        }

        public double GetCommBreakLength()
        {
            lock (_token) { return _ucanComm.GetBreakLength(); }
        }

        public void SetCommBreakLength(double breakLength)
        {
            lock (_token) { _ucanComm.SetBreakLength(breakLength); }
        }

        public uint GetCommWakeupLength()
        {
            lock (_token) { return _ucanComm.GetWakeupLength(); }
        }

        public void SetCommWakeupLength(uint wakeupLength)
        {
            lock (_token) { _ucanComm.SetWakeupLength(wakeupLength); }
        }

        public uint GetCommBitrate()
        {
            lock (_token) { return _ucanComm.GetBitrate(); }
        }

        public void SetCommBitrate(uint bitrate)
        {
            lock (_token) { _ucanComm.SetBitrate(bitrate); }
        }

        public uint GetCommParity()
        {
            lock (_token) { return _ucanComm.GetParity(); }
        }

        public void SetCommParity(uint parity)
        {
            lock (_token) { _ucanComm.SetParity(parity); }
        }

        public void SetCommSync(uint sync)
        {
            lock (_token) { _commParameters.sync = sync; }
        }

        public void SetCommHeader(uint header)
        {
            lock (_token) { _commParameters.header = header; }
        }

        public void SendWakeup(bool symbol = true, bool ack = true)
        {
            lock (_token) {
                if (symbol)
                {
                    _ucanComm.SendWakeup();
                    Thread.Sleep(25);
                }
                if (ack)
                {
                    _commBroadcast.SendImmCommandWakeupAck();
                }
            }
        }

        public void SetBreak()
        {
            lock (_token)
            {
                _ucanComm.SetBreak();
            }
        }

        public void ClearBreak()
        {
            lock (_token)
            {
                _ucanComm.ClearBreak();
            }
        }

        public void SendSleepBroadcast()
        {
            lock (_token) { _commBroadcast.SendImmCommandSleep();}
        }

        public UcanCommParameters GetUcanCommParametersCopy(){
            lock (_token) { return _commParameters.Copy(); }
        }

        public byte GetCommLastLiveCounter()
        {
            lock (_token) { return _ucanComm.GetLastLiveCounter(); }
        }

        #endregion


        #region "Comm Debug Error Injection"

        public void SetCommDebugHeaderCrcError(bool enable)
        {
            lock (_token) { _commParameters.debugHeaderCrcError = enable; }
        }

        public bool GetCommDebugHeaderCrcError()
        {
            lock (_token) { return _commParameters.debugHeaderCrcError; }
        }

        public void SetCommDebugWriteCrcError(bool enable)
        {
            lock (_token) { _commParameters.debugWriteCrcError = enable; }
        }

        public bool GetCommDebugWriteCrcError()
        {
            lock (_token) { return _commParameters.debugWriteCrcError; }
        }

        public void SetCommDebugUseLastLiveCounter(bool enable)
        {
            lock (_token) { _commParameters.debugUseLastLiveCounter = enable; }
        }

        public bool GetCommDebugUseLastLiveCounter()
        {
            lock (_token) { return _commParameters.debugUseLastLiveCounter; }
        }

        #endregion

        #region "Device Handling"

        public byte AddDevice()
        {
            lock (_token)
            {
                byte ret = (byte)_commDevices.Count;
                _commDevices.Add(new CommDevice(_ucanComm, _commParameters));
                return ret;
            }
        }

        public byte GetDeviceNode(byte deviceId)
        {
            lock (_token) { return _commDevices[deviceId].node; }
        }

        public void SetDeviceNode(byte deviceId, byte value)
        {
            lock (_token) { _commDevices[deviceId].node = value; }
            if (_nodeAddrChangeCallback != null) _nodeAddrChangeCallback(deviceId, value);
        }

        #endregion

        #region "Automatic"

        public bool GetAutoNodeEnabled()
        {
            lock (_token) { return _autoNodeEnabled; }
        }

        public void SetAutoNodeEnabled(bool enabled)
        {
            lock (_token) { _autoNodeEnabled = enabled; }
            if (_autoNodeEnabledChangeCallback != null) _autoNodeEnabledChangeCallback(enabled);
        }

        public void SetAutoNodeId(byte id){
            lock (_token) { _autoNodeId = id; }
        }

        public void SetAutoNodeWrite(ushort addr, ushort words, ushort data)
        {
            lock (_token) {
                _autoNodeWriteAddr = addr;
                _autoNodeWriteWords = words;
                _autoNodeWriteData = data;
            }
        }

        public void SetAutoNodeVerify(ushort addr, ushort words, ushort mask, ushort expected)
        {
            lock (_token)
            {
                _autoNodeVerifyAddr = addr;
                _autoNodeVerifyWords = words;
                _autoNodeVerifyMask = mask;
                _autoNodeVerifyExpected = expected;
            }
        }


        public void SetAutoWriteCurrents(bool value)
        {
            lock (_token) { _autoWriteCurrents = value; }
        }

        public void SetAutoWritePWM(bool value)
        {
            lock (_token) { _autoWritePwm = value; }
        }

        public ushort GetAutoWriteIntervalMs()
        {
            lock (_token) { return _autoIntervalMs; }
        }

        public void SetAutoWriteIntervalMs(ushort value)
        {
            lock (_token)
            {
                _autoIntervalMs = value;
            }
        }

        public void SetAutoReadFullStatus(bool value)
        {
            lock (_token) { 
                _autoReadFullStatus = value;
                _autoReadCycleNoLock = 0;
            }
        }

        public void SetAutoReadDiagStatus(bool value)
        {
            lock (_token)
            {
                _autoReadDiagStatus = value;
                _autoReadCycleNoLock = 0;
            }
        }

        public void SelectAutoReadDevice(byte deviceId)
        {
            lock (_token) { _autoReadDeviceId = deviceId; }
        }

        public void SetAutoImmPulse0All(bool value)
        {
            lock (_token)
            {
                _autoImmPulse0All = value;
            }
        }

        public void SetAutoImmCurrent0All(bool value)
        {
            lock (_token)
            {
                _autoImmCurrent0All = value;
            }
        }

        public void SetAutoImmEnablesAll(bool value)
        {
            lock (_token)
            {
                _autoImmEnablesAll = value;
            }
        }

        #endregion

        #region "Animation"

        private void _ResetAnimationInsideLock()
        {
            _animationDelay = 0;
            _animationReader.DiscardBufferedData();
            _animationReader.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
        }

        public void SetAnimationEnabled(bool value)
        {
            lock (_token) {
                if (value)
                {
                    _ResetAnimationInsideLock();
                    _animationEnabled = true;
                }
                else
                {
                    _animationEnabled = false;
                }
            }
        }

        public bool GetAnimationEnabled()
        {
            lock (_token) { return _animationEnabled; }
        }

        public void ReadAnimationFile(string fileName)
        {
            lock (_token)
            {
                _animationEnabled = false;
                _animationMemory.SetLength(0);
            }

            // copy to memory
            using (FileStream fs = File.OpenRead(fileName))
            {
                fs.CopyTo(_animationMemory);
            }

            // create memory reader
            _animationReader = new StreamReader(_animationMemory);
        }

        #endregion

        #region "Device Access: BusConfig"

        public void SetDevicePulses(byte deviceId, ushort[] pulses)
        {
            lock (_token)
            {
                for (byte i = 0; i < pulses.Length; i += 1)
                {
                    _commDevices[deviceId].deviceRef.busConfig.SetPulse(i, pulses[i]);
                }
            }
        }

        public void SetDeviceCurrents(byte deviceId, ushort[] currents)
        {
            lock (_token)
            {
                for (byte i = 0; i < currents.Length; i += 1)
                {
                    _commDevices[deviceId].deviceRef.busConfig.SetCurrent(i, currents[i]);
                }
            }
        }

        public BusConfig ReadBusConfigCopy(byte deviceId)
        {
            lock (_token)
            {
                _commDevices[deviceId].ReadBusConfig();
                return _commDevices[deviceId].deviceRef.busConfig.Copy();
            }
        }

        public void WriteBusConfig(byte deviceId, BusConfig busConfig)
        {
            lock (_token)
            {
                _commDevices[deviceId].deviceRef.busConfig = busConfig.Copy();
                _commDevices[deviceId].WriteBusConfig();
            }
        }

        public void WriteBusConfigPwmData(byte deviceId)
        {
            lock (_token) { _commDevices[deviceId].WriteBusConfigPwmData(); }
        }

        public void WriteBusConfigCurrentsData(byte deviceId)
        {
            lock (_token) { _commDevices[deviceId].WriteBusConfigCurrentsData(); }
        }

        public void SendImmCommandReset(byte deviceId)
        {
            lock (_token) { _commDevices[deviceId].SendImmCommandReset(); }
        }

        public void SendImmCommandLedEnable(byte deviceId)
        {
            lock (_token) { _commDevices[deviceId].SendImmCommandLedEnable(); }
        }

        #endregion

        #region "Device Access: Bus Status"

        public BusStatus ReadBusStatusCopy(byte deviceId)
        {
            lock (_token)
            {
                _commDevices[deviceId].ReadBusStatus();
                BusStatus copy = _commDevices[deviceId].deviceRef.busStatus.Copy();
                _commDevices[deviceId].deviceRef.busStatus.ClearAllBitfieldBitFlags();
                return copy;
            }
        }

        public BusStatus GetCachedBusStatusCopy(byte deviceId)
        {
            lock (_token) 
            {
                BusStatus copy = _commDevices[deviceId].deviceRef.busStatus.Copy();
                _commDevices[deviceId].deviceRef.busStatus.ClearAllBitfieldBitFlags();
                return copy;
            }
        }

        #endregion

        #region "Device Access: Standalone"

        public Standalone ReadDeviceInfoCopy(byte deviceId)
        {
            lock (_token)
            {
                _commDevices[deviceId].ReadDeviceInfo();
                return _commDevices[deviceId].deviceRef.parameters.standalone.Copy();
            }
        }

        public DeviceParameters ReadParametersCopy(byte deviceId)
        {
            lock (_token)
            {
                _commDevices[deviceId].ReadParameters();
                return _commDevices[deviceId].deviceRef.parameters.Copy();
            }
        }

        public BoolString WriteParameters(byte deviceId, DeviceParameters parameters)
        {
            lock (_token)
            {
                _commDevices[deviceId].deviceRef.parameters = parameters.Copy();
                BoolString ret = _commDevices[deviceId].WriteParameters();
                return ret;
            }
        }

        public bool SetSramSel(byte deviceId, bool sram_otp_n)
        {
            lock (_token)
            {
                return _commDevices[deviceId].SetSramSel(sram_otp_n);
            }
        }

        #endregion

        #region "Device Access"

        public bool WriteData(byte deviceId, ushort addr, ushort[] data)
        {
            lock (_token) { return _commDevices[deviceId].WriteDataHandleCommError(addr, data); }
        }

        public bool ReadData(byte deviceId, ushort addr, ref ushort[] data)
        {
            lock (_token) { return _commDevices[deviceId].ReadDataHandleCommError(addr, ref data); }
        }

        public ushort[] VerifyData(byte deviceId, ushort addr, ushort words, ushort mask, ushort expected)
        {
            lock (_token) { return _commDevices[deviceId].VerifyData(addr, words, mask, expected); }
        }

        public Device GetDeviceCopy(byte deviceId)
        {
            lock (_token) { return _commDevices[deviceId].deviceRef.Copy(); }
        }

        public bool GetDeviceCommError(byte deviceId)
        {
            lock (_token)
            {
                bool ret = _commDevices[deviceId].commError.GetClearWasIs1();
                _commDevices[deviceId].commError.SetValue(false);
                return ret;
            }
        }

        public bool GetDeviceVerifyError(byte deviceId)
        {
            lock (_token)
            {
                bool ret = _commDevices[deviceId].verifyError.GetClearWasIs1();
                _commDevices[deviceId].verifyError.SetValue(false);
                return ret;
            }
        }

        #endregion

        #region "UcanMaster Thread"

        public ushort GetThreadExecTimesMeanMs()
        {
            lock (_token) { return _threadExecTimesMeanMs; }
        }

        public ushort GetThreadExecTimesMaxMs()
        {
            lock (_token) { return _threadExecTimesMaxMs; }
        }

        public void AbortThread()
        {
            _thread.Abort();
        }

        private void ThreadRun()
        {
            Thread.CurrentThread.Name = "Master";

            while (true)
            {
                Stopwatch stopWatch = Stopwatch.StartNew();
                ushort autoIntervalMsCopy = DEFAULT_AUTO_INTERVAL_MS;
                ushort lockTimeMs = 0;
                ushort execTimeMs = 0;
                // ---------------------------------------------------------------------------
                // DO STUFF
                lock (_token)
                {
                    lockTimeMs = (ushort)stopWatch.Elapsed.Milliseconds;
                    autoIntervalMsCopy = _autoIntervalMs;
                    _ThreadDoAnimationInsideLock();
                    _ThreadDoAutoWriteInsideLock();
                    _ThreadDoAutoReadStatusInsideLock();
                    _ThreadDoAutoNodeInsideLock();
                    _ThreadDoAutoImmInsideLock();

                    execTimeMs = (ushort)stopWatch.Elapsed.Milliseconds;
                }
                // ---------------------------------------------------------------------------

                // Sleep at least some elapsedMs outside the Lock
                Thread.Sleep(1);

                ushort stopTimeMs = (ushort)stopWatch.Elapsed.Milliseconds;
                if (stopTimeMs < autoIntervalMsCopy)
                {
                    ushort diff = (ushort) (autoIntervalMsCopy - stopTimeMs - 1);  // one reserve
                    if (diff > 0)
                        Thread.Sleep(diff);
                }
                ushort endTimeMs = (ushort)stopWatch.Elapsed.Milliseconds;
                stopWatch.Stop();

                // calculation of exec times mean + max
                ushort maxTime = 0;
                ushort meanTime = 0;
                _threadExecTimesMsNoLock.Enqueue(endTimeMs);
                if (_threadExecTimesMsNoLock.Count > MAX_THREAD_EXEC_TIME_SAMPLES)
                    _threadExecTimesMsNoLock.Dequeue();
                if (_threadExecTimesMsNoLock.Count == MAX_THREAD_EXEC_TIME_SAMPLES)
                {
                    foreach (ushort et in _threadExecTimesMsNoLock)
                    {
                        if (et > maxTime) maxTime = et;
                        meanTime += et;
                    }
                    meanTime /= MAX_THREAD_EXEC_TIME_SAMPLES;
                }
                lock (_token)
                {
                    _threadExecTimesMaxMs = maxTime;
                    _threadExecTimesMeanMs = meanTime;
                }
            }
        }

        private void _ThreadDoAnimationInsideLock()
        {
            if ((_animationReader != null) && _animationEnabled && (_animationDelay == 0))
            {
                bool gotDelayStmt = false;
                String line = null;
                do
                {
                    if (_animationReader.EndOfStream)
                    {
                        _ResetAnimationInsideLock();
                    }
                    line = _animationReader.ReadLine();
                    if (line != null)
                    {
                        if (line.Length > 0)
                        {
                            if (line[0] == '#') // new cycle with optional delay
                            {
                                gotDelayStmt = true;
                                String delayStr = line.Substring(1, line.Length - 1);
                                if (delayStr.Length > 0)
                                {
                                    byte delay = 0;
                                    try
                                    {
                                        delay = Convert.ToByte(delayStr);
                                    }
                                    catch { };
                                    if (delay > 1)
                                    {
                                        _animationDelay = delay - 1;
                                    }
                                }

                            }
                            else if (line[0] == '@') // PWM assignment to device
                            {
                                String[] command = line.Split('=');
                                if (command.Length > 1)
                                {
                                    String deviceStr = command[0].Substring(1, command[0].Length - 1);
                                    byte device = 0;
                                    try
                                    {
                                        device = Convert.ToByte(deviceStr);
                                    }
                                    catch { };

                                    if (_commDevices.Count > device)
                                    {
                                        String[] values = command[1].Split(';');
                                        if (values.Length > 15)
                                        {
                                            for (byte led = 0; led < 16; led += 1)
                                            {
                                                byte pwm = 0;
                                                try
                                                {
                                                    pwm = Convert.ToByte(values[led]);
                                                }
                                                catch { };

                                                _commDevices[device].deviceRef.busConfig.SetPulse(led, pwm);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                } while ((line != null) && !gotDelayStmt);
            }

            if (_animationDelay > 0)
            {
                _animationDelay -= 1;
            }
        }

        private void _ThreadDoAutoWriteInsideLock()
        {
            if (_autoWritePwm || _autoWriteCurrents)
            {
                if (_autoWritePwm && (!_lastPwmWrittenNoLock || !_autoWriteCurrents))
                {
                    foreach (CommDevice d in _commDevices) d.WriteBusConfigPwmData(false);
                    _lastPwmWrittenNoLock = true;
                }
                else if (_autoWriteCurrents)
                {
                    foreach (CommDevice d in _commDevices) d.WriteBusConfigCurrentsData(false);
                    _lastPwmWrittenNoLock = false;
                }
                _commBroadcast.SendImmCommandUpdate(false);    // wait handled by timer
            }
        }

        private void _ThreadDoAutoReadStatusInsideLock()
        {
            if (_autoReadFullStatus || _autoReadDiagStatus)
            {
                switch (_autoReadCycleNoLock)
                {
                    case 0: _commDevices[_autoReadDeviceId].ReadBusStatusDiag(); break; 
                    case 1: _commDevices[_autoReadDeviceId].ReadBusStatusVdifs(); break;
                    case 2: _commDevices[_autoReadDeviceId].ReadBusStatusIleds(); break;
                    case 3: _commDevices[_autoReadDeviceId].ReadBusStatusVleds(); break;
                }
                if (_autoReadFullStatus) 
                    _autoReadCycleNoLock = (byte)((_autoReadCycleNoLock + 1) % 4);
                else 
                    _autoReadCycleNoLock = 0;

            }
        }

        private void _ThreadDoAutoNodeInsideLock()
        {
            if (_autoNodeEnabled)
            {
                if (_autoNodeWriteWords > 0)
                {
                    ushort[] wdata = new ushort[_autoNodeWriteWords];
                    for (int i = 0; i < _autoNodeWriteWords; i += 1)
                    {
                        wdata[i] = (ushort)_autoNodeWriteData;
                    }
                    WriteData(_autoNodeId, _autoNodeWriteAddr, wdata);
                }
                if (_autoNodeVerifyWords > 0)
                {
                    VerifyData(_autoNodeId, _autoNodeVerifyAddr, _autoNodeVerifyWords, _autoNodeVerifyMask, _autoNodeVerifyExpected);
                }
            }
        }

        private void _ThreadDoAutoImmInsideLock()
        {
            if (_autoImmPulse0All || _autoImmCurrent0All || _autoImmEnablesAll)
            {
                if (_autoImmPulse0All && _autoImmCurrent0All)
                {
                    foreach (CommDevice d in _commDevices) d.SendImmPulseCurrentAll(d.deviceRef.busConfig.GetPulse(0), d.deviceRef.busConfig.GetCurrent(0), false);
                }
                else if (_autoImmPulse0All)
                {
                    foreach (CommDevice d in _commDevices) d.SendImmPulseAll(d.deviceRef.busConfig.GetPulse(0), false);
                }
                else if (_autoImmCurrent0All)
                {
                    foreach (CommDevice d in _commDevices) d.SendImmCurrentAll(d.deviceRef.busConfig.GetCurrent(0), false);
                }

                if (_autoImmEnablesAll)
                {
                    foreach (CommDevice d in _commDevices) d.SendImmCommandLedEnable(false);
                }

            }
        }

        #endregion

        public UcanMaster()
        {
            _commParameters = new UcanCommParameters();
            _ucanComm = new UcanComm(_commParameters);
            _commDevices = new List<CommDevice>();
            _commBroadcast = new CommBroadcast(_ucanComm, _commParameters);

            // animation Memory
            _animationEnabled = false;
            _animationDelay = 0;
            _animationMemory = new MemoryStream();

            _autoIntervalMs = DEFAULT_AUTO_INTERVAL_MS;

            _lastPwmWrittenNoLock = false;
            _autoWritePwm = false;
            _autoWriteCurrents = false;

            _autoReadDiagStatus = false;
            _autoReadFullStatus = false;
            _autoReadDeviceId = 0;

            _autoImmPulse0All = false;
            _autoImmCurrent0All = false;
            _autoImmEnablesAll = false;

            _threadExecTimesMsNoLock = new Queue<ushort>(MAX_THREAD_EXEC_TIME_SAMPLES);
            _threadExecTimesMeanMs = 0;
            _threadExecTimesMaxMs = 0;

            // start Thread
            _thread = new Thread(new ThreadStart(this.ThreadRun));
            _thread.Start();
        }

    }
}
