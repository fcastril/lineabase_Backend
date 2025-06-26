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
    public class FrecuencyServiceTest
    {
        private Mock<IFrecuencyRepository> frecuencyRepositoryMock;
        private FrecuencyService frecuencyService;

        [TestInitialize]
        public void Setup()
        {
            frecuencyRepositoryMock = new Mock<IFrecuencyRepository>();
            frecuencyService = new FrecuencyService(frecuencyRepositoryMock.Object);
        }

        [TestMethod]
        public async Task ShouldCountById()
        {
            frecuencyRepositoryMock.Setup(repo => repo.Count(It.IsAny<Expression<Func<Frecuency, bool>>>())).ReturnsAsync(2);

            var result = await frecuencyService.Count(x => x.Id == "1");

            Assert.IsNotNull(result);
        }
    }
}
