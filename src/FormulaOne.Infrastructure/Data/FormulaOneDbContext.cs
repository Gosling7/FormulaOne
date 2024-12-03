using FormulaOne.Infrastructure.Data.DataAccessObjects;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Infrastructure.Data;

public class FormulaOneDbContext : DbContext
{
    public FormulaOneDbContext(DbContextOptions<FormulaOneDbContext> options) : base(options)
    {
    }

    public DbSet<TeamDao> Teams { get; set; }
    public DbSet<DriverDao> Drivers { get; set; }
    public DbSet<CircuitDao> Circuits { get; set; }
    public DbSet<DriverStandingDao> DriverStandings { get; set; }
    public DbSet<TeamStandingDao> TeamStandings { get; set; }
    public DbSet<RaceResultDao> RaceResults { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DriverStandingDao>()
            .HasOne(ds => ds.Driver)
            .WithMany(d => d.DriverStandings)
            .HasForeignKey(ds => ds.DriverId);

        modelBuilder.Entity<TeamStandingDao>()
            .HasOne(ts => ts.Team)
            .WithMany(t => t.TeamStandings)
            .HasForeignKey(ts => ts.TeamId);

        modelBuilder.Entity<RaceResultDao>()
            .HasOne(rs => rs.Driver)
            .WithMany(d => d.RaceResults)
            .HasForeignKey(rs => rs.DriverId);

        modelBuilder.Entity<RaceResultDao>()
            .HasOne(rs => rs.Team)
            .WithMany(t => t.RaceResults)
            .HasForeignKey(rs => rs.TeamId);

        modelBuilder.Entity<RaceResultDao>()
            .HasOne(rs => rs.Circuit)
            .WithMany(c => c.RaceResults)
            .HasForeignKey(rs => rs.CircuitId);
    }
}
