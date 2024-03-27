using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELMOS_521._38_UART_Eval.DeviceTabPanel
{
    public partial class PatchMatrixForm : Form
    {

        private int[,] matrix;

        public class ConnectPanel : Panel
        {
            private readonly Pen linePen;
            public bool active = false;

            public readonly int led;
            public readonly int color;
            public readonly int channel;

            public ConnectPanel(Color lineColor, bool active, int led, int color, int channel, bool enabled) : base()
            {

                this.led = led;
                this.color = color;
                this.channel = channel;
                this.active = active;

                BackColor = Color.Transparent;
                //Size = new Size(10, 10);
                Margin = new Padding(0);
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

                linePen = new Pen(lineColor, 2);

                if (enabled) Click += ClickEventHandler;
            }

            private void ClickEventHandler(object s, EventArgs e)
            {
                active ^= true;
                
                if (FindForm() is PatchMatrixForm patchMatrix)
                {
                    patchMatrix.DeactivateOthers(led, color, channel);
                }
                Refresh();
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                
                e.Graphics.DrawLine(linePen, -1, Height / 2, Width + 1, Height / 2);

                e.Graphics.FillEllipse(new SolidBrush(Color.Black), Width / 2 - 5, Height / 2 - 5, 10, 10);
                if (!active)
                    e.Graphics.FillEllipse(new SolidBrush(SystemColors.Control), Width / 2 - 3, Height / 2 - 3, 6, 6);
            }
        }

        public void DeactivateOthers(int led, int color, int channel)
        {
            foreach (ConnectPanel panel in TblMatrix.Controls.OfType<ConnectPanel>())
            {
                if ((panel.led == led && panel.color == color) || panel.channel == channel)
                {
                    if (panel.led == led && panel.color == color && panel.channel == channel) continue;

                    panel.active = false;
                    panel.Refresh();
                }
            }
        }

        public void DefaultConfiguration()
        {
            foreach (ConnectPanel panel in TblMatrix.Controls.OfType<ConnectPanel>())
            {
                if (panel.led == panel.channel / 3 && panel.color == panel.channel % 3)
                {
                    panel.active = true;
                    panel.Refresh();
                }
            }
        }

        public PatchMatrixForm(ref int[,] matrix, bool multiplexing)
        {
            InitializeComponent();

            this.matrix = matrix;

            for (int row = 0; row < 6; row++)
            {
                Label label = new Label()
                {
                    Text = "LED " + row.ToString(),
                    Name = "LED" + row.ToString(),
                    TextAlign = ContentAlignment.MiddleRight,
                    Dock = DockStyle.None,
                    Anchor = AnchorStyles.Right,
                    BackColor = Color.Transparent,
                };
                TblMatrix.Controls.Add(label, 0, row * 4 + 3);
                
                
            }
            for (int col = 0; col < Properties.Settings.Default.Channels; col++)
            {
                Label label = new Label()
                {
                    Text = col.ToString(),
                    Name = "LabChannel" + col.ToString(),
                    Size = new Size(20, 15),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Anchor = AnchorStyles.None,
                    BackColor = Color.Transparent,
                };
                TblMatrix.Controls.Add(label, col + 2, 1);
            }

            for (int led = 0; led < 6; led++)
            {
                for (int color = 0; color < 3; color++)
                {
                    AddPickerPanel(led, color, enabled: led < 4 || multiplexing);
                }
            }
        }
        /// <summary>
        /// Hier wird die Farbreihenfolge der Patchmatrix definiert (aktuell oben blau, mitte green, unten rot, wie auf dem Eval-Board)
        /// </summary>
        /// <param name="led">Nummer der aktuell angesteuerten RGB</param>
        /// <param name="color">ID entsprechend der Farbe</param>
        /// <param name="enabled">freigabe-boolwert, ob Pickerpanel enabled ist oder nicht
        /// wichtig für den unterschied von 4er und 6er Modul, da beim 4er 2 reihen der Matrix nicht enabelt sind</param>
        void AddPickerPanel(int led, int color, bool enabled)
        {
            Color rowColor = Color.Gray;

            if (enabled)
            {
                switch (color)
                {
                    case 0:
                        rowColor = Color.DarkBlue;
                        break;
                    case 1:
                        rowColor = Color.DarkGreen;
                        break;
                    case 2:
                        rowColor = Color.DarkRed;
                        break;
                }
            }
            

            for (int channel = 0; channel < Properties.Settings.Default.Channels; channel++)
            {
                ConnectPanel panel = new ConnectPanel(rowColor, matrix[led, color] == channel, led, color, channel, enabled)
                {
                    Dock = DockStyle.Fill,
                    Anchor = AnchorStyles.None,
                    BackColor = Color.Transparent,
                    Name = String.Format("ConPan{0:d}{0:d}{0:dd}", led, color, channel),
                };
                TblMatrix.Controls.Add(panel, channel + 2, (led * 4) + color + 2);
            }
        }

        private void ButOk_Click(object sender, EventArgs e)
        {
            // copy values
            foreach (ConnectPanel panel in TblMatrix.Controls.OfType<ConnectPanel>())
            {
                if (panel.active)
                {
                    matrix[panel.led, panel.color] = panel.channel;
                }
            }
        }
    }
}
