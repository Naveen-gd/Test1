
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;

namespace ELMOS_521._38_UART_Eval
{

    public partial class ApplicationData : INotifyPropertyChanged
    {
        private bool sendPWM = false;
        public bool SendPWM { get { return sendPWM; } set { sendPWM = value; OnPropertyChanged("sendPWM"); } }

        private double sendPWMUpdateInterval = 20;
        public double SendPWMUpdateInterval { get { return sendPWMUpdateInterval; } set { sendPWMUpdateInterval = value; OnPropertyChanged("sendPWMUpdateInterval"); } }

        private bool statusUpdates = false;
        public bool StatusUpdates { get { return statusUpdates; } set { statusUpdates = value; OnPropertyChanged("statusUpdates"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool establishConnection = false;
        public bool EstablishConnection { get { return establishConnection; } set { establishConnection = value; OnPropertyChanged("establishConnection"); } }

        private bool connectionEstablished = false;
        public bool ConnectionEstablished { get { return connectionEstablished; } set { connectionEstablished = value; OnPropertyChanged("connectionEstablished"); } }

        private string comPort = "";
        public string COMPort { get { return comPort; } set { comPort = value; OnPropertyChanged("comPort"); } }

        private int baudRate = 1000000;
        public int BaudRate { get { return baudRate; } set { baudRate = value; OnPropertyChanged("baudRate"); } }

        private dynamic aPI = null;
        public dynamic API { get { return aPI; } set { aPI = value; OnPropertyChanged("aPI"); } }

        private bool writeLogToFile = false;
        public dynamic WriteLogToFile { get { return writeLogToFile; } set { writeLogToFile = value; OnPropertyChanged("writeLogFile"); } }

        public dynamic E52138py;
        public dynamic E52138AutoAddressingMasterPy;

        public Dictionary<int, E52138ChipAPI> chips;
        public int ActiveChip;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
