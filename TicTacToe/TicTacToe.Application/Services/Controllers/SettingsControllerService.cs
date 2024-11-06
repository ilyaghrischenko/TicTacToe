using System.Security.Claims;
using TicTacToe.Domain.Interfaces.Controllers;
using TicTacToe.Domain.Interfaces.DbModelsServices;
using TicTacToe.DTO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TicTacToe.Application.Services.Controllers;

public class SettingsControllerService(
    IUserService userService,
    IHttpContextAccessor httpContextAccessor) : ISettingsControllerService
{
    private readonly IUserService _userService = userService;

    private readonly string _userId = httpContextAccessor.HttpContext.User.Claims
        .FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

    public async Task ChangeAvatarAsync(IFormFile avatar)
    {
        var changeAvatarModel = await GetChangeAvatarModelAsync(avatar);
        await _userService.ChangeAvatarAsync(_userId, changeAvatarModel);
    }

    public async Task ChangePasswordAsync(ChangePasswordModel changePasswordModel)
    {
        await _userService.ChangePasswordAsync(_userId, changePasswordModel.PasswordInput);
    }

    public async Task ChangeEmailAsync(ChangeEmailModel changeEmailModel)
    {
        await _userService.ChangeEmailAsync(_userId, changeEmailModel.EmailInput);
    }

    public async Task ChangeLoginAsync(ChangeLoginModel changeLoginModel)
    {
        await _userService.ChangeLoginAsync(_userId, changeLoginModel.LoginInput);
    }

    public async Task<ChangeAvatarModel> GetChangeAvatarModelAsync(IFormFile avatar)
    {
        using var memoryStream = new MemoryStream();
        await avatar.CopyToAsync(memoryStream);
        return new ChangeAvatarModel(memoryStream.ToArray());
    }
}