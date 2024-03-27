using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UcanCommLib
{
    public partial class TracerControl : UserControl
    {
        private Tracer _tracer;
        private Timer _updateTimer;

        public Tracer tracer
        {
            get { return _tracer; }
        }

        public TracerControl()
        {
            InitializeComponent();

            textBox1.DoubleBuffered();

            _updateTimer = new Timer();
            _tracer = new Tracer();

            _updateTimer.Interval = 1000;    // ms
            _updateTimer.Tick += new EventHandler(_Update);

            Stop();
        }

        public void NewFrame()
        {
            _tracer.NewFrame();
        }

        public void FrameData(byte[] data, uint numBytesRead, bool timeout)
        {
            _tracer.FrameData(data, numBytesRead, timeout);
        }

        public void Stop()
        {
            button_stop_Click(null, null);
        }

        private void _Update(Object myObject, EventArgs myEventArgs)
        {
            List<TracerData> dataCopy = _tracer.GetClearDataCopy();

            if (dataCopy.Count > 0)
            {
                for (int i = 0; i < dataCopy.Count; i+=1){
                    if (dataCopy[i].GetType().Equals(typeof(TracerDataBreak)))
                    {
                        TracerDataBreak b = (TracerDataBreak)dataCopy[i];
                        String entry = "\r\n" + String.Format("{0,15}", String.Format("{0:F3} s:", Convert.ToDouble(b.elapsedMs) / 1000.0));
                        textBox1.AppendText(entry);
                    }
                    if (dataCopy[i].GetType().Equals(typeof(TracerDataReceived)))
                    {
                        TracerDataReceived r = (TracerDataReceived)dataCopy[i];
                        String entry = String.Empty;
                        for (int b = 0; b < r.data.Count; b += 1)
                            entry += " " + r.data[b].ToHexString();
                        if (r.timeout)
                            entry += " *";
                        textBox1.AppendText(entry);
                    }
                }
            }
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            _tracer.Start();
            _updateTimer.Enabled = true;
            label_status.Text = "RUNNING";
            button_start.Enabled = false;
            button_stop.Enabled = true;
            button_clear.Enabled = false;
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            _updateTimer.Enabled = false;
            _tracer.Stop();
            label_status.Text = "STOPPED";
            button_start.Enabled = true;
            button_stop.Enabled = false;
            button_clear.Enabled = true;
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            _tracer.Clear();
            textBox1.Clear();
        }

    }

}
