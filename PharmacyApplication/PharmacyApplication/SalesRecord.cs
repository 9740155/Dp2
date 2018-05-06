using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApplication
{
    public class SalesRecord : DBReadable
    {
        // DateTime, Procduct id, Product Name, quantity
        public override Type[] FieldTypesToRead
        {
            get { return new Type[4] {typeof(DateTime), typeof(int), typeof(string), typeof(int)}; }
        }
        public DateTime DateOfSale
        {
            get { return (DateTime) _elements[0];}
            set { _elements[0]= value; }
        }
        public int ID
        {
            get { return (int)_elements[1]; }
            set { _elements[1] = value; }
        }
        public string Name
        {
            get { return (string)_elements[2]; }
            set { _elements[2] = value; }
        }
        public int Quantity
        {
            get { return (int)_elements[3]; }
            set { _elements[3] = value; }
        }
        public SalesRecord(int id, string name, DateTime date, int quantity) : base()
        {
            this.ID = id;
            this.Name = name;
            this.DateOfSale = date;
            this.Quantity = quantity;
        }

        public SalesRecord(object[] objs) : base()
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
        public static int[] SearchFor(string workbook, string table, bool matchDate, bool matchID, bool matchName, bool matchQuantity, DateTime date, int ID, string name, int quantity)
        {
            List<int> result = new List<int>();            

            int length = Database.FindEndLineNumber(workbook, table);

            int i = 0;
            while (i < length)
            {
                SalesRecord temp = Database.ReadSalesRecord(workbook, table, i);

                if (temp != null)
                {
                    bool found = true;

                    if (found && matchDate)
                    {
                        found = (temp.DateOfSale == date);
                    }

                    if (found && matchID)
                    {
                        found = (temp.ID == ID);
                    }

                    if (found && matchName)
                    {
                        found = (temp.Name == name);
                    }

                    if (found && matchQuantity)
                    {
                        found = (temp.Quantity == quantity);
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
