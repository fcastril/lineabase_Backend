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
    public class CustomerServiceTest
    {
        private Mock<ICustomerRepository> customerRepositoryMock;
        private Mock<IBaseServiceApplication<Sector, SectorDto>> sectorServiceMock;
        private CustomerService customerService;

        [TestInitialize]
        public void Setup()
        {
            customerRepositoryMock = new Mock<ICustomerRepository>();
            sectorServiceMock = new Mock<IBaseServiceApplication<Sector, SectorDto>>();
            customerService = new CustomerService(customerRepositoryMock.Object, sectorServiceMock.Object);
        }

        [TestMethod]
        public async Task ShouldCountById()
        {
            customerRepositoryMock.Setup(repo => repo.Count(It.IsAny<Expression<Func<Customer, bool>>>())).ReturnsAsync(2);

            var result = await customerService.Count(x => x.Id == "1");

            Assert.IsNotNull(result);
        }
    }
}
