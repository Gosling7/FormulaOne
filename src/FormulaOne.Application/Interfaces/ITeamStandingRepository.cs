using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Application.Interfaces;

public interface ITeamStandingRepository
{
    Task<(int, IEnumerable<TeamStandingDto>)> GetStandingsAsync(GetTeamStandingsParameter parameters);
}
