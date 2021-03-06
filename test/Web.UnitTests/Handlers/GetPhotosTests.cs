﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using RestEase;
using Web.Exceptions;
using Web.Handlers;
using Web.Services;
using Web.Services.Models;
using Web.ViewModels;
using Xunit;

namespace Web.UnitTests.Handlers
{
    public class GetPhotosTests
    {
        [Fact]
        public async Task Will_return_correctly_correlated_models_from_APIs()
        {
            var photos = new List<Photo>()
            {
                new Photo()
                {
                    AlbumId = 1,
                    Title = "Photo 1",
                    ThumbnailUrl = new Uri("http://localhost/photo/thumb/1"),
                    Url = new Uri("http://localhost/photo/1")
                },
                new Photo()
                {
                    AlbumId = 1,
                    Title = "Photo 2",
                    ThumbnailUrl = new Uri("http://localhost/photo/thumb/2"),
                    Url = new Uri("http://localhost/photo/2")
                }
            };

            var albums = new List<Album>()
            {
                new Album()
                {
                    Id = 1,
                    Title = "Album 1",
                }
            };

            var photosApi = new Mock<IPhotosApi>();
            var albumsApi = new Mock<IAlbumsApi>();

            photosApi.Setup(p => p.GetPhotos(CancellationToken.None)).ReturnsAsync(photos);
            albumsApi.Setup(a => a.GetAlbums(CancellationToken.None)).ReturnsAsync(albums);

            var handler = new GetPhotosHandler(photosApi.Object, albumsApi.Object);

            var result = await handler.Handle(new GetPhotos(), CancellationToken.None);

            result.Should()
                .BeEquivalentTo(new[]
                {
                    new PhotoViewModel()
                    {
                        PhotoTitle = "Photo 1",
                        AlbumName = "Album 1",
                        ThumbnailUrl = new Uri("http://localhost/photo/thumb/1"),
                        Url = new Uri("http://localhost/photo/1"),
                    },
                    new PhotoViewModel()
                    {
                        PhotoTitle = "Photo 2",
                        AlbumName = "Album 1",
                        ThumbnailUrl = new Uri("http://localhost/photo/thumb/2"),
                        Url = new Uri("http://localhost/photo/2"),
                    }
                });
        }

        [Fact]
        public void Handler_wraps_API_Exception_in_application_exception()
        {
            var photosApi = new Mock<IPhotosApi>();
            var albumsApi = new Mock<IAlbumsApi>();

            photosApi.Setup(p => p.GetPhotos(CancellationToken.None))
                .ThrowsAsync(new ApiException(new System.Net.Http.HttpRequestMessage(), new System.Net.Http.HttpResponseMessage(), ""));

            var handler = new GetPhotosHandler(photosApi.Object, albumsApi.Object);

            Func<Task> testHandler = () => handler.Handle(new GetPhotos(), CancellationToken.None);

            testHandler.Should().ThrowExactly<RunpathAlbumWebException>();
        }
    }
}
