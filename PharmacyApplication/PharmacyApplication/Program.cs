﻿using System;
using System.Collections.Generic;
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
            /*string[] testTypes = new string[] {"float", "int", "string"};
            string[] testLabels = new string[] {"stock", "id", "name"};

            Database.CreateTable("A", "intake", testTypes, testLabels);//*/

            Form f = new Form();

            StockTypeDisplay std = new StockTypeDisplay("1997", "intake", 0);

            std.ShowDialog();
        }
    }
}
