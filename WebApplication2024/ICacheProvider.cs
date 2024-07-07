using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication2024
{
    public interface ICacheProvider
    {
        Task<IEnumerable<Guest>> GetCachedResponse();
    }
}
