namespace Mafia.DTO.Models;

public class ChangeEmailModel(string email)
{
    public string Email { get; set; } = email;
}