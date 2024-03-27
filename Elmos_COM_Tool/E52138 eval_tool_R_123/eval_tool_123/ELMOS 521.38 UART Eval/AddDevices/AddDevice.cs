using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ELMOS_521._38_UART_Eval
{
    public partial class AddDevice : Form
    {
        private FormMain mainForm;
        private ApplicationData data;
        private E52138ChipAPI chip;

        public AddDevice(ApplicationData data, FormMain mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;
            this.data = data;

            chip = new E52138ChipAPI(data, 1);

            chip.PropertyChanged += DataChanged;
            data.PropertyChanged += DataChanged;

            if (data.API == null)
            {
                LabStatus.Text = "NO API";
            }
            else
            {
                LabStatus.Text = "SEARCH";
                chip.GetDeviceAddress();
                chip.GetFWVersion();
                chip.GetFWVariant();
                chip.IsMultiplexingActive();
            }
        }

        private void DataChanged(object sender, PropertyChangedEventArgs e)
        {
            if (BoxDIDeviceAddress.InvokeRequired)
            {
                BoxDIDeviceAddress.Invoke((MethodInvoker)delegate { DataChanged(sender, e); });
            }
            else if (e.PropertyName == "deviceInfo")
            {
                // BoxDIDeviceAddress.Text = chip.DeviceAddress;
                BoxDIFWVersion.Text = chip.FirmwareVersion;
                BoxDIFWVariant.Text = chip.FirmwareVariant;
                BoxDIHWVersion.Text = chip.HardwareVersion;

                if (!String.IsNullOrWhiteSpace(chip.FirmwareVersion)) 
                {
                    LabStatus.Text = "";
                }
            }
            else if (e.PropertyName == "aPI")
            {
                if (data.API == null)
                {
                    LabStatus.Text = "NO API";
                }
                else
                {
                    LabStatus.Text = "SEARCH";
                    chip.GetDeviceAddress();
                    chip.GetFWVersion();
                    chip.GetFWVariant();
                }
            }
        }

        private void CloseForm(bool cleanupChip)
        {
            chip.PropertyChanged -= DataChanged;
            data.PropertyChanged -= DataChanged;

            if (cleanupChip)
            {
                chip.Close();
                chip.Dispose();
            }
            Close();
        }

        private void ButCancel_Click(object sender, EventArgs e)
        {
            CloseForm(true);
        }

        private void ButAdd_Click(object sender, EventArgs e)
        {
            List<E52138ChipAPI> chips = new List<E52138ChipAPI>() { chip };
            mainForm.AddNewTabs(chips);

            CloseForm(false);
        }

        private void BoxDIDeviceAddress_TextChanged(object sender, EventArgs e)
        {

            BoxDIFWVersion.Text = "";
            BoxDIFWVariant.Text = "";
            BoxDIHWVersion.Text = "";

            if (int.TryParse(BoxDIDeviceAddress.Text, out int address) && address < 256 && address > 0)
            {
                LabStatus.Text = "SEARCH";
                chip.SetDeviceAddress(address);
                chip.GetDeviceAddress();
                chip.GetFWVersion();
                chip.GetFWVariant();
                ButAdd.Enabled = true;
            }
            else
            {
                LabStatus.Text = "INVALID";
                ButAdd.Enabled = false;
            }
            
        }
    }
}
