using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _52294_UCAN_Tool.view
{
    public partial class ExpertComSettingsForm : Form
    {
        private Dictionary<uint, String> _parity;
        private Dictionary<uint, String> _stopBits;
        private Dictionary<uint, String> _scope;
        private Dictionary<uint, String> _targetOperator;
        public ExpertComSettingsForm()
        {
            InitializeComponent();
            this.AutoSize= true;
            // parity
            _parity = new Dictionary<uint, String>();
            _parity[0] = "EVEN";
            _parity[1] = "ODD";
            foreach (KeyValuePair<uint, String> br in _parity)
            {
                comboBox_comm_parity.Items.Add(br.Value);
            }
            comboBox_comm_parity.SelectedIndex = 0; // parity

            // Stop bits
            _stopBits = new Dictionary<uint, String>();
            _stopBits[0] = "One";
            _stopBits[1] = "Two";
            foreach (KeyValuePair<uint, String> br in _stopBits)
            {
                cmbStopBits.Items.Add(br.Value);
            }
            cmbStopBits.SelectedIndex = 0; // stopbits

            // Scope
            _scope = new Dictionary<uint, String>();
            _scope[0] = "All nodes";
            _scope[1] = "Individual node";
            foreach (KeyValuePair<uint, String> br in _scope)
            {
                cmbScope.Items.Add(br.Value);
            }
            cmbScope.SelectedIndex = 0; // Scope

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

        }

        private void rdo3BHeader_CheckedChanged(object sender, EventArgs e)
        {
            if (rdo3BHeader.Checked)
            {
                txtLiveCount.Enabled = false;
                txtLiveCount.Text = "none";

            }
        }

        private void rdo4BHeader_CheckedChanged(object sender, EventArgs e)
        {
            if(rdo4BHeader.Checked)
            {
                txtLiveCount.Enabled = true;
                txtLiveCount.Text = "0x00";

            }
        }

        private void chkEditCRC_CheckedChanged(object sender, EventArgs e)
        {
            if(chkEditCRC.Checked)
            {
                txtCRC.Enabled = true;
            }
            else
                txtCRC.Enabled = false;
        }
    }
}
