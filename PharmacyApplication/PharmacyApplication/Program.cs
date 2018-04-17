using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PharmacyApplication.UserInterfaces;

namespace PharmacyApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Type[] testTypes = new Type[] {typeof(int), typeof(string), typeof(int)};
            string[] testLabels = new string[] {"ID", "Name", "Level"};

            Database.CreateTable("1997", "stock", testTypes, testLabels);//*/

            //StockType st = Database.ReadStockType("1997", "stock", 0);

            //StockTypeDisplay std = new StockTypeDisplay("1997", "stock", 0);
            DisplaySalesRecord std = new DisplaySalesRecord("1997", "stock", 0);

            std.ShowDialog();
        }
    }
}
