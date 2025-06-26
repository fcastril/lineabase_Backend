using AutoMapper;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace UnitMSTest.Service
{
    [TestClass]
    public class SecurityMigrationMapperTest
    {
        private MapperConfiguration _mapperConfiguration;
        private IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                SecurityMigrationMapper.Expresion(cfg);
            });

            _mapper = _mapperConfiguration.CreateMapper();
        }

        [TestMethod]
        public void Should_Map_SecurityMigrationDto_To_SecurityMigration_Correctly()
        {
            var securityMigrationDto = new SecurityMigrationDto
            {
                DiscoveryId = "12345",
                ProjectId = "312",
                Project = "Test Project",
                TeamId = "10",
                Team = "Test Team",
                User = "Test User",
                UserEmail = "test@example.com"
            };

            var result = _mapper.Map<SecurityMigration>(securityMigrationDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(securityMigrationDto.DiscoveryId, result.DiscoveryId);
            Assert.AreEqual(securityMigrationDto.ProjectId, result.ProjectId);
            Assert.AreEqual(securityMigrationDto.Project, result.Project);
            Assert.AreEqual(securityMigrationDto.TeamId, result.TeamId);
            Assert.AreEqual(securityMigrationDto.Team, result.Team);
            Assert.AreEqual(securityMigrationDto.User, result.User);
            Assert.AreEqual(securityMigrationDto.UserEmail, result.UserEmail);
        }
    }
}
