using AutoMapper;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace UnitMSTest.Service
{
    [TestClass]
    public class FrecuencyMapperTest
    {
        private MapperConfiguration _mapperConfiguration;
        private IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                FrecuencyMapper.Expresion(cfg);
            });

            _mapper = _mapperConfiguration.CreateMapper();
        }

        [TestMethod]
        public void Should_Map_FrecuencyDto_To_Frecuency_Correctly()
        {
            var frecuencyDto = new FrecuencyDto
            {
                Name = "Test Name",
                Status = true
            };

            var result = _mapper.Map<Frecuency>(frecuencyDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(frecuencyDto.Name, result.Name);
            Assert.AreEqual(frecuencyDto.Status, result.Status);
        }
    }
}
