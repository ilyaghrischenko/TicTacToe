using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Api.Filters;
using TicTacToe.Contracts.Controllers;
using TicTacToe.Domain.Enums;
using TicTacToe.DTO.Requests;

namespace TicTacToe.Api.Controllers
{
    [Role(Role.User)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController(ISettingsControllerService settingsControllerService) : ControllerBase
    {
        private readonly ISettingsControllerService _settingsControllerService = settingsControllerService;

        [HttpPost("changeAvatar")]
        public async Task<IActionResult> ChangeAvatar(IFormFile avatar)
        {
            _settingsControllerService.GetErrors(ModelState);
            await _settingsControllerService.ChangeAvatarAsync(avatar);
            return Ok();
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            _settingsControllerService.GetErrors(ModelState);
            await _settingsControllerService.ChangePasswordAsync(changePasswordRequest);
            return Ok();
        }

        [HttpPost("changeEmail")]
        public async Task<IActionResult> ChangeEmail(ChangeEmailRequest changeEmailRequest)
        {
            _settingsControllerService.GetErrors(ModelState);
            await _settingsControllerService.ChangeEmailAsync(changeEmailRequest);
            return Ok();
        }

        [HttpPost("changeLogin")]
        public async Task<IActionResult> ChangeLogin(ChangeLoginRequest changeLoginRequest)
        {
            _settingsControllerService.GetErrors(ModelState);
            await _settingsControllerService.ChangeLoginAsync(changeLoginRequest);
            return Ok();
        }
    }
}