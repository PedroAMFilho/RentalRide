using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RentalRide.Api.Security;
using RentalRide.Domain.UserBaseContext.Commands.Inputs;
using RentalRide.Domain.UserBaseContext.Commands.Outputs;
using RentalRide.Domain.UserBaseContext.Entities;
using RentalRide.Domain.UserBaseContext.Repositories;
using RentalRide.Domain.UserBaseContext.ValueObjects;
using RentalRide.Shared.Commands;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace RentalRide.Api.Controllers.UserBaseContext
{
    public class LoginController : Controller
    {
        private User _user;
        private readonly IUserBaseRepository _repository;
        private readonly SigningConfigurations _signingConfigurations;
        private readonly TokenConfigurations _tokenConfigurations;

        public LoginController(IUserBaseRepository repository,
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations)
        {
            _repository = repository;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("rentalride/auth")]
        public ICommandResult Post([FromBody]AuthenticateUserBaseCommand command) 
        {
            var login = new LoginVO(command.User, command.Password);
            if (login.Invalid)
                return new CommandResult(false, "Please, correct following fields:", login.Notifications);

            _user = _repository.UserBase(login.User, login.Password);
            if (_user == new User())
                return new CommandResult(false, "User and/or Password is invalid.", new { });
            //else if (_usuarioBase.Ativo == EBoolean.False)
            //    return new CommandResult(false, "Usuário inativo. Favor, contactar o administrador do sistema para ativar o seu cadastro.", new { });

            //var senhaTemporaria = _repository.BuscarUsuarioSenhaTemporaria(_usuarioBase.Id);
            //if (senhaTemporaria != null)
            //    if (DateTime.Now.Subtract(senhaTemporaria.DataCadastro).Days >= 1)
            //        if (senhaTemporaria.Ativo == EBoolean.True)
            //            return new CommandResult(false, "Usuário e/ou senha inválidos. Possivelmente sua senha temporária já expirou. Favor, Solicitar uma nova senha.", new { });

            return Authenticate;
        }

        public ICommandResult Authenticate 
        {
            get
            {
                var dataCriacao = DateTime.Now;
                var dataExpiracao = dataCriacao + TimeSpan.FromSeconds(_tokenConfigurations.SECONDS);
                var handler = new JwtSecurityTokenHandler();

                var identity = new ClaimsIdentity(
                    new GenericIdentity(_user.username, "TokenAuth"),
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, _user.username)
                    }
                );

                return new CommandResult(true, "User authenticated",
                    new
                    {
                        userId = _user.Id,
                        user = _user.username,
                        authenticated = true,
                        created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                        expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                        accessToken = handler.WriteToken(handler.CreateToken(new SecurityTokenDescriptor
                        {
                            Issuer = _tokenConfigurations.ISSUER,
                            Audience = _tokenConfigurations.AUDIENCE,
                            SigningCredentials = _signingConfigurations.SigningCredentials,
                            Subject = identity,
                            NotBefore = dataCriacao,
                            Expires = dataExpiracao
                        })),
                        message = "OK"
                    });
            }

        }

        [HttpGet]
        [AllowAnonymous]
        [Route("rentalride/password/{pass}")]
        public string GetPassword(string pass)
        {
            if (string.IsNullOrEmpty(pass)) return "";
            //var password = (pass += "|3c3ad4a0-627f-43a2-b2f4-a8c1529dcf23");
            var password = pass;
            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(password));
            var sbString = new StringBuilder();
            foreach (var t in data)
                sbString.Append(t.ToString("x2"));

            return sbString.ToString();
 
        }
    }
}
