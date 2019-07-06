using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestEase;
using Web.Controllers;
using Web.Exceptions;
using Web.Handlers;
using Web.ViewModels;
using Xunit;

namespace Web.UnitTests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public async Task Data_sends_correct_message_to_mediator()
        {
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<GetPhotos>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Enumerable.Empty<PhotoViewModel>())
                .Verifiable();

            var controller = new HomeController(mediator.Object);

            await controller.Data(CancellationToken.None);

            mediator.VerifyAll();
        }

        [Fact]
        public async Task Data_returns_expected_data_when_invoked()
        {
            var data = new[]
            {
                new PhotoViewModel()
                {
                    AlbumName = "Name1",
                    PhotoTitle = "Title1",
                    ThumbnailUrl = new Uri("http://localhost/thumb/1"),
                    Url = new Uri("http://localhost/1")
                },
                new PhotoViewModel()
                {
                    AlbumName = "Name2",
                    PhotoTitle = "Title2",
                    ThumbnailUrl = new Uri("http://localhost/thumb/2"),
                    Url = new Uri("http://localhost/2")
                }
            };

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<GetPhotos>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(data);

            var controller = new HomeController(mediator.Object);

            var result = await controller.Data(CancellationToken.None);

            result.Should().NotBeNull()
                .And.BeOfType<JsonResult>()
                .Which.Value.Should().BeOfType<PhotoViewModel[]>()
                .Which.Should().BeEquivalentTo(data);
        }

        [Fact]
        public async Task Data_returns_500_status_code_when_mediator_throws_exception()
        {
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<GetPhotos>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new RunpathAlbumWebException());

            var controller = new HomeController(mediator.Object);

            var result = await controller.Data(CancellationToken.None);

            result.Should().NotBeNull()
                .And.BeOfType<StatusCodeResult>()
                .Which.StatusCode.Should().Be(500);
        }
    }
}
