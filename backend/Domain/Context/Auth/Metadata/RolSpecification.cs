using System;
using Domain.Entities;
using System.Linq.Expressions;
using Domain.Common.Specification;

namespace Domain.AggregateModels {
  public class RolSpecification : SpecificationBasic<Rol> {
    public static Expression<Func<Rol, bool>> ExistRolByName(string name) {
      return x => x.Name == name;
    }
    public static Expression<Func<Rol, bool>> ExistRolByDescription(string description) {
      return x => x.Description == description;
    }
  }
}
