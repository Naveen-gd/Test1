namespace ELMOS_521._38_UART_Eval
{
    partial class AddDevice
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
            this.ButAdd = new System.Windows.Forms.Button();
            this.tblDeviceInfo = new System.Windows.Forms.TableLayoutPanel();
            this.BoxDIHWVersion = new System.Windows.Forms.TextBox();
            this.LabDIHWVersion = new System.Windows.Forms.Label();
            this.LabDIAddress = new System.Windows.Forms.Label();
            this.BoxDIDeviceAddress = new System.Windows.Forms.TextBox();
            this.BoxDIFWVariant = new System.Windows.Forms.TextBox();
            this.LabDIFWVariant = new System.Windows.Forms.Label();
            this.LabDIFWVersion = new System.Windows.Forms.Label();
            this.BoxDIFWVersion = new System.Windows.Forms.TextBox();
            this.ButCancel = new System.Windows.Forms.Button();
            this.LabStatus = new System.Windows.Forms.Label();
            this.tblDeviceInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButAdd
            // 
            this.ButAdd.Location = new System.Drawing.Point(81, 195);
            this.ButAdd.Name = "ButAdd";
            this.ButAdd.Size = new System.Drawing.Size(66, 23);
            this.ButAdd.TabIndex = 1;
            this.ButAdd.Text = "ADD";
            this.ButAdd.UseVisualStyleBackColor = true;
            this.ButAdd.Click += new System.EventHandler(this.ButAdd_Click);
            // 
            // tblDeviceInfo
            // 
            this.tblDeviceInfo.BackColor = System.Drawing.Color.Transparent;
            this.tblDeviceInfo.ColumnCount = 1;
            this.tblDeviceInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDeviceInfo.Controls.Add(this.BoxDIHWVersion, 0, 3);
            this.tblDeviceInfo.Controls.Add(this.LabDIHWVersion, 0, 2);
            this.tblDeviceInfo.Controls.Add(this.LabDIAddress, 0, 0);
            this.tblDeviceInfo.Controls.Add(this.BoxDIDeviceAddress, 0, 1);
            this.tblDeviceInfo.Controls.Add(this.BoxDIFWVariant, 0, 7);
            this.tblDeviceInfo.Controls.Add(this.LabDIFWVariant, 0, 6);
            this.tblDeviceInfo.Controls.Add(this.LabDIFWVersion, 0, 4);
            this.tblDeviceInfo.Controls.Add(this.BoxDIFWVersion, 0, 5);
            this.tblDeviceInfo.Location = new System.Drawing.Point(12, 1);
            this.tblDeviceInfo.Name = "tblDeviceInfo";
            this.tblDeviceInfo.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.tblDeviceInfo.RowCount = 8;
            this.tblDeviceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblDeviceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblDeviceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblDeviceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblDeviceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblDeviceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblDeviceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblDeviceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblDeviceInfo.Size = new System.Drawing.Size(214, 188);
            this.tblDeviceInfo.TabIndex = 11;
            // 
            // BoxDIHWVersion
            // 
            this.BoxDIHWVersion.Location = new System.Drawing.Point(6, 72);
            this.BoxDIHWVersion.Name = "BoxDIHWVersion";
            this.BoxDIHWVersion.ReadOnly = true;
            this.BoxDIHWVersion.Size = new System.Drawing.Size(201, 20);
            this.BoxDIHWVersion.TabIndex = 1;
            // 
            // LabDIHWVersion
            // 
            this.LabDIHWVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LabDIHWVersion.AutoSize = true;
            this.LabDIHWVersion.Location = new System.Drawing.Point(6, 56);
            this.LabDIHWVersion.Name = "LabDIHWVersion";
            this.LabDIHWVersion.Size = new System.Drawing.Size(91, 13);
            this.LabDIHWVersion.TabIndex = 0;
            this.LabDIHWVersion.Text = "Hardware Version";
            // 
            // LabDIAddress
            // 
            this.LabDIAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LabDIAddress.AutoSize = true;
            this.LabDIAddress.Location = new System.Drawing.Point(6, 10);
            this.LabDIAddress.Name = "LabDIAddress";
            this.LabDIAddress.Size = new System.Drawing.Size(69, 13);
            this.LabDIAddress.TabIndex = 0;
            this.LabDIAddress.Text = "Chip Address";
            // 
            // BoxDIDeviceAddress
            // 
            this.BoxDIDeviceAddress.Location = new System.Drawing.Point(6, 26);
            this.BoxDIDeviceAddress.Name = "BoxDIDeviceAddress";
            this.BoxDIDeviceAddress.Size = new System.Drawing.Size(201, 20);
            this.BoxDIDeviceAddress.TabIndex = 1;
            this.BoxDIDeviceAddress.Text = "01";
            this.BoxDIDeviceAddress.TextChanged += new System.EventHandler(this.BoxDIDeviceAddress_TextChanged);
            // 
            // BoxDIFWVariant
            // 
            this.BoxDIFWVariant.Location = new System.Drawing.Point(6, 164);
            this.BoxDIFWVariant.Name = "BoxDIFWVariant";
            this.BoxDIFWVariant.ReadOnly = true;
            this.BoxDIFWVariant.Size = new System.Drawing.Size(201, 20);
            this.BoxDIFWVariant.TabIndex = 1;
            // 
            // LabDIFWVariant
            // 
            this.LabDIFWVariant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LabDIFWVariant.AutoSize = true;
            this.LabDIFWVariant.Location = new System.Drawing.Point(6, 148);
            this.LabDIFWVariant.Name = "LabDIFWVariant";
            this.LabDIFWVariant.Size = new System.Drawing.Size(85, 13);
            this.LabDIFWVariant.TabIndex = 0;
            this.LabDIFWVariant.Text = "Firmware Variant";
            // 
            // LabDIFWVersion
            // 
            this.LabDIFWVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LabDIFWVersion.AutoSize = true;
            this.LabDIFWVersion.Location = new System.Drawing.Point(6, 102);
            this.LabDIFWVersion.Name = "LabDIFWVersion";
            this.LabDIFWVersion.Size = new System.Drawing.Size(87, 13);
            this.LabDIFWVersion.TabIndex = 0;
            this.LabDIFWVersion.Text = "Firmware Version";
            // 
            // BoxDIFWVersion
            // 
            this.BoxDIFWVersion.Location = new System.Drawing.Point(6, 118);
            this.BoxDIFWVersion.Name = "BoxDIFWVersion";
            this.BoxDIFWVersion.ReadOnly = true;
            this.BoxDIFWVersion.Size = new System.Drawing.Size(201, 20);
            this.BoxDIFWVersion.TabIndex = 1;
            // 
            // ButCancel
            // 
            this.ButCancel.Location = new System.Drawing.Point(153, 195);
            this.ButCancel.Name = "ButCancel";
            this.ButCancel.Size = new System.Drawing.Size(66, 23);
            this.ButCancel.TabIndex = 1;
            this.ButCancel.Text = "CANCEL";
            this.ButCancel.UseVisualStyleBackColor = true;
            this.ButCancel.Click += new System.EventHandler(this.ButCancel_Click);
            // 
            // LabStatus
            // 
            this.LabStatus.AutoSize = true;
            this.LabStatus.Location = new System.Drawing.Point(15, 199);
            this.LabStatus.Name = "LabStatus";
            this.LabStatus.Size = new System.Drawing.Size(50, 13);
            this.LabStatus.TabIndex = 12;
            this.LabStatus.Text = "STATUS";
            // 
            // AddDevice
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(228, 220);
            this.ControlBox = false;
            this.Controls.Add(this.LabStatus);
            this.Controls.Add(this.tblDeviceInfo);
            this.Controls.Add(this.ButCancel);
            this.Controls.Add(this.ButAdd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AddDevice";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Device";
            this.tblDeviceInfo.ResumeLayout(false);
            this.tblDeviceInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButAdd;
        private System.Windows.Forms.TableLayoutPanel tblDeviceInfo;
        private System.Windows.Forms.TextBox BoxDIHWVersion;
        private System.Windows.Forms.Label LabDIHWVersion;
        private System.Windows.Forms.Label LabDIAddress;
        private System.Windows.Forms.TextBox BoxDIDeviceAddress;
        private System.Windows.Forms.TextBox BoxDIFWVariant;
        private System.Windows.Forms.Label LabDIFWVariant;
        private System.Windows.Forms.Label LabDIFWVersion;
        private System.Windows.Forms.TextBox BoxDIFWVersion;
        private System.Windows.Forms.Button ButCancel;
        private System.Windows.Forms.Label LabStatus;
    }
}