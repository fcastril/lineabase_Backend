using AutoMapper;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceApplication;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;
using System;

namespace UnitMSTest.Service
{
    [TestClass]
    public class ProblemDetailsMapperTests
    {
        private Mock<IBaseServiceApplication<Customer, CustomerDto>> _customerServiceMock;
        private Mock<IBaseServiceApplication<Sector, SectorDto>> _sectorServiceMock;
        private Mock<IBaseServiceApplication<Discovery, DiscoveryDto>> _discoveryServiceMock;
        private Mock<IBaseServiceApplication<Category, CategoryDto>> _categoryServiceMock;
        private Mock<IBaseServiceApplication<Problem, ProblemDto>> _problemServiceMock;
        private Mock<IBaseServiceApplication<Frecuency, FrecuencyDto>> _frecuencyServiceMock;
        private MapperConfiguration _mapperConfiguration;
        private IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _customerServiceMock = new Mock<IBaseServiceApplication<Customer, CustomerDto>>();
            _sectorServiceMock = new Mock<IBaseServiceApplication<Sector, SectorDto>>();
            _discoveryServiceMock = new Mock<IBaseServiceApplication<Discovery, DiscoveryDto>>();
            _categoryServiceMock = new Mock<IBaseServiceApplication<Category, CategoryDto>>();
            _problemServiceMock = new Mock<IBaseServiceApplication<Problem, ProblemDto>>();
            _frecuencyServiceMock = new Mock<IBaseServiceApplication<Frecuency, FrecuencyDto>>();

            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                ProblemDetailsMapper.Expresion(cfg, _customerServiceMock.Object, _sectorServiceMock.Object, _discoveryServiceMock.Object, _categoryServiceMock.Object, _problemServiceMock.Object, _frecuencyServiceMock.Object);
            });

            _mapper = _mapperConfiguration.CreateMapper();
        }

        [TestMethod]
        public void Should_Map_ProblemDetailsDto_To_ProblemDetails_Correctly()
        {
            var discoveryDto = new DiscoveryDto()
            {
                Id = "1345",
                Date = DateTime.Now,
                Description = "Description",
                StatusBenefit = "OK",
                Customer = new()
            };
            var categoryDto = new CategoryDto()
            {
                Id = "1",
                Description = "Description",
                Status = true
            };
            var problemDto = new ProblemDto()
            {
                Id = "2",
                Status = true,
                Description = "Description"
            };
            var frecuencyDto = new FrecuencyDto() 
            { 
                Id = "3",
                Status = true,
                Name = "Name",
            };
            var problemDetailsDto = new ProblemDetailsDto
            {
                Discovery = discoveryDto,
                Category = categoryDto,
                Problem = problemDto,
                Impact = 2,
                Frecuency = frecuencyDto,
                Description = "Test Description"
            };

            _discoveryServiceMock.Setup(x => x.MapToENT<Discovery, DiscoveryDto>(discoveryDto)).Returns(new Discovery());
            _categoryServiceMock.Setup(x => x.MapToENT<Category, CategoryDto>(categoryDto)).Returns(new Category());
            _problemServiceMock.Setup(x => x.MapToENT<Problem, ProblemDto>(problemDto)).Returns(new Problem());
            _frecuencyServiceMock.Setup(x => x.MapToENT<Frecuency, FrecuencyDto>(frecuencyDto)).Returns(new Frecuency());

            var result = _mapper.Map<ProblemDetails>(problemDetailsDto);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Discovery);
            Assert.IsNotNull(result.Category);
            Assert.IsNotNull(result.Problem);
            Assert.IsNotNull(result.Frecuency);
            Assert.AreEqual(2, result.Impact);
            Assert.AreEqual("Test Description", result.Description);
        }

        [TestMethod]
        public void Should_Map_ProblemDetails_To_ProblemDetailsDto_Correctly()
        {
            var discovery = new Discovery();
            var category = new Category();
            var problem = new Problem();
            var frecuency = new Frecuency();
            var problemDetails = new ProblemDetails(discovery, category, problem, 2, frecuency, "Test Description");

            var result = _mapper.Map<ProblemDetailsDto>(problemDetails);

            Assert.IsNotNull(result);
            Assert.AreEqual(problemDetails.Impact, result.Impact);
            Assert.AreEqual(problemDetails.Description, result.Description);
        }
    }
}
