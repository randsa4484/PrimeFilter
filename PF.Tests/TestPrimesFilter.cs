using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PF.WebAPI.Services.Filtering;

namespace PF.Tests
{
    [TestClass]
    public class TestPrimesFilter
    {
        [TestMethod]
        public void Test()
        {
            var str = new [] {"Bob", "3", "1", "Jim", "3.1415"};

            var pt = new Moq.Mock<IPrimeTester>();
            pt.Setup(x => x.NumberIsPrime(3)).ReturnsAsync(true);
            pt.SetupGet(x => x.MaxValueSupported).Returns(10);

            var res = CreatePrimesFilter(pt.Object, str);

            Assert.AreEqual(4, res.Count());
        }

        [TestMethod]
        public void Test_Number_Larger_Than_Supported()
        {
            var str = new [] {"Bob", "3", "1", "Jim", "3.1415", "5000"};

            var pt = new Moq.Mock<IPrimeTester>();
            pt.Setup(x => x.NumberIsPrime(3)).ReturnsAsync(true);
            pt.SetupGet(x => x.MaxValueSupported).Returns(10);

            bool caught = false;
            try
            {
                var res = CreatePrimesFilter(pt.Object, str);
            }
            catch (AggregateException e)
            {
                var ex = e.InnerExceptions.First();
                caught = ex.Message == "Number to be tested (5000) exceeds the capability of the current prime number tester (10)";
            }

            Assert.IsTrue(caught);
        }

        private static IEnumerable<string> CreatePrimesFilter(IPrimeTester primeTester, string[] str)
        {
            var pg = new PrimesFilter(new Moq.Mock<ILogger<PrimesFilter>>().Object, primeTester);
            var res = pg.Filter(str).Result;
            return res;
        }


    }
}