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
        private const string workbook = "Intake Tests";
        private const string table = "intake";

        [TestInitialize()]
        public void Initialize()
        {
            StockIntake broken = new StockIntake("empty", 0, 0);

            Database.DeleteTable(workbook, table);

            Database.CreateTable(workbook, table, broken.FieldTypesToRead, new string[] {"Date", "ID", "Amount"});
        }

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
        public void StockIntakeInitialisationFailDate()
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
        public void StockIntakeInitialisationFailID()
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
        public void StockIntakeInitialisationFailAmount()
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

        [TestMethod()]
        public void StockIntakeAddSuccess()
        {
            string date = "01-02-1997";
            int id = 700;
            int amount = 5;

            StockIntake toTest = new StockIntake(date, id, amount);

            Database.AddStockIntake(workbook, table, toTest);

            Assert.AreEqual(true, true);//Shows an exception wasn't raised in add
        }

        [TestMethod()]
        public void StockIntakeAddFail()
        {
            string date = "01-02-1997";
            int id = 700;
            int amount = 5;

            StockIntake toTest = null;

            try
            {
                Database.AddStockIntake(workbook, table, toTest);

                Assert.Fail();
            }

            catch
            {
                Assert.AreEqual(true, true);//Shows an exception was raised in add
            }
        }

        [TestMethod()]
        public void StockIntakeSearchSuccess()
        {
            string date = "01-02-1997";
            int id = 700;
            int amount = 5;

            StockIntake toTest = new StockIntake(date, id, amount);

            Database.AddStockIntake(workbook, table, toTest);

            int[] rows = StockIntake.SearchFor(workbook, table, true, true, true, date, id, amount);

            StockIntake retrieved = Database.ReadStockIntake(workbook, table, rows[0]);//An entry should exist due to call to add

            Assert.AreEqual(toTest.Date, retrieved.Date);
            Assert.AreEqual(toTest.ID, retrieved.ID);
            Assert.AreEqual(toTest.Amount, retrieved.Amount);
        }

        [TestMethod()]
        public void StockIntakeSearchDateSuccess()
        {
            string date = "01-02-1997";
            int id = 700;
            int amount = 5;

            StockIntake toTest = new StockIntake(date, id, amount);

            Database.AddStockIntake(workbook, table, toTest);

            int[] rows = StockIntake.SearchFor(workbook, table, true, false, false, date, id, amount);

            StockIntake retrieved = Database.ReadStockIntake(workbook, table, rows[0]);//An entry should exist due to call to add

            Assert.AreEqual(toTest.Date, retrieved.Date);
        }

        [TestMethod()]
        public void StockIntakeSearchIDSuccess()
        {
            string date = "01-02-1997";
            int id = 700;
            int amount = 5;

            StockIntake toTest = new StockIntake(date, id, amount);

            Database.AddStockIntake(workbook, table, toTest);

            int[] rows = StockIntake.SearchFor(workbook, table, false, true, false, date, id, amount);

            StockIntake retrieved = Database.ReadStockIntake(workbook, table, rows[0]);//An entry should exist due to call to add

            Assert.AreEqual(toTest.ID, retrieved.ID);
        }

        [TestMethod()]
        public void StockIntakeSearchAmountSuccess()
        {
            string date = "01-02-1997";
            int id = 700;
            int amount = 5;

            StockIntake toTest = new StockIntake(date, id, amount);

            Database.AddStockIntake(workbook, table, toTest);

            int[] rows = StockIntake.SearchFor(workbook, table, false, false, true, date, id, amount);

            StockIntake retrieved = Database.ReadStockIntake(workbook, table, rows[0]);//An entry should exist due to call to add

            Assert.AreEqual(toTest.Amount, retrieved.Amount);
        }

        [TestMethod()]
        public void StockIntakeSearchFail()
        {
            string date = "01-02-1998";
            int id = 800;
            int amount = 4;

            StockIntake toTest = new StockIntake(date, id, amount);

            int[] rows = StockIntake.SearchFor(workbook, table, true, true, true, date, id, amount);

            Assert.AreEqual(0, rows.Length);
        }
    }
}
