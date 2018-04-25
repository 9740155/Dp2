using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApplication
{
    //Authors: Jed
    /// <summary>
    /// Represents a stock intake, designed to store values when loaded out of memory
    /// </summary>
    public class StockIntake : DBReadable
    {
        //For IDBReadable
        public override Type[] FieldTypesToRead
        { 
            get
            {
                return new Type[3] { typeof(string), typeof(int), typeof(int) };
            }
        }

        /// <summary>
        /// Represnts the date on which the intake occured
        /// </summary>
        //Elements 0
        public string Date
        {
            get
            {
                return (string)_elements[0];//TODO update to use FieldTypesToRead instead of hard cast to int.
            }

            set
            {
                _elements[0] = value;
            }
        }

        /// <summary>
        /// Represents a unique identifier for the stock type in-taken
        /// </summary>
        //Elements 1
        public int ID
        {
            get
            {
                return (int)_elements[1];//TODO update to use FieldTypesToRead instead of hard cast to int.
            }

            set
            {
                _elements[1] = value;
            }
        }

        /// <summary>
        /// Represents the number of stock in-taken.
        /// </summary>
        //Elements 2
        public int Amount
        {
            get
            {
                return (int)_elements[2];//TODO update to use FieldTypesToRead instead of hard cast to int.
            }

            set
            {
                _elements[2] = value;
            }
        }


        public StockIntake(string date, int id, int amount) : base()
        {
            this.ID = id;
            this.Date = date;
            this.Amount = amount;
        }

        public StockIntake(object[] objs) : base()
        {
            this.ReadFromGeneric(objs);
        }

        //Author: Jed
        /// <summary>
        /// Searches through a table, returns an array of row numbers which contain data matching the match types specified
        /// </summary>
        /// <param name="date"></param>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <param name="matchDate"></param>
        /// <param name="matchID"></param>
        /// <param name="matchAmount"></param>
        /// <param name="workbook"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static int[] SearchFor(string workbook, string table, bool matchDate, bool matchID, bool matchAmount, string date, int ID, int amount)
        {
            List<int> result = new List<int>();

            int length = Database.FindEndLineNumber(workbook, table);

            int i = 0;
            while (i < length)
            {
                StockIntake temp = Database.ReadStockIntake(workbook, table, i);

                if (temp != null)
                {
                    bool found = true;

                    if (found && matchDate)
                    {
                        found = (temp.Date == date);
                    }

                    if (found && matchID)
                    {
                        found = (temp.ID == ID);
                    }

                    if (found && matchAmount)
                    {
                        found = (temp.Amount == amount);
                    }



                    if (found)
                    {
                        result.Add(i);//A match was found
                    }
                }
                i += 1;
            }


            return result.ToArray();
        }
    }
}
