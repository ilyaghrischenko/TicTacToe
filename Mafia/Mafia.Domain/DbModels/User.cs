using System.Text.Json.Serialization;
using Mafia.Domain.Enums;

namespace Mafia.Domain.DbModels;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
    public byte[]? Avatar { get; set; }
    public GameRole? GameRole { get; set; }
    public Statistic Statistic { get; set; }
    
    public User() { }
    public User(string login, string password, string email, Role role, GameRole? gameRole = null, byte[]? avatar = null)
    {
        Login = login;
        Password = password;
        Email = email;
        Role = role;
        GameRole = gameRole;
        Avatar = avatar;
    }
    
    public override string ToString()
        => $"Login: {Login}, Password: {Password}, Email: {Email}";
}