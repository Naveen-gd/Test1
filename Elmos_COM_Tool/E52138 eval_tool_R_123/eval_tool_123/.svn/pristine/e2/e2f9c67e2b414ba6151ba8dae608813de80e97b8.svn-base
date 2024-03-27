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

    public partial class LEDAccess : PanelForm
    {
        private ColorPicker[] pickers;

        private bool propertyChangedLock = false;
        private bool patchButtonClicked = false;

        public LEDAccess() : base()
        {

            InitializeComponent();

            Panel = panLEDAccess;

            pickers = new ColorPicker[6] { ColPi0, ColPi1, ColPi2, ColPi3, ColPi4, ColPi5 };
            ColPi0.GrabberColorChange += (s, e) => { ColorChangedHandler(s, e, 0); };
            ColPi1.GrabberColorChange += (s, e) => { ColorChangedHandler(s, e, 1); };
            ColPi2.GrabberColorChange += (s, e) => { ColorChangedHandler(s, e, 2); };
            ColPi3.GrabberColorChange += (s, e) => { ColorChangedHandler(s, e, 3); };
            ColPi4.GrabberColorChange += (s, e) => { ColorChangedHandler(s, e, 4); };
            ColPi5.GrabberColorChange += (s, e) => { ColorChangedHandler(s, e, 5); };
        }

        protected override void UpdateData(string propertyName, bool force = false)
        {
            if (propertyName == "multiplexing" || force)
            {
                InvokeWrapper(() =>
                {
                    ColPi4.Enabled = chip.Multiplexing;
                    ColPi5.Enabled = chip.Multiplexing;
                });
            }
            if (propertyName == "pWMValues" || force)
            {
                if (propertyChangedLock) return;

                InvokeWrapper(() =>
                {
                    for (int led = 0; led < 4; led++)
                    {
                        UpdateValues(led);
                    }
                    if (chip.Multiplexing)
                    {
                        UpdateValues(4);
                        UpdateValues(5);
                    }
                });
            }
        }

        private void UpdateValues(int ledNumber)
        {
            bool isSet = false;

            Color color = pickers[ledNumber].Color;
            int[] RGB = new int[] { color.R, color.G, color.B };

            for (int channel = 0; channel < RGB.Length; channel++)
            {
                if (chip.Matrix[ledNumber, channel] > -1)
                {
                    RGB[channel] = chip.PWMValues[chip.Matrix[ledNumber, channel]];
                    isSet = true;
                }
            }

            if (isSet) pickers[ledNumber].Color = Color.FromArgb(RGB[0], RGB[1], RGB[2]);
        }

        private void ColorChangedHandler(object sender, ColorPicker.GrabberColorChangeArgs e, int led)
        {
            
            int[] values = (int[])chip.PWMValues.Clone();
            if (patchButtonClicked)
            {
                if (chip.Matrix[led, 0] > -1)
                    values[chip.Matrix[led, 0]] = e.GrabberColor.B;
                if (chip.Matrix[led, 1] > -1)
                    values[chip.Matrix[led, 1]] = e.GrabberColor.R;
                if (chip.Matrix[led, 2] > -1)
                    values[chip.Matrix[led, 2]] = e.GrabberColor.G;
            }

            propertyChangedLock = true;
            chip.PWMValues = values;
            propertyChangedLock = false;
        }

        private bool MatrixUnconfigured(int[,] matrix)
        {
            foreach (int element in matrix)
            {
                if (element > -1) return false;
            }
            return true;
        }

        private void ButRGBpatch_Click(object sender, EventArgs e)
        {
            PatchMatrixForm patchMatrix = new PatchMatrixForm(ref chip.Matrix, chip.Multiplexing);

            if (!patchButtonClicked && MatrixUnconfigured(chip.Matrix))
            {
                patchMatrix.DefaultConfiguration();
                chip.SendPatchMatrix();
                
            }
            patchButtonClicked = true;
            patchMatrix.ShowDialog();

            chip.SendPatchMatrix();
            UpdateData("", force: true);
        }
    }

    
}
