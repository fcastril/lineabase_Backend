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
    public class EstimationOverviewServiceTest
    {
        private Mock<IEstimationOverviewRepository> estimationOverviewRepositoryMock;
        private EstimationOverviewService estimationOverviewService;

        [TestInitialize]
        public void Setup()
        {
            estimationOverviewRepositoryMock = new Mock<IEstimationOverviewRepository>();
            estimationOverviewService = new EstimationOverviewService(estimationOverviewRepositoryMock.Object);
        }

        [TestMethod]
        public async Task ShouldCountById()
        {
            estimationOverviewRepositoryMock.Setup(repo => repo.Count(It.IsAny<Expression<Func<EstimationOverview, bool>>>())).ReturnsAsync(2);

            var result = await estimationOverviewService.Count(x => x.Id == "1");

            Assert.IsNotNull(result);
        }
    }
}
