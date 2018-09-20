using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Web.Services;
using Web.ViewModels;

namespace Web.Handlers
{
    public class GetPhotos : IRequest<IEnumerable<PhotoViewModel>>
    {
    }

    public class GetPhotosHandler : IRequestHandler<GetPhotos, IEnumerable<PhotoViewModel>>
    {
        private readonly IPhotosApi photos;
        private readonly IAlbumsApi albums;

        public GetPhotosHandler(IPhotosApi photos, IAlbumsApi albums)
        {
            this.photos = photos;
            this.albums = albums;
        }

        public Task<IEnumerable<PhotoViewModel>> Handle(GetPhotos request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
