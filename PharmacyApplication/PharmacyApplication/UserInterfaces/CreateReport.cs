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
    class CreateReport : Form
    {       
        private const int SMALLGAP = 10;
        private const int LABELWIDTH = 50;
        private const int BUTTONSIZE = 50;

        private TextBox _ids;
        private TextBox _start, _end;

        public CreateReport()
        {
            Label l = new Label();
            l.Width = BUTTONSIZE;
            l.Height = BUTTONSIZE;
            l.Top = SMALLGAP;
            l.Left = SMALLGAP;
            l.Text = "IDs:";
            this.Controls.Add(l);

            TextBox tb = new TextBox();
            tb.Top = l.Top;
            tb.Left = l.Right + SMALLGAP;
            tb.Text = "1,5";
            _ids = tb;
            this.Controls.Add(tb);

            l = new Label();
            l.Width = BUTTONSIZE;
            l.Height = BUTTONSIZE;
            l.Top = 2 * SMALLGAP + BUTTONSIZE;
            l.Left = SMALLGAP;
            l.Text = "Dates:";
            this.Controls.Add(l);

            tb = new TextBox();
            tb.Top = l.Top;
            tb.Left = l.Right + SMALLGAP;
            tb.Text = "01-May-18 12:00:00 AM";
            _start = tb;
            this.Controls.Add(tb);

            tb = new TextBox();
            tb.Top = l.Top;
            tb.Left = _start.Right + SMALLGAP;
            tb.Text = "27-May-18 12:00:00 AM";
            _end = tb;
            this.Controls.Add(tb);

            //Initialise buttons
            Button temp = new Button();
            temp.Text = "Create Report";
            temp.Width = BUTTONSIZE;
            temp.Height = BUTTONSIZE;
            temp.Top = l.Bottom + SMALLGAP;
            temp.Left = SMALLGAP;
            temp.Click += Generate_Click;
            this.Controls.Add(temp);
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            List<int> read = new List<int>();

            foreach (string s in _ids.Text.Split(','))
            {
                read.Add(int.Parse(s));
            }

            PredictionReport PR = new PredictionReport();
            foreach (int i in read)
            {
                PR.SaveReport("Demo", "salesPred", DateTime.Parse(_start.Text), DateTime.Parse(_end.Text), "Demo", "sales", i);
            }
        }
    }
}
