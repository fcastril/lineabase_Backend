using Domain.Entities;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceApplication;
using ServiceApplication.CQRS;
using ServiceApplication.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UnitMSTest.Service
{
    [TestClass]
    public class ToListCurrentToolTest
    {
        Mock<IBaseServiceApplication<CurrentTool, CurrentToolDto>> implementationMock;
        IRequestHandler<ToListCurrentToolAsyncQuery, List<CurrentToolDto>> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<CurrentTool, CurrentToolDto>>();
            handler = new ToListCurrentToolAsyncQueryHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            ToListCurrentToolAsyncQuery request = new("12345");
            List<CurrentToolDto> list = new()
            {
                new() { Id = "1", DiscoveryId = "12345" }
            };

            implementationMock.Setup(x => x.ToListModelBy(It.IsAny<Expression<Func<CurrentTool, bool>>>())).ReturnsAsync(list);
            List<CurrentToolDto> result = await handler.Handle(request, new());

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }
    }
}
