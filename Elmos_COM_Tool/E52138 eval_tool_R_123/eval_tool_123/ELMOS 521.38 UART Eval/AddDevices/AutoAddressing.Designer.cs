namespace ELMOS_521._38_UART_Eval
{
    partial class AutoAddressing
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
            if (aaMaster != null)
            {
                aaMaster.Dispose();
                aaMaster = null;
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
            this.ButCancel = new System.Windows.Forms.Button();
            this.LabStatus = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirmwareVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirmwareVariant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ButStartAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ButCancel
            // 
            this.ButCancel.Location = new System.Drawing.Point(269, 288);
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
            this.LabStatus.Location = new System.Drawing.Point(17, 292);
            this.LabStatus.Name = "LabStatus";
            this.LabStatus.Size = new System.Drawing.Size(50, 13);
            this.LabStatus.TabIndex = 12;
            this.LabStatus.Text = "STATUS";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Address,
            this.FirmwareVersion,
            this.FirmwareVariant});
            this.dataGridView1.Location = new System.Drawing.Point(13, 13);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(322, 269);
            this.dataGridView1.TabIndex = 13;
            // 
            // Address
            // 
            this.Address.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Address.HeaderText = "Address";
            this.Address.Name = "Address";
            this.Address.ReadOnly = true;
            this.Address.Width = 70;
            // 
            // FirmwareVersion
            // 
            this.FirmwareVersion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FirmwareVersion.HeaderText = "FW Version";
            this.FirmwareVersion.Name = "FirmwareVersion";
            this.FirmwareVersion.ReadOnly = true;
            // 
            // FirmwareVariant
            // 
            this.FirmwareVariant.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FirmwareVariant.HeaderText = "FW Variant";
            this.FirmwareVariant.Name = "FirmwareVariant";
            this.FirmwareVariant.ReadOnly = true;
            // 
            // ButStartAdd
            // 
            this.ButStartAdd.Location = new System.Drawing.Point(197, 288);
            this.ButStartAdd.Name = "ButStartAdd";
            this.ButStartAdd.Size = new System.Drawing.Size(66, 23);
            this.ButStartAdd.TabIndex = 1;
            this.ButStartAdd.Text = "ADD";
            this.ButStartAdd.UseVisualStyleBackColor = true;
            this.ButStartAdd.Click += new System.EventHandler(this.ButStartAdd_Click);
            // 
            // AutoAddressing
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(346, 319);
            this.ControlBox = false;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.LabStatus);
            this.Controls.Add(this.ButStartAdd);
            this.Controls.Add(this.ButCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AutoAddressing";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auto Addressing";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ButCancel;
        private System.Windows.Forms.Label LabStatus;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirmwareVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirmwareVariant;
        private System.Windows.Forms.Button ButStartAdd;
    }
}