namespace Mafia.DTO.Models;

public class ChangePasswordModel(string password)
{
    public string Password { get; set; } = password;
}