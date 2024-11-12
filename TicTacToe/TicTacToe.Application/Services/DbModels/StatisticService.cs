using TicTacToe.Contracts.DbModelsServices;
using TicTacToe.Contracts.Repositories;
using TicTacToe.Domain.DbModels;

namespace TicTacToe.Application.Services.DbModels;

public class StatisticService(
    IRepository<Statistic> statisticRepository,
    IUserRepository userRepository) : IStatisticService
{
    private readonly IRepository<Statistic> _statisticRepository = statisticRepository;
    private readonly IUserRepository _userRepository = userRepository;
    
    public async Task<Statistic> GetUserStatisticsAsync(int userId)
    {
        var user = await _userRepository.GetAsync(userId);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }

        var statistic = await _statisticRepository.GetAsync(user.Statistic.Id);
        return statistic;
    }
}