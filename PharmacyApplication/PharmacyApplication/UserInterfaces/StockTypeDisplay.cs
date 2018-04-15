using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyApplication.UserInterfaces
{
    //Authors: Jed
    class StockTypeDisplay : Form
    {
        private Label _IDLabel = new Label();
        private TextBox _IDOutput = new TextBox();

        private Label _NameLabel = new Label();
        private TextBox _NameOutput = new TextBox();

        private Label _LevelLabel = new Label();
        private TextBox _LevelOutput = new TextBox();

        private int _indexOfStockType = -1;
        private string _workbook;
        private string _table;

        
        private const int SMALLGAP = 10;
        private const int LABELWIDTH = 50;
        private const int BUTTONSIZE = 25;

        public StockTypeDisplay(string workbook, string table, int index)
        {
            _workbook = workbook;
            _table = table;
            this.UpdateIndex(index);

            //Initialise labels
            this.Controls.Add(_IDLabel);
            _IDLabel.Text = "ID:";
            _IDLabel.Left = SMALLGAP;
            _IDLabel.Top = SMALLGAP;
            _IDLabel.Width = LABELWIDTH;
            _IDLabel.Show();

            this.Controls.Add(_NameLabel);
            _NameLabel.Text = "Name:";
            _NameLabel.Left = SMALLGAP;
            _NameLabel.Top = _IDLabel.Bottom + SMALLGAP;
            _NameLabel.Width = LABELWIDTH;
            _NameLabel.Show();

            this.Controls.Add(_LevelLabel);
            _LevelLabel.Text = "Level:";
            _LevelLabel.Left = SMALLGAP;
            _LevelLabel.Top = _NameLabel.Bottom + SMALLGAP;
            _LevelLabel.Width = LABELWIDTH;
            _LevelLabel.Show();

            //Initialise buttons
            Button decrementMagnitude = new Button();
            this.Controls.Add(decrementMagnitude);
            decrementMagnitude.Text = "<<";
            decrementMagnitude.Width = BUTTONSIZE;
            decrementMagnitude.Height = BUTTONSIZE;
            decrementMagnitude.Left = SMALLGAP;
            decrementMagnitude.Top = _LevelLabel.Bottom + SMALLGAP;
            decrementMagnitude.Click += DecrementMagnitude_Click;

            Button decrement = new Button();
            this.Controls.Add(decrement);
            decrement.Text = "<";
            decrement.Width = BUTTONSIZE;
            decrement.Height = BUTTONSIZE;
            decrement.Left = decrementMagnitude.Right + SMALLGAP;
            decrement.Top = decrementMagnitude.Top;
            decrement.Click += Decrement_Click;

            Button increment = new Button();
            this.Controls.Add(increment);
            increment.Text = ">";
            increment.Width = BUTTONSIZE;
            increment.Height = BUTTONSIZE;
            increment.Left = decrement.Right + SMALLGAP;
            increment.Top = decrementMagnitude.Top;
            increment.Click += Increment_Click;            

            Button incrementMagnitude = new Button();
            this.Controls.Add(incrementMagnitude);
            incrementMagnitude.Text = ">>";
            incrementMagnitude.Width = BUTTONSIZE;
            incrementMagnitude.Height = BUTTONSIZE;
            incrementMagnitude.Left = increment.Right + SMALLGAP;
            incrementMagnitude.Top = decrementMagnitude.Top;
            incrementMagnitude.Click += IncrementMagnitude_Click;

            Button addStockType = new Button();
            this.Controls.Add(addStockType);
            addStockType.Text = "ADD";
            addStockType.Width = 2 * BUTTONSIZE;
            addStockType.Height = BUTTONSIZE;
            addStockType.Left = decrementMagnitude.Left;
            addStockType.Top = decrementMagnitude.Bottom + SMALLGAP;
            addStockType.Click += AddStockType_Click;//*/

            //Initialise outputs
            this.Controls.Add(_IDOutput);
            _IDOutput.Left = _IDLabel.Right + SMALLGAP;
            _IDOutput.Top = _IDLabel.Top;
            _IDOutput.Show();

            this.Controls.Add(_NameOutput);
            _NameOutput.Left = _NameLabel.Right + SMALLGAP;
            _NameOutput.Top = _NameLabel.Top;
            _NameOutput.Show();

            this.Controls.Add(_LevelOutput);
            _LevelOutput.Left = _LevelLabel.Right + SMALLGAP;
            _LevelOutput.Top = _LevelLabel.Top;
            _LevelOutput.Show();

            this.UpdateOutputs();
        }

        private void AddStockType_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DecrementMagnitude_Click(object sender, EventArgs e)
        {
            this.UpdateIndex(_indexOfStockType - 10);
        }

        private void IncrementMagnitude_Click(object sender, EventArgs e)
        {
            this.UpdateIndex(_indexOfStockType + 10);
        }

        private void Increment_Click(object sender, EventArgs e)
        {
            this.UpdateIndex(_indexOfStockType + 1);
        }

        private void Decrement_Click(object sender, EventArgs e)
        {
            this.UpdateIndex(_indexOfStockType - 1);
        }

        /// <summary>
        /// Reads value from database and writes the to the forms outputs
        /// </summary>
        public void UpdateOutputs()
        {
            //TODO, from Jed read this from the Database using _workbook, _table, _indexOfStockType;

            StockType toDisplay = Database.ReadStockType(_workbook, _table, _indexOfStockType);

            if(toDisplay == null)
            {
                //Failed to read
                throw new PAReadException("Failed to Read Stock Type");
            }

            _IDOutput.Text = toDisplay.ID.ToString();
            _NameOutput.Text = toDisplay.Name;
            _LevelOutput.Text = toDisplay.Level.ToString();
        }

        /// <summary>
        /// Checks index is inside of bounds and calls UpdateOutputs with new index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool UpdateIndex(int index)
        {
            bool result = false;

            //Used for refusing reads passed end of file
            int oldIndex = _indexOfStockType;

            if (index >= 0)
            {
                _indexOfStockType = index;
                result = true;
            }

            else
            {
                index = 0;
            }


            if(result)
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
    }
}
