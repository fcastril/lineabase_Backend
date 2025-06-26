using AutoMapper;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace UnitMSTest.Service
{
    [TestClass]
    public class BenefitMapperTests
    {
        private IMapper _mapper;

        [TestInitialize]
        public void Initialize()
        {
            var config = new MapperConfiguration(cnf =>
            {
                BenefitMapper.Expresion(cnf);
            });

            _mapper = config.CreateMapper();
        }

        [TestMethod]
        public void Map_BenefitDto_To_Benefit_Should_Work_Correctly()
        {
            var benefitDto = new BenefitDto
            {
                Category = "Test Category",
                CurrentProblem = "Test Problem",
                CurrentTool = "Test Tool",
                NewTool = "New Tool",
                MigrationBenefit = "Test Benefit",
                IsRelevant = true,
                DiscoveryId = "TestDiscoveryId"
            };

            var benefit = _mapper.Map<Benefit>(benefitDto);

            Assert.IsNotNull(benefit);
            Assert.AreEqual(benefitDto.Category, benefit.Category);
            Assert.AreEqual(benefitDto.CurrentProblem, benefit.CurrentProblem);
            Assert.AreEqual(benefitDto.CurrentTool, benefit.CurrentTool);
            Assert.AreEqual(benefitDto.NewTool, benefit.NewTool);
            Assert.AreEqual(benefitDto.MigrationBenefit, benefit.MigrationBenefit);
            Assert.AreEqual(benefitDto.IsRelevant, benefit.IsRelevant);
            Assert.AreEqual(benefitDto.DiscoveryId, benefit.DiscoveryId);
        }

        [TestMethod]
        public void Map_Null_BenefitDto_To_Benefit_Should_Return_Null()
        {
            BenefitDto benefitDto = null;

            var benefit = _mapper.Map<Benefit>(benefitDto);

            Assert.IsNull(benefit);
        }
    }
}