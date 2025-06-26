using System;
using System.Linq.Expressions;
using Domain.Common.Specification;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitMSTest.Domain.Common.SpecificationTest {
  [TestClass]
  public class ParameterReplacerTests {
    [TestMethod]
    public void Replace_ShouldReplaceParameterInExpression() {
      // Arrange
      Expression<Func<int, bool>> originalExpression = x => x > 5;
      var newParameter = Expression.Parameter(typeof(int), "y");

      // Act
      var replacedExpression = ParameterReplacer.Replace(newParameter, originalExpression);

      // Assert
      Assert.IsNotNull(replacedExpression);
      var lambda = (Expression<Func<int, bool>>)replacedExpression;
      Assert.AreEqual("y", lambda.Parameters[0].Name);
    }

    [TestMethod]
    public void Replace_ShouldNotReplaceParameterIfTypesDoNotMatch() {
      // Arrange
      Expression<Func<int, bool>> originalExpression = x => x > 5;
      var newParameter = Expression.Parameter(typeof(string), "y");

      // Act
      var replacedExpression = ParameterReplacer.Replace(newParameter, originalExpression);

      // Assert
      Assert.IsNotNull(replacedExpression);
      var lambda = (Expression<Func<int, bool>>)replacedExpression;
      Assert.AreEqual("x", lambda.Parameters[0].Name);
    }
  }
}
