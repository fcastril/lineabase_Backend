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
    public class BenefitServiceTest
    {
        private Mock<IBenefitRepository> benefitRepositoryMock;
        private Mock<ICategoryService> categoryServiceMock;
        private BenefitService benefitService;

        [TestInitialize]
        public void Setup()
        {
            benefitRepositoryMock = new Mock<IBenefitRepository>();
            categoryServiceMock = new Mock<ICategoryService>();
            benefitService = new BenefitService(benefitRepositoryMock.Object, categoryServiceMock.Object);
        }

        [TestMethod]
        public async Task ShouldCountById()
        {
            benefitRepositoryMock.Setup(repo => repo.Count(It.IsAny<Expression<Func<Benefit, bool>>>())).ReturnsAsync(2);

            var result = await benefitService.Count(x => x.Id == "1");

            Assert.IsNotNull(result);
        }
    }
}
