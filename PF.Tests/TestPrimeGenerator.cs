
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PF.WebAPI.Services;

namespace PF.Tests
{
    [TestClass]
    public class TestPrimeTester
    {
        [TestMethod]
        public void Zero_Not_Prime()
        {
            var tester = new PrimeTester();
            Assert.IsFalse(tester.NumberIsPrime(0));           
        }     
        [TestMethod]
        
        public void One_Not_Prime()
        {
            var tester = new PrimeTester();
            Assert.IsFalse(tester.NumberIsPrime(1));
        }
        
        [TestMethod]
        public void NonInteger_Cant_Be_Prime()
        {
            var tester = new PrimeTester();
            Assert.IsFalse(tester.NumberIsPrime(3.6));           
        }

        [TestMethod]
        public void Negative_NonInteger_Cant_Be_Prime()
        {
            var tester = new PrimeTester();
            Assert.IsFalse(tester.NumberIsPrime(-3789.345));           
        }
    }

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
            var bunchOfPrimes = new List<int>{
             2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199};

            var pg = new PrimesGenerator();
            var s = new Stopwatch();
            s.Start();
            var res = pg.GetPrimes(200);
            s.Stop();
            System.Console.WriteLine("Took {0} seconds", s.Elapsed.TotalSeconds);
            var arr = res.ToArray();
            Assert.AreEqual(2, arr[0]);
            Assert.AreEqual(3, arr[1]);
            Assert.AreEqual(5, arr[2]);

            var resStr = string.Join(",", res);
            var testStr = string.Join(",", bunchOfPrimes);

            Assert.AreEqual(testStr, resStr);
        }
    }
}