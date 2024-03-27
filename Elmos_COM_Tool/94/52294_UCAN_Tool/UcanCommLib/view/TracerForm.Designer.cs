namespace UcanCommLib
{
    partial class TracerForm
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
            this._tracerControl = new TracerControl();
            this.SuspendLayout();
            // 
            // _tracerControl
            // 
            this._tracerControl.Location = new System.Drawing.Point(12, 12);
            this._tracerControl.Name = "_tracerControl";
            this._tracerControl.Size = new System.Drawing.Size(1173, 511);
            this._tracerControl.TabIndex = 0;
            // 
            // TracerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1219, 533);
            this.Controls.Add(this._tracerControl);
            this.Name = "TracerForm";
            this.Text = "Tracer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TracerForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private TracerControl _tracerControl;

    }
}