namespace _52295_CAN_Tool
{
	partial class CommSettingsForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxCanSpeed = new System.Windows.Forms.ComboBox();
            this.radioButton_vector = new System.Windows.Forms.RadioButton();
            this.radioButton_peak = new System.Windows.Forms.RadioButton();
            this.openFileDialog_animation = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox_comm_secure = new System.Windows.Forms.GroupBox();
            this.textBoxCommParameters_secure_s = new System.Windows.Forms.TextBox();
            this.textBoxCommParameters_secure_m = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox_adapter = new System.Windows.Forms.GroupBox();
            this.groupBox_comm_types = new System.Windows.Forms.GroupBox();
            this.groupBox_custom_config = new System.Windows.Forms.GroupBox();
            this.buttonCustomerSave = new System.Windows.Forms.Button();
            this.radioButtonDefaultConfig = new System.Windows.Forms.RadioButton();
            this.radioButtonCustomConfig = new System.Windows.Forms.RadioButton();
            this.buttonCustomerLoad = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_apply = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.numericUpDownTypeM_W = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownTypeM_W3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownTypeM_R = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownTypeS_R = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.groupBox_comm_secure.SuspendLayout();
            this.groupBox_adapter.SuspendLayout();
            this.groupBox_comm_types.SuspendLayout();
            this.groupBox_custom_config.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTypeM_W)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTypeM_W3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTypeM_R)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTypeS_R)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxCanSpeed);
            this.groupBox1.Location = new System.Drawing.Point(5, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(245, 51);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bitrate";
            // 
            // comboBoxCanSpeed
            // 
            this.comboBoxCanSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCanSpeed.FormattingEnabled = true;
            this.comboBoxCanSpeed.Location = new System.Drawing.Point(4, 19);
            this.comboBoxCanSpeed.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxCanSpeed.Name = "comboBox_can_speed";
            this.comboBoxCanSpeed.Size = new System.Drawing.Size(233, 21);
            this.comboBoxCanSpeed.TabIndex = 2;
            // 
            // radioButton_vector
            // 
            this.radioButton_vector.AutoSize = true;
            this.radioButton_vector.Location = new System.Drawing.Point(5, 39);
            this.radioButton_vector.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton_vector.Name = "radioButton_vector";
            this.radioButton_vector.Size = new System.Drawing.Size(56, 17);
            this.radioButton_vector.TabIndex = 4;
            this.radioButton_vector.TabStop = true;
            this.radioButton_vector.Text = "Vector";
            this.radioButton_vector.UseVisualStyleBackColor = true;
            // 
            // radioButton_peak
            // 
            this.radioButton_peak.AutoSize = true;
            this.radioButton_peak.Location = new System.Drawing.Point(5, 18);
            this.radioButton_peak.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton_peak.Name = "radioButton_peak";
            this.radioButton_peak.Size = new System.Drawing.Size(50, 17);
            this.radioButton_peak.TabIndex = 3;
            this.radioButton_peak.TabStop = true;
            this.radioButton_peak.Text = "Peak";
            this.radioButton_peak.UseVisualStyleBackColor = true;
            // 
            // openFileDialog_animation
            // 
            this.openFileDialog_animation.DefaultExt = "txt";
            this.openFileDialog_animation.FileName = "animation.txt";
            this.openFileDialog_animation.Filter = "txt files (*.txt)|*.txt";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox_comm_secure
            // 
            this.groupBox_comm_secure.Controls.Add(this.textBoxCommParameters_secure_s);
            this.groupBox_comm_secure.Controls.Add(this.textBoxCommParameters_secure_m);
            this.groupBox_comm_secure.Controls.Add(this.label3);
            this.groupBox_comm_secure.Controls.Add(this.label2);
            this.groupBox_comm_secure.Location = new System.Drawing.Point(9, 73);
            this.groupBox_comm_secure.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_comm_secure.Name = "groupBox_comm_secure";
            this.groupBox_comm_secure.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_comm_secure.Size = new System.Drawing.Size(241, 50);
            this.groupBox_comm_secure.TabIndex = 28;
            this.groupBox_comm_secure.TabStop = false;
            this.groupBox_comm_secure.Text = "Comm Secure Parameters";
            // 
            // textBoxCommParameters_secure_s
            // 
            this.textBoxCommParameters_secure_s.Location = new System.Drawing.Point(141, 19);
            this.textBoxCommParameters_secure_s.MaxLength = 4;
            this.textBoxCommParameters_secure_s.Name = "textBoxCommParameters_secure_s";
            this.textBoxCommParameters_secure_s.Size = new System.Drawing.Size(31, 20);
            this.textBoxCommParameters_secure_s.TabIndex = 31;
            this.textBoxCommParameters_secure_s.Text = "0x00";
            // 
            // textBoxCommParameters_secure_m
            // 
            this.textBoxCommParameters_secure_m.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxCommParameters_secure_m.Location = new System.Drawing.Point(54, 19);
            this.textBoxCommParameters_secure_m.MaxLength = 4;
            this.textBoxCommParameters_secure_m.Name = "textBoxCommParameters_secure_m";
            this.textBoxCommParameters_secure_m.Size = new System.Drawing.Size(31, 20);
            this.textBoxCommParameters_secure_m.TabIndex = 30;
            this.textBoxCommParameters_secure_m.Text = "0x00";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(104, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "S:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "M:";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 13);
            this.label9.TabIndex = 39;
            this.label9.Text = "M_W3:";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(104, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 37;
            this.label7.Text = "S_R:";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "M_R:";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "M_W:";
            // 
            // groupBox_adapter
            // 
            this.groupBox_adapter.Controls.Add(this.radioButton_peak);
            this.groupBox_adapter.Controls.Add(this.radioButton_vector);
            this.groupBox_adapter.Location = new System.Drawing.Point(191, 12);
            this.groupBox_adapter.Name = "groupBox_adapter";
            this.groupBox_adapter.Size = new System.Drawing.Size(85, 69);
            this.groupBox_adapter.TabIndex = 32;
            this.groupBox_adapter.TabStop = false;
            this.groupBox_adapter.Text = "Adapter";
            // 
            // groupBox_comm_types
            // 
            this.groupBox_comm_types.Controls.Add(this.numericUpDownTypeS_R);
            this.groupBox_comm_types.Controls.Add(this.numericUpDownTypeM_R);
            this.groupBox_comm_types.Controls.Add(this.numericUpDownTypeM_W3);
            this.groupBox_comm_types.Controls.Add(this.numericUpDownTypeM_W);
            this.groupBox_comm_types.Controls.Add(this.label5);
            this.groupBox_comm_types.Controls.Add(this.label9);
            this.groupBox_comm_types.Controls.Add(this.label6);
            this.groupBox_comm_types.Controls.Add(this.label7);
            this.groupBox_comm_types.Location = new System.Drawing.Point(9, 128);
            this.groupBox_comm_types.Name = "groupBox_comm_types";
            this.groupBox_comm_types.Size = new System.Drawing.Size(241, 107);
            this.groupBox_comm_types.TabIndex = 33;
            this.groupBox_comm_types.TabStop = false;
            this.groupBox_comm_types.Text = "Comm Types";
            // 
            // groupBox_custom_config
            // 
            this.groupBox_custom_config.Controls.Add(this.groupBox1);
            this.groupBox_custom_config.Controls.Add(this.groupBox_comm_secure);
            this.groupBox_custom_config.Controls.Add(this.groupBox_comm_types);
            this.groupBox_custom_config.Location = new System.Drawing.Point(12, 87);
            this.groupBox_custom_config.Name = "groupBox_custom_config";
            this.groupBox_custom_config.Size = new System.Drawing.Size(264, 242);
            this.groupBox_custom_config.TabIndex = 34;
            this.groupBox_custom_config.TabStop = false;
            // 
            // buttonCustomerSave
            // 
            this.buttonCustomerSave.Location = new System.Drawing.Point(10, 19);
            this.buttonCustomerSave.Name = "buttonCustomerSave";
            this.buttonCustomerSave.Size = new System.Drawing.Size(100, 23);
            this.buttonCustomerSave.TabIndex = 34;
            this.buttonCustomerSave.Text = "SAVE";
            this.buttonCustomerSave.UseVisualStyleBackColor = true;
            this.buttonCustomerSave.Click += new System.EventHandler(this.button_save_Click);
            // 
            // radioButtonDefaultConfig
            // 
            this.radioButtonDefaultConfig.AutoSize = true;
            this.radioButtonDefaultConfig.Location = new System.Drawing.Point(12, 30);
            this.radioButtonDefaultConfig.Name = "radioButtonDefaultConfig";
            this.radioButtonDefaultConfig.Size = new System.Drawing.Size(144, 17);
            this.radioButtonDefaultConfig.TabIndex = 35;
            this.radioButtonDefaultConfig.TabStop = true;
            this.radioButtonDefaultConfig.Text = "Default Config (PROG=1)";
            this.radioButtonDefaultConfig.UseVisualStyleBackColor = true;
            this.radioButtonDefaultConfig.CheckedChanged += new System.EventHandler(this.radioButtonDefaultConfig_CheckedChanged);
            // 
            // radioButtonCustomConfig
            // 
            this.radioButtonCustomConfig.AutoSize = true;
            this.radioButtonCustomConfig.Location = new System.Drawing.Point(12, 66);
            this.radioButtonCustomConfig.Name = "radioButtonCustomConfig";
            this.radioButtonCustomConfig.Size = new System.Drawing.Size(96, 17);
            this.radioButtonCustomConfig.TabIndex = 36;
            this.radioButtonCustomConfig.TabStop = true;
            this.radioButtonCustomConfig.Text = "Custom Config:";
            this.radioButtonCustomConfig.UseVisualStyleBackColor = true;
            this.radioButtonCustomConfig.CheckedChanged += new System.EventHandler(this.radioButtonCustomConfig_CheckedChanged);
            // 
            // buttonCustomerLoad
            // 
            this.buttonCustomerLoad.Location = new System.Drawing.Point(150, 19);
            this.buttonCustomerLoad.Name = "buttonCustomerLoad";
            this.buttonCustomerLoad.Size = new System.Drawing.Size(100, 23);
            this.buttonCustomerLoad.TabIndex = 37;
            this.buttonCustomerLoad.Text = "LOAD";
            this.buttonCustomerLoad.UseVisualStyleBackColor = true;
            this.buttonCustomerLoad.Click += new System.EventHandler(this.button_load_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonCustomerSave);
            this.groupBox2.Controls.Add(this.buttonCustomerLoad);
            this.groupBox2.Location = new System.Drawing.Point(12, 335);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(264, 54);
            this.groupBox2.TabIndex = 38;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Customer Default";
            // 
            // button_apply
            // 
            this.button_apply.Location = new System.Drawing.Point(22, 411);
            this.button_apply.Name = "button_apply";
            this.button_apply.Size = new System.Drawing.Size(100, 23);
            this.button_apply.TabIndex = 34;
            this.button_apply.Text = "APPLY";
            this.button_apply.UseVisualStyleBackColor = true;
            this.button_apply.Click += new System.EventHandler(this.button_apply_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(162, 411);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(100, 23);
            this.button_cancel.TabIndex = 37;
            this.button_cancel.Text = "CANCEL";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // numericUpDownTypeM_W
            // 
            this.numericUpDownTypeM_W.Location = new System.Drawing.Point(53, 25);
            this.numericUpDownTypeM_W.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numericUpDownTypeM_W.Name = "numericUpDownTypeM_W";
            this.numericUpDownTypeM_W.Size = new System.Drawing.Size(31, 20);
            this.numericUpDownTypeM_W.TabIndex = 41;
            // 
            // numericUpDownTypeM_W3
            // 
            this.numericUpDownTypeM_W3.Location = new System.Drawing.Point(53, 51);
            this.numericUpDownTypeM_W3.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numericUpDownTypeM_W3.Name = "numericUpDownTypeM_W3";
            this.numericUpDownTypeM_W3.Size = new System.Drawing.Size(31, 20);
            this.numericUpDownTypeM_W3.TabIndex = 42;
            // 
            // numericUpDownTypeM_R
            // 
            this.numericUpDownTypeM_R.Location = new System.Drawing.Point(53, 77);
            this.numericUpDownTypeM_R.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numericUpDownTypeM_R.Name = "numericUpDownTypeM_R";
            this.numericUpDownTypeM_R.Size = new System.Drawing.Size(31, 20);
            this.numericUpDownTypeM_R.TabIndex = 43;
            // 
            // numericUpDownTypeS_R
            // 
            this.numericUpDownTypeS_R.Location = new System.Drawing.Point(141, 77);
            this.numericUpDownTypeS_R.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numericUpDownTypeS_R.Name = "numericUpDownTypeS_R";
            this.numericUpDownTypeS_R.Size = new System.Drawing.Size(31, 20);
            this.numericUpDownTypeS_R.TabIndex = 44;
            // 
            // CommSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 458);
            this.Controls.Add(this.button_apply);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.radioButtonCustomConfig);
            this.Controls.Add(this.radioButtonDefaultConfig);
            this.Controls.Add(this.groupBox_custom_config);
            this.Controls.Add(this.groupBox_adapter);
            this.Name = "CommSettingsForm";
            this.Text = "Communication Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox_comm_secure.ResumeLayout(false);
            this.groupBox_comm_secure.PerformLayout();
            this.groupBox_adapter.ResumeLayout(false);
            this.groupBox_adapter.PerformLayout();
            this.groupBox_comm_types.ResumeLayout(false);
            this.groupBox_comm_types.PerformLayout();
            this.groupBox_custom_config.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTypeM_W)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTypeM_W3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTypeM_R)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTypeS_R)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxCanSpeed;
        private System.Windows.Forms.OpenFileDialog openFileDialog_animation;
                private System.Windows.Forms.OpenFileDialog openFileDialog1;
                private System.Windows.Forms.GroupBox groupBox_comm_secure;
                private System.Windows.Forms.Label label3;
                private System.Windows.Forms.Label label2;
                private System.Windows.Forms.Label label9;
                private System.Windows.Forms.Label label7;
                private System.Windows.Forms.Label label6;
                private System.Windows.Forms.Label label5;
                private System.Windows.Forms.TextBox textBoxCommParameters_secure_s;
                private System.Windows.Forms.TextBox textBoxCommParameters_secure_m;
                private System.Windows.Forms.RadioButton radioButton_vector;
                private System.Windows.Forms.RadioButton radioButton_peak;
                private System.Windows.Forms.GroupBox groupBox_adapter;
                private System.Windows.Forms.GroupBox groupBox_comm_types;
                private System.Windows.Forms.GroupBox groupBox_custom_config;
                private System.Windows.Forms.Button buttonCustomerSave;
                private System.Windows.Forms.RadioButton radioButtonDefaultConfig;
                private System.Windows.Forms.RadioButton radioButtonCustomConfig;
                private System.Windows.Forms.Button buttonCustomerLoad;
                private System.Windows.Forms.GroupBox groupBox2;
                private System.Windows.Forms.Button button_apply;
                private System.Windows.Forms.Button button_cancel;
                private System.Windows.Forms.NumericUpDown numericUpDownTypeS_R;
                private System.Windows.Forms.NumericUpDown numericUpDownTypeM_R;
                private System.Windows.Forms.NumericUpDown numericUpDownTypeM_W3;
                private System.Windows.Forms.NumericUpDown numericUpDownTypeM_W;
	}
}

