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
using System.Linq;

using FtdiLib;
using Device_52294_Lib;
using UcanCommLib;
using Extensions;
using _52294_UCAN_Tool.view;

namespace _52294_UCAN_Tool
{
    public partial class MainForm : Form
    {
        private UcanMaster _ucanMaster;
        private System.Windows.Forms.Timer _autoUpdateTimer;
        private TracerForm _tracerForm;

        #region "Animation"

        private void _animationStop()
        {
            _ucanMaster.SetAnimationEnabled(false);
            button_animation_startstop.Text = "START";
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
            _ucanMaster.SetAnimationEnabled(false);
            button_animation_startstop.Enabled = true;
            button_animation_startstop.Text = "START";
            label_animation_file.Text = Path.GetFileName(openFileDialog_animation.FileName);
            _ucanMaster.ReadAnimationFile(openFileDialog_animation.FileName);
        }

        private void button_animation_startstop_Click(object sender, EventArgs e)
        {
            if (_ucanMaster.GetAnimationEnabled())
            {
                _animationStop();
            }
            else
            {
                _ucanMaster.SetAnimationEnabled(true);
                button_animation_startstop.Text = "STOP";
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
            _ucanMaster.SetAutoWriteCurrents(checkBoxAutoWriteCurrents.Checked);

            for (int i = 0; i < tabControlDevices.TabPages.Count; i += 1)
            {
                ((DeviceTab)tabControlDevices.TabPages[i].Controls[0]).SetCurrentButtonsAccess(!checkBoxAutoWriteCurrents.Checked);
            }
        }

        private void checkBox_auto_write_pwm_CheckedChanged(object sender, EventArgs e)
        {
            _ucanMaster.SetAutoWritePWM(checkBoxAutoWritePwm.Checked);

            for (int i = 0; i < tabControlDevices.TabPages.Count; i += 1)
            {
                ((DeviceTab)tabControlDevices.TabPages[i].Controls[0]).SetPwmButtonsAccess(!checkBoxAutoWritePwm.Checked);
            }
        }

        private void checkBox_auto_read_full_status_CheckedChanged(object sender, EventArgs e)
        {
            _ucanMaster.SetAutoReadFullStatus(checkBoxAutoReadFullStatus.Checked);
        }

        private void checkBox_autoReadDiag_CheckedChanged(object sender, EventArgs e)
        {
            _ucanMaster.SetAutoReadDiagStatus(checkBoxAutoReadDiag.Checked);
        }

        private void numericUpDownAutoWriteInterval_ValueChanged(object sender, EventArgs e)
        {
            _ucanMaster.SetAutoWriteIntervalMs(Decimal.ToUInt16(numericUpDownAutoWriteInterval.Value));
        }

        private void checkBox_auto_imm_pulse0_CheckedChanged(object sender, EventArgs e)
        {
            _ucanMaster.SetAutoImmPulse0All(checkBox_auto_imm_pulse0.Checked);
        }

        private void checkBox_auto_imm_current0_CheckedChanged(object sender, EventArgs e)
        {
            _ucanMaster.SetAutoImmCurrent0All(checkBox_auto_imm_current0.Checked);
        }

        private void checkBox_auto_imm_enables_CheckedChanged(object sender, EventArgs e)
        {
            _ucanMaster.SetAutoImmEnablesAll(checkBox_auto_imm_enables.Checked);
        }

        #endregion

        private Dictionary<uint, String> _bitrates;
        private Dictionary<uint, String> _header;
        private Dictionary<uint, String> _parity;
        private Dictionary<uint, String> _sync;
        private List<FtdiBase.DeviceListEntry> _ucanDevices;

        private bool _hiddenMode;

        private ushort _GetGuiUpdateInterval(){
            return Convert.ToUInt16(comboBox_gui_interval.Text.ToString());
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            ((DeviceTab)tabControlDevices.TabPages[tabControlDevices.SelectedIndex].Controls[0]).UpdateStatus();
            _autoUpdateTimer.Interval = _GetGuiUpdateInterval(); // ms
            label_stat_max.Text = String.Format("{0:D}", _ucanMaster.GetThreadExecTimesMaxMs());
            label_stat_mean.Text = String.Format("{0:D}", _ucanMaster.GetThreadExecTimesMeanMs());
        }

        private void buttonOpenClose_Click(object sender, EventArgs e)
        {
            if (_ucanMaster.GetConnected())
            {
                // DO CLOSE

                _animationStop();
                checkBoxAutoWritePwm.Checked = false;
                checkBoxAutoWriteCurrents.Checked = false;
                checkBoxAutoReadFullStatus.Checked = false;
                checkBoxAutoReadDiag.Checked = false;
                buttonOpenClose.Text = "OPEN";
                groupBox_auto.Enabled = false;
                groupBoxAnimation.Enabled = false;
                _autoUpdateTimer.Enabled = false;
                groupBox_sleep.Enabled = false;
                groupBox_trace.Enabled = false;
                comboBox_commDevices.Enabled = true;
                groupBox_adapter.Enabled = true;
                checkBox_pg_mode.Enabled = true;
                groupBox_direct_bus.Enabled = false;
                groupBox_auto_imm.Enabled = false;
                for (int i = 0; i < tabControlDevices.TabPages.Count; i += 1)
                {
                    // For debug purpose change the bool to true
                    ((DeviceTab)tabControlDevices.TabPages[i].Controls[0]).SetOpenCloseAccess(false);
                }
                _ucanMaster.Close();
            }
            else
            {
                // open
                try
                {
                    // Magic mode select + open
                    FtdiBase.DeviceListEntry dev = _ucanDevices.ElementAt(comboBox_commDevices.SelectedIndex);
                    if (checkBox_pg_mode.Checked)
                    {
                        if (dev.Channels == 2)
                        {
                            _ucanMaster.ucanCommRef.SetMode(FtdiBitBangUart.Mode.UART_RX_PG_TX);
                            _ucanMaster.OpenChannelByDevice(dev, 0);
                        }
                    }
                    else
                    {
                        // CH0 -> UART
                        _ucanMaster.ucanCommRef.SetMode(FtdiBitBangUart.Mode.UART_RX_TX);
                        _ucanMaster.OpenChannelByDevice(dev, 0);
                        // CH1 -> LIN
                    }
                    // some config
                    _ucanMaster.SetCommBitrate(_bitrates.ElementAt(comboBox_comm_bitrate.SelectedIndex).Key);
                    _ucanMaster.SetCommParity(_parity.ElementAt(comboBox_comm_parity.SelectedIndex).Key);
                }
                catch (Exception x)
                {
                    MessageBox.Show("Open Exception: " + x);
                }

                //commented for debbug purpose
                //if (_ucanMaster.GetConnected())
                if (true)
                {
                    // DID OPEN

                    buttonOpenClose.Text = "CLOSE";
                    comboBox_commDevices.Enabled = false;
                    for (int i = 0; i < tabControlDevices.TabPages.Count; i += 1)
                    {
                        ((DeviceTab)tabControlDevices.TabPages[i].Controls[0]).SetOpenCloseAccess(true);
                        //TODO: ((DeviceTab)tabControlDevices.TabPages[i].Controls[0]).ReadDeviceInfo();
                    }
                    _autoUpdateTimer.Enabled = true;
                    groupBox_auto.Enabled = true;
                    groupBoxAnimation.Enabled = true;
                    groupBox_sleep.Enabled = true;
                    groupBox_trace.Enabled = true;
                    checkBox_wake_symbol.Checked = true;
                    checkBox_wake_ack.Checked = true;
                    checkBox_pg_mode.Enabled = false;
                    groupBox_direct_bus.Enabled = true;
                    groupBox_auto_imm.Enabled = true;
                }
            }
        }

        private void buttonDeviceAdd_Click(object sender, EventArgs e)
        {
            byte newIndex = (byte)(tabControlDevices.TabPages.Count);

            TabPage newTabPage = new TabPage();

            newTabPage.Name = "tabPageDevice_" + newIndex;
            newTabPage.Text = "Device " + newIndex;

            DeviceTab newDeviceTab = new DeviceTab(this, newIndex, _ucanMaster);

            newTabPage.Controls.Add(newDeviceTab);

            tabControlDevices.TabPages.Add(newTabPage);
            
            tabControlDevices.SelectedIndex = newIndex;

            newDeviceTab.UpdatePWMData();
        }

        private void label_version_Click(object sender, EventArgs e)
        {
            VersionForm vf = new VersionForm();

            string versionString = "";

            versionString += "Version 21:\r\n";
            versionString += "- improved break generation\r\n";
            versionString += "\r\n";

            versionString += "Version 20:\r\n";
            versionString += "- corrected parameter txt files\r\n";
            versionString += "\r\n";

            versionString += "Version 19:\r\n";
            versionString += "- added 250k bitrate\r\n";
            versionString += "- allow faster cycles\r\n";
            versionString += "- internal updates\r\n";
            versionString += "\r\n";

            versionString += "Version 18:\r\n";
            versionString += "- corrected STANDALONE.UART_CONFIG.break_sel description\r\n";
            versionString += "- minor improvements\r\n";
            versionString += "\r\n";

            versionString += "Version 17:\r\n";
            versionString += "- improved Wakeup Symbol generation\r\n";
            versionString += "\r\n";

            versionString += "Version 16:\r\n";
            versionString += "- improved support for dual channel FTDI devices\r\n";
            versionString += "- added parameter read/write target sel (OTP or RAM)\r\n";
            versionString += "\r\n";

            versionString += "Version 15:\r\n";
            versionString += "- added support for dual channel FTDI devices\r\n";
            versionString += "- corrected bug after closing tracer window and opening again\r\n";
            versionString += "\r\n";

            versionString += "Version 14:\r\n";
            versionString += "- added tracer\r\n";
            versionString += "- added missing check for read data CRC\r\n";
            versionString += "\r\n";

            versionString += "Version 13:\r\n";
            versionString += "- rework of libraries\r\n";
            versionString += "\r\n";

            versionString += "Version 12:\r\n";
            versionString += "- added more bitrate\r\n";
            versionString += "- added more break lengths\r\n";
            versionString += "\r\n";

            versionString += "Version 11:\r\n";
            versionString += "- corrected livecounter handling\r\n";
            versionString += "\r\n";

            versionString += "Version 10:\r\n";
            versionString += "- added configurable break length\r\n";
            versionString += "- changed Read Error to more generic Comm Error\r\n";
            versionString += "- corrected Sync32 communication\r\n";
            versionString += "- implemented writing of BUS_CONFIG.ASSERT_DIAG\r\n";
            versionString += "- implemented writing of BUS_CONFIG.SRAM_SEL\r\n";
            versionString += "- implemented writing of BUS_CONFIG.BUS_DERATE_GAIN\r\n";
            versionString += "- implemented writing of BUS_CONFIG commands\r\n";
            versionString += "\r\n";

            versionString += "Version 9:\r\n";
            versionString += "- split Wakeup into Symbol and Ack\r\n";
            versionString += "- corrected VS Derate Range Parameter Description\r\n";
            versionString += "- improved performance during read error\r\n";
            versionString += "\r\n";

            versionString += "Version 8:\r\n";
            versionString += "- added Descriptions to Parameters\r\n";
            versionString += "- added sortable index column to memory views\r\n";
            versionString += "- added column to indicate read-only to memory views\r\n";
            versionString += "\r\n";

            versionString += "Version 7:\r\n";
            versionString += "- added reading of 48er Wizard CFG Files\r\n";
            versionString += "\r\n";

            versionString += "Version 6:\r\n";
            versionString += "- corrected Parameters load/save\r\n";
            versionString += "- new Version Dialog\r\n";
            versionString += "- added Animation Help\r\n";
            versionString += "\r\n";
            
            versionString += "Version 5:\r\n";
            versionString += "- extended Parameter Bitfields\r\n";
            versionString += "- corrected displayed Temp\r\n";
            versionString += "- corrected displayed IC+FW Version\r\n";
            versionString += "- corrected 4 Byte Header\r\n";
            versionString += "\r\n";

            versionString += "Version 4:\r\n";
            versionString += "- protect already written parameters\r\n";
            versionString += "- correct 10 bit data handling\r\n";
            versionString += "- corrected some standalone addresses\r\n";
            versionString += "- added comm config\r\n";
            versionString += "\r\n";

            versionString += "Version 3:\r\n";
            versionString += "- Parameter writing\r\n";
            versionString += "\r\n";

            versionString += "Version 2:\r\n";
            versionString += "- corrected device readout\r\n";
            versionString += "\r\n";

            versionString += "Version 1:\r\n";
            versionString += "- initial version\r\n";
            versionString += "\r\n";

            versionString += "(c) EBL";

            vf.SetText(versionString);

            vf.ShowDialog();

            if (vf.passOk)
                  _UnlockHiddenMode();
        }

        private void _UnlockHiddenMode(){
            if (!_hiddenMode)
            {
                _hiddenMode = true;
                groupBox_hidden.Visible = true;
                this.Size = new Size(this.Size.Width+10, this.Size.Height); // re-adjust
            }
        }

        private void _AddBitrate(UInt32 bitrate){
            _bitrates[bitrate] = String.Format("{0:D} kbaud", bitrate / 1000);
        }

        public MainForm()
        {
            InitializeComponent();

            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.AutoScroll = true;
            
            MaximizeBox = false;
            
            //this.Size = new Size(1010, 884);
            grpBoxExpert.Visible = false;

            this.AutoSize = true;

            //Thread.CurrentThread.Name = "GUI94";

            if (DeviceType.IsM52294A) this.Text += " (M52294A)";

            // start UcanMaster
            _ucanMaster = new UcanMaster();

            // Tracer
            _tracerForm = new TracerForm();
            _ucanMaster.ucanCommRef.sendBreakCallback = _tracerForm.tracer.NewFrame;
            _ucanMaster.ucanCommRef.receiveDataCallback = _tracerForm.tracer.FrameData;

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

            // bitrates
            _bitrates = new Dictionary<uint, String>();
            _AddBitrate(100000);
            _AddBitrate(250000);
            _AddBitrate(400000);
            _AddBitrate(480000);
            _AddBitrate(500000);
            _AddBitrate(600000);
            _AddBitrate(750000);
            _AddBitrate(800000);
            _AddBitrate(960000);
            _AddBitrate(1000000);
            _AddBitrate(1200000);
            _AddBitrate(1500000);
            _AddBitrate(1600000);
            _AddBitrate(2000000);
            foreach (KeyValuePair<uint, String> br in _bitrates)
            {
                comboBox_comm_bitrate.Items.Add(br.Value);
            }
            comboBox_comm_bitrate.SelectedIndex = 4;

            // header
            _header = new Dictionary<uint, String>();
            _header[3] = "3 Byte";
            _header[4] = "4 Byte";
            foreach (KeyValuePair<uint, String> br in _header)
            {
                comboBox_comm_header.Items.Add(br.Value);
            }
            comboBox_comm_header.SelectedIndex = 0;

            // parity
            _parity = new Dictionary<uint, String>();
            _parity[0] = "EVEN";
            _parity[1] = "ODD";
            _parity[2] = "ZERO";
            _parity[3] = "NONE";
            foreach (KeyValuePair<uint, String> br in _parity)
            {
                comboBox_comm_parity.Items.Add(br.Value);
            }
            comboBox_comm_parity.SelectedIndex = 0;

            // sync
            _sync = new Dictionary<uint, String>();
            _sync[8] = "8 Bit";
            _sync[32] = "32 Bit";
            foreach (KeyValuePair<uint, String> br in _sync)
            {
                comboBox_comm_sync.Items.Add(br.Value);
            }
            comboBox_comm_sync.SelectedIndex = 0;

            for (uint i = 0; i < 43; i += 1)
            {
                comboBox_comm_break.Items.Add(String.Format("{0:F}", (9.0 + 0.5 * i)));
            }
            comboBox_comm_break.SelectedIndex = 9;

            // device list
            _ucanDevices = _ucanMaster.GetUCanCommDevices();
            foreach (FtdiBase.DeviceListEntry dev in _ucanDevices)
            {
                comboBox_commDevices.Items.Add(dev.Description + String.Format(" ({0:D} ch)", dev.Channels));
                comboBox_commDevices.Items.Add("COMBOX");
            }
            if (_ucanDevices.Count > 0)
                comboBox_commDevices.SelectedIndex = 0;

            // wakeup length
            for (int i = 100; i < 2100; i += 100)
            {
                comboBox_wakeup_length.Items.Add(i);
            }
            comboBox_wakeup_length.SelectedIndex = 4;

            // add first device
            buttonDeviceAdd_Click(null, null);

            // Pattern gen Mode
            checkBox_pg_mode.Checked = false;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _autoUpdateTimer.Stop();
            _ucanMaster.AbortThread();
        }

        private void tabControlDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ucanMaster.SelectAutoReadDevice((byte) tabControlDevices.SelectedIndex);
        }

        private void comboBox_comm_bitrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ucanMaster.SetCommBitrate(_bitrates.ElementAt(comboBox_comm_bitrate.SelectedIndex).Key);
        }

        private void comboBox_comm_header_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ucanMaster.SetCommHeader(_header.ElementAt(comboBox_comm_header.SelectedIndex).Key);
        }

        private void comboBox_comm_parity_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ucanMaster.SetCommParity(_parity.ElementAt(comboBox_comm_parity.SelectedIndex).Key);
        }

        private void comboBox_comm_sync_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ucanMaster.SetCommSync(_sync.ElementAt(comboBox_comm_sync.SelectedIndex).Key);

            if (_sync.ElementAt(comboBox_comm_sync.SelectedIndex).Key == 32)
            {
                comboBox_comm_parity.SelectedIndex = 3; // NONE
                comboBox_comm_parity.Enabled = false;
            }
            else
            {
                comboBox_comm_parity.Enabled = true;
            }
        }

        private void comboBox_comm_break_SelectedIndexChanged(object sender, EventArgs e)
        {
            double breakLength = Convert.ToDouble(comboBox_comm_break.Text);
            _ucanMaster.SetCommBreakLength(breakLength);
        }

        private void button_wakeup_Click(object sender, EventArgs e)
        {
            _ucanMaster.SendWakeup(checkBox_wake_symbol.Checked, checkBox_wake_ack.Checked);
            //TODO: ((DeviceTab)tabControlDevices.TabPages[tabControlDevices.SelectedIndex].Controls[0]).ReadDeviceInfo();
        }

        private void button_sleep_Click(object sender, EventArgs e)
        {
            checkBoxAutoReadFullStatus.Checked = false;
            checkBoxAutoReadDiag.Checked = false;
            checkBoxAutoWritePwm.Checked = false;
            checkBoxAutoWriteCurrents.Checked = false;
            _ucanMaster.SendSleepBroadcast();
        }

        private void buttonAnimationHelp_Click(object sender, EventArgs e)
        {
            AnimationHelpForm ahf = new AnimationHelpForm();
            ahf.ShowDialog();
        }

        private void button_tracer_Click(object sender, EventArgs e)
        {
            _tracerForm.Show();
            _tracerForm.BringToFront();
        }

        private void button_set_break_Click(object sender, EventArgs e)
        {
            _ucanMaster.SetBreak();
        }

        private void button_clear_break_Click(object sender, EventArgs e)
        {
            _ucanMaster.ClearBreak();
        }

        private void comboBox_wakeup_length_SelectedIndexChanged(object sender, EventArgs e)
        {
            uint wakeupLength = Convert.ToUInt16(comboBox_wakeup_length.Text);
            _ucanMaster.SetCommWakeupLength(wakeupLength);
        }

        private void btnExpert_Click(object sender, EventArgs e)
        {
            //txtAuthenticateExpert.ForeColor = Color.White;
            if (txtAuthenticateExpert.Text == "1")
            {
                txtAuthenticateExpert.Text = "";
                ExpertComSettingsForm expertCommSettingsForm = new ExpertComSettingsForm();
                //ExpertComSettingForm expertCommSettingsForm = new ExpertComSettingForm(_commParametersRef.adapter);
                expertCommSettingsForm.ShowDialog();
            }
            else
                //txtAuthenticateExpert.BackColorChanged = Color.Red;
                MessageBox.Show("Please enter correct password");
        }

        private void comboBox_commDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox_commDevices.SelectedItem == "COMBOX")
            {
                grpBoxExpert.Visible = true;
            }
            else
                grpBoxExpert.Visible = false;
        }
    }
}
