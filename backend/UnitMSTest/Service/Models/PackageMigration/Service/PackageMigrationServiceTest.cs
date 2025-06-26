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
    public class PackageMigrationServiceTest
    {
        private Mock<IPackageMigrationRepository> packageMigrationRepositoryMock;
        private PackageMigrationService packageMigrationService;

        [TestInitialize]
        public void Setup()
        {
            packageMigrationRepositoryMock = new Mock<IPackageMigrationRepository>();
            packageMigrationService = new PackageMigrationService(packageMigrationRepositoryMock.Object);
        }

        [TestMethod]
        public async Task ShouldCountById()
        {
            packageMigrationRepositoryMock.Setup(repo => repo.Count(It.IsAny<Expression<Func<PackageMigration, bool>>>())).ReturnsAsync(2);

            var result = await packageMigrationService.Count(x => x.Id == "1");

            Assert.IsNotNull(result);
        }
    }
}
