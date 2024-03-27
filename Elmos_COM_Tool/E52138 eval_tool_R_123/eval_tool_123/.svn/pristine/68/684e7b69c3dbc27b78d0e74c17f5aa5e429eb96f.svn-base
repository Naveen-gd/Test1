using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELMOS_521._38_UART_Eval.DeviceTabPanel
{
    public partial class General : PanelForm
    {

        public General()
        {
            InitializeComponent();
            Panel = panGeneral;
        }

        protected override void UpdateData(string propertyName, bool force = false)
        {
            if (propertyName == "deviceInfo" || force)
            {
                InvokeWrapper(() =>
                {
                    boxDIDeviceAddress.Text = string.Format("{0:000}", chip.DeviceAddress);
                    boxDIFWVersion.Text = chip.FirmwareVersion;
                    boxDIFWVariant.Text = chip.FirmwareVariant;
                    boxDIHWVersion.Text = chip.HardwareVersion;
                });
            }
        }
    }
}
