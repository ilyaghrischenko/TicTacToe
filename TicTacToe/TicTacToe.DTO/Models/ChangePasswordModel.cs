namespace TicTacToe.DTO.Models;

public class ChangePasswordModel(string passwordInput, string confirmPassword)
{
    public string PasswordInput { get; set; } = passwordInput;
    public string ConfirmPassword { get; set; } = confirmPassword;
}