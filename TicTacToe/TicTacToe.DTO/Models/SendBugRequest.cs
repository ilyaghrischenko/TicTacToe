using TicTacToe.Domain.Enums;

namespace TicTacToe.DTO.Models;

public class SendBugRequest(TriggeredAction action, string description, Importance importance)
{
    public TriggeredAction Action { get; set; } = action;
    public string Description { get; set; } = description;
    public Importance Importance { get; set; } = importance;
}