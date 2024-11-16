namespace TicTacToe.DTO.Models;

public class ValidationErrorResponse
{
    public string Field { get; set; }
    public string[] Errors { get; set; }
}