using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApplication
{
    //Base Class for all Report Types
     public abstract class ReportType
    {
        //Author: Ronan
        //Reusing Database.cs Code. reports will need their own file as dicussed previously
        //WARNING: This helps the database find files its already created, changing this will essentially remove access for the database to existing files.
        public static string ROOTDRECTORY
        {
            get
            {
                return "./Root";
            }
        }
        public static string _fullReportName;
        public static string DEFAULTEXTENSION
        {
            get
            {
                return ".csv";
            }
        }
        //Ronan
        //A placeholder for child classes to tmp store data.
        public DBReadable infoContainer
        {
            get
            {
                return infoContainer;
            }
            set
            {
                infoContainer = value;
            }
        }

        //Author: Ronan : Reworked Database.cs 
        //Params (workbook="1997", reportName = "Stock Predicitions", )
        /// <summary>
        /// Creates a new report folder if one dosn't already exist in the specified workbook with the specified name. Allows reports
        ///  to be run multiple times per day. Returns true if new report was created.
        /// </summary>
        /// <param name="Workbook">The workbook to be accessed or created</param>
        /// <param name="reportName">The table in the workbook to be created</param>

        /// <returns></returns>
        public virtual bool SaveReport(string Workbook, string reportName, DateTime from, DateTime to, string workbooktable, string table, int idToSearch)
        {
            bool result = false;

            string dir = ReportType.ROOTDRECTORY + "/" + Workbook;
            DateTime today = DateTime.Now;
            string dateString = today.ToShortDateString().Replace("/","");
            string timeString = today.ToShortTimeString().Replace(":", "").Replace(" PM", "").Replace(" AM", "");
            //Check Workbook exists
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            _fullReportName = dir +"/"+ reportName + "_" + dateString +"_" + timeString  + Database.DEFAULTEXTENSION;

            //Check Report exists
            if (!File.Exists(_fullReportName))
            {
                File.Create(_fullReportName).Close();
            }
            else
            {
                result = false;
            }

            return result;
        }

        public static bool WriteRecord(string[] toWrite)
        {
            
            bool result = false;

            if (File.Exists(_fullReportName))
            {
                StreamWriter writer = new StreamWriter(_fullReportName, true); //opens file in append mode

                int i = 0;
                while (i < toWrite.Length)
                {
                    writer.Write("\"");//For string safety

                    writer.Write(toWrite[i].ToString());

                    writer.Write("\"");//For string safety

                    if (i < (toWrite.Length - 1))
                    {
                        writer.WriteLine("");
                    }

                    i += 1;
                }

                writer.WriteLine();//End entry

                writer.Close();

                result = true;
            }

            else
            {
                result = false;
                Console.WriteLine(_fullReportName + " dosn't exist");
            }

            return result;
        }


    }
}
