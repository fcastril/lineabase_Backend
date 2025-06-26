using System;
using System.Linq;
using Infrastructure;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitMSTest.Infrastructure.Repository.Bases.PaginatorTests {
  //namespace Infrastructure.Tests {
  [TestClass]
  public class PaginatorTests {
    [TestMethod]
    public void Paginator_Constructor_Success() {
      // Arrange
      var items = new List<string> { "item1", "item2", "item3" };
      int count = items.Count;
      int pageIndex = 1;
      int pageSize = 2;

      // Act
      var paginator = new Paginator<string>(items, count, pageIndex, pageSize);

      // Assert
      Assert.AreEqual(pageIndex, paginator.PageIndex);
      Assert.AreEqual(2, paginator.TotalPages);
    }

    [TestMethod]
    public void Paginator_Constructor_Failure() {
      // Arrange
      var items = new List<string> { "item1", "item2", "item3" };
      int count = items.Count;
      int pageIndex = -1; // Invalid page index
      int pageSize = 2;

      // Act & Assert
      Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Paginator<string>(items, count, pageIndex, pageSize));
    }

    [TestMethod]
    public void Paginator_HasPreviousPage_Success() {
      // Arrange
      var items = new List<string> { "item1", "item2", "item3" };
      int count = items.Count;
      int pageIndex = 2;
      int pageSize = 2;
      var paginator = new Paginator<string>(items, count, pageIndex, pageSize);

      // Act
      var hasPreviousPage = paginator.HasPreviousPage;

      // Assert
      Assert.IsTrue(hasPreviousPage);
    }

    [TestMethod]
    public void Paginator_HasPreviousPage_Failure() {
      // Arrange
      var items = new List<string> { "item1", "item2", "item3" };
      int count = items.Count;
      int pageIndex = 1;
      int pageSize = 2;
      var paginator = new Paginator<string>(items, count, pageIndex, pageSize);

      // Act
      var hasPreviousPage = paginator.HasPreviousPage;

      // Assert
      Assert.IsFalse(hasPreviousPage);
    }

    [TestMethod]
    public void Paginator_HasNextPage_Success() {
      // Arrange
      var items = new List<string> { "item1", "item2", "item3" };
      int count = items.Count;
      int pageIndex = 1;
      int pageSize = 2;
      var paginator = new Paginator<string>(items, count, pageIndex, pageSize);

      // Act
      var hasNextPage = paginator.HasNextPage;

      // Assert
      Assert.IsTrue(hasNextPage);
    }

    [TestMethod]
    public void Paginator_HasNextPage_Failure() {
      // Arrange
      var items = new List<string> { "item1", "item2", "item3" };
      int count = items.Count;
      int pageIndex = 2;
      int pageSize = 2;
      var paginator = new Paginator<string>(items, count, pageIndex, pageSize);

      // Act
      var hasNextPage = paginator.HasNextPage;

      // Assert
      Assert.IsFalse(hasNextPage);
    }

    [TestMethod]
    public async Task Paginator_Paginate_Success() {
      // Arrange
      var items = new List<string> { "item1", "item2", "item3" }.AsQueryable();
      int pageIndex = 1;
      int pageSize = 2;

      // Act
      var result = await Paginator<string>.Paginate(items, pageIndex, pageSize);

      // Assert
      Assert.AreEqual(pageIndex, result.Page);
      Assert.AreEqual(pageSize, result.Count);
      Assert.AreEqual(3, result.RowsTotal);
      Assert.AreEqual(2, result.PagesTotal);
      Assert.AreEqual(2, result.Data.Count);
    }

    [TestMethod]
    public async Task Paginator_Paginate_Failure() {
      // Arrange
      var items = new List<string> { "item1", "item2", "item3" }.AsQueryable();
      int pageIndex = -1; // Invalid page index
      int pageSize = 2;

      // Act & Assert
      await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(async () => await Paginator<string>.Paginate(items, pageIndex, pageSize));
    }
  }
  //}


  //////  internal class PaginatorTests {
  //////  }
  //////}

  //////namespace Infrastructure.Tests {
  ////  [TestClass]
  ////  public class PaginatorTests {
  ////    [TestMethod]
  ////    public void Paginator_Constructor_Success() {
  ////      // Arrange
  ////      var items = new List<string> { "item1", "item2", "item3" };
  ////      int count = items.Count;
  ////      int pageIndex = 1;
  ////      int pageSize = 2;

  ////      // Act
  ////      var paginator = new Paginator<string>(items, count, pageIndex, pageSize);

  ////      // Assert
  ////      Assert.AreEqual(pageIndex, paginator.PageIndex);
  ////      Assert.AreEqual(2, paginator.TotalPages);
  ////    }

  ////    [TestMethod]
  ////    public void Paginator_Constructor_Failure() {
  ////      // Arrange
  ////      var items = new List<string> { "item1", "item2", "item3" };
  ////      int count = items.Count;
  ////      int pageIndex = -1; // Invalid page index
  ////      int pageSize = 2;

  ////      // Act & Assert
  ////      Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Paginator<string>(items, count, pageIndex, pageSize));
  ////    }

  ////    [TestMethod]
  ////    public void Paginator_HasPreviousPage_Success() {
  ////      // Arrange
  ////      var items = new List<string> { "item1", "item2", "item3" };
  ////      int count = items.Count;
  ////      int pageIndex = 2;
  ////      int pageSize = 2;
  ////      var paginator = new Paginator<string>(items, count, pageIndex, pageSize);

  ////      // Act
  ////      var hasPreviousPage = paginator.HasPreviousPage;

  ////      // Assert
  ////      Assert.IsTrue(hasPreviousPage);
  ////    }

  ////    [TestMethod]
  ////    public void Paginator_HasPreviousPage_Failure() {
  ////      // Arrange
  ////      var items = new List<string> { "item1", "item2", "item3" };
  ////      int count = items.Count;
  ////      int pageIndex = 1;
  ////      int pageSize = 2;
  ////      var paginator = new Paginator<string>(items, count, pageIndex, pageSize);

  ////      // Act
  ////      var hasPreviousPage = paginator.HasPreviousPage;

  ////      // Assert
  ////      Assert.IsFalse(hasPreviousPage);
  ////    }

  ////    [TestMethod]
  ////    public void Paginator_HasNextPage_Success() {
  ////      // Arrange
  ////      var items = new List<string> { "item1", "item2", "item3" };
  ////      int count = items.Count;
  ////      int pageIndex = 1;
  ////      int pageSize = 2;
  ////      var paginator = new Paginator<string>(items, count, pageIndex, pageSize);

  ////      // Act
  ////      var hasNextPage = paginator.HasNextPage;

  ////      // Assert
  ////      Assert.IsTrue(hasNextPage);
  ////    }

  ////    [TestMethod]
  ////    public void Paginator_HasNextPage_Failure() {
  ////      // Arrange
  ////      var items = new List<string> { "item1", "item2", "item3" };
  ////      int count = items.Count;
  ////      int pageIndex = 2;
  ////      int pageSize = 2;
  ////      var paginator = new Paginator<string>(items, count, pageIndex, pageSize);

  ////      // Act
  ////      var hasNextPage = paginator.HasNextPage;

  ////      // Assert
  ////      Assert.IsFalse(hasNextPage);
  ////    }

  ////    [TestMethod]
  ////    public async Task Paginator_Paginate_Success() {
  ////      // Arrange
  ////      var items = new List<string> { "item1", "item2", "item3" }.AsQueryable();
  ////      int pageIndex = 1;
  ////      int pageSize = 2;

  ////      // Act
  ////      var result = await Paginator<string>.Paginate(items, pageIndex, pageSize);

  ////      // Assert
  ////      Assert.AreEqual(pageIndex, result.Page);
  ////      Assert.AreEqual(pageSize, result.Count);
  ////      Assert.AreEqual(3, result.RowsTotal);
  ////      Assert.AreEqual(2, result.PagesTotal);
  ////      Assert.AreEqual(2, result.Data.Count);
  ////    }

  ////    [TestMethod]
  ////    public async Task Paginator_Paginate_Failure() {
  ////      // Arrange
  ////      var items = new List<string> { "item1", "item2", "item3" }.AsQueryable();
  ////      int pageIndex = -1; // Invalid page index
  ////      int pageSize = 2;

  ////      // Act & Assert
  ////      await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(async () => await Paginator<string>.Paginate(items, pageIndex, pageSize));
  ////    }
  ////  }
}
