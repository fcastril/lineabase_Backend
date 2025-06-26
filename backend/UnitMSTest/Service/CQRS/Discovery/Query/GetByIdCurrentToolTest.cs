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
    public class GetByIdCurrentToolTest
    {
        Mock<IBaseServiceApplication<CurrentTool, CurrentToolDto>> implementationMock;
        IRequestHandler<GetByIdCurrentToolAsyncQuery, CurrentToolDto> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<CurrentTool, CurrentToolDto>>();
            handler = new GetByIdCurrentToolAsyncQueryHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            DiscoveryDto discoveryDto = new() { Id = "12345", Description = "description" };
            CurrentToolDto currentToolDto = new() { Id = "1", DiscoveryId = "12345", Discovery = discoveryDto };
            GetByIdCurrentToolAsyncQuery request = new("12345");

            implementationMock.Setup(x => x.FirstOrDefautlModelBy(It.IsAny<Expression<Func<CurrentTool, bool>>>())).ReturnsAsync(currentToolDto);

            var result = await handler.Handle(request, new());

            Assert.IsNotNull(result);
            Assert.AreEqual("1", result.Id);
        }
    }
}
