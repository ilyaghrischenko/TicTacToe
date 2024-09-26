using TicTacToe.DTO.Models;
using Microsoft.AspNetCore.Http;

namespace TicTacToe.Domain.Interfaces.Controllers;

public interface ISettingsControllerService : IBaseControllerService
{
    public Task ChangeAvatar(IFormFile avatar);
    public Task ChangePassword(ChangePasswordModel changePasswordModel);
    public Task ChangeEmail(ChangeEmailModel changeEmailModel);
    public Task ChangeLogin(ChangeLoginModel changeLoginModel);
    public Task<ChangeAvatarModel> GetChangeAvatarModel(IFormFile avatar);
}