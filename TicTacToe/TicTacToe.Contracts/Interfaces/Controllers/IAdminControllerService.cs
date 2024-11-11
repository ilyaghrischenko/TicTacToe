using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TicTacToe.Domain.DbModels;

namespace TicTacToe.Contracts.Interfaces.Controllers;

public interface IAdminControllerService
{
    Task<List<User>?> GetAppealedUsersAsync();
    Task<List<Report>> GetUserReportsAsync(int userId);
    Task BlockUserAsync(int userId);
}