using System.Collections.Generic;
using System.Threading.Tasks;
using RestEase;
using Web.Services.Models;

namespace Web.Services
{
    public interface IPhotosApi
    {
        [Get("photos")]
        Task<List<Photo>> GetPhotos();

        [Get("photos/{id}")]
        Task<Photo> GetPhoto(int id);
    }
}
