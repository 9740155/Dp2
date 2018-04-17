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
                        sW.Write(types[i].FullName);

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
                        sW.Write(labels[i]);

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

        public static bool WriteRecordAlter(string Workbook, string Table, int lineToEdit, string stock, string id, string name)
        {
            bool result = false;

            string lineToWrite = null;

            string dir = Database.ROOTDRECTORY + "/" + Workbook + "/" + Table;

            string dir = Database.ROOTDRECTORY + "/" + Workbook + "/" + Table;
            StreamWriter sW = new StreamWriter(dir);
            StreamReader sR = new StreamReader(dir);

            // Initialises line to the current line being read
            while ((line = sR.ReadLine()) != null)
            {
                for (int i = 1; i <= lineToEdit; i++)
                {
                    lineToWrite = sR.ReadLine();
                }
            }

            string[] lines = File.ReadAllLines(dir);

            using (StreamWriter sW = new StreamWriter(dir))
            {
                for (int currentLine = 1; currentLine <= lines.Length; currentLine++)
                {
                    if (currentLine == lineToEdit)
                    {
                        sW.WriteLine(stock + ", " + id + ", " + name);
                        result = true;
                    }
                    else
                    {
                        sW.WriteLine(lines[currentLine - 1]);
                    }
                }
            }
            return result;
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
            dataTypes = firstline.Split(',');

            string[] labels;
            string secoundLine = sR.ReadLine();
            labels = secoundLine.Split(',');

            string[] readData = null;
            
            string readLine = null;
            while ((readLine = sR.ReadLine()) != null)
            {
                if ((lineNumber - offset) == lineToRead)
                {
                    readData = readLine.Split(',');
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

                else if (types[i].ToLower() ==typeof(float).FullName.ToLower())
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

        public static void AddNewStockType(StockType addedStockType, string workbook)
        {
            string stockTable = "stock";
            int endLine = FindEndLineNumber(workbook, stockTable) + 1 ;
            WriteRecordAlter(workbook, stockTable, endLine, addedStockType.Level.ToString(), addedStockType.ID.ToString(), addedStockType.Name);
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
