namespace _52295_CAN_Tool
{
	partial class MainForm
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
            this.buttonOpenClose = new System.Windows.Forms.Button();
            this.groupBox_group_auto = new System.Windows.Forms.GroupBox();
            this.checkBox_auto_write_pwm = new System.Windows.Forms.CheckBox();
            this.checkBox_auto_write_currents = new System.Windows.Forms.CheckBox();
            this.numericUpDownAutoWriteInterval = new System.Windows.Forms.NumericUpDown();
            this.labelAutoInterval = new System.Windows.Forms.Label();
            this.groupBox_auto = new System.Windows.Forms.GroupBox();
            this.checkBox_autoReadDiag = new System.Windows.Forms.CheckBox();
            this.checkBox_auto_read_full_status = new System.Windows.Forms.CheckBox();
            this.groupBoxDevices = new System.Windows.Forms.GroupBox();
            this.buttonDeviceAdd = new System.Windows.Forms.Button();
            this.tabControlDevices = new System.Windows.Forms.TabControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelCommStatus = new System.Windows.Forms.Label();
            this.label_stat_max = new System.Windows.Forms.Label();
            this.buttonCommSettings = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label_stat_mean = new System.Windows.Forms.Label();
            this.label_version = new System.Windows.Forms.Label();
            this.groupBoxAnimation = new System.Windows.Forms.GroupBox();
            this.buttonAnimationHelp = new System.Windows.Forms.Button();
            this.label_animation_file = new System.Windows.Forms.Label();
            this.buttonAnimationStartStop = new System.Windows.Forms.Button();
            this.buttonAnimationLoad = new System.Windows.Forms.Button();
            this.openFileDialog_animation = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBox_gui_interval = new System.Windows.Forms.ComboBox();
            this.groupBox_group_auto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutoWriteInterval)).BeginInit();
            this.groupBox_auto.SuspendLayout();
            this.groupBoxDevices.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxAnimation.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOpenClose
            // 
            this.buttonOpenClose.Location = new System.Drawing.Point(8, 72);
            this.buttonOpenClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonOpenClose.Name = "buttonOpenClose";
            this.buttonOpenClose.Size = new System.Drawing.Size(123, 35);
            this.buttonOpenClose.TabIndex = 1;
            this.buttonOpenClose.Text = "OPEN";
            this.buttonOpenClose.UseVisualStyleBackColor = true;
            this.buttonOpenClose.Click += new System.EventHandler(this.buttonOpenClose_Click);
            // 
            // groupBox_group_auto
            // 
            this.groupBox_group_auto.Controls.Add(this.checkBox_auto_write_pwm);
            this.groupBox_group_auto.Controls.Add(this.checkBox_auto_write_currents);
            this.groupBox_group_auto.Controls.Add(this.numericUpDownAutoWriteInterval);
            this.groupBox_group_auto.Controls.Add(this.labelAutoInterval);
            this.groupBox_group_auto.Enabled = false;
            this.groupBox_group_auto.Location = new System.Drawing.Point(670, 8);
            this.groupBox_group_auto.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_group_auto.Name = "groupBox_group_auto";
            this.groupBox_group_auto.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_group_auto.Size = new System.Drawing.Size(292, 163);
            this.groupBox_group_auto.TabIndex = 29;
            this.groupBox_group_auto.TabStop = false;
            this.groupBox_group_auto.Text = "Automatic Group Write";
            // 
            // checkBox_auto_write_pwm
            // 
            this.checkBox_auto_write_pwm.AutoSize = true;
            this.checkBox_auto_write_pwm.Location = new System.Drawing.Point(8, 34);
            this.checkBox_auto_write_pwm.Name = "checkBox_auto_write_pwm";
            this.checkBox_auto_write_pwm.Size = new System.Drawing.Size(155, 24);
            this.checkBox_auto_write_pwm.TabIndex = 26;
            this.checkBox_auto_write_pwm.Text = "Write PWM every";
            this.checkBox_auto_write_pwm.UseVisualStyleBackColor = true;
            this.checkBox_auto_write_pwm.CheckedChanged += new System.EventHandler(this.checkBox_auto_write_pwm_CheckedChanged);
            // 
            // checkBox_auto_write_currents
            // 
            this.checkBox_auto_write_currents.AutoSize = true;
            this.checkBox_auto_write_currents.Location = new System.Drawing.Point(8, 66);
            this.checkBox_auto_write_currents.Name = "checkBox_auto_write_currents";
            this.checkBox_auto_write_currents.Size = new System.Drawing.Size(178, 24);
            this.checkBox_auto_write_currents.TabIndex = 27;
            this.checkBox_auto_write_currents.Text = "Write Currents every";
            this.checkBox_auto_write_currents.UseVisualStyleBackColor = true;
            this.checkBox_auto_write_currents.CheckedChanged += new System.EventHandler(this.checkBox_auto_write_currents_CheckedChanged);
            // 
            // numericUpDownAutoWriteInterval
            // 
            this.numericUpDownAutoWriteInterval.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numericUpDownAutoWriteInterval.Location = new System.Drawing.Point(198, 46);
            this.numericUpDownAutoWriteInterval.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownAutoWriteInterval.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownAutoWriteInterval.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownAutoWriteInterval.Name = "numericUpDownAutoWriteInterval";
            this.numericUpDownAutoWriteInterval.Size = new System.Drawing.Size(57, 26);
            this.numericUpDownAutoWriteInterval.TabIndex = 23;
            this.numericUpDownAutoWriteInterval.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownAutoWriteInterval.ValueChanged += new System.EventHandler(this.numericUpDownAutoWriteInterval_ValueChanged);
            // 
            // labelAutoInterval
            // 
            this.labelAutoInterval.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelAutoInterval.AutoSize = true;
            this.labelAutoInterval.Location = new System.Drawing.Point(260, 51);
            this.labelAutoInterval.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAutoInterval.Name = "labelAutoInterval";
            this.labelAutoInterval.Size = new System.Drawing.Size(30, 20);
            this.labelAutoInterval.TabIndex = 24;
            this.labelAutoInterval.Text = "ms";
            // 
            // groupBox_auto
            // 
            this.groupBox_auto.Controls.Add(this.checkBox_autoReadDiag);
            this.groupBox_auto.Controls.Add(this.checkBox_auto_read_full_status);
            this.groupBox_auto.Enabled = false;
            this.groupBox_auto.Location = new System.Drawing.Point(972, 6);
            this.groupBox_auto.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_auto.Name = "groupBox_auto";
            this.groupBox_auto.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_auto.Size = new System.Drawing.Size(159, 165);
            this.groupBox_auto.TabIndex = 28;
            this.groupBox_auto.TabStop = false;
            this.groupBox_auto.Text = "Automatic Status";
            // 
            // checkBox_autoReadDiag
            // 
            this.checkBox_autoReadDiag.AutoSize = true;
            this.checkBox_autoReadDiag.Location = new System.Drawing.Point(8, 63);
            this.checkBox_autoReadDiag.Name = "checkBox_autoReadDiag";
            this.checkBox_autoReadDiag.Size = new System.Drawing.Size(106, 24);
            this.checkBox_autoReadDiag.TabIndex = 26;
            this.checkBox_autoReadDiag.Text = "Diag Area";
            this.checkBox_autoReadDiag.UseVisualStyleBackColor = true;
            this.checkBox_autoReadDiag.CheckedChanged += new System.EventHandler(this.checkBox_autoReadDiag_CheckedChanged);
            // 
            // checkBox_auto_read_full_status
            // 
            this.checkBox_auto_read_full_status.AutoSize = true;
            this.checkBox_auto_read_full_status.Location = new System.Drawing.Point(8, 35);
            this.checkBox_auto_read_full_status.Name = "checkBox_auto_read_full_status";
            this.checkBox_auto_read_full_status.Size = new System.Drawing.Size(117, 24);
            this.checkBox_auto_read_full_status.TabIndex = 25;
            this.checkBox_auto_read_full_status.Text = "Full Update";
            this.checkBox_auto_read_full_status.UseVisualStyleBackColor = true;
            this.checkBox_auto_read_full_status.CheckedChanged += new System.EventHandler(this.checkBox_auto_read_full_status_CheckedChanged);
            // 
            // groupBoxDevices
            // 
            this.groupBoxDevices.Controls.Add(this.buttonDeviceAdd);
            this.groupBoxDevices.Controls.Add(this.tabControlDevices);
            this.groupBoxDevices.Location = new System.Drawing.Point(14, 180);
            this.groupBoxDevices.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxDevices.Name = "groupBoxDevices";
            this.groupBoxDevices.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxDevices.Size = new System.Drawing.Size(1497, 1005);
            this.groupBoxDevices.TabIndex = 29;
            this.groupBoxDevices.TabStop = false;
            this.groupBoxDevices.Text = "Devices";
            // 
            // buttonDeviceAdd
            // 
            this.buttonDeviceAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDeviceAdd.Location = new System.Drawing.Point(9, 29);
            this.buttonDeviceAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonDeviceAdd.Name = "buttonDeviceAdd";
            this.buttonDeviceAdd.Size = new System.Drawing.Size(30, 32);
            this.buttonDeviceAdd.TabIndex = 1;
            this.buttonDeviceAdd.Text = "+";
            this.buttonDeviceAdd.UseVisualStyleBackColor = true;
            this.buttonDeviceAdd.Click += new System.EventHandler(this.buttonDeviceAdd_Click);
            // 
            // tabControlDevices
            // 
            this.tabControlDevices.Location = new System.Drawing.Point(48, 29);
            this.tabControlDevices.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControlDevices.Name = "tabControlDevices";
            this.tabControlDevices.SelectedIndex = 0;
            this.tabControlDevices.Size = new System.Drawing.Size(1438, 962);
            this.tabControlDevices.TabIndex = 0;
            this.tabControlDevices.SelectedIndexChanged += new System.EventHandler(this.tabControlDevices_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.labelCommStatus);
            this.groupBox1.Controls.Add(this.label_stat_max);
            this.groupBox1.Controls.Add(this.buttonCommSettings);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.buttonOpenClose);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label_stat_mean);
            this.groupBox1.Location = new System.Drawing.Point(188, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 157);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CAN Connection";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(237, 109);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.MaximumSize = new System.Drawing.Size(202, 22);
            this.label4.MinimumSize = new System.Drawing.Size(22, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 22);
            this.label4.TabIndex = 33;
            this.label4.Text = "ms";
            // 
            // labelCommStatus
            // 
            this.labelCommStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelCommStatus.AutoSize = true;
            this.labelCommStatus.Location = new System.Drawing.Point(140, 35);
            this.labelCommStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCommStatus.MaximumSize = new System.Drawing.Size(202, 22);
            this.labelCommStatus.MinimumSize = new System.Drawing.Size(22, 22);
            this.labelCommStatus.Name = "labelCommStatus";
            this.labelCommStatus.Size = new System.Drawing.Size(31, 22);
            this.labelCommStatus.TabIndex = 47;
            this.labelCommStatus.Text = "n/a";
            // 
            // label_stat_max
            // 
            this.label_stat_max.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_stat_max.AutoSize = true;
            this.label_stat_max.Location = new System.Drawing.Point(196, 109);
            this.label_stat_max.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_stat_max.MaximumSize = new System.Drawing.Size(202, 22);
            this.label_stat_max.MinimumSize = new System.Drawing.Size(22, 22);
            this.label_stat_max.Name = "label_stat_max";
            this.label_stat_max.Size = new System.Drawing.Size(31, 22);
            this.label_stat_max.TabIndex = 32;
            this.label_stat_max.Text = "n/a";
            // 
            // buttonCommSettings
            // 
            this.buttonCommSettings.Location = new System.Drawing.Point(8, 28);
            this.buttonCommSettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonCommSettings.Name = "buttonCommSettings";
            this.buttonCommSettings.Size = new System.Drawing.Size(123, 35);
            this.buttonCommSettings.TabIndex = 46;
            this.buttonCommSettings.Text = "SETTINGS";
            this.buttonCommSettings.UseVisualStyleBackColor = true;
            this.buttonCommSettings.Click += new System.EventHandler(this.buttonCommSettings_Click);
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(140, 109);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.MaximumSize = new System.Drawing.Size(202, 22);
            this.label11.MinimumSize = new System.Drawing.Size(22, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 22);
            this.label11.TabIndex = 31;
            this.label11.Text = "Max:";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(237, 80);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.MaximumSize = new System.Drawing.Size(202, 22);
            this.label8.MinimumSize = new System.Drawing.Size(22, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 22);
            this.label8.TabIndex = 30;
            this.label8.Text = "ms";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(140, 80);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.MaximumSize = new System.Drawing.Size(202, 22);
            this.label1.MinimumSize = new System.Drawing.Size(22, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 22);
            this.label1.TabIndex = 28;
            this.label1.Text = "Mean:";
            // 
            // label_stat_mean
            // 
            this.label_stat_mean.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_stat_mean.AutoSize = true;
            this.label_stat_mean.Location = new System.Drawing.Point(196, 80);
            this.label_stat_mean.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_stat_mean.MaximumSize = new System.Drawing.Size(202, 22);
            this.label_stat_mean.MinimumSize = new System.Drawing.Size(22, 22);
            this.label_stat_mean.Name = "label_stat_mean";
            this.label_stat_mean.Size = new System.Drawing.Size(31, 22);
            this.label_stat_mean.TabIndex = 29;
            this.label_stat_mean.Text = "n/a";
            // 
            // label_version
            // 
            this.label_version.AutoSize = true;
            this.label_version.Cursor = System.Windows.Forms.Cursors.Help;
            this.label_version.Location = new System.Drawing.Point(10, 139);
            this.label_version.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_version.MaximumSize = new System.Drawing.Size(168, 25);
            this.label_version.MinimumSize = new System.Drawing.Size(150, 20);
            this.label_version.Name = "label_version";
            this.label_version.Size = new System.Drawing.Size(150, 20);
            this.label_version.TabIndex = 31;
            this.label_version.Text = "Version 22";
            this.label_version.Click += new System.EventHandler(this.label_version_Click);
            // 
            // groupBoxAnimation
            // 
            this.groupBoxAnimation.Controls.Add(this.buttonAnimationHelp);
            this.groupBoxAnimation.Controls.Add(this.label_animation_file);
            this.groupBoxAnimation.Controls.Add(this.buttonAnimationStartStop);
            this.groupBoxAnimation.Controls.Add(this.buttonAnimationLoad);
            this.groupBoxAnimation.Enabled = false;
            this.groupBoxAnimation.Location = new System.Drawing.Point(1138, 6);
            this.groupBoxAnimation.Name = "groupBoxAnimation";
            this.groupBoxAnimation.Size = new System.Drawing.Size(372, 165);
            this.groupBoxAnimation.TabIndex = 27;
            this.groupBoxAnimation.TabStop = false;
            this.groupBoxAnimation.Text = "Animation";
            // 
            // buttonAnimationHelp
            // 
            this.buttonAnimationHelp.Location = new System.Drawing.Point(322, 35);
            this.buttonAnimationHelp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonAnimationHelp.Name = "buttonAnimationHelp";
            this.buttonAnimationHelp.Size = new System.Drawing.Size(39, 35);
            this.buttonAnimationHelp.TabIndex = 28;
            this.buttonAnimationHelp.Text = "?";
            this.buttonAnimationHelp.UseVisualStyleBackColor = true;
            this.buttonAnimationHelp.Click += new System.EventHandler(this.buttonAnimationHelp_Click);
            // 
            // label_animation_file
            // 
            this.label_animation_file.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_animation_file.AutoSize = true;
            this.label_animation_file.Location = new System.Drawing.Point(15, 105);
            this.label_animation_file.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_animation_file.MaximumSize = new System.Drawing.Size(300, 22);
            this.label_animation_file.MinimumSize = new System.Drawing.Size(300, 22);
            this.label_animation_file.Name = "label_animation_file";
            this.label_animation_file.Size = new System.Drawing.Size(300, 22);
            this.label_animation_file.TabIndex = 27;
            this.label_animation_file.Text = "n/a";
            // 
            // buttonAnimationStartStop
            // 
            this.buttonAnimationStartStop.Enabled = false;
            this.buttonAnimationStartStop.Location = new System.Drawing.Point(106, 35);
            this.buttonAnimationStartStop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonAnimationStartStop.Name = "buttonAnimationStartStop";
            this.buttonAnimationStartStop.Size = new System.Drawing.Size(90, 35);
            this.buttonAnimationStartStop.TabIndex = 4;
            this.buttonAnimationStartStop.Text = "START";
            this.buttonAnimationStartStop.UseVisualStyleBackColor = true;
            this.buttonAnimationStartStop.Click += new System.EventHandler(this.button_animation_startstop_Click);
            // 
            // buttonAnimationLoad
            // 
            this.buttonAnimationLoad.Location = new System.Drawing.Point(8, 35);
            this.buttonAnimationLoad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonAnimationLoad.Name = "buttonAnimationLoad";
            this.buttonAnimationLoad.Size = new System.Drawing.Size(90, 35);
            this.buttonAnimationLoad.TabIndex = 3;
            this.buttonAnimationLoad.Text = "LOAD";
            this.buttonAnimationLoad.UseVisualStyleBackColor = true;
            this.buttonAnimationLoad.Click += new System.EventHandler(this.button_animation_load_Click);
            // 
            // openFileDialog_animation
            // 
            this.openFileDialog_animation.DefaultExt = "txt";
            this.openFileDialog_animation.FileName = "animation.txt";
            this.openFileDialog_animation.Filter = "txt files (*.txt)|*.txt";
            this.openFileDialog_animation.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_animation_FileOk);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(108, 35);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.MaximumSize = new System.Drawing.Size(202, 22);
            this.label10.MinimumSize = new System.Drawing.Size(22, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 22);
            this.label10.TabIndex = 36;
            this.label10.Text = "ms";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboBox_gui_interval);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Location = new System.Drawing.Point(502, 14);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(159, 157);
            this.groupBox3.TabIndex = 37;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "GUI Update Rate";
            // 
            // comboBox_gui_interval
            // 
            this.comboBox_gui_interval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_gui_interval.FormattingEnabled = true;
            this.comboBox_gui_interval.Location = new System.Drawing.Point(9, 31);
            this.comboBox_gui_interval.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox_gui_interval.Name = "comboBox_gui_interval";
            this.comboBox_gui_interval.Size = new System.Drawing.Size(88, 28);
            this.comboBox_gui_interval.TabIndex = 37;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1526, 1050);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox_auto);
            this.Controls.Add(this.groupBox_group_auto);
            this.Controls.Add(this.groupBoxAnimation);
            this.Controls.Add(this.label_version);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxDevices);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "ELMOS 522.95 CAN Eval";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.groupBox_group_auto.ResumeLayout(false);
            this.groupBox_group_auto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutoWriteInterval)).EndInit();
            this.groupBox_auto.ResumeLayout(false);
            this.groupBox_auto.PerformLayout();
            this.groupBoxDevices.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxAnimation.ResumeLayout(false);
            this.groupBoxAnimation.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Button buttonOpenClose;
        private System.Windows.Forms.Label labelAutoInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownAutoWriteInterval;
        private System.Windows.Forms.GroupBox groupBoxDevices;
                private System.Windows.Forms.Button buttonDeviceAdd;
                private System.Windows.Forms.TabControl tabControlDevices;
                private System.Windows.Forms.GroupBox groupBox1;
                private System.Windows.Forms.Label label_version;
                private System.Windows.Forms.CheckBox checkBox_auto_write_pwm;
                private System.Windows.Forms.GroupBox groupBoxAnimation;
                private System.Windows.Forms.OpenFileDialog openFileDialog_animation;
                private System.Windows.Forms.Button buttonAnimationLoad;
                private System.Windows.Forms.Label label_animation_file;
                private System.Windows.Forms.Button buttonAnimationStartStop;
                private System.Windows.Forms.OpenFileDialog openFileDialog1;
                private System.Windows.Forms.CheckBox checkBox_auto_write_currents;
                private System.Windows.Forms.GroupBox groupBox_group_auto;
                private System.Windows.Forms.GroupBox groupBox_auto;
                private System.Windows.Forms.CheckBox checkBox_auto_read_full_status;
                private System.Windows.Forms.CheckBox checkBox_autoReadDiag;
                private System.Windows.Forms.Label label4;
                private System.Windows.Forms.Label label_stat_max;
                private System.Windows.Forms.Label label11;
                private System.Windows.Forms.Label label8;
                private System.Windows.Forms.Label label_stat_mean;
                private System.Windows.Forms.Label label1;
                private System.Windows.Forms.Label label10;
                private System.Windows.Forms.GroupBox groupBox3;
                private System.Windows.Forms.ComboBox comboBox_gui_interval;
                private System.Windows.Forms.Button buttonCommSettings;
                private System.Windows.Forms.Label labelCommStatus;
                private System.Windows.Forms.Button buttonAnimationHelp;
	}
}

