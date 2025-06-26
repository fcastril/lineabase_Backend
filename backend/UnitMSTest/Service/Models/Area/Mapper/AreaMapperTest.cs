using AutoMapper;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace UnitMSTest.Service
{
    [TestClass]
    public class AreaMapperTest
    {
        private IMapper _mapper;

        [TestInitialize]
        public void Initialize()
        {
            // Configurar AutoMapper
            var config = new MapperConfiguration(cnf =>
            {
                AreaMapper.Expresion(cnf);
            });

            _mapper = config.CreateMapper();
        }

        [TestMethod]
        public void Map_AreaDto_To_Area_Should_Work_Correctly()
        {
            // Arrange
            var areaDto = new AreaDto
            {
                Description = "Test Area",
                Status = true
            };

            // Act
            var area = _mapper.Map<Area>(areaDto);

            // Assert
            Assert.IsNotNull(area);
            Assert.AreEqual(areaDto.Description, area.Description);
            Assert.AreEqual(areaDto.Status, area.Status);
        }

        [TestMethod]
        public void Map_Null_AreaDto_To_Area_Should_Return_Null()
        {
            // Arrange
            AreaDto areaDto = null;

            // Act
            var area = _mapper.Map<Area>(areaDto);

            // Assert
            Assert.IsNull(area);
        }

        [TestMethod]
        public void Map_Area_To_AreaDto_Should_Work_Correctly()
        {
            // Arrange
            var area = new Area("Test Area", true);

            // Act
            var areaDto = _mapper.Map<AreaDto>(area);

            // Assert
            Assert.IsNotNull(areaDto);
            Assert.AreEqual(area.Description, areaDto.Description);
            Assert.AreEqual(area.Status, areaDto.Status);
        }

        [TestMethod]
        public void Map_Null_Area_To_AreaDto_Should_Return_Null()
        {
            // Arrange
            Area area = null;

            // Act
            var areaDto = _mapper.Map<AreaDto>(area);

            // Assert
            Assert.IsNull(areaDto);
        }
    }
}
