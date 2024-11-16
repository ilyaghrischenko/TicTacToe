namespace TicTacToe.DTO.Models;

public class ChangeLoginRequest(string loginInput)
{
    public string LoginInput { get; set; } = loginInput;
}