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
    public class PromptServiceTest
    {
        private Mock<IPromptRepository> promptRepositoryMock;
        private PromptService promptService;

        [TestInitialize]
        public void Setup()
        {
            promptRepositoryMock = new Mock<IPromptRepository>();
            promptService = new PromptService(promptRepositoryMock.Object);
        }

        [TestMethod]
        public async Task ShouldCountById()
        {
            promptRepositoryMock.Setup(repo => repo.Count(It.IsAny<Expression<Func<Prompt, bool>>>())).ReturnsAsync(2);

            var result = await promptService.Count(x => x.Id == "1");

            Assert.IsNotNull(result);
        }
    }
}
