using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Application.Interfaces;

public interface IRaceResultRepository
{
    Task<(int, IEnumerable<RaceResultDto>)> GetRaceResultsAsync(
        GetTeamResultsParameter parameters);
    Task<(int, IEnumerable<RaceResultDto>)> GetRaceResultsAsync(
        GetDriverResultsParameter parameters);
}
