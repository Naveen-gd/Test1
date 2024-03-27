using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELMOS_521._38_UART_Eval.DeviceTabPanel
{
    public partial class DeviceTabPanel : Form, DeviceTabPanelBase
    {
        enum Panels : ushort
        {
            GENERAL,
            LED_ACCESS,
            CHIP_ACCESS,
            COMMAND
        }

        private PanelSelector<Panels> PanelSelector;
        private FormMain main;

        public E52138ChipAPI Chip { get; private set; }
        

        public DeviceTabPanel(FormMain main)
        {
            InitializeComponent();

            this.main = main;
            
            // generate panels for device 0
            PanelSelector = new PanelSelector<Panels>(
                new Dictionary<Panels, TabPanel>
                {
                    [Panels.GENERAL] = new TabPanel() { PanelForm = new General(), Selector = labFRMGeneral },
                    [Panels.LED_ACCESS] = new TabPanel() { PanelForm = new LEDAccess(), Selector = labFRMLEDAccess },
                    [Panels.CHIP_ACCESS] = new TabPanel() { PanelForm = new ChipAccess(), Selector = labFRMChipAccess },
                    [Panels.COMMAND] = new TabPanel() { PanelForm = new Command(), Selector = labFRMCommand }
                },
                panFrame
            );
        }

        public Panel getPanel()
        {
            return panDeviceTab;
        }

        public void SetChip(E52138ChipAPI chip)
        {
            foreach (TabPanel panel in PanelSelector.Panels.Values)
            {
                panel.PanelForm.SetChip(chip);
            }
        }

        private void butRemove_Click(object sender, EventArgs e)
        {
            main.RemoveCurrentTab();
        }
    }
}
