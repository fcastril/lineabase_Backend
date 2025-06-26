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
    public class ProblemServiceTest
    {
        private Mock<IProblemRepository> problemRepositoryMock;
        private ProblemService problemService;

        [TestInitialize]
        public void Setup()
        {
            problemRepositoryMock = new Mock<IProblemRepository>();
            problemService = new ProblemService(problemRepositoryMock.Object);
        }

        [TestMethod]
        public async Task ShouldCountById()
        {
            problemRepositoryMock.Setup(repo => repo.Count(It.IsAny<Expression<Func<Problem, bool>>>())).ReturnsAsync(2);

            var result = await problemService.Count(x => x.Id == "1");

            Assert.IsNotNull(result);
        }
    }
}
