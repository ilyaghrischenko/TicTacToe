using TicTacToe.Domain.DbModels;

namespace TicTacToe.Domain.Interfaces.TokenServices;

public interface ITokenService
{
    public string GenerateToken(User user);
}