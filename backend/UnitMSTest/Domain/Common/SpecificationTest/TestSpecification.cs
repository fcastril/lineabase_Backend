using System;
using Domain.Specification;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitMSTest.Domain.Common.SpecificationTest {
  [TestClass]
  public class TestSpecification : SpecificationBase<int> {
    private readonly Expression<Func<int, bool>> _expression;

    public TestSpecification(Expression<Func<int, bool>> expression) {
      _expression = expression;
    }

    public override Expression<Func<int, bool>> SpecExpression => _expression;
  }

  public class SpecificationBaseTestsI {
    [TestMethod]
    public void SpecExpression_ShouldReturnCorrectExpression() {
      // Arrange
      Expression<Func<int, bool>> expression = x => x > 5;
      var specification = new TestSpecification(expression);

      // Act
      var result = specification.SpecExpression;

      // Assert
      Assert.AreEqual(expression, result);
    }

    [TestMethod]
    public void SpecExpression_ShouldEvaluateCorrectly() {
      // Arrange
      Expression<Func<int, bool>> expression = x => x > 5;
      var specification = new TestSpecification(expression);
      var compiledExpression = specification.SpecExpression.Compile();

      // Act
      var result = compiledExpression(10);

      // Assert
      Assert.IsTrue(result);
    }

    [TestMethod]
    public void SpecExpression_ShouldEvaluateIncorrectly() {
      // Arrange
      Expression<Func<int, bool>> expression = x => x > 5;
      var specification = new TestSpecification(expression);
      var compiledExpression = specification.SpecExpression.Compile();

      // Act
      var result = compiledExpression(3);

      // Assert
      Assert.IsFalse(result);
    }
  }
}
