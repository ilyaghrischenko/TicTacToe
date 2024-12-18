namespace TicTacToe.DTO.Requests;

public class ChangePasswordRequest(string passwordInput, string confirmPassword)
{
    public string PasswordInput { get; set; } = passwordInput;
    public string ConfirmPassword { get; set; } = confirmPassword;
}