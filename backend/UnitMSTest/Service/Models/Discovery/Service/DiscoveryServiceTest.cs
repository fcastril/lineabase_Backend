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
    public class DiscoveryServiceTest
    {
        private Mock<IDiscoveryRepository> discoveryRepositoryMock;
        Mock<IBaseServiceApplication<Customer, CustomerDto>> customerServiceMock;
        Mock<IBaseServiceApplication<Sector, SectorDto>> sectorServiceMock;
        private DiscoveryService discoveryService;

        [TestInitialize]
        public void Setup()
        {
            discoveryRepositoryMock = new Mock<IDiscoveryRepository>();
            customerServiceMock = new Mock<IBaseServiceApplication<Customer, CustomerDto>>();
            sectorServiceMock = new Mock<IBaseServiceApplication<Sector, SectorDto>>();
            discoveryService = new DiscoveryService(discoveryRepositoryMock.Object, customerServiceMock.Object, sectorServiceMock.Object);
        }

        [TestMethod]
        public async Task ShouldCountById()
        {
            discoveryRepositoryMock.Setup(repo => repo.Count(It.IsAny<Expression<Func<Discovery, bool>>>())).ReturnsAsync(2);

            var result = await discoveryService.Count(x => x.Id == "1");

            Assert.IsNotNull(result);
        }
    }
}
