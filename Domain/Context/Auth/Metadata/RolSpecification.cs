using System;
using System.Linq.Expressions;
using Domain.Common.Specification;
using Domain.Entities;

namespace Domain.AggregateModels
{
    public class RolSpecification : SpecificationBasic<Rol>
    {
        public static Expression<Func<Rol, bool>> ExistRolByName(string name, string id)
        {
            return x => x.Name.Value == name && x.Id != Guid.Parse(id);
        }
        public static Expression<Func<Rol, bool>> ExistRolByDescription(string description, string id)
        {
            return x => x.Description.Value == description && x.Id != Guid.Parse(id);
        }
    }
}
