using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.IO;
using System.Threading;
using System.Diagnostics;

using Can_Comm_Lib;
using Device_52295_Lib;

namespace _52295_CAN_Tool
{
    internal class Master
    {
        public const ushort DEFAULT_AUTO_INTERVAL_MS = 10;
        public const ushort MIN_AUTO_INTERVAL_MS = 10;
        public const ushort MAX_THREAD_EXEC_TIME_SAMPLES = 16;

        // ---------------------------------------------------------------------------
        // VARIABLES WITH LOCK

        static readonly object _token = new object();

        private List<CommDevice> _commDevices;
        private List<CommDeviceGroup> _commDeviceGroups;

        private CanComm _canComm;
        private CommParameters _commParameters;

        private ushort _autoWriteIntervalMs;
        private bool _autoWritePwm;
        private bool _autoWriteCurrents;

        private bool _autoReadDiagStatus;
        private bool _autoReadFullStatus;
        private byte _autoReadDeviceId;

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

        // ---------------------------------------------------------------------------

        #region "Comm Parameters"

        public CommParameters GetCommParmetersCopy()
        {
            lock (_token) { return _commParameters.Copy(); }
        }

        public void applyCommParameters(CommParameters commParameters)
        {
            lock (_token) { 
                _commParameters.apply(commParameters); 
            }
        }

        #endregion

        #region "Comm"

        public bool GetConnected(){
            lock (_token) { return _canComm.Connected(); }
        }

        public void Open()
        {
            lock (_token)
            {
                if (!_canComm.Connected())
                {
                    _canComm.Open(_commParameters.adapter, _commParameters.bitrate);
                }
            }
        }

        public void Close()
        {
            lock (_token)
            {
                if (_canComm.Connected())
                {
                    _canComm.Close();
                }
            }
        }

        #endregion

        #region "Device Handling"

        public byte AddDevice()
        {
            lock (_token)
            {
                byte ret = (byte)_commDevices.Count;
                _commDevices.Add(new CommDevice(_canComm, _commParameters));
                _UpdateDeviceGroupsInsideLock();
                return ret;
            }
        }

        public void SetDeviceNode(byte deviceId, byte value)
        {
            lock (_token)
            {
                _commDevices[deviceId].canNode = value;
                _UpdateDeviceGroupsInsideLock();
            }
        }

        public void SetDeviceGroup(byte deviceId, byte value)
        {
            lock (_token)
            {
                _commDevices[deviceId].canGroup = value;
                _UpdateDeviceGroupsInsideLock();
            }
        }

        public void SetDeviceIndex(byte deviceId, byte value)
        {
            lock (_token)
            {
                _commDevices[deviceId].canIndex = value;
                _UpdateDeviceGroupsInsideLock();
            }
        }

        private void _UpdateDeviceGroupsInsideLock()
        {
            // create device groups
            _commDeviceGroups.Clear();

            foreach (CommDevice d in _commDevices)
            {
                CommDeviceGroup dg = _commDeviceGroups.Find(x => x.group == d.canGroup);
                if (dg == null)
                {
                    _commDeviceGroups.Add(new CommDeviceGroup(d.canGroup, _canComm, _commParameters));
                    dg = _commDeviceGroups[_commDeviceGroups.Count - 1];
                }
                dg.AddCommDeviceRef(d);
            }
        }

        #endregion

        #region "Automatic"

        public void SetAutoWriteCurrents(bool value)
        {
            lock (_token) { _autoWriteCurrents = value; }
        }

        public void SetAutoWritePWM(bool value)
        {
            lock (_token) { _autoWritePwm = value; }
        }

        public void SetAutoWriteIntervalMs(ushort value)
        {
            lock (_token) { 
                if (value >= MIN_AUTO_INTERVAL_MS)
                    _autoWriteIntervalMs = value; 
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

        public void SetDevicePulses(byte deviceId, byte[] pulses)
        {
            lock (_token)
            {
                for (byte i = 0; i < pulses.Length; i += 1)
                {
                    _commDevices[deviceId].deviceRef.busConfig.SetPulse(i, pulses[i]);
                }
            }
        }

        public void SetDeviceCurrents(byte deviceId, byte[] currents)
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

        public void SendCommandUnlockEeprom(byte deviceId)
        {
            lock (_token) { _commDevices[deviceId].SendCommandUnlockEeprom(); }
        }

        public void SendCommandReset(byte deviceId)
        {
            lock (_token) { _commDevices[deviceId].SendCommandReset(); }
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

        #region "Device Access"

        public Device GetDeviceCopy(byte deviceId)
        {
            lock (_token) { return _commDevices[deviceId].deviceRef.Copy(); }
        }

        public EEProm ReadEepromCopy(byte deviceId)
        {
            lock (_token)
            {
                _commDevices[deviceId].ReadEeprom();
                return _commDevices[deviceId].deviceRef.eeprom.Copy();
            }
        }

        public BoolString WriteEeprom(byte deviceId, EEProm eeprom)
        {
            lock (_token) 
            { 
                _commDevices[deviceId].deviceRef.eeprom = eeprom.Copy();
                BoolString ret = _commDevices[deviceId].WriteEeprom();
                return ret;
            }
        }

        public bool GetDeviceReadFail(byte deviceId)
        {
            lock (_token) { 
                bool ret = _commDevices[deviceId].deviceRef.readFail.GetClearWasIs1();
                _commDevices[deviceId].deviceRef.readFail.SetValue(false);
                return ret;
            }
        }

        #endregion

        #region "Master Thread"

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
                    autoIntervalMsCopy = _autoWriteIntervalMs;
                    _ThreadDoAnimationInsideLock();
                    _ThreadDoW3InsideLock();
                    _ThreadDoReadStatusInsideLock();

                    execTimeMs = (ushort)stopWatch.Elapsed.Milliseconds;
                }
                // ---------------------------------------------------------------------------

                // Sleep at least some time outside the Lock
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

        private void _ThreadDoW3InsideLock()
        {
            if (_autoWritePwm || _autoWriteCurrents)
            {
                if (_autoWritePwm && (!_lastPwmWrittenNoLock || !_autoWriteCurrents))
                {
                    foreach (CommDeviceGroup dg in _commDeviceGroups) dg.WritePwmData();
                    _lastPwmWrittenNoLock = true;
                }
                else if (_autoWriteCurrents)
                {
                    foreach (CommDeviceGroup dg in _commDeviceGroups) dg.WriteCurrentsData();
                    _lastPwmWrittenNoLock = false;
                }
            }
        }

        private void _ThreadDoReadStatusInsideLock()
        {
            if (_autoReadFullStatus || _autoReadDiagStatus)
            {
                switch (_autoReadCycleNoLock)
                {
                    case 0: _commDevices[_autoReadDeviceId].ReadBusStatusDiag(); break; 
                    case 1: _commDevices[_autoReadDeviceId].ReadBusStatusVdifs(); break;
                    case 2: _commDevices[_autoReadDeviceId].ReadBusStatusIleds(); break;
                    case 3: _commDevices[_autoReadDeviceId].ReadBusStatusVleds(); break;
                    case 4: _commDevices[_autoReadDeviceId].ReadBusStatusMisc(); break;
                }
                if (_autoReadFullStatus) 
                    _autoReadCycleNoLock = (byte)((_autoReadCycleNoLock + 1) % 5);
                else 
                    _autoReadCycleNoLock = 0;

            }
        }

        #endregion

        public Master()
        {
            _canComm = new CanComm("ELMOS_52295_CAN_Eval");
            _commParameters = new CommParameters();
            _commDevices = new List<CommDevice>();
            _commDeviceGroups = new List<CommDeviceGroup>();

            // animation Memory
            _animationEnabled = false;
            _animationDelay = 0;
            _animationMemory = new MemoryStream();

            _autoWriteIntervalMs = DEFAULT_AUTO_INTERVAL_MS;
            _lastPwmWrittenNoLock = false;
            _autoWritePwm = false;
            _autoWriteCurrents = false;
            _autoReadFullStatus = false;
            _autoReadDeviceId = 0;
            _threadExecTimesMsNoLock = new Queue<ushort>(MAX_THREAD_EXEC_TIME_SAMPLES);
            _threadExecTimesMeanMs = 0;
            _threadExecTimesMaxMs = 0;

            // start Thread
            _thread = new Thread(new ThreadStart(this.ThreadRun));
            _thread.Priority = ThreadPriority.AboveNormal;
            _thread.Start();
        }

    }
}
