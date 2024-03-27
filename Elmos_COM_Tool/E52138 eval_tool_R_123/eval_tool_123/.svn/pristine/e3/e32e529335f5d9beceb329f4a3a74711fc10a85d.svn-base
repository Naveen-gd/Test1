using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELMOS_521._38_UART_Eval
{
    public class FileLoader
    {
        public class ChipsAddedEventArgs : EventArgs
        {
            public List<E52138ChipAPI> Chips;
            public ChipsAddedEventArgs(List<E52138ChipAPI> chips) => Chips = chips;
        }
        public event EventHandler<ChipsAddedEventArgs> ChipsAddedEvent;
        public event EventHandler FilePathSet;

        public class FileStructure
        {
            public string Version;
            public List<E52138ChipAPI> Devices;
        }

        private readonly Version version;
        private readonly ApplicationData data;
        private readonly FormMain main;

        private string filePath;

        public FileLoader(ApplicationData data, FormMain main)
        {
            version = new Version(0, 1, 0);

            this.data = data;
            this.main = main;
        }

        public void Open()
        {
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "./Save/";
                openFileDialog.Filter = "JSON (*.json)|*.json|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                }
            }

            if (filePath.EndsWith(".json"))
            {
                StreamReader file = null;
                try
                {
                    file = File.OpenText(filePath);

                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        FileStructure struc = new FileStructure();

                        JsonSerializer serializer = new JsonSerializer();
                        struc = serializer.Deserialize<FileStructure>(reader);

                        //TODO version check

                        foreach (E52138ChipAPI chip in struc.Devices)
                        {
                            if (chip != null)
                            {
                                chip.StartFromJson(data);
                            }
                        }
                        ChipsAddedEvent(this, new ChipsAddedEventArgs(struc.Devices));

                        this.filePath = filePath;
                        FilePathSet(this, null);
                    }
                }
                finally
                {
                    if(file != null)
                    {
                        file.Dispose();
                    }
                }
            }
        }

        public void Save(List<E52138ChipAPI> chips)
        {
            FileStructure struc = new FileStructure
            {
                Version = version.ToString(),
                Devices = chips
            };
            using (StreamWriter file = File.CreateText(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, struc);
            }
        }

        public void SaveAs(List<E52138ChipAPI> chips)
        {
            var filePath = string.Empty;

            using (SaveFileDialog openFileDialog = new SaveFileDialog())
            {
                openFileDialog.InitialDirectory = "./Save/";
                openFileDialog.Filter = "JSON (*.json)|*.json|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                }
            }

            if (filePath.EndsWith(".json"))
            {
                this.filePath = filePath;
                Save(chips);
            }

        }
    }
}
