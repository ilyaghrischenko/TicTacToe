using System.Security.Claims;
using TicTacToe.DTO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Contracts.Controllers;
using TicTacToe.Contracts.DbModelsServices;

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

    public async Task ChangePasswordAsync(ChangePasswordRequest changePasswordRequest)
    {
        await _userService.ChangePasswordAsync(_userId, changePasswordRequest.PasswordInput);
    }

    public async Task ChangeEmailAsync(ChangeEmailRequest changeEmailRequest)
    {
        await _userService.ChangeEmailAsync(_userId, changeEmailRequest.EmailInput);
    }

    public async Task ChangeLoginAsync(ChangeLoginRequest changeLoginRequest)
    {
        await _userService.ChangeLoginAsync(_userId, changeLoginRequest.LoginInput);
    }

    public async Task<ChangeAvatarRequest> GetChangeAvatarModelAsync(IFormFile avatar)
    {
        using var memoryStream = new MemoryStream();
        await avatar.CopyToAsync(memoryStream);
        return new ChangeAvatarRequest(memoryStream.ToArray());
    }
}