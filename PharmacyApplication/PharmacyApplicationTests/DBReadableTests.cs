using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacyApplication;

namespace PharmacyApplicationTests
{
    [TestClass]
    public class DBReadableTests
    {
        [TestMethod()]
        public void IDBReadable()
        {
            int id = 700;
            string name = "berries";
            int level = 40;

            StockType toTest = new StockType(id, name, level);

            int stuffid = 4;
            string stuffName = "Strepsols";
            int stuffLevel = 15;

            object[] stuff = new object[] { stuffid, stuffName, stuffLevel };

            toTest.ReadFromGeneric(stuff);

            Assert.AreNotEqual(id, toTest.ID);
            Assert.AreNotEqual(name, toTest.Name);
            Assert.AreNotEqual(level, toTest.Level);

            Assert.AreEqual(stuffid, toTest.ID);
            Assert.AreEqual(stuffName, toTest.Name);
            Assert.AreEqual(stuffLevel, toTest.Level);
        }
    }
}
