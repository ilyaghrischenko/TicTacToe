using Microsoft.AspNetCore.Http;
using TicTacToe.DTO.Models;

namespace TicTacToe.Contracts.Controllers;

public interface ISettingsControllerService : IBaseControllerService
{
    public Task ChangePasswordAsync(ChangePasswordModel changePasswordModel);
    public Task ChangeEmailAsync(ChangeEmailModel changeEmailModel);
    public Task ChangeLoginAsync(ChangeLoginModel changeLoginModel);
    public Task ChangeAvatarAsync(IFormFile avatar);
    public Task<ChangeAvatarModel> GetChangeAvatarModelAsync(IFormFile avatar);
}