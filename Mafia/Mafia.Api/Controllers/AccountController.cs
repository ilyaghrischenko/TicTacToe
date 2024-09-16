using Mafia.DTO.Models;
using Mafia.Domain.Interfaces.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Mafia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAccountControllerService accountControllerService) : ControllerBase
    {
        private readonly IAccountControllerService _accountControllerService = accountControllerService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (ModelState.IsValid is false)
            {
                var errors = _accountControllerService.GetErrors(ModelState);
                return BadRequest(errors);
            }
            
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if (ModelState.IsValid is false)
            {
                var errors = _accountControllerService.GetErrors(ModelState);
                return BadRequest(errors);
            }
            return Ok();
        }
    }
}