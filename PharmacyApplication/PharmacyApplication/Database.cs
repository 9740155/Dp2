using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PharmacyApplication
{
    public static class Database
    {
        //WARNING: This helps the database find files its already created, changing this will essentially remove access for the database to existing files.
        public static string ROOTDRECTORY
        {
            get
            {
                return "./Root";
            }
        }

        public static string DEFAULTEXTENSION
        {
            get
            {
                return ".csv";
            }
        }


        //Author: Jed
        //Params (workbook="1997", tableName = "Stock", types = {int, string, int}, labels = {id, name, number in stock})
        /// <summary>
        /// Creates a new table if one dosn't already exist in the specified workbook with the specified name. Returns true if the file was created.
        /// </summary>
        /// <param name="Workbook">The workbook to be accessed or created</param>
        /// <param name="tableName">The table in the workbook to be created</param>
        /// <param name="types">The types of data to be stored in each column</param>
        /// <param name="labels">The titles of each of the columns</param>
        /// <returns></returns>
        public static bool CreateTable(string Workbook, string tableName, string[] types, string[] labels)
        {
            bool result = false;

            string dir = Database.ROOTDRECTORY + "/" + Workbook;

            //Check number of comlumns matches for types and labels
            if (labels.Length == types.Length)
            {

                //Check Workbook exists
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                dir += "/" + tableName + Database.DEFAULTEXTENSION;

                //Check table exists
                if (!File.Exists(dir))
                {
                    File.Create(dir).Close();

                    //Initialisation

                    StreamWriter sW = new StreamWriter(dir);

                    int i = 0;
                    while(i < types.Length)
                    {
                        sW.Write(types[i]);

                        if (i < (types.Length - 1))
                        {
                            sW.Write(", ");
                        }

                        i += 1;
                    }

                    //End line
                    sW.WriteLine();

                    i = 0;
                    while (i < labels.Length)
                    {
                        sW.Write(labels[i]);

                        if (i < (labels.Length - 1))
                        {
                            sW.Write(", ");
                        }

                        i += 1;
                    }

                    //End line
                    sW.WriteLine();

                    sW.Close();
                    result = true;
                }
            }

            else
            {
                result = false;
            }

            return result;
        }

        //Author: Jed
        //Params (workbook="1997", tableName = "Stock")
        /// <summary>
        /// Deletes the specified table in the specified workbook if it exists and deletes workbook if it is empty after the delete. Returns true if a file was deleted.
        /// </summary>
        /// <param name="workbook">The workbook to be accessed and/or deleted</param>
        /// <param name="table">The table in the workbook to be deleted</param>
        /// <returns></returns>
        public static bool DeleteTable(string workbook, string table)
        {
            bool result = false;

            string wdir = Database.ROOTDRECTORY + "/" + workbook;
            string fdir = wdir + "/" + table + Database.DEFAULTEXTENSION;

            if(File.Exists(fdir))
            {
                File.Delete(fdir);

                if (Directory.GetFiles(wdir).Length <= 0)
                {
                    Directory.Delete(wdir);
                }

                result = true;
            }            

            return result;
        }

        public static bool WriteRecordAlter(string Workbook, string Table, int lineToEdit, string stock, string id, string name)
        {
            bool result = false;

            int lineNumber = 2;

            string line = null;

            string dir = Database.ROOTDRECTORY + "/" + Workbook + "/" + Table;
            StreamWriter sW = new StreamWriter(dir);
            StreamReader sR = new StreamReader(dir);

            // Initialises line to the current line being read
            while ((line = sR.ReadLine()) != null)
            {
                // Tests if the current line is the same as the
                // passed in lineToEdit variable
                if (lineNumber == lineToEdit)
                {
                    // If the line numbers match then overwrite the line
                    // with the passed in variables
                    sW.WriteLine(stock + ", " + id + ", " + name);
                    result = true;
                }
                else
                {
                    // If the lines dont match then rewrite the
                    // line with the same data to keep the 
                    // ReadLine() and WriteLine() in sync
                    sW.WriteLine(line);
                }
                // Increment the lineNumber to keep in sync with
                // ReadLine() and WriteLine()
                lineNumber++;
            }

            return result;
        }

        public static object[] Read(string workbook, string tableName, int lineToRead)
        {
            Object[] result;
            int lineNumber = 1;
            string dir = Database.ROOTDRECTORY + "/" + workbook + "/" + tableName + Database.DEFAULTEXTENSION; ;
            StreamReader sR = new StreamReader(dir);

            string[] dataTypes;
            string firstline = sR.ReadLine();
            dataTypes = firstline.Split(',');
            string[] labels;
            string secoundLine = sR.ReadLine();
            labels = secoundLine.Split(',');
            
            string readLine = null;
            while ((readLine = sR.ReadLine()) != null)
            {
                if (lineNumber == lineToRead)
                {
                    result = readLine.Split(',');
                    sR.Close();
                    return result;
                }
                lineNumber++;
            }
            Console.WriteLine("Error line not found");
            Debug.WriteLine("Error line not found");
            sR.Close();

            //Throw exception to mark EOF, instead of return null
            throw new EndOfStreamException();
        }

        //Author: Jed
        /// <summary>
        /// Reads data from the specified table returning a StockType if one exists at the given index else returns null
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="table"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static StockType ReadStockType(string workbook, string table, int indexToRead)
        {
            StockType result = null;

            object[] vals = Database.Read(workbook, table, indexToRead);

            if (vals == null)
            {
                //Assumed error code, just return null
                result = null;
            }

            else
            {
                result = new StockType(vals);
            }

            return result;
        }

        public static void AddNewStockType(StockType addedStockType,string workbook)
        {
            string StockTable = "stock";
            int endLine = FindEndLineNumber(workbook, StockTable) + 1 ;
            WriteRecordAlter(workbook, endLine, addedStockType.Level.ToString(), addedStockType.ID.ToString(), addedStockType.Name);
        }

        public static int FindEndLineNumber(string workBook, string tableName)
        {
            string readLine = null;
            string dir = Database.ROOTDRECTORY + "/" + workBook + "/" + tableName + Database.DEFAULTEXTENSION;
            StreamReader sR = new StreamReader(dir);
            int lineNumber = -2;
            while ((readLine = sR.ReadLine()) != null)
            {
                lineNumber++;
            }
            sR.Close();
            return lineNumber;
        }
    }
}
