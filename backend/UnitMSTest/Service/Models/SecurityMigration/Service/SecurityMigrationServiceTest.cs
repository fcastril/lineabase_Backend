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
    public class SecurityMigrationServiceTest
    {
        private Mock<ISecurityMigrationRepository> securityMigrationRepositoryMock;
        private SecurityMigrationService securityMigrationService;

        [TestInitialize]
        public void Setup()
        {
            securityMigrationRepositoryMock = new Mock<ISecurityMigrationRepository>();
            securityMigrationService = new SecurityMigrationService(securityMigrationRepositoryMock.Object);
        }

        [TestMethod]
        public async Task ShouldCountById()
        {
            securityMigrationRepositoryMock.Setup(repo => repo.Count(It.IsAny<Expression<Func<SecurityMigration, bool>>>())).ReturnsAsync(2);

            var result = await securityMigrationService.Count(x => x.Id == "1");

            Assert.IsNotNull(result);
        }
    }
}
