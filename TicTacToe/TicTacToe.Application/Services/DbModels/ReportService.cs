using Microsoft.IdentityModel.Tokens;
using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Interfaces.DbModelsServices;
using TicTacToe.Domain.Interfaces.Repositories;

namespace TicTacToe.Application.Services.DbModels;

public class ReportService(
    IRepository<Report> reportRepository,
    IUserRepository userRepository) : IReportService
{
    private readonly IRepository<Report> _reportRepository = reportRepository;
    private readonly IUserRepository _userRepository = userRepository;
    
    public async Task<List<Report>> GetUserReportsAsync(int userId)
    {
        var allReports = await _reportRepository.GetAllAsync();
        if (allReports.IsNullOrEmpty())
        {
            return new();
        }

        var userReports = allReports?.Where(r => r.User.Id == userId).ToList();
        return userReports;
    }

    public async Task SendReportAsync(int id, string message)
    {
        var user = await _userRepository.GetAsync(id);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        var report = new Report(user, message);
        await _reportRepository.AddAsync(report);
    }
}