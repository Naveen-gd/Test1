using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Device_52295_Lib;
using Can_Comm_Lib;

namespace _52295_CAN_Tool
{
    internal partial class QuickProgForm : Form
    {
        private byte _deviceId;
        private Device _deviceCopyRef;
        private Master _masterRef;

        private String _filePath;

        public QuickProgForm(Master masterRef, Device deviceCopyRef, byte deviceId)
        {
            InitializeComponent();

            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            label_file_name.Text = "n/a";
            button_prog.Enabled = false;
            statusLed.CheckState = CheckState.Indeterminate;

            _deviceId = deviceId;
            _masterRef = masterRef;
            _deviceCopyRef = deviceCopyRef;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt";

            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.Cancel)
            {
                Load += (s, e) => Close();
                return;
            }

            if (dialogResult == DialogResult.OK)
            {
                _filePath = openFileDialog.FileName;
                label_file_name.Text = _filePath;
                button_prog.Enabled = true;
            }
        }

        private void button_prog_Click(object sender, EventArgs e)
        {
            statusLed.CheckState = CheckState.Indeterminate;
            // device EE as default
            _deviceCopyRef.eeprom = _masterRef.ReadEepromCopy(_deviceId);
            // add file contents
            _deviceCopyRef.eeprom.loadFromFile(_filePath);
            // unlock
            _masterRef.SendCommandUnlockEeprom(_deviceId);
            // write + verify
            BoolString res = _masterRef.WriteEeprom(_deviceId, _deviceCopyRef.eeprom);
            if (res.bval)
            {
                statusLed.CheckState = CheckState.Checked;
                _deviceCopyRef.eeprom.ClearAllModified();
            }
            else
            {
                statusLed.CheckState = CheckState.Unchecked;
            }
        }
    }
}
