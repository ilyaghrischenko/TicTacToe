namespace TicTacToe.DTO.Models;

public class BugChangeStatusRequest(int id, int status)
{
    public int Id { get; init; } = id;
    public int Status { get; init; } = status;
}