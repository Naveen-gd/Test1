using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELMOS_521._38_UART_Eval
{
    
    public class LEDStatusDataTable : System.Data.DataTable
    {
        public LEDStatusDataTable()
        {
            System.Data.DataColumn column = new System.Data.DataColumn();
            column.DataType = System.Type.GetType("System.Single");
            column.ColumnName = "VLED [V]";
            this.Columns.Add(column);

            column = new System.Data.DataColumn();
            column.DataType = System.Type.GetType("System.Boolean");
            column.ColumnName = "vled_valid";
            this.Columns.Add(column);

            column = new System.Data.DataColumn();
            column.DataType = System.Type.GetType("System.Single");
            column.ColumnName = "VDIF [V]";
            this.Columns.Add(column);

            column = new System.Data.DataColumn();
            column.DataType = System.Type.GetType("System.Boolean");
            column.ColumnName = "vdif_valid";
            this.Columns.Add(column);

            column = new System.Data.DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Channel";
            this.Columns.Add(column);

            column = new System.Data.DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Status";
            this.Columns.Add(column);

            System.Data.DataRow row;
            for (int i = 0; i < Properties.Settings.Default.Channels; i++)
            {
                row = this.NewRow();
                row["VLED [V]"] = 0.0;
                row["vled_valid"] = false;
                row["VDIF [V]"] = 0.0;
                row["vdif_valid"] = false;
                row["Channel"] = i;
                row["Status"] = "n/a";
                this.Rows.Add(row);
            }

        }

        public System.Data.DataRow this[int index]
        {
            get => this.Rows[index];
        }

    }
}
