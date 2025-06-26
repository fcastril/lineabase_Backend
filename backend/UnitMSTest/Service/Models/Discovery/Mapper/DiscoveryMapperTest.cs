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
    public class DiscoveryMapperTests
    {
        private Mock<IBaseServiceApplication<Customer, CustomerDto>> _customerServiceMock;
        private Mock<IBaseServiceApplication<Sector, SectorDto>> _sectorServiceMock;
        private MapperConfiguration _mapperConfiguration;
        private IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _customerServiceMock = new Mock<IBaseServiceApplication<Customer, CustomerDto>>();
            _sectorServiceMock = new Mock<IBaseServiceApplication<Sector, SectorDto>>();

            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                DiscoveryMapper.Expresion(cfg, _customerServiceMock.Object, _sectorServiceMock.Object);
            });

            _mapper = _mapperConfiguration.CreateMapper();
        }

        [TestMethod]
        public void Should_Map_DiscoveryDto_To_Discovery_Correctly()
        {
            var customerDto = new CustomerDto();
            var discoveryDto = new DiscoveryDto
            {
                Customer = customerDto,
                Date = System.DateTimeOffset.Now,
                Description = "Test Description"
            };

            _customerServiceMock.Setup(x => x.MapToENT<Customer, CustomerDto>(customerDto)).Returns(new Customer());

            var result = _mapper.Map<Discovery>(discoveryDto);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Customer);
            Assert.AreEqual(discoveryDto.Date, result.Date);
            Assert.AreEqual("Test Description", result.Description);
        }

        [TestMethod]
        public void Should_Map_Discovery_To_DiscoveryDto_Correctly()
        {
            var customer = new Customer();
            var discovery = new Discovery(customer, System.DateTimeOffset.Now, "Test Description");

            var result = _mapper.Map<DiscoveryDto>(discovery);

            Assert.IsNotNull(result);
            Assert.AreEqual(discovery.Date, result.Date);
            Assert.AreEqual(discovery.Description, result.Description);
        }
    }
}
