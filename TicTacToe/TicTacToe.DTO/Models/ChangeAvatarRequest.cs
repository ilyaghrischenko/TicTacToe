namespace TicTacToe.DTO.Models;

public class ChangeAvatarRequest (byte[] avatar)
{
    public byte[] Avatar { get; set; } = avatar;
}