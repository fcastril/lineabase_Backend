using AutoMapper;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace UnitMSTest.Service
{
    [TestClass]
    public class ToolMapperTest
    {
        private MapperConfiguration _mapperConfiguration;
        private IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                ToolMapper.Expresion(cfg);
            });

            _mapper = _mapperConfiguration.CreateMapper();
        }

        [TestMethod]
        public void Should_Map_ToolDto_To_Tool_Correctly()
        {
            var toolDto = new ToolDto
            {
                Description = "Test Tool",
                Status = true
            };

            var result = _mapper.Map<Tool>(toolDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(toolDto.Description, result.Description);
            Assert.AreEqual(toolDto.Status, result.Status);
        }
    }
}
