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
    public class PaginateProblemDetailsTest
    {
        Mock<IBaseServiceApplication<ProblemDetails, ProblemDetailsDto>> implementationMock;
        IRequestHandler<PaginateProblemDetailsAsyncQuery, Paginate<ProblemDetailsDto>> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<ProblemDetails, ProblemDetailsDto>>();
            handler = new PaginateProblemDetailsAsyncQueryHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            CancellationToken cancellationToken = new();
            PaginateProblemDetailsAsyncQuery request = new(new(), "12345");
            Paginate<ProblemDetailsDto> paginate = new()
            {
                Count = 1,
                Page = 1,
                Data = new List<ProblemDetailsDto>() { new() { Id = "1", DiscoveryId = "12345" } }
            };

            implementationMock.Setup(x => x.Paginate(It.IsAny<Paginate<ProblemDetailsDto>>())).ReturnsAsync(paginate);
            Paginate<ProblemDetailsDto> result = await handler.Handle(request, cancellationToken);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }
    }
}
