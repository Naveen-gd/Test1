using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELMOS_521._38_UART_Eval.DeviceTabPanel
{
    public interface DeviceTabPanelBase
    {
        Panel getPanel();

        void Close();
    }
}
