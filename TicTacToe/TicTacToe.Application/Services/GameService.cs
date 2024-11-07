using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TicTacToe.Domain.Interfaces;
using TicTacToe.Domain.Interfaces.Repositories;

namespace TicTacToe.Application.Services;

public class GameService(IUserRepository userRepository) : IGameService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task WinAsync(int id)
    {
        var user = await _userRepository.GetAsync(id);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        await _userRepository.UpdateAsync(user, () =>
        {
            user.Statistic.Wins++;
        });
    }

    public async Task LoseAsync(int id)
    {
        var user = await _userRepository.GetAsync(id);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        await _userRepository.UpdateAsync(user, () =>
        {
            user.Statistic.Losses++;
        });
    }
}