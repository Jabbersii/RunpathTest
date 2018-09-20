using System.Collections.Generic;
using System.Threading.Tasks;
using RestEase;
using Web.Services.Models;

namespace Web.Services
{
    public interface IAlbumsApi
    {
        [Get("albums")]
        Task<List<Album>> GetAlbums([Query("id")]IEnumerable<int> ids);

        [Get("albums/{id}")]
        Task<Album> GetAlbum([Path("id")] int id);
    }
}
