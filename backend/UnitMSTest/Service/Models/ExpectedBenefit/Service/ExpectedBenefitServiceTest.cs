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
    public class ExpectedBenefitServiceTest
    {
        private Mock<IExpectedBenefitRepository> expectedBenefitRepositoryMock;
        private ExpectedBenefitService expectedBenefitService;

        [TestInitialize]
        public void Setup()
        {
            expectedBenefitRepositoryMock = new Mock<IExpectedBenefitRepository>();
            expectedBenefitService = new ExpectedBenefitService(expectedBenefitRepositoryMock.Object);
        }

        [TestMethod]
        public async Task ShouldCountById()
        {
            expectedBenefitRepositoryMock.Setup(repo => repo.Count(It.IsAny<Expression<Func<ExpectedBenefit, bool>>>())).ReturnsAsync(2);

            var result = await expectedBenefitService.Count(x => x.Id == "1");

            Assert.IsNotNull(result);
        }
    }
}
