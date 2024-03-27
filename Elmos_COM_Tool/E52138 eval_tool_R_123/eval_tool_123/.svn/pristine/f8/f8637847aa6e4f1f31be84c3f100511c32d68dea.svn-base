using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELMOS_521._38_UART_Eval
{
    public partial class AutoAddressing : Form
    {
        private FormMain mainForm;
        private ApplicationData data;

        private List<E52138ChipAPI> chips;

        private E52138AutoAddressingMaster aaMaster;
        private bool close = false;

        public AutoAddressing(ApplicationData data, FormMain mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;
            this.data = data;

            chips = new List<E52138ChipAPI>();
            aaMaster = new E52138AutoAddressingMaster(data);
            aaMaster.DeviceAddedEvent += DeviceAdded;
            aaMaster.AutoAddressingStopped += AutoAddressingStopped;
            aaMaster.AutoAddressingFailure += AutoAddressingFailed;
            
            LabStatus.Text = "";
            ButStartAdd.Text = "START";
        }
        protected void InvokeWrapper(Control c, Action f)
        {
            if (c == null || !c.IsHandleCreated || c.IsDisposed)
                return;
            
            if (c.InvokeRequired)
            {
                try
                {
                    c?.Invoke((Action)delegate { InvokeWrapper(c, f); });
                }
                catch (System.ObjectDisposedException e)
                {
                    Console.WriteLine("Object disposed: " + e.Message);
                }
            }
            else
            {
                f();
            }
        }

        private void DeviceAdded(object sender, E52138AutoAddressingMaster.DeviceAddedEventArgs e)
        {
            E52138ChipAPI chip = new E52138ChipAPI(data, e.Address);
            chip.GetFWVersion();
            chip.GetFWVariant();
            chip.IsMultiplexingActive();
            string[] deviceInfo = new string[]
            {
                string.Format("{0:000}", chip.DeviceAddress),
                chip.FirmwareVersion,
                chip.FirmwareVariant,
            };

            InvokeWrapper(LabStatus, () =>
            {
                dataGridView1.Rows.Add(deviceInfo);
                LabStatus.Text = "DEVICE FOUND";
                ButStartAdd.Text = "ADD";
            });
            chips.Add(chip);
        }

        private void AutoAddressingStopped()
        {
            if (!close)
            {
                InvokeWrapper(LabStatus, () =>
                {
                    LabStatus.Text = "COMPLETE";
                });
            }
        }

        private void AutoAddressingFailed()
        {
            if (!close)
            {
                InvokeWrapper(LabStatus, () =>
                {
                    LabStatus.Text = "FAILED";
                });
            }
        }

        private void ButCancel_Click(object sender, EventArgs e)
        {
            aaMaster.Stop();
            Close();
        }

        private void ButStartAdd_Click(object sender, EventArgs e)
        {
            if (chips.Count == 0)
            {
                // Start
                LabStatus.Text = "START PROCEDURE";
                aaMaster.Start();
            }
            else
            {
                mainForm.AddNewTabs(chips);
                close = true;
                aaMaster.Stop();
                Close();
            }
        }


    }
}
