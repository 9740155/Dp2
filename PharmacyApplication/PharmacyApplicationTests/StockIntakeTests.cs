using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacyApplication;

namespace PharmacyApplicationTests
{
    /// <summary>
    /// Summary description for StockTypeTests
    /// </summary>
    [TestClass]
    public class StockIntakeTests
    {
        [TestMethod()]
        public void StockIntakeInitialisationSucceed()
        {
            string date = "01-02-1997";
            int id = 700;
            int amount = 5;

            StockIntake toTest = new StockIntake(date, id, amount);

            Assert.AreEqual(date, toTest.Date);
            Assert.AreEqual(id, toTest.ID);
            Assert.AreEqual(amount, toTest.Amount);
        }

        [TestMethod()]
        public void StockTypeInitialisationFailDate()
        {
            string date = "01-02-1997";
            int id = 700;
            int amount = 5;

            StockIntake toTest = new StockIntake(date, id, amount);

            toTest.Date = "26-07-1998";

            Assert.AreNotEqual(date, toTest.Date);
            Assert.AreEqual(id, toTest.ID);
            Assert.AreEqual(amount, toTest.Amount);
        }

        [TestMethod()]
        public void StockTypeInitialisationFailID()
        {
            string date = "01-02-1997";
            int id = 700;
            int amount = 5;

            StockIntake toTest = new StockIntake(date, id, amount);

            toTest.ID -= 5;

            Assert.AreEqual(date, toTest.Date);
            Assert.AreNotEqual(id, toTest.ID);
            Assert.AreEqual(amount, toTest.Amount);
        }

        [TestMethod()]
        public void StockTypeInitialisationFailAmount()
        {
            string date = "01-02-1997";
            int id = 700;
            int amount = 5;

            StockIntake toTest = new StockIntake(date, id, amount);

            toTest.Amount += 20;

            Assert.AreEqual(date, toTest.Date);
            Assert.AreEqual(id, toTest.ID);
            Assert.AreNotEqual(amount, toTest.Amount);
        }
    }
}
