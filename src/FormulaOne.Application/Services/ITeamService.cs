using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Application.Services;

public interface ITeamService
{
    Task<PagedResult<TeamDto>> GetTeams(GetTeamsParameters parameters);
}