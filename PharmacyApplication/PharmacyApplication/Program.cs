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

            Database.CreateTable("Jed", "stock", testTypes, testLabels);//*/

            /*Type[] testIntakeTypes = new Type[] {typeof(string), typeof(int), typeof(int)};
            string[] testIntakeLabels = new string[] {"Date", "ID", "Amount"};

            Database.CreateTable("Jed", "intake", testIntakeTypes, testIntakeLabels);//*/

            //StockIntake st = new StockIntake("01-02-1998", 1, 5);

            //Database.AddStockIntake("Jed", "intake", st);//*/

            //Console.WriteLine((StockIntake.SearchFor("Jed", "intake", true, true, true, "01-02-1998", 1, 5))[0]);



            //StockType st = Database.ReadStockType("1997", "stock", 0);

            //StockTypeDisplay std = new StockTypeDisplay("1997", "stock", 0);
            /*DisplaySalesRecord std = new DisplaySalesRecord("1997", "stock", 0);

            std.ShowDialog();//*/

            // Console.WriteLine(Predictor.PredictLinear("Tests", "sales", new DateTime(2018, 05, 01), new DateTime(2018, 05, 27), 1).ExpectedValue);
            StockReport test = new StockReport();
            test.SaveReport("StockAlerts", "PositiveLevel", "Stock", "stock", 33);
            test.SaveReport("StockAlerts", "PositiveLevel", "Stock", "stock", 113);


            //  Console.ReadKey();


        }
    }
}
