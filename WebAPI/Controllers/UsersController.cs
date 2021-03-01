using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Business.Entities;
using Business.Services;
using WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.Filter;
using WebAPI.Models;
using WebAPI.Models.Users;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authentication;

        public UsersController(IUserRepository userRepository, IAuthenticationService authentication)
        {
            _userRepository = userRepository;
            _authentication = authentication;
        }

        [SwaggerResponse(statusCode: 200, description: "Success", type: typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Required Fields", type: typeof(FieldValidatorViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Intern Error", type: typeof(GenericErrorViewModel))]
        [CustomValidatorModelState]
        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginViewModelInput loginViewModelInput)
        {
            var user = _userRepository.GetUser(loginViewModelInput.Login);

            if (user == null)
                return BadRequest("Error");

            var userViewModelOutput = new UserViewModelOutput()
            {
                Id = user.Id,
                Login = loginViewModelInput.Login,
                Email = user.Email
            };

            var token = _authentication.GenerateToken(userViewModelOutput);

            return Ok(new { Token = token, User = userViewModelOutput });
        }

        [SwaggerResponse(statusCode: 201, description: "Success", type: typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Required Fields", type: typeof(FieldValidatorViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Intern Error", type: typeof(GenericErrorViewModel))]
        [CustomValidatorModelState]
        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterViewModelInput registerViewModelInput)
        {            
            var user = new User();

            user.Login = registerViewModelInput.Login;
            user.Password = registerViewModelInput.Password;
            user.Email = registerViewModelInput.Email;

            _userRepository.Add(user);
            _userRepository.Commit();

            return Created("", user);
        }
    }
}