using Mafia.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
namespace Mafia.Data;

public class MafiaContext : DbContext
{
    public MafiaContext() { }
    public MafiaContext(DbContextOptions<MafiaContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(@"data source=sql.bsite.net\MSSQL2016;initial catalog=iluhahr_Mafia;User ID=iluhahr_Mafia;Password=1234; Trust Server Certificate=True;");
        
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<GameRole> Roles { get; set; }
    public virtual DbSet<Statistic> Statistics { get; set; }
}