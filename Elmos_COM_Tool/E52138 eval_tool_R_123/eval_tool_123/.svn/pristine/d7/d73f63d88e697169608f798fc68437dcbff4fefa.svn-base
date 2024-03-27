using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ELMOS_521._38_UART_Eval.DeviceTabPanel
{
    public partial class Command : PanelForm
    {
        public Command()
        {
            InitializeComponent();
            Panel = panCommand;

            Console.SetOut(new TextBoxWriter(txtLog));
        }

        protected override void UpdateData(string propertyName, bool force = false)
        {
            if (propertyName == "deviceInfo" || force)
            {
                InvokeWrapper(() =>
                {

                });
            }
        }

        private void txtCommand_TextChanged(object sender, EventArgs e)
        {
            int cursor_pos = txtCommand.SelectionStart;
            string filtered_pre = Regex.Replace(txtCommand.Text.Substring(0, cursor_pos).ToUpper(), @"[^A-F0-9]", "");
            string filtered = filtered_pre + Regex.Replace(txtCommand.Text.Substring(cursor_pos).ToUpper(), @"[^A-F0-9]", "");
            cursor_pos = filtered_pre.Length;

            for (int i = 2; i < filtered.Length; i += 3)
            {
                filtered = filtered.Insert(i, " ");
                if (cursor_pos >= i) cursor_pos++;
            }

            txtCommand.TextChanged -= txtCommand_TextChanged;
            txtCommand.Text = filtered;
            txtCommand.TextChanged += txtCommand_TextChanged;

            txtCommand.SelectionStart = Math.Min(cursor_pos, txtCommand.TextLength);
        }
        
        private void txtCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (txtCommand.SelectionStart > 0)
                {
                    if (txtCommand.Text[txtCommand.SelectionStart - 1] == ' ')
                    {
                        txtCommand.SelectionStart -= 1;
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (txtCommand.SelectionStart < txtCommand.TextLength)
                {
                    if (txtCommand.Text[txtCommand.SelectionStart] == ' ')
                    {
                        txtCommand.SelectionStart += 1;
                    }
                }
            }
        }

        private void butWrite_Click(object sender, EventArgs e)
        {
            string text = Regex.Replace(txtCommand.Text.ToUpper(), @"[^A-F0-9]", "");
            byte[] bytes = new byte[text.Length / 2];

            for (int i = 0; i < bytes.Length; i++)
            {
                // Convert string character into hex value
                char chr = text[i * 2];
                bytes[i] = (byte) ((chr - (chr <= 0x39 ? 0x30 : 0x37)) << 4);
                chr = text[i * 2 + 1];
                bytes[i] += (byte)(chr - (chr <= 0x39 ? 0x30 : 0x37));
            }
            chip?.SendRaw(bytes);
        }

        private void butRead_Click(object sender, EventArgs e)
        {
            chip?.ReadBytes(Convert.ToInt32(numReadBytes.Value));
        }

        private void chkLogAll_CheckedChanged(object sender, EventArgs e)
        {
            chip?.SetDiagnoseMode(chkLogAll.Checked, true);
        }
    }
    
    public class TextBoxWriter : TextWriter
    {
        private TextBox textbox;
        public TextBoxWriter(TextBox textbox)
        {
            this.textbox = textbox;
        }

        public override void Write(string value)
        {
            if (textbox.IsHandleCreated)
            {
                if (textbox.InvokeRequired)
                {
                    textbox.BeginInvoke(new Action(() => textbox.AppendText(value)));
                }
                else
                {
                    textbox.AppendText(value);
                }
            }
        }

        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }
    }
}
