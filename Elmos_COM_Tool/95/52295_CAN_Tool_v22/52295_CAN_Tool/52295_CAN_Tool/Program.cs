using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

using Device_52295_Lib;

namespace _52295_CAN_Tool
{
	static class Program
	{
		/// <summary>
		/// Der Haupteinstiegspunkt für die Anwendung.
		/// </summary>
		[STAThread]
		static void Main()
		{

#if M52295A
            DeviceType.SetM52295A();
#endif

#if E52295A
            DeviceType.SetE52295A();
#endif

            Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
		}
	}
}
