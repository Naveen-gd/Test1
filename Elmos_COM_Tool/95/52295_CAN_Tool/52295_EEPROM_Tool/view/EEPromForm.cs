using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using Device_52295_Lib;

namespace _52295_EEPROM_Tool
{
    internal partial class EEPromForm : Form
    {
        private Device _device;
        private MemForm _eepromForm;

        public EEPromForm()
        {
            InitializeComponent();

            if (DeviceType.IsM52295A) this.Text += " (M52295A)";
            if (DeviceType.IsE52295A) this.Text += " (E52295A)";

            _device = new Device();

            _eepromForm = new MemForm(String.Format("Device EEPROM"), new List<Memory> {_device.eeprom});
        }

        private void buttonEditEE_Click(object sender, EventArgs e)
        {
            _eepromForm.Show();
        }

        private void buttonSaveEE_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "txt files (*.txt)|*.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                String filePath = saveFileDialog.FileName;
                _device.eeprom.saveToFile(filePath);
            }
        }

        private void buttonLoadEE_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                String filePath = openFileDialog.FileName;
                _device.eeprom.loadFromFile(filePath);
                _eepromForm.UpdateFromMemory();
                _eepromForm.Show();
            }
        }

    }
}
