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
    public class CategoryServiceTest
    {
        private Mock<ICategoryRepository> categoryRepositoryMock;
        private CategoryService categoryService;

        [TestInitialize]
        public void Setup()
        {
            categoryRepositoryMock = new Mock<ICategoryRepository>();
            categoryService = new CategoryService(categoryRepositoryMock.Object);
        }

        [TestMethod]
        public async Task ShouldCountById()
        {
            categoryRepositoryMock.Setup(repo => repo.Count(It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync(2);

            var result = await categoryService.Count(x => x.Id == "1");

            Assert.IsNotNull(result);
        }
    }
}
