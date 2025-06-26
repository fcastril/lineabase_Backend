using AutoMapper;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace UnitMSTest.Service
{
    [TestClass]
    public class ProblemMapperTest
    {
        private MapperConfiguration _mapperConfiguration;
        private IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                ProblemMapper.Expresion(cfg);
            });

            _mapper = _mapperConfiguration.CreateMapper();
        }

        [TestMethod]
        public void Should_Map_ProblemDto_To_Problem_Correctly()
        {
            var problemDto = new ProblemDto
            {
                Description = "Test Problem",
                Status = true
            };

            var result = _mapper.Map<Problem>(problemDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(problemDto.Description, result.Description);
            Assert.AreEqual(problemDto.Status, result.Status);
        }

        [TestMethod]
        public void Should_Map_Problem_To_ProblemDto_Correctly()
        {
            var problem = new Problem("Test Problem", true);

            var result = _mapper.Map<ProblemDto>(problem);

            Assert.IsNotNull(result);
            Assert.AreEqual(problem.Description, result.Description);
            Assert.AreEqual(problem.Status, result.Status);
        }
    }
}
