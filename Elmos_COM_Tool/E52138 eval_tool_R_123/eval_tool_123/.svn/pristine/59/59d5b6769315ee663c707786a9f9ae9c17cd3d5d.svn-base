using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELMOS_521._38_UART_Eval.Helper
{
    public class HSV
    {
        public float H;
        public float S;
        public float V;

        public HSV() { }
        public HSV(Color color)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));
            
            H = color.GetHue();
            if (max == 0)
            {
                S = 0;
            }
            else
            {
                S = (255 - (255 * ((float)min) / max));
            }
            V = max;
        }

        public Color RGB()
        {
            if (S == 0)
            {
                // gray
                return Color.FromArgb((int)V, (int)V, (int)V);
            }

            int h = (int) Math.Floor(H / 60);
            float f = H / 60 - h;

            int p = (int) (V * (1 - S / 255));
            int q = (int) (V * (1 - S / 255 * f));
            int t = (int) (V * (1 - S / 255 * (1 - f)));

            switch(h)
            {
                case 0:
                case 6:
                    return Color.FromArgb((int) V, t, p);
                case 1:
                    return Color.FromArgb(q, (int)V, p);
                case 2:
                    return Color.FromArgb(p, (int)V, t);
                case 3:
                    return Color.FromArgb(p, q, (int)V);
                case 4:
                    return Color.FromArgb(t, p, (int)V);
                case 5:
                    return Color.FromArgb((int)V, p, q);
                default:
                    return Color.Black;
            }
        }
    }
}
