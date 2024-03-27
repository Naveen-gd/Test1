using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ELMOS_521._38_UART_Eval
{
    public class StateIterator : IDisposable
    {
        private bool isDisposed = false;

        protected class TransitionAttribute : Attribute { }
        private List<MethodInfo> transitions;
        private int currentTransition = 0;
        private int nextTransition = 0;
        private Timer timer;
        private bool timerMutex;

        private List<Dictionary<runtimeData, dynamic>> runtimeInformation;

        enum runtimeData
        {
            name,
            averageExecutionTime,
            mutex,
            executed,
        }

        public StateIterator(double runTimer = 0.0, bool useMutex = true)
        {
            transitions = new List<MethodInfo>();
            runtimeInformation = new List<Dictionary<runtimeData, dynamic>>();

            foreach (MethodInfo method in GetType().GetMethods())
            {
                if (method.GetCustomAttributes().Contains(new TransitionAttribute()))
                {
                    transitions.Add(method);
                    runtimeInformation.Add(new Dictionary<runtimeData, dynamic>()
                    {
                        [runtimeData.name] = method.Name,
                        [runtimeData.averageExecutionTime] = 0.0,
                        [runtimeData.mutex] = 0,
                        [runtimeData.executed] = 0,
                    });
                }
            }

            if (transitions.Count == 0)
            {
                throw new Exception("StateMachine needs at least one method marked with an TransitionAttribute");
            }

            if (runTimer > 0)
            {
                timer = new Timer(runTimer);
                if (useMutex)
                    timer.Elapsed += RunTimerHandlerMutex;
                else
                    timer.Elapsed += RunTimerHandler;
            }
        }

        ~StateIterator()
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
            currentTransition = nextTransition;

            nextTransition += 1;
            nextTransition %= transitions.Count;

            runtimeInformation[currentTransition][runtimeData.executed] += 1;

            var watch = System.Diagnostics.Stopwatch.StartNew();
            transitions[currentTransition].Invoke(this, null);
            watch.Stop();

            runtimeInformation[currentTransition][runtimeData.averageExecutionTime] *= runtimeInformation[currentTransition][runtimeData.executed] - 1;
            runtimeInformation[currentTransition][runtimeData.averageExecutionTime] += watch.ElapsedMilliseconds;
            runtimeInformation[currentTransition][runtimeData.averageExecutionTime] /= runtimeInformation[currentTransition][runtimeData.executed];

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
                runtimeInformation[currentTransition][runtimeData.mutex] += 1;
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
