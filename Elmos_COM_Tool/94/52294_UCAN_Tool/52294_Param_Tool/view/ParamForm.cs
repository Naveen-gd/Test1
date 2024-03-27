using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using Device_52294_Lib;
using MemLib;

namespace _52294_Param_Tool
{
    internal partial class ParamForm : Form
    {
        private DeviceParameters _deviceParams;
        private MemForm _memForm;

        public ParamForm()
        {
            InitializeComponent();

            if (DeviceType.IsM52294A) this.Text += " (M52294A)";
            if (DeviceType.IsE52294A) this.Text += " (E52294A)";

            _deviceParams = new DeviceParameters();

            _memForm = new MemForm(String.Format("Device Parameters"), new List<Memory> {_deviceParams.standalone, _deviceParams.busDefConfig, _deviceParams.standaloneExt});
        }

        private void buttonEditEE_Click(object sender, EventArgs e)
        {
            _memForm.Show();
        }

        private void buttonSaveEE_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = DeviceParameters.FILE_FILTER_SAVE;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                String filePath = saveFileDialog.FileName;
                _deviceParams.saveToFile(filePath);
            }
        }

        private void buttonLoadEE_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = DeviceParameters.FILE_FILTER_OPEN;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                String filePath = openFileDialog.FileName;
                _deviceParams.loadFromFile(filePath);
                _memForm.UpdateFromMemory();
                _memForm.Show();
            }
        }

    }
}
