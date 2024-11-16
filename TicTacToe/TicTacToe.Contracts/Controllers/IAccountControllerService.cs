using System.Security.Claims;
using System.Threading.Tasks;
using TicTacToe.Domain.DbModels;
using TicTacToe.DTO.Models;

namespace TicTacToe.Contracts.Controllers;

public interface IAccountControllerService : IBaseControllerService
{
    public Task<object> LoginAsync(LogInRequest logInRequest);
    public Task RegisterAsync(RegisterRequest registerRequest);
    public ClaimsIdentity GetIdentity(User user);
    public string? GetToken(ClaimsIdentity identity);
}