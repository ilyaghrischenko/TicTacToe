namespace TicTacToe.Domain.DbModels;

public class Report
{
    public int Id { get; set; }
    public User User { get; set; }
    public string Message { get; set; }
    
    public Report() { }
    
    public Report(User user, string message)
    {
        User = user;
        Message = message;
    }
}