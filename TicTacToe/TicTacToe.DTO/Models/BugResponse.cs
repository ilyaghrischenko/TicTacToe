namespace TicTacToe.DTO.Models;

public class BugResponse(int id, string description, string action, string importance, int status, DateTime dateTime)
{
    public int Id { get; set; } = id;
    public string Description { get; set; } = description;
    public string Action { get; set; } = action;
    public string Importance { get; set; } = importance;
    public int Status { get; set; } = status;
    public DateTime CreatedAt { get; set; } = dateTime;
}