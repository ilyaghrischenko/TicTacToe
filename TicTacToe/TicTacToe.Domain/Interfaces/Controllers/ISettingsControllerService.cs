using TicTacToe.DTO.Models;
using Microsoft.AspNetCore.Http;

namespace TicTacToe.Domain.Interfaces.Controllers;

public interface ISettingsControllerService : IBaseControllerService
{
    public Task ChangeAvatarAsync(IFormFile avatar);
    public Task ChangePasswordAsync(ChangePasswordModel changePasswordModel);
    public Task ChangeEmailAsync(ChangeEmailModel changeEmailModel);
    public Task ChangeLoginAsync(ChangeLoginModel changeLoginModel);
    public Task<ChangeAvatarModel> GetChangeAvatarModelAsync(IFormFile avatar);
}