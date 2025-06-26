using Domain.Entities;
using Domain.Port;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceApplication;
using ServiceApplication.Dto;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UnitMSTest.Service
{
    [TestClass]
    public class ProblemDetailsServiceTest
    {
        private Mock<IProblemDetailsRepository> problemDetailsRepositoryMock;
        private Mock<IBaseServiceApplication<Customer, CustomerDto>> customerServiceMock;
        private Mock<IBaseServiceApplication<Sector, SectorDto>> sectorServiceMock;
        private Mock<IBaseServiceApplication<Discovery, DiscoveryDto>> discoveryServiceMock;
        private Mock<IBaseServiceApplication<Category, CategoryDto>> categoryServiceMock;
        private Mock<IBaseServiceApplication<Problem, ProblemDto>> problemServiceMock;
        private Mock<IBaseServiceApplication<Frecuency, FrecuencyDto>> frecuencyServiceMock;
        private ProblemDetailsService problemDetailsService;

        [TestInitialize]
        public void Setup()
        {
            problemDetailsRepositoryMock = new Mock<IProblemDetailsRepository>();
            customerServiceMock = new Mock<IBaseServiceApplication<Customer, CustomerDto>>();
            sectorServiceMock = new Mock<IBaseServiceApplication<Sector, SectorDto>>();
            discoveryServiceMock = new Mock<IBaseServiceApplication<Discovery, DiscoveryDto>>();
            categoryServiceMock = new Mock<IBaseServiceApplication<Category, CategoryDto>>();
            problemServiceMock = new Mock<IBaseServiceApplication<Problem, ProblemDto>>();
            frecuencyServiceMock = new Mock<IBaseServiceApplication<Frecuency, FrecuencyDto>>();
            problemDetailsService = new ProblemDetailsService(problemDetailsRepositoryMock.Object, customerServiceMock.Object,
                                                              sectorServiceMock.Object, discoveryServiceMock.Object,
                                                              categoryServiceMock.Object, problemServiceMock.Object, frecuencyServiceMock.Object);
        }

        [TestMethod]
        public async Task ShouldCountById()
        {
            problemDetailsRepositoryMock.Setup(repo => repo.Count(It.IsAny<Expression<Func<ProblemDetails, bool>>>())).ReturnsAsync(2);

            var result = await problemDetailsService.Count(x => x.Id == "1");

            Assert.IsNotNull(result);
        }
    }
}
