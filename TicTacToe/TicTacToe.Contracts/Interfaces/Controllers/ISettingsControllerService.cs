using System.Threading.Tasks;
using TicTacToe.DTO.Models;
using Microsoft.AspNetCore.Http;

namespace TicTacToe.Contracts.Interfaces.Controllers;

public interface ISettingsControllerService : IBaseControllerService
{
    public Task ChangePasswordAsync(ChangePasswordModel changePasswordModel);
    public Task ChangeEmailAsync(ChangeEmailModel changeEmailModel);
    public Task ChangeLoginAsync(ChangeLoginModel changeLoginModel);
    public Task ChangeAvatarAsync(IFormFile avatar);
    public Task<ChangeAvatarModel> GetChangeAvatarModelAsync(IFormFile avatar);
}