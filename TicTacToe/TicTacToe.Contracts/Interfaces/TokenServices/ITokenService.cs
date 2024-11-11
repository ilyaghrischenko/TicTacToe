using TicTacToe.Domain.DbModels;

namespace TicTacToe.Contracts.Interfaces.TokenServices;

public interface ITokenService
{
    public string GenerateToken(User user);
}