using AutoMapper;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceApplication;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace UnitMSTest.Service
{
    [TestClass]
    public class CustomerMapperTests
    {
        private IMapper _mapper;
        private Mock<IBaseServiceApplication<Sector, SectorDto>> _sectorServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            _sectorServiceMock = new Mock<IBaseServiceApplication<Sector, SectorDto>>();

            var config = new MapperConfiguration(cnf =>
            {
                CustomerMapper.Expresion(cnf, _sectorServiceMock.Object);
            });

            _mapper = config.CreateMapper();
        }

        [TestMethod]
        public void Map_CustomerDto_To_Customer_Should_Work_Correctly()
        {
            var sectorDto = new SectorDto { Description = "Test Sector", Status = true };
            var sector = new Sector("Test Sector", true);

            _sectorServiceMock
                .Setup(x => x.MapToENT<Sector, SectorDto>(It.IsAny<SectorDto>()))
                .Returns(sector);

            var customerDto = new CustomerDto
            {
                Name = "Test Customer",
                Email = "test@example.com",
                Developers = 10,
                Sector = sectorDto,
                MarketingEmails = true,
                NewsUpdate = true,
                ProductionProcess = true,
                Status = true
            };

            var customer = _mapper.Map<Customer>(customerDto);

            Assert.IsNotNull(customer);
            Assert.AreEqual(customerDto.Name, customer.Name);
            Assert.AreEqual(customerDto.Email, customer.Email);
            Assert.AreEqual(customerDto.Developers, customer.Developers);
            Assert.AreEqual(customerDto.MarketingEmails, customer.MarketingEmails);
            Assert.AreEqual(customerDto.NewsUpdate, customer.NewsUpdate);
            Assert.AreEqual(customerDto.ProductionProcess, customer.ProductionProcess);
            Assert.AreEqual(customerDto.Status, customer.Status);
            Assert.AreEqual(sector.Description, customer.Sector.Description);
        }

        [TestMethod]
        public void Map_Null_CustomerDto_To_Customer_Should_Return_Null()
        {
            CustomerDto customerDto = null;

            var customer = _mapper.Map<Customer>(customerDto);

            Assert.IsNull(customer);
        }

        [TestMethod]
        public void Map_Customer_To_CustomerDto_Should_Work_Correctly()
        {
            var sector = new Sector("Test Sector", true);
            var customer = new Customer(
                "Test Customer",
                "test@example.com",
                10,
                sector,
                true,
                true,
                true,
                true
            );

            var customerDto = _mapper.Map<CustomerDto>(customer);

            Assert.IsNotNull(customerDto);
            Assert.AreEqual(customer.Name, customerDto.Name);
            Assert.AreEqual(customer.Email, customerDto.Email);
            Assert.AreEqual(customer.Developers, customerDto.Developers);
            Assert.AreEqual(customer.MarketingEmails, customerDto.MarketingEmails);
            Assert.AreEqual(customer.NewsUpdate, customerDto.NewsUpdate);
            Assert.AreEqual(customer.ProductionProcess, customerDto.ProductionProcess);
            Assert.AreEqual(customer.Status, customerDto.Status);
            Assert.AreEqual(customer.Sector.Description, customerDto.Sector.Description);
        }

        [TestMethod]
        public void Map_Null_Customer_To_CustomerDto_Should_Return_Null()
        {
            Customer customer = null;

            var customerDto = _mapper.Map<CustomerDto>(customer);

            Assert.IsNull(customerDto);
        }
    }
}