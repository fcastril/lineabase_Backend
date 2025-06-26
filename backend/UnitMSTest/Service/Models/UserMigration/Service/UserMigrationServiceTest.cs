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
    public class UserMigrationServiceTest
    {
        private Mock<IUserMigrationRepository> userMigrationRepositoryMock;
        private UserMigrationService userMigrationService;

        [TestInitialize]
        public void Setup()
        {
            userMigrationRepositoryMock = new Mock<IUserMigrationRepository>();
            userMigrationService = new UserMigrationService(userMigrationRepositoryMock.Object);
        }

        [TestMethod]
        public async Task ShouldCountById()
        {
            userMigrationRepositoryMock.Setup(repo => repo.Count(It.IsAny<Expression<Func<UserMigration, bool>>>())).ReturnsAsync(2);

            var result = await userMigrationService.Count(x => x.Id == "1");

            Assert.IsNotNull(result);
        }
    }
}
