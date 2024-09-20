using Mafia.Domain.Interfaces.Controllers;
using Mafia.Domain.Interfaces.DbModelsServices;
using Mafia.DTO.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mafia.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController(ISettingsControllerService settingsControllerService) : ControllerBase
    {
        private readonly ISettingsControllerService _settingsControllerService = settingsControllerService;

        [HttpPost("changeAvatar")]
        public async Task<IActionResult> ChangeAvatar(IFormFile avatar)
        {
            try
            {
                await _settingsControllerService.ChangeAvatar(avatar);
            }
            catch (KeyNotFoundException k)
            {
                return NotFound(k.Message);
            }
            catch (ArgumentException a)
            {
                return BadRequest(a.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

            return Ok();
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            try
            {
                await _settingsControllerService.ChangePassword(changePasswordModel);
            }
            catch (KeyNotFoundException k)
            {
                return NotFound(k.Message);
            }
            catch (ArgumentException a)
            {
                return BadRequest(a.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

            return Ok();
        }

        [HttpPost("changeEmail")]
        public async Task<IActionResult> ChangeEmail(ChangeEmailModel changeEmailModel)
        {
            try
            {
                await _settingsControllerService.ChangeEmail(changeEmailModel);
            }
            catch (KeyNotFoundException k)
            {
                return NotFound(k.Message);
            }
            catch (ArgumentException a)
            {
                return BadRequest(a.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

            return Ok();
        }

        [HttpPost("changeLogin")]
        public async Task<IActionResult> ChangeLogin(ChangeLoginModel changeLoginModel)
        {
            try
            {
                await _settingsControllerService.ChangeLogin(changeLoginModel);
            }
            catch (KeyNotFoundException k)
            {
                return NotFound(k.Message);
            }
            catch (ArgumentException a)
            {
                return BadRequest(a.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

            return Ok();
        }
    }
}