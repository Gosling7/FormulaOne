using FormulaOne.Application.DataTransferObjects;

namespace FormulaOne.Tests.Shared.Builders;

public class RaceResultDtoBuilder
{
    private RaceResultDto _raceResult = null!;

    public RaceResultDtoBuilder SetDefaultValues()
    {
        _raceResult = new RaceResultDto
        {
            Id = Guid.NewGuid().ToString(),
            Position = 1,
            Date = DateOnly.FromDateTime(DateTime.UtcNow),
            CircuitId = Guid.NewGuid().ToString(),
            CircuitName = "circuit name",
            DriverId = Guid.NewGuid().ToString(),
            DriverName = "driver name",
            TeamId = Guid.NewGuid().ToString(),
            TeamName = "team name",
            Laps = 50,
            Time = "1:31:44.742",
            Points = 25
        };

        return this;
    }

    public RaceResultDto Build()
    {
        return _raceResult;
    }

    public RaceResultDtoBuilder SetId(string id)
    {
        _raceResult = _raceResult with { Id = id };
        return this;
    }

    public RaceResultDtoBuilder SetPosition(int position)
    {
        _raceResult = _raceResult with { Position = position };
        return this;
    }

    public RaceResultDtoBuilder SetDate(DateOnly date)
    {
        _raceResult = _raceResult with { Date = date };
        return this;
    }

    public RaceResultDtoBuilder SetCircuitId(string circuitId)
    {
        _raceResult = _raceResult with { CircuitId = circuitId };
        return this;
    }

    public RaceResultDtoBuilder SetCircuitName(string circuitName)
    {
        _raceResult = _raceResult with { CircuitName = circuitName };
        return this;
    }

    public RaceResultDtoBuilder SetDriverId(string driverId)
    {
        _raceResult = _raceResult with { DriverId = driverId };
        return this;
    }

    public RaceResultDtoBuilder SetDriverName(string driverName)
    {
        _raceResult = _raceResult with { DriverName = driverName };
        return this;
    }

    public RaceResultDtoBuilder SetTeamId(string? teamId)
    {
        _raceResult = _raceResult with { TeamId = teamId };
        return this;
    }

    public RaceResultDtoBuilder SetTeamName(string? teamName)
    {
        _raceResult = _raceResult with { TeamName = teamName };
        return this;
    }

    public RaceResultDtoBuilder SetLaps(int laps)
    {
        _raceResult = _raceResult with { Laps = laps };
        return this;
    }

    public RaceResultDtoBuilder SetTime(string? time)
    {
        _raceResult = _raceResult with { Time = time };
        return this;
    }

    public RaceResultDtoBuilder SetPoints(int points)
    {
        _raceResult = _raceResult with { Points = points };
        return this;
    }
}
