namespace MemLib
{
	partial class MemForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewMem = new System.Windows.Forms.DataGridView();
            this.ColumnNr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnArea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnBitfield = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnRO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMem)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewMem
            // 
            this.dataGridViewMem.AllowUserToAddRows = false;
            this.dataGridViewMem.AllowUserToDeleteRows = false;
            this.dataGridViewMem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnNr,
            this.ColumnArea,
            this.ColumnName,
            this.ColumnAddress,
            this.ColumnBitfield,
            this.ColumnValue,
            this.ColumnRO,
            this.ColumnModified,
            this.ColumnDescription});
            this.dataGridViewMem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewMem.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewMem.Name = "dataGridViewMem";
            this.dataGridViewMem.RowHeadersWidth = 15;
            this.dataGridViewMem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewMem.Size = new System.Drawing.Size(1363, 630);
            this.dataGridViewMem.TabIndex = 1;
            this.dataGridViewMem.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this._DataGridViewMemCellEndEdit);
            this.dataGridViewMem.KeyUp += new System.Windows.Forms.KeyEventHandler(this._DataGridViewMemKeyUp);
            this.dataGridViewMem.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this._SortCompare);
            // 
            // ColumnNr
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = null;
            this.ColumnNr.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnNr.HeaderText = "#";
            this.ColumnNr.MinimumWidth = 40;
            this.ColumnNr.Name = "ColumnNr";
            this.ColumnNr.ReadOnly = true;
            this.ColumnNr.Width = 40;
            // 
            // ColumnArea
            // 
            this.ColumnArea.HeaderText = "Area";
            this.ColumnArea.MinimumWidth = 20;
            this.ColumnArea.Name = "ColumnArea";
            this.ColumnArea.ReadOnly = true;
            // 
            // ColumnName
            // 
            this.ColumnName.HeaderText = "Name";
            this.ColumnName.MaxInputLength = 50;
            this.ColumnName.MinimumWidth = 300;
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.ReadOnly = true;
            this.ColumnName.Width = 300;
            // 
            // ColumnAddress
            // 
            this.ColumnAddress.HeaderText = "Address";
            this.ColumnAddress.MaxInputLength = 10;
            this.ColumnAddress.MinimumWidth = 80;
            this.ColumnAddress.Name = "ColumnAddress";
            this.ColumnAddress.ReadOnly = true;
            this.ColumnAddress.Width = 80;
            // 
            // ColumnBitfield
            // 
            this.ColumnBitfield.HeaderText = "Bitfield";
            this.ColumnBitfield.MaxInputLength = 50;
            this.ColumnBitfield.MinimumWidth = 200;
            this.ColumnBitfield.Name = "ColumnBitfield";
            this.ColumnBitfield.ReadOnly = true;
            this.ColumnBitfield.Width = 200;
            // 
            // ColumnValue
            // 
            this.ColumnValue.HeaderText = "Value";
            this.ColumnValue.MaxInputLength = 10;
            this.ColumnValue.MinimumWidth = 100;
            this.ColumnValue.Name = "ColumnValue";
            // 
            // ColumnRO
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnRO.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnRO.HeaderText = "RO";
            this.ColumnRO.MinimumWidth = 40;
            this.ColumnRO.Name = "ColumnRO";
            this.ColumnRO.ReadOnly = true;
            this.ColumnRO.Width = 40;
            // 
            // ColumnModified
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnModified.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnModified.HeaderText = "M";
            this.ColumnModified.MaxInputLength = 1;
            this.ColumnModified.MinimumWidth = 40;
            this.ColumnModified.Name = "ColumnModified";
            this.ColumnModified.ReadOnly = true;
            this.ColumnModified.Width = 40;
            // 
            // ColumnDescription
            // 
            this.ColumnDescription.HeaderText = "Description";
            this.ColumnDescription.MinimumWidth = 500;
            this.ColumnDescription.Name = "ColumnDescription";
            this.ColumnDescription.ReadOnly = true;
            this.ColumnDescription.Width = 500;
            // 
            // MemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1363, 630);
            this.Controls.Add(this.dataGridViewMem);
            this.Name = "MemForm";
            this.Text = "MemForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this._MemForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMem)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.DataGridView dataGridViewMem;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNr;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnArea;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBitfield;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnModified;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescription;
	}
}