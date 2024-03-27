using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Device_52294_Lib;

namespace _52294_UCAN_Tool
{
    internal partial class QuickProgForm : Form
    {
        private Device _device;
        private String _filePath;

        public QuickProgForm(Device device)
        {
            InitializeComponent();

            label_file_name.Text = "n/a";
            button_prog.Enabled = false;
            statusLed.CheckState = CheckState.Indeterminate;

            _device = device;

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
            //TODO: _device.ReadEeprom();   // device EE as default
            _device.parameters.standalone.loadFromFile(_filePath);
            //TODO: _device.SendCommandUnlockEeprom();
            //TODO: _device.WriteEeprom();
            //TODO: if (_device.VerifyEeprom())
            //TODO: {
            //TODO:     statusLed.CheckState = CheckState.Checked;
            //TODO: }
            //TODO: else
            //TODO: {
            //TODO:     statusLed.CheckState = CheckState.Unchecked;
            //TODO: }
        }
    }
}
