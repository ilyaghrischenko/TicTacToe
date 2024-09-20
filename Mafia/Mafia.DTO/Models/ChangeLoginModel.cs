namespace Mafia.DTO.Models;

public class ChangeLoginModel(string login)
{
    public string Login { get; set; } = login;
}