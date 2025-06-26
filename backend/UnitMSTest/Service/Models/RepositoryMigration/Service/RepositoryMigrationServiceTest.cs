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
    public class RepositoryMigrationServiceTest
    {
        private Mock<IRepositoryMigrationRepository> repositoryMigrationRepositoryMock;
        private RepositoryMigrationService repositoryMigrationService;

        [TestInitialize]
        public void Setup()
        {
            repositoryMigrationRepositoryMock = new Mock<IRepositoryMigrationRepository>();
            repositoryMigrationService = new RepositoryMigrationService(repositoryMigrationRepositoryMock.Object);
        }

        [TestMethod]
        public async Task ShouldCountById()
        {
            repositoryMigrationRepositoryMock.Setup(repo => repo.Count(It.IsAny<Expression<Func<RepositoryMigration, bool>>>())).ReturnsAsync(2);

            var result = await repositoryMigrationService.Count(x => x.Id == "1");

            Assert.IsNotNull(result);
        }
    }
}
