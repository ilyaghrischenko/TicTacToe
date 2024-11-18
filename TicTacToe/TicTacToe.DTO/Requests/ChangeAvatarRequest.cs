namespace TicTacToe.DTO.Requests;

public class ChangeAvatarRequest (byte[] avatar)
{
    public byte[] Avatar { get; set; } = avatar;
}