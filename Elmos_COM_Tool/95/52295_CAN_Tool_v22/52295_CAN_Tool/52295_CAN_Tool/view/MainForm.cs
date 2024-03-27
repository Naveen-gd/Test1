using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using Device_52295_Lib;
using Can_Comm_Lib;

namespace _52295_CAN_Tool
{
    public partial class MainForm : Form
    {
        private SettingsFile _settingsFile;
        private Master _master;
        private System.Windows.Forms.Timer _autoUpdateTimer;

        #region "Animation"

        private void _animationStop()
        {
            _master.SetAnimationEnabled(false);
            buttonAnimationStartStop.Text = "START";
            // enable PWM and Dimming in Device Tabs
            for (int i = 0; i < tabControlDevices.TabPages.Count; i += 1)
            {
                ((DeviceTab)tabControlDevices.TabPages[i].Controls[0]).SetPwmBoxAccess(true);
            }
        }

        private void button_animation_load_Click(object sender, EventArgs e)
        {
            openFileDialog_animation.ShowDialog();
        }

        private void openFileDialog_animation_FileOk(object sender, CancelEventArgs e)
        {
            _master.SetAnimationEnabled(false);
            buttonAnimationStartStop.Enabled = true;
            buttonAnimationStartStop.Text = "START";
            label_animation_file.Text = Path.GetFileName(openFileDialog_animation.FileName);
            _master.ReadAnimationFile(openFileDialog_animation.FileName);
        }

        private void button_animation_startstop_Click(object sender, EventArgs e)
        {
            if (_master.GetAnimationEnabled())
            {
                _animationStop();
            }
            else
            {
                _master.SetAnimationEnabled(true);
                buttonAnimationStartStop.Text = "STOP";
                // disable PWM and Dimming in Device Tabs
                for (int i = 0; i < tabControlDevices.TabPages.Count; i += 1)
                {
                    ((DeviceTab)tabControlDevices.TabPages[i].Controls[0]).SetPwmBoxAccess(false);
                }
            }
        }

        #endregion

        #region "Automatic"

        private void checkBox_auto_write_currents_CheckedChanged(object sender, EventArgs e)
        {
            _master.SetAutoWriteCurrents(checkBox_auto_write_currents.Checked);

            for (int i = 0; i < tabControlDevices.TabPages.Count; i += 1)
            {
                ((DeviceTab)tabControlDevices.TabPages[i].Controls[0]).SetCurrentButtonsAccess(!checkBox_auto_write_currents.Checked);
            }
        }

        private void checkBox_auto_write_pwm_CheckedChanged(object sender, EventArgs e)
        {
            _master.SetAutoWritePWM(checkBox_auto_write_pwm.Checked);

            for (int i = 0; i < tabControlDevices.TabPages.Count; i += 1)
            {
                ((DeviceTab)tabControlDevices.TabPages[i].Controls[0]).SetPwmButtonsAccess(!checkBox_auto_write_pwm.Checked);
            }
        }

        private void checkBox_auto_read_full_status_CheckedChanged(object sender, EventArgs e)
        {
            _master.SetAutoReadFullStatus(checkBox_auto_read_full_status.Checked);
        }

        private void checkBox_autoReadDiag_CheckedChanged(object sender, EventArgs e)
        {
            _master.SetAutoReadDiagStatus(checkBox_autoReadDiag.Checked);
        }

        private void numericUpDownAutoWriteInterval_ValueChanged(object sender, EventArgs e)
        {
            _master.SetAutoWriteIntervalMs(Decimal.ToUInt16(numericUpDownAutoWriteInterval.Value));
        }

        #endregion

        private ushort _GetGuiUpdateInterval(){
            return Convert.ToUInt16(comboBox_gui_interval.Text.ToString());
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            CommParameters commParametersCopy = _master.GetCommParmetersCopy();
            if (_master.GetConnected())
            {
                if (commParametersCopy.defaultConfig)
                    ((DeviceTab)tabControlDevices.TabPages[tabControlDevices.SelectedIndex].Controls[0]).UpdateMiscStatus();
                else
                    ((DeviceTab)tabControlDevices.TabPages[tabControlDevices.SelectedIndex].Controls[0]).UpdateStatus();
            }

            _autoUpdateTimer.Interval = _GetGuiUpdateInterval(); // ms
            label_stat_max.Text = String.Format("{0:D}", _master.GetThreadExecTimesMaxMs());
            label_stat_mean.Text = String.Format("{0:D}", _master.GetThreadExecTimesMeanMs());
        }

        private void buttonOpenClose_Click(object sender, EventArgs e)
        {
            CommParameters commParametersCopy = _master.GetCommParmetersCopy();
            
            if (_master.GetConnected())
            {
                _animationStop();
                checkBox_auto_write_pwm.Checked = false;
                checkBox_auto_write_currents.Checked = false;
                checkBox_auto_read_full_status.Checked = false;
                buttonOpenClose.Text = "OPEN";
                groupBox_auto.Enabled = false;
                groupBox_group_auto.Enabled = false;
                groupBoxAnimation.Enabled = false;
                _autoUpdateTimer.Enabled = false;
                buttonCommSettings.Enabled = true;
                for (int i = 0; i < tabControlDevices.TabPages.Count; i += 1)
                {
                    ((DeviceTab)tabControlDevices.TabPages[i].Controls[0]).SetOpenCloseAccess(false, commParametersCopy.defaultConfig);
                }
                _master.Close();
            }
            else
            {
                // open
                try
                {
                    _master.Open();
                }
                catch (Exception x)
                {
                    MessageBox.Show("Open Exception: " + x);
                }

                if (_master.GetConnected())
                {
                    buttonOpenClose.Text = "CLOSE";
                    buttonCommSettings.Enabled = false;
                    _autoUpdateTimer.Enabled = true;
                    for (int i = 0; i < tabControlDevices.TabPages.Count; i += 1)
                    {
                        if (commParametersCopy.defaultConfig)
                        {
                            ((DeviceTab)(tabControlDevices.TabPages[i].Controls[0])).SetCommNode(255);
                            ((DeviceTab)(tabControlDevices.TabPages[i].Controls[0])).SetCommGroup(255);
                        }
                        ((DeviceTab)tabControlDevices.TabPages[i].Controls[0]).SetOpenCloseAccess(true, commParametersCopy.defaultConfig);
                        ((DeviceTab)tabControlDevices.TabPages[i].Controls[0]).ReadBusStatusAndUpdateMiscStatus();
                    }
                    if (!commParametersCopy.defaultConfig)
                    {
                        
                        groupBox_auto.Enabled = true;
                        groupBox_group_auto.Enabled = true;
                        groupBoxAnimation.Enabled = true;
                        checkBox_auto_read_full_status.Checked = true;
                    }
                }
            }
        }

        private void buttonDeviceAdd_Click(object sender, EventArgs e)
        {
            byte newIndex = (byte)tabControlDevices.TabPages.Count;

            TabPage newTabPage = new TabPage();

            newTabPage.Name = "tabPageDevice_" + newIndex;
            newTabPage.Text = "Device " + newIndex;

            DeviceTab newDeviceTab = new DeviceTab(this, newIndex, _master);

            newTabPage.Controls.Add(newDeviceTab);

            tabControlDevices.TabPages.Add(newTabPage);

            tabControlDevices.SelectedIndex = newIndex;

            newDeviceTab.UpdatePWMData();
        }

        private void label_version_Click(object sender, EventArgs e)
        {
            VersionForm vf = new VersionForm();

            string versionText = "";

            versionText += "Version 22:\r\n";
            versionText += "- improved CAN Timings dialog with individual bitrate settings\r\n";
            versionText += "\r\n";

            versionText += "Version 21:\r\n";
            versionText += "- added DIAG_RETRY_CONFIG.vs_diag_disable_threshold\r\n";
            versionText += "- added DIAG_RETRY_CONFIG.comb_retry_all\r\n";
            versionText += "\r\n";

            versionText += "Version 20:\r\n";
            versionText += "- rework of libraries\r\n";
            versionText += "- corrected descriptions for Group OPEN thresholds\r\n";
            versionText += "\r\n";

            versionText += "Version 19:\r\n";
            versionText += "- corrected descriptions for Group SHORT and OPEN_MIN thresholds\r\n";
            versionText += "\r\n";

            versionText += "Version 18:\r\n";
            versionText += "- added Descriptions to most memory fields\r\n";
            versionText += "- faster memory view scrolling\r\n";
            versionText += "- rework of internal libraries\r\n";
            versionText += "\r\n";

            versionText += "Version 17:\r\n";
            versionText += "- enabled multi-paste with Ctrl+V in Memory View\r\n";
            versionText += "- number parsing (dec. and 0x...) in Memory View\r\n";
            versionText += "- minor corrections in Memory View\r\n";
            versionText += "- added Help for Animations\r\n";
            versionText += "\r\n";

            versionText += "Version 16:\r\n";
            versionText += "- added Communication Settings Dialog\r\n";
            versionText += "- Customer Default Config can be stored and will be loaded at startup\r\n";
            versionText += "- added missing bitfields to CAN_CONFIG_3\r\n";
            versionText += "- prevent resizing of some Forms\r\n";
            versionText += "- nicer Version info :-)\r\n";
            versionText += "\r\n";

            versionText += "Version 15:\r\n";
            versionText += "- added 1M/2M bitrate\r\n";
            versionText += "- commands will always become modified\r\n";
            versionText += "- corrected EEPROM PWMIN_MID_ALT\r\n";
            versionText += "\r\n";

            versionText += "Version 14:\r\n";
            versionText += "- Quick Prog working again\r\n";
            versionText += "- Read Error set for every failed read operation and can be cleared without status read\r\n";
            versionText += "- display cycle time\r\n";
            versionText += "- full auto read enabled by default when not in default config\r\n";
            versionText += "- HW and FW version always updated after open\r\n";
            versionText += "- corrected BUS_STATUS.LED_OPEN_SHORT_*\r\n";
            versionText += "- added Parameter CRC readout\r\n";
            versionText += "- added GPIO Binning Error\r\n";
            versionText += "\r\n";

            versionText += "Version 13:\r\n";
            versionText += "- added comm cycle statistic and selectable gui update rate\r\n";
            versionText += "- improved bus performance\r\n";
            versionText += "- improved behavior of some buttons and boxes\r\n";
            versionText += "\r\n";

            versionText += "Version 12:\r\n";
            versionText += "- automatic reading and animation disabled during Default Config access\r\n";
            versionText += "- added display of set currents in mA\r\n";
            versionText += "- reworked memory access\r\n";
            versionText += "- improved window handling of EEPROM and Memory window\r\n";
            versionText += "- corrected false EEPROM verify error\r\n";
            versionText += "- corrected wrong PWM and Currents Read button\r\n";
            versionText += "\r\n";

            versionText += "Version 11:\r\n";
            versionText += "- added easy Default Config setting\r\n";
            versionText += "- added status read fail flag indication\r\n";
            versionText += "- corrected some display errors\r\n";
            versionText += "\r\n";

            versionText += "Version 10:\r\n";
            versionText += "- improved EEPROM handling\r\n";
            versionText += "- added Currents group\r\n";
            versionText += "- added automatic current write\r\n";
            versionText += "\r\n";

            versionText += "Version 9:\r\n";
            versionText += "- added a second decimal digit for ADC readings\r\n";
            versionText += "\r\n";

            versionText += "Version 8:\r\n";
            versionText += "- support for CAN sample points\r\n";
            versionText += "\r\n";

            versionText += "Version 7:\r\n";
            versionText += "- unlock will read out key before unlocking\r\n";
            versionText += "- added batch quick programming\r\n";
            versionText += "\r\n";

            versionText += "Version 6:\r\n";
            versionText += "- change of automatic write timing should have immediate effect\r\n";
            versionText += "- corrected Vector Timings\r\n";
            versionText += "\r\n";

            versionText += "Version 5:\r\n";
            versionText += "- added support for Vector USB Adapter\r\n";
            versionText += "\r\n";

            versionText += "Version 4:\r\n";
            versionText += "- improved handling of device resets\r\n";
            versionText += "- Status Leds will display active error in Bright Red\r\n";
            versionText += "- Status Leds will display previous active error in Dark Red\r\n";
            versionText += "\r\n";

            versionText += "Version 3:\r\n";
            versionText += "- corrected CAN Timing 500k/500k\r\n";
            versionText += "- added version box\r\n";
            versionText += "\r\n";

            versionText += "(c) EBL";

            vf.SetText(versionText);

            vf.ShowDialog();
        }

        private void updateCommStatus()
        {
            CommParameters commParametersCopy = _master.GetCommParmetersCopy();

            if (commParametersCopy.defaultConfig) labelCommStatus.Text = "DEFAULT";
            else labelCommStatus.Text = "CUSTOMER";
        }

        public MainForm()
        {
            InitializeComponent();

            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            this.AutoScroll = true;

            //Thread.CurrentThread.Name = "GUI95";

            if (DeviceType.IsM52295A) this.Text += " (M52295A)";
            if (DeviceType.IsE52295A) this.Text += " (E52295A)";

            // settings file
            _settingsFile = new SettingsFile("settings.txt");
            _settingsFile.loadFromFile();

            // start Master
            _master = new Master();

            // init comm parameters
            CommParameters commParametersCopy = _master.GetCommParmetersCopy();
            commParametersCopy.getFromSettingsFile(_settingsFile);
            _master.applyCommParameters(commParametersCopy);
            updateCommStatus();

            // Gui Interval
            comboBox_gui_interval.Items.Add(100);
            comboBox_gui_interval.Items.Add(250);
            comboBox_gui_interval.Items.Add(500);
            comboBox_gui_interval.Items.Add(750);
            comboBox_gui_interval.Items.Add(1000);
            comboBox_gui_interval.SelectedIndex = 2;

            // update Timer
            _autoUpdateTimer = new System.Windows.Forms.Timer();
            _autoUpdateTimer.Interval = _GetGuiUpdateInterval(); // ms
            _autoUpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            _autoUpdateTimer.Enabled = true;

            // add first device
            buttonDeviceAdd_Click(null, null);

            // debug
            #if DEBUG
            
            #endif
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _autoUpdateTimer.Stop();
            _master.AbortThread();
        }

        private void tabControlDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            _master.SelectAutoReadDevice((byte) tabControlDevices.SelectedIndex);
        }

        private void buttonCommSettings_Click(object sender, EventArgs e)
        {
            CommParameters commParametersCopy = _master.GetCommParmetersCopy();
            CommSettingsForm commSettingsForm = new CommSettingsForm(_settingsFile, commParametersCopy);
            commSettingsForm.ShowDialog();

            if (commSettingsForm.apply)
            {
                _master.applyCommParameters(commParametersCopy);
            }

            updateCommStatus();
        }

        private void buttonAnimationHelp_Click(object sender, EventArgs e)
        {
            AnimationHelpForm ahf = new AnimationHelpForm();
            ahf.ShowDialog();
        }

    }
}
