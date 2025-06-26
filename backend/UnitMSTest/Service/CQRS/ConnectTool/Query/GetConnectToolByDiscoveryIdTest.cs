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
    public class GetConnectToolByDiscoveryIdTest
    {
        Mock<IBaseServiceApplication<ConnectTool, ConnectToolDto>> implementation;
        IRequestHandler<GetConnectToolByDiscoveryIdAsyncQuery, ConnectToolDto> handler;

        [TestInitialize]
        public void Init()
        {
            implementation = new Mock<IBaseServiceApplication<ConnectTool, ConnectToolDto>>();
            handler = new GetConnectToolByDiscoveryIdAsyncQueryHandler(implementation.Object);
        }

        [TestMethod]
        public async Task HandlerSuccessfulTest()
        {
            ConnectToolDto connectToolDto = new() { Id = "1", DiscoveryId = "12345", Organization = "SWO", PAT = "123" };
            GetConnectToolByDiscoveryIdAsyncQuery request = new("12345");
            implementation.Setup(x => x.FirstOrDefautlModelBy(It.IsAny<Expression<Func<ConnectTool, bool>>>())).ReturnsAsync(connectToolDto);
            var result = await handler.Handle(request, new());

            Assert.IsNotNull(result);
            Assert.AreEqual("SWO", result.Organization);
        }
    }
}
