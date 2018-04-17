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
        protected override Type[] FieldTypesToRead
        {
            get
            {
                return new Type[3] { typeof(int), typeof(string), typeof(int) };
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


        public StockType(int id, string name, int level) : base()
        {
            this.ID = id;
            this.Name = name;
            this.Level = level;
        }

        public StockType(object[] objs):base()
        {
            this.ReadFromGeneric(objs);
        }
    }
}
