using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using TicTacToe.Application.Services.DbModels;
using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Enums;
using TicTacToe.Domain.Interfaces;
using TicTacToe.Domain.Interfaces.Repositories;

namespace TicTacToe.Application.Services;

public class AdminService(
    IUserRepository userRepository, 
    IRepository<Report> reportRepository,
    IHttpContextAccessor httpContextAccessor) : IAdminService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRepository<Report> _reportRepository = reportRepository;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<List<User>?> GetAppealedUsers()
    {
        if (await IsAdmin() is false)
        {
            throw new UnauthorizedAccessException("User is not admin");
        }
        
        var allReports = await _reportRepository.GetAll();
        if (allReports.IsNullOrEmpty())
        {
            return new();
        }

        var appealedUsers = allReports?
            .Where(report => report.User.Status == UserStatus.Available
            && report.User.Role == Role.User)
            .Select(report => report.User)
            .Distinct()
            .ToList();
        
        return appealedUsers;
    }

    public async Task BlockUser(int userId)
    {
        if (await IsAdmin() is false)
        {
            throw new UnauthorizedAccessException("User is not admin");
        }
        
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

    public async Task<bool> IsAdmin()
    {
        var userId = GetCurrentUserId();
        var user = await _userRepository.Get(userId);
        
        return user.Role == Role.Admin;
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