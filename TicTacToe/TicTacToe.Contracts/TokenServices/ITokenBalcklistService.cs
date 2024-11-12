using System.Threading.Tasks;

namespace TicTacToe.Contracts.TokenServices;

public interface ITokenBlacklistService
{
    Task AddToBlacklistAsync(string token);
    Task<bool> IsTokenBlacklistedAsync(string token);
}