using System.ComponentModel.DataAnnotations;

namespace DTO.Models;

public class LoginModel(string login, string password)
{
    public string Login { get; set; } = login;
    public string Password { get; set; } = password;
}