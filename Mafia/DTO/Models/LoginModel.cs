using System.ComponentModel.DataAnnotations;

namespace DTO.Models;

public class LoginModel(string login, string password)
{
    [Required(ErrorMessage = "Login is required")]
    [MinLength(4, ErrorMessage = "Login must be at least 4 characters long")]
    [MaxLength(20, ErrorMessage = "Login must be at most 20 characters long")]
    public string Login { get; set; } = login;
    
    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    [MaxLength(20, ErrorMessage = "Password must be at most 20 characters long")]
    public string Password { get; set; } = password;
}