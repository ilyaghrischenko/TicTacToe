using Microsoft.AspNetCore.Http;
using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Interfaces.Controllers;
using TicTacToe.DTO.Models;

namespace TicTacToe.Domain.Interfaces;

public interface IAdminService : ICurrentUserId, IPngToIFormFile
{
    Task<List<User>?> GetAppealedUsersAsync();
    Task BlockUserAsync(int userId, string token);
    public Task ChangeAvatarAsync(IFormFile avatar, int userId);
    public Task<ChangeAvatarModel> GetChangeAvatarModelAsync(IFormFile avatar);
}