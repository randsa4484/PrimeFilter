using System.Collections.Generic;
using System.Threading.Tasks;

namespace PF.WebAPI.Services.Filtering
{
    public interface IWordFilter
    {
        Task<List<string>> Filter(IEnumerable<string> words);
        string Name { get; }
    }
    
}