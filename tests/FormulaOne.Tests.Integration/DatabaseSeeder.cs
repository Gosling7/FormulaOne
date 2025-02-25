using FormulaOne.Core.Entities;
using FormulaOne.Infrastructure;

namespace FormulaOne.Tests.Integration;

public static class DatabaseSeeder
{
    private static readonly object _lock = new();
    private static bool _isSeeded;

    public static void EnsureSeeded(FormulaOneDbContext dbContext)
    {
        if (_isSeeded)
        {
            return;
        }

        lock (_lock)
        {
            if (_isSeeded)
            {
                return;
            }

            dbContext.Database.EnsureCreated();
            if (dbContext.Circuits.Any())
            {
                return;
            }

            var circuit1 = new Circuit(
                id: Guid.Parse(TestConstant.CircuitId1),
                name: TestConstant.CircuitName1,
                location: TestConstant.CircuitLocation1);
            var circuit2 = new Circuit(
                id: Guid.Parse(TestConstant.CircuitId2),
                name: TestConstant.CircuitName2,
                location: TestConstant.CircuitLocation2);
            dbContext.Circuits.AddRange(circuit1, circuit2);

            var team1 = new Team(
                id: Guid.Parse(TestConstant.TeamId1),
                name: TestConstant.TeamName1);
            var team2 = new Team(
                id: Guid.Parse(TestConstant.TeamId2),
                name: TestConstant.TeamName2);
            dbContext.Teams.AddRange(team1, team2);

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
            dbContext.Drivers.AddRange(driver1, driver2);

            dbContext.DriverStandings.AddRange(
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

            dbContext.TeamStandings.AddRange(
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

            dbContext.RaceResults.AddRange(
                new RaceResult(
                    id: Guid.Parse(TestConstant.RaceResultId1),
                    position: TestConstant.RaceResultPosition1,
                    circuit: circuit1,
                    date: new DateOnly(TestConstant.RaceResultYear1,
                        TestConstant.RaceResultMonth1, TestConstant.RaceResultDay1),
                    driver: driver1,
                    team: team1,
                    laps: TestConstant.RaceResultLaps,
                    time: TestConstant.RaceResultTime1,
                    points: TestConstant.RaceResultPoints1),
                new RaceResult(
                    id: Guid.Parse(TestConstant.RaceResultId2),
                    position: TestConstant.RaceResultPosition2,
                    circuit: circuit2,
                    date: new DateOnly(TestConstant.RaceResultYear2,
                        TestConstant.RaceResultMonth2, TestConstant.RaceResultDay2),
                    driver: driver2,
                    team: team2,
                    laps: TestConstant.RaceResultLaps,
                    time: TestConstant.RaceResultTime2,
                    points: TestConstant.RaceResultPoints2));

            dbContext.SaveChanges();            
            _isSeeded = true;
        }
    }
}
