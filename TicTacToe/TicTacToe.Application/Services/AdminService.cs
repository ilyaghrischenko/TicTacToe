using TicTacToe.Application.Services.DbModels;
using TicTacToe.Domain.Enums;
using TicTacToe.Domain.Interfaces;
using TicTacToe.Domain.Interfaces.Repositories;

namespace TicTacToe.Application.Services;

public class AdminService(IUserRepository userRepository) : IAdminService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task BlockUser(int userId)
    {
        var user = await _userRepository.Get(userId);
        if (user == null)
        {
            throw new ArgumentNullException("User not found");
        }

        await _userRepository.Update(user, () =>
        {
            user.Status = UserStatus.Blocked;
        });
    }
}