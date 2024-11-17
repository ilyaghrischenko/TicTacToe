namespace TicTacToe.DTO.Requests;

public class ChangeEmailRequest(string emailInput)
{
    public string EmailInput { get; set; } = emailInput;
}