using System;
using Util.Ex;
using Utilidades;
using Domain.Port;
using System.Text;
using Domain.Entities;
using ServiceApplication.Dto;
using System.Security.Claims;
using System.Threading.Tasks;
using ServiceApplication.Base;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using ServiceApplication.Models.Auth.Mapper;

namespace ServiceApplication
{
  public class SecurityService : BaseServiceApplication<User, UserDto>, ISecurityService
  {
    private readonly IConfiguration _configurate;
    public SecurityService(IConfiguration configuration, ISecurityRepository securityRepository) : base(securityRepository)
    {
      _configurate = configuration;
      CreateMapperExpresion<User, UserDto>(cnf =>
      {
        UserMapper.Expresion(cnf);
      });
    }

    public async Task<Login> Login(Login login)
    {
      var user = await this.FirstOrDefautlModelBy(f => (f.UserName == login.UserName || f.Email == login.UserName));

      if (user is null)
        throw new DomainException("Invalid credentials");

      if (user.Password != login.Password)
        throw new DomainException("Invalid credentials");

      var authClaims = new[] {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Email, user.Email)
      };

      var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurate["JWTMONGO:Secret"]));
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(authClaims),
        Issuer = _configurate["JWTMONGO:ValidIssuer"],
        Audience = _configurate["JWTMONGO:ValidAudience"],

        Expires = DateTime.Now.AddHours(24),
        SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
      };

      var tokenHandler = new JwtSecurityTokenHandler();
      var createdToken = tokenHandler.CreateToken(tokenDescriptor);

      Login log = new Login
      {
        Token = tokenHandler.WriteToken(createdToken),
        Expira = tokenDescriptor.Expires,
        UserName = user.UserName,
        Profile = new System.Collections.Generic.List<Rol>(),
      };

      log.Profile = MapLstToENT<Rol, RolDto>(user?.Roles);
      return log;
    }

    public override async Task<UserDto> CreateModel(UserDto User)
    {
      try
      {
        var userExiste = await this.FirstOrDefautlModelBy(f => (f.UserName == User.UserName || f.Email == User.Email));
        if (userExiste != null)
          throw new DomainException("Este User ya está registrado");
        if (User is null)
          throw new DomainException("El User contiene valores nulos");
        if (string.IsNullOrEmpty(User.Password))
          throw new DomainException("La contraseña no puede ser vacio");
        if (string.IsNullOrEmpty(User.UserName))
          throw new DomainException("El User no puede ser vacio");
        if (User.Roles is null || User.Roles.Count == 0)
          throw new DomainException("Debe seleccionar un rol valido para este User");
        if (!Constants.ExpresionRegular(User.Email, Constants.EmailRegex))
          throw new DomainException("El email no contiene un formato valido");
        return MapToDTO<User, UserDto>(await RepositoryBase.CreateModel(MapToENT<User, UserDto>(User)));
      }
      catch (DomainException)
      {
        throw;
      }
      catch (Exception)
      {
        throw;
      }
    }

  }
}
