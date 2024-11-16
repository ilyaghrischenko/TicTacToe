namespace TicTacToe.DTO.Models;

public class ChangeEmailRequest(string emailInput)
{
    public string EmailInput { get; set; } = emailInput;
}