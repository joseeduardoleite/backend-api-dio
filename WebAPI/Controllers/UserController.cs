using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Filter;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [SwaggerResponse(statusCode: 200, description: "Success", type: typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Required Fields", type: typeof(FieldValidatorViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Intern Error", type: typeof(GenericErrorViewModel))]
        [CustomValidatorModelState]
        [HttpPost]
        [Route("login")]
        public IActionResult Log(LoginViewModelInput login)
        {            
            return Ok(login);
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