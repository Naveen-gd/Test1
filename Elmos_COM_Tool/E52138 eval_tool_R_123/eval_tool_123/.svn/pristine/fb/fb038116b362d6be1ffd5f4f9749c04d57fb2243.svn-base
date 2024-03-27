using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ELMOS_521._38_UART_Eval.ApplicationData;

namespace ELMOS_521._38_UART_Eval
{
    public partial class E52138ChipAPI : INotifyPropertyChanged
    {
        public E52138ChipAPI()
        {
            InitializeData();
        }

        public void StartFromJson(ApplicationData data)
        {
            this.data = data;
            data.PropertyChanged += DataChanged;

            dut = data.E52138py(api: data.API, device_address: deviceAddress);

            communicationStateMachine = new CommunicationStateMachine(this, data, data.SendPWMUpdateInterval);
            deviceInformationStateMachine = new DeviceInformationStateMachine(this, 5000);

            comMode = ComMode.DeviceInfo;
        }
    }
}
