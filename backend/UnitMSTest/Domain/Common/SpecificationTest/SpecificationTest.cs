using System;
using Domain.Specification;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitMSTest.Domain.Common.SpecificationTest {  
  [TestClass]
  public class SpecificationTest : SpecificationBase<int> {
    private readonly Expression<Func<int, bool>> _expression;

    public SpecificationTest(Expression<Func<int, bool>> expression) {
      _expression = expression;
    }

    public override Expression<Func<int, bool>> SpecExpression => _expression;
  }

  [TestClass]
  public class SpecificationBaseTests {
    [TestMethod]
    public void SpecExpression_ShouldReturnCorrectExpression() {
      // Arrange
      Expression<Func<int, bool>> expression = x => x > 5;
      var specification = new SpecificationTest(expression);

      // Act
      var result = specification.SpecExpression;

      // Assert
      Assert.AreEqual(expression, result);
    }

    [TestMethod]
    public void SpecExpression_ShouldEvaluateCorrectly() {
      // Arrange
      Expression<Func<int, bool>> expression = x => x > 5;
      var specification = new SpecificationTest(expression);
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
      var specification = new SpecificationTest(expression);
      var compiledExpression = specification.SpecExpression.Compile();

      // Act
      var result = compiledExpression(3);

      // Assert
      Assert.IsFalse(result);
    }
  }
}
