using AutoMapper;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace UnitMSTest.Service
{
    [TestClass]
    public class ExpectedBenefitMapperTest
    {
        private MapperConfiguration _mapperConfiguration;
        private IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                ExpectedBenefitMapper.Expresion(cfg);
            });

            _mapper = _mapperConfiguration.CreateMapper();
        }

        [TestMethod]
        public void Should_Map_ExpectedBenefitDto_To_ExpectedBenefit_Correctly()
        {
            var expectedBenefitDto = new ExpectedBenefitDto
            {
                Category = "Test Category",
                Description = "Test Description",
                ExpectedROI = "15.5",
                FigureFrom = 1000,
                FigureTo = 5000,
                DiscoveryId = "12345"
            };

            var result = _mapper.Map<ExpectedBenefit>(expectedBenefitDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedBenefitDto.Category, result.Category);
            Assert.AreEqual(expectedBenefitDto.Description, result.Description);
            Assert.AreEqual(expectedBenefitDto.ExpectedROI, result.ExpectedROI);
            Assert.AreEqual(expectedBenefitDto.FigureFrom, result.FigureFrom);
            Assert.AreEqual(expectedBenefitDto.FigureTo, result.FigureTo);
            Assert.AreEqual(expectedBenefitDto.DiscoveryId, result.DiscoveryId);
        }
    }
}
