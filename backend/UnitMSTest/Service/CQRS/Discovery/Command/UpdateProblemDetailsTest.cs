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
    public class UpdateProblemDetailsTest
    {
        Mock<IBaseServiceApplication<ProblemDetails, ProblemDetailsDto>> implementationMock;
        Mock<IBaseServiceApplication<Discovery, DiscoveryDto>> discoveryServiceMock;
        IRequestHandler<UpdateProblemDetailsAsyncCommand, ProblemDetailsDto> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<ProblemDetails, ProblemDetailsDto>>();
            discoveryServiceMock = new Mock<IBaseServiceApplication<Discovery, DiscoveryDto>>();
            handler = new UpdateProblemDetailsAsyncCommandHandler(implementationMock.Object, discoveryServiceMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            DiscoveryDto discoveryDto = new() { Id = "12345", Description = "description" };
            ProblemDetailsDto currentToolDto = new() { Id = "1", DiscoveryId = "12345", Discovery = discoveryDto };
            UpdateProblemDetailsAsyncCommand request = new(currentToolDto);

            discoveryServiceMock.Setup(x => x.FirstOrDefautlModelBy(It.IsAny<Expression<Func<Discovery, bool>>>())).ReturnsAsync(discoveryDto);
            implementationMock.Setup(x => x.UpdateModel(It.IsAny<ProblemDetailsDto>())).ReturnsAsync(currentToolDto);

            var result = await handler.Handle(request, new());

            Assert.IsNotNull(result);
            Assert.AreEqual("1", result.Id);
        }
    }
}
