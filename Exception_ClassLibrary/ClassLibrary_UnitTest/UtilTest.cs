using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Exception_ClassLibrary
{
    [TestFixture]
    public class UtilTest
    {
        [Test]
        public void TestValidFile1()
        {
            StringHelper cs = new StringHelper();
            bool result = cs.ValidFileName("demo.SLF");
            Assert.AreEqual(false, result);
        }

        [Test]
        public void TestValidFile2()
        {
            StringHelper cs = new StringHelper();
            bool result = cs.ValidFileName("demo.foo");
            Assert.AreEqual(true, result);
        }
    }
}
