using System.Security.Claims;
using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Interfaces.Controllers;
using TicTacToe.Domain.Interfaces.DbModelsServices;
using TicTacToe.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace TicTacToe.Application.Services.Controllers;

public class UserControllerService(
    IHttpContextAccessor httpContextAccessor,
    IUserService userService) : IUserControllerService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IUserService _userService = userService;
    
    public async Task<User> GetUser()
    {
        try
        {
            var userId = GetCurrentUserId();
            var user = await _userService.GetUserById(userId);
            return user;
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new UnauthorizedAccessException(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            throw new KeyNotFoundException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<User>?> GetFriends()
    {
        try
        {
            var userId = GetCurrentUserId();
            var userFriends = await _userService.GetFriends(userId);
            return userFriends;
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new UnauthorizedAccessException(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            throw new KeyNotFoundException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<User>?> GetAllUsers()
    {
        try
        {
            var userId = GetCurrentUserId();
            var allUsers = await _userService.GetAllUsers(userId);
            return allUsers;
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new UnauthorizedAccessException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int GetCurrentUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        if (userIdClaim == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        return int.Parse(userIdClaim);
    }
}