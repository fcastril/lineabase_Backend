using Domain.Common.Specification;
using System;
using System.Linq.Expressions;

namespace Domain.Entities
{
    public class UserSpecification : SpecificationBasic<User>
    {
        public static Expression<Func<User, bool>> ExistByUsername(string userName, Guid id) => x => x.UserName.Value == userName && x.Id != id;
    }
}
