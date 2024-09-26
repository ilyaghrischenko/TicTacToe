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
    
    public async Task ChangeAvatar(IFormFile avatar)
    {
        try
        {
            var changeAvatarModel = await GetChangeAvatarModel(avatar);
            await _userService.ChangeAvatar(_userId, changeAvatarModel);
        }
        catch (KeyNotFoundException ex)
        {
            throw new KeyNotFoundException(ex.Message);
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
    public async Task ChangePassword(ChangePasswordModel changePasswordModel)
    {
        try
        {
            await _userService.ChangePassword(_userId, changePasswordModel.PasswordInput);
        }
        catch (KeyNotFoundException ex)
        {
            throw new KeyNotFoundException(ex.Message);
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
    public async Task ChangeEmail(ChangeEmailModel changeEmailModel)
    {
        try
        {
            await _userService.ChangeEmail(_userId, changeEmailModel.EmailInput);
        }
        catch (KeyNotFoundException ex)
        {
            throw new KeyNotFoundException(ex.Message);
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
    public async Task ChangeLogin(ChangeLoginModel changeLoginModel)
    {
        try
        {
            await _userService.ChangeLogin(_userId, changeLoginModel.LoginInput);
        }
        catch (KeyNotFoundException ex)
        {
            throw new KeyNotFoundException(ex.Message);
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<ChangeAvatarModel> GetChangeAvatarModel(IFormFile avatar)
    {
        using var memoryStream = new MemoryStream();
        await avatar.CopyToAsync(memoryStream);
        return new ChangeAvatarModel(memoryStream.ToArray());
    }
}