using Domain.DbModels;

namespace Domain.Models;

public class Game(List<User> players)
{
    public List<User> Players { get; set; } = players;

    public async Task<bool> Start()
    {
        if (Players.Count < 5) return false;

        Players.ForEach(player =>
        {
            //TODO: end this class
        });
        
        return true;
    }
}