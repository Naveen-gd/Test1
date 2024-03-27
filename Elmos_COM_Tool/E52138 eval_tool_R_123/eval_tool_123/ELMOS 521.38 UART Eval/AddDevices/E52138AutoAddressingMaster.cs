using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ELMOS_521._38_UART_Eval
{
    class AutoAddressingStateMachine : StateMachine
    {
        readonly E52138AutoAddressingMaster parent;
        
        int currentAddress = 1;

        public AutoAddressingStateMachine(E52138AutoAddressingMaster parent, bool useMutex) : base(runTimer: 40, useMutex: useMutex)
        {
            this.parent = parent;

            nextState = StartSequence;
        }


        private void StartSequence()
        {
            Console.WriteLine("Start Seq");
            try
            {
                parent.StartSequence();
                nextState = StartMeasurement;
            }
            catch (System.Exception e) when (e.Message.StartsWith("Transmission"))
            {
                Console.WriteLine("Could not send start message.");
                Stop();
                parent.Failure();
            }
        }

        private void StartMeasurement()
        {
            Console.WriteLine("Start Measure");
            parent.StartMeasurement();

            nextState = SendSequenceId;
        }

        private void StartNextMeasurement()
        {
            Console.WriteLine("Start Measure");
            parent.StartMeasurement();

            parent.AddDevice(currentAddress);
            currentAddress += 6;

            nextState = SendSequenceId;
        }

        private void SendSequenceId()
        {
            try
            {
                Console.WriteLine("SendID");
                parent.SendSequenceId(1, currentAddress);
                
                nextState = StartNextMeasurement;
            }
            catch (System.Exception e) when (e.Message.StartsWith("Transmission"))
            {
                Console.WriteLine("Stop");
                nextState = StopSequence;
            }
        }
        private void StopSequence()
        {
            parent.StopSequence();
            Stop();
        }
    }
    

    class E52138AutoAddressingMaster : IDisposable
    {
        public class DeviceAddedEventArgs : EventArgs
        {
            public int Address;

            public DeviceAddedEventArgs(int address) => Address = address;
        }

        public event EventHandler<DeviceAddedEventArgs> DeviceAddedEvent;
        public event Action AutoAddressingStopped;
        public event Action AutoAddressingFailure;

        private bool isDisposed = false;

        private dynamic aaMasterPy;
        private bool active = false;

        public List<int> AddressesAssigned { get; private set; }

        AutoAddressingStateMachine autoAddressingStateMachine;

        private const int SEQUENCE_MEASUREMENTS = 14;

        public E52138AutoAddressingMaster(ApplicationData data)
        {
            aaMasterPy = data.E52138AutoAddressingMasterPy(api: data.API, diag: true, publish_retries: 50, subscribe_retries: 50);

            autoAddressingStateMachine = new AutoAddressingStateMachine(this, useMutex: true);
            AddressesAssigned = new List<int>();
        }

        ~E52138AutoAddressingMaster()
        {
            try
            {
                StopSequence();
                Dispose(false);
            }
            catch
            {
                Console.WriteLine("Could not close auto addressing");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                // dispose managed resources
                autoAddressingStateMachine.Dispose();
            }

            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Start()
        {
            autoAddressingStateMachine.Start();
        }

        public void Stop()
        {
            autoAddressingStateMachine.Stop();
        }

        public void Failure()
        {
            AutoAddressingFailure?.Invoke();
        }

        public void AddDevice(int address)
        {
            AddressesAssigned.Add(address);
            DeviceAddedEvent?.Invoke(this, new DeviceAddedEventArgs(address));
        }

        public void StartSequence()
        {
            aaMasterPy.aa_sequence_start();
            active = true;
        }

        public void StartMeasurement()
        {
            aaMasterPy.aa_sequence_measure(SEQUENCE_MEASUREMENTS);
        }

        public void SendSequenceId(int endpoint, int address)
        {
            aaMasterPy.aa_sequence_id(endpoint, address);
        }

        public void StopSequence()
        {
            if (active) aaMasterPy.aa_sequence_stop();

            active = false;
            AutoAddressingStopped?.Invoke();
        }
    }
}
