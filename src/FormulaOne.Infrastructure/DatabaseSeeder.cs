using FormulaOne.Application.Constants;
using FormulaOne.Core.Entities;
using FormulaOne.Infrastructure.Scrapers;
using FormulaOne.Application.Interfaces;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace FormulaOne.Infrastructure;

internal class DatabaseSeeder : IDatabaseSeeder
{
    private readonly CircuitScraper _circuitScraper;
    private readonly RaceResultLinksScraper _raceResultsLinksScraper;
    private readonly DriverScraper _driverScraper;
    private readonly TeamScraper _teamScraper;
    private readonly DriverStandingScraper _driverStandingScraper;
    private readonly TeamStandingScraper _teamStandingScraper;
    private readonly RaceResultScraper _raceResultScraper;

    private readonly FormulaOneDbContext _dbContext;

    private const int StartYear = ValidationConstant.StartYear;
    private const int TeamStandingStartYear = ValidationConstant.TeamStandingStartYear;
    private readonly int _currentYear = DateTime.UtcNow.Year;

    public DatabaseSeeder(FormulaOneDbContext dbContext,
        RaceResultLinksScraper raceResultsLinksScraper,
        CircuitScraper circuitScraper,
        DriverScraper driverScraper,
        TeamScraper teamScraper,
        DriverStandingScraper driverStandingScraper,
        TeamStandingScraper teamStandingScraper,
        RaceResultScraper raceResultScraper)
    {
        _dbContext = dbContext;
        _raceResultsLinksScraper = raceResultsLinksScraper;
        _circuitScraper = circuitScraper;
        _driverScraper = driverScraper;
        _teamScraper = teamScraper;
        _driverStandingScraper = driverStandingScraper;
        _teamStandingScraper = teamStandingScraper;
        _raceResultScraper = raceResultScraper;
    }

    public async Task Seed()
    {
        Console.WriteLine("Starting database seeding.");
        var stopwatch = Stopwatch.StartNew();

        var raceResultLinksBag = await _raceResultsLinksScraper.ScrapeAsync(StartYear, _currentYear);
        var circuitsDict = await _circuitScraper.ScrapeAsync(raceResultLinksBag);
        var driversDict = await _driverScraper.ScrapeAsync(StartYear, _currentYear, raceResultLinksBag);
        var teamsDict = await _teamScraper.ScrapeAsync(raceResultLinksBag);
        var driverStandingsBag = await _driverStandingScraper.ScrapeAsync(StartYear, _currentYear,
            driversDict, teamsDict);
        var teamStandingsBag = await _teamStandingScraper.ScrapeAsync(TeamStandingStartYear, _currentYear,
            teamsDict);
        var raceResultsBag = await _raceResultScraper.ScrapeAsync(raceResultLinksBag, circuitsDict,
            driversDict, teamsDict);

        await _dbContext.Circuits.AddRangeAsync(circuitsDict.Values);
        await _dbContext.Drivers.AddRangeAsync(driversDict.Values);
        await _dbContext.Teams.AddRangeAsync(teamsDict.Values);

        await SeedDriverStandings(driversDict, teamsDict, driverStandingsBag);
        await SeedTeamStandings(teamsDict, teamStandingsBag);
        await SeedRaceResults(circuitsDict, driversDict, teamsDict, raceResultsBag);

        await _dbContext.SaveChangesAsync();

        stopwatch.Stop();

        Console.WriteLine($"Database seeding completed. Elapsed: {stopwatch.Elapsed}.");
    }

    private async Task SeedDriverStandings(ConcurrentDictionary<string, Driver> driversDict,
        ConcurrentDictionary<string, Team> teamsDict,
        ConcurrentBag<DriverStanding> driverStandingsBag)
    {
        var driverStandings = driverStandingsBag
            .Select(standing =>
            {
                var driverFullName = $"{standing.Driver.FirstName} {standing.Driver.LastName}";
                if (!driversDict.TryGetValue(driverFullName, out var existingDriver))
                {
                    Console.WriteLine($"Driver {driverFullName} is not in driversDict!");
                    return null;
                }

                if (!teamsDict.TryGetValue(standing.Team.Name, out var existingTeam))
                {
                    Console.WriteLine($"Team {standing.Team.Name} is not in teamsDict!");
                    return null;
                }

                return new DriverStanding
                {
                    Id = Guid.NewGuid(),
                    Position = standing.Position,
                    Driver = existingDriver,
                    Team = existingTeam,
                    Points = standing.Points,
                    Year = standing.Year
                };
            })
            .Where(ds => ds is not null)
            .ToList();

        await _dbContext.DriverStandings.AddRangeAsync(driverStandings);
    }

    private async Task SeedTeamStandings(ConcurrentDictionary<string, Team> teamsDict, 
        ConcurrentBag<TeamStanding> teamStandingsBag)
    {
        var teamStandings = teamStandingsBag
            .Select(standing =>
            {
                if (!teamsDict.TryGetValue(standing.Team.Name, out var existingTeam))
                {
                    Console.WriteLine($"Team {standing.Team.Name} is not in driversDict!");
                    return null;
                }

                return new TeamStanding
                {
                    Id = Guid.NewGuid(),
                    Position = standing.Position,
                    Team = existingTeam,
                    Points = standing.Points,
                    Year = standing.Year
                };
            })
            .Where(ts => ts is not null)
            .ToList();

        await _dbContext.TeamStandings.AddRangeAsync(teamStandings);
    }

    private async Task SeedRaceResults(ConcurrentDictionary<string, Circuit> circuitsDict,
        ConcurrentDictionary<string, Driver> driversDict,
        ConcurrentDictionary<string, Team> teamsDict,
        ConcurrentBag<RaceResult> raceResultsBag)
    {
        var raceResults = raceResultsBag
            .Select(result =>
            {
                var driverFullName = $"{result.Driver.FirstName} {result.Driver.LastName}";
                if (!driversDict.TryGetValue(driverFullName, out var existingDriver))
                {
                    Console.WriteLine($"Driver {driverFullName} is not in driversDict!");
                    return null;
                }

                Team? team = null;
                if (result.Team is not null)
                {
                    if (teamsDict.TryGetValue(result.Team.Name, out var existingTeam))
                    {
                        team = existingTeam;
                    }
                    else
                    {
                        Console.WriteLine($"Team {result.Team.Name} is not in teamsDict!");
                        return null;
                    }
                }

                if (!circuitsDict.TryGetValue(result.Circuit.Name, out var existingCircuit))
                {
                    Console.WriteLine($"Team {result.Team.Name} is not in circuitDict!");
                    return null;
                }

                return new RaceResult
                {
                    Id = Guid.NewGuid(),
                    Position = result.Position,
                    Circuit = existingCircuit,
                    Date = result.Date,
                    Driver = existingDriver,
                    Team = team,
                    Laps = result.Laps,
                    Time = result.Time,
                    Points = result.Points,
                };
            })
            .Where(rr => rr is not null)
            .ToList();

        await _dbContext.RaceResults.AddRangeAsync(raceResults);
    }
}
