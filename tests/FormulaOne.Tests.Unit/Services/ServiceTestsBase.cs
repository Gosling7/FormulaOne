using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Interfaces;
using NSubstitute;

namespace FormulaOne.Tests.Unit.Services;

public class ServiceTestsBase
{
    protected readonly IRaceResultRepository _raceResultRepository =
        Substitute.For<IRaceResultRepository>();

    protected const string InvalidId = "invalidguid";

    protected List<RaceResultDto> GetRaceResults() => 
    [
        new RaceResultDto
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
        }
    ];
}
