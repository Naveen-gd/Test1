using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;

namespace ELMOS_521._38_UART_Eval.Animations
{
    class AnimationHandler
    {
        private ScriptEngine engine;
        private ScriptScope scope;
        private ScriptSource source;

        private ApplicationData data;

        private Thread task;
        private bool run;

        public string AnimationModule { get; private set; } = "";

        public AnimationHandler(ApplicationData data)
        {
            this.data = data;
            
            engine = Python.CreateEngine();

            ICollection<string> paths = engine.GetSearchPaths();
            paths.Add(@".\Animations");
            engine.SetSearchPaths(paths);
        }

        public bool LoadModule(string filename)
        {
            dynamic time = engine.ImportModule("time");
            AnimationModule = filename;
            
            Dictionary<String, Object> dict = new Dictionary<string, object>
            {
                { "sleep", time.sleep },
                { "setPWM", new SetPWMDelegate(SetPWM) },
                { "setLED", new SetLEDDelegate(SetLED) },
            };

            source = engine.CreateScriptSourceFromFile(filename);
            scope = engine.CreateScope(dict);

            return true;
        }

        private void Run()
        {
            while(run)
            {
                try
                {
                    source.Execute(scope);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Python script aborted: {0}", e.Message);
                    Thread.Sleep(1000);
                }
            }
        }

        public void Start()
        {
            if (run) return;

            run = true;

            task = new Thread(Run);
            task.Start();
        }

        public void Stop()
        {
            task?.Abort();
            run = false;

            Console.WriteLine("Python script terminated.");
        }

        private delegate void SetPWMDelegate(int device, IronPython.Runtime.List values);
        private void SetPWM(int device, IronPython.Runtime.List values)
        {
            int[] valuesArr = new int[Properties.Settings.Default.Channels];

            for (int i = 0; i < Math.Min(values.Count, valuesArr.Length); i++)
                if (values[i] is int integer)
                    valuesArr[i] = integer;

            if (data.chips.TryGetValue(device, out E52138ChipAPI chip)) {
                chip.PWMValues = valuesArr;
            }
            else
            {
                Console.WriteLine("AnimationHandler: Chip {0} not found", device);
            }
        }

        private delegate void SetLEDDelegate(int led, IronPython.Runtime.List RGB = null, IronPython.Runtime.List HSV = null);
        private void SetLED(int led, IronPython.Runtime.List RGB = null, IronPython.Runtime.List HSV = null)
        {
            int[] valuesArr = null;

            if (RGB != null)
            {
                valuesArr = new int[3];
                for (int i = 0; i < Math.Min(RGB.Count, valuesArr.Length); i++)
                    if (RGB[i] is int integer)
                        valuesArr[i] = integer;
            }
            else if (HSV != null)
            {
                Helper.HSV hsv = new Helper.HSV();
                valuesArr = new int[3];

                try
                {
                    if (HSV[0] is int H) hsv.H = H;
                    if (HSV[1] is int S) hsv.S = S;
                    if (HSV[2] is int V) hsv.V = V;

                    Color color = hsv.RGB();

                    valuesArr[0] = color.R;
                    valuesArr[1] = color.G;
                    valuesArr[2] = color.B;
                }
                catch
                {
                    Console.WriteLine("AnimationHandler: SetRGB Command HSV value must be a list of 3 integer elements.");
                    return;
                }
            }

            if (valuesArr == null)
            {
                Console.WriteLine("AnimationHandler: SetRGB Command had no valid data.");
                return;
            }
            // TODO remove magic
            int chip_led = ((led - 1) % 6);
            int device = led - chip_led;
            if (data.chips.TryGetValue(device, out E52138ChipAPI chip))
            { 
                int[] values = (int[])chip.PWMValues.Clone();

                bool isSet = false;

                for (int channel = 0; channel < valuesArr.Length; channel++)
                {
                    if (chip.Matrix[chip_led, channel] > -1)
                    {
                        values[chip.Matrix[chip_led, channel]] = valuesArr[channel];
                        isSet = true;
                    }
                }

                if (isSet)
                {
                    chip.PWMValues = values;
                }
                else
                {
                    Console.WriteLine("AnimationHandler: LED {0} not patched.", led);
                }
            }
            else
            {
                Console.WriteLine("AnimationHandler: Chip {0} not found", device);
            }
        }
    }
}
