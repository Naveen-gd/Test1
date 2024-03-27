using System;
using System.Timers;


namespace ELMOS_521._38_UART_Eval
{
    abstract class StateMachine : IDisposable
    {
        private bool isDisposed = false;

        protected class StateAttribute : Attribute { }
        protected Action nextState = null;
        private Timer timer;
        private bool timerMutex;


        public StateMachine(double runTimer = 0.0, bool useMutex = true)
        {
            
            if (runTimer > 0)
            {
                timer = new Timer(runTimer);
                if (useMutex)
                    timer.Elapsed += RunTimerHandlerMutex;
                else
                    timer.Elapsed += RunTimerHandler;
            }

            
        }

        ~StateMachine()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            Stop();

            if (disposing)
            {
                // dispose managed resources
                if (timer != null)
                {
                    timer.Dispose();
                    timer = null;
                }
            }

            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Execute()
        {
            nextState();
        }

        public void Start()
        {
            if (timer != null)
            {
                if (timer.Enabled == false)
                    Execute();

                timer.Enabled = true;
            }
        }

        public void Stop()
        {
            if (timer != null)
            {
                timer.Enabled = false;
            }
        }

        private void RunTimerHandler(Object sender, ElapsedEventArgs eventArgs)
        {
            Execute();
        }

        private void RunTimerHandlerMutex(Object sender, ElapsedEventArgs eventArgs)
        {
            if (timerMutex)
            {
                return;
            }

            timerMutex = true;
            Execute();
            timerMutex = false;
        }

        public void Interval(double value)
        {
            if (timer != null)
            {
                timer.Interval = value;
            }
        }
    }
}
