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
    public class AreaServiceTest
    {
        private Mock<IAreaRepository> areaRepositoryMock;
        private AreaService areaService;

        [TestInitialize]
        public void Setup()
        {
            areaRepositoryMock = new Mock<IAreaRepository>();
            areaService = new AreaService(areaRepositoryMock.Object);
        }

        [TestMethod]
        public async Task ShouldCountById()
        {
            areaRepositoryMock.Setup(repo => repo.Count(It.IsAny<Expression<Func<Area, bool>>>())).ReturnsAsync(2);

            var result = await areaService.Count(x => x.Id == "1");

            Assert.IsNotNull(result);
        }
    }
}
