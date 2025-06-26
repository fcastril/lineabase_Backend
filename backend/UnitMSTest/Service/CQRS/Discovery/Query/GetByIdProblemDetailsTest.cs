using Domain.Entities;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceApplication;
using ServiceApplication.CQRS;
using ServiceApplication.Dto;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UnitMSTest.Service
{
    [TestClass]
    public class GetByIdProblemDetailsTest
    {
        Mock<IBaseServiceApplication<ProblemDetails, ProblemDetailsDto>> implementationMock;
        IRequestHandler<GetByIdProblemDetailsAsyncQuery, ProblemDetailsDto> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<ProblemDetails, ProblemDetailsDto>>();
            handler = new GetByIdProblemDetailsAsyncQueryHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            DiscoveryDto discoveryDto = new() { Id = "12345", Description = "description" };
            ProblemDetailsDto problemDetailsDto = new() { Id = "1", DiscoveryId = "12345", Discovery = discoveryDto };
            GetByIdProblemDetailsAsyncQuery request = new("12345");

            implementationMock.Setup(x => x.FirstOrDefautlModelBy(It.IsAny<Expression<Func<ProblemDetails, bool>>>())).ReturnsAsync(problemDetailsDto);

            var result = await handler.Handle(request, new());

            Assert.IsNotNull(result);
            Assert.AreEqual("1", result.Id);
        }
    }
}
