namespace Domain.Models;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    
    public Role? Role { get; set; }
    
    public byte[]? Avatar { get; set; }
    
    public virtual List<Friend>? Friends { get; set; } = new();
    
    public User() { }
    public User(string login, string password, string email, Role? role, byte[]? avatar)
    {
        Login = login;
        Password = password;
        Email = email;
        Role = role;
        Avatar = avatar;
    }
    
    public override string ToString()
        => $"Login: {Login}, Password: {Password}, Email: {Email}";
}