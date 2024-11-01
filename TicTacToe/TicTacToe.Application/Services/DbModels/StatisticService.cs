using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Interfaces.DbModelsServices;
using TicTacToe.Domain.Interfaces.Repositories;

namespace TicTacToe.Application.Services.DbModels;

public class StatisticService(
    IRepository<Statistic> statisticRepository,
    IUserRepository userRepository) : IStatisticService
{
    private readonly IRepository<Statistic> _statisticRepository = statisticRepository;
    private readonly IUserRepository _userRepository = userRepository;
    
    public async Task<Statistic> GetUserStatistics(int userId)
    {
        var user = await _userRepository.Get(userId);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }

        var statistic = await _statisticRepository.Get(user.Statistic.Id);
        return statistic;
    }
}