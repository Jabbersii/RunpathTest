using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RestEase;
using Web.Services.Models;

namespace Web.Services
{
    public interface IAlbumsApi
    {
        [Get("albums")]
        Task<List<Album>> GetAlbums(CancellationToken token = default(CancellationToken));
    }
}
