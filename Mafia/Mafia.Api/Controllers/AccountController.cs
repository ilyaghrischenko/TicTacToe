using Mafia.DTO.Models;
using Mafia.Domain.DbModels;
using Mafia.Domain.Interfaces.Controllers;
using Mafia.Domain.Interfaces.Repositories;
using Mafia.Domain.Interfaces.DbModelsServices;
using Microsoft.AspNetCore.Http;
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
                var errors = await _accountControllerService.GetErrors(ModelState);
                return BadRequest(errors);
            }
            
            try
            {
                await _accountControllerService.Login(loginModel);
                return Ok("Successfully login");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if (ModelState.IsValid is false)
            {
                var errors = await _accountControllerService.GetErrors(ModelState);
                return BadRequest(errors);
            }
            
            try
            {
                await _accountControllerService.Register(registerModel);
                return Ok("Successfully register");
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}