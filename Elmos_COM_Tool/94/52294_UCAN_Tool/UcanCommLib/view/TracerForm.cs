using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UcanCommLib
{
    public partial class TracerForm : Form
    {
        public Tracer tracer
        {
            get { return _tracerControl.tracer; }
        }


        public TracerForm()
        {
            InitializeComponent();
        }

        private void TracerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
