using System.Text.Json.Serialization;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Domain.DbModels;

public class User : BaseEntity
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public byte[]? Avatar { get; set; } = null;
    public Role Role { get; set; }
    public Statistic Statistic { get; set; }
    
    public User() { }
    public User(string login, string password, string email, Role role)
    {
        Login = login;
        Password = password;
        Email = email;
        Role = role;
    }
    
    public override string ToString()
        => $"Login: {Login}, Password: {Password}, Email: {Email}";
}