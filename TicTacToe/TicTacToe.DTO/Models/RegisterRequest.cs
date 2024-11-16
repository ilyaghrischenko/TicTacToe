using System.ComponentModel.DataAnnotations;

namespace TicTacToe.DTO.Models;

public class RegisterRequest(string email, string login, string password, string confirmPassword)
{
    public string Email { get; set; } = email;
    public string Login { get; set; } = login;
    public string Password { get; set; } = password;
    public string ConfirmPassword { get; set; } = confirmPassword;
}