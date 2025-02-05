using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Application.Interfaces;

public interface IDriverService
{
    Task<PagedResult<DriverDto>> GetDrivers(GetDriversParameter parameters);
    Task<PagedResult<DriverStandingDto>> GetDriverStandings(GetDriverStandingsParameter parameters);
    Task<PagedResult<RaceResultDto>> GetDriverResults(GetDriverResultsParameter parameters);
}
