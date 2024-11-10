namespace TicTacToe.DTO.Models;

public class BugModel
{
    public TriggeredAction Action { get; set; }
    public string Description { get; set; }
    public Importance Importance { get; set; }
}