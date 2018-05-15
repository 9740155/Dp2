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
    class HomePage : Form
    {       
        private const int SMALLGAP = 10;
        private const int LABELWIDTH = 50;
        private const int BUTTONSIZE = 50;

        public HomePage()
        {
            //Initialise buttons
            Button temp = new Button();
            temp.Text = "Stock";
            temp.Width = BUTTONSIZE;
            temp.Height = BUTTONSIZE;
            temp.Top = SMALLGAP;
            temp.Left = SMALLGAP;
            temp.Click += Stock_Click;
            this.Controls.Add(temp);

            temp = new Button();
            temp.Text = "Sales";
            temp.Width = BUTTONSIZE;
            temp.Height = BUTTONSIZE;
            temp.Top = SMALLGAP;
            temp.Left = BUTTONSIZE + 2 * SMALLGAP;
            temp.Click += Sales_Click; ;
            this.Controls.Add(temp);

            temp = new Button();
            temp.Text = "Report";
            temp.Width = 2 * BUTTONSIZE;
            temp.Height = BUTTONSIZE;
            temp.Top = BUTTONSIZE + 2 * SMALLGAP;
            temp.Left = SMALLGAP;
            temp.Click += Report_Click; ;
            this.Controls.Add(temp);
        }

        private void Report_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new CreateReport()).ShowDialog();
            this.Show();
        }

        private void Sales_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new DisplaySalesRecord("Demo", "sales", 0)).ShowDialog();
            this.Show();
        }

        private void Stock_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new StockTypeDisplay("Demo", "stock", 0)).ShowDialog();
            this.Show();
        }
    }
}
