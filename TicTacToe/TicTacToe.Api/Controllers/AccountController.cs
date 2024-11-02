using TicTacToe.DTO.Models;
using TicTacToe.Domain.Interfaces.Controllers;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Api.Filters;

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
            try
            {
                _accountControllerService.GetErrors(ModelState);
                var response = await _accountControllerService.Login(loginModel);
                return Ok(response);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            try
            {
                _accountControllerService.GetErrors(ModelState);
                await _accountControllerService.Register(registerModel);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

            return Ok();
        }
    }
}