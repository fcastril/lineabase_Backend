using AutoMapper;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace UnitMSTest.Service
{
    [TestClass]
    public class CategoryMapperTests
    {
        private IMapper _mapper;

        [TestInitialize]
        public void Initialize()
        {
            var config = new MapperConfiguration(cnf =>
            {
                CategoryMapper.Expresion(cnf);
            });

            _mapper = config.CreateMapper();
        }

        [TestMethod]
        public void Map_CategoryDto_To_Category_Should_Work_Correctly()
        {
            var categoryDto = new CategoryDto
            {
                Description = "Test Category",
                Status = true
            };

            var category = _mapper.Map<Category>(categoryDto);

            Assert.IsNotNull(category);
            Assert.AreEqual(categoryDto.Description, category.Description);
            Assert.AreEqual(categoryDto.Status, category.Status);
        }

        [TestMethod]
        public void Map_Null_CategoryDto_To_Category_Should_Return_Null()
        {
            CategoryDto categoryDto = null;

            var category = _mapper.Map<Category>(categoryDto);

            Assert.IsNull(category);
        }

        [TestMethod]
        public void Map_Category_To_CategoryDto_Should_Work_Correctly()
        {
            var category = new Category("Test Category", true);

            var categoryDto = _mapper.Map<CategoryDto>(category);

            Assert.IsNotNull(categoryDto);
            Assert.AreEqual(category.Description, categoryDto.Description);
            Assert.AreEqual(category.Status, categoryDto.Status);
        }

        [TestMethod]
        public void Map_Null_Category_To_CategoryDto_Should_Return_Null()
        {
            Category category = null;

            var categoryDto = _mapper.Map<CategoryDto>(category);

            Assert.IsNull(categoryDto);
        }
    }
}