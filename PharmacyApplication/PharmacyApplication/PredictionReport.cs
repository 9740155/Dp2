using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApplication
{
    class PredictionReport : ReportType
    {

        public string[] _reportContents;

        public override bool SaveReport(string Workbook, string reportName, DateTime from, DateTime to, string workbooktable, string table, int idToSearch)
        {
            bool result = false;
            base.SaveReport(Workbook, reportName, from, to, workbooktable, table, idToSearch);

            string[] ReportContents = {
                    "--- Prediction Report ---",
                    "ID              : "  + idToSearch,
                    "From            : "  + from.ToLongDateString(),
                    "To              : "  + to.ToLongDateString(),
                    "Prediction Sold : " + Predictor.PredictLinear(workbooktable, table, from, to, idToSearch).ExpectedValue
 

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
