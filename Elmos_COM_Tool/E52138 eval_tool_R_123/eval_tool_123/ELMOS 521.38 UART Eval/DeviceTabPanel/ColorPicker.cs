using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ELMOS_521._38_UART_Eval.DeviceTabPanel
{
    public partial class ColorPicker : UserControl
    {
        

        bool propertyChangedLock = false;

        public class Grabber : Panel
        {
            const int CIRCLE_WIDTH = 2;
            
            
            public Color PickerColor = Color.Black;

            public Grabber() : base()
            {
                BackColor = Color.Transparent;
                Size = new Size(12, 12);

                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
                
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Pen pen = new Pen(PickerColor, 2);
                
                e.Graphics.DrawEllipse(pen, CIRCLE_WIDTH, CIRCLE_WIDTH, Width - CIRCLE_WIDTH * 2, Height - CIRCLE_WIDTH * 2);
                
            }

            public new Point Location {
                get { return base.Location; }
                set { value.Offset(-Width / 2, -Height / 2); base.Location = value; }
            }
        }


        bool mouseDown = false;
        Grabber grabber;
        const int margin = 5;

        readonly Point center;
        readonly int radius;

        public Color Color {
            get { return Color.FromArgb((int)NumLED0R.Value, (int)NumLED0G.Value, (int)NumLED0B.Value); }
            set { SetColor(value); }
        }


        public class GrabberColorChangeArgs : EventArgs
        {
            public Color GrabberColor;
        }
        public event EventHandler<GrabberColorChangeArgs> GrabberColorChange;

        public ColorPicker()
        {
            InitializeComponent();
            Antialiase();

            center = new Point(PicColorCycle.Width / 2, PicColorCycle.Height / 2);
            radius = PicColorCycle.Width / 2 - margin;
            
            grabber = new Grabber()
            {
                Location = center
            };
            PicColorCycle.Controls.Add(grabber);

            grabber.MouseDown += Grabber_MouseDown;
            grabber.MouseUp += Grabber_MouseUp;
            grabber.MouseMove += Grabber_MouseMove;

            EnabledChanged += (s, e) => { Antialiase(); } ;
        }

        private void UpdateNumValues()
        {
            Color grabberColor = GetColor();

            UpdateRGBNums(grabberColor);
            UpdateHSVNums(grabberColor);
        }

        private void Antialiase()
        {
            Image img = Properties.Resources.color_circle;
            Rectangle outer = new Rectangle(0, 0, img.Width, img.Height);

            using (var g = Graphics.FromImage(img))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                if (!Enabled)
                {
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(200, Color.Gray)))
                        g.FillEllipse(brush, new Rectangle(margin - 1, margin - 1, img.Width - margin + 1, img.Height - margin + 1));
                }
                
                g.DrawImage(img, outer);

                using (SolidBrush brush = new SolidBrush(Color.White))
                    using (GraphicsPath path = new GraphicsPath())
                    {
                        path.AddEllipse(Rectangle.Inflate(outer, -margin, -margin));
                        path.AddRectangle(outer);
                        g.FillPath(brush, path);
                    }
            }

            PicColorCycle.Image = img;
        }

        public Color GetColor()
        {
            Color clr = Properties.Resources.color_circle.GetPixel(grabber.Location.X + grabber.Width / 2, grabber.Location.Y + grabber.Height / 2);
            
            clr = Color.FromArgb(clr.R * TrkBar.Value / 255, clr.G * TrkBar.Value / 255, clr.B * TrkBar.Value / 255);
            return clr;
        }

        private void SetColor(Color color)
        {

            while (propertyChangedLock) { }

            propertyChangedLock = true;

            NumLED0R.Value = color.R;
            NumLED0G.Value = color.G;
            NumLED0B.Value = color.B;

            UpdateHSVNums(color);
            UpdateGrabberPosition(new Helper.HSV(color));

            propertyChangedLock = false;
        }

        private void Grabber_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDown = true;
            }
            Grabber_MouseMove(sender, e);
        }

        private void Grabber_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDown = false;
            }
        }

        private bool RadiusMax(int radius, Point a)
        {
            if (Math.Pow(a.X - center.X, 2) + Math.Pow(a.Y - center.Y, 2) > Math.Pow(radius, 2))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void Grabber_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                Point newLocation = e.Location;
                if (sender is Control control)
                {
                    newLocation.X += control.Location.X;
                    newLocation.Y += control.Location.Y;
                }

                // TODO RadiusMax shall return a position
                if (RadiusMax(radius, newLocation))
                {
                    grabber.Location = newLocation;

                    propertyChangedLock = true;
                    UpdateNumValues();
                    propertyChangedLock = false;
                }
            }
        }


        private void TrkBar_Scroll(object sender, EventArgs e)
        {
            propertyChangedLock = true;
            UpdateNumValues();
            propertyChangedLock = false;
        }

        private void UpdateRGBNums(Color color)
        {
            NumLED0R.Value = color.R;
            NumLED0G.Value = color.G;
            NumLED0B.Value = color.B;

            GrabberColorChange?.Invoke(this, new GrabberColorChangeArgs() { GrabberColor = color });
        }

        private void UpdateHSVNums(Color color)
        {
            Helper.HSV hsv = new Helper.HSV(color);

            NumLED0H.Value = (decimal) hsv.H;
            NumLED0S.Value = (decimal) hsv.S;
            NumLED0V.Value = (decimal) hsv.V;
        }

        private void UpdateGrabberPosition(Helper.HSV hsv)
        {
            /* hsv <=> polar coordinates
             *
             * x = r cos (phi)
             * y = r sin (phi)
             */
            double r   = radius * hsv.S / 255;
            double phi = hsv.H / 360 * 2 * Math.PI;

            double x =   r * Math.Sin(phi) + PicColorCycle.Width / 2;
            double y = - r * Math.Cos(phi) + PicColorCycle.Height / 2;

            grabber.Location = new Point((int) x, (int) y);

            TrkBar.Value = (int) hsv.V;
        }

        private void NumLEDHSV_ValueChanged(object sender, EventArgs e)
        {
            if (propertyChangedLock) return;

            propertyChangedLock = true;
            Helper.HSV hsv = new Helper.HSV()
            {
                H = (float) NumLED0H.Value,
                S = (float) NumLED0S.Value,
                V = (float) NumLED0V.Value,
            };

            UpdateGrabberPosition(hsv);
            UpdateRGBNums(GetColor());

            propertyChangedLock = false;
        }

        private void NumLEDRGB_ValueChanged(object sender, EventArgs e)
        {
            if (propertyChangedLock) return;

            propertyChangedLock = true;
            Color color = Color.FromArgb((int)NumLED0R.Value, (int)NumLED0G.Value, (int)NumLED0B.Value);

            UpdateHSVNums(color);
            UpdateGrabberPosition(new Helper.HSV(color));
            propertyChangedLock = false;
        }
    }
}
