
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PF.WebAPI.Services;
using PF.WebAPI.Services.Filtering;

namespace PF.Tests
{
    [TestClass]
    public class TestPrimeGenerator
    {
        [TestMethod]
        public void TestZeroNotPrime()
        {
            var initialiser = new Moq.Mock<IPrimeGeneratorInitialiser>();
            initialiser.Setup(x => x.Limit).Returns(0);
            var res = CreateGenerator(initialiser);

            Assert.IsTrue(!res.Any());
        }

        [TestMethod]
        public void TestOneIsNotPrime()
        {
            var initialiser = new Moq.Mock<IPrimeGeneratorInitialiser>();
            initialiser.Setup(x => x.Limit).Returns(1);
            var res = CreateGenerator(initialiser);

            Assert.IsTrue(!res.Any());
        } 
        
        [TestMethod]
        public void TestPrimes()
        {
            var bunchOfPrimes = new List<int>{
             2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199};

            var initialiser = new Moq.Mock<IPrimeGeneratorInitialiser>();
            initialiser.Setup(x => x.Limit).Returns(200);
            var res = CreateGenerator(initialiser);
            var arr = res.ToArray();
            Assert.AreEqual(2, arr[0]);
            Assert.AreEqual(3, arr[1]);
            Assert.AreEqual(5, arr[2]);

            var resStr = string.Join(",", res);
            var testStr = string.Join(",", bunchOfPrimes);

            Assert.AreEqual(testStr, resStr);
        }

        private static IEnumerable<int> CreateGenerator(Mock<IPrimeGeneratorInitialiser> initialiser)
        {
            var pg = new SieveOfEratosthenes(new Moq.Mock<ILogger<SieveOfEratosthenes>>().Object, initialiser.Object);
            var res = pg.Primes;
            return res;
        }
    }
}