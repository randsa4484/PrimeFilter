using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PF.WebAPI.Services.StringParsing;

namespace PF.Tests
{
    [TestClass]
    public class TestStringParser
    {
        Mock<IConfiguration> config;
        private StringParser _stringParser;

        [TestInitialize]
        public void Initialise()
        {
            config = new Moq.Mock<IConfiguration>();
            config.SetupGet(x => x["Parsing:Delimiter"]).Returns(",");
            _stringParser = new StringParser(new Mock<ILogger<StringParser>>().Object);
        }

        [TestMethod]
        public void ParseEmptyString()
        {
            var res = _stringParser.ParseString(null, null);
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void ParseString()
        {
            var res = _stringParser.ParseString("1, 2, 5.5", ", ").ToArray();

            Assert.AreEqual(3, res.Length);
            Assert.AreEqual("1", res[0]);
            Assert.AreEqual("2", res[1]);
            Assert.AreEqual("5.5", res[2]);
        }

        [TestMethod]
        public void ParseString_DoubleSpace()
        {
            var res = _stringParser.ParseString("-1 5.5  test string", " ").ToArray();

            Assert.AreEqual(4, res.Length);
        }
    }
}