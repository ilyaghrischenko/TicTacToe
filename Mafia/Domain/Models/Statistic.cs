namespace Domain.Models;

public class Statistic
{
    public int Id { get; set; }
    public User User { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    
    public Statistic() { }
    public Statistic(User user, int wins, int losses)
    {
        User = user;
        Wins = wins;
        Losses = losses;
    }
    
    public override string ToString()
        => $"User: {User}, Wins: {Wins}, Losses: {Losses}";
}