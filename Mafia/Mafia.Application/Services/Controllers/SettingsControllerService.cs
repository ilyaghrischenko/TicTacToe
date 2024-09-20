using System.Security.Claims;
using Mafia.Domain.Interfaces.Controllers;
using Mafia.Domain.Interfaces.DbModelsServices;
using Mafia.DTO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mafia.Application.Services.Controllers;

public class SettingsControllerService(IUserService userService, IHttpContextAccessor httpContextAccessor) : ISettingsControllerService
{
    private readonly IUserService _userService = userService;

    private readonly string _userLogin = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
    
    public async Task ChangeAvatar(IFormFile avatar)
    {
        var changeAvatarModel = await GetChangeAvatarModel(avatar);
        await _userService.ChangeAvatar(_userLogin, changeAvatarModel);
    }
    
    public async Task ChangePassword(ChangePasswordModel changePasswordModel)
    {
        await _userService.ChangePassword(_userLogin, changePasswordModel.Password);
    }
    
    public async Task ChangeEmail(ChangeEmailModel changeEmailModel)
    {
        await _userService.ChangeEmail(_userLogin, changeEmailModel.Email);
    }
    
    public async Task ChangeLogin(ChangeLoginModel changeLoginModel)
    {
        await _userService.ChangeLogin(_userLogin, changeLoginModel.Login);
    }
    public async Task<ChangeAvatarModel> GetChangeAvatarModel(IFormFile avatar)
    {
        using var memoryStream = new MemoryStream();
        await avatar.CopyToAsync(memoryStream);
        return new ChangeAvatarModel(memoryStream.ToArray());
    }
}