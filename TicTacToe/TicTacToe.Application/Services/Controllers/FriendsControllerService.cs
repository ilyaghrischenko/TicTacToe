using System.Security.Claims;
using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Interfaces.Controllers;
using TicTacToe.Domain.Interfaces.DbModelsServices;
using TicTacToe.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;

namespace TicTacToe.Application.Services.Controllers;

public class FriendsControllerService(
    IUserService userService,
    IHttpContextAccessor httpContextAccessor) : IFriendsControllerService
{
    private readonly IUserService _userService = userService;
    private readonly int _userId = int.Parse(
        httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value);

    public async Task AddFriend(int newFriendId)
    {
        try
        {
            await _userService.AddFriend(_userId, newFriendId);
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

    public async Task DeleteFriend(int friendId)
    {
        try
        {
            await _userService.DeleteFriend(_userId, friendId);
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
}