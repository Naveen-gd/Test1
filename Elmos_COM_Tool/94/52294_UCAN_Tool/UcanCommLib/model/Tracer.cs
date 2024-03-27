using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.IO;
using System.Threading;
using System.Diagnostics;

using Extensions;

namespace UcanCommLib
{
    public class TracerData
    {
    }

    public class TracerDataBreak : TracerData
    {
        public long elapsedMs;
    }

    public class TracerDataReceived : TracerData
    {
        public List<byte> data;
        public bool timeout;

        public TracerDataReceived()
        {
            data = new List<byte>();
        }
    }

    public class Tracer
    {
        static readonly object _token = new object();

        private List<TracerData> _traceData;
        private Stopwatch  _timer;

        public void Start(){
            lock (_token)
            {
                _timer.Start();
            }
        }

        public void Stop(){
            lock (_token)
            {
                _timer.Stop();
            }
        }

        public void Clear(){
            lock (_token) {
                _traceData.Clear();
                _timer.Restart();
            }
        }

        public void NewFrame()
        {
            lock (_token)
            {
                if (_timer.IsRunning)
                {
                    TracerDataBreak d = new TracerDataBreak();
                    d.elapsedMs = _timer.ElapsedMilliseconds;
                    _traceData.Add(d);
                }
            }
        }

        public void FrameData(byte[] data, uint numBytesRead, bool timeout)
        {
            lock (_token)
            {
                if (_timer.IsRunning)
                {
                    TracerDataReceived r = new TracerDataReceived();
                    r.timeout = timeout;
                    for (int i = 0; i < numBytesRead; i += 1)
                    {
                        r.data.Add(data[i]);
                    }
                    _traceData.Add(r);
                }
            }
        }

        public List<TracerData> GetClearDataCopy()
        {
            lock (_token)
            {
                List<TracerData> ret = _traceData.Copy();
                _traceData.Clear();
                return ret;
            }
        }


        public Tracer()
        {
            _traceData = new List<TracerData>();
            _timer = new Stopwatch();
            _timer.Stop();
        }

    }
}
