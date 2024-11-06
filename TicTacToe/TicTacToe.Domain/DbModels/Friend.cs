namespace TicTacToe.Domain.DbModels;

public class Friend : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }

    public int FriendUserId { get; set; }
    public User FriendUser { get; set; }
    
    public Friend() { }

    public Friend(User user, User friendUser)
    {
        User = user;
        FriendUser = friendUser;
    }
}