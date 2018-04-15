using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacyApplication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyApplication;

namespace PharmacyApplication.Tests
{
    [TestClass()]
    public class DatabaseTests
    {
        //All global test variable MUST be constant, somone else will come along and change them just so your test won't work
        const string workbook = "Tests";
        const string tableName = "Test Slock Levels";
        readonly string[] testTypes = new string[] { "float", "int", "string" };
        readonly string[] testLabels = new string[] { "stock", "id", "name" };


        [TestInitialize()]
        public void Initialize()
        {
             Database.CreateTable(workbook, tableName, testTypes, testLabels);
        }

        [TestMethod()]
        public void DeleteTableSucceed()
        {
            string table = "CreateTest";

            Database.CreateTable(workbook, table, testTypes, testLabels);

            Assert.IsTrue(Database.DeleteTable(workbook, table));
        }

        [TestMethod()]
        public void DeleteTableFail()
        {
            string table = "CreateTest";

            Database.DeleteTable(workbook, table);

            Assert.IsFalse(Database.DeleteTable(workbook, table));
        }

        [TestMethod()]
        public void CreateTableSucceed()
        {
            string table = "CreateTest";

            //Delete any table that's already there, otherwise the creation wil return false
            Database.DeleteTable(workbook, table);

            Assert.IsTrue(Database.CreateTable(workbook, table, testTypes, testLabels));
        }

        [TestMethod()]
        public void CreateTableFail()
        {
            string table = "CreateTest";

            Database.CreateTable(workbook, "CreateTest", testTypes, testLabels);

            //Fails because table already exists
            Assert.IsFalse(Database.CreateTable(workbook, table, testTypes, testLabels));
        }

        [TestMethod()]
        public void CreateTableBoundaryDirectoryMissing()
        {
            string table = "CreateTest";

            string dir = Database.ROOTDRECTORY + "/" + workbook;

            try
            {
                Directory.Delete(dir, true);
            }

            catch
            {
                //nothing
            }

            Assert.IsFalse(Directory.Exists(dir));
            Assert.IsTrue(Database.CreateTable(workbook, table, testTypes, testLabels));
        }

        [TestMethod()]
        public void CreateTableBoundaryDirectoryExists()
        {
            string table = "CreateTest";

            string dir = Database.ROOTDRECTORY + "/" + workbook;

            try
            {
                Directory.Delete(dir, true);
            }

            catch
            {
                //nothing
            }

            try
            {
                Directory.CreateDirectory(dir);
            }

            catch
            {
                //nothing
            }

            
            Assert.IsTrue(Database.CreateTable(workbook, table, testTypes, testLabels));
        }

        [TestMethod()]
        public void StockTypeInitialisationSucceed()
        {
            int id = 700;
            string name = "berries";
            int level = 40;

            StockType toTest = new StockType(id, name, level);

            /*Assert.AreEqual(id, toTest.ID);
            Assert.AreEqual(name, toTest.Name);
            Assert.AreEqual(level, toTest.Level);//*/
        }

        [TestMethod()]
        public void StockTypeInitialisationFailID()
        {
            int id = 700;
            string name = "berries";
            int level = 40;

            StockType toTest = new StockType(id, name, level);

            toTest.ID -= 50;

            Assert.AreNotEqual(id, toTest.ID);
            Assert.AreEqual(name, toTest.Name);
            Assert.AreEqual(level, toTest.Level);
        }

        [TestMethod()]
        public void StockTypeInitialisationFailName()
        {
            int id = 700;
            string name = "berries";
            int level = 40;

            StockType toTest = new StockType(id, name, level);

            toTest.Name = "BlueBerries";

            Assert.AreEqual(id, toTest.ID);
            Assert.AreNotEqual(name, toTest.Name);
            Assert.AreEqual(level, toTest.Level);
        }

        [TestMethod()]
        public void StockTypeInitialisationFailLevelIncrement()
        {
            int id = 700;
            string name = "berries";
            int level = 40;

            StockType toTest = new StockType(id, name, level);

            toTest.Level += 1;

            Assert.AreEqual(id, toTest.ID);
            Assert.AreEqual(name, toTest.Name);
            Assert.AreNotEqual(level, toTest.Level);
        }

        [TestMethod()]
        public void StockTypeInitialisationFailLevelDecrement()
        {
            int id = 700;
            string name = "berries";
            int level = 40;

            StockType toTest = new StockType(id, name, level);

            toTest.Level -= 1;

            Assert.AreEqual(id, toTest.ID);
            Assert.AreEqual(name, toTest.Name);
            Assert.AreNotEqual(level, toTest.Level);
        }

        public void IDBReadable ()
        {
            int id = 700;
            string name = "berries";
            int level = 40;

            StockType toTest = new StockType(id, name, level);

            int stuffid = 4;
            string stuffName = "Strepsols";
            int stuffLevel = 15;

            object[] stuff = new object[] { stuffid, stuffName, stuffLevel};

            Assert.AreNotEqual(id, toTest.ID);
            Assert.AreNotEqual(name, toTest.Name);
            Assert.AreNotEqual(level, toTest.Level);

            Assert.AreEqual(stuffid, toTest.ID);
            Assert.AreEqual(stuffName, toTest.Name);
            Assert.AreEqual(stuffLevel, toTest.Level);
        }

        [TestMethod()]
        public void ReadTest()
        {
            object[] testObjects = Database.Read(workbook, tableName, 3);
            Assert.IsNotNull(testObjects);
        }

        [TestMethod()]
        public void WriteRecordAlterTest()
        {
            Assert.IsTrue(Database.WriteRecordAlter("TestData", "intake.csv", 4, "34", "10", "toothpaste"));
            //Assert.IsFalse(Database.WriteRecordAlter("TestData", "intake.csv", 4, "34", "10", "toothpaste"));
        }
    }
}