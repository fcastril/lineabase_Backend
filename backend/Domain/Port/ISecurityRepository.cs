
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entity;

namespace Domain.Port
{
    public interface ISecurityRepository : IRepositoryBase<User>
    {
    }
}
