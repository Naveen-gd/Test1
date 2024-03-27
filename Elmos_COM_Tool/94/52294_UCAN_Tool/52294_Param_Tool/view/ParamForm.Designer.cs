﻿namespace _52294_Param_Tool
{
    partial class ParamForm
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParamForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog_animation = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBoxEE = new System.Windows.Forms.GroupBox();
            this.buttonLoadEE = new System.Windows.Forms.Button();
            this.buttonSaveEE = new System.Windows.Forms.Button();
            this.buttonEditEE = new System.Windows.Forms.Button();
            this.label_version = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBoxEE.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(10, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(110, 39);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.WaitOnLoad = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBoxEE
            // 
            this.groupBoxEE.Controls.Add(this.buttonLoadEE);
            this.groupBoxEE.Controls.Add(this.buttonSaveEE);
            this.groupBoxEE.Controls.Add(this.buttonEditEE);
            this.groupBoxEE.Location = new System.Drawing.Point(126, 9);
            this.groupBoxEE.Name = "groupBoxEE";
            this.groupBoxEE.Size = new System.Drawing.Size(116, 112);
            this.groupBoxEE.TabIndex = 31;
            this.groupBoxEE.TabStop = false;
            this.groupBoxEE.Text = "Parameters";
            // 
            // buttonLoadEE
            // 
            this.buttonLoadEE.Location = new System.Drawing.Point(6, 77);
            this.buttonLoadEE.Name = "buttonLoadEE";
            this.buttonLoadEE.Size = new System.Drawing.Size(100, 23);
            this.buttonLoadEE.TabIndex = 30;
            this.buttonLoadEE.Text = "LOAD FILE";
            this.buttonLoadEE.UseVisualStyleBackColor = true;
            this.buttonLoadEE.Click += new System.EventHandler(this.buttonLoadEE_Click);
            // 
            // buttonSaveEE
            // 
            this.buttonSaveEE.Location = new System.Drawing.Point(6, 48);
            this.buttonSaveEE.Name = "buttonSaveEE";
            this.buttonSaveEE.Size = new System.Drawing.Size(100, 23);
            this.buttonSaveEE.TabIndex = 29;
            this.buttonSaveEE.Text = "SAVE FILE";
            this.buttonSaveEE.UseVisualStyleBackColor = true;
            this.buttonSaveEE.Click += new System.EventHandler(this.buttonSaveEE_Click);
            // 
            // buttonEditEE
            // 
            this.buttonEditEE.Location = new System.Drawing.Point(6, 19);
            this.buttonEditEE.Name = "buttonEditEE";
            this.buttonEditEE.Size = new System.Drawing.Size(100, 23);
            this.buttonEditEE.TabIndex = 28;
            this.buttonEditEE.Text = "EDIT";
            this.buttonEditEE.UseVisualStyleBackColor = true;
            this.buttonEditEE.Click += new System.EventHandler(this.buttonEditEE_Click);
            // 
            // label_version
            // 
            this.label_version.AutoSize = true;
            this.label_version.Cursor = System.Windows.Forms.Cursors.Help;
            this.label_version.Location = new System.Drawing.Point(7, 62);
            this.label_version.MaximumSize = new System.Drawing.Size(112, 16);
            this.label_version.MinimumSize = new System.Drawing.Size(100, 13);
            this.label_version.Name = "label_version";
            this.label_version.Size = new System.Drawing.Size(100, 13);
            this.label_version.TabIndex = 32;
            this.label_version.Text = "Version 19";
            // 
            // ParamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 137);
            this.Controls.Add(this.label_version);
            this.Controls.Add(this.groupBoxEE);
            this.Controls.Add(this.pictureBox1);
            this.Name = "ParamForm";
            this.Text = "ELMOS 522.94 Parameters Tool";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBoxEE.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog_animation;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBoxEE;
        private System.Windows.Forms.Button buttonLoadEE;
        private System.Windows.Forms.Button buttonSaveEE;
        private System.Windows.Forms.Button buttonEditEE;
        private System.Windows.Forms.Label label_version;
	}
}

