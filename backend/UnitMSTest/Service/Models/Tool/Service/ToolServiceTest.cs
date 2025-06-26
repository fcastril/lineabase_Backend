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
    public class ToolServiceTest
    {
        private Mock<IToolRepository> toolRepositoryMock;
        private ToolService toolService;

        [TestInitialize]
        public void Setup()
        {
            toolRepositoryMock = new Mock<IToolRepository>();
            toolService = new ToolService(toolRepositoryMock.Object);
        }

        [TestMethod]
        public async Task ShouldCountById()
        {
            toolRepositoryMock.Setup(repo => repo.Count(It.IsAny<Expression<Func<Tool, bool>>>())).ReturnsAsync(2);

            var result = await toolService.Count(x => x.Id == "1");

            Assert.IsNotNull(result);
        }
    }
}
