using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Application.Interfaces;

public interface ITeamService
{
    Task<PagedResult<TeamDto>> GetTeams(GetTeamsParameter parameters);
    Task<PagedResult<TeamStandingDto>> GetTeamStandings(GetTeamStandingsParameter parameters);
    Task<PagedResult<RaceResultDto>> GetTeamResults(GetTeamResultsParameter parameters);
}