using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApplication
{
    class StockReport : ReportType
    {



        public string[] _reportContents;




        public override bool SaveReport(string Workbook, string reportName, DateTime from, DateTime to, string workbooktable, string table, int idToSearch)
        {
            return  base.SaveReport(Workbook, reportName, from, to, workbooktable, table, idToSearch);
        }

        public bool SaveReport(string Workbook, string reportName, string workbooktable, string table, int idToSearch)
        {
            bool result = false;

            DateTime to = new DateTime();
            DateTime from = new DateTime();
            SaveReport(Workbook, reportName, from, to, workbooktable, table, idToSearch);
           
            StockType stockRecord = Database.ReadStockType(workbooktable, table, idToSearch);





            string alertstatus;

            if (stockRecord.isLow())
            {
                alertstatus = "Low";
            }else
            {
                alertstatus = "Acceptable";
            }

            string[] ReportContents = {
                    "--- Stock Report ---",
                    "ID           : " + stockRecord.ID,
                    "Name         : " + stockRecord.Name,
                    "Level        : " + stockRecord.Level,
                    "Alert Status : " + alertstatus

            };

            _reportContents = ReportContents;

            if (WriteRecord(ReportContents))
            {
                result = true;
            }

            return result;
        }
    }
}
