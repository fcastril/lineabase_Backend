using AutoMapper;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace UnitMSTest.Service
{
    [TestClass]
    public class UserMigrationMapperTest
    {
        private MapperConfiguration _mapperConfiguration;
        private IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                UserMigrationMapper.Expresion(cfg);
            });

            _mapper = _mapperConfiguration.CreateMapper();
        }

        [TestMethod]
        public void Should_Map_UserMigrationDto_To_UserMigration_Correctly()
        {
            var userMigrationDto = new UserMigrationDto
            {
                DiscoveryId = "12345",
                Name = "Test User",
                Email = "test@example.com",
                Role = "Admin",
                Tool = "Some Tool",
                LastAccess = System.DateTime.Now
            };

            var result = _mapper.Map<UserMigration>(userMigrationDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(userMigrationDto.DiscoveryId, result.DiscoveryId);
            Assert.AreEqual(userMigrationDto.Name, result.Name);
            Assert.AreEqual(userMigrationDto.Email, result.Email);
            Assert.AreEqual(userMigrationDto.Role, result.Role);
            Assert.AreEqual(userMigrationDto.Tool, result.Tool);
            Assert.AreEqual(userMigrationDto.LastAccess, result.LastAccess);
        }
    }
}
