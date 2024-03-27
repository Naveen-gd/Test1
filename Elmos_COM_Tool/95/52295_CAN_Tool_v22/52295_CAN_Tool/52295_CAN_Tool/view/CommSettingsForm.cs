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
using _52295_CAN_Tool.view;

namespace _52295_CAN_Tool
{
    internal partial class CommSettingsForm : Form
    {
        public bool apply = false;
        
        private CommParameters _commParametersRef;
        private SettingsFile _settingsFileRef;
        private Dictionary<uint, String> _targetOperator;

        public CommSettingsForm(SettingsFile settingsFileRef, CommParameters commParametersRef)
        {
            InitializeComponent();
            dgvBZFrame.AllowUserToAddRows = false;
            
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            this.Size = new Size(600, 505);
            //this.AutoSize = true;
            txtAuthenticateExpert.Visible = false;
            btnExpert.Visible = false;
            _commParametersRef = commParametersRef;
            _settingsFileRef = settingsFileRef;

            foreach (string value in CommParameters.BITRATE_LABELS.Values)
            {
                comboBoxCanSpeed.Items.Add(value);
            }
            //comboBoxCanSpeed.SelectedIndex = (int) _commParametersRef.bitrate;
            //For Expert Mode
            // Target Operator
            _targetOperator = new Dictionary<uint, String>();
            _targetOperator[0] = "==";
            _targetOperator[1] = ">";
            _targetOperator[2] = "<";
            foreach (KeyValuePair<uint, String> br in _targetOperator)
            {
                cmbTargetOperator.Items.Add(br.Value);
            }
            cmbTargetOperator.SelectedIndex = 0; // Scope

            grpExpertCom.Visible = false;
            updateGui();
        }

        private void updateGui()
        {
            // adapter
            if (_commParametersRef.adapter == CanCommAdapter.VECTOR) 
                radioButton_vector.Checked = true;
            else 
                radioButton_peak.Checked = true;

            // default config
            if (_commParametersRef.defaultConfig)
                radioButtonDefaultConfig.Checked = true;
            else
                radioButtonCustomConfig.Checked = true;

            // bitrate config
            label_fclock_MHz.Text = String.Format("{0:D}", _commParametersRef.bitrateConfig.f_clock_mhz);

            numericUpDown_arb_brp.Value = _commParametersRef.bitrateConfig.arbBrp;
            numericUpDown_arb_tseg_1.Value = _commParametersRef.bitrateConfig.arbTseg1;
            numericUpDown_arb_tseg_2.Value = _commParametersRef.bitrateConfig.arbTseg2;

            numericUpDown_data_brp.Value = _commParametersRef.bitrateConfig.dataBrp;
            numericUpDown_data_tseg_1.Value = _commParametersRef.bitrateConfig.dataTseg1;
            numericUpDown_data_tseg_2.Value = _commParametersRef.bitrateConfig.dataTseg2;

            CanComm.ValidateBitrateConfig(_commParametersRef.adapter, ref _commParametersRef.bitrateConfig);

            label_valid.Text = String.Format("{0:D}", _commParametersRef.bitrateConfig.validated);

            label_arb_bitrate.Text = String.Format("{0:D}", _commParametersRef.bitrateConfig.arbBitrate);
            label_arb_sp.Text = String.Format("{0:F}", _commParametersRef.bitrateConfig.arbSP);
            label_data_bitrate.Text = String.Format("{0:D}", _commParametersRef.bitrateConfig.dataBitrate);
            label_data_sp.Text = String.Format("{0:F}", _commParametersRef.bitrateConfig.dataSP);

            // comm sec parameters
            textBoxCommParameters_secure_m.Text = String.Format("0x{0:X2}", _commParametersRef.secureByte_M);
            textBoxCommParameters_secure_s.Text = String.Format("0x{0:X2}", _commParametersRef.secureByte_S);

            // comm parameters
            numericUpDownTypeM_W.Value = _commParametersRef.frameType_M_W;
            numericUpDownTypeM_W3.Value = _commParametersRef.frameType_M_W3;
            numericUpDownTypeM_R.Value = _commParametersRef.frameType_M_R;
            numericUpDownTypeS_R.Value = _commParametersRef.frameType_S_R;

            // enables
            numericUpDown_arb_brp.Enabled = !radioButtonDefaultConfig.Checked && _commParametersRef.bitrateConfig.allowBrp;
            numericUpDown_data_brp.Enabled = !radioButtonDefaultConfig.Checked && _commParametersRef.bitrateConfig.allowBrp;

            numericUpDown_arb_tseg_1.Enabled = !radioButtonDefaultConfig.Checked;
            numericUpDown_arb_tseg_2.Enabled = !radioButtonDefaultConfig.Checked;
            numericUpDown_data_tseg_1.Enabled = !radioButtonDefaultConfig.Checked;
            numericUpDown_data_tseg_2.Enabled = !radioButtonDefaultConfig.Checked;

            textBoxCommParameters_secure_m.Enabled = !radioButtonDefaultConfig.Checked;
            textBoxCommParameters_secure_s.Enabled = !radioButtonDefaultConfig.Checked;
            numericUpDownTypeM_W.Enabled = !radioButtonDefaultConfig.Checked;
            numericUpDownTypeM_W3.Enabled = !radioButtonDefaultConfig.Checked;
            numericUpDownTypeM_R.Enabled = !radioButtonDefaultConfig.Checked;
            numericUpDownTypeS_R.Enabled = !radioButtonDefaultConfig.Checked;

            button_load_bitrate.Enabled = !radioButtonDefaultConfig.Checked;
        }

        private void button_save_Click(object sender, EventArgs e)
        {
                _commParametersRef.setToSettingsFile(_settingsFileRef);
                _settingsFileRef.saveToFile();
        }

        private void button_load_Click(object sender, EventArgs e)
        {
            _settingsFileRef.loadFromFile();
            _commParametersRef.getFromSettingsFile(_settingsFileRef);
            updateGui();
        }

        private void button_apply_Click(object sender, EventArgs e)
        {
            if (CanComm.ValidateBitrateConfig(_commParametersRef.adapter, ref _commParametersRef.bitrateConfig))
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

        private void button_load_bitrate_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<CanCommBitrate, string> kvp in CommParameters.BITRATE_LABELS)
            {
                if (comboBoxCanSpeed.Text == kvp.Value)
                {
                    _commParametersRef.bitrateConfig = CanComm.GetBitrateConfigForBitrate(_commParametersRef.adapter, kvp.Key);
                    updateGui();
                }
            }

        }

        private void setAdapter(CanCommAdapter adapter)
        {
            _commParametersRef.adapter = adapter;

            _commParametersRef.patchAdapterSpecificSettings();

            if (radioButtonDefaultConfig.Checked)
                _commParametersRef.setDefaultConfig();

            updateGui();
        }

        private void radioButton_peak_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_peak.Checked)
            {
                //this.Size = new Size(825, 688);
                this.AutoSize = true;
                txtAuthenticateExpert.Visible = false;
                btnExpert.Visible = false;
                grpExpertCom.Visible = false;
                this.AutoSize = false;
                setAdapter(CanCommAdapter.PEAK);
            }
        }

        private void radioButton_vector_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_vector.Checked)
            {
                //this.Size = new Size(825, 688);
                this.AutoSize = true;
                txtAuthenticateExpert.Visible = false;
                btnExpert.Visible = false;
                grpExpertCom.Visible = false;
                this.AutoSize = false;
                setAdapter(CanCommAdapter.VECTOR);
            }
        }
        private void radioButton_ComBox_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton_ComBox.Checked)
            {
                //this.Size = new Size(1734, 775);
                this.AutoSize= true;
                txtAuthenticateExpert.Visible = true;
                btnExpert.Visible = true;
                //setAdapter(CanCommAdapter.COMBOX);
            }
        }

        private void radioButtonDefaultConfig_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDefaultConfig.Checked)
            {
                _commParametersRef.setDefaultConfig();
                updateGui();
            }
        }

        private void radioButtonCustomConfig_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCustomConfig.Checked)
            {
                _commParametersRef.defaultConfig = false;
                updateGui();
            }
        }

        private void numericUpDown_arb_brp_ValueChanged(object sender, EventArgs e)
        {
            if (_commParametersRef.bitrateConfig.allowBrp)
            {
                _commParametersRef.bitrateConfig.arbBrp = Convert.ToByte(numericUpDown_arb_brp.Value);
            }
            updateGui();
        }

        private void numericUpDown_arb_tseg_1_ValueChanged(object sender, EventArgs e)
        {
            _commParametersRef.bitrateConfig.arbTseg1 = Convert.ToByte(numericUpDown_arb_tseg_1.Value);
            updateGui();
        }

        private void numericUpDown_arb_tseg_2_ValueChanged(object sender, EventArgs e)
        {
            _commParametersRef.bitrateConfig.arbTseg2 = Convert.ToByte(numericUpDown_arb_tseg_2.Value);
            updateGui();
        }

        private void numericUpDown_data_brp_ValueChanged(object sender, EventArgs e)
        {
            if (_commParametersRef.bitrateConfig.allowBrp)
            {
                _commParametersRef.bitrateConfig.dataBrp = Convert.ToByte(numericUpDown_data_brp.Value);
            }
            updateGui();
        }

        private void numericUpDown_data_tseg_1_ValueChanged(object sender, EventArgs e)
        {
            _commParametersRef.bitrateConfig.dataTseg1 = Convert.ToByte(numericUpDown_data_tseg_1.Value);
            updateGui();
        }

        private void numericUpDown_data_tseg_2_ValueChanged(object sender, EventArgs e)
        {
            _commParametersRef.bitrateConfig.dataTseg2 = Convert.ToByte(numericUpDown_data_tseg_2.Value);
            updateGui();
        }

        private void textBoxCommParameters_secure_m_FokusLeave(object sender, EventArgs e)
        {
            try
            {
                _commParametersRef.secureByte_M = textBoxCommParameters_secure_m.Text.ToString().ParseAsByte();
                textBoxCommParameters_secure_m.BackColor = System.Drawing.SystemColors.Window;
            }
            catch (Exception x)
            {
                textBoxCommParameters_secure_m.BackColor = Color.Red;
            }
            updateGui();
        }

        private void textBoxCommParameters_secure_s_FokusLeave(object sender, EventArgs e)
        {
            try
            {
                _commParametersRef.secureByte_S = textBoxCommParameters_secure_s.Text.ToString().ParseAsByte();
                textBoxCommParameters_secure_s.BackColor = System.Drawing.SystemColors.Window;
            }
            catch (Exception x)
            {
                textBoxCommParameters_secure_s.BackColor = Color.Red;
            }
            updateGui();
        }

        private void numericUpDownTypeM_W_ValueChanged(object sender, EventArgs e)
        {
            _commParametersRef.frameType_M_W = Convert.ToByte(numericUpDownTypeM_W.Value);
            updateGui();
        }

        private void numericUpDownTypeM_W3_ValueChanged(object sender, EventArgs e)
        {
            _commParametersRef.frameType_M_W3 = Convert.ToByte(numericUpDownTypeM_W3.Value);
            updateGui();
        }

        private void numericUpDownTypeM_R_ValueChanged(object sender, EventArgs e)
        {
            _commParametersRef.frameType_M_R = Convert.ToByte(numericUpDownTypeM_R.Value);
            updateGui();
        }

        private void numericUpDownTypeS_R_ValueChanged(object sender, EventArgs e)
        {
            _commParametersRef.frameType_S_R = Convert.ToByte(numericUpDownTypeS_R.Value);
            updateGui();
        }

        private void btnExpert_Click(object sender, EventArgs e)
        {
            //txtAuthenticateExpert.ForeColor = Color.White;
            if (txtAuthenticateExpert.Text == "1")
            {
                txtAuthenticateExpert.Text = "";
                grpExpertCom.Visible = true;
                this.AutoSize = true;
                //ExpertComSettingForm expertCommSettingsForm = new ExpertComSettingForm();
                //ExpertComSettingForm expertCommSettingsForm = new ExpertComSettingForm(_commParametersRef.adapter);
                //expertCommSettingsForm.ShowDialog();
            }
            else
                //txtAuthenticateExpert.BackColorChanged = Color.Red;
                MessageBox.Show("Please enter password");
        }

        private void cmbTargetOperator_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void nudBzCount_ValueChanged(object sender, EventArgs e)
        {
            int rowCount = dgvBZFrame.Rows.Count;
            int value = Convert.ToInt16(nudBzCount.Value);

            if(value > rowCount)
            {
                dgvBZFrame.Rows.Add("");
            }
            else if(value < rowCount)
            {
                dgvBZFrame.Rows.RemoveAt(rowCount-1);
            }
          
        }

        private void dgv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv.CurrentCell is DataGridViewComboBoxCell)
            {
                DataGridViewComboBoxEditingControl combo = e.Control as DataGridViewComboBoxEditingControl;
                combo.SelectedIndexChanged -= ComboBox_SelectedIndexChanged;
                combo.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataGridViewComboBoxEditingControl combobox = sender as DataGridViewComboBoxEditingControl;
            int rowIndex = dgvBZFrame.CurrentCell.RowIndex;
            if (combobox.SelectedItem != null)
            {
                
                int selectedValue = Convert.ToInt32(combobox.SelectedItem.ToString());
                for(int i = 0; i < combobox.Items.Count; i++)
                {
                    if(i<selectedValue)
                    {
                        dgvBZFrame.Columns[i + 2].Visible = true;
                        dgvBZFrame.Rows[rowIndex].Cells[i + 2].ReadOnly = false;
                        dgvBZFrame.Rows[rowIndex].Cells[i + 2].Value = "0x00";
                        dgvBZFrame.Rows[rowIndex].Cells[i + 2].Style.BackColor = Color.White;
                    }
                    else
                    {
                        dgvBZFrame.Rows[rowIndex].Cells[i + 2].ReadOnly = true;
                        dgvBZFrame.Rows[rowIndex].Cells[i + 2].Style.BackColor = Color.Gray;
                    }
                   
                }
            }
        }

        private void dgv_CurrentCellChanged(object sender, EventArgs e)
        {
            //DataGridView dgv = sender as DataGridView;
            //if (dgv.CurrentCell is DataGridViewComboBoxCell)
            //{
            //    DataGridViewComboBoxEditingControl combo = e.Control as DataGridViewComboBoxEditingControl;
            //    combo.SelectedIndexChanged -= ComboBox_SelectedIndexChanged;
            //    combo.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            //}
        }

        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridView dgv = sender as DataGridView;
            //if (dgv.CurrentCell is DataGridViewComboBoxCell)
            //{
            //    DataGridViewComboBoxEditingControl combo = e.Control as DataGridViewCellEventArgs;
            //    combo.SelectedIndexChanged -= ComboBox_SelectedIndexChanged;
            //    combo.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            //}
        }

        private void dgvBZFrame_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
