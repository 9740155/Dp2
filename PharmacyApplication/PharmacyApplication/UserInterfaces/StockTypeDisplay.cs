using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyApplication.UserInterfaces
{
    class StockTypeDisplay : Form
    {
        private TextBox _IDLabel = new TextBox();
        private TextBox _IDOutput = new TextBox();

        private TextBox _NameLabel = new TextBox();
        private TextBox _NameOutput = new TextBox();

        private TextBox _LevelLabel = new TextBox();
        private TextBox _LevelOutput = new TextBox();

        private int _indexOfStockType = -1;
        private string _workbook;
        private string _table;

        private const int SMALLGAP = 10;

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
            _IDLabel.Show();

            this.Controls.Add(_NameLabel);
            _NameLabel.Text = "Name:";
            _NameLabel.Left = SMALLGAP;
            _NameLabel.Top = _IDLabel.Bottom + SMALLGAP;
            _NameLabel.Show();

            this.Controls.Add(_LevelLabel);
            _LevelLabel.Text = "Level:";
            _LevelLabel.Left = SMALLGAP;
            _LevelLabel.Top = _NameLabel.Bottom + SMALLGAP;
            _LevelLabel.Show();

            //Initialise buttons
            Button decrementMagnitude = new Button();
            this.Controls.Add(decrementMagnitude);
            decrementMagnitude.Text = "<<";
            decrementMagnitude.Left = SMALLGAP;
            decrementMagnitude.Top = _LevelLabel.Bottom + SMALLGAP;
            decrementMagnitude.Width = 25;
            decrementMagnitude.Click += DecrementMagnitude_Click;

            Button decrement = new Button();
            this.Controls.Add(decrement);
            decrement.Text = "<";
            decrement.Left = decrementMagnitude.Right + SMALLGAP;
            decrement.Top = decrementMagnitude.Top;
            decrement.Width = 25;
            decrement.Click += Decrement_Click;

            Button increment = new Button();
            this.Controls.Add(increment);
            increment.Text = ">";
            increment.Left = decrement.Right + SMALLGAP;
            increment.Top = decrementMagnitude.Top;
            increment.Width = 25;
            increment.Click += Increment_Click;            

            Button incrementMagnitude = new Button();
            this.Controls.Add(incrementMagnitude);
            incrementMagnitude.Text = ">>";
            incrementMagnitude.Left = increment.Right + SMALLGAP;
            incrementMagnitude.Top = decrementMagnitude.Top;
            incrementMagnitude.Width = 25;
            incrementMagnitude.Click += IncrementMagnitude_Click;//*/

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

        public void UpdateOutputs()
        {
            //TODO, from Jed read this from the Database using _workbook, _table, _indexOfStockType;
            StockType toDisplay = new StockType(_indexOfStockType, "Sunnies", 32);

            Console.WriteLine("Output");

            _IDOutput.Text = toDisplay.ID.ToString();
            _NameOutput.Text = toDisplay.Name;
            _LevelOutput.Text = toDisplay.Level.ToString();
        }

        public bool UpdateIndex(int index)
        {
            bool result = false;

            //TODO, from Jed add check for end of file aswell
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
                this.UpdateOutputs();
            }

            return result;
        }
    }
}
