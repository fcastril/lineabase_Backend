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
    public class RepositoryAnalizeGenAIServiceTest
    {
        private Mock<IRepositoryAnalizeGenAIRepository> repositoryAnalizeGenAIRepositoryMock;
        private RepositoryAnalizeGenAIService repositoryAnalizeGenAIService;

        [TestInitialize]
        public void Setup()
        {
            repositoryAnalizeGenAIRepositoryMock = new Mock<IRepositoryAnalizeGenAIRepository>();
            repositoryAnalizeGenAIService = new RepositoryAnalizeGenAIService(repositoryAnalizeGenAIRepositoryMock.Object);
        }

        [TestMethod]
        public async Task ShouldCountById()
        {
            repositoryAnalizeGenAIRepositoryMock.Setup(repo => repo.Count(It.IsAny<Expression<Func<RepositoryAnalizeGenAI, bool>>>())).ReturnsAsync(2);

            var result = await repositoryAnalizeGenAIService.Count(x => x.Id == "1");

            Assert.IsNotNull(result);
        }
    }
}
