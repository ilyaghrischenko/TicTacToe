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

    public async Task<List<User>?> GetAppealedUsersAsync()
    {
        return await _adminService.GetAppealedUsersAsync();
    }

    public async Task<List<Report>> GetUserReportsAsync(int userId)
    {
        return await _reportService.GetUserReportsAsync(userId);
    }

    public async Task BlockUserAsync(int userId)
    {
        var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (string.IsNullOrEmpty(token))
        {
            throw new UnauthorizedAccessException("Token is not provided");
        }
        
        await _adminService.BlockUserAsync(userId, token);
    }
}