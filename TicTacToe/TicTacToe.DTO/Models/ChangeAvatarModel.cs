namespace TicTacToe.DTO.Models;

public class ChangeAvatarModel (byte[] avatar)
{
    public byte[] Avatar { get; set; } = avatar;
}