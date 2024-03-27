using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

using Device_52294_Lib;

namespace _52294_Param_Tool
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {

#if M52294A
            DeviceType.SetM52294A();
#endif

#if E52294A
            DeviceType.SetE52294A();
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ParamForm());
        }
    }
}
