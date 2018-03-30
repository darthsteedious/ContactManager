using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactManager.Data
{
    public interface IQueryProvider
    {
        Task<IEnumerable<TResult>> QueryAsync<TResult>(string text);
        Task<TResult> QuerySingleAsync<TResult>(string text);
    }
    
    public class QueryProvider
    {
        
    }
}