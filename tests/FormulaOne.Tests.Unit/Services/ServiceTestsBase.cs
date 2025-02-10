using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Interfaces;
using NSubstitute;

namespace FormulaOne.Tests.Unit.Services;

public class ServiceTestsBase
{
    protected readonly IRaceResultRepository _raceResultRepository =
        Substitute.For<IRaceResultRepository>();

    protected const string InvalidId = "invalidguid";

    protected static List<RaceResultDto> GetRaceResults()
    {
        return [
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

    protected static PagedResult<RaceResultDto> GetExpectedRaceResultsWithErrors<TParameterType>(
        TParameterType parameters, List<string> errors)
        where TParameterType : IQueryParameter
    {
        return new PagedResult<RaceResultDto>(
            CurrentPage: parameters.Page,
            TotalPages: 0,
            PageSize: parameters.PageSize,
            TotalResults: 0,
            Errors: errors,
            Items: new List<RaceResultDto>());
    }

    protected static PagedResult<RaceResultDto> GetExpectedRaceResultsWithoutErrors<TParameterType>(
        TParameterType parameters, List<RaceResultDto> circuitsResults)
        where TParameterType : IQueryParameter
    {
        return new PagedResult<RaceResultDto>(
            CurrentPage: parameters.Page,
            TotalPages: 1,
            PageSize: parameters.PageSize,
            TotalResults: 1,
            Errors: new List<string>(),
            Items: circuitsResults);
    }
}
