namespace TicTacToe.Domain.DbModels;

public class Report : BaseEntity
{
    public User User { get; set; }
    public string Message { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public Report() { }
    
    public Report(User user, string message)
    {
        User = user;
        Message = message;
    }
}