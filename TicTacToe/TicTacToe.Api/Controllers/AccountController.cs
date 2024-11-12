using TicTacToe.DTO.Models;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Contracts.Controllers;

namespace TicTacToe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAccountControllerService accountControllerService) : ControllerBase
    {
        private readonly IAccountControllerService _accountControllerService = accountControllerService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            _accountControllerService.GetErrors(ModelState);
            var response = await _accountControllerService.LoginAsync(loginModel);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            _accountControllerService.GetErrors(ModelState);
            await _accountControllerService.RegisterAsync(registerModel);
            return Ok();
        }
    }
}