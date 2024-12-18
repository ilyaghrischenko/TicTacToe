namespace TicTacToe.DTO.Requests;

public class ChangeLoginRequest(string loginInput)
{
    public string LoginInput { get; set; } = loginInput;
}