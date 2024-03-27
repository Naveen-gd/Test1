using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _52295_CAN_Tool
{
    internal partial class VersionForm : Form
    {
        public void SetText(string text)
        {
            labelVersion.Text = text;
        }
        
        public VersionForm()
        {
            InitializeComponent();

            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }
    }
}
