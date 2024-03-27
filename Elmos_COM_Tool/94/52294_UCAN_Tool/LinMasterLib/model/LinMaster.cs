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

namespace LinMasterLib
{
    public class LinMaster
    {
        public const ushort DEFAULT_AUTO_INTERVAL_MS = 10;
        public const ushort MIN_AUTO_INTERVAL_MS = 10;

        // ---------------------------------------------------------------------------
        // VARIABLES WITH LOCK

        static readonly object _token = new object();

        private LinComm _linComm;

        private byte _lastId = 0;
        private bool[] _startIdFlags;
        private bool[] _receiveIdFlag;

        private ushort _autoWriteIntervalMs;

        // ---------------------------------------------------------------------------
        // VARIABLES WITHOUT LOCK OR COPIES

        private Thread _thread;

        // ---------------------------------------------------------------------------

        public FtdiUart ftdiUartRef
        {
            get { return _linComm.ftdiUartRef; }
        }

        #region "Comm"

        public List<FtdiBase.DeviceListEntry> GetUCanCommDevices()
        {
            lock (_token) { return ftdiUartRef.GetDevices(); }
        }

        public bool GetConnected(){
            lock (_token) { return ftdiUartRef.Connected(); }
        }

        public void OpenChannelByDevice(FtdiBase.DeviceListEntry device, uint channel = 0)
        {
            lock (_token)
            {
                if (!ftdiUartRef.Connected())
                {
                    ftdiUartRef.OpenChannelByDevice(device, channel);
                }
            }
        }

        public void OpenChannelBySerialNumber(string serial, uint channel = 0)
        {
            lock (_token)
            {
                if (!ftdiUartRef.Connected())
                {
                    ftdiUartRef.OpenChannelBySerialNumber(serial, channel);
                }
            }
        }

        public void Close()
        {
            lock (_token)
            {
                if (ftdiUartRef.Connected())
                {
                    ftdiUartRef.Close();
                }
            }
        }

        public void SetCommBreak(double breakLength)
        {
            lock (_token) { ftdiUartRef.SetBreakLength(breakLength); }
        }

        public void SetCommBitrate(uint bitrate)
        {
            lock (_token) { ftdiUartRef.SetBitrate(bitrate); }
        }

        public void SetCommParity(uint parity)
        {
            lock (_token) { ftdiUartRef.SetParity(parity); }
        }

        public void SendWakeup(bool symbol = true, bool ack = true)
        {
            lock (_token) {
                if (symbol)
                {
                    ftdiUartRef.SendWakeup();
                    Thread.Sleep(25);
                }
                if (ack)
                {
                    //TODO _commBroadcast.SendCommandWakeupAck();
                }
            }
        }

        #endregion

        #region "Automatic"

        /*
        public void SetAutoStartId(byte id, bool enabled)
        {
            lock (_token) { _autoNodeEnabled = enabled; }
        }

        public void SetAutoReceiveId(byte id, bool enabled)
        {
            lock (_token) { _autoNodeEnabled = enabled; }
        }
        */

        #endregion

        #region "Device Access"

        /*
        public bool WriteData(byte deviceId, ushort addr, ushort[] outData)
        {
            lock (_token) { return _commDevices[deviceId].ucanCommRef.WriteData(addr, outData); }
        }

        public bool ReadData(byte deviceId, ushort addr, ref ushort[] outData)
        {
            lock (_token) { return _commDevices[deviceId].ucanCommRef.ReadData(addr, ref outData); }
        }
         * */

        #endregion

        #region "Master Thread"

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
                // ---------------------------------------------------------------------------
                // DO STUFF
                lock (_token)
                {
                    autoIntervalMsCopy = _autoWriteIntervalMs;
                    _ThreadDoAutoSendHeaderInsideLock();
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
                stopWatch.Stop();

            }
        }

        private void _ThreadDoAutoSendHeaderInsideLock()
        {
        }

        #endregion

        public LinMaster()
        {
            _linComm = new LinComm();

            _autoWriteIntervalMs = DEFAULT_AUTO_INTERVAL_MS;

            // start Thread
            _thread = new Thread(new ThreadStart(this.ThreadRun));
            _thread.Start();
        }

    }
}
