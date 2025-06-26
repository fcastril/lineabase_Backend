using Domain.Entities;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceApplication.CQRS;
using ServiceApplication.Dto;
using ServiceApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace UnitMSTest.Service
{
    [TestClass]
    public class CreateProblemDetailsTest
    {
        Mock<IBaseServiceApplication<ProblemDetails, ProblemDetailsDto>> implementationMock;
        Mock<IBaseServiceApplication<Discovery, DiscoveryDto>> discoveryServiceMock;
        IRequestHandler<CreateProblemDetailsAsyncCommand, ProblemDetailsDto> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<ProblemDetails, ProblemDetailsDto>>();
            discoveryServiceMock = new Mock<IBaseServiceApplication<Discovery, DiscoveryDto>>();
            handler = new CreateProblemDetailsAsyncCommandHandler(implementationMock.Object, discoveryServiceMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            DiscoveryDto discoveryDto = new() { Id = "12345", Description = "description" };
            ProblemDetailsDto currentToolDto = new() { Id = "1", DiscoveryId = "12345", Discovery = discoveryDto };
            CreateProblemDetailsAsyncCommand request = new(currentToolDto);

            discoveryServiceMock.Setup(x => x.FirstOrDefautlModelBy(It.IsAny<Expression<Func<Discovery, bool>>>())).ReturnsAsync(discoveryDto);
            implementationMock.Setup(x => x.CreateModel(It.IsAny<ProblemDetailsDto>())).ReturnsAsync(currentToolDto);

            var result = await handler.Handle(request, new());

            Assert.IsNotNull(result);
            Assert.AreEqual("1", result.Id);
        }
    }
}
