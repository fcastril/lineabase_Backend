using Domain.Entities;
using Domain.Port;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceApplication;
using ServiceApplication.Dto;
using ServiceApplication.Events;
using ServiceApplication.Port;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace UnitMSTest.Service
{
    [TestClass]
    public class ConnectToolsServiceTest
    {
        private Mock<IConnectToolRepository> connectToolsRepositoryMock;
        private Mock<IMessageSender<CommGeneric>> messageSenderMock;
        private ConnectToolService connectToolsService;

        [TestInitialize]
        public void Setup()
        {
            connectToolsRepositoryMock = new Mock<IConnectToolRepository>();
            messageSenderMock = new Mock<IMessageSender<CommGeneric>>();
            connectToolsService = new ConnectToolService(connectToolsRepositoryMock.Object, messageSenderMock.Object);
        }

        [TestMethod]
        public async Task ShouldCountById()
        {
            connectToolsRepositoryMock.Setup(repo => repo.Count(It.IsAny<Expression<Func<ConnectTool, bool>>>())).ReturnsAsync(2);

            var result = await connectToolsService.Count(x => x.Id == "1");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task CreateModelSuccessfulTest()
        {
            ConnectToolDto dto = new()
            {
                DiscoveryId = "1",
                Id = "1",
                Organization = "SWO",
                PAT = "12354"
            };

            connectToolsRepositoryMock.Setup(repo => repo.CreateModel(It.IsAny<ConnectTool>())).ReturnsAsync(() => new());
            messageSenderMock.Setup(x => x.SendCommAsync(It.IsAny<CommGeneric>(), It.IsAny<CancellationToken>()));

            var result = await connectToolsService.CreateModel(dto);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UpdateModelSuccessfulTest()
        {
            ConnectToolDto dto = new()
            {
                DiscoveryId = "1",
                Id = "1",
                Organization = "SWO",
                PAT = "12354"
            };

            connectToolsRepositoryMock.Setup(repo => repo.UpdateModel(It.IsAny<ConnectTool>())).ReturnsAsync(() => new());
            messageSenderMock.Setup(x => x.SendCommAsync(It.IsAny<CommGeneric>(), It.IsAny<CancellationToken>()));

            var result = await connectToolsService.UpdateModel(dto);

            Assert.IsNotNull(result);
        }
    }
}
