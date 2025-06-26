using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceApplication;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UnitMSTest.Service
{
    [TestClass]
    public class OverviewMigrationServiceTest
    {
        private Mock<IUserMigrationService> userMigrationServiceMock;
        private Mock<IRepositoryMigrationService> repositoryMigrationServiceMock;
        private Mock<IPackageMigrationService> packageMigrationServiceMock;
        private Mock<IPipelineMigrationService> pipelineMigrationServiceMock;
        private Mock<ISecurityMigrationService> securityMigrationServiceMock;
        private OverviewMigrationService overviewMigrationService;

        [TestInitialize]
        public void Setup()
        {
            userMigrationServiceMock = new Mock<IUserMigrationService>();
            repositoryMigrationServiceMock = new Mock<IRepositoryMigrationService>();
            packageMigrationServiceMock = new Mock<IPackageMigrationService>();
            pipelineMigrationServiceMock = new Mock<IPipelineMigrationService>();
            securityMigrationServiceMock = new Mock<ISecurityMigrationService>();
            overviewMigrationService = new OverviewMigrationService(userMigrationServiceMock.Object, repositoryMigrationServiceMock.Object,
                                                                    packageMigrationServiceMock.Object, pipelineMigrationServiceMock.Object,
                                                                    securityMigrationServiceMock.Object);
        }

        [TestMethod]
        public async Task GetOverviewSuccessfulTest()
        {
            userMigrationServiceMock.Setup(repo => repo.Count(It.IsAny<Expression<Func<UserMigration, bool>>>())).ReturnsAsync(2);
            repositoryMigrationServiceMock.Setup(repo => repo.Count(It.IsAny<Expression<Func<RepositoryMigration, bool>>>())).ReturnsAsync(2);
            packageMigrationServiceMock.Setup(repo => repo.Count(It.IsAny<Expression<Func<PackageMigration, bool>>>())).ReturnsAsync(2);
            pipelineMigrationServiceMock.Setup(repo => repo.Count(It.IsAny<Expression<Func<PipelineMigration, bool>>>())).ReturnsAsync(2);
            securityMigrationServiceMock.Setup(repo => repo.Count(It.IsAny<Expression<Func<SecurityMigration, bool>>>())).ReturnsAsync(2);

            var result = await overviewMigrationService.GetOverview("12345");

            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Count);
        }
    }
}
