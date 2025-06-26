using AutoMapper;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace UnitMSTest.Service
{
    [TestClass]
    public class PipelineMigrationMapperTest
    {
        private MapperConfiguration _mapperConfiguration;
        private IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                PipelineMigrationMapper.Expresion(cfg);
            });

            _mapper = _mapperConfiguration.CreateMapper();
        }

        [TestMethod]
        public void Should_Map_PipelineMigrationDto_To_PipelineMigration_Correctly()
        {
            var pipelineMigrationDto = new PipelineMigrationDto
            {
                DiscoveryId = "12345",
                Name = "Test Pipeline",
                StatusLastExecution = "Success",
                SourceBranch = "main",
                LastExecution = System.DateTime.Now
            };

            var result = _mapper.Map<PipelineMigration>(pipelineMigrationDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(pipelineMigrationDto.DiscoveryId, result.DiscoveryId);
            Assert.AreEqual(pipelineMigrationDto.Name, result.Name);
            Assert.AreEqual(pipelineMigrationDto.StatusLastExecution, result.StatusLastExecution);
            Assert.AreEqual(pipelineMigrationDto.SourceBranch, result.SourceBranch);
            Assert.AreEqual(pipelineMigrationDto.LastExecution, result.LastExecution);
        }
    }
}
