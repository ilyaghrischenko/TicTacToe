using TicTacToe.DTO.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Api.Filters;
using TicTacToe.Contracts.Controllers;
using TicTacToe.Domain.Enums;

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
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            _settingsControllerService.GetErrors(ModelState);
            await _settingsControllerService.ChangePasswordAsync(changePasswordModel);
            return Ok();
        }

        [HttpPost("changeEmail")]
        public async Task<IActionResult> ChangeEmail(ChangeEmailModel changeEmailModel)
        {
            _settingsControllerService.GetErrors(ModelState);
            await _settingsControllerService.ChangeEmailAsync(changeEmailModel);
            return Ok();
        }

        [HttpPost("changeLogin")]
        public async Task<IActionResult> ChangeLogin(ChangeLoginModel changeLoginModel)
        {
            _settingsControllerService.GetErrors(ModelState);
            await _settingsControllerService.ChangeLoginAsync(changeLoginModel);
            return Ok();
        }
    }
}