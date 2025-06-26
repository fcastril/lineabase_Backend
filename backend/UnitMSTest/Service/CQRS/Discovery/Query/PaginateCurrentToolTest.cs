using Domain.Entities;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceApplication;
using ServiceApplication.CQRS;
using ServiceApplication.Dto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Util.Common;

namespace UnitMSTest.Service
{
    [TestClass]
    public class PaginateCurrentToolTest
    {
        Mock<IBaseServiceApplication<CurrentTool, CurrentToolDto>> implementationMock;
        IRequestHandler<PaginateCurrentToolAsyncQuery, Paginate<CurrentToolDto>> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<CurrentTool, CurrentToolDto>>();
            handler = new PaginateCurrentToolAsyncQueryHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            CancellationToken cancellationToken = new();
            PaginateCurrentToolAsyncQuery request = new(new(), "12345");
            Paginate<CurrentToolDto> paginate = new()
            {
                Count = 1,
                Page = 1,
                Data = new List<CurrentToolDto>() { new() { Id = "1", DiscoveryId = "12345" } }
            };

            implementationMock.Setup(x => x.Paginate(It.IsAny<Paginate<CurrentToolDto>>())).ReturnsAsync(paginate);
            Paginate<CurrentToolDto> result = await handler.Handle(request, cancellationToken);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }
    }
}
