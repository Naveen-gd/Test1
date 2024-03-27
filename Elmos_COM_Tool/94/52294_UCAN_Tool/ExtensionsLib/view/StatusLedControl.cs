using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Extensions
{
    public class StatusLedControl : CheckBox
    {
        [DefaultValue(typeof(Color), "Green")]
        public Color EnabledCheckedColor { get; set; }

        [DefaultValue(typeof(Color), "Red")]
        public Color EnabledUncheckedColor { get; set; }

        [DefaultValue(typeof(Color), "DarkRed")]
        public Color EnabledIndeterminateColor { get; set; }

        [DefaultValue(typeof(Color), "LightGray")]
        public Color DisabledCheckedColor { get; set; }

        [DefaultValue(typeof(Color), "DarkGray")]
        public Color DisabledUncheckedColor { get; set; }

        [DefaultValue(typeof(Color), "Black")]
        public Color DisabledIndeterminateColor { get; set; }

        public void Set(bool value)
        {
            if (value)
                CheckState = CheckState.Checked;
            else
            {
                if (Enabled && ThreeState && Checked) CheckState = CheckState.Indeterminate;
                else CheckState = CheckState.Unchecked;
            }
        }

        public StatusLedControl()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
            EnabledCheckedColor = Color.Green;
            EnabledIndeterminateColor = Color.DarkRed;
            EnabledUncheckedColor = Color.Red;
            DisabledCheckedColor = Color.LightGray;
            DisabledUncheckedColor = Color.DarkGray;
            DisabledIndeterminateColor = Color.Black;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int diameter = Height - Margin.Top;

            var darkColor = Color.Black;
            var lightColor = Color.FromArgb(200, Color.White);
            var cornerAlpha = 80;
            this.OnPaintBackground(e);
            using (var path = new GraphicsPath())
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, diameter, diameter);
                path.AddEllipse(rect);
                rect.Inflate(-1, -1);
                using (var bgBrush = new SolidBrush(darkColor))
                {
                    e.Graphics.FillEllipse(bgBrush, rect);
                }
                using (var pathGrBrush = new PathGradientBrush(path))
                {
                    var color = Color.Empty;
                    if (Enabled)
                    {
                        if (CheckState == CheckState.Checked) color = EnabledCheckedColor;
                        if (CheckState == CheckState.Unchecked) color = EnabledUncheckedColor;
                        if (CheckState == CheckState.Indeterminate) color = EnabledIndeterminateColor;
                    }
                    else
                    {
                        if (CheckState == CheckState.Checked) color = DisabledCheckedColor;
                        if (CheckState == CheckState.Unchecked) color = DisabledUncheckedColor;
                        if (CheckState == CheckState.Indeterminate) color = DisabledIndeterminateColor;
                    }
                    pathGrBrush.CenterColor = color; ;
                    Color[] colors = { Color.FromArgb(cornerAlpha, color) };
                    pathGrBrush.SurroundColors = colors;
                    e.Graphics.FillEllipse(pathGrBrush, rect);
                }
                using (var pathGrBrush = new PathGradientBrush(path))
                {
                    pathGrBrush.CenterColor = lightColor; ;
                    Color[] colors = { Color.Transparent };
                    pathGrBrush.SurroundColors = colors;
                    var r = (float)(Math.Sqrt(2) * diameter / 2);
                    var x = r / 8;
                    e.Graphics.FillEllipse(pathGrBrush, new RectangleF(-x, -x, r, r));
                    e.Graphics.ResetClip();
                }
            }

            TextRenderer.DrawText(e.Graphics, Text, Font,
                    new Rectangle(diameter, 0, Width - diameter, diameter), ForeColor,
                     TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
        }
    }
}
