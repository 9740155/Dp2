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
        protected override Type[] FieldTypesToRead
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
    }
}
