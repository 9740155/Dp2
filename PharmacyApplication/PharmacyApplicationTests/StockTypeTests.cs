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
    public class StockTypeTests
    {
        [TestMethod()]
        public void StockTypeInitialisationSucceed()
        {
            int id = 700;
            string name = "berries";
            int level = 40;

            StockType toTest = new StockType(id, name, level);

            Assert.AreEqual(id, toTest.ID);
            Assert.AreEqual(name, toTest.Name);
            Assert.AreEqual(level, toTest.Level);
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
    }
}
