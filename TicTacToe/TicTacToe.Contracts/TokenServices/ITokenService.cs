using TicTacToe.Domain.DbModels;

namespace TicTacToe.Contracts.TokenServices;

public interface ITokenService
{
    public string GenerateToken(User user);
}