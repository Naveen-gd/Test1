﻿using IronPython.Hosting;
using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Scripting.Hosting;
using System.ComponentModel;

namespace ELMOS_521._38_UART_Eval
{
    public class ApplicationController
    {
        private ApplicationData data;
        private readonly dynamic diffUARTAPI;

        private ScriptEngine engine;

        public ApplicationController(ApplicationData data)
        {
            this.data = data;
            data.PropertyChanged += DataChanged;

            var engineOptions = new Dictionary<string, object> { ["LightweightScopes"] = true };
            engine = Python.CreateEngine(engineOptions);

            ICollection<string> paths = engine.GetSearchPaths();

            paths.Add(@".\Lib");
            engine.SetSearchPaths(paths);

            dynamic e52138pyModule = engine.ImportModule("e52138");
            data.E52138py = e52138pyModule.E52138DiffUART;
            data.E52138AutoAddressingMasterPy = e52138pyModule.E52138AutoAddressingMaster;

            diffUARTAPI = engine.ImportModule("DiffUARTAPI");

            engine.Runtime.IO.RedirectToConsole();

            data.chips = new Dictionary<int, E52138ChipAPI>();
        }

        ~ApplicationController()
        {
            if (data.API != null)
            {
                data.API.close();
            }
        }

        private void EstablishUARTConnection()
        {
            try
            {
                data.API = diffUARTAPI.DiffUARTAPI(data.COMPort, data.BaudRate);
                data.ConnectionEstablished = true;
            }
            catch (System.Exception e) when (e.Message.StartsWith("Could not open transport"))
            {
                Console.WriteLine(e.Message);
                data.ConnectionEstablished = false;
            }
            
        }

        private void SetupFileLogger()
        {
            foreach (E52138ChipAPI chip in data.chips.Values)
            {
                chip.SetDiagnoseMode(true);
            }

            try
            {
                FileStream outstream = new FileStream("./bus.log", FileMode.Create, FileAccess.Write);
                engine.Runtime.IO.SetOutput(outstream, System.Text.Encoding.Default);
            }
            catch { }
        }

        private void DisableFileLogger()
        {
            foreach (E52138ChipAPI chip in data.chips.Values)
            {
                chip.SetDiagnoseMode(false);
            }

            var stream = engine.Runtime.IO.OutputStream;
            engine.Runtime.IO.RedirectToConsole();
            stream.Dispose();
        }

        private void CloseUARTConnection()
        {
            dynamic api = data.API;
            data.API = null;
            
            api.close();
            
            data.ConnectionEstablished = false;
        }

        private void DataChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "establishConnection")
            {
                if (data.EstablishConnection)
                {
                    EstablishUARTConnection();
                }
                else
                {
                    CloseUARTConnection();
                }
            }
            else if (e.PropertyName == "writeLogFile")
            {
                if (data.WriteLogToFile)
                {
                    SetupFileLogger();
                }
                else
                {
                    DisableFileLogger();
                }
            }
        }
    }
}
