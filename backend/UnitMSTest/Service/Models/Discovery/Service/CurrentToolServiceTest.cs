using Domain.Entities;
using Domain.Port;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceApplication;
using ServiceApplication.Dto;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UnitMSTest.Service
{
    [TestClass]
    public class CurrentToolServiceTest
    {
        private Mock<ICurrentToolRepository> currentToolRepositoryMock;
        private Mock<IBaseServiceApplication<Customer, CustomerDto>> customerServiceMock;
        private Mock<IBaseServiceApplication<Sector, SectorDto>> sectorServiceMock;
        private Mock<IBaseServiceApplication<Discovery, DiscoveryDto>> discoveryServiceMock;
        private Mock<IBaseServiceApplication<Tool, ToolDto>> toolServiceMock;
        private Mock<IBaseServiceApplication<Area, AreaDto>> areaServiceMock;
        private CurrentToolService currentToolService;

        [TestInitialize]
        public void Setup()
        {
            currentToolRepositoryMock = new Mock<ICurrentToolRepository>();
            customerServiceMock = new Mock<IBaseServiceApplication<Customer, CustomerDto>>();
            sectorServiceMock = new Mock<IBaseServiceApplication<Sector, SectorDto>>();
            discoveryServiceMock = new Mock<IBaseServiceApplication<Discovery, DiscoveryDto>>();
            toolServiceMock = new Mock<IBaseServiceApplication<Tool, ToolDto>>();
            areaServiceMock = new Mock<IBaseServiceApplication<Area, AreaDto>>();
            currentToolService = new CurrentToolService(currentToolRepositoryMock.Object, customerServiceMock.Object,
                                                        sectorServiceMock.Object, discoveryServiceMock.Object,
                                                        toolServiceMock.Object, areaServiceMock.Object);
        }

        [TestMethod]
        public async Task ShouldCountById()
        {
            currentToolRepositoryMock.Setup(repo => repo.Count(It.IsAny<Expression<Func<CurrentTool, bool>>>())).ReturnsAsync(2);

            var result = await currentToolService.Count(x => x.Id == "1");

            Assert.IsNotNull(result);
        }
    }
}
