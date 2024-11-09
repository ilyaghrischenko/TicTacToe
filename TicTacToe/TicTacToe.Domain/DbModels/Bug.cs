using TicTacToe.Domain.Enums;

namespace TicTacToe.Domain.DbModels;

public class Bug : BaseEntity
{
    public TriggeredAction Action { get; set; }
    public string Description { get; set; }
    public Importance Importance { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public Bug(TriggeredAction action, string description, Importance importance)
    {
        Action = action;
        Description = description;
        Importance = importance;
    }
}