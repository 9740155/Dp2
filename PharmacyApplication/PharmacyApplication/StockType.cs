using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApplication
{
    //Authors: Jed
    /// <summary>
    /// Represents a stocktype, designed to store values when loaded out of memory
    /// </summary>
    public class StockType : DBReadable
    {
        //For IDBReadable
        public override Type[] FieldTypesToRead
        {
            get
            {
                return new Type[4] { typeof(int), typeof(string), typeof(int), typeof(int) };
            }
        }

        /// <summary>
        /// Represnts a unique identification number for the product
        /// </summary>
        //Elements 0
        public int ID
        {
            get
            {
                return (int)_elements[0];//TODO update to use FieldTypesToRead instead of hard cast to int.
            }

            set
            {
                _elements[0] = value;
            }
        }

        /// <summary>
        /// Represents an end user readable identifier for the product
        /// </summary>
        //Elements 1
        public string Name
        {
            get
            {
                return (string)_elements[1];//TODO update to use FieldTypesToRead instead of hard cast to int.
            }

            set
            {
                _elements[1] = value;
            }
        }

        /// <summary>
        /// Represents the number of a product in stock, positive means there are unit on shelves, negative means there are units owed to customers.
        /// </summary>
        //Elements 2
        public int Level
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

        /// <summary>
        /// Level of stock where an alert should be raised to the user that stock is low
        /// </summary>
        public int Alert
        {
            get
            {
                return (int)_elements[3];
            }

            set
            {
                _elements[3] = value;
            }
        }



        public StockType(int id, string name, int level) : base()
        {
            this.ID = id;
            this.Name = name;
            this.Level = level;
            this.Alert = 0;
        }

        public StockType(int id, string name, int level, int alert) : base()
        {
            this.ID = id;
            this.Name = name;
            this.Level = level;
            this.Alert = alert;
        }

        /// <summary>
        /// Returns true if number of stock is lower than alert amount.
        /// </summary>
        /// <returns></returns>
        public bool isLow()
        {
            return Level <= Alert;
        }

        public StockType(object[] objs):base()
        {
            this.ReadFromGeneric(objs);
        }
    }
}
