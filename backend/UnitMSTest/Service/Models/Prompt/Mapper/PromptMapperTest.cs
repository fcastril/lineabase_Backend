using AutoMapper;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace UnitMSTest.Service
{
    [TestClass]
    public class PromptMapperTest
    {
        private MapperConfiguration _mapperConfiguration;
        private IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                PromptMapper.Expresion(cfg);
            });

            _mapper = _mapperConfiguration.CreateMapper();
        }

        [TestMethod]
        public void Should_Map_PromptDto_To_Prompt_Correctly()
        {
            var promptDto = new PromptDto
            {
                Name = "Test Prompt",
                Body = "Test Body",
                Overrides = "Test Overrides"
            };

            var result = _mapper.Map<Prompt>(promptDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(promptDto.Name, result.Name);
            Assert.AreEqual(promptDto.Body, result.Body);
            Assert.AreEqual(promptDto.Overrides, result.Overrides);
        }
    }
}
