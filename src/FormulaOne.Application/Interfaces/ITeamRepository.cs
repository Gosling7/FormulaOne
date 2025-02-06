using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Application.Interfaces;

public interface ITeamRepository
{
    Task<(int, IEnumerable<TeamDto>)> GetItemsAsync(GetTeamsParameter parameters);
}
