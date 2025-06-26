using AutoMapper;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace UnitMSTest.Service
{
    [TestClass]
    public class EstimationOverviewMapperTest
    {
        private MapperConfiguration _mapperConfiguration;
        private IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                EstimationOverviewMapper.Expresion(cfg);
            });

            _mapper = _mapperConfiguration.CreateMapper();
        }

        [TestMethod]
        public void Should_Map_EstimationOverviewDto_To_EstimationOverview_Correctly()
        {
            var estimationOverviewDto = new EstimationOverviewDto
            {
                DiscoveryId = "12345",
                Activity = "Test Activity",
                Description = "Test Description",
                Time = "10",
                Dependency = "Test Dependency",
                Category = "Test Category",
                AutomationMigration = "yes"
            };

            var result = _mapper.Map<EstimationOverview>(estimationOverviewDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(estimationOverviewDto.DiscoveryId, result.DiscoveryId);
            Assert.AreEqual(estimationOverviewDto.Activity, result.Activity);
            Assert.AreEqual(estimationOverviewDto.Description, result.Description);
            Assert.AreEqual(estimationOverviewDto.Time, result.Time);
            Assert.AreEqual(estimationOverviewDto.Dependency, result.Dependency);
            Assert.AreEqual(estimationOverviewDto.Category, result.Category);
            Assert.AreEqual(estimationOverviewDto.AutomationMigration, result.AutomationMigration);
        }
    }
}
