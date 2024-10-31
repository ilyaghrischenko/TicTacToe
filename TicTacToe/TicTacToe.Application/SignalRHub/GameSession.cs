namespace TicTacToe.Application.SignalRHub;

public class GameSession
{
    public string GameId { get; set; }
    public string PlayerX { get; set; }
    public string PlayerO { get; set; }
    public string CurrentTurn { get; set; }
}