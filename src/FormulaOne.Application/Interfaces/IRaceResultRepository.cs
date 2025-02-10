using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Application.Interfaces;

public interface IRaceResultRepository
{
    Task<(int, IReadOnlyCollection<RaceResultDto>)> GetItemsAsync(
        GetTeamResultsParameter parameters);
    Task<(int, IReadOnlyCollection<RaceResultDto>)> GetItemsAsync(
        GetDriverResultsParameter parameters);
    Task<(int, IReadOnlyCollection<RaceResultDto>)> GetItemsAsync(
        GetCircuitResultsParameter parameters);
}
