using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Interfaces;
using TicTacToe.Domain.Interfaces.Controllers;
using TicTacToe.Domain.Interfaces.DbModelsServices;
using TicTacToe.Domain.Interfaces.Repositories;

namespace TicTacToe.Application.Services.Controllers;

public class AdminControllerService(
    IReportService reportService, 
    IAdminService adminService,
    IHttpContextAccessor httpContextAccessor) : IAdminControllerService
{
    private readonly IReportService _reportService = reportService;
    private readonly IAdminService _adminService = adminService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<List<User>?> GetAppealedUsers()
    {
        return await _adminService.GetAppealedUsers();
    }

    public async Task<List<Report>> GetUserReports(int userId)
    {
        return await _reportService.GetUserReports(userId);
    }

    public async Task BlockUser(int userId)
    {
        await _adminService.BlockUser(userId);
    }
}