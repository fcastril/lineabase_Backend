using Domain.Entities;
using Domain.Port;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceApplication;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UnitMSTest.Service
{
    [TestClass]
    public class SectorServiceTest
    {
        private Mock<ISectorRepository> sectorRepositoryMock;
        private SectorService sectorService;

        [TestInitialize]
        public void Setup()
        {
            sectorRepositoryMock = new Mock<ISectorRepository>();
            sectorService = new SectorService(sectorRepositoryMock.Object);
        }

        [TestMethod]
        public async Task ShouldCountById()
        {
            sectorRepositoryMock.Setup(repo => repo.Count(It.IsAny<Expression<Func<Sector, bool>>>())).ReturnsAsync(2);

            var result = await sectorService.Count(x => x.Id == "1");

            Assert.IsNotNull(result);
        }
    }
}
