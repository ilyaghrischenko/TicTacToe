using Domain.DbModels;
using Microsoft.EntityFrameworkCore;
namespace Data;

public class MafiaContext : DbContext
{
    public MafiaContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer
            ("data source=(localdb)\\MSSQLLocalDB;initial catalog=MafiaDB;integrated security=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Friend>()
            .HasOne(f => f.User)
            .WithMany() 
            .HasForeignKey("UserId") 
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Friend>()
            .HasOne(f => f.FriendUser)
            .WithMany()
            .HasForeignKey("FriendUserId") 
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
    
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Friend> Friends { get; set; }
    public virtual DbSet<Statistic> Statistics { get; set; }
}