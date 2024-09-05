namespace Domain.Models;

public class Friend
{
    public int Id { get; set; }
    public User User { get; set; }
    public User FriendUser { get; set; }
    
    public Friend() { }
    public Friend(User user, User friendUser)
    {
        User = user;
        FriendUser = friendUser;
    }
    
    public override string ToString()
        => $"User: {User}, Friend: {FriendUser}";
}