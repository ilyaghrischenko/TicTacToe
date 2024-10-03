using Microsoft.IdentityModel.Tokens;
using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Interfaces.DbModelsServices;
using TicTacToe.Domain.Interfaces.Repositories;

namespace TicTacToe.Application.Services.DbModels;

public class ReportService(IRepository<Report> reportRepository) : IReportService
{
    private readonly IRepository<Report> _reportRepository = reportRepository;

    public async Task<List<Report>> GetUserReports(int userId)
    {
        var allReports = await _reportRepository.GetAll();
        if (allReports.IsNullOrEmpty())
        {
            return new();
        }

        var userReports = allReports?.Where(r => r.User.Id == userId).ToList();
        return userReports;
    }
}