namespace _52294_Socket_Master
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog_animation = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label_port = new System.Windows.Forms.Label();
            this.button_start = new System.Windows.Forms.Button();
            this.button_stop = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label_socket_state = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_timeout = new System.Windows.Forms.Label();
            this.label_version = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label_ftdi_state = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label_node_addr = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label_periodic = new System.Windows.Forms.Label();
            this.tracerControl1 = new UcanCommLib.TracerControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(10, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(110, 46);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.WaitOnLoad = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(251, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Port:";
            // 
            // label_port
            // 
            this.label_port.AutoSize = true;
            this.label_port.Location = new System.Drawing.Point(328, 9);
            this.label_port.Name = "label_port";
            this.label_port.Size = new System.Drawing.Size(24, 13);
            this.label_port.TabIndex = 32;
            this.label_port.Text = "n/a";
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(148, 9);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(75, 23);
            this.button_start.TabIndex = 33;
            this.button_start.Text = "START";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // button_stop
            // 
            this.button_stop.Location = new System.Drawing.Point(148, 38);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(75, 23);
            this.button_stop.TabIndex = 34;
            this.button_stop.Text = "STOP";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "Socket State:";
            // 
            // label_socket_state
            // 
            this.label_socket_state.AutoSize = true;
            this.label_socket_state.Location = new System.Drawing.Point(328, 31);
            this.label_socket_state.Name = "label_socket_state";
            this.label_socket_state.Size = new System.Drawing.Size(24, 13);
            this.label_socket_state.TabIndex = 36;
            this.label_socket_state.Text = "n/a";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(251, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 37;
            this.label3.Text = "Timeout:";
            // 
            // label_timeout
            // 
            this.label_timeout.AutoSize = true;
            this.label_timeout.Location = new System.Drawing.Point(328, 53);
            this.label_timeout.Name = "label_timeout";
            this.label_timeout.Size = new System.Drawing.Size(24, 13);
            this.label_timeout.TabIndex = 38;
            this.label_timeout.Text = "n/a";
            // 
            // label_version
            // 
            this.label_version.AutoSize = true;
            this.label_version.Cursor = System.Windows.Forms.Cursors.Help;
            this.label_version.Location = new System.Drawing.Point(7, 72);
            this.label_version.MaximumSize = new System.Drawing.Size(112, 16);
            this.label_version.MinimumSize = new System.Drawing.Size(100, 13);
            this.label_version.Name = "label_version";
            this.label_version.Size = new System.Drawing.Size(100, 13);
            this.label_version.TabIndex = 39;
            this.label_version.Text = "Version 7";
            this.label_version.Click += new System.EventHandler(this.label_version_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(471, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 41;
            this.label4.Text = "FTDI State:";
            // 
            // label_ftdi_state
            // 
            this.label_ftdi_state.AutoSize = true;
            this.label_ftdi_state.Location = new System.Drawing.Point(539, 9);
            this.label_ftdi_state.Name = "label_ftdi_state";
            this.label_ftdi_state.Size = new System.Drawing.Size(24, 13);
            this.label_ftdi_state.TabIndex = 42;
            this.label_ftdi_state.Text = "n/a";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(471, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 43;
            this.label5.Text = "Node Addr:";
            // 
            // label_node_addr
            // 
            this.label_node_addr.AutoSize = true;
            this.label_node_addr.Location = new System.Drawing.Point(539, 31);
            this.label_node_addr.Name = "label_node_addr";
            this.label_node_addr.Size = new System.Drawing.Size(24, 13);
            this.label_node_addr.TabIndex = 44;
            this.label_node_addr.Text = "n/a";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(471, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 45;
            this.label6.Text = "Periodic:";
            // 
            // label_periodic
            // 
            this.label_periodic.AutoSize = true;
            this.label_periodic.Location = new System.Drawing.Point(539, 53);
            this.label_periodic.Name = "label_periodic";
            this.label_periodic.Size = new System.Drawing.Size(24, 13);
            this.label_periodic.TabIndex = 46;
            this.label_periodic.Text = "n/a";
            // 
            // tracerControl1
            // 
            this.tracerControl1.Location = new System.Drawing.Point(13, 101);
            this.tracerControl1.Name = "tracerControl1";
            this.tracerControl1.Size = new System.Drawing.Size(1152, 548);
            this.tracerControl1.TabIndex = 40;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 661);
            this.Controls.Add(this.label_periodic);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label_node_addr);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label_ftdi_state);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tracerControl1);
            this.Controls.Add(this.label_version);
            this.Controls.Add(this.label_timeout);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_socket_state);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_stop);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.label_port);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "MainForm";
            this.Text = "ELMOS 522.94 Socket Master";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog_animation;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_port;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_socket_state;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_timeout;
        private System.Windows.Forms.Label label_version;
        private UcanCommLib.TracerControl tracerControl1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_ftdi_state;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_node_addr;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label_periodic;
	}
}

