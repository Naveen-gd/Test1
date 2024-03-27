using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace Gui_Lib
{

	public partial class MemForm : Form
	{
        private const int _COLUMN_AREA = 0;
        private const int _COLUMN_NAME = 1;
        private const int _COLUMN_ADDR = 2;
        private const int _COLUMN_BF = 3;
        private const int _COLUMN_DATA = 4;
        private const int _COLUMN_MOD = 5;
        private const int _COLUMN_DESC = 6;

        private List<Memory> _memories;

        private void _GenerateFromMemory()
        {
            String addrStr;
            String dataStr;
            String[] rowStrs;
            uint data;

            dataGridViewMem.Rows.Clear();

            for (int m = 0; m < _memories.Count; m += 1)
            {
                Memory memory = _memories[m];
                for (int r = 0; r < memory.Count; r += 1)
                {
                    addrStr = String.Format("0x{0:X3}", memory.ElementAt(r).Value.addr);

                    // always add mem location itself
                    data = memory.ElementAt(r).Value.data;
                    dataStr = data.ToHexString(memory.data_bits);

                    rowStrs = new String[7];
                    rowStrs[_COLUMN_AREA] = memory.area;
                    rowStrs[_COLUMN_NAME] = memory.ElementAt(r).Value.name;
                    rowStrs[_COLUMN_BF] = "";
                    rowStrs[_COLUMN_ADDR] = addrStr;
                    rowStrs[_COLUMN_DATA] = dataStr;
                    rowStrs[_COLUMN_DESC] = memory.ElementAt(r).Value.Description();
                    if (memory.ElementAt(r).Value.modified) rowStrs[_COLUMN_MOD] = "*";
                    else rowStrs[_COLUMN_MOD] = "";
                    int index = dataGridViewMem.Rows.Add(rowStrs);
                    dataGridViewMem.Rows[index].ReadOnly = memory.readOnly || memory.ElementAt(r).Value.readOnly;

                    // add optional bitfields
                    List<MemBitfield> bitfields = memory.ElementAt(r).Value.GetAllBitfields();
                    if (bitfields.Count > 0)
                    {
                        for (int f = 0; f < bitfields.Count; f += 1)
                        {
                            data = bitfields[f].GetData();
                            dataStr = data.ToHexString(bitfields[f].bits);

                            rowStrs[_COLUMN_BF] = bitfields[f].name;
                            rowStrs[_COLUMN_DATA] = dataStr;
                            rowStrs[_COLUMN_DESC] = memory.Description(bitfields[f]);
                            index = dataGridViewMem.Rows.Add(rowStrs);
                            dataGridViewMem.Rows[index].ReadOnly = memory.readOnly || memory.ElementAt(r).Value.readOnly;
                        }
                    }
                }
            }
        }

        private void _MemForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.Hide();
			e.Cancel = true;
		}

        private void _UpdateCell(DataGridViewCell cell, string newValue)
        {
            if ((cell != null) && (cell.ColumnIndex == _COLUMN_DATA))
            {
                String areaStr = dataGridViewMem.Rows[cell.RowIndex].Cells[_COLUMN_AREA].Value.ToString();
                String nameStr = dataGridViewMem.Rows[cell.RowIndex].Cells[_COLUMN_NAME].Value.ToString();
                ushort addr = Convert.ToUInt16(dataGridViewMem.Rows[cell.RowIndex].Cells[_COLUMN_ADDR].Value.ToString(), 16);
                Memory memory = _memories.FirstOrDefault(x => x.area == areaStr);
                MemLocation memLoc = memory.FirstOrDefault(x => x.Value.addr == addr).Value;

                try
                {
                    uint newData = newValue.ParseAsUInt();
                    String bitfieldStr = dataGridViewMem.Rows[cell.RowIndex].Cells[_COLUMN_BF].Value.ToString();
                    if (bitfieldStr != "")
                    {
                        MemBitfield memBf = memLoc.GetBitfield(bitfieldStr);
                        if ((newData != memBf.GetData()) || memLoc.writeOnly)
                            memBf.SetDataSetModified(newData);
                    }
                    else
                    {
                        if ((newData != memLoc.data) || memLoc.writeOnly)
                            memLoc.SetDataSetModified(newData);
                    }
                }
                catch (Exception x)
                {
                }
            }
        }

        private void _DataGridViewMemCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = ((DataGridView)sender).CurrentCell;
            _UpdateCell(cell, cell.Value.ToString());
            UpdateFromMemory();
        }

        private void _DataGridViewMemKeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.KeyCode == Keys.V))     // Paste
            {
                //get the text from clipboard
                IDataObject dataInClipboard = Clipboard.GetDataObject();
                string stringInClipboard = (string)dataInClipboard.GetData(DataFormats.Text);

                // paste into selected cells
                DataGridViewSelectedCellCollection selCells = dataGridViewMem.SelectedCells;
                foreach (DataGridViewCell c in selCells)
                {
                    _UpdateCell(c, stringInClipboard);
                }
            }
            UpdateFromMemory();
        }

        public MemForm(String name, List<Memory> memories)
        {
            InitializeComponent();

            dataGridViewMem.DoubleBuffered(true);

            Text = name;
            _memories = memories;

            _GenerateFromMemory();
        }

        public void UpdateFromMemory(List<Memory> memories = null)
        {
            if (memories != null)
            {
                _memories = memories;
            }

            for (int r = 0; r < dataGridViewMem.Rows.Count; r += 1)
            {
                String areaStr = dataGridViewMem.Rows[r].Cells[_COLUMN_AREA].Value.ToString();
                String nameStr = dataGridViewMem.Rows[r].Cells[_COLUMN_NAME].Value.ToString();
                String bitfieldStr = dataGridViewMem.Rows[r].Cells[_COLUMN_BF].Value.ToString();
                ushort addr = Convert.ToUInt16(dataGridViewMem.Rows[r].Cells[_COLUMN_ADDR].Value.ToString(), 16);

                Memory memory = _memories.FirstOrDefault(x => x.area == areaStr);
                MemLocation memLoc = memory.FirstOrDefault(x => x.Value.addr == addr).Value;

                String dataStr = memLoc.data.ToHexString(memory.data_bits);
                String descStr = memLoc.Description();

                if (bitfieldStr != "")
                {
                    MemBitfield memBf = memLoc.GetBitfield(bitfieldStr);
                    dataStr = memBf.GetData().ToHexString(memBf.bits);
                    descStr = memory.Description(memBf);
                }

                dataGridViewMem.Rows[r].Cells[_COLUMN_DATA].Value = dataStr;
                dataGridViewMem.Rows[r].Cells[_COLUMN_DESC].Value = descStr;

                if (memLoc.modified) dataGridViewMem.Rows[r].Cells[_COLUMN_MOD].Value = "*";
                else dataGridViewMem.Rows[r].Cells[_COLUMN_MOD].Value = "";
            }
        }

    }
}
