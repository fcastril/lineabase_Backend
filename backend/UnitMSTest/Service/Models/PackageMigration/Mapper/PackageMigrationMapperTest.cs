using AutoMapper;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;
using System;

namespace UnitMSTest.Service
{
    [TestClass]
    public class PackageMigrationMapperTest
    {
        private MapperConfiguration _mapperConfiguration;
        private IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                PackageMigrationMapper.Expresion(cfg);
            });

            _mapper = _mapperConfiguration.CreateMapper();
        }

        [TestMethod]
        public void Should_Map_PackageMigrationDto_To_PackageMigration_Correctly()
        {
            var packageMigrationDto = new PackageMigrationDto
            {
                DiscoveryId = "12345",
                Name = "Test Package",
                Type = "Library",
                Version = "1.0.0",
                IsDeleted = false,
                LastUpdate = DateTime.Now,
                Project = "Test Project",
                ProjectId = "100"
            };

            var result = _mapper.Map<PackageMigration>(packageMigrationDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(packageMigrationDto.DiscoveryId, result.DiscoveryId);
            Assert.AreEqual(packageMigrationDto.Name, result.Name);
            Assert.AreEqual(packageMigrationDto.Type, result.Type);
            Assert.AreEqual(packageMigrationDto.Version, result.Version);
            Assert.AreEqual(packageMigrationDto.IsDeleted, result.IsDeleted);
            Assert.AreEqual(packageMigrationDto.LastUpdate, result.LastUpdate);
            Assert.AreEqual(packageMigrationDto.Project, result.Project);
            Assert.AreEqual(packageMigrationDto.ProjectId, result.ProjectId);
        }
    }
}
