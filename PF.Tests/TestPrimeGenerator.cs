
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PF.WebAPI.Services;

namespace PF.Tests
{
    [TestClass]
    public class TestPrimeGenerator
    {
        [TestMethod]
        public void TestZeroNotPrime()
        {
            var pg = new PrimesGenerator();
            var res = pg.GetPrimes(0);

            Assert.IsTrue(res.Count() == 0);
        }   
        
        [TestMethod]
        public void TestOneIsNotPrime()
        {
            var pg = new PrimesGenerator();
            var res = pg.GetPrimes(1);

            Assert.IsTrue(res.Count() == 0);
        } 
        
        [TestMethod]
        public void TestPrimes()
        {
            var pg = new PrimesGenerator();
            var res = pg.GetPrimes(5);

            var arr = res.ToArray();
            Assert.AreEqual(2, arr[0]);
            Assert.AreEqual(3, arr[1]);
            Assert.AreEqual(5, arr[2]);
        }
    }
}