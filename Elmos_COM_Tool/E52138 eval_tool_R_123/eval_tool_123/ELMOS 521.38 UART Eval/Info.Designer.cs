namespace ELMOS_521._38_UART_Eval
{
    partial class FormInfo
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
            this.ButClose = new System.Windows.Forms.Button();
            this.PicElmosLogo = new System.Windows.Forms.PictureBox();
            this.LabProgram = new System.Windows.Forms.Label();
            this.LabVersion = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PicElmosLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // ButClose
            // 
            this.ButClose.Location = new System.Drawing.Point(291, 168);
            this.ButClose.Name = "ButClose";
            this.ButClose.Size = new System.Drawing.Size(75, 23);
            this.ButClose.TabIndex = 0;
            this.ButClose.Text = "CLOSE";
            this.ButClose.UseVisualStyleBackColor = true;
            this.ButClose.Click += new System.EventHandler(this.ButClose_Click);
            // 
            // PicElmosLogo
            // 
            this.PicElmosLogo.Image = global::ELMOS_521._38_UART_Eval.Properties.Resources.Elmos_Logo;
            this.PicElmosLogo.Location = new System.Drawing.Point(12, 12);
            this.PicElmosLogo.Name = "PicElmosLogo";
            this.PicElmosLogo.Size = new System.Drawing.Size(278, 80);
            this.PicElmosLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicElmosLogo.TabIndex = 1;
            this.PicElmosLogo.TabStop = false;
            // 
            // LabProgram
            // 
            this.LabProgram.AutoSize = true;
            this.LabProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabProgram.Location = new System.Drawing.Point(12, 106);
            this.LabProgram.Name = "LabProgram";
            this.LabProgram.Size = new System.Drawing.Size(133, 15);
            this.LabProgram.TabIndex = 2;
            this.LabProgram.Text = "521.38 UART Eval Tool";
            // 
            // LabVersion
            // 
            this.LabVersion.AutoSize = true;
            this.LabVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabVersion.Location = new System.Drawing.Point(12, 125);
            this.LabVersion.Name = "LabVersion";
            this.LabVersion.Size = new System.Drawing.Size(68, 15);
            this.LabVersion.TabIndex = 3;
            this.LabVersion.Text = "Version 0.2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 174);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "© 2020 Elmos Semiconductor AG";
            // 
            // FormInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 200);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LabVersion);
            this.Controls.Add(this.LabProgram);
            this.Controls.Add(this.PicElmosLogo);
            this.Controls.Add(this.ButClose);
            this.MaximizeBox = false;
            this.Name = "FormInfo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Info über 521.38 UART Eval";
            ((System.ComponentModel.ISupportInitialize)(this.PicElmosLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButClose;
        private System.Windows.Forms.PictureBox PicElmosLogo;
        private System.Windows.Forms.Label LabProgram;
        private System.Windows.Forms.Label LabVersion;
        private System.Windows.Forms.Label label1;
    }
}