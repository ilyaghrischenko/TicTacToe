namespace TicTacToe.DTO.Models;

public class ValidationError
{
    public string Field { get; set; }
    public string[] Errors { get; set; }
}