namespace Mafia.Domain.DbModels;

public class Friend
{
    public int Id { get; set; }
    public int UserId { get; set; } // Внешний ключ для User
    public User User { get; set; }

    public int FriendUserId { get; set; } // Внешний ключ для FriendUser
    public User FriendUser { get; set; }
    
    public Friend() { }

    public Friend(User user, User friendUser)
    {
        User = user;
        FriendUser = friendUser;
    }
}