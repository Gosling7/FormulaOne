using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Application.Interfaces;

public interface IRaceResultRepository
{
    Task<(int, IEnumerable<RaceResultDto>)> GetItemsAsync(GetTeamResultsParameter parameters);
    Task<(int, IEnumerable<RaceResultDto>)> GetItemsAsync(GetDriverResultsParameter parameters);
    Task<(int, IEnumerable<RaceResultDto>)> GetItemsAsync(GetCircuitResultsParameter parameters);
}
