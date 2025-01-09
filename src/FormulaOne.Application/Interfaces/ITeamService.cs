using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Application.Interfaces;

public interface ITeamService
{
    Task<PagedResult<TeamDto>> GetTeams(GetTeamsParameters parameters);
    Task<PagedResult<TeamStandingDto>> GetTeamStandings(GetTeamStandingsParameters parameters);
    Task<PagedResult<RaceResultDto>> GetTeamResults(GetTeamResultsParameters parameters);
}