using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

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
        public static bool CreateTable(string Workbook, string tableName, Type[] types, string[] labels)
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
                        sW.Write("\"");//For string safety

                        sW.Write(types[i].FullName);

                        sW.Write("\"");//For string safety

                        if (i < (types.Length - 1))
                        {
                            sW.Write(",");//No characters aside from commas here, will break reads
                        }

                        i += 1;
                    }

                    //End line
                    sW.WriteLine();

                    i = 0;
                    while (i < labels.Length)
                    {
                        sW.Write("\"");//For string safety

                        sW.Write(labels[i]);

                        sW.Write("\"");//For string safety

                        if (i < (labels.Length - 1))
                        {
                            sW.Write(",");
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

        //appends a record to a table
        public static bool WriteRecord(string Workbook, string Table, string stock, string id, string name)
        {
            StreamWriter writer = new StreamWriter(ROOTDRECTORY + "/" + Workbook + "/" + Table + Database.DEFAULTEXTENSION, true); //opens file in append mode
            writer.WriteLine("\"" + stock + "\", \"" + id + "\", \"" + name + "\"");// \" for string safety
            writer.Close();
            return true;
        }

        //Author: Jed
        /// <summary>
        /// Writes DBReadable objects into a table
        /// </summary>
        /// <param name="Workbook"></param>
        /// <param name="Table"></param>
        /// <param name="toWrite"></param>
        /// <returns></returns>
        public static bool WriteRecord(string workbook, string table, object[] toWrite)
        {
            string dir = Database.ROOTDRECTORY + "/" + workbook + "/" + table + Database.DEFAULTEXTENSION;
            bool result = false;

            if (File.Exists(dir))
            {
                StreamWriter writer = new StreamWriter(dir, true); //opens file in append mode

                int i = 0;
                while (i < toWrite.Length)
                {
                    writer.Write("\"");//For string safety

                    writer.Write(toWrite[i].ToString());

                    writer.Write("\"");//For string safety

                    if (i < (toWrite.Length - 1))
                    {
                        writer.Write(",");
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
                Console.WriteLine(dir + " dosn't exist");
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

        //Author: Jed
        /// <summary>
        /// Splits strings read from the DB into an array of strings delimited by , and removing "
        /// </summary>
        /// <param name="toSplit"></param>
        /// <returns></returns>
        public static string[] Split(string toSplit)
        {
            List<string> result = new List<string>();

            char stringIndicator = '"';
            char delimiter = ',';

            bool insideString = false;
            string temp = "";
            int i = 0;
            while (i < toSplit.Length)
            {
                if (toSplit[i] == stringIndicator)
                {
                    insideString = !insideString;//toggle
                }

                else
                {
                    if (insideString)
                    {
                        temp += toSplit[i];
                    }

                    else
                    {
                        if (toSplit[i] == delimiter)
                        {
                            result.Add(temp);
                            temp = "";//reset
                        }
                    }
                }

                i += 1;
            }

            result.Add(temp);//flush last entry

            return result.ToArray();
        }

        //Authors: Jed, Jaques
        /// <summary>
        /// Reads a single row of values from a table and returns them as a generic object array.
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="tableName"></param>
        /// <param name="lineToRead"></param>
        /// <returns></returns>
        public static object[] Read(string workbook, string tableName, int lineToRead)
        {
            const int offset = 2;//Number of lines to be skipped, lets 0 based index reads make sense

            Object[] result;
            int lineNumber = offset;//Current total index of lines read
            string dir = Database.ROOTDRECTORY + "/" + workbook + "/" + tableName + Database.DEFAULTEXTENSION; ;
            StreamReader sR = new StreamReader(dir);

            string[] dataTypes;
            string firstline = sR.ReadLine();
            dataTypes = Database.Split(firstline);

            string[] labels;
            string secoundLine = sR.ReadLine();
            labels = Database.Split(secoundLine);

            string[] readData = null;
            
            string readLine = null;
            while ((readLine = sR.ReadLine()) != null)
            {
                if ((lineNumber - offset) == lineToRead)
                {
                    readData = Database.Split(readLine);
                    sR.Close();

                    //removed return to allow for typing of objects
                    break;
                }
                lineNumber++;
            }

            //Line not found
            if (readData == null)
            {
                Console.WriteLine("Error line not found");
                Debug.WriteLine("Error line not found");
                sR.Close();

                //Throw exception to mark EOF, instead of return null
                throw new EndOfStreamException();
            }

            else
            {
                if(dataTypes.Length != readData.Length)
                {
                    throw new FormatException(String.Format("Data and data types mismatch, Data found: {0}, Data types found {1}", readData.Length, dataTypes.Length));
                }

                result = new object[readData.Length];

                result = Database.ParseReadTypes(dataTypes, readData);
            }

            return result;
        }

        //Author: Jed
        /// <summary>
        /// Parse the strings in toParse and casts them as the types represented by types.
        /// </summary>
        /// <param name="types"></param>
        /// <param name="toParse"></param>
        /// <returns></returns>
        public static object[] ParseReadTypes(string[] types, string[] toParse)
        {
            if (types.Length != toParse.Length)
            {
                throw new FormatException(String.Format("Mismatch in number of colums, {0} types given and {1} strings to parse.\n", types.Length, toParse.Length));
            }

            object[] result = new object[toParse.Length];

            int i = 0;
            while(i < toParse.Length)
            {
                //Check if the types full name matches the following expected types
                if (types[i].ToLower() == typeof(int).FullName.ToLower())
                {
                    result[i] = Int32.Parse(toParse[i]);
                }

                else if (types[i].ToLower() == typeof(long).FullName.ToLower())
                {
                    result[i] = Int64.Parse(toParse[i]);
                }

                else if (types[i].ToLower() == typeof(short).FullName.ToLower())
                {
                    result[i] = Int16.Parse(toParse[i]);
                }

                else if (types[i].ToLower() == typeof(string).FullName.ToLower())
                {
                    //Strings don't parse
                    result[i] = toParse[i];
                }

                else if (types[i].ToLower() == typeof(float).FullName.ToLower())
                {
                    result[i] = float.Parse(toParse[i]);
                }

                else
                {
                    throw new InvalidCastException(String.Format("No suitable type was found for coverting {0} to {1}, consider extending Database.ParseReadTypes.\n", toParse[i], types[i]));
                }

                i += 1;
            }

            return result;
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

        //Author: Jed
        /// <summary>
        /// Reads data from the specified table returning a SalesRecord if one exists at the given index else returns null
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="table"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static SalesRecord ReadSalesRecord(string workbook, string table, int indexToRead)
        {
            SalesRecord result = null;

            object[] vals = Database.Read(workbook, table, indexToRead);

            if (vals == null)
            {
                //Assumed error code, just return null
                result = null;
            }

            else
            {
                result = new SalesRecord(vals);
            }

            return result;
        }

        //Author: Jed
        /// <summary>
        /// Reads data from the specified table returning a StockIntake if one exists at the given index else returns null
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="table"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static StockIntake ReadStockIntake(string workbook, string table, int indexToRead)
        {
            StockIntake result = null;

            object[] vals = Database.Read(workbook, table, indexToRead);

            if (vals == null)
            {
                //Assumed error code, just return null
                result = null;
            }

            else
            {
                result = new StockIntake(vals);
            }

            return result;
        }

        public static void AddNewStockType(StockType addedStockType, string workbook)
        {
            string stockTable = "stock";
            int endLine = FindEndLineNumber(workbook, stockTable) + 1 ;
            WriteRecordAlter(workbook, stockTable, endLine, addedStockType.Level.ToString(), addedStockType.ID.ToString(), addedStockType.Name);
        }

        public static void AddStockIntake(string workbook, string table, StockIntake toAdd)
        {
           Database.WriteRecord(workbook, table, toAdd.Elements);
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

        public static bool UpdateStockLevel(string stockID, int stockLevel, string WorkBook, string Table)
        {
            bool result = false;
            string stock = "";
            string name = "";
            string[] tempArr;
            string tempStr;

            int lineToEdit = 0;

            string dir = Database.ROOTDRECTORY + "/" + WorkBook + "/" + Table;
            StreamReader sR = new StreamReader(dir);

            while (result == false)
            {
                lineToEdit++;
                tempStr = sR.ReadLine();
                // Clears the white spaces from the string
                tempStr = tempStr.Replace(" ", "");
                // Splits the read line into sections where it finds a ','
                tempArr = tempStr.Split(',');

                // Removes the ',' from each string
                for(int i = 0; i < 3; i++)
                {
                    tempArr[i] = tempArr[i].Replace(",", "");
                }

                if (tempArr[0] == stockID)
                {
                    result = true;
                    name = tempArr[1];
                    stock = stockLevel.ToString();
                }
            }

            WriteRecordAlter(WorkBook, Table, lineToEdit, stock, stockID, name);

            return result;
        }
    }
}
