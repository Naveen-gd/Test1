using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELMOS_521._38_UART_Eval.DeviceTabPanel
{

    public class PanelForm : Form
    {
        protected E52138ChipAPI chip;

        public PanelForm() { } // throw new Exception("This constructor is used only for c# Designer");

        private Panel panel;
        public virtual Panel Panel { get { return panel; } protected set { panel = value; } }

        public new void Close()
        {
            base.Close();
        }

        protected void InvokeWrapper(Action f)
        {
            if (Panel == null || Panel.IsDisposed)
                return;

            if (Panel.InvokeRequired)
            {
                if (!Panel.IsHandleCreated)
                    return;

                try
                {
                    Panel?.Invoke((Action)delegate { InvokeWrapper(f); });
                }
                catch (System.ObjectDisposedException e)
                {
                    Console.WriteLine("Object disposed: " + e.Message);
                }
                catch (System.ComponentModel.InvalidAsynchronousStateException e)
                {
                    Console.WriteLine("Invalid Thread: " + e.Message);
                }
            }
            else
            {
                f();
            }
        }

        private void DataChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateData(e.PropertyName, force: e.PropertyName == "force");
        }

        public void SetChip(E52138ChipAPI chip)
        {
            if (this.chip != null) this.chip.PropertyChanged -= DataChanged;
            this.chip = chip;
            this.chip.PropertyChanged += DataChanged;
            NewChip(chip);
            UpdateData(force: true);
        }

        protected virtual void UpdateData(string propertyName = "", bool force = false) { }
        protected virtual void NewChip(E52138ChipAPI chip) { }

    }
    public struct TabPanel
    {
        public PanelForm PanelForm;
        public Label Selector;
    }

    public class PanelSelector<KeyType> //where KeyType : IComparable<int>, IEquatable<int>
    {

        private KeyType Active;
        public Dictionary<KeyType, TabPanel> Panels { get; private set; }
        private Panel Frame;
        private List<EventHandler> activateCallbacks;

        private readonly System.Drawing.Color BUTTON_ACTIVE_COLOR = System.Drawing.SystemColors.GradientInactiveCaption;
        private readonly System.Drawing.Color BUTTON_INACTIVE_COLOR = System.Drawing.SystemColors.ControlLight;

        public PanelSelector(Dictionary<KeyType, TabPanel> panels, Panel frame)
        {
            Panels = panels;
            Frame = frame;

            // add event handler for buttons
            activateCallbacks = new List<EventHandler>();
            foreach (KeyValuePair<KeyType, TabPanel> panel in Panels)
            {
                activateCallbacks.Add((object sender, EventArgs e) => { Activate(panel.Key); });
                panel.Value.Selector.Click += activateCallbacks.Last();
                Frame.Controls.Add(panel.Value.PanelForm.Panel);
            }

            Activate(panels.First().Key, true);
        }

        public void Close()
        {
            foreach (TabPanel panel in Panels.Values)
            {
                foreach (var handler in activateCallbacks)
                {
                    panel.Selector.Click -= handler;
                }
            }
            activateCallbacks.Clear();
            Panels.Clear();
        }

        public void Activate(KeyType key, bool force = false)
        {
            if (!Active.Equals(key) || force)
            {
                Active = key;

                // enable selected panel
                foreach (KeyValuePair<KeyType, TabPanel> panel in Panels)
                {
                    if (panel.Key.Equals(key))
                    {
                        panel.Value.Selector.BackColor = BUTTON_ACTIVE_COLOR;
                        panel.Value.PanelForm.Panel.Visible = true;
                    }
                    else
                    {
                        panel.Value.Selector.BackColor = BUTTON_INACTIVE_COLOR;
                        panel.Value.PanelForm.Panel.Visible = false;
                    }
                }
            }
        }
    }
}
