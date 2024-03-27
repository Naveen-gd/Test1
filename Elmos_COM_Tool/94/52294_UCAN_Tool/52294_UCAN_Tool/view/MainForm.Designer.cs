namespace _52294_UCAN_Tool
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
            this.groupBox_auto = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDownAutoWriteInterval = new System.Windows.Forms.NumericUpDown();
            this.labelAutoInterval = new System.Windows.Forms.Label();
            this.checkBoxAutoReadDiag = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoWritePwm = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoReadFullStatus = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoWriteCurrents = new System.Windows.Forms.CheckBox();
            this.groupBoxDevices = new System.Windows.Forms.GroupBox();
            this.buttonDeviceAdd = new System.Windows.Forms.Button();
            this.tabControlDevices = new System.Windows.Forms.TabControl();
            this.comboBox_commDevices = new System.Windows.Forms.ComboBox();
            this.label_version = new System.Windows.Forms.Label();
            this.groupBoxAnimation = new System.Windows.Forms.GroupBox();
            this.buttonAnimationHelp = new System.Windows.Forms.Button();
            this.label_animation_file = new System.Windows.Forms.Label();
            this.button_animation_startstop = new System.Windows.Forms.Button();
            this.button_animation_load = new System.Windows.Forms.Button();
            this.openFileDialog_animation = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox_comm_secure = new System.Windows.Forms.GroupBox();
            this.comboBox_comm_break = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox_comm_sync = new System.Windows.Forms.ComboBox();
            this.comboBox_comm_parity = new System.Windows.Forms.ComboBox();
            this.comboBox_comm_header = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox_comm_bitrate = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox_adapter = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label_stat_max = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label_stat_mean = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBox_gui_interval = new System.Windows.Forms.ComboBox();
            this.groupBox_sleep = new System.Windows.Forms.GroupBox();
            this.checkBox_wake_ack = new System.Windows.Forms.CheckBox();
            this.checkBox_wake_symbol = new System.Windows.Forms.CheckBox();
            this.button_wakeup = new System.Windows.Forms.Button();
            this.button_sleep = new System.Windows.Forms.Button();
            this.groupBox_trace = new System.Windows.Forms.GroupBox();
            this.button_tracer = new System.Windows.Forms.Button();
            this.checkBox_pg_mode = new System.Windows.Forms.CheckBox();
            this.groupBox_hidden = new System.Windows.Forms.GroupBox();
            this.groupBox_auto_imm = new System.Windows.Forms.GroupBox();
            this.checkBox_auto_imm_enables = new System.Windows.Forms.CheckBox();
            this.checkBox_auto_imm_current0 = new System.Windows.Forms.CheckBox();
            this.checkBox_auto_imm_pulse0 = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox_wakeup_length = new System.Windows.Forms.GroupBox();
            this.comboBox_wakeup_length = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox_direct_bus = new System.Windows.Forms.GroupBox();
            this.button_clear_break = new System.Windows.Forms.Button();
            this.button_set_break = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.grpBoxExpert = new System.Windows.Forms.GroupBox();
            this.btnExpert = new System.Windows.Forms.Button();
            this.txtAuthenticateExpert = new System.Windows.Forms.TextBox();
            this.groupBox_auto.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutoWriteInterval)).BeginInit();
            this.groupBoxDevices.SuspendLayout();
            this.groupBoxAnimation.SuspendLayout();
            this.groupBox_comm_secure.SuspendLayout();
            this.groupBox_adapter.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox_sleep.SuspendLayout();
            this.groupBox_trace.SuspendLayout();
            this.groupBox_hidden.SuspendLayout();
            this.groupBox_auto_imm.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox_wakeup_length.SuspendLayout();
            this.groupBox_direct_bus.SuspendLayout();
            this.grpBoxExpert.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOpenClose
            // 
            this.buttonOpenClose.Location = new System.Drawing.Point(538, 31);
            this.buttonOpenClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonOpenClose.Name = "buttonOpenClose";
            this.buttonOpenClose.Size = new System.Drawing.Size(112, 32);
            this.buttonOpenClose.TabIndex = 1;
            this.buttonOpenClose.Text = "OPEN";
            this.buttonOpenClose.UseVisualStyleBackColor = true;
            this.buttonOpenClose.Click += new System.EventHandler(this.buttonOpenClose_Click);
            // 
            // groupBox_auto
            // 
            this.groupBox_auto.Controls.Add(this.groupBox1);
            this.groupBox_auto.Controls.Add(this.checkBoxAutoReadDiag);
            this.groupBox_auto.Controls.Add(this.checkBoxAutoWritePwm);
            this.groupBox_auto.Controls.Add(this.checkBoxAutoReadFullStatus);
            this.groupBox_auto.Controls.Add(this.checkBoxAutoWriteCurrents);
            this.groupBox_auto.Enabled = false;
            this.groupBox_auto.Location = new System.Drawing.Point(858, 8);
            this.groupBox_auto.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_auto.Name = "groupBox_auto";
            this.groupBox_auto.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_auto.Size = new System.Drawing.Size(304, 157);
            this.groupBox_auto.TabIndex = 29;
            this.groupBox_auto.TabStop = false;
            this.groupBox_auto.Text = "Automatic";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.numericUpDownAutoWriteInterval);
            this.groupBox1.Controls.Add(this.labelAutoInterval);
            this.groupBox1.Location = new System.Drawing.Point(171, 28);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(116, 115);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 28);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 20);
            this.label9.TabIndex = 28;
            this.label9.Text = "every";
            // 
            // numericUpDownAutoWriteInterval
            // 
            this.numericUpDownAutoWriteInterval.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numericUpDownAutoWriteInterval.Location = new System.Drawing.Point(9, 57);
            this.numericUpDownAutoWriteInterval.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownAutoWriteInterval.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownAutoWriteInterval.Minimum = new decimal(new int[] {
            1,
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
            this.labelAutoInterval.Location = new System.Drawing.Point(70, 62);
            this.labelAutoInterval.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAutoInterval.Name = "labelAutoInterval";
            this.labelAutoInterval.Size = new System.Drawing.Size(30, 20);
            this.labelAutoInterval.TabIndex = 24;
            this.labelAutoInterval.Text = "ms";
            // 
            // checkBoxAutoReadDiag
            // 
            this.checkBoxAutoReadDiag.AutoSize = true;
            this.checkBoxAutoReadDiag.Location = new System.Drawing.Point(8, 122);
            this.checkBoxAutoReadDiag.Name = "checkBoxAutoReadDiag";
            this.checkBoxAutoReadDiag.Size = new System.Drawing.Size(149, 24);
            this.checkBoxAutoReadDiag.TabIndex = 26;
            this.checkBoxAutoReadDiag.Text = "Read Diag Area";
            this.checkBoxAutoReadDiag.UseVisualStyleBackColor = true;
            this.checkBoxAutoReadDiag.CheckedChanged += new System.EventHandler(this.checkBox_autoReadDiag_CheckedChanged);
            // 
            // checkBoxAutoWritePwm
            // 
            this.checkBoxAutoWritePwm.AutoSize = true;
            this.checkBoxAutoWritePwm.Location = new System.Drawing.Point(8, 34);
            this.checkBoxAutoWritePwm.Name = "checkBoxAutoWritePwm";
            this.checkBoxAutoWritePwm.Size = new System.Drawing.Size(114, 24);
            this.checkBoxAutoWritePwm.TabIndex = 26;
            this.checkBoxAutoWritePwm.Text = "Write PWM";
            this.checkBoxAutoWritePwm.UseVisualStyleBackColor = true;
            this.checkBoxAutoWritePwm.CheckedChanged += new System.EventHandler(this.checkBox_auto_write_pwm_CheckedChanged);
            // 
            // checkBoxAutoReadFullStatus
            // 
            this.checkBoxAutoReadFullStatus.AutoSize = true;
            this.checkBoxAutoReadFullStatus.Location = new System.Drawing.Point(8, 94);
            this.checkBoxAutoReadFullStatus.Name = "checkBoxAutoReadFullStatus";
            this.checkBoxAutoReadFullStatus.Size = new System.Drawing.Size(154, 24);
            this.checkBoxAutoReadFullStatus.TabIndex = 25;
            this.checkBoxAutoReadFullStatus.Text = "Read Full Status";
            this.checkBoxAutoReadFullStatus.UseVisualStyleBackColor = true;
            this.checkBoxAutoReadFullStatus.CheckedChanged += new System.EventHandler(this.checkBox_auto_read_full_status_CheckedChanged);
            // 
            // checkBoxAutoWriteCurrents
            // 
            this.checkBoxAutoWriteCurrents.AutoSize = true;
            this.checkBoxAutoWriteCurrents.Location = new System.Drawing.Point(8, 62);
            this.checkBoxAutoWriteCurrents.Name = "checkBoxAutoWriteCurrents";
            this.checkBoxAutoWriteCurrents.Size = new System.Drawing.Size(137, 24);
            this.checkBoxAutoWriteCurrents.TabIndex = 27;
            this.checkBoxAutoWriteCurrents.Text = "Write Currents";
            this.checkBoxAutoWriteCurrents.UseVisualStyleBackColor = true;
            this.checkBoxAutoWriteCurrents.CheckedChanged += new System.EventHandler(this.checkBox_auto_write_currents_CheckedChanged);
            // 
            // groupBoxDevices
            // 
            this.groupBoxDevices.Controls.Add(this.buttonDeviceAdd);
            this.groupBoxDevices.Controls.Add(this.tabControlDevices);
            this.groupBoxDevices.Location = new System.Drawing.Point(14, 262);
            this.groupBoxDevices.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxDevices.Name = "groupBoxDevices";
            this.groupBoxDevices.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxDevices.Size = new System.Drawing.Size(1460, 1017);
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
            this.tabControlDevices.Size = new System.Drawing.Size(1402, 978);
            this.tabControlDevices.TabIndex = 0;
            this.tabControlDevices.SelectedIndexChanged += new System.EventHandler(this.tabControlDevices_SelectedIndexChanged);
            // 
            // comboBox_commDevices
            // 
            this.comboBox_commDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_commDevices.FormattingEnabled = true;
            this.comboBox_commDevices.Location = new System.Drawing.Point(10, 31);
            this.comboBox_commDevices.Name = "comboBox_commDevices";
            this.comboBox_commDevices.Size = new System.Drawing.Size(518, 28);
            this.comboBox_commDevices.TabIndex = 2;
            this.comboBox_commDevices.SelectedIndexChanged += new System.EventHandler(this.comboBox_commDevices_SelectedIndexChanged);
            // 
            // label_version
            // 
            this.label_version.AutoSize = true;
            this.label_version.Cursor = System.Windows.Forms.Cursors.Help;
            this.label_version.Location = new System.Drawing.Point(10, 172);
            this.label_version.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_version.MaximumSize = new System.Drawing.Size(168, 25);
            this.label_version.MinimumSize = new System.Drawing.Size(150, 20);
            this.label_version.Name = "label_version";
            this.label_version.Size = new System.Drawing.Size(150, 20);
            this.label_version.TabIndex = 31;
            this.label_version.Text = "Version 21";
            this.label_version.Click += new System.EventHandler(this.label_version_Click);
            // 
            // groupBoxAnimation
            // 
            this.groupBoxAnimation.Controls.Add(this.buttonAnimationHelp);
            this.groupBoxAnimation.Controls.Add(this.label_animation_file);
            this.groupBoxAnimation.Controls.Add(this.button_animation_startstop);
            this.groupBoxAnimation.Controls.Add(this.button_animation_load);
            this.groupBoxAnimation.Enabled = false;
            this.groupBoxAnimation.Location = new System.Drawing.Point(1173, 6);
            this.groupBoxAnimation.Name = "groupBoxAnimation";
            this.groupBoxAnimation.Size = new System.Drawing.Size(300, 158);
            this.groupBoxAnimation.TabIndex = 27;
            this.groupBoxAnimation.TabStop = false;
            this.groupBoxAnimation.Text = "Animation";
            // 
            // buttonAnimationHelp
            // 
            this.buttonAnimationHelp.Location = new System.Drawing.Point(254, 32);
            this.buttonAnimationHelp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonAnimationHelp.Name = "buttonAnimationHelp";
            this.buttonAnimationHelp.Size = new System.Drawing.Size(36, 35);
            this.buttonAnimationHelp.TabIndex = 28;
            this.buttonAnimationHelp.Text = "?";
            this.buttonAnimationHelp.UseVisualStyleBackColor = true;
            this.buttonAnimationHelp.Click += new System.EventHandler(this.buttonAnimationHelp_Click);
            // 
            // label_animation_file
            // 
            this.label_animation_file.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_animation_file.AutoSize = true;
            this.label_animation_file.Location = new System.Drawing.Point(15, 100);
            this.label_animation_file.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_animation_file.MaximumSize = new System.Drawing.Size(278, 22);
            this.label_animation_file.MinimumSize = new System.Drawing.Size(278, 22);
            this.label_animation_file.Name = "label_animation_file";
            this.label_animation_file.Size = new System.Drawing.Size(278, 22);
            this.label_animation_file.TabIndex = 27;
            this.label_animation_file.Text = "n/a";
            // 
            // button_animation_startstop
            // 
            this.button_animation_startstop.Enabled = false;
            this.button_animation_startstop.Location = new System.Drawing.Point(116, 32);
            this.button_animation_startstop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_animation_startstop.Name = "button_animation_startstop";
            this.button_animation_startstop.Size = new System.Drawing.Size(99, 35);
            this.button_animation_startstop.TabIndex = 4;
            this.button_animation_startstop.Text = "START";
            this.button_animation_startstop.UseVisualStyleBackColor = true;
            this.button_animation_startstop.Click += new System.EventHandler(this.button_animation_startstop_Click);
            // 
            // button_animation_load
            // 
            this.button_animation_load.Location = new System.Drawing.Point(8, 32);
            this.button_animation_load.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_animation_load.Name = "button_animation_load";
            this.button_animation_load.Size = new System.Drawing.Size(99, 35);
            this.button_animation_load.TabIndex = 3;
            this.button_animation_load.Text = "LOAD";
            this.button_animation_load.UseVisualStyleBackColor = true;
            this.button_animation_load.Click += new System.EventHandler(this.button_animation_load_Click);
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
            // groupBox_comm_secure
            // 
            this.groupBox_comm_secure.Controls.Add(this.comboBox_comm_break);
            this.groupBox_comm_secure.Controls.Add(this.label7);
            this.groupBox_comm_secure.Controls.Add(this.comboBox_comm_sync);
            this.groupBox_comm_secure.Controls.Add(this.comboBox_comm_parity);
            this.groupBox_comm_secure.Controls.Add(this.comboBox_comm_header);
            this.groupBox_comm_secure.Controls.Add(this.label6);
            this.groupBox_comm_secure.Controls.Add(this.comboBox_comm_bitrate);
            this.groupBox_comm_secure.Controls.Add(this.label5);
            this.groupBox_comm_secure.Controls.Add(this.label3);
            this.groupBox_comm_secure.Controls.Add(this.label2);
            this.groupBox_comm_secure.Location = new System.Drawing.Point(189, 172);
            this.groupBox_comm_secure.Name = "groupBox_comm_secure";
            this.groupBox_comm_secure.Size = new System.Drawing.Size(974, 82);
            this.groupBox_comm_secure.TabIndex = 28;
            this.groupBox_comm_secure.TabStop = false;
            this.groupBox_comm_secure.Text = "Comm Parameters";
            // 
            // comboBox_comm_break
            // 
            this.comboBox_comm_break.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_comm_break.FormattingEnabled = true;
            this.comboBox_comm_break.Location = new System.Drawing.Point(300, 34);
            this.comboBox_comm_break.Name = "comboBox_comm_break";
            this.comboBox_comm_break.Size = new System.Drawing.Size(78, 28);
            this.comboBox_comm_break.TabIndex = 37;
            this.comboBox_comm_break.SelectedIndexChanged += new System.EventHandler(this.comboBox_comm_break_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(236, 38);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.MaximumSize = new System.Drawing.Size(202, 22);
            this.label7.MinimumSize = new System.Drawing.Size(22, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 22);
            this.label7.TabIndex = 36;
            this.label7.Text = "Break:";
            // 
            // comboBox_comm_sync
            // 
            this.comboBox_comm_sync.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_comm_sync.FormattingEnabled = true;
            this.comboBox_comm_sync.Location = new System.Drawing.Point(464, 34);
            this.comboBox_comm_sync.Name = "comboBox_comm_sync";
            this.comboBox_comm_sync.Size = new System.Drawing.Size(74, 28);
            this.comboBox_comm_sync.TabIndex = 35;
            this.comboBox_comm_sync.SelectedIndexChanged += new System.EventHandler(this.comboBox_comm_sync_SelectedIndexChanged);
            // 
            // comboBox_comm_parity
            // 
            this.comboBox_comm_parity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_comm_parity.FormattingEnabled = true;
            this.comboBox_comm_parity.Location = new System.Drawing.Point(621, 34);
            this.comboBox_comm_parity.Name = "comboBox_comm_parity";
            this.comboBox_comm_parity.Size = new System.Drawing.Size(88, 28);
            this.comboBox_comm_parity.TabIndex = 34;
            this.comboBox_comm_parity.SelectedIndexChanged += new System.EventHandler(this.comboBox_comm_parity_SelectedIndexChanged);
            // 
            // comboBox_comm_header
            // 
            this.comboBox_comm_header.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_comm_header.FormattingEnabled = true;
            this.comboBox_comm_header.Location = new System.Drawing.Point(798, 34);
            this.comboBox_comm_header.Name = "comboBox_comm_header";
            this.comboBox_comm_header.Size = new System.Drawing.Size(78, 28);
            this.comboBox_comm_header.TabIndex = 33;
            this.comboBox_comm_header.SelectedIndexChanged += new System.EventHandler(this.comboBox_comm_header_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(404, 38);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.MaximumSize = new System.Drawing.Size(202, 22);
            this.label6.MinimumSize = new System.Drawing.Size(22, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 22);
            this.label6.TabIndex = 32;
            this.label6.Text = "Sync:";
            // 
            // comboBox_comm_bitrate
            // 
            this.comboBox_comm_bitrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_comm_bitrate.FormattingEnabled = true;
            this.comboBox_comm_bitrate.Location = new System.Drawing.Point(75, 34);
            this.comboBox_comm_bitrate.Name = "comboBox_comm_bitrate";
            this.comboBox_comm_bitrate.Size = new System.Drawing.Size(130, 28);
            this.comboBox_comm_bitrate.TabIndex = 4;
            this.comboBox_comm_bitrate.SelectedIndexChanged += new System.EventHandler(this.comboBox_comm_bitrate_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(560, 38);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.MaximumSize = new System.Drawing.Size(202, 22);
            this.label5.MinimumSize = new System.Drawing.Size(22, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 22);
            this.label5.TabIndex = 31;
            this.label5.Text = "Parity:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(724, 38);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.MaximumSize = new System.Drawing.Size(202, 22);
            this.label3.MinimumSize = new System.Drawing.Size(22, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 22);
            this.label3.TabIndex = 30;
            this.label3.Text = "Header:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.MaximumSize = new System.Drawing.Size(202, 22);
            this.label2.MinimumSize = new System.Drawing.Size(22, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 22);
            this.label2.TabIndex = 29;
            this.label2.Text = "Bitrate:";
            // 
            // groupBox_adapter
            // 
            this.groupBox_adapter.Controls.Add(this.buttonOpenClose);
            this.groupBox_adapter.Controls.Add(this.comboBox_commDevices);
            this.groupBox_adapter.Location = new System.Drawing.Point(189, 8);
            this.groupBox_adapter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_adapter.Name = "groupBox_adapter";
            this.groupBox_adapter.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_adapter.Size = new System.Drawing.Size(660, 74);
            this.groupBox_adapter.TabIndex = 32;
            this.groupBox_adapter.TabStop = false;
            this.groupBox_adapter.Text = "Adapter";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label_stat_max);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label_stat_mean);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(9, 29);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(222, 131);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Comm Cycle Statistic";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(106, 78);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.MaximumSize = new System.Drawing.Size(202, 22);
            this.label4.MinimumSize = new System.Drawing.Size(22, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 22);
            this.label4.TabIndex = 33;
            this.label4.Text = "ms";
            // 
            // label_stat_max
            // 
            this.label_stat_max.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_stat_max.AutoSize = true;
            this.label_stat_max.Location = new System.Drawing.Point(66, 78);
            this.label_stat_max.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_stat_max.MaximumSize = new System.Drawing.Size(202, 22);
            this.label_stat_max.MinimumSize = new System.Drawing.Size(22, 22);
            this.label_stat_max.Name = "label_stat_max";
            this.label_stat_max.Size = new System.Drawing.Size(31, 22);
            this.label_stat_max.TabIndex = 32;
            this.label_stat_max.Text = "n/a";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 78);
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
            this.label8.Location = new System.Drawing.Point(106, 43);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.MaximumSize = new System.Drawing.Size(202, 22);
            this.label8.MinimumSize = new System.Drawing.Size(22, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 22);
            this.label8.TabIndex = 30;
            this.label8.Text = "ms";
            // 
            // label_stat_mean
            // 
            this.label_stat_mean.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_stat_mean.AutoSize = true;
            this.label_stat_mean.Location = new System.Drawing.Point(66, 43);
            this.label_stat_mean.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_stat_mean.MaximumSize = new System.Drawing.Size(202, 22);
            this.label_stat_mean.MinimumSize = new System.Drawing.Size(22, 22);
            this.label_stat_mean.Name = "label_stat_mean";
            this.label_stat_mean.Size = new System.Drawing.Size(31, 22);
            this.label_stat_mean.TabIndex = 29;
            this.label_stat_mean.Text = "n/a";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 43);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.MaximumSize = new System.Drawing.Size(202, 22);
            this.label1.MinimumSize = new System.Drawing.Size(22, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 22);
            this.label1.TabIndex = 28;
            this.label1.Text = "Mean:";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(162, 37);
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
            this.groupBox3.Location = new System.Drawing.Point(9, 169);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(222, 80);
            this.groupBox3.TabIndex = 37;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "GUI Update Rate";
            // 
            // comboBox_gui_interval
            // 
            this.comboBox_gui_interval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_gui_interval.FormattingEnabled = true;
            this.comboBox_gui_interval.Location = new System.Drawing.Point(9, 32);
            this.comboBox_gui_interval.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox_gui_interval.Name = "comboBox_gui_interval";
            this.comboBox_gui_interval.Size = new System.Drawing.Size(142, 28);
            this.comboBox_gui_interval.TabIndex = 37;
            // 
            // groupBox_sleep
            // 
            this.groupBox_sleep.Controls.Add(this.checkBox_wake_ack);
            this.groupBox_sleep.Controls.Add(this.checkBox_wake_symbol);
            this.groupBox_sleep.Controls.Add(this.button_wakeup);
            this.groupBox_sleep.Controls.Add(this.button_sleep);
            this.groupBox_sleep.Enabled = false;
            this.groupBox_sleep.Location = new System.Drawing.Point(336, 91);
            this.groupBox_sleep.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_sleep.Name = "groupBox_sleep";
            this.groupBox_sleep.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_sleep.Size = new System.Drawing.Size(513, 74);
            this.groupBox_sleep.TabIndex = 29;
            this.groupBox_sleep.TabStop = false;
            this.groupBox_sleep.Text = "Sleep";
            // 
            // checkBox_wake_ack
            // 
            this.checkBox_wake_ack.AutoSize = true;
            this.checkBox_wake_ack.Checked = true;
            this.checkBox_wake_ack.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_wake_ack.Location = new System.Drawing.Point(399, 34);
            this.checkBox_wake_ack.Name = "checkBox_wake_ack";
            this.checkBox_wake_ack.Size = new System.Drawing.Size(67, 24);
            this.checkBox_wake_ack.TabIndex = 27;
            this.checkBox_wake_ack.Text = "ACK";
            this.checkBox_wake_ack.UseVisualStyleBackColor = true;
            // 
            // checkBox_wake_symbol
            // 
            this.checkBox_wake_symbol.AutoSize = true;
            this.checkBox_wake_symbol.Checked = true;
            this.checkBox_wake_symbol.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_wake_symbol.Location = new System.Drawing.Point(303, 34);
            this.checkBox_wake_symbol.Name = "checkBox_wake_symbol";
            this.checkBox_wake_symbol.Size = new System.Drawing.Size(87, 24);
            this.checkBox_wake_symbol.TabIndex = 26;
            this.checkBox_wake_symbol.Text = "Symbol";
            this.checkBox_wake_symbol.UseVisualStyleBackColor = true;
            // 
            // button_wakeup
            // 
            this.button_wakeup.Location = new System.Drawing.Point(190, 28);
            this.button_wakeup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_wakeup.Name = "button_wakeup";
            this.button_wakeup.Size = new System.Drawing.Size(105, 35);
            this.button_wakeup.TabIndex = 3;
            this.button_wakeup.Text = "Wakeup!";
            this.button_wakeup.UseVisualStyleBackColor = true;
            this.button_wakeup.Click += new System.EventHandler(this.button_wakeup_Click);
            // 
            // button_sleep
            // 
            this.button_sleep.Location = new System.Drawing.Point(9, 28);
            this.button_sleep.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_sleep.Name = "button_sleep";
            this.button_sleep.Size = new System.Drawing.Size(105, 35);
            this.button_sleep.TabIndex = 2;
            this.button_sleep.Text = "Sleep!";
            this.button_sleep.UseVisualStyleBackColor = true;
            this.button_sleep.Click += new System.EventHandler(this.button_sleep_Click);
            // 
            // groupBox_trace
            // 
            this.groupBox_trace.Controls.Add(this.button_tracer);
            this.groupBox_trace.Enabled = false;
            this.groupBox_trace.Location = new System.Drawing.Point(189, 91);
            this.groupBox_trace.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_trace.Name = "groupBox_trace";
            this.groupBox_trace.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_trace.Size = new System.Drawing.Size(138, 74);
            this.groupBox_trace.TabIndex = 38;
            this.groupBox_trace.TabStop = false;
            this.groupBox_trace.Text = "Trace";
            // 
            // button_tracer
            // 
            this.button_tracer.Location = new System.Drawing.Point(12, 25);
            this.button_tracer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_tracer.Name = "button_tracer";
            this.button_tracer.Size = new System.Drawing.Size(105, 35);
            this.button_tracer.TabIndex = 3;
            this.button_tracer.Text = "Tracer";
            this.button_tracer.UseVisualStyleBackColor = true;
            this.button_tracer.Click += new System.EventHandler(this.button_tracer_Click);
            // 
            // checkBox_pg_mode
            // 
            this.checkBox_pg_mode.AutoSize = true;
            this.checkBox_pg_mode.Enabled = false;
            this.checkBox_pg_mode.Location = new System.Drawing.Point(9, 28);
            this.checkBox_pg_mode.Name = "checkBox_pg_mode";
            this.checkBox_pg_mode.Size = new System.Drawing.Size(166, 24);
            this.checkBox_pg_mode.TabIndex = 39;
            this.checkBox_pg_mode.Text = "Pattern Gen Mode";
            this.checkBox_pg_mode.UseVisualStyleBackColor = true;
            // 
            // groupBox_hidden
            // 
            this.groupBox_hidden.Controls.Add(this.groupBox_auto_imm);
            this.groupBox_hidden.Controls.Add(this.groupBox4);
            this.groupBox_hidden.Controls.Add(this.groupBox_wakeup_length);
            this.groupBox_hidden.Controls.Add(this.groupBox_direct_bus);
            this.groupBox_hidden.Controls.Add(this.groupBox2);
            this.groupBox_hidden.Controls.Add(this.groupBox3);
            this.groupBox_hidden.Location = new System.Drawing.Point(1497, 6);
            this.groupBox_hidden.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_hidden.Name = "groupBox_hidden";
            this.groupBox_hidden.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_hidden.Size = new System.Drawing.Size(244, 1272);
            this.groupBox_hidden.TabIndex = 40;
            this.groupBox_hidden.TabStop = false;
            this.groupBox_hidden.Text = "Hidden";
            this.groupBox_hidden.Visible = false;
            // 
            // groupBox_auto_imm
            // 
            this.groupBox_auto_imm.Controls.Add(this.checkBox_auto_imm_enables);
            this.groupBox_auto_imm.Controls.Add(this.checkBox_auto_imm_current0);
            this.groupBox_auto_imm.Controls.Add(this.checkBox_auto_imm_pulse0);
            this.groupBox_auto_imm.Enabled = false;
            this.groupBox_auto_imm.Location = new System.Drawing.Point(9, 582);
            this.groupBox_auto_imm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_auto_imm.Name = "groupBox_auto_imm";
            this.groupBox_auto_imm.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_auto_imm.Size = new System.Drawing.Size(222, 143);
            this.groupBox_auto_imm.TabIndex = 43;
            this.groupBox_auto_imm.TabStop = false;
            this.groupBox_auto_imm.Text = "Automatic IMM Write";
            // 
            // checkBox_auto_imm_enables
            // 
            this.checkBox_auto_imm_enables.AutoSize = true;
            this.checkBox_auto_imm_enables.Location = new System.Drawing.Point(9, 97);
            this.checkBox_auto_imm_enables.Name = "checkBox_auto_imm_enables";
            this.checkBox_auto_imm_enables.Size = new System.Drawing.Size(124, 24);
            this.checkBox_auto_imm_enables.TabIndex = 44;
            this.checkBox_auto_imm_enables.Text = "Led Enables";
            this.checkBox_auto_imm_enables.UseVisualStyleBackColor = true;
            this.checkBox_auto_imm_enables.CheckedChanged += new System.EventHandler(this.checkBox_auto_imm_enables_CheckedChanged);
            // 
            // checkBox_auto_imm_current0
            // 
            this.checkBox_auto_imm_current0.AutoSize = true;
            this.checkBox_auto_imm_current0.Location = new System.Drawing.Point(9, 65);
            this.checkBox_auto_imm_current0.Name = "checkBox_auto_imm_current0";
            this.checkBox_auto_imm_current0.Size = new System.Drawing.Size(156, 24);
            this.checkBox_auto_imm_current0.TabIndex = 41;
            this.checkBox_auto_imm_current0.Text = "Current[0] -> ALL";
            this.checkBox_auto_imm_current0.UseVisualStyleBackColor = true;
            this.checkBox_auto_imm_current0.CheckedChanged += new System.EventHandler(this.checkBox_auto_imm_current0_CheckedChanged);
            // 
            // checkBox_auto_imm_pulse0
            // 
            this.checkBox_auto_imm_pulse0.AutoSize = true;
            this.checkBox_auto_imm_pulse0.Location = new System.Drawing.Point(9, 32);
            this.checkBox_auto_imm_pulse0.Name = "checkBox_auto_imm_pulse0";
            this.checkBox_auto_imm_pulse0.Size = new System.Drawing.Size(154, 24);
            this.checkBox_auto_imm_pulse0.TabIndex = 39;
            this.checkBox_auto_imm_pulse0.Text = "Pulse[0]    -> ALL";
            this.checkBox_auto_imm_pulse0.UseVisualStyleBackColor = true;
            this.checkBox_auto_imm_pulse0.CheckedChanged += new System.EventHandler(this.checkBox_auto_imm_pulse0_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBox_pg_mode);
            this.groupBox4.Location = new System.Drawing.Point(9, 492);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(222, 80);
            this.groupBox4.TabIndex = 42;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Pattern Gen Mode";
            // 
            // groupBox_wakeup_length
            // 
            this.groupBox_wakeup_length.Controls.Add(this.comboBox_wakeup_length);
            this.groupBox_wakeup_length.Controls.Add(this.label12);
            this.groupBox_wakeup_length.Location = new System.Drawing.Point(9, 403);
            this.groupBox_wakeup_length.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_wakeup_length.Name = "groupBox_wakeup_length";
            this.groupBox_wakeup_length.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_wakeup_length.Size = new System.Drawing.Size(222, 80);
            this.groupBox_wakeup_length.TabIndex = 41;
            this.groupBox_wakeup_length.TabStop = false;
            this.groupBox_wakeup_length.Text = "Wakeup Symbol Length";
            // 
            // comboBox_wakeup_length
            // 
            this.comboBox_wakeup_length.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_wakeup_length.FormattingEnabled = true;
            this.comboBox_wakeup_length.Location = new System.Drawing.Point(9, 32);
            this.comboBox_wakeup_length.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox_wakeup_length.Name = "comboBox_wakeup_length";
            this.comboBox_wakeup_length.Size = new System.Drawing.Size(142, 28);
            this.comboBox_wakeup_length.TabIndex = 37;
            this.comboBox_wakeup_length.SelectedIndexChanged += new System.EventHandler(this.comboBox_wakeup_length_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(162, 37);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.MaximumSize = new System.Drawing.Size(202, 22);
            this.label12.MinimumSize = new System.Drawing.Size(22, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 22);
            this.label12.TabIndex = 36;
            this.label12.Text = "us";
            // 
            // groupBox_direct_bus
            // 
            this.groupBox_direct_bus.Controls.Add(this.button_clear_break);
            this.groupBox_direct_bus.Controls.Add(this.button_set_break);
            this.groupBox_direct_bus.Enabled = false;
            this.groupBox_direct_bus.Location = new System.Drawing.Point(9, 260);
            this.groupBox_direct_bus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_direct_bus.Name = "groupBox_direct_bus";
            this.groupBox_direct_bus.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_direct_bus.Size = new System.Drawing.Size(222, 134);
            this.groupBox_direct_bus.TabIndex = 40;
            this.groupBox_direct_bus.TabStop = false;
            this.groupBox_direct_bus.Text = "Direct Bus";
            // 
            // button_clear_break
            // 
            this.button_clear_break.Location = new System.Drawing.Point(9, 74);
            this.button_clear_break.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_clear_break.Name = "button_clear_break";
            this.button_clear_break.Size = new System.Drawing.Size(144, 35);
            this.button_clear_break.TabIndex = 5;
            this.button_clear_break.Text = "ClearBreak";
            this.button_clear_break.UseVisualStyleBackColor = true;
            this.button_clear_break.Click += new System.EventHandler(this.button_clear_break_Click);
            // 
            // button_set_break
            // 
            this.button_set_break.Location = new System.Drawing.Point(9, 29);
            this.button_set_break.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_set_break.Name = "button_set_break";
            this.button_set_break.Size = new System.Drawing.Size(144, 35);
            this.button_set_break.TabIndex = 4;
            this.button_set_break.Text = "SetBreak";
            this.button_set_break.UseVisualStyleBackColor = true;
            this.button_set_break.Click += new System.EventHandler(this.button_set_break_Click);
            // 
            // grpBoxExpert
            // 
            this.grpBoxExpert.Controls.Add(this.btnExpert);
            this.grpBoxExpert.Controls.Add(this.txtAuthenticateExpert);
            this.grpBoxExpert.Location = new System.Drawing.Point(14, 8);
            this.grpBoxExpert.Name = "grpBoxExpert";
            this.grpBoxExpert.Size = new System.Drawing.Size(167, 121);
            this.grpBoxExpert.TabIndex = 41;
            this.grpBoxExpert.TabStop = false;
            this.grpBoxExpert.Text = "Expert Mode";
            // 
            // btnExpert
            // 
            this.btnExpert.Location = new System.Drawing.Point(25, 80);
            this.btnExpert.Name = "btnExpert";
            this.btnExpert.Size = new System.Drawing.Size(115, 31);
            this.btnExpert.TabIndex = 1;
            this.btnExpert.Text = "Expert";
            this.btnExpert.UseVisualStyleBackColor = true;
            this.btnExpert.Click += new System.EventHandler(this.btnExpert_Click);
            // 
            // txtAuthenticateExpert
            // 
            this.txtAuthenticateExpert.Location = new System.Drawing.Point(25, 36);
            this.txtAuthenticateExpert.Name = "txtAuthenticateExpert";
            this.txtAuthenticateExpert.Size = new System.Drawing.Size(114, 26);
            this.txtAuthenticateExpert.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1758, 1049);
            this.Controls.Add(this.grpBoxExpert);
            this.Controls.Add(this.groupBox_hidden);
            this.Controls.Add(this.groupBox_trace);
            this.Controls.Add(this.groupBox_sleep);
            this.Controls.Add(this.groupBox_adapter);
            this.Controls.Add(this.groupBox_auto);
            this.Controls.Add(this.groupBox_comm_secure);
            this.Controls.Add(this.groupBoxAnimation);
            this.Controls.Add(this.label_version);
            this.Controls.Add(this.groupBoxDevices);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "ELMOS 522.94 UCAN Eval";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.groupBox_auto.ResumeLayout(false);
            this.groupBox_auto.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutoWriteInterval)).EndInit();
            this.groupBoxDevices.ResumeLayout(false);
            this.groupBoxAnimation.ResumeLayout(false);
            this.groupBoxAnimation.PerformLayout();
            this.groupBox_comm_secure.ResumeLayout(false);
            this.groupBox_comm_secure.PerformLayout();
            this.groupBox_adapter.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox_sleep.ResumeLayout(false);
            this.groupBox_sleep.PerformLayout();
            this.groupBox_trace.ResumeLayout(false);
            this.groupBox_hidden.ResumeLayout(false);
            this.groupBox_auto_imm.ResumeLayout(false);
            this.groupBox_auto_imm.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox_wakeup_length.ResumeLayout(false);
            this.groupBox_wakeup_length.PerformLayout();
            this.groupBox_direct_bus.ResumeLayout(false);
            this.grpBoxExpert.ResumeLayout(false);
            this.grpBoxExpert.PerformLayout();
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
                private System.Windows.Forms.Label label_version;
                private System.Windows.Forms.CheckBox checkBoxAutoWritePwm;
                private System.Windows.Forms.ComboBox comboBox_commDevices;
                private System.Windows.Forms.GroupBox groupBoxAnimation;
                private System.Windows.Forms.OpenFileDialog openFileDialog_animation;
                private System.Windows.Forms.Button button_animation_load;
                private System.Windows.Forms.Label label_animation_file;
                private System.Windows.Forms.Button button_animation_startstop;
                private System.Windows.Forms.OpenFileDialog openFileDialog1;
                private System.Windows.Forms.GroupBox groupBox_comm_secure;
                private System.Windows.Forms.CheckBox checkBoxAutoWriteCurrents;
                private System.Windows.Forms.GroupBox groupBox_auto;
                private System.Windows.Forms.CheckBox checkBoxAutoReadFullStatus;
                private System.Windows.Forms.GroupBox groupBox_adapter;
                private System.Windows.Forms.CheckBox checkBoxAutoReadDiag;
                private System.Windows.Forms.GroupBox groupBox2;
                private System.Windows.Forms.Label label4;
                private System.Windows.Forms.Label label_stat_max;
                private System.Windows.Forms.Label label11;
                private System.Windows.Forms.Label label8;
                private System.Windows.Forms.Label label_stat_mean;
                private System.Windows.Forms.Label label1;
                private System.Windows.Forms.Label label10;
                private System.Windows.Forms.GroupBox groupBox3;
                private System.Windows.Forms.ComboBox comboBox_gui_interval;
                private System.Windows.Forms.Label label5;
                private System.Windows.Forms.Label label3;
                private System.Windows.Forms.Label label2;
                private System.Windows.Forms.ComboBox comboBox_comm_bitrate;
                private System.Windows.Forms.Label label6;
                private System.Windows.Forms.GroupBox groupBox_sleep;
                private System.Windows.Forms.Button button_sleep;
                private System.Windows.Forms.Button button_wakeup;
                private System.Windows.Forms.ComboBox comboBox_comm_sync;
                private System.Windows.Forms.ComboBox comboBox_comm_parity;
                private System.Windows.Forms.ComboBox comboBox_comm_header;
                private System.Windows.Forms.Button buttonAnimationHelp;
                private System.Windows.Forms.CheckBox checkBox_wake_ack;
                private System.Windows.Forms.CheckBox checkBox_wake_symbol;
                private System.Windows.Forms.ComboBox comboBox_comm_break;
                private System.Windows.Forms.Label label7;
                private System.Windows.Forms.Label label9;
                private System.Windows.Forms.GroupBox groupBox1;
                private System.Windows.Forms.GroupBox groupBox_trace;
                private System.Windows.Forms.Button button_tracer;
                private System.Windows.Forms.CheckBox checkBox_pg_mode;
                private System.Windows.Forms.GroupBox groupBox_hidden;
                private System.Windows.Forms.GroupBox groupBox_direct_bus;
                private System.Windows.Forms.Button button_clear_break;
                private System.Windows.Forms.Button button_set_break;
                private System.Windows.Forms.GroupBox groupBox_wakeup_length;
                private System.Windows.Forms.ComboBox comboBox_wakeup_length;
                private System.Windows.Forms.Label label12;
                private System.Windows.Forms.GroupBox groupBox_auto_imm;
                private System.Windows.Forms.CheckBox checkBox_auto_imm_enables;
                private System.Windows.Forms.CheckBox checkBox_auto_imm_current0;
                private System.Windows.Forms.CheckBox checkBox_auto_imm_pulse0;
                private System.Windows.Forms.GroupBox groupBox4;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox grpBoxExpert;
        private System.Windows.Forms.Button btnExpert;
        private System.Windows.Forms.TextBox txtAuthenticateExpert;
    }
}

