using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Extensions
{
    public partial class VersionForm : Form
    {
        private String PASS = "elmos";
        private String _buffer;

        public bool passOk;

        public void SetText(string text)
        {
            labelVersion.Text = text;
        }
        
        public VersionForm()
        {
            InitializeComponent();

            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            _buffer = "";

            passOk = false;
        }

        private void VersionForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            _buffer += e.KeyChar;

            if (_buffer.Length > PASS.Length)
                _buffer = "";

            if (_buffer == PASS)
                passOk = true;
        }
    }
}
