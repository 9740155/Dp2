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
            string book = "NothingElse";

            //Delete any table that's already there, otherwise the creation wil return false
            Database.DeleteTable(book, table);

            Assert.IsTrue(Database.CreateTable(book, table, testTypes, testLabels));
        }

        [TestMethod()]
        public void CreateTableFail()
        {
            string table = "CreateTest";
            string book = "BothingElse";

            Database.CreateTable(book, "CreateTest", testTypes, testLabels);

            //Fails because table already exists
            Assert.IsFalse(Database.CreateTable(book, table, testTypes, testLabels));
        }

        [TestMethod()]
        public void CreateTableBoundaryDirectoryMissing()
        {
            string table = "CreateTest";
            string book = "BothingElse";

            string dir = Database.ROOTDRECTORY + "/" + book;

            try
            {
                Directory.Delete(dir, true);
            }

            catch
            {
                //nothing
            }

            Assert.IsFalse(Directory.Exists(dir));
            Assert.IsTrue(Database.CreateTable(book, table, testTypes, testLabels));
        }

        [TestMethod()]
        public void CreateTableBoundaryDirectoryExists()
        {
            string table = "CreateTest";
            string book = "BothingElse";

            string dir = Database.ROOTDRECTORY + "/" + book;

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

            
            Assert.IsTrue(Database.CreateTable(book, table, testTypes, testLabels));
        }

        [TestMethod()]
        public void ReadTest()
        {
            object[] testObjects = Database.Read(workbook, "stock", 1);
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(testObjects[i]);
            }
            Assert.IsNotNull(testObjects);
            Assert.AreEqual(testObjects[0],"0");
            Assert.AreEqual(testObjects[1], "Sunnies");
            Assert.AreEqual(testObjects[2], "32");
        }
    }
}