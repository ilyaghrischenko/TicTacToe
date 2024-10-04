using Microsoft.IdentityModel.Tokens;
using TicTacToe.Application.Services.DbModels;
using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Enums;
using TicTacToe.Domain.Interfaces;
using TicTacToe.Domain.Interfaces.Repositories;

namespace TicTacToe.Application.Services;

public class AdminService(
    IUserRepository userRepository, 
    IRepository<Report> reportRepository) : IAdminService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRepository<Report> _reportRepository = reportRepository;

    public async Task<List<User>?> GetAppealedUsers()
    {
        var allReports = await _reportRepository.GetAll();
        if (allReports.IsNullOrEmpty())
        {
            return new();
        }

        var appealedUsers = allReports?
            .Where(report => report.User.Status == UserStatus.Available)
            .Select(report => report.User)
            .Distinct()
            .ToList();
        
        return appealedUsers;
    }

    public async Task BlockUser(int userId)
    {
        var user = await _userRepository.Get(userId);
        if (user == null)
        {
            throw new ArgumentNullException("User not found");
        }

        await _userRepository.Update(user, () =>
        {
            user.Status = UserStatus.Blocked;
        });
    }
}