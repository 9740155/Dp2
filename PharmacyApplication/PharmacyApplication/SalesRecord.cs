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
        protected override Type[] FieldTypesToRead
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

    }
}
