using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PF.WebAPI.Services.Filtering;

namespace PF.Tests
{
    [TestClass]
    public class TestPrimeTester
    {
        private BruteForcePrimeTester _tester;
        private Mock<IPrimesGenerator> _primesCollection;

        [TestInitialize]
        public void Initialise()
        {
            _primesCollection = new Moq.Mock<IPrimesGenerator>();
            _primesCollection.SetupGet(x => x.Primes).Returns(new List<int> { 2, 3, 5 });

            _tester = new BruteForcePrimeTester(_primesCollection.Object);
        }

        [TestMethod]
        public void MaxValueSupported_Set()
        {
            Assert.AreEqual(35, _tester.MaxValueSupported);
        }

        [TestMethod]
        public void Zero_Not_Prime()
        {
            Assert.IsFalse(_tester.NumberIsPrimeImp(0));           
        }     
        [TestMethod]
        
        public void One_Not_Prime()
        {
            Assert.IsFalse(_tester.NumberIsPrimeImp(1));
        }
        
        [TestMethod]
        public void NonInteger_Cant_Be_Prime()
        {
            Assert.IsFalse(_tester.NumberIsPrimeImp(3.6));           
        }

        [TestMethod]
        public void Negative_NonInteger_Cant_Be_Prime()
        {
            Assert.IsFalse(_tester.NumberIsPrimeImp(-3789.345));           
        }

        [TestMethod]
        public void Square_Number_Cannot_Be_Prime()
        {
            Assert.IsFalse(_tester.NumberIsPrimeImp(36));           
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Exceeding_MaxSupportedValue_Throws_NotSupportedException()
        {
            Assert.IsTrue(_tester.NumberIsPrimeImp(37));           
        }

        [TestMethod]
        public void Test_All_Ints_Up_To_Limit()
        {
            for (var i = 0; i < 36; i++)
            {
                var value = _tester.NumberIsPrimeImp(i);

                switch (i)
                {
                    case 2:
                    case 3:
                    case 5:
                    case 7:
                    case 11:
                    case 13:
                    case 17:
                    case 19:
                    case 23:
                    case 29:
                    case 31:
                        Assert.IsTrue(value, $"{i} is prime");
                        break;
                    default:
                        Assert.IsFalse(value, $"{i} is NOT prime");
                        break;
                }
            }
        }

    }
}