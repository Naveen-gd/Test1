using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using _52294_UCAN_Tool;
using _52295_CAN_Tool;
using Device_52295_Lib;
using ELMOS_521._38_UART_Eval;

namespace Elmos_COM_Tool
{
    public partial class ComTool : Form
    {
        private _52294_UCAN_Tool.MainForm mainForm94 = null;
        private _52295_CAN_Tool.MainForm mainForm95 = null;
        private ELMOS_521._38_UART_Eval.FormMain mainForm38 = null;
        public ComTool()
        {
            InitializeComponent();
            this.Size = new Size(1010, 884);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            //this.AutoScroll = true;
            // Set the auto-scroll position to the maximum value to scroll to the bottom
            
            if (rdoUARTButton.Checked)
            { 
                cmb_ComDevices.Items.Add("--Select chip--");
                cmb_ComDevices.Items.Add("--E521.38--");
                cmb_ComDevices.Items.Add("--E522.59--");
                cmb_ComDevices.Items.Add("--E522.94--");
                cmb_ComDevices.Items.Add("--E522.96--");
                cmb_ComDevices.SelectedIndex = 0;
            }
            else
            {
                cmb_ComDevices.Items.Add("--Select Chip--");
                cmb_ComDevices.Items.Add("--E522.95--");
                cmb_ComDevices.Items.Add("--E522.96--");
                //cmb_ComDevices.Items.Add("--E522.94--");
                cmb_ComDevices.SelectedIndex = 0;
            }

        }
       
        private void cmb_ComDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            //_52294_UCAN_Tool.MainForm mainForm94 = null;
            //_52295_CAN_Tool.MainForm mainForm95 = null;
            //ELMOS_521._38_UART_Eval.FormMain mainForm38 = null;
            // //FormMain mainForm38 = null;
            if (mainForm95 != null)
            {
                MethodInfo privMethod = mainForm95.GetType().GetMethod("MainForm_FormClosing", BindingFlags.NonPublic | BindingFlags.Instance);
                privMethod.Invoke(mainForm95, new object[] { this, new FormClosingEventArgs(CloseReason.None, false) });
                //mainForm95.MainForm_FormClosing(this, new FormClosingEventArgs(CloseReason.None, false));
                mainForm95 = null;
            }
            if (mainForm94 != null)
            {
                MethodInfo privMethod = mainForm94.GetType().GetMethod("MainForm_FormClosing", BindingFlags.NonPublic | BindingFlags.Instance);
                privMethod.Invoke(mainForm94, new object[] { this, new FormClosingEventArgs(CloseReason.None, false) });
                //mainForm95.MainForm_FormClosing(this, new FormClosingEventArgs(CloseReason.None, false));
                mainForm94 = null;
                //mainForm94.MainForm_FormClosing(this, new FormClosingEventArgs(CloseReason.None, false));
                //mainForm94 = null;
            }
            if (mainForm38 != null)
            {
                MethodInfo privMethod = mainForm38.GetType().GetMethod("FormMain_FormClosed", BindingFlags.NonPublic | BindingFlags.Instance);
                privMethod.Invoke(mainForm38, new object[] { this, new FormClosedEventArgs(CloseReason.None) });
                mainForm38 = null;
            }

            // 38 UART
            if (cmb_ComDevices.SelectedIndex == 1 && rdoUARTButton.Checked)
            {
                tabPage.Controls.Clear();
                ApplicationData data = new ApplicationData();

                ApplicationController controller = new ApplicationController(data);
                mainForm38 = new FormMain(data);
                mainForm38.TopLevel = false;
                mainForm38.FormBorderStyle = FormBorderStyle.None;
                tabPage.Controls.Add(mainForm38);
                tabPage.Size = this.Size;
                tabPage.AutoScroll = true;
                tabPage.Text = "E521.38";
                mainForm38.Dock = DockStyle.Fill;
                mainForm38.Show();

            }
            // 59 for UART
            if (cmb_ComDevices.SelectedIndex == 2 && rdoUARTButton.Checked)
            {
                tabPage.Controls.Clear();
                mainForm94 = new _52294_UCAN_Tool.MainForm();
                mainForm94.TopLevel = false;
                mainForm94.FormBorderStyle = FormBorderStyle.None;
                tabPage.Controls.Add(mainForm94);
                tabPage.Size = this.Size;
                tabPage.AutoScroll = true;
                tabPage.AutoScrollPosition = new Point(0, tabPage.VerticalScroll.Maximum);
                tabPage.Text = "E522.59";
                mainForm94.Dock = DockStyle.Fill;
                mainForm94.Show();
            }
            // 94 UART  
            if (cmb_ComDevices.SelectedIndex == 3 && rdoUARTButton.Checked)
            {
                tabPage.Controls.Clear();
                mainForm94 = new _52294_UCAN_Tool.MainForm();
                mainForm94.TopLevel = false;
                mainForm94.FormBorderStyle = FormBorderStyle.None;
                tabPage.Controls.Add(mainForm94);
                //mainForm95.MainForm_FormClosing(this, new FormClosingEventArgs(CloseReason.None, false));
                //tabPage.Size = this.Size;
                tabPage.AutoScroll = true;
                tabPage.AutoScrollPosition = new Point(0, tabPage.VerticalScroll.Maximum);
                tabPage.AutoScrollMargin = new Size(0, 20);
                tabControl_Device.AutoScrollOffset = new Point(0, 100);
                tabPage.Text = "E522.94";
                mainForm94.Dock = DockStyle.Fill;
                mainForm94.Show();

            }
            // 96 for UART
            if (cmb_ComDevices.SelectedIndex == 4 && rdoUARTButton.Checked)
            {
                tabPage.Controls.Clear();
                mainForm94 = new _52294_UCAN_Tool.MainForm();
                mainForm94.TopLevel = false;
                mainForm94.FormBorderStyle = FormBorderStyle.None;
                tabPage.Controls.Add(mainForm94);
                tabPage.Size = this.Size;
                tabPage.AutoScroll = true;
                tabPage.Text = "E522.96";
                mainForm94.Dock = DockStyle.Fill;
                mainForm94.Show();

            }
            // 95 FDCAN
            if (cmb_ComDevices.SelectedIndex == 1 && rdoFDCANButton.Checked)
            {
                tabPage.Controls.Clear();
                DeviceType.SetE52295A();
                mainForm95 = new _52295_CAN_Tool.MainForm();
                mainForm95.TopLevel = false;
                mainForm95.FormBorderStyle = FormBorderStyle.None;
                tabPage.Controls.Add(mainForm95);
                tabPage.Size = this.Size;
                tabPage.AutoScroll = true;
                tabPage.Text = "E522.95";
                mainForm95.Dock = DockStyle.Fill;
                mainForm95.Show();
            }
            //96 from FDCAN protocol
            if (cmb_ComDevices.SelectedIndex == 2 && rdoFDCANButton.Checked)
            {
                tabPage.Controls.Clear();
                DeviceType.SetE52295A();
                mainForm95 = new _52295_CAN_Tool.MainForm();
                mainForm95.TopLevel = false;
                mainForm95.FormBorderStyle = FormBorderStyle.None;
                tabPage.Controls.Add(mainForm95);
                tabPage.Size = this.Size;
                tabPage.AutoScroll = true;
                tabPage.Text = "E522.96";
                mainForm95.Dock = DockStyle.Fill;
                mainForm95.Show();
            }
            //tabPage.AutoScrollPosition = new System.Drawing.Point(0, tabPage.Height);
            //if (tabPage.Controls.Count > 0)
            //    tabPage.Controls[0].Dispose();

            //if (mainForm != null && !mainForm.IsDisposed)
            //{
            //    mainForm.Close();
            //}
            //else if (cmb_ComDevices.SelectedIndex != 1 && !rdoUARTButton.Checked)
            //{
            //    if (mainForm1 != null && !mainForm1.IsDisposed)
            //    {
            //        mainForm1.Close();
            //    }
            //}
            //if (mainForm95 != null && !mainForm95.IsDisposed)
            //{
            //    mainForm95.MainForm_FormClosing(this, new FormClosingEventArgs(CloseReason.None, false));
            //}

            //if (cmb_ComDevices.SelectedIndex != 1 && rdoFDCANButton.Checked)
            //{
            //    //_52295_CAN_Tool.MainForm mfrm = new _52295_CAN_Tool.MainForm();
            //    //mfrm.MainForm_FormClosing += FormClosing
            //}

        }
        
        private void rdoFDCANButton_CheckedChanged(object sender, EventArgs e)
        {
           if(rdoFDCANButton.Checked)
            {
                cmb_ComDevices.Items.Clear();
                cmb_ComDevices.Items.Add("--Select Chip--");
                cmb_ComDevices.Items.Add("--E522.95--");
                cmb_ComDevices.Items.Add("--E522.96--");
                cmb_ComDevices.SelectedIndex = 0;
            }
        }

        private void rdoUARTButton_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoUARTButton.Checked)
            {
                cmb_ComDevices.Items.Clear();
                cmb_ComDevices.Items.Add("--Select Chip--");
                cmb_ComDevices.Items.Add("--E521.38--");
                cmb_ComDevices.Items.Add("--E522.59--");
                cmb_ComDevices.Items.Add("--E522.94--");
                cmb_ComDevices.Items.Add("--E522.96--");
                cmb_ComDevices.SelectedIndex = 0;
            }
        }

        private void ComTool_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnComm_Click(object sender, EventArgs e)
        {
            ComportComm comm = new ComportComm();
            comm.ShowDialog();
        }
    }
}
