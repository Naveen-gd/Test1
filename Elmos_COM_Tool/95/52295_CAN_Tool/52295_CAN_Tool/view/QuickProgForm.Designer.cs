using Device_52295_Lib;

namespace _52295_CAN_Tool
{
    partial class QuickProgForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_file_name = new System.Windows.Forms.Label();
            this.button_prog = new System.Windows.Forms.Button();
            this.statusLed = new StatusLedControl();
            this.SuspendLayout();
            // 
            // label_file_name
            // 
            this.label_file_name.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_file_name.AutoSize = true;
            this.label_file_name.Location = new System.Drawing.Point(12, 9);
            this.label_file_name.MaximumSize = new System.Drawing.Size(200, 14);
            this.label_file_name.MinimumSize = new System.Drawing.Size(200, 14);
            this.label_file_name.Name = "label_file_name";
            this.label_file_name.Size = new System.Drawing.Size(200, 14);
            this.label_file_name.TabIndex = 29;
            this.label_file_name.Text = "n/a";
            // 
            // button_prog
            // 
            this.button_prog.Enabled = false;
            this.button_prog.Location = new System.Drawing.Point(12, 43);
            this.button_prog.Name = "button_prog";
            this.button_prog.Size = new System.Drawing.Size(66, 23);
            this.button_prog.TabIndex = 28;
            this.button_prog.Text = "PROG";
            this.button_prog.UseVisualStyleBackColor = true;
            this.button_prog.Click += new System.EventHandler(this.button_prog_Click);
            // 
            // statusLed
            // 
            this.statusLed.AutoSize = true;
            this.statusLed.DisabledCheckedColor = System.Drawing.Color.Green;
            this.statusLed.DisabledIndeterminateColor = System.Drawing.Color.Gray;
            this.statusLed.DisabledUncheckedColor = System.Drawing.Color.Red;
            this.statusLed.Enabled = false;
            this.statusLed.Location = new System.Drawing.Point(101, 48);
            this.statusLed.Name = "statusLed";
            this.statusLed.Size = new System.Drawing.Size(15, 14);
            this.statusLed.TabIndex = 30;
            this.statusLed.ThreeState = true;
            this.statusLed.UseVisualStyleBackColor = true;
            // 
            // QuickProgForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 98);
            this.Controls.Add(this.statusLed);
            this.Controls.Add(this.label_file_name);
            this.Controls.Add(this.button_prog);
            this.Name = "QuickProgForm";
            this.Text = "QuickProgForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_file_name;
        private System.Windows.Forms.Button button_prog;
        private StatusLedControl statusLed;
    }
}