using Domain.Entities;
using ServiceApplication.Dto;
using System.Threading.Tasks;

namespace ServiceApplication
{
    public interface IUserService : IBaseServiceApplication<User, UserDto>
    {
        Task<Login> Login(Login login);

    }
}
