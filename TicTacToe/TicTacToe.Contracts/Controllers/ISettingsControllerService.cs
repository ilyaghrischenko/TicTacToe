using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TicTacToe.DTO.Models;

namespace TicTacToe.Contracts.Controllers;

public interface ISettingsControllerService : IBaseControllerService
{
    public Task ChangePasswordAsync(ChangePasswordRequest changePasswordRequest);
    public Task ChangeEmailAsync(ChangeEmailRequest changeEmailRequest);
    public Task ChangeLoginAsync(ChangeLoginRequest changeLoginRequest);
    public Task ChangeAvatarAsync(IFormFile avatar);
    public Task<ChangeAvatarRequest> GetChangeAvatarModelAsync(IFormFile avatar);
}