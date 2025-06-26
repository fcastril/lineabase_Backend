using AutoMapper;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace UnitMSTest.Service
{
    [TestClass]
    public class RepositoryAnalizeGenAIMapperTest
    {
        private MapperConfiguration _mapperConfiguration;
        private IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                RepositoryAnalizeGenAIMapper.Expresion(cfg);
            });

            _mapper = _mapperConfiguration.CreateMapper();
        }

        [TestMethod]
        public void Should_Map_RepositoryAnalizeGenAIDto_To_RepositoryAnalizeGenAI_Correctly()
        {
            var repositoryAnalizeGenAIDto = new RepositoryAnalizeGenAIDto
            {
                DiscoveryId = "12345",
                RepositoryId = "54321",
                RepositoryName = "Test Repo",
                ValidationRule = "Test Rule",
                Status = "Active",
                ProblemDetail = "Test Problem",
                Suggestion = "Test Suggestion"
            };

            var result = _mapper.Map<RepositoryAnalizeGenAI>(repositoryAnalizeGenAIDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(repositoryAnalizeGenAIDto.DiscoveryId, result.DiscoveryId);
            Assert.AreEqual(repositoryAnalizeGenAIDto.RepositoryId, result.RepositoryId);
            Assert.AreEqual(repositoryAnalizeGenAIDto.RepositoryName, result.RepositoryName);
            Assert.AreEqual(repositoryAnalizeGenAIDto.ValidationRule, result.ValidationRule);
            Assert.AreEqual(repositoryAnalizeGenAIDto.Status, result.Status);
            Assert.AreEqual(repositoryAnalizeGenAIDto.ProblemDetail, result.ProblemDetail);
            Assert.AreEqual(repositoryAnalizeGenAIDto.Suggestion, result.Suggestion);
        }
    }
}
