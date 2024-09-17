namespace Mafia.Domain.DbModels;

public class Statistic
{
    public int Id { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }

    public Statistic()
    {
        Wins = 0;
        Losses = 0;
    }
    public Statistic(int wins, int losses)
    {
        Wins = wins;
        Losses = losses;
    }
    
    public override string ToString()
        => $"Wins: {Wins}, Losses: {Losses}";
}