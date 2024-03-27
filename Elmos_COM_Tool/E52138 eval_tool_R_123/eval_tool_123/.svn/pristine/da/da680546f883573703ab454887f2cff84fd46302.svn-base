using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELMOS_521._38_UART_Eval
{
    public partial class FormInfo : Form
    {
        public FormInfo()
        {
            InitializeComponent();

            LabVersion.Text = "Version " + Assembly.GetExecutingAssembly().GetName().Version.Major.ToString()
                            + "." + Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString()
                            + "." + Assembly.GetExecutingAssembly().GetName().Version.Build.ToString();
        }

        private void ButClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
