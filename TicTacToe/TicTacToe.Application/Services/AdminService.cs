using System.Net.PeerToPeer;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using TicTacToe.Application.Exceptions;
using TicTacToe.Contracts;
using TicTacToe.Contracts.DbModelsServices;
using TicTacToe.Contracts.Repositories;
using TicTacToe.Contracts.TokenServices;
using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Enums;
using TicTacToe.DTO.Models;

namespace TicTacToe.Application.Services;

public class AdminService(
    IUserRepository userRepository,
    IRepository<Report> reportRepository,
    ITokenBlacklistService blacklistService,
    IUserService userService,
    IReportService reportService,
    IHttpContextAccessor httpContextAccessor) : IAdminService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRepository<Report> _reportRepository = reportRepository;
    private readonly ITokenBlacklistService _blacklistService = blacklistService;
    private readonly IUserService _userService = userService;
    private readonly IReportService _reportService = reportService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<List<User>?> GetAppealedUsersAsync()
    {
        var allReports = await _reportRepository.GetAllAsync();
        if (allReports.IsNullOrEmpty())
        {
            return new();
        }

        var appealedUsers = allReports?
            .Where(report => report.User.Role == Role.User)
            .Select(report => report.User)
            .Distinct()
            .ToList();

        return appealedUsers;
    }

    public async Task BlockUserAsync(int userId, string token)
    {
        var user = await _userRepository.GetAsync(userId);
        if (user == null)
        {
            throw new EntityNotFoundException("User not found");
        }

        await _userRepository.UpdateAsync(user, () =>
        {
            user.Role = Role.Blocked;
        });
        await _reportService.DeleteAllUserReportsAsync(userId);
        
        await _blacklistService.AddToBlacklistAsync(token);

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "block.png");
        var formFile = ConvertPngToIFormFile(filePath);
        await ChangeAvatarAsync(formFile, userId);
    }

    public int GetCurrentUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        if (userIdClaim == null)
        {
            throw new AuthenticationException("User is not authenticated");
        }

        return int.Parse(userIdClaim);
    }

    public async Task ChangeAvatarAsync(IFormFile avatar, int userId)
    {
        var changeAvatarModel = await GetChangeAvatarModelAsync(avatar);
        await _userService.ChangeAvatarAsync(userId.ToString(), changeAvatarModel);
    }

    public async Task<ChangeAvatarRequest> GetChangeAvatarModelAsync(IFormFile avatar)
    {
        using var memoryStream = new MemoryStream();
        await avatar.CopyToAsync(memoryStream);
        return new ChangeAvatarRequest(memoryStream.ToArray());
    }

    public IFormFile ConvertPngToIFormFile(string filePath)
    {
        byte[] fileBytes = File.ReadAllBytes(filePath);

        var stream = new MemoryStream(fileBytes);
        var formFile = new FormFile(stream, 0, stream.Length, "file", Path.GetFileName(filePath))
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/png"
        };

        return formFile;
    }
}