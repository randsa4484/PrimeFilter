
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
        public void TestOneIsPrime()
        {
            var pg = new PrimesGenerator();
            var res = pg.GetPrimes(1);

            Assert.IsTrue(res.Count() == 1);
            Assert.IsTrue(res.First() == 1);
        } 
        
        [TestMethod]
        public void TestPrimes()
        {
            var pg = new PrimesGenerator();
            var res = pg.GetPrimes(5);
        }
    }
}