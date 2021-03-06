﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestEase;
using Web.Exceptions;
using Web.Services;
using Web.ViewModels;

namespace Web.Handlers
{
    public class GetPhotos : IRequest<IEnumerable<PhotoViewModel>>
    {
    }

    public class GetPhotosHandler : IRequestHandler<GetPhotos, IEnumerable<PhotoViewModel>>
    {
        private readonly IPhotosApi photosApi;
        private readonly IAlbumsApi albumsApi;

        public GetPhotosHandler(IPhotosApi photosApi, IAlbumsApi albumsApi)
        {
            this.photosApi = photosApi;
            this.albumsApi = albumsApi;
        }

        public async Task<IEnumerable<PhotoViewModel>> Handle(GetPhotos request, CancellationToken cancellationToken)
        {
            try
            {
                var photosTask = this.photosApi.GetPhotos(cancellationToken);
                var albumsTask = this.albumsApi.GetAlbums(cancellationToken);

                await Task.WhenAll(photosTask, albumsTask);

                var photos = await photosTask;
                var albums = await albumsTask;

                var photoViewModels = photos.Join(albums,
                    p => p.AlbumId,
                    a => a.Id,
                    (p, a) => new PhotoViewModel()
                    {
                        PhotoTitle = p.Title,
                        AlbumName = a.Title,
                        ThumbnailUrl = p.ThumbnailUrl,
                        Url = p.Url
                    });

                return photoViewModels;
            }
            catch (ApiException ex)
            {
                throw new RunpathAlbumWebException("Error communicating with API", ex);
            }
        }
    }
}
