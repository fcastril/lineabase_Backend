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
    public class PipelineMigrationServiceTest
    {
        private Mock<IPipelineMigrationRepository> pipelineMigrationRepositoryMock;
        private PipelineMigrationService pipelineMigrationService;

        [TestInitialize]
        public void Setup()
        {
            pipelineMigrationRepositoryMock = new Mock<IPipelineMigrationRepository>();
            pipelineMigrationService = new PipelineMigrationService(pipelineMigrationRepositoryMock.Object);
        }

        [TestMethod]
        public async Task ShouldCountById()
        {
            pipelineMigrationRepositoryMock.Setup(repo => repo.Count(It.IsAny<Expression<Func<PipelineMigration, bool>>>())).ReturnsAsync(2);

            var result = await pipelineMigrationService.Count(x => x.Id == "1");

            Assert.IsNotNull(result);
        }
    }
}
