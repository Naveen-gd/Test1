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

namespace MemLib
{

	public partial class MemForm : Form
	{
        private enum ColumnIndexes : int
        {
            NUMBER = 0,
            AREA = 1,
            NAME = 2,
            ADDR = 3,
            BF = 4,
            DATA = 5,
            RO = 6,
            MOD = 7,
            DESC = 8
        }

        private List<Memory> _memories;
        
        private void _SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            //Suppose your interested column has index 1
            if (e.Column.Index == Convert.ToInt32(ColumnIndexes.NUMBER))
            {
                e.SortResult = int.Parse(e.CellValue1.ToString()).CompareTo(int.Parse(e.CellValue2.ToString()));
                e.Handled = true;//pass by the default sorting
            }
        }

        private void _GenerateFromMemory()
        {
            String addrStr;
            String dataStr;
            String[] rowStrs;
            uint data;

            uint number = 0;

            dataGridViewMem.Rows.Clear();

            for (int m = 0; m < _memories.Count; m += 1)
            {
                Memory memory = _memories[m];
                for (int r = 0; r < memory.Count; r += 1)
                {
                    MemLocation memLoc = memory.ElementAt(r).Value;
                    addrStr = String.Format("0x{0:X3}", memLoc.addr);

                    // always add mem location itself
                    data = memLoc.data;
                    dataStr = data.ToHexString(memory.data_bits);

                    bool read_only = memory.readOnly || memLoc.readOnly;

                    rowStrs = new String[Enum.GetNames(typeof(ColumnIndexes)).Length];
                    rowStrs[Convert.ToInt32(ColumnIndexes.NUMBER)] = String.Format("{0:D}", number);
                    rowStrs[Convert.ToInt32(ColumnIndexes.AREA)] = memory.area;
                    rowStrs[Convert.ToInt32(ColumnIndexes.NAME)] = memLoc.name;
                    rowStrs[Convert.ToInt32(ColumnIndexes.BF)] = "";
                    rowStrs[Convert.ToInt32(ColumnIndexes.ADDR)] = addrStr;
                    rowStrs[Convert.ToInt32(ColumnIndexes.DATA)] = dataStr;
                    rowStrs[Convert.ToInt32(ColumnIndexes.DESC)] = memory.Description(memLoc);
                    if (memLoc.modified) rowStrs[Convert.ToInt32(ColumnIndexes.MOD)] = "*";
                    else rowStrs[Convert.ToInt32(ColumnIndexes.MOD)] = "";
                    if (read_only) rowStrs[Convert.ToInt32(ColumnIndexes.RO)] = "x";
                    else rowStrs[Convert.ToInt32(ColumnIndexes.RO)] = "";

                    int index = dataGridViewMem.Rows.Add(rowStrs);
                    dataGridViewMem.Rows[index].ReadOnly = read_only;
                    number += 1;

                    // add optional bitfields
                    List<MemBitfield> bitfields = memLoc.GetAllBitfields();
                    if (bitfields.Count > 0)
                    {
                        for (int f = 0; f < bitfields.Count; f += 1)
                        {
                            data = bitfields[f].GetData();
                            dataStr = data.ToHexString(bitfields[f].bits);

                            rowStrs[Convert.ToInt32(ColumnIndexes.NUMBER)] = String.Format("{0:D}", number);
                            rowStrs[Convert.ToInt32(ColumnIndexes.BF)] = bitfields[f].name;
                            rowStrs[Convert.ToInt32(ColumnIndexes.DATA)] = dataStr;
                            rowStrs[Convert.ToInt32(ColumnIndexes.DESC)] = memory.Description(bitfields[f]);

                            index = dataGridViewMem.Rows.Add(rowStrs);
                            dataGridViewMem.Rows[index].ReadOnly = read_only;
                            number += 1;
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
            if ((cell != null) && (cell.ColumnIndex == Convert.ToInt32(ColumnIndexes.DATA)))
            {
                String areaStr = dataGridViewMem.Rows[cell.RowIndex].Cells[Convert.ToInt32(ColumnIndexes.AREA)].Value.ToString();
                String nameStr = dataGridViewMem.Rows[cell.RowIndex].Cells[Convert.ToInt32(ColumnIndexes.NAME)].Value.ToString();
                ushort addr = Convert.ToUInt16(dataGridViewMem.Rows[cell.RowIndex].Cells[Convert.ToInt32(ColumnIndexes.ADDR)].Value.ToString(), 16);
                Memory memory = _memories.FirstOrDefault(x => x.area == areaStr);
                MemLocation memLoc = memory.FirstOrDefault(x => x.Value.addr == addr).Value;

                try
                {
                    uint newData = newValue.ParseAsUInt();
                    String bitfieldStr = dataGridViewMem.Rows[cell.RowIndex].Cells[Convert.ToInt32(ColumnIndexes.BF)].Value.ToString();
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
                catch
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

            dataGridViewMem.DoubleBuffered();

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
                String areaStr = dataGridViewMem.Rows[r].Cells[Convert.ToInt32(ColumnIndexes.AREA)].Value.ToString();
                String nameStr = dataGridViewMem.Rows[r].Cells[Convert.ToInt32(ColumnIndexes.NAME)].Value.ToString();
                String bitfieldStr = dataGridViewMem.Rows[r].Cells[Convert.ToInt32(ColumnIndexes.BF)].Value.ToString();
                ushort addr = Convert.ToUInt16(dataGridViewMem.Rows[r].Cells[Convert.ToInt32(ColumnIndexes.ADDR)].Value.ToString(), 16);

                Memory memory = _memories.FirstOrDefault(x => x.area == areaStr);
                MemLocation memLoc = memory.FirstOrDefault(x => x.Value.addr == addr).Value;

                String dataStr = memLoc.data.ToHexString(memory.data_bits);
                String descStr = memory.Description(memLoc);

                if (bitfieldStr != "")
                {
                    MemBitfield memBf = memLoc.GetBitfield(bitfieldStr);
                    dataStr = memBf.GetData().ToHexString(memBf.bits);
                    descStr = memory.Description(memBf);
                }

                dataGridViewMem.Rows[r].Cells[Convert.ToInt32(ColumnIndexes.DATA)].Value = dataStr;
                dataGridViewMem.Rows[r].Cells[Convert.ToInt32(ColumnIndexes.DESC)].Value = descStr;

                if (memLoc.modified) dataGridViewMem.Rows[r].Cells[Convert.ToInt32(ColumnIndexes.MOD)].Value = "*";
                else dataGridViewMem.Rows[r].Cells[Convert.ToInt32(ColumnIndexes.MOD)].Value = "";
            }
        }

    }
}
