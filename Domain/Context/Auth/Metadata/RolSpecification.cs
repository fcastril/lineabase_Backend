using System;
using System.Linq.Expressions;
using Domain.Common.Specification;
using Domain.Entities;

namespace Domain.AggregateModels
{
    public class RolSpecification : SpecificationBasic<Rol>
    {
        public static Expression<Func<Rol, bool>> ExistRolByName(string name, Guid id)
        {
            return x => x.Name.Value == name && x.Id != id;
        }
        public static Expression<Func<Rol, bool>> ExistRolByDescription(string description, Guid id)
        {
            return x => x.Description.Value == description && x.Id != id;
        }
    }
}
