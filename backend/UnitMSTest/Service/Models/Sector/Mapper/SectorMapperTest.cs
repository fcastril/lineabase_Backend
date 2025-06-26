using AutoMapper;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace UnitMSTest.Service
{
    [TestClass]
    public class SectorMapperTest
    {
        private MapperConfiguration _mapperConfiguration;
        private IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                SectorMapper.Expresion(cfg);
            });

            _mapper = _mapperConfiguration.CreateMapper();
        }

        [TestMethod]
        public void Should_Map_SectorDto_To_Sector_Correctly()
        {
            var sectorDto = new SectorDto
            {
                Description = "Test Sector",
                Status = true
            };

            var result = _mapper.Map<Sector>(sectorDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(sectorDto.Description, result.Description);
            Assert.AreEqual(sectorDto.Status, result.Status);
        }

        [TestMethod]
        public void Should_Map_Sector_To_SectorDto_Correctly()
        {
            var sector = new Sector("Test Sector", true);

            var result = _mapper.Map<SectorDto>(sector);

            Assert.IsNotNull(result);
            Assert.AreEqual(sector.Description, result.Description);
            Assert.AreEqual(sector.Status, result.Status);
        }
    }
}