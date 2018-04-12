using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacyApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApplication.Tests
{
    [TestClass()]
    public class DatabaseTests
    {
        string workbook = "Tests";
        string tableName = "Test Slock Levels";
        string[] testTypes = new string[] { "float", "int", "string" };
        string[] testLabels = new string[] { "stock", "id", "name" };
        [TestInitialize()]
        public void Initialize()
        {
            Database.CreateTable(workbook, tableName, testTypes, testLabels);
        }

        [TestMethod()]
        public void CreateTableTest()
        {
            Assert.IsTrue(Database.CreateTable(workbook, "CreateTest", testTypes, testLabels));
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