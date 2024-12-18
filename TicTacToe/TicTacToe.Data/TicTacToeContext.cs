using TicTacToe.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TicTacToe.Data
{
    public class TicTacToeContext : DbContext
    {
        public TicTacToeContext(DbContextOptions<TicTacToeContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(
                "Host=storonimv-dimabutnik71-03a0.k.aivencloud.com;Port=13017;Database=tictactoe;Username=avnadmin;Password=avns_jidfutlyjh98hui0hrx;Ssl Mode=Require");
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friend>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Friend>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friend>()
                .HasOne(f => f.FriendUser)
                .WithMany()
                .HasForeignKey(f => f.FriendUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<Statistic> Statistics { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Bug> Bugs { get; set; }
    }
}