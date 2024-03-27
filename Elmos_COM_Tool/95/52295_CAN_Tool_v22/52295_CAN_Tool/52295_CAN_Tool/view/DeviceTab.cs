using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using Device_52295_Lib;
using Can_Comm_Lib;

namespace _52295_CAN_Tool
{
    internal partial class DeviceTab : UserControl
    {
        private byte _deviceId;

        private Master _masterRef;
        private MainForm _mainFormRef;

        private Device _deviceCopy;

        private List<Memory> _eepromFormMemories;
        private MemForm _eepromForm;

        private List<Memory> _busFormMemories;
        private MemForm _busForm;

        private void _SetStatusLed(bool value, StatusLedControl led)
        {
            if (value)
                led.CheckState = CheckState.Checked;
            else
            {
                if (led.Enabled && led.ThreeState && led.Checked) led.CheckState = CheckState.Indeterminate;
                else led.CheckState = CheckState.Unchecked;
            }
        }

        private void _UpdateMemFormMemories()
        {
            _eepromFormMemories = new List<Memory> { _deviceCopy.eeprom };
            _busFormMemories = new List<Memory> { _deviceCopy.busConfig, _deviceCopy.busStatus };
        }

        public Device device
        {
            get { return _deviceCopy; }
        }

        public DeviceTab(MainForm mainForm, byte devID, Master masterRef)
        {
            InitializeComponent();

            if (DeviceType.IsE52295A)
            {
                statusLed_pwmin_mid.Visible = true;
                label_fw_version_nr.Visible = true;
                label_fw_version_num.Visible = true;
                label_param_crc.Visible = true;
                label_param_crc_val.Visible = true;
            }
            else
            {
                statusLed_pwmin_mid.Visible = false;
                label_fw_version_nr.Visible = false;
                label_fw_version_num.Visible = false;
                label_param_crc.Visible = false;
                label_param_crc_val.Visible = false;
            }

            _mainFormRef = mainForm;
            _masterRef = masterRef;
            _deviceId = devID;

            byte masterId = _masterRef.AddDevice();

            Debug.Assert(_deviceId == masterId);

            // get initial copy, will only be updated afterwards
            _deviceCopy = _masterRef.GetDeviceCopy(_deviceId);

            _UpdateMemFormMemories();

            _eepromForm = new MemForm(String.Format("Device {0:D} EEPROM", _deviceId), _eepromFormMemories);
            _busForm = new MemForm(String.Format("Device {0:D} CONFIG + STATUS", _deviceId), _busFormMemories);

            // default config
            numericUpDownCommNode.Value = _deviceId;
            numericUpDownCommGroup.Value = _deviceId / 3;
            numericUpDownCommIndex.Value = _deviceId % 3;

            // first update
            UpdatePWMData();
            UpdateCurrentsData();

            // will be called in MainForm: updateDeviceGroups();
        }

        public void SetPwmBoxAccess(bool value)
        {
            this.groupBoxPWM.Enabled = value;
        }

        public void SetPwmButtonsAccess(bool value)
        {
            this.buttonPwmWrite.Enabled = value;
            this.buttonPwmRead.Enabled = value;
            this.buttonPwmZero.Enabled = value;
        }

        public void SetCurrentButtonsAccess(bool value)
        {
            this.buttonCurrentWrite.Enabled = value;
            this.buttonCurrentRead.Enabled = value;
            this.buttonCurrentZero.Enabled = value;
        }

        public void SetOpenCloseAccess(bool value, bool default_config)
        {
            // always available
            this.groupBoxInfo.Enabled = value;
            this.groupBoxEE.Enabled = value;
            this.groupBoxBusMem.Enabled = value;
            // not in default config
            this.groupBoxReset.Enabled = value && !default_config;
            this.groupBoxPWM.Enabled = value && !default_config;
            this.groupBoxCurrent.Enabled = value && !default_config;
            this.groupBoxCommStatus.Enabled = value && !default_config;
            this.groupBoxDiag.Enabled = value && !default_config;
            this.groupBoxGpio.Enabled = value && !default_config;
            this.groupBoxInternal.Enabled = value && !default_config;
            this.groupBoxPwmin.Enabled = value && !default_config;
            this.groupBoxLedStatus.Enabled = value && !default_config;
            this.groupBoxVs.Enabled = value && !default_config;
            this.groupBoxSense0.Enabled = value && !default_config;
            this.groupBoxSense1.Enabled = value && !default_config;
            this.groupBoxTemp.Enabled = value && !default_config;
        }

        public void UpdatePWMData()
        {
            byte[] pwmData = new byte[16];
            pwmData[0] = Decimal.ToByte(numericUpDownPWM_0.Value);
            pwmData[1] = Decimal.ToByte(numericUpDownPWM_1.Value);
            pwmData[2] = Decimal.ToByte(numericUpDownPWM_2.Value);
            pwmData[3] = Decimal.ToByte(numericUpDownPWM_3.Value);
            pwmData[4] = Decimal.ToByte(numericUpDownPWM_4.Value);
            pwmData[5] = Decimal.ToByte(numericUpDownPWM_5.Value);
            pwmData[6] = Decimal.ToByte(numericUpDownPWM_6.Value);
            pwmData[7] = Decimal.ToByte(numericUpDownPWM_7.Value);
            pwmData[8] = Decimal.ToByte(numericUpDownPWM_8.Value);
            pwmData[9] = Decimal.ToByte(numericUpDownPWM_9.Value);
            pwmData[10] = Decimal.ToByte(numericUpDownPWM_10.Value);
            pwmData[11] = Decimal.ToByte(numericUpDownPWM_11.Value);
            pwmData[12] = Decimal.ToByte(numericUpDownPWM_12.Value);
            pwmData[13] = Decimal.ToByte(numericUpDownPWM_13.Value);
            pwmData[14] = Decimal.ToByte(numericUpDownPWM_14.Value);
            pwmData[15] = Decimal.ToByte(numericUpDownPWM_15.Value);
            _masterRef.SetDevicePulses(_deviceId, pwmData);
        }

        public void UpdateCurrentsData()
        {
            byte[] currentData = new byte[16];
            currentData[0] = Decimal.ToByte(numericUpDownCurrent_0.Value);
            currentData[1] = Decimal.ToByte(numericUpDownCurrent_1.Value);
            currentData[2] = Decimal.ToByte(numericUpDownCurrent_2.Value);
            currentData[3] = Decimal.ToByte(numericUpDownCurrent_3.Value);
            currentData[4] = Decimal.ToByte(numericUpDownCurrent_4.Value);
            currentData[5] = Decimal.ToByte(numericUpDownCurrent_5.Value);
            currentData[6] = Decimal.ToByte(numericUpDownCurrent_6.Value);
            currentData[7] = Decimal.ToByte(numericUpDownCurrent_7.Value);
            currentData[8] = Decimal.ToByte(numericUpDownCurrent_8.Value);
            currentData[9] = Decimal.ToByte(numericUpDownCurrent_9.Value);
            currentData[10] = Decimal.ToByte(numericUpDownCurrent_10.Value);
            currentData[11] = Decimal.ToByte(numericUpDownCurrent_11.Value);
            currentData[12] = Decimal.ToByte(numericUpDownCurrent_12.Value);
            currentData[13] = Decimal.ToByte(numericUpDownCurrent_13.Value);
            currentData[14] = Decimal.ToByte(numericUpDownCurrent_14.Value);
            currentData[15] = Decimal.ToByte(numericUpDownCurrent_15.Value);
            _masterRef.SetDeviceCurrents(_deviceId, currentData);

            label_current_0.Text = String.Format("{0:F2}", (0.4 * (double)currentData[0]));
            label_current_1.Text = String.Format("{0:F2}", (0.4 * (double)currentData[1]));
            label_current_2.Text = String.Format("{0:F2}", (0.4 * (double)currentData[2]));
            label_current_3.Text = String.Format("{0:F2}", (0.4 * (double)currentData[3]));
            label_current_4.Text = String.Format("{0:F2}", (0.4 * (double)currentData[4]));
            label_current_5.Text = String.Format("{0:F2}", (0.4 * (double)currentData[5]));
            label_current_6.Text = String.Format("{0:F2}", (0.4 * (double)currentData[6]));
            label_current_7.Text = String.Format("{0:F2}", (0.4 * (double)currentData[7]));
            label_current_8.Text = String.Format("{0:F2}", (0.4 * (double)currentData[8]));
            label_current_9.Text = String.Format("{0:F2}", (0.4 * (double)currentData[9]));
            label_current_10.Text = String.Format("{0:F2}", (0.4 * (double)currentData[10]));
            label_current_11.Text = String.Format("{0:F2}", (0.4 * (double)currentData[11]));
            label_current_12.Text = String.Format("{0:F2}", (0.4 * (double)currentData[12]));
            label_current_13.Text = String.Format("{0:F2}", (0.4 * (double)currentData[13]));
            label_current_14.Text = String.Format("{0:F2}", (0.4 * (double)currentData[14]));
            label_current_15.Text = String.Format("{0:F2}", (0.4 * (double)currentData[15]));
        }

        public void ReadBusStatusAndUpdateMiscStatus()
        {
            _deviceCopy.busStatus = _masterRef.ReadBusStatusCopy(_deviceId);
            UpdateMiscStatus();
        }

        public void UpdateMiscStatus()
        {
            label_hw_version.Text = String.Format("{0}-{1}", _deviceCopy.busStatus.hw_version_major, _deviceCopy.busStatus.hw_version_minor);
            label_fw_version.Text = String.Format("{0:X2}.{1:X2}.{2:X4}", _deviceCopy.busStatus.fw_version_dd, _deviceCopy.busStatus.fw_version_mm, _deviceCopy.busStatus.fw_version_yyyy);
            label_fw_version_num.Text = String.Format("{0:D}", (int)_deviceCopy.busStatus.fw_version_num);
            label_param_crc_val.Text = String.Format("0x{0:X4}", (int)_deviceCopy.busStatus.parameter_crc);

            _SetStatusLed(_masterRef.GetDeviceReadFail(_deviceId), statusLedControl_comm_read_error);
        }

        public void UpdateStatus()
        {
            _deviceCopy.busStatus = _masterRef.GetCachedBusStatusCopy(_deviceId);

            labelVLED_0.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVLED(0) / Device.LSB_VLED));
            labelVLED_1.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVLED(1) / Device.LSB_VLED));
            labelVLED_2.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVLED(2) / Device.LSB_VLED));
            labelVLED_3.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVLED(3) / Device.LSB_VLED));
            labelVLED_4.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVLED(4) / Device.LSB_VLED));
            labelVLED_5.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVLED(5) / Device.LSB_VLED));
            labelVLED_6.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVLED(6) / Device.LSB_VLED));
            labelVLED_7.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVLED(7) / Device.LSB_VLED));
            labelVLED_8.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVLED(8) / Device.LSB_VLED));
            labelVLED_9.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVLED(9) / Device.LSB_VLED));
            labelVLED_10.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVLED(10) / Device.LSB_VLED));
            labelVLED_11.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVLED(11) / Device.LSB_VLED));
            labelVLED_12.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVLED(12) / Device.LSB_VLED));
            labelVLED_13.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVLED(13) / Device.LSB_VLED));
            labelVLED_14.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVLED(14) / Device.LSB_VLED));
            labelVLED_15.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVLED(15) / Device.LSB_VLED));

            labelVDIF_0.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVDIF(0) / Device.LSB_VDIF));
            labelVDIF_1.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVDIF(1) / Device.LSB_VDIF));
            labelVDIF_2.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVDIF(2) / Device.LSB_VDIF));
            labelVDIF_3.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVDIF(3) / Device.LSB_VDIF));
            labelVDIF_4.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVDIF(4) / Device.LSB_VDIF));
            labelVDIF_5.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVDIF(5) / Device.LSB_VDIF));
            labelVDIF_6.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVDIF(6) / Device.LSB_VDIF));
            labelVDIF_7.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVDIF(7) / Device.LSB_VDIF));
            labelVDIF_8.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVDIF(8) / Device.LSB_VDIF));
            labelVDIF_9.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVDIF(9) / Device.LSB_VDIF));
            labelVDIF_10.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVDIF(10) / Device.LSB_VDIF));
            labelVDIF_11.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVDIF(11) / Device.LSB_VDIF));
            labelVDIF_12.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVDIF(12) / Device.LSB_VDIF));
            labelVDIF_13.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVDIF(13) / Device.LSB_VDIF));
            labelVDIF_14.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVDIF(14) / Device.LSB_VDIF));
            labelVDIF_15.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetVDIF(15) / Device.LSB_VDIF));

            labelILED_0.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetILED(0) / Device.LSB_ILED));
            labelILED_1.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetILED(1) / Device.LSB_ILED));
            labelILED_2.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetILED(2) / Device.LSB_ILED));
            labelILED_3.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetILED(3) / Device.LSB_ILED));
            labelILED_4.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetILED(4) / Device.LSB_ILED));
            labelILED_5.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetILED(5) / Device.LSB_ILED));
            labelILED_6.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetILED(6) / Device.LSB_ILED));
            labelILED_7.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetILED(7) / Device.LSB_ILED));
            labelILED_8.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetILED(8) / Device.LSB_ILED));
            labelILED_9.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetILED(9) / Device.LSB_ILED));
            labelILED_10.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetILED(10) / Device.LSB_ILED));
            labelILED_11.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetILED(11) / Device.LSB_ILED));
            labelILED_12.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetILED(12) / Device.LSB_ILED));
            labelILED_13.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetILED(13) / Device.LSB_ILED));
            labelILED_14.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetILED(14) / Device.LSB_ILED));
            labelILED_15.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.GetILED(15) / Device.LSB_ILED));

            label_temp.Text = String.Format("{0:D}", (int)_deviceCopy.busStatus.vt - 60);
            _SetStatusLed(_deviceCopy.busStatus.vt_too_high, statusLed_temp_too_high);
            _SetStatusLed(_deviceCopy.busStatus.vt_derating, statusLed_temp_derating);

            label_vs.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.vsup_vs * 4 / Device.LSB_VSUP));
            _SetStatusLed(_deviceCopy.busStatus.vs_too_high, statusLed_vs_too_high);
            _SetStatusLed(_deviceCopy.busStatus.vs_too_low, statusLed_vs_too_low);
            _SetStatusLed(_deviceCopy.busStatus.vs_derating, statusLed_vs_derating);

            label_sense0.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.vsup_sense0 * 4 / Device.LSB_VSUP));
            label_sense1.Text = String.Format("{0:F2}", ((double)_deviceCopy.busStatus.vsup_sense1 * 4 / Device.LSB_VSUP));

            label_comm_state.Text = _deviceCopy.busStatus.getCommStateString();

            _SetStatusLed(_deviceCopy.busStatus.pwmin_high, statusLed_pwmin_high);
            _SetStatusLed(_deviceCopy.busStatus.pwmin_mid, statusLed_pwmin_mid);
            _SetStatusLed(_deviceCopy.busStatus.pwmin_low, statusLed_pwmin_low);
            _SetStatusLed(_deviceCopy.busStatus.pwmin_invalid, statusLed_pwmin_invalid);

            _SetStatusLed(_deviceCopy.busStatus.led_current_mismatch, statusLed_curr_mismatch);
            _SetStatusLed(_deviceCopy.busStatus.led_pwm_error, statusLed_pwm_error);
            _SetStatusLed(_deviceCopy.busStatus.supply_too_low, statusLed_supply_too_low);
            _SetStatusLed(_deviceCopy.busStatus.led_short, statusLed_led_short);
            _SetStatusLed(_deviceCopy.busStatus.led_open, statusLed_led_open);

            labelStatus_0.Text = _deviceCopy.busStatus.getLedDiagStateString(0);
            labelStatus_1.Text = _deviceCopy.busStatus.getLedDiagStateString(1);
            labelStatus_2.Text = _deviceCopy.busStatus.getLedDiagStateString(2);
            labelStatus_3.Text = _deviceCopy.busStatus.getLedDiagStateString(3);
            labelStatus_4.Text = _deviceCopy.busStatus.getLedDiagStateString(4);
            labelStatus_5.Text = _deviceCopy.busStatus.getLedDiagStateString(5);
            labelStatus_6.Text = _deviceCopy.busStatus.getLedDiagStateString(6);
            labelStatus_7.Text = _deviceCopy.busStatus.getLedDiagStateString(7);
            labelStatus_8.Text = _deviceCopy.busStatus.getLedDiagStateString(8);
            labelStatus_9.Text = _deviceCopy.busStatus.getLedDiagStateString(9);
            labelStatus_10.Text = _deviceCopy.busStatus.getLedDiagStateString(10);
            labelStatus_11.Text = _deviceCopy.busStatus.getLedDiagStateString(11);
            labelStatus_12.Text = _deviceCopy.busStatus.getLedDiagStateString(12);
            labelStatus_13.Text = _deviceCopy.busStatus.getLedDiagStateString(13);
            labelStatus_14.Text = _deviceCopy.busStatus.getLedDiagStateString(14);
            labelStatus_15.Text = _deviceCopy.busStatus.getLedDiagStateString(15);

            label_w3_counter.Text = String.Format("#{0:D}", (int)_deviceCopy.busStatus.can_counter);

            // can only set to true
            _SetStatusLed(_deviceCopy.busStatus.reset, statusLed_device_reset);
            _SetStatusLed(_deviceCopy.busStatus.timeout, statusLed_comm_timeout);
            _SetStatusLed(_deviceCopy.busStatus.bus_error, statusLed_comm_bus_error);
            _SetStatusLed(_deviceCopy.busStatus.dlc_error_comb, statusLed_comm_dlc_error);
            _SetStatusLed(_deviceCopy.busStatus.len_error_comb, statusLed_comm_len_error);
            _SetStatusLed(_deviceCopy.busStatus.crc_error_comb, statusLed_comm_crc_error);
            _SetStatusLed(_deviceCopy.busStatus.bz_error_comb, statusLed_comm_bz_error);
            _SetStatusLed(_deviceCopy.busStatus.clk_accuracy_low, statusLed_clk_accuracy);
            _SetStatusLed(_deviceCopy.busStatus.meas_error, statusLed_meas_error);
            _SetStatusLed(_deviceCopy.busStatus.bus_failsafe, statusLed_pwmin_failsafe);

            // TODO: V ? LSB_GPIO
            label_gpio0.Text = String.Format("{0:D}", (int)_deviceCopy.busStatus.gpio_status_0);
            label_gpio1.Text = String.Format("{0:D}", (int)_deviceCopy.busStatus.gpio_status_1);
            statusLed_gpio_dac_error.Checked = _deviceCopy.busStatus.gpio_curr_dac_error ? true : false;
            statusLed_gpio_binning_error.Checked = _deviceCopy.busStatus.gpio_binning_error ? true : false;

            _SetStatusLed(_deviceCopy.busStatus.diag0_in, statusLed_diag_in0);
            _SetStatusLed(_deviceCopy.busStatus.diag1_in, statusLed_diag_in1);
            _SetStatusLed(_deviceCopy.busStatus.diag2_in, statusLed_diag_in2);
            _SetStatusLed(_deviceCopy.busStatus.diag0_out, statusLed_diag_out0);
            _SetStatusLed(_deviceCopy.busStatus.diag1_out, statusLed_diag_out1);
            _SetStatusLed(_deviceCopy.busStatus.diag2_out, statusLed_diag_out2);
            _SetStatusLed(_deviceCopy.busStatus.assert_active, statusLed_diag_assert);
            _SetStatusLed(_deviceCopy.busStatus.mask_active, statusLed_diag_mask);

            UpdateMiscStatus();
        }

        #region "Comm"

        public void SetCommNode(byte node)
        {
            numericUpDownCommNode.Value = node;
        }

        public void SetCommGroup(byte group)
        {
            numericUpDownCommGroup.Value = group;
        }

        private void numericUpDownCANNode_ValueChanged(object sender, EventArgs e)
        {
            _masterRef.SetDeviceNode(_deviceId, Decimal.ToByte(numericUpDownCommNode.Value));
        }

        private void numericUpDownCANGroup_ValueChanged(object sender, EventArgs e)
        {
            _masterRef.SetDeviceGroup(_deviceId, Decimal.ToByte(numericUpDownCommGroup.Value));
        }

        private void numericUpDownCANIndex_ValueChanged(object sender, EventArgs e)
        {
            _masterRef.SetDeviceIndex(_deviceId, Decimal.ToByte(numericUpDownCommIndex.Value));
        }

        #endregion

        #region "EEPROM"

        private void button_EE_Write_Click(object sender, EventArgs e)
        {
            BoolString res = _masterRef.WriteEeprom(_deviceId, _deviceCopy.eeprom);
            if (res.bval)
            {
                MessageBox.Show(res.sval, "Info", MessageBoxButtons.OK);
                _deviceCopy.eeprom.ClearAllModified();
            }
            else
            {
                MessageBox.Show(res.sval, "Error", MessageBoxButtons.OK);
            }
            _eepromForm.UpdateFromMemory();
        }

        private void button_EE_Read_Click(object sender, EventArgs e)
        {
            _deviceCopy.eeprom = _masterRef.ReadEepromCopy(_deviceId);
            _UpdateMemFormMemories();
            _eepromForm.UpdateFromMemory(_eepromFormMemories);
            _eepromForm.Show();
            _eepromForm.BringToFront();
        }

        private void button_EE_Unlock_Click(object sender, EventArgs e)
        {
            _masterRef.SendCommandUnlockEeprom(_deviceId);
        }

        private void button_EE_Edit_Click(object sender, EventArgs e)
        {
            _eepromForm.Show();
            _eepromForm.BringToFront();
        }

        #endregion

        #region "Pulses"

        private void trackBarPWMDimming_ValueChanged(object sender, EventArgs e)
        {
            if (checkBoxDimmingPWM_0.Checked) numericUpDownPWM_0.Value = trackBarPWMDimming.Value;
            if (checkBoxDimmingPWM_1.Checked) numericUpDownPWM_1.Value = trackBarPWMDimming.Value;
            if (checkBoxDimmingPWM_2.Checked) numericUpDownPWM_2.Value = trackBarPWMDimming.Value;
            if (checkBoxDimmingPWM_3.Checked) numericUpDownPWM_3.Value = trackBarPWMDimming.Value;
            if (checkBoxDimmingPWM_4.Checked) numericUpDownPWM_4.Value = trackBarPWMDimming.Value;
            if (checkBoxDimmingPWM_5.Checked) numericUpDownPWM_5.Value = trackBarPWMDimming.Value;
            if (checkBoxDimmingPWM_6.Checked) numericUpDownPWM_6.Value = trackBarPWMDimming.Value;
            if (checkBoxDimmingPWM_7.Checked) numericUpDownPWM_7.Value = trackBarPWMDimming.Value;
            if (checkBoxDimmingPWM_8.Checked) numericUpDownPWM_8.Value = trackBarPWMDimming.Value;
            if (checkBoxDimmingPWM_9.Checked) numericUpDownPWM_9.Value = trackBarPWMDimming.Value;
            if (checkBoxDimmingPWM_10.Checked) numericUpDownPWM_10.Value = trackBarPWMDimming.Value;
            if (checkBoxDimmingPWM_11.Checked) numericUpDownPWM_11.Value = trackBarPWMDimming.Value;
            if (checkBoxDimmingPWM_12.Checked) numericUpDownPWM_12.Value = trackBarPWMDimming.Value;
            if (checkBoxDimmingPWM_13.Checked) numericUpDownPWM_13.Value = trackBarPWMDimming.Value;
            if (checkBoxDimmingPWM_14.Checked) numericUpDownPWM_14.Value = trackBarPWMDimming.Value;
            if (checkBoxDimmingPWM_15.Checked) numericUpDownPWM_15.Value = trackBarPWMDimming.Value;
            UpdatePWMData();
        }

        private void checkBoxDimmingPWM_0_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDimmingPWM_0.Checked) { numericUpDownPWM_0.Enabled = false; numericUpDownPWM_0.Value = trackBarPWMDimming.Value; }
            else numericUpDownPWM_0.Enabled = true;
            UpdatePWMData();
        }

        private void checkBoxDimmingPWM_1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDimmingPWM_1.Checked) { numericUpDownPWM_1.Enabled = false; numericUpDownPWM_1.Value = trackBarPWMDimming.Value; }
            else numericUpDownPWM_1.Enabled = true;
            UpdatePWMData();
        }

        private void checkBoxDimmingPWM_2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDimmingPWM_2.Checked) { numericUpDownPWM_2.Enabled = false; numericUpDownPWM_2.Value = trackBarPWMDimming.Value; }
            else numericUpDownPWM_2.Enabled = true;
            UpdatePWMData();
        }

        private void checkBoxDimmingPWM_3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDimmingPWM_3.Checked) { numericUpDownPWM_3.Enabled = false; numericUpDownPWM_3.Value = trackBarPWMDimming.Value; }
            else numericUpDownPWM_3.Enabled = true;
            UpdatePWMData();
        }

        private void checkBoxDimmingPWM_4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDimmingPWM_4.Checked) { numericUpDownPWM_4.Enabled = false; numericUpDownPWM_4.Value = trackBarPWMDimming.Value; }
            else numericUpDownPWM_4.Enabled = true;
            UpdatePWMData();
        }

        private void checkBoxDimmingPWM_5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDimmingPWM_5.Checked) { numericUpDownPWM_5.Enabled = false; numericUpDownPWM_5.Value = trackBarPWMDimming.Value; }
            else numericUpDownPWM_5.Enabled = true;
            UpdatePWMData();
        }

        private void checkBoxDimmingPWM_6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDimmingPWM_6.Checked) { numericUpDownPWM_6.Enabled = false; numericUpDownPWM_6.Value = trackBarPWMDimming.Value; }
            else numericUpDownPWM_6.Enabled = true;
            UpdatePWMData();
        }

        private void checkBoxDimmingPWM_7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDimmingPWM_7.Checked) { numericUpDownPWM_7.Enabled = false; numericUpDownPWM_7.Value = trackBarPWMDimming.Value; }
            else numericUpDownPWM_7.Enabled = true;
            UpdatePWMData();
        }

        private void checkBoxDimmingPWM_8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDimmingPWM_8.Checked) { numericUpDownPWM_8.Enabled = false; numericUpDownPWM_8.Value = trackBarPWMDimming.Value; }
            else numericUpDownPWM_8.Enabled = true;
            UpdatePWMData();
        }

        private void checkBoxDimmingPWM_9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDimmingPWM_9.Checked) { numericUpDownPWM_9.Enabled = false; numericUpDownPWM_9.Value = trackBarPWMDimming.Value; }
            else numericUpDownPWM_9.Enabled = true;
            UpdatePWMData();
        }

        private void checkBoxDimmingPWM_10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDimmingPWM_10.Checked) { numericUpDownPWM_10.Enabled = false; numericUpDownPWM_10.Value = trackBarPWMDimming.Value; }
            else numericUpDownPWM_10.Enabled = true;
            UpdatePWMData();
        }

        private void checkBoxDimmingPWM_11_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDimmingPWM_11.Checked) { numericUpDownPWM_11.Enabled = false; numericUpDownPWM_11.Value = trackBarPWMDimming.Value; }
            else numericUpDownPWM_11.Enabled = true;
            UpdatePWMData();
        }

        private void checkBoxDimmingPWM_12_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDimmingPWM_12.Checked) { numericUpDownPWM_12.Enabled = false; numericUpDownPWM_12.Value = trackBarPWMDimming.Value; }
            else numericUpDownPWM_12.Enabled = true;
            UpdatePWMData();
        }

        private void checkBoxDimmingPWM_13_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDimmingPWM_13.Checked) { numericUpDownPWM_13.Enabled = false; numericUpDownPWM_13.Value = trackBarPWMDimming.Value; }
            else numericUpDownPWM_13.Enabled = true;
            UpdatePWMData();
        }

        private void checkBoxDimmingPWM_14_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDimmingPWM_14.Checked) { numericUpDownPWM_14.Enabled = false; numericUpDownPWM_14.Value = trackBarPWMDimming.Value; }
            else numericUpDownPWM_14.Enabled = true;
            UpdatePWMData();
        }

        private void checkBoxDimmingPWM_15_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDimmingPWM_15.Checked) { numericUpDownPWM_15.Enabled = false; numericUpDownPWM_15.Value = trackBarPWMDimming.Value; }
            else numericUpDownPWM_15.Enabled = true;
            UpdatePWMData();
        }

        #endregion

        #region "Current"

        private void trackBarCurrent_ValueChanged(object sender, EventArgs e)
        {
            if (checkBoxCurrent_0.Checked) numericUpDownCurrent_0.Value = trackBarCurrent.Value;
            if (checkBoxCurrent_1.Checked) numericUpDownCurrent_1.Value = trackBarCurrent.Value;
            if (checkBoxCurrent_2.Checked) numericUpDownCurrent_2.Value = trackBarCurrent.Value;
            if (checkBoxCurrent_3.Checked) numericUpDownCurrent_3.Value = trackBarCurrent.Value;
            if (checkBoxCurrent_4.Checked) numericUpDownCurrent_4.Value = trackBarCurrent.Value;
            if (checkBoxCurrent_5.Checked) numericUpDownCurrent_5.Value = trackBarCurrent.Value;
            if (checkBoxCurrent_6.Checked) numericUpDownCurrent_6.Value = trackBarCurrent.Value;
            if (checkBoxCurrent_7.Checked) numericUpDownCurrent_7.Value = trackBarCurrent.Value;
            if (checkBoxCurrent_8.Checked) numericUpDownCurrent_8.Value = trackBarCurrent.Value;
            if (checkBoxCurrent_9.Checked) numericUpDownCurrent_9.Value = trackBarCurrent.Value;
            if (checkBoxCurrent_10.Checked) numericUpDownCurrent_10.Value = trackBarCurrent.Value;
            if (checkBoxCurrent_11.Checked) numericUpDownCurrent_11.Value = trackBarCurrent.Value;
            if (checkBoxCurrent_12.Checked) numericUpDownCurrent_12.Value = trackBarCurrent.Value;
            if (checkBoxCurrent_13.Checked) numericUpDownCurrent_13.Value = trackBarCurrent.Value;
            if (checkBoxCurrent_14.Checked) numericUpDownCurrent_14.Value = trackBarCurrent.Value;
            if (checkBoxCurrent_15.Checked) numericUpDownCurrent_15.Value = trackBarCurrent.Value;
            UpdateCurrentsData();
        }

        private void checkBoxCurrent_0_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCurrent_0.Checked) { numericUpDownCurrent_0.Enabled = false; numericUpDownCurrent_0.Value = trackBarCurrent.Value; }
            else numericUpDownCurrent_0.Enabled = true;
            UpdateCurrentsData();
        }

        private void checkBoxCurrent_1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCurrent_1.Checked) { numericUpDownCurrent_1.Enabled = false; numericUpDownCurrent_1.Value = trackBarCurrent.Value; }
            else numericUpDownCurrent_1.Enabled = true;
            UpdateCurrentsData();
        }

        private void checkBoxCurrent_2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCurrent_2.Checked) { numericUpDownCurrent_2.Enabled = false; numericUpDownCurrent_2.Value = trackBarCurrent.Value; }
            else numericUpDownCurrent_2.Enabled = true;
            UpdateCurrentsData();
        }

        private void checkBoxCurrent_3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCurrent_3.Checked) { numericUpDownCurrent_3.Enabled = false; numericUpDownCurrent_3.Value = trackBarCurrent.Value; }
            else numericUpDownCurrent_3.Enabled = true;
            UpdateCurrentsData();
        }

        private void checkBoxCurrent_4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCurrent_4.Checked) { numericUpDownCurrent_4.Enabled = false; numericUpDownCurrent_4.Value = trackBarCurrent.Value; }
            else numericUpDownCurrent_4.Enabled = true;
            UpdateCurrentsData();
        }

        private void checkBoxCurrent_5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCurrent_5.Checked) { numericUpDownCurrent_5.Enabled = false; numericUpDownCurrent_5.Value = trackBarCurrent.Value; }
            else numericUpDownCurrent_5.Enabled = true;
            UpdateCurrentsData();
        }

        private void checkBoxCurrent_6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCurrent_6.Checked) { numericUpDownCurrent_6.Enabled = false; numericUpDownCurrent_6.Value = trackBarCurrent.Value; }
            else numericUpDownCurrent_6.Enabled = true;
            UpdateCurrentsData();
        }

        private void checkBoxCurrent_7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCurrent_7.Checked) { numericUpDownCurrent_7.Enabled = false; numericUpDownCurrent_7.Value = trackBarCurrent.Value; }
            else numericUpDownCurrent_7.Enabled = true;
            UpdateCurrentsData();
        }

        private void checkBoxCurrent_8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCurrent_8.Checked) { numericUpDownCurrent_8.Enabled = false; numericUpDownCurrent_8.Value = trackBarCurrent.Value; }
            else numericUpDownCurrent_8.Enabled = true;
            UpdateCurrentsData();
        }

        private void checkBoxCurrent_9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCurrent_9.Checked) { numericUpDownCurrent_9.Enabled = false; numericUpDownCurrent_9.Value = trackBarCurrent.Value; }
            else numericUpDownCurrent_9.Enabled = true;
            UpdateCurrentsData();
        }

        private void checkBoxCurrent_10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCurrent_10.Checked) { numericUpDownCurrent_10.Enabled = false; numericUpDownCurrent_10.Value = trackBarCurrent.Value; }
            else numericUpDownCurrent_10.Enabled = true;
            UpdateCurrentsData();
        }

        private void checkBoxCurrent_11_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCurrent_11.Checked) { numericUpDownCurrent_11.Enabled = false; numericUpDownCurrent_11.Value = trackBarCurrent.Value; }
            else numericUpDownCurrent_11.Enabled = true;
            UpdateCurrentsData();
        }

        private void checkBoxCurrent_12_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCurrent_12.Checked) { numericUpDownCurrent_12.Enabled = false; numericUpDownCurrent_12.Value = trackBarCurrent.Value; }
            else numericUpDownCurrent_12.Enabled = true;
            UpdateCurrentsData();
        }

        private void checkBoxCurrent_13_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCurrent_13.Checked) { numericUpDownCurrent_13.Enabled = false; numericUpDownCurrent_13.Value = trackBarCurrent.Value; }
            else numericUpDownCurrent_13.Enabled = true;
            UpdateCurrentsData();
        }

        private void checkBoxCurrent_14_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCurrent_14.Checked) { numericUpDownCurrent_14.Enabled = false; numericUpDownCurrent_14.Value = trackBarCurrent.Value; }
            else numericUpDownCurrent_14.Enabled = true;
            UpdateCurrentsData();
        }

        private void checkBoxCurrent_15_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCurrent_15.Checked) { numericUpDownCurrent_15.Enabled = false; numericUpDownCurrent_15.Value = trackBarCurrent.Value; }
            else numericUpDownCurrent_15.Enabled = true;
            UpdateCurrentsData();
        }

        #endregion

        private void buttonEditMEM_Click(object sender, EventArgs e)
        {
            _busForm.Show();
            _busForm.BringToFront();
        }

        private void buttonReadMEM_Click(object sender, EventArgs e)
        {
            _deviceCopy.busConfig = _masterRef.ReadBusConfigCopy(_deviceId);
            _deviceCopy.busStatus = _masterRef.ReadBusStatusCopy(_deviceId);
            _UpdateMemFormMemories();
            _busForm.UpdateFromMemory(_busFormMemories);
            _busForm.Show();
            _busForm.BringToFront();
        }

        private void buttonWriteMEM_Click(object sender, EventArgs e)
        {
            _masterRef.WriteBusConfig(_deviceId, _deviceCopy.busConfig);
            _deviceCopy.busConfig.ClearAllModified();
            _busForm.UpdateFromMemory();
        }

        private void buttonZeroPWM_Click(object sender, EventArgs e)
        {
            if (!checkBoxDimmingPWM_0.Checked) numericUpDownPWM_0.Value = 0;
            if (!checkBoxDimmingPWM_1.Checked) numericUpDownPWM_1.Value = 0;
            if (!checkBoxDimmingPWM_2.Checked) numericUpDownPWM_2.Value = 0;
            if (!checkBoxDimmingPWM_3.Checked) numericUpDownPWM_3.Value = 0;
            if (!checkBoxDimmingPWM_4.Checked) numericUpDownPWM_4.Value = 0;
            if (!checkBoxDimmingPWM_5.Checked) numericUpDownPWM_5.Value = 0;
            if (!checkBoxDimmingPWM_6.Checked) numericUpDownPWM_6.Value = 0;
            if (!checkBoxDimmingPWM_7.Checked) numericUpDownPWM_7.Value = 0;
            if (!checkBoxDimmingPWM_8.Checked) numericUpDownPWM_8.Value = 0;
            if (!checkBoxDimmingPWM_9.Checked) numericUpDownPWM_9.Value = 0;
            if (!checkBoxDimmingPWM_10.Checked) numericUpDownPWM_10.Value = 0;
            if (!checkBoxDimmingPWM_11.Checked) numericUpDownPWM_11.Value = 0;
            if (!checkBoxDimmingPWM_12.Checked) numericUpDownPWM_12.Value = 0;
            if (!checkBoxDimmingPWM_13.Checked) numericUpDownPWM_13.Value = 0;
            if (!checkBoxDimmingPWM_14.Checked) numericUpDownPWM_14.Value = 0;
            if (!checkBoxDimmingPWM_15.Checked) numericUpDownPWM_15.Value = 0;
            UpdatePWMData();
            _masterRef.WriteBusConfigPwmData(_deviceId);
        }

        private void buttonCurrentsZero_Click(object sender, EventArgs e)
        {
            if (!checkBoxCurrent_0.Checked) numericUpDownCurrent_0.Value = 0;
            if (!checkBoxCurrent_1.Checked) numericUpDownCurrent_1.Value = 0;
            if (!checkBoxCurrent_2.Checked) numericUpDownCurrent_2.Value = 0;
            if (!checkBoxCurrent_3.Checked) numericUpDownCurrent_3.Value = 0;
            if (!checkBoxCurrent_4.Checked) numericUpDownCurrent_4.Value = 0;
            if (!checkBoxCurrent_5.Checked) numericUpDownCurrent_5.Value = 0;
            if (!checkBoxCurrent_6.Checked) numericUpDownCurrent_6.Value = 0;
            if (!checkBoxCurrent_7.Checked) numericUpDownCurrent_7.Value = 0;
            if (!checkBoxCurrent_8.Checked) numericUpDownCurrent_8.Value = 0;
            if (!checkBoxCurrent_9.Checked) numericUpDownCurrent_9.Value = 0;
            if (!checkBoxCurrent_10.Checked) numericUpDownCurrent_10.Value = 0;
            if (!checkBoxCurrent_11.Checked) numericUpDownCurrent_11.Value = 0;
            if (!checkBoxCurrent_12.Checked) numericUpDownCurrent_12.Value = 0;
            if (!checkBoxCurrent_13.Checked) numericUpDownCurrent_13.Value = 0;
            if (!checkBoxCurrent_14.Checked) numericUpDownCurrent_14.Value = 0;
            if (!checkBoxCurrent_15.Checked) numericUpDownCurrent_15.Value = 0;
            UpdateCurrentsData();
            _masterRef.WriteBusConfigCurrentsData(_deviceId);
        }

        private void buttonWritePWM_Click(object sender, EventArgs e)
        {
            UpdatePWMData();
            _masterRef.WriteBusConfigPwmData(_deviceId);
        }

        private void buttonCurrentsWrite_Click(object sender, EventArgs e)
        {
            UpdateCurrentsData();
            _masterRef.WriteBusConfigCurrentsData(_deviceId);
        }

        private void buttonReadPWM_Click(object sender, EventArgs e)
        {
            _deviceCopy.busConfig = _masterRef.ReadBusConfigCopy(_deviceId);
            numericUpDownPWM_0.Value = _deviceCopy.busConfig.GetPulse(0);
            numericUpDownPWM_1.Value = _deviceCopy.busConfig.GetPulse(1);
            numericUpDownPWM_2.Value = _deviceCopy.busConfig.GetPulse(2);
            numericUpDownPWM_3.Value = _deviceCopy.busConfig.GetPulse(3);
            numericUpDownPWM_4.Value = _deviceCopy.busConfig.GetPulse(4);
            numericUpDownPWM_5.Value = _deviceCopy.busConfig.GetPulse(5);
            numericUpDownPWM_6.Value = _deviceCopy.busConfig.GetPulse(6);
            numericUpDownPWM_7.Value = _deviceCopy.busConfig.GetPulse(7);
            numericUpDownPWM_8.Value = _deviceCopy.busConfig.GetPulse(8);
            numericUpDownPWM_9.Value = _deviceCopy.busConfig.GetPulse(9);
            numericUpDownPWM_10.Value = _deviceCopy.busConfig.GetPulse(10);
            numericUpDownPWM_11.Value = _deviceCopy.busConfig.GetPulse(11);
            numericUpDownPWM_12.Value = _deviceCopy.busConfig.GetPulse(12);
            numericUpDownPWM_13.Value = _deviceCopy.busConfig.GetPulse(13);
            numericUpDownPWM_14.Value = _deviceCopy.busConfig.GetPulse(14);
            numericUpDownPWM_15.Value = _deviceCopy.busConfig.GetPulse(15);
            UpdatePWMData();
        }

        private void buttonCurrentsRead_Click(object sender, EventArgs e)
        {
            _deviceCopy.busConfig = _masterRef.ReadBusConfigCopy(_deviceId);
            byte[] currentsData = new byte[16];
            numericUpDownCurrent_0.Value = _deviceCopy.busConfig.GetCurrent(0);
            numericUpDownCurrent_1.Value = _deviceCopy.busConfig.GetCurrent(1);
            numericUpDownCurrent_2.Value = _deviceCopy.busConfig.GetCurrent(2);
            numericUpDownCurrent_3.Value = _deviceCopy.busConfig.GetCurrent(3);
            numericUpDownCurrent_4.Value = _deviceCopy.busConfig.GetCurrent(4);
            numericUpDownCurrent_5.Value = _deviceCopy.busConfig.GetCurrent(5);
            numericUpDownCurrent_6.Value = _deviceCopy.busConfig.GetCurrent(6);
            numericUpDownCurrent_7.Value = _deviceCopy.busConfig.GetCurrent(7);
            numericUpDownCurrent_8.Value = _deviceCopy.busConfig.GetCurrent(8);
            numericUpDownCurrent_9.Value = _deviceCopy.busConfig.GetCurrent(9);
            numericUpDownCurrent_10.Value = _deviceCopy.busConfig.GetCurrent(10);
            numericUpDownCurrent_11.Value = _deviceCopy.busConfig.GetCurrent(11);
            numericUpDownCurrent_12.Value = _deviceCopy.busConfig.GetCurrent(12);
            numericUpDownCurrent_13.Value = _deviceCopy.busConfig.GetCurrent(13);
            numericUpDownCurrent_14.Value = _deviceCopy.busConfig.GetCurrent(14);
            numericUpDownCurrent_15.Value = _deviceCopy.busConfig.GetCurrent(15);
            UpdateCurrentsData();
        }

        private void buttonPWMDimmingAll_Click(object sender, EventArgs e)
        {
            checkBoxDimmingPWM_0.Checked = true; numericUpDownPWM_0.Enabled = false; numericUpDownPWM_0.Value = trackBarPWMDimming.Value;
            checkBoxDimmingPWM_1.Checked = true; numericUpDownPWM_1.Enabled = false; numericUpDownPWM_1.Value = trackBarPWMDimming.Value;
            checkBoxDimmingPWM_2.Checked = true; numericUpDownPWM_2.Enabled = false; numericUpDownPWM_2.Value = trackBarPWMDimming.Value;
            checkBoxDimmingPWM_3.Checked = true; numericUpDownPWM_3.Enabled = false; numericUpDownPWM_3.Value = trackBarPWMDimming.Value;
            checkBoxDimmingPWM_4.Checked = true; numericUpDownPWM_4.Enabled = false; numericUpDownPWM_4.Value = trackBarPWMDimming.Value;
            checkBoxDimmingPWM_5.Checked = true; numericUpDownPWM_5.Enabled = false; numericUpDownPWM_5.Value = trackBarPWMDimming.Value;
            checkBoxDimmingPWM_6.Checked = true; numericUpDownPWM_6.Enabled = false; numericUpDownPWM_6.Value = trackBarPWMDimming.Value;
            checkBoxDimmingPWM_7.Checked = true; numericUpDownPWM_7.Enabled = false; numericUpDownPWM_7.Value = trackBarPWMDimming.Value;
            checkBoxDimmingPWM_8.Checked = true; numericUpDownPWM_8.Enabled = false; numericUpDownPWM_8.Value = trackBarPWMDimming.Value;
            checkBoxDimmingPWM_9.Checked = true; numericUpDownPWM_9.Enabled = false; numericUpDownPWM_9.Value = trackBarPWMDimming.Value;
            checkBoxDimmingPWM_10.Checked = true; numericUpDownPWM_10.Enabled = false; numericUpDownPWM_10.Value = trackBarPWMDimming.Value;
            checkBoxDimmingPWM_11.Checked = true; numericUpDownPWM_11.Enabled = false; numericUpDownPWM_11.Value = trackBarPWMDimming.Value;
            checkBoxDimmingPWM_12.Checked = true; numericUpDownPWM_12.Enabled = false; numericUpDownPWM_12.Value = trackBarPWMDimming.Value;
            checkBoxDimmingPWM_13.Checked = true; numericUpDownPWM_13.Enabled = false; numericUpDownPWM_13.Value = trackBarPWMDimming.Value;
            checkBoxDimmingPWM_14.Checked = true; numericUpDownPWM_14.Enabled = false; numericUpDownPWM_14.Value = trackBarPWMDimming.Value;
            checkBoxDimmingPWM_15.Checked = true; numericUpDownPWM_15.Enabled = false; numericUpDownPWM_15.Value = trackBarPWMDimming.Value;
            UpdatePWMData();
        }

        private void buttonCurrentsAll_Click(object sender, EventArgs e)
        {
            checkBoxCurrent_0.Checked = true; numericUpDownCurrent_0.Enabled = false; numericUpDownCurrent_0.Value = trackBarCurrent.Value;
            checkBoxCurrent_1.Checked = true; numericUpDownCurrent_1.Enabled = false; numericUpDownCurrent_1.Value = trackBarCurrent.Value;
            checkBoxCurrent_2.Checked = true; numericUpDownCurrent_2.Enabled = false; numericUpDownCurrent_2.Value = trackBarCurrent.Value;
            checkBoxCurrent_3.Checked = true; numericUpDownCurrent_3.Enabled = false; numericUpDownCurrent_3.Value = trackBarCurrent.Value;
            checkBoxCurrent_4.Checked = true; numericUpDownCurrent_4.Enabled = false; numericUpDownCurrent_4.Value = trackBarCurrent.Value;
            checkBoxCurrent_5.Checked = true; numericUpDownCurrent_5.Enabled = false; numericUpDownCurrent_5.Value = trackBarCurrent.Value;
            checkBoxCurrent_6.Checked = true; numericUpDownCurrent_6.Enabled = false; numericUpDownCurrent_6.Value = trackBarCurrent.Value;
            checkBoxCurrent_7.Checked = true; numericUpDownCurrent_7.Enabled = false; numericUpDownCurrent_7.Value = trackBarCurrent.Value;
            checkBoxCurrent_8.Checked = true; numericUpDownCurrent_8.Enabled = false; numericUpDownCurrent_8.Value = trackBarCurrent.Value;
            checkBoxCurrent_9.Checked = true; numericUpDownCurrent_9.Enabled = false; numericUpDownCurrent_9.Value = trackBarCurrent.Value;
            checkBoxCurrent_10.Checked = true; numericUpDownCurrent_10.Enabled = false; numericUpDownCurrent_10.Value = trackBarCurrent.Value;
            checkBoxCurrent_11.Checked = true; numericUpDownCurrent_11.Enabled = false; numericUpDownCurrent_11.Value = trackBarCurrent.Value;
            checkBoxCurrent_12.Checked = true; numericUpDownCurrent_12.Enabled = false; numericUpDownCurrent_12.Value = trackBarCurrent.Value;
            checkBoxCurrent_13.Checked = true; numericUpDownCurrent_13.Enabled = false; numericUpDownCurrent_13.Value = trackBarCurrent.Value;
            checkBoxCurrent_14.Checked = true; numericUpDownCurrent_14.Enabled = false; numericUpDownCurrent_14.Value = trackBarCurrent.Value;
            checkBoxCurrent_15.Checked = true; numericUpDownCurrent_15.Enabled = false; numericUpDownCurrent_15.Value = trackBarCurrent.Value;
            UpdateCurrentsData();
        }

        private void buttonPWMDimmingNone_Click(object sender, EventArgs e)
        {
            checkBoxDimmingPWM_0.Checked = false; numericUpDownPWM_0.Enabled = true;
            checkBoxDimmingPWM_1.Checked = false; numericUpDownPWM_1.Enabled = true;
            checkBoxDimmingPWM_2.Checked = false; numericUpDownPWM_2.Enabled = true;
            checkBoxDimmingPWM_3.Checked = false; numericUpDownPWM_3.Enabled = true;
            checkBoxDimmingPWM_4.Checked = false; numericUpDownPWM_4.Enabled = true;
            checkBoxDimmingPWM_5.Checked = false; numericUpDownPWM_5.Enabled = true;
            checkBoxDimmingPWM_6.Checked = false; numericUpDownPWM_6.Enabled = true;
            checkBoxDimmingPWM_7.Checked = false; numericUpDownPWM_7.Enabled = true;
            checkBoxDimmingPWM_8.Checked = false; numericUpDownPWM_8.Enabled = true;
            checkBoxDimmingPWM_9.Checked = false; numericUpDownPWM_9.Enabled = true;
            checkBoxDimmingPWM_10.Checked = false; numericUpDownPWM_10.Enabled = true;
            checkBoxDimmingPWM_11.Checked = false; numericUpDownPWM_11.Enabled = true;
            checkBoxDimmingPWM_12.Checked = false; numericUpDownPWM_12.Enabled = true;
            checkBoxDimmingPWM_13.Checked = false; numericUpDownPWM_13.Enabled = true;
            checkBoxDimmingPWM_14.Checked = false; numericUpDownPWM_14.Enabled = true;
            checkBoxDimmingPWM_15.Checked = false; numericUpDownPWM_15.Enabled = true;
        }

        private void buttonCurrentsNone_Click(object sender, EventArgs e)
        {
            checkBoxCurrent_0.Checked = false; numericUpDownCurrent_0.Enabled = true;
            checkBoxCurrent_1.Checked = false; numericUpDownCurrent_1.Enabled = true;
            checkBoxCurrent_2.Checked = false; numericUpDownCurrent_2.Enabled = true;
            checkBoxCurrent_3.Checked = false; numericUpDownCurrent_3.Enabled = true;
            checkBoxCurrent_4.Checked = false; numericUpDownCurrent_4.Enabled = true;
            checkBoxCurrent_5.Checked = false; numericUpDownCurrent_5.Enabled = true;
            checkBoxCurrent_6.Checked = false; numericUpDownCurrent_6.Enabled = true;
            checkBoxCurrent_7.Checked = false; numericUpDownCurrent_7.Enabled = true;
            checkBoxCurrent_8.Checked = false; numericUpDownCurrent_8.Enabled = true;
            checkBoxCurrent_9.Checked = false; numericUpDownCurrent_9.Enabled = true;
            checkBoxCurrent_10.Checked = false; numericUpDownCurrent_10.Enabled = true;
            checkBoxCurrent_11.Checked = false; numericUpDownCurrent_11.Enabled = true;
            checkBoxCurrent_12.Checked = false; numericUpDownCurrent_12.Enabled = true;
            checkBoxCurrent_13.Checked = false; numericUpDownCurrent_13.Enabled = true;
            checkBoxCurrent_14.Checked = false; numericUpDownCurrent_14.Enabled = true;
            checkBoxCurrent_15.Checked = false; numericUpDownCurrent_15.Enabled = true;
        }

        private void numericUpDownPWM_ValueChanged(object sender, EventArgs e)
        {
            if (((NumericUpDown)sender).Focused)
                UpdatePWMData();
        }

        private void numericUpDownCurrent_ValueChanged(object sender, EventArgs e)
        {
            if (((NumericUpDown) sender).Focused)
                UpdateCurrentsData();
        }

        private void button_bus_reset_Click(object sender, EventArgs e)
        {
            _masterRef.SendCommandReset(_deviceId);
            ReadBusStatusAndUpdateMiscStatus();
        }

        private void statusLed_Click(object sender, EventArgs e)
        {
            // can only set to false
            ((StatusLedControl)sender).Checked = false;
        }

        private void buttonSaveEE_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "txt files (*.txt)|*.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                String filePath = saveFileDialog.FileName;
                _deviceCopy.eeprom.saveToFile(filePath);
            }
        }

        private void buttonLoadEE_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                String filePath = openFileDialog.FileName;
                _deviceCopy.eeprom.loadFromFile(filePath);
                _eepromForm.UpdateFromMemory();
            }
            _eepromForm.Show();
            _eepromForm.BringToFront();
        }

        private void buttonQuickProgEE_Click(object sender, EventArgs e)
        {
            QuickProgForm quickProgForm = new QuickProgForm(_masterRef, _deviceCopy, _deviceId);
            quickProgForm.ShowDialog();
        }

    }
}
