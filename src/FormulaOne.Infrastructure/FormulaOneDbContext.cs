using FormulaOne.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Infrastructure;

public class FormulaOneDbContext : DbContext
{
    public FormulaOneDbContext(DbContextOptions<FormulaOneDbContext> options) : base(options)
    {
    }

    public DbSet<Driver> Drivers { get; set; }
    public DbSet<DriverStanding> DriverStandings { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamStanding> TeamStandings { get; set; }
    public DbSet<RaceResult> RaceResults { get; set; }
    public DbSet<Circuit> Circuits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>()
           .HasMany(t => t.TeamStandings)
           .WithOne(ts => ts.Team)
           .HasForeignKey(ts => ts.TeamId);
        modelBuilder.Entity<Team>()
            .HasMany(t => t.RaceResults)
            .WithOne(rr => rr.Team)
            .HasForeignKey(rr => rr.TeamId);

        modelBuilder.Entity<Driver>()
            .HasMany(d => d.RaceResults)
            .WithOne(rr => rr.Driver)
            .HasForeignKey(rs => rs.DriverId);

        modelBuilder.Entity<Circuit>()
            .HasMany(c => c.RaceResults)
            .WithOne(rr => rr.Circuit)
            .HasForeignKey(rr => rr.CircuitId);
    }
}
