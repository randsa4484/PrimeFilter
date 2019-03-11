using System.Collections.Generic;

namespace PF.WebAPI.Services.StringParsing
{
    public interface IStringParser
    {
        IEnumerable<string> ParseString(string stringtoParse, string delimiter);
    }
}