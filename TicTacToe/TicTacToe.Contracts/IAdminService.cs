using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TicTacToe.Contracts.Controllers;
using TicTacToe.Domain.DbModels;
using TicTacToe.DTO.Models;

namespace TicTacToe.Contracts;

public interface IAdminService : ICurrentUserId, IPngToIFormFile
{
    Task<List<User>?> GetAppealedUsersAsync();
    Task BlockUserAsync(int userId, string token);
    public Task ChangeAvatarAsync(IFormFile avatar, int userId);
    public Task<ChangeAvatarModel> GetChangeAvatarModelAsync(IFormFile avatar);
}