using Microsoft.EntityFrameworkCore;

namespace TicTacToe.Data;

public static class DatabaseInitializer
{
    public static void Initialize(TicTacToeContext context)
    {
        if (context.Database.EnsureCreated())
        {
            context.Database.Migrate();
        }
        else
        {
            Console.WriteLine("Database already exists.");
        }
    }
}