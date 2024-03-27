namespace Elmos_COM_Tool
{
    partial class ComTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComTool));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmb_ComDevices = new System.Windows.Forms.ComboBox();
            this.tabPage = new System.Windows.Forms.TabPage();
            this.tabControl_Device = new System.Windows.Forms.TabControl();
            this.grpProtocol = new System.Windows.Forms.GroupBox();
            this.rdoUARTButton = new System.Windows.Forms.RadioButton();
            this.rdoFDCANButton = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnComm = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tabControl_Device.SuspendLayout();
            this.grpProtocol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmb_ComDevices);
            this.groupBox1.Location = new System.Drawing.Point(483, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(428, 55);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chip";
            // 
            // cmb_ComDevices
            // 
            this.cmb_ComDevices.FormattingEnabled = true;
            this.cmb_ComDevices.Location = new System.Drawing.Point(68, 23);
            this.cmb_ComDevices.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_ComDevices.Name = "cmb_ComDevices";
            this.cmb_ComDevices.Size = new System.Drawing.Size(283, 24);
            this.cmb_ComDevices.TabIndex = 0;
            this.cmb_ComDevices.SelectedIndexChanged += new System.EventHandler(this.cmb_ComDevices_SelectedIndexChanged);
            // 
            // tabPage
            // 
            this.tabPage.AutoScroll = true;
            this.tabPage.Location = new System.Drawing.Point(4, 25);
            this.tabPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage.Name = "tabPage";
            this.tabPage.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage.Size = new System.Drawing.Size(1348, 1025);
            this.tabPage.TabIndex = 0;
            this.tabPage.UseVisualStyleBackColor = true;
            // 
            // tabControl_Device
            // 
            this.tabControl_Device.Controls.Add(this.tabPage);
            this.tabControl_Device.Location = new System.Drawing.Point(15, 79);
            this.tabControl_Device.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl_Device.Name = "tabControl_Device";
            this.tabControl_Device.SelectedIndex = 0;
            this.tabControl_Device.Size = new System.Drawing.Size(1356, 1054);
            this.tabControl_Device.TabIndex = 1;
            // 
            // grpProtocol
            // 
            this.grpProtocol.Controls.Add(this.rdoUARTButton);
            this.grpProtocol.Controls.Add(this.rdoFDCANButton);
            this.grpProtocol.Location = new System.Drawing.Point(214, 10);
            this.grpProtocol.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpProtocol.Name = "grpProtocol";
            this.grpProtocol.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpProtocol.Size = new System.Drawing.Size(240, 58);
            this.grpProtocol.TabIndex = 2;
            this.grpProtocol.TabStop = false;
            this.grpProtocol.Text = "Protocols";
            // 
            // rdoUARTButton
            // 
            this.rdoUARTButton.AutoSize = true;
            this.rdoUARTButton.Location = new System.Drawing.Point(148, 20);
            this.rdoUARTButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rdoUARTButton.Name = "rdoUARTButton";
            this.rdoUARTButton.Size = new System.Drawing.Size(66, 20);
            this.rdoUARTButton.TabIndex = 1;
            this.rdoUARTButton.TabStop = true;
            this.rdoUARTButton.Text = "UART";
            this.rdoUARTButton.UseVisualStyleBackColor = true;
            this.rdoUARTButton.CheckedChanged += new System.EventHandler(this.rdoUARTButton_CheckedChanged);
            // 
            // rdoFDCANButton
            // 
            this.rdoFDCANButton.AutoSize = true;
            this.rdoFDCANButton.Checked = true;
            this.rdoFDCANButton.Location = new System.Drawing.Point(35, 22);
            this.rdoFDCANButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rdoFDCANButton.Name = "rdoFDCANButton";
            this.rdoFDCANButton.Size = new System.Drawing.Size(74, 20);
            this.rdoFDCANButton.TabIndex = 0;
            this.rdoFDCANButton.TabStop = true;
            this.rdoFDCANButton.Text = "FDCAN";
            this.rdoFDCANButton.UseVisualStyleBackColor = true;
            this.rdoFDCANButton.CheckedChanged += new System.EventHandler(this.rdoFDCANButton_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Elmos_COM_Tool.Properties.Resources.elmos_logo_rgb_standard;
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(19, 15);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(138, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 31;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.WaitOnLoad = true;
            // 
            // btnComm
            // 
            this.btnComm.Location = new System.Drawing.Point(1119, 15);
            this.btnComm.Name = "btnComm";
            this.btnComm.Size = new System.Drawing.Size(108, 37);
            this.btnComm.TabIndex = 32;
            this.btnComm.Text = "Communication";
            this.btnComm.UseVisualStyleBackColor = true;
            this.btnComm.Visible = false;
            this.btnComm.Click += new System.EventHandler(this.btnComm_Click);
            // 
            // ComTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1561, 844);
            this.Controls.Add(this.btnComm);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.grpProtocol);
            this.Controls.Add(this.tabControl_Device);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ComTool";
            this.Text = "Com Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ComTool_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.tabControl_Device.ResumeLayout(false);
            this.grpProtocol.ResumeLayout(false);
            this.grpProtocol.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmb_ComDevices;
        private System.Windows.Forms.TabPage tabPage;
        private System.Windows.Forms.TabControl tabControl_Device;
        private System.Windows.Forms.GroupBox grpProtocol;
        private System.Windows.Forms.RadioButton rdoUARTButton;
        private System.Windows.Forms.RadioButton rdoFDCANButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnComm;
    }
}

