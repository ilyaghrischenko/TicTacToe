using Microsoft.AspNetCore.Http;
using TicTacToe.Contracts.Interfaces.Controllers;
using TicTacToe.Domain.DbModels;
using TicTacToe.DTO.Models;

namespace TicTacToe.Contracts.Interfaces;

public interface IAdminService : ICurrentUserId, IPngToIFormFile
{
    Task<List<User>?> GetAppealedUsersAsync();
    Task BlockUserAsync(int userId, string token);
    public Task ChangeAvatarAsync(IFormFile avatar, int userId);
    public Task<ChangeAvatarModel> GetChangeAvatarModelAsync(IFormFile avatar);
}