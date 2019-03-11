using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PF.WebAPI.Services.StringParsing
{
    public class StringParser : IStringParser
    {
        public StringParser(ILogger<StringParser> logger)
        {
        }

        public IEnumerable<string> ParseString(string stringtoParse, string delimiter)
        {
            if (string.IsNullOrEmpty(stringtoParse))
                return new List<string>();

            return stringtoParse.Split(delimiter).Where(word => !string.IsNullOrEmpty(word));
        }

    }
}
