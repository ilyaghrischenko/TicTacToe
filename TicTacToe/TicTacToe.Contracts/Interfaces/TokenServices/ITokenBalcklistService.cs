using System.Threading.Tasks;

namespace TicTacToe.Contracts.Interfaces.TokenServices;

public interface ITokenBlacklistService
{
    Task AddToBlacklistAsync(string token);
    Task<bool> IsTokenBlacklistedAsync(string token);
}