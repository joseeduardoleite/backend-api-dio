using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Filter;
using WebAPI.Models;
using WebAPI.Models.Users;

namespace WebAPI.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [SwaggerResponse(statusCode: 200, description: "Success", type: typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Required Fields", type: typeof(FieldValidatorViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Intern Error", type: typeof(GenericErrorViewModel))]
        [CustomValidatorModelState]
        [HttpPost]
        [Route("login")]
        // public IActionResult Login(LoginViewModelInput login)
        public IActionResult Login()
        {
            var userViewModelOutput = new UserViewModelOutput()
            {
                Code = 1,
                Login = "eduardo",
                Email = "eduardo@test.com"
            };

            var secret = Encoding.ASCII.GetBytes("MzfsT&d9gprP>!9$Es(X!5g@;ef!5sbk:jH\\2.}8ZP'qY#7");
            var symmetricSecurityKey = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userViewModelOutput.Code.ToString()),
                    new Claim(ClaimTypes.Name, userViewModelOutput.Login.ToString()),
                    new Claim(ClaimTypes.Email, userViewModelOutput.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            var token = jwtSecurityTokenHandler.WriteToken(tokenGenerated);

            return Ok(new { Token = token, User = userViewModelOutput });
        }

        [CustomValidatorModelState]
        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterViewModelInput register)
        {
            return Created("", register);
        }
    }
}