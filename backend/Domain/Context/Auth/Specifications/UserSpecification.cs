using System;
using System.Linq.Expressions;
using Domain.Common.Specification;

namespace Domain.Entities {
  public class UserSpecification : SpecificationBasic<User> {
    public static Expression<Func<User, bool>> ExistByUsername(string userName, string id) => x => x.UserName == userName && x.Id != id;
  }
}
