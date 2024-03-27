﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using _52294_UCAN_Tool.view;

namespace ELMOS_521._38_UART_Eval
{

    public partial class FormMain : Form
    {

        private ApplicationData data;
        public TabControl TabDevices { get { return tabDevices; } }

        private Animations.AnimationHandler animationHandler;
        private bool baudRateControlMutex = false;
        private bool autoAnimControlMutex = false;

        private DeviceTabPanel.DeviceTabPanel devicePanel;

        private FileLoader fileLoader;

        private bool compatibilityWarningAccepted = false;

        private const string DEVICE_TAB_PREFIX = "Device ";

        public FormMain(ApplicationData data)
        {
            InitializeComponent();

            CorrectAddDeviceButtonLocation();

            this.data = data;
            data.PropertyChanged += DataChanged;
            cmbAdapter.SelectedIndex = 0;

            SerialPort serialPort = null;
            //serialPort
            // fill com port list
            foreach (var port in SerialPort.GetPortNames())
                comUARTCOMPort.Items.Add(port);

            if (comUARTCOMPort.Items.Count > 0)
                comUARTCOMPort.SelectedIndex = 0;

            devicePanel = new DeviceTabPanel.DeviceTabPanel(this);
            animationHandler = new Animations.AnimationHandler(data);

            fileLoader = new FileLoader(data, this);
            fileLoader.ChipsAddedEvent += ChipAddedHandler;
            fileLoader.FilePathSet += FilePathSetHandler;

            LabToolVersion.Text = LabToolVersion.Text = "Version " + Assembly.GetExecutingAssembly().GetName().Version.Major.ToString()
                            + "." + Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString()
                            + "." + Assembly.GetExecutingAssembly().GetName().Version.Build.ToString();
        }

        private void DataChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "connectionEstablished")
            {
                if (data.ConnectionEstablished)
                    {
                        comUARTCOMPort.Enabled = false;
                        cmbUARTBaudrate.Enabled = false;
                        butUARTOpen.Enabled = false;
                        butUARTClose.Enabled = true;
                        MenItmTransportOpen.Enabled = false;
                        MenItmTransportClose.Enabled = true;
                        MenItmBaudRate.Enabled = false;
                        MenItmPort.Enabled = false;
                        MenItmDataLink.Enabled = false;
                    }
                    else
                    {
                        comUARTCOMPort.Enabled = true;
                        cmbUARTBaudrate.Enabled = true;
                        butUARTOpen.Enabled = true;
                        butUARTClose.Enabled = false;
                        MenItmTransportOpen.Enabled = true;
                        MenItmTransportClose.Enabled = false;
                        MenItmBaudRate.Enabled = true;
                        MenItmPort.Enabled = true;
                        MenItmDataLink.Enabled = true;
                }
            }
        }

        private void CorrectAddDeviceButtonLocation()
        {
            // correct add device location
            // calc text width
            int total = 0;
            foreach (System.Windows.Forms.TabPage page in tabDevices.Controls)
            {
                int width = TextRenderer.MeasureText(page.Text, page.Font).Width;

                width += 5;
                width = Math.Max(width, 42);
                total += width;
            }

            labAddDevice.Left = total + tabDevices.Left + 10;
        }

        private void AddNewTab(E52138ChipAPI chip)
        {
            TabPage tp = new TabPage(DEVICE_TAB_PREFIX + chip.DeviceAddress);
            if (data.chips.ContainsKey(chip.DeviceAddress))
            {
                MessageBox.Show("The device " + string.Format("{0:000}", chip.DeviceAddress) + " is already registered.", "Information");
            }
            else
            {
                data.chips[chip.DeviceAddress] = chip;

                tp.BackColor = System.Drawing.SystemColors.ControlLightLight;

                tabDevices.TabPages.Add(tp);
                foreach (TabPage page in tabDevices.TabPages)
                {
                    if (page.Name == "tabNoDevice")
                        RemoveTab(page);
                }
                tabDevices.Enabled = true;

                chip.SetMode(E52138ChipAPI.ComMode.Started);

                // select new tab
                tabDevices.SelectTab(tabDevices.TabCount - 1);
            }
        }

        private bool handleHwCompatibility(List<E52138ChipAPI> chips)
        {
            /*if (!compatibilityWarningAccepted)
            {
                bool showCompatibilityWarning = false;
                foreach (E52138ChipAPI chip in chips)
                {
                    if (chip.GetHWVersion() < 90)
                    {
                        showCompatibilityWarning = true;
                        break;
                    }
                }

                if (showCompatibilityWarning)
                {
                    DialogResult dialogResult = MessageBox.Show("Support only up to M521.38C. Newer version can be destroyed. For Help contact Elmos.", "Warning!", MessageBoxButtons.OKCancel);
                    if (dialogResult.Equals(DialogResult.OK))
                    {
                        //Application.Exit();
                        compatibilityWarningAccepted = true;
                    }
                }
            }*/

            return true;
        }

        public void AddNewTabs(List<E52138ChipAPI> chips)
        {
            // Check for incompatible hardware revisions
            if (handleHwCompatibility(chips))
            {
                // Add
                DialogResult result = DialogResult.Yes;

                // Find already existing devices
                List<TabPage> dublicatePages = new List<TabPage>();
                List<E52138ChipAPI> dublicateChips = new List<E52138ChipAPI>();

                foreach (TabPage tab in tabDevices.TabPages)
                {
                    var chip = chips.Find(c => tab.Text.EndsWith(string.Format("{0:000}", c.DeviceAddress)));
                    if (chip != null)
                    {
                        dublicatePages.Add(tab);
                        dublicateChips.Add(chip);
                    }
                }
                if (dublicatePages.Count == 1)
                {
                    result = MessageBox.Show("The device found is already registered. Do you want to reset the page in the main view?", "Overwrite", MessageBoxButtons.YesNo);
                }
                else if (dublicatePages.Count > 1)
                {
                    result = MessageBox.Show("Devices found are already registered. Do you want to reset the pages in the main view?", "Overwrite", MessageBoxButtons.YesNo);
                }

                if (result == DialogResult.Yes)
                {
                    // remove existent
                    foreach (TabPage page in dublicatePages)
                    {
                        RemoveTab(page);
                    }
                    foreach (E52138ChipAPI chip in dublicateChips)
                    {
                        data.chips.Remove(chip.DeviceAddress);
                    }
                }
                // Add device tabs
                foreach (var chip in chips)
                {
                    if (result == DialogResult.Yes || !dublicateChips.Exists(c => c == chip))
                    {
                        AddNewTab(chip);
                    }
                }
            }
        }

        public void ChipAddedHandler(object s, FileLoader.ChipsAddedEventArgs e)
        {
            AddNewTabs(e.Chips);
        }

        public void RemoveTab(TabPage page)
        {
            tabDevices.TabPages.Remove(page);
            if (page.Text != "No Device")
            {
                var deviceAddress = int.Parse(page.Text.Substring(DEVICE_TAB_PREFIX.Length));
                if (data.chips.TryGetValue(deviceAddress, out E52138ChipAPI chip))
                {
                    chip.Close();
                }
                data.chips.Remove(deviceAddress);
            }

        }

        public void RemoveCurrentTab()
        {
            RemoveTab(tabDevices.SelectedTab);
            GC.Collect();
        }

        public void FilePathSetHandler(object sender, EventArgs e)
        {
            MenItmSaveSetup.Enabled = true;
        }

        private void labAddDevice_Click(object sender, EventArgs e)
        {
            AddDevice addDevice = new AddDevice(data, this);
            addDevice.Show(this);
        }

        private void tabDevices_ControlAdded(object sender, ControlEventArgs e)
        {
            CorrectAddDeviceButtonLocation();
        }

        private void tabDevices_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (tabDevices.TabPages.Count <= 1)
            {
                tabDevices.TabPages.Add(tabNoDevice);
                tabDevices.Enabled = false;
            }

            CorrectAddDeviceButtonLocation();
        }

        private void numAUTOSendPWM_ValueChanged(object sender, EventArgs e)
        {
            data.SendPWMUpdateInterval = (double)numAUTOSendPWM.Value;
        }

        private void chkAUTOSendPWM_CheckedChanged(object sender, EventArgs e)
        {
            data.SendPWM = chkAUTOSendPWM.Checked;
        }

        private void chkAUTOStatusUpdates_CheckedChanged(object sender, EventArgs e)
        {
            data.StatusUpdates = chkAUTOStatusUpdates.Checked;
        }

        private void comUARTCOMPort_DropDown(object sender, EventArgs e)
        {
            comUARTCOMPort.Items.Clear();
            MenItmPort.DropDownItems.Clear();
            // fill com port list and menu
            foreach (var port in SerialPort.GetPortNames())
            {
                comUARTCOMPort.Items.Add(port);
                ToolStripItem item = MenItmPort.DropDownItems.Add(port);
                item.Click += comUARTCOMPort_TextUpdate;
                comUARTCOMPort_TextUpdate(comUARTCOMPort, new EventArgs());
            }       
        }

        private void butUARTOpen_Click(object sender, EventArgs e)
        {
            comUARTCOMPort.Enabled = false;
            cmbUARTBaudrate.Enabled = false;
            butUARTOpen.Enabled = false;

            data.EstablishConnection = true;
        }

        private void butUARTClose_Click(object sender, EventArgs e)
        {
            data.EstablishConnection = false;
        }

        private void comUARTCOMPort_TextUpdate(object sender, EventArgs e)
        {
            string text = "";
            if (sender is ComboBox cmb)
                text = cmb.Text;
            if (sender is ToolStripMenuItem men)
                text = men.Text;

            if (!String.IsNullOrWhiteSpace(text))
            {
                data.COMPort = text;
                comUARTCOMPort.Text = text;
                ActivateMenuItem(MenItmPort, text);
            }
        }

        private static void ActivateMenuItem(ToolStripMenuItem parent, ToolStripMenuItem men)
        {
            foreach (ToolStripMenuItem item in parent.DropDownItems.OfType<ToolStripMenuItem>())
            {
                item.Checked = (item == men);
            }
        }
        private void ActivateMenuItem(ToolStripMenuItem parent, string text)
        {
            foreach (ToolStripMenuItem item in parent.DropDownItems.OfType<ToolStripMenuItem>())
            {
                item.Checked = (item.Text == text);
            }
        }

        private void cmbUARTBaudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (baudRateControlMutex) return;

            baudRateControlMutex = true;
            string text = "";

            if (sender is ComboBox cmb)
                text = cmb.Text;
            if (sender is ToolStripMenuItem men)
                text = men.Text;

            switch (text.Trim())
            {
                case "57.6 kBaud":
                    data.BaudRate = 57600;
                    cmbUARTBaudrate.Text = text;
                    ActivateMenuItem(MenItmBaudRate, kBaudToolStripMenuItem57);
                    break;
                case "460.8 kBaud":
                    data.BaudRate = 460800;
                    cmbUARTBaudrate.Text = text;
                    ActivateMenuItem(MenItmBaudRate, kBaudToolStripMenuItem460);
                    break;
                case "500 kBaud":
                    data.BaudRate = 500000;
                    cmbUARTBaudrate.Text = text;
                    ActivateMenuItem(MenItmBaudRate, kBaudToolStripMenuItem500);
                    break;
                case "921.6 kBaud":
                    data.BaudRate = 921600;
                    cmbUARTBaudrate.Text = text;
                    ActivateMenuItem(MenItmBaudRate, kBaudToolStripMenuItem921);
                    break;
                case "1 MBaud":
                    data.BaudRate = 1000000;
                    cmbUARTBaudrate.Text = text;
                    ActivateMenuItem(MenItmBaudRate, kBaudToolStripMenuItem1M);
                    break;
                case "2 MBaud":
                    data.BaudRate = 2000000;
                    cmbUARTBaudrate.Text = text;
                    ActivateMenuItem(MenItmBaudRate, kBaudToolStripMenuItem2M);
                    break;
            }

            baudRateControlMutex = false;
        }

        private void cmbAutoAnim_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (autoAnimControlMutex) return;
            autoAnimControlMutex = true;
            
            string path = AppDomain.CurrentDomain.BaseDirectory;
            if (animationHandler.LoadModule(Path.Combine(path, "Animations", cmbAutoAnim.Text + ".py")))
            {
                chkAnimStart.Enabled = true;
            }

            autoAnimControlMutex = false;
        }

        private void butAutoAddressing_Click(object sender, EventArgs e)
        {
            if (data.API == null)
            {
                MessageBox.Show("Please open transport before using Auto Addressing.");
                return;
            }
            
            AutoAddressing autoAddressing = new AutoAddressing(data, this);
            autoAddressing.Show();
        }

        private void butAnimLoad_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "./Animations/";
                openFileDialog.Filter = "Python scripts (*.py)|*.py|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                }
            }
            
            if (!String.IsNullOrEmpty(filePath) && animationHandler.LoadModule(filePath))
            {
                ActivateMenuItem(MenItmAnimationsLoad, Path.GetFileName(filePath));
                chkAnimStart.Enabled = true;
            }
        }

        private void chkAnimStart_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox box)
                MenItmAnimationsRun.Checked = box.Checked;

            if (sender is ToolStripMenuItem men)
            {
                chkAnimStart.Checked = men.Checked;
                if (chkAnimStart.Checked)
                {
                    animationHandler.Start();
                }
                else
                {
                    animationHandler.Stop();
                }
            }
        }

        private void removeCurrentDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveCurrentTab();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MenItmAnimationsLoad_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem MenItm)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;

                if (animationHandler.LoadModule(Path.Combine(path, "Animations", MenItm.Text + ".py")))
                {
                    cmbAutoAnim.Text = MenItm.Text;
                    chkAnimStart.Enabled = true;
                }
            }
        }

        private void MenItmAnimations_Click(object sender, EventArgs e)
        {
            while(MenItmAnimationsLoad.DropDownItems.Count > 1)
            {
                MenItmAnimationsLoad.DropDownItems.RemoveAt(1);
            }

            string path = AppDomain.CurrentDomain.BaseDirectory;

            bool customAnimation = true;
            if (Directory.Exists(Path.Combine(path, "Animations")))
            {
                bool newItemAdded = false;
                foreach (var filepath in Directory.GetFiles(Path.Combine(path, "Animations"), "*.py"))
                {
                    string filename = Path.GetFileName(filepath);
                    filename = filename.Substring(0, filename.Length - 3);
                    ToolStripMenuItem menItm = new ToolStripMenuItem(filename);
                    MenItmAnimationsLoad.DropDownItems.Add(menItm);

                    menItm.Click += MenItmAnimationsLoad_Click;

                    if (filepath == animationHandler.AnimationModule)
                    {
                        menItm.Checked = true;
                        customAnimation = false;
                    }
                    newItemAdded = true;
                }
                if (newItemAdded)
                    MenItmAnimationsLoad.DropDownItems.Insert(1, new ToolStripSeparator());
            }

            if (customAnimation && animationHandler.AnimationModule != "")
            {
                MenItmAnimationsLoad.DropDownItems.Add(new ToolStripSeparator());
                var newMenItm = new ToolStripMenuItem("Custom Animation");
                MenItmAnimationsLoad.DropDownItems.Add(newMenItm);
                newMenItm.Checked = true;
            }
        }

        private void MenItmOpenSetup_Click(object sender, EventArgs e)
        {
            fileLoader.Open();
        }

        private void MenItmSaveSetupAs_Click(object sender, EventArgs e)
        {
            fileLoader.SaveAs(data.chips.Values.ToList());
        }

        private void MenItmSaveSetup_Click(object sender, EventArgs e)
        {
            fileLoader.Save(data.chips.Values.ToList());
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            animationHandler.Stop();
        }

        private void tabDevices_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage.Text != "No Device")
            {
                e.TabPage.Controls.Add(devicePanel.getPanel());
                var deviceAddress = int.Parse(e.TabPage.Text.Substring(DEVICE_TAB_PREFIX.Length));
                devicePanel.SetChip(data.chips[deviceAddress]);
                data.ActiveChip = deviceAddress;
            }
        }

        private void cmbAutoAnim_DropDown(object sender, EventArgs e)
        {
            cmbAutoAnim.Items.Clear();
            string path = AppDomain.CurrentDomain.BaseDirectory;

            bool customAnimation = true;
            if (Directory.Exists(Path.Combine(path, "Animations")))
            {
                foreach (var filepath in Directory.GetFiles(Path.Combine(path, "Animations"), "*.py"))
                {
                    string filename = Path.GetFileName(filepath);
                    filename = filename.Substring(0, filename.Length - 3);
                    cmbAutoAnim.Items.Add(filename);

                    if (filepath == animationHandler.AnimationModule)
                    {
                        customAnimation = false;
                    }
                }
            }

            if (customAnimation && animationHandler.AnimationModule != "")
            {
                cmbAutoAnim.Items.Add("Custom Animation");
            }
        }

        private void btnExpert_Click(object sender, EventArgs e)
        {
            //txtAuthenticateExpert.ForeColor = Color.White;
            if (txtAuthenticateExpert.Text == "1")
            {
                txtAuthenticateExpert.Text = "";
                ExpertComSettingsForm expertCommSettingsForm = new ExpertComSettingsForm();
                expertCommSettingsForm.ShowDialog();
            }
            else
                //txtAuthenticateExpert.BackColorChanged = Color.Red;
                MessageBox.Show("Please enter correct password");
        }

        private void cmbAdapter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAdapter.SelectedIndex == 0)
            {
                //ApplicationController controller = new ApplicationController(this.data);
                grpExpertMode.Visible = false;
            }
            else
            {
                grpExpertMode.Visible = true;
            }
        }
    }
}
