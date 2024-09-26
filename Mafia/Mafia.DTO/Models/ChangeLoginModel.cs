namespace Mafia.DTO.Models;

public class ChangeLoginModel(string loginInput)
{
    public string LoginInput { get; set; } = loginInput;
}