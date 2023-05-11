using Domain.Entities;
using Domain.Port;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Models.Auth.Mapper;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Util.Ex;

namespace ServiceApplication
{

    public class UserService : BaseServiceApplication<User, UserDto>, IBaseServiceApplication<User, UserDto>, IUserService
    {


        private readonly IConfiguration _configurate;
        private readonly IRolService _rolService;

        public UserService(IConfiguration configuration,
            IRolService rolService,
            IUserRepository securityRepository)
            : base(securityRepository)
        {
            _configurate = configuration;
            _rolService = rolService;
            CreateMapperExpresion<User, UserDto>(cnf =>
            {
                UserMapper.Expresion(cnf, rolService);
            });
        }

        public async Task<Login> Login(Login login)
        {
            const string MessageError = "Error at the moment autentication";
            var user = await this.FirstOrDefautlModelBy(f => (f.UserName.Value == login.UserName || f.Email == login.UserName));
            if (user is null)
                throw new DomainException(MessageError);
            if (user.Password.Value != login.Password)
                throw new DomainException(MessageError);
            if (user != null)
            {
                var authClaims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                    };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurate["JWT:Secret"]));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(authClaims),
                    Issuer = _configurate["JWT:ValidIssuer"],
                    Audience = _configurate["JWT:ValidAudience"],

                    Expires = DateTime.Now.AddHours(24),
                    SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                Login log = new Login
                {
                    Token = tokenHandler.WriteToken(createdToken),
                    Expira = tokenDescriptor.Expires,
                    UserName = user.UserName.Value,
                    Profile = new Rol(),
                };
                log.Profile = user?.Rol;
                return log;
            }
            else
            {
                throw new DomainException(MessageError);
            }
        }
    }
}
