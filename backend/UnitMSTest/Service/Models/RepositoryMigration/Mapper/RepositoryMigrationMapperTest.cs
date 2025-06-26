using AutoMapper;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace UnitMSTest.Service
{
    [TestClass]
    public class RepositoryMigrationMapperTest
    {
        private MapperConfiguration _mapperConfiguration;
        private IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                RepositoryMigrationMapper.Expresion(cfg);
            });

            _mapper = _mapperConfiguration.CreateMapper();
        }

        [TestMethod]
        public void Should_Map_RepositoryMigrationDto_To_RepositoryMigration_Correctly()
        {
            var repositoryMigrationDto = new RepositoryMigrationDto
            {
                DiscoveryId = "12345",
                Assessment = "Test Assessment",
                Name = "Test Repository",
                Description = "Test Description",
                Size = 12345,
                BranchCount = 5,
                ActivePullRequest = true,
                LastCommit = System.DateTime.Now,
                IsDisable = false,
                DefaultBranch = "main",
                Branchs = "main, dev",
                FrecuencyCommits = "Weekly",
                FileExtensions = ".cs, .json"
            };

            var result = _mapper.Map<RepositoryMigration>(repositoryMigrationDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(repositoryMigrationDto.DiscoveryId, result.DiscoveryId);
            Assert.AreEqual(repositoryMigrationDto.Assessment, result.Assessment);
            Assert.AreEqual(repositoryMigrationDto.Name, result.Name);
            Assert.AreEqual(repositoryMigrationDto.Description, result.Description);
            Assert.AreEqual(repositoryMigrationDto.Size, result.Size);
            Assert.AreEqual(repositoryMigrationDto.BranchCount, result.BranchCount);
            Assert.AreEqual(repositoryMigrationDto.ActivePullRequest, result.ActivePullRequest);
            Assert.AreEqual(repositoryMigrationDto.LastCommit, result.LastCommit);
            Assert.AreEqual(repositoryMigrationDto.IsDisable, result.IsDisabled);
            Assert.AreEqual(repositoryMigrationDto.DefaultBranch, result.DefaultBranch);
            Assert.AreEqual(repositoryMigrationDto.Branchs, result.Branchs);
            Assert.AreEqual(repositoryMigrationDto.FrecuencyCommits, result.FrecuencyCommits);
            Assert.AreEqual(repositoryMigrationDto.FileExtensions, result.FileExtensions);
        }
    }
}
