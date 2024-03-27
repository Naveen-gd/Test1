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
    internal partial class CommSettingsForm : Form
    {
        public bool apply = false;
        public bool sane = false;
        
        private CommParameters _commParametersRef;
        private SettingsFile _settingsFileRef;

        public CommSettingsForm(SettingsFile settingsFileRef, CommParameters commParametersRef)
        {
            InitializeComponent();

            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            _commParametersRef = commParametersRef;
            _settingsFileRef = settingsFileRef;

            foreach (string value in CommParameters.bitrateLabels.Values)
            {
                comboBoxCanSpeed.Items.Add(value);
            }
            comboBoxCanSpeed.SelectedIndex = (int) _commParametersRef.bitrate;

            updateGui();
        }

        private void radioButtonDefaultConfig_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDefaultConfig.Checked)
            {
                groupBox_custom_config.Enabled = false;
                _commParametersRef.setDefaultConfig();
                updateGui();
            }
        }

        private void radioButtonCustomConfig_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCustomConfig.Checked)
            {
                groupBox_custom_config.Enabled = true;
                _commParametersRef.defaultConfig = false;
                updateGui();
            }
        }

        private void updateGui()
        {
            // comm sec parameters
            textBoxCommParameters_secure_m.Text = String.Format("0x{0:X2}", _commParametersRef.secureByte_M);
            textBoxCommParameters_secure_s.Text = String.Format("0x{0:X2}", _commParametersRef.secureByte_S);

            // comm parameters
            numericUpDownTypeM_W.Value = _commParametersRef.frameType_M_W;
            numericUpDownTypeM_W3.Value = _commParametersRef.frameType_M_W3;
            numericUpDownTypeM_R.Value = _commParametersRef.frameType_M_R;
            numericUpDownTypeS_R.Value = _commParametersRef.frameType_S_R;

            // adapter
            if (_commParametersRef.adapter == CanCommAdapter.VECTOR) radioButton_vector.Checked = true;
            else radioButton_peak.Checked = true;

            int index = 0;
            foreach (KeyValuePair<CanCommBitrate, string> kvp in CommParameters.bitrateLabels)
            {
                if (_commParametersRef.bitrate == kvp.Key)
                {
                    comboBoxCanSpeed.SelectedIndex = index;
                }
                index += 1;
            }

            // mode
            if (_commParametersRef.defaultConfig) radioButtonDefaultConfig.Checked = true;
            else radioButtonCustomConfig.Checked = true;

        }

        private void sanityCheck()
        {
            sane = true;

            try { 
                _commParametersRef.secureByte_M = textBoxCommParameters_secure_m.Text.ToString().ParseAsByte();
                textBoxCommParameters_secure_m.BackColor = System.Drawing.SystemColors.Window;
            }
            catch (Exception x) { 
                textBoxCommParameters_secure_m.BackColor = Color.Red; 
                sane = false; 
            }

            try { 
                _commParametersRef.secureByte_S = textBoxCommParameters_secure_s.Text.ToString().ParseAsByte();
                textBoxCommParameters_secure_s.BackColor = System.Drawing.SystemColors.Window;
            }
            catch (Exception x) { 
                textBoxCommParameters_secure_s.BackColor = Color.Red; 
                sane = false; 
            }

            _commParametersRef.frameType_M_W3 = Convert.ToByte(numericUpDownTypeM_W3.Value);
            _commParametersRef.frameType_M_W = Convert.ToByte(numericUpDownTypeM_W.Value);
            _commParametersRef.frameType_M_R = Convert.ToByte(numericUpDownTypeM_R.Value);
            _commParametersRef.frameType_S_R = Convert.ToByte(numericUpDownTypeS_R.Value);

            if (radioButton_vector.Checked) _commParametersRef.adapter = CanCommAdapter.VECTOR;
            else _commParametersRef.adapter = CanCommAdapter.PEAK;

            foreach (KeyValuePair<CanCommBitrate, string> kvp in CommParameters.bitrateLabels)
            {
                if (comboBoxCanSpeed.Text == kvp.Value)
                {
                    _commParametersRef.bitrate = kvp.Key;
                }
            }

            if (radioButtonCustomConfig.Checked) _commParametersRef.defaultConfig = false;
            else _commParametersRef.setDefaultConfig();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            sanityCheck();
            if (sane)
            {
                _commParametersRef.setSettingsFile(_settingsFileRef);
                _settingsFileRef.saveToFile();
            }
        }

        private void button_load_Click(object sender, EventArgs e)
        {
            _settingsFileRef.loadFromFile();
            _commParametersRef.getFromSettingsFile(_settingsFileRef);
            updateGui();
            sanityCheck();
        }

        private void button_apply_Click(object sender, EventArgs e)
        {
            sanityCheck();
            if (sane)
            {
                apply = true;
                Close();
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            apply = false;
            Close();
        }

    }
}
