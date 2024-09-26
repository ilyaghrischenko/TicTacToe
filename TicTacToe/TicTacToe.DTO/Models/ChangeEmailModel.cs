namespace TicTacToe.DTO.Models;

public class ChangeEmailModel(string emailInput)
{
    public string EmailInput { get; set; } = emailInput;
}