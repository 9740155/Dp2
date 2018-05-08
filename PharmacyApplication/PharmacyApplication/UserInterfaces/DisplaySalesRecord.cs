using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyApplication.UserInterfaces
{
    public partial class DisplaySalesRecord : Form
    {
        private string _workbook;
        private string _table;
        private int _indexOfStockType;
        private const int READINDEXOFFSET = 0;
        public DisplaySalesRecord(string workbook, string table, int index)
        {
            InitializeComponent();
            _workbook = workbook;
            _table = table;
            _indexOfStockType = index;

            this.UpdateOutputs();
        }

        public void UpdateOutputs()
        {
            SalesRecord toDisplay = Database.ReadSalesRecord(_workbook, _table, _indexOfStockType);

            if (toDisplay == null)
            {
                //Failed to read
                throw new PAReadException("Failed to Read Stock Type");
            }

            lbl_id_output.Text = toDisplay.ID.ToString();
            lbl_name_output.Text = toDisplay.Name;
            lbl_stock_output.Text = toDisplay.Quantity.ToString();
            lbl_index.Text = (_indexOfStockType + 1).ToString();
            label1.Text = toDisplay.DateOfSale.ToString();
        }

        private void btn_increase_Click(object sender, EventArgs e)
        {
            this.UpdateIndex(_indexOfStockType + 1);
        }

        private void btn_decrease_Click(object sender, EventArgs e)
        {
            this.UpdateIndex(_indexOfStockType - 1);
        }

        public bool UpdateIndex(int index)
        {
            bool result = false;

            //Used for refusing reads passed end of file
            int oldIndex = _indexOfStockType;

            int toFind = 0 + READINDEXOFFSET;

            if (index >= toFind)
            {
                _indexOfStockType = index;
                result = true;
            }

            else
            {
                index = toFind;
            }

            if (result)
            {
                try
                {
                    this.UpdateOutputs();

                    result = true;
                }

                catch (EndOfStreamException e)
                {
                    //Index is out of bounds
                    _indexOfStockType = oldIndex;

                    this.UpdateOutputs();

                    result = false;
                }
            }

            return result;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
