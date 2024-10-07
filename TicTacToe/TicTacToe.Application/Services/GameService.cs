using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TicTacToe.Domain.Interfaces;
using TicTacToe.Domain.Interfaces.Repositories;

namespace TicTacToe.Application.Services;

public class GameService(
    IHttpContextAccessor httpContextAccessor,
    IUserRepository userRepository) : IGameService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IUserRepository _userRepository = userRepository;
    
    public async Task<string> Win()
    {
        var userId = GetCurrentUserId();
        var user = await _userRepository.Get(userId);

        await _userRepository.Update(user, () =>
        {
            user.Statistic.Wins++;
        });
        
        return $"{user.Login} wins!";
    }

    public async Task<string> Lose()
    {
        var userId = GetCurrentUserId();
        var user = await _userRepository.Get(userId);

        await _userRepository.Update(user, () =>
        {
            user.Statistic.Losses++;
        });
        
        return $"{user.Login} loses!";
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