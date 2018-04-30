using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyApplication
{
    class ReportTypeDisplay : Form

    { 
        private Label _Instructions = new Label();
        private Label _SearchReportLabel = new Label();
        private TextBox _SearchReportInput = new TextBox();
        private  Button _SalesForecastButton = new Button();


        private string _workbook;
        private string _report;

        //Constants
        private const int SMALLGAP = 10;
        private const int LABELWIDTH = 50;
        private const int BUTTONSIZE = 25;
        private const int FORMWIDTH = 800;
        private const int FORMHEIGHT = 600;


        
        public ReportTypeDisplay(string workbook, string report)
        {
            _workbook = workbook;
            _report = report;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.Width = FORMWIDTH;
            this.Height = FORMHEIGHT;


            //Main Instuctions
            this.Controls.Add(_Instructions);
            _Instructions.Text = "Enter the report name you would like to view below";
            _Instructions.Width = 150;
            _Instructions.Height = 60;
            _Instructions.Left = SMALLGAP;
            _Instructions.Top = 100;
            _Instructions.Show();

            //Existing Reports
            //label
            this.Controls.Add(_SearchReportLabel);
            _SearchReportLabel.Text      = "Report Name: ";
            _SearchReportLabel.Left      = SMALLGAP;
            _SearchReportLabel.Top       = 160;
            _SearchReportLabel.Width     = LABELWIDTH;
            _SearchReportLabel.ForeColor = System.Drawing.Color.DimGray;
            _SearchReportLabel.Show();
            //Textinput
            this.Controls.Add(_SearchReportInput);
            _SearchReportInput.BackColor = System.Drawing.Color.DimGray;
            _SearchReportInput.Left = _SearchReportLabel.Left + _SearchReportLabel.Width + SMALLGAP;
            _SearchReportInput.Top = _SearchReportLabel.Top;
            _SearchReportInput.Width = 100;
            _SearchReportInput.Show();




            //Buttons
            this.Controls.Add(_SalesForecastButton);
            _SalesForecastButton.Top = 100;
            _SalesForecastButton.Left = 600;
            _SalesForecastButton.Width = 100;
            _SalesForecastButton.BackColor = System.Drawing.Color.Snow;
            _SalesForecastButton.Text = "Sales Report";
            _SalesForecastButton.Show();

    
        }



    }
}
