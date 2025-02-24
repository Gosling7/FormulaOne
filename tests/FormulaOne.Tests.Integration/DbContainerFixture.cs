﻿using FormulaOne.Core.Entities;
using FormulaOne.Infrastructure;
using FormulaOne.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace FormulaOne.Tests.Integration;

public class DbContainerFixture : IAsyncLifetime
{
    public CircuitRepository CircuitRepository { get; private set; } = null!;
    public TeamRepository TeamRepository { get; private set; } = null!;
    public DriverRepository DriverRepository { get; private set; } = null!;
    public DriverStandingRepository DriverStandingRepository { get; private set; } = null!;
    public TeamStandingRepository TeamStandingRepository { get; private set; } = null!;
    public RaceResultRepository RaceResultRepository { get; private set; } = null!;

    private static readonly DateOnly _raceResultDate1 = new(
        TestConstant.RaceResultYear1, TestConstant.RaceResultMonth1, TestConstant.RaceResultDay1);
    private static readonly DateOnly _raceResultDate2 = new(
        TestConstant.RaceResultYear2, TestConstant.RaceResultMonth2, TestConstant.RaceResultDay2);

    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-CU17-ubuntu-22.04")
        .WithEnvironment("MSSQL_SA_PASSWORD", "yourStrong(!)Password")
        .WithEnvironment("ACCEPT_EULA", "Y")
        .WithPortBinding(1433, true)
        .Build();

    private FormulaOneDbContext _dbContext = null!;

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        var options = new DbContextOptionsBuilder<FormulaOneDbContext>()
            .UseSqlServer(_dbContainer.GetConnectionString())
            .Options;

        _dbContext = new FormulaOneDbContext(options);
        await _dbContext.Database.EnsureCreatedAsync();

        CircuitRepository = new CircuitRepository(_dbContext);
        var circuit1 = new Circuit(
            id: Guid.Parse(TestConstant.CircuitId1),
            name: TestConstant.CircuitName1,
            location: TestConstant.CircuitLocation1);
        var circuit2 = new Circuit(
            id: Guid.Parse(TestConstant.CircuitId2),
            name: TestConstant.CircuitName2,
            location: TestConstant.CircuitLocation2);
        await _dbContext.Circuits.AddRangeAsync(circuit1, circuit2);

        TeamRepository = new TeamRepository(_dbContext);
        var team1 = new Team(
            id: Guid.Parse(TestConstant.TeamId1),
            name: TestConstant.TeamName1);
        var team2 = new Team(
            id: Guid.Parse(TestConstant.TeamId2),
            name: TestConstant.TeamName2);
        await _dbContext.Teams.AddRangeAsync(team1, team2);

        DriverRepository = new DriverRepository(_dbContext);
        var driver1 = new Driver(
            id: Guid.Parse(TestConstant.DriverId1),
            firstName: TestConstant.DriverFirstName1,
            lastName: TestConstant.DriverLastName1,
            nationality: TestConstant.DriverNationality1);
        var driver2 = new Driver(
            id: Guid.Parse(TestConstant.DriverId2),
            firstName: TestConstant.DriverFirstName2,
            lastName: TestConstant.DriverLastName2,
            nationality: TestConstant.DriverNationality2);
        await _dbContext.Drivers.AddRangeAsync(driver1, driver2);

        DriverStandingRepository = new DriverStandingRepository(_dbContext);
        await _dbContext.DriverStandings.AddRangeAsync(
            new DriverStanding(
                id: Guid.Parse(TestConstant.DriverStandingId1),
                position: TestConstant.DriverStandingPosition1,
                driver: driver1,
                team: team1,
                points: TestConstant.DriverStandingPoints1,
                year: TestConstant.StandingYear1),
            new DriverStanding(
                id: Guid.Parse(TestConstant.DriverStandingId2),
                position: TestConstant.DriverStandingPosition2,
                driver: driver2,
                team: team2,
                points: TestConstant.DriverStandingPoints2,
                year: TestConstant.StandingYear2));

        TeamStandingRepository = new TeamStandingRepository(_dbContext);
        await _dbContext.TeamStandings.AddRangeAsync(
            new TeamStanding(
                id: Guid.Parse(TestConstant.TeamStandingId1),
                year: TestConstant.StandingYear1,
                position: TestConstant.TeamStandingPosition1,
                points: TestConstant.TeamStandingPoints1,
                team: team1),
            new TeamStanding(
                id: Guid.Parse(TestConstant.TeamStandingId2),
                year: TestConstant.StandingYear2,
                position: TestConstant.TeamStandingPosition2,
                points: TestConstant.TeamStandingPoints2,
                team: team2));

        RaceResultRepository = new RaceResultRepository(_dbContext);
        await _dbContext.RaceResults.AddRangeAsync(
            new RaceResult(
                id: Guid.Parse(TestConstant.RaceResultId1),
                position: TestConstant.RaceResultPosition1,
                circuit: circuit1,
                date: _raceResultDate1,
                driver: driver1,
                team: team1,
                laps: TestConstant.RaceResultLaps,
                time: TestConstant.RaceResultTime1,
                points: TestConstant.RaceResultPoints1),
            new RaceResult(
                id: Guid.Parse(TestConstant.RaceResultId2),
                position: TestConstant.RaceResultPosition2,
                circuit: circuit2,
                date: _raceResultDate2,
                driver: driver2,
                team: team2,
                laps: TestConstant.RaceResultLaps,
                time: TestConstant.RaceResultTime2,
                points: TestConstant.RaceResultPoints2));

        await _dbContext.SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
    }
}
