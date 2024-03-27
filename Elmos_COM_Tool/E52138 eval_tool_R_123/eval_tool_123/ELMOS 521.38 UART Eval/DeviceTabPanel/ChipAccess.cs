using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELMOS_521._38_UART_Eval.DeviceTabPanel
{
    public partial class ChipAccess : PanelForm
    {
        private List<Control> multiplexingControls;
        private List<CheckBox> dimmingCheckboxes;
        private List<CheckBox> dimmingCheckboxesMulti;
        private List<NumericUpDown> PWMNumerics;
        private List<NumericUpDown> PWMNumericsMulti;
        private List<CheckBox> boostCheckboxes;
        private List<CheckBox> boostCheckboxesMulti;
        private List<PictureBox> boostStatusLEDs;
        private List<PictureBox> boostStatusLEDsMulti;

        private bool numPWM_ValueChanged_active = true;

        public ChipAccess() : base()
        {
            InitializeComponent();

            grdView.DefaultCellStyle.SelectionBackColor = SystemColors.ControlLight;
            grdView.DefaultCellStyle.SelectionForeColor = SystemColors.ControlText;
            
            grdViewMulti.DefaultCellStyle.SelectionBackColor = SystemColors.ControlLight;
            grdViewMulti.DefaultCellStyle.SelectionForeColor = SystemColors.ControlText;
            
            DoubleBuffered = true;

            multiplexingControls = new List<Control> { tblDimmingMulti, tblPWMmulti, tblBoostMulti, grdViewMulti};
            multiplexingControls.ForEach(control => control.Enabled = false);

            // register control lists out of tables
            dimmingCheckboxes = tblDimming.Controls.OfType<CheckBox>().ToList();
            dimmingCheckboxesMulti = tblDimmingMulti.Controls.OfType<CheckBox>().ToList();
            PWMNumerics = tblPWM.Controls.OfType<NumericUpDown>().ToList();
            PWMNumericsMulti = tblPWMmulti.Controls.OfType<NumericUpDown>().ToList();
            boostCheckboxes = tblBoost.Controls.OfType<CheckBox>().ToList();
            boostCheckboxesMulti = tblBoostMulti.Controls.OfType<CheckBox>().ToList();
            boostStatusLEDs = tblBoost.Controls.OfType<PictureBox>().ToList();
            boostStatusLEDsMulti = tblBoostMulti.Controls.OfType<PictureBox>().ToList();
            dimmingCheckboxes       = dimmingCheckboxes.OrderBy(x => tblDimming.GetRow(x)).ToList();
            dimmingCheckboxesMulti  = dimmingCheckboxesMulti.OrderBy(x => tblDimmingMulti.GetRow(x)).ToList();
            PWMNumerics             = PWMNumerics.OrderBy(x => tblPWM.GetRow(x)).ToList();
            PWMNumericsMulti        = PWMNumericsMulti.OrderBy(x => tblPWMmulti.GetRow(x)).ToList();
            boostCheckboxes         = boostCheckboxes.OrderBy(x => tblBoost.GetRow(x)).ToList();
            boostCheckboxesMulti    = boostCheckboxesMulti.OrderBy(x => tblBoostMulti.GetRow(x)).ToList();
            boostStatusLEDs         = boostStatusLEDs.OrderBy(x => tblBoost.GetRow(x)).ToList();
            boostStatusLEDsMulti    = boostStatusLEDsMulti.OrderBy(x => tblBoost.GetRow(x)).ToList();

            grdViewMulti_EnabledChanged(this, null);

            Panel = panChipAccess;
        }

        protected override void NewChip(E52138ChipAPI chip)
        {

            if (grdView.DataSource is DataView view)
            {
                view.Dispose();
            }
            // link data table for standard area
            DataView newView = chip.LEDStatusTable.AsDataView();
            newView.RowFilter = "Channel < 12";
            grdView.DataSource = newView;

            if (grdViewMulti.DataSource is DataView viewMulti)
            {
                viewMulti.Dispose();
            }
            DataView newViewMulti = chip.LEDStatusTable.AsDataView();
            newViewMulti.RowFilter = "Channel >= 12";
            grdViewMulti.DataSource = newViewMulti;

            multiplexingControls.ForEach(control => control.Enabled = chip.Multiplexing);
        }

        private void SwitchLEDRed(PictureBox pic, bool enable)
        {
            InvokeWrapper(() =>
            {
                if (enable)
                {
                    pic.Image = ELMOS_521._38_UART_Eval.Properties.Resources.led_red_on;
                }
                else
                {
                    pic.Image = ELMOS_521._38_UART_Eval.Properties.Resources.led_red_off;
                }
            });
        }

        private void SwitchLEDGreen(PictureBox pic, bool enable)
        {
            InvokeWrapper(() =>
            {
                if (enable)
                {
                    pic.Image = ELMOS_521._38_UART_Eval.Properties.Resources.led_green_on;
                }
                else
                {
                    pic.Image = ELMOS_521._38_UART_Eval.Properties.Resources.led_green_off;
                }
            });
        }

        protected override void UpdateData(string propertyName, bool force)
        {
            if (propertyName == "multiplexing" || force)
            {
                InvokeWrapper(() =>
                {
                    multiplexingControls.ForEach(control => control.Enabled = chip.Multiplexing);
                    tblDimmingMulti.Controls.OfType<CheckBox>().ToList().ForEach(checkbox => checkbox.Checked = false);
                });
            }
            if (propertyName == "pWMValues" || force)
            {
                numPWM_ValueChanged_active = false;
                InvokeWrapper(() =>
                {
                    for (int i = 0; i < Properties.Settings.Default.StandardChannels; i++)
                    {
                        PWMNumerics[i].Value = chip.PWMValues[i];

                    }
                    for (int i = Properties.Settings.Default.StandardChannels; i < Properties.Settings.Default.Channels; i++)
                    {
                        PWMNumericsMulti[i - Properties.Settings.Default.StandardChannels].Value = chip.PWMValues[i];
                    }
                });
                numPWM_ValueChanged_active = true;
            }
            if (propertyName == "boostStatus" || force)
            {
                for (int i = 0; i < Properties.Settings.Default.StandardChannels; i++)
                {
                    SwitchLEDRed(boostStatusLEDs[i], chip.BoostStatus[i]);
                }
                for (int i = Properties.Settings.Default.StandardChannels; i < Properties.Settings.Default.Channels; i++)
                {
                    SwitchLEDRed(boostStatusLEDsMulti[i - Properties.Settings.Default.StandardChannels], chip.BoostStatus[i]);
                }
            }
            if (propertyName == "comperatorBISTStatus" || force)
            {
                SwitchLEDGreen(picCompVDDD_UV, chip.ComperatorBISTStatus.VDDD_uv);
                SwitchLEDGreen(picCompVDDD_OV, chip.ComperatorBISTStatus.VDDD_ov);
                SwitchLEDGreen(picCompVDDA_UV, chip.ComperatorBISTStatus.VDDA_uv);
                SwitchLEDGreen(picCompVDDA_OV, chip.ComperatorBISTStatus.VDDA_ov);
                SwitchLEDGreen(picCompVS_UV, chip.ComperatorBISTStatus.VS_uv);
                SwitchLEDGreen(picCompVS_OV, chip.ComperatorBISTStatus.VS_ov);
                SwitchLEDGreen(picCompIREF_VBG1_ERR, chip.ComperatorBISTStatus.IREF_vbg1_err);
                SwitchLEDGreen(picCompIREF_VBG2_ERR, chip.ComperatorBISTStatus.IREF_vbg2_err);
                SwitchLEDGreen(picCompIREF_LOW, chip.ComperatorBISTStatus.IREF_low);
                SwitchLEDGreen(picCompIREF_HIGH, chip.ComperatorBISTStatus.IREF_high);
                SwitchLEDGreen(picCompOVT, chip.ComperatorBISTStatus.OVT);
                SwitchLEDGreen(picCompVS_CRIT, chip.ComperatorBISTStatus.VS_crit);
            }
            if (propertyName == "anyShort" || force)
            {
                SwitchLEDRed(picLEDStatusShort, chip.AnyShort);
            }
            if (propertyName == "anyOpen" || force)
            {
                SwitchLEDRed(picLEDStatusOpen, chip.AnyOpen);
            }
        }

        public void SetPWMValues(int[] values)
        {
            foreach (var value in values)
            {
                Console.Write(value);
            }
            Console.WriteLine();

            InvokeWrapper(() =>
            {
                for (int i = 0; i < Properties.Settings.Default.StandardChannels; i++)
                {
                    PWMNumerics[i].Value = values[i];
            
                }
                for (int i = Properties.Settings.Default.StandardChannels; i < Properties.Settings.Default.Channels; i++)
                {
                    PWMNumericsMulti[i - Properties.Settings.Default.StandardChannels].Value = values[i];
                }

            });
        }

        private void grdViewMulti_EnabledChanged(object sender, EventArgs e)
        {
            InvokeWrapper(() =>
            {
                if (grdViewMulti.Enabled)
                {
                    grdViewMulti.DefaultCellStyle.SelectionBackColor = SystemColors.ControlLight;
                    grdViewMulti.DefaultCellStyle.SelectionForeColor = SystemColors.ControlText;
                    grdViewMulti.DefaultCellStyle.ForeColor = SystemColors.ControlText;
                }
                else
                {
                    grdViewMulti.DefaultCellStyle.SelectionBackColor = SystemColors.ControlLightLight;
                    grdViewMulti.DefaultCellStyle.SelectionForeColor = SystemColors.ControlLight;
                    grdViewMulti.DefaultCellStyle.ForeColor = SystemColors.ControlLight;
                }
            });
        }

        private void butDimmingAll_Click(object sender, EventArgs e)
        {
            InvokeWrapper(() =>
            {
                dimmingCheckboxes.ForEach(checkbox => checkbox.Checked = true);
                dimmingCheckboxesMulti.ForEach(checkbox => checkbox.Checked = true & chip.Multiplexing);
            });
        }

        private void butDimmingNone_Click(object sender, EventArgs e)
        {
            InvokeWrapper(() =>
            {
                dimmingCheckboxes.ForEach(checkbox => checkbox.Checked = false);
                dimmingCheckboxesMulti.ForEach(checkbox => checkbox.Checked = false);
            });
        }

        private void trkDimming_Scroll(object sender, EventArgs e)
        {

            int[] values = (int[])chip.PWMValues.Clone();
            
            foreach (var checkbox in dimmingCheckboxes)
            {
                if (checkbox.Checked)
                {
                    values[tblDimming.GetRow(checkbox)] = trkDimming.Value;
                }
            }
            
            if (chip.Multiplexing)
            {
                foreach (var checkbox in dimmingCheckboxesMulti)
                {
                    if (checkbox.Checked)
                    {
                        values[tblDimmingMulti.GetRow(checkbox) + Properties.Settings.Default.StandardChannels] = trkDimming.Value;
                    }
                }
            }

            chip.PWMValues = values;
        }

        private void butPWMzero_Click(object sender, EventArgs e)
        {
            chip.PWMValues = new int[Properties.Settings.Default.Channels];
        }

        private void chkBoost_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = sender as CheckBox;
            if (checkbox == null) return;

            bool[] status = chip.BoostEnable;
            status[tblBoost.GetRow(checkbox)] = checkbox.Checked;

            chip.BoostEnable = status;
        }

        private void chkBoost_CheckedChangedMulti(object sender, EventArgs e)
        {
            CheckBox checkbox = sender as CheckBox;
            if (checkbox == null) return;

            bool[] status = chip.BoostEnable;
            status[tblBoostMulti.GetRow(checkbox) + Properties.Settings.Default.StandardChannels] = checkbox.Checked;

            chip.BoostEnable = status;
        }

        private void butBoostAll_Click(object sender, EventArgs e)
        {
            InvokeWrapper(() =>
            {
                boostCheckboxes.ForEach(checkbox => checkbox.Checked = true);
                boostCheckboxesMulti.ForEach(checkbox => checkbox.Checked = true & chip.Multiplexing);
            });
        }

        private void butBoostNone_Click(object sender, EventArgs e)
        {
            InvokeWrapper(() =>
            {
                boostCheckboxes.ForEach(checkbox => checkbox.Checked = false);
                boostCheckboxesMulti.ForEach(checkbox => checkbox.Checked = false);
            });
        }

        private void numPWM_ValueChanged(object sender, EventArgs e)
        {
            if (!numPWM_ValueChanged_active) return;

            NumericUpDown numeric = sender as NumericUpDown;
            if (numeric == null) return;

            int[] values = (int[]) chip.PWMValues.Clone();
            values[tblPWM.GetRow(numeric)] = (int) numeric.Value;

            chip.PWMValues = values;
        }

        private void numPWM_ValueChangedMulti(object sender, EventArgs e)
        {
            if (!numPWM_ValueChanged_active) return;

            NumericUpDown numeric = sender as NumericUpDown;
            if (numeric == null) return;

            int[] values = (int[])chip.PWMValues.Clone();
            values[tblPWMmulti.GetRow(numeric) + Properties.Settings.Default.StandardChannels] = (int)numeric.Value;

            chip.PWMValues = values;
        }

        private void butPWMwrite_Click(object sender, EventArgs e)
        {
            chip.WritePWMValues = true;
            chip.WriteBoostValues = true;
        }

        private void butPWMread_Click(object sender, EventArgs e)
        {
            chip.ReadPWMValues = true;
        }

        private void picLEDStatusOpen_Click(object sender, EventArgs e)
        {
            chip.AnyOpen = false;
        }

        private void picLEDStatusShort_Click(object sender, EventArgs e)
        {
            chip.AnyShort = false;
        }

        private void grdView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is bool value)
            {
                if (value)
                {
                    e.Value = ELMOS_521._38_UART_Eval.Properties.Resources.led_green_on;
                }
                else
                {
                    e.Value = ELMOS_521._38_UART_Eval.Properties.Resources.led_red_on;
                }
            }
        }
    }
}
