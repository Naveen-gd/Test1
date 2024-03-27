using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;

namespace _52294_Socket_Master
{
    internal class ConnectionTimeout
    {
        internal delegate void EnableChangeCallback(bool state);
        internal delegate void KillConnectionCallback();

        private uint _TimeoutSeconds = 30;
        private EnableChangeCallback _enableChangeCallback;
        private KillConnectionCallback _killConnectionCallback;

        internal EnableChangeCallback enableChangeCallback
        {
            set { _enableChangeCallback = value; }
        }

        internal KillConnectionCallback killConnectionCallback
        {
            set { _killConnectionCallback = value; }
        }

        internal uint TimeoutSeconds
        {
            set
            {
                Disable();
                _TimeoutSeconds = value;
                if (_TimeoutSeconds > 0)
                {
                    _timer.Interval = _TimeoutSeconds * 1000;
                    Enable();
                }
            }
            get { return _TimeoutSeconds; }
        }

        public void DefaultTimeout(){
            TimeoutSeconds = 30;
        }

        private Timer _timer;

        internal ConnectionTimeout()
        {
            _timer = new Timer(_TimeoutSeconds * 1000);
            _timer.Elapsed += _TimerElapsed;
        }

        internal void Enable()
        {
            _timer.Enabled = true;
            if (_enableChangeCallback != null) _enableChangeCallback(true);
        }

        internal void Disable()
        {
            _timer.Enabled = false;
            if (_enableChangeCallback != null) _enableChangeCallback(false);
        }

        internal void Trigger()
        {
            if (_timer.Enabled)
            {
                _timer.Enabled = false;
                _timer.Enabled = true;
            }
        }

        private void _TimerElapsed(Object source, ElapsedEventArgs e)
        {
            if (_killConnectionCallback != null) _killConnectionCallback();
        }

    }
}
