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
    public class CurrentToolMapperTests
    {
        private Mock<IBaseServiceApplication<Customer, CustomerDto>> _customerServiceMock;
        private Mock<IBaseServiceApplication<Sector, SectorDto>> _sectorServiceMock;
        private Mock<IBaseServiceApplication<Discovery, DiscoveryDto>> _discoveryServiceMock;
        private Mock<IBaseServiceApplication<Tool, ToolDto>> _toolServiceMock;
        private Mock<IBaseServiceApplication<Area, AreaDto>> _areaServiceMock;
        private MapperConfiguration _mapperConfiguration;
        private IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _customerServiceMock = new Mock<IBaseServiceApplication<Customer, CustomerDto>>();
            _sectorServiceMock = new Mock<IBaseServiceApplication<Sector, SectorDto>>();
            _discoveryServiceMock = new Mock<IBaseServiceApplication<Discovery, DiscoveryDto>>();
            _toolServiceMock = new Mock<IBaseServiceApplication<Tool, ToolDto>>();
            _areaServiceMock = new Mock<IBaseServiceApplication<Area, AreaDto>>();

            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                CurrentToolMapper.Expresion(cfg, _customerServiceMock.Object, _sectorServiceMock.Object, _discoveryServiceMock.Object, _toolServiceMock.Object, _areaServiceMock.Object);
            });

            _mapper = _mapperConfiguration.CreateMapper();
        }

        [TestMethod]
        public void Should_Map_CurrentToolDto_To_CurrentTool_Correctly()
        {
            var discoveryDto = new DiscoveryDto()
            {
                Id = "1345",
                Date = DateTime.Now,
                Description = "Description",
                StatusBenefit = "OK",
                Customer = new()
            };
            var toolDto = new ToolDto()
            {
                Id = "321",
                Description = "Description",
                Status = true
            };
            var currentToolDto = new CurrentToolDto
            {
                Discovery = discoveryDto,
                Tool = toolDto,
                NameServer = "TestServer"
            };

            _discoveryServiceMock.Setup(x => x.MapToENT<Discovery, DiscoveryDto>(It.IsAny<DiscoveryDto>())).Returns(new Discovery());
            _toolServiceMock.Setup(x => x.MapToENT<Tool, ToolDto>(It.IsAny<ToolDto>())).Returns(new Tool());

            var result = _mapper.Map<CurrentTool>(currentToolDto);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Discovery);
            Assert.IsNotNull(result.Tool);
            Assert.AreEqual("TestServer", result.NameServer);
        }

        [TestMethod]
        public void Should_Map_CurrentTool_To_CurrentToolDto_Correctly()
        {
            var discovery = new Discovery();
            var tool = new Tool();
            var currentTool = new CurrentTool(discovery, tool, "TestServer");

            var result = _mapper.Map<CurrentToolDto>(currentTool);

            Assert.IsNotNull(result);
            Assert.AreEqual(currentTool.NameServer, result.NameServer);
        }
    }
}
