using FormulaOne.Application.Parameters;

namespace FormulaOne.Application.Interfaces;

internal interface IQueryTeamsParameterValidator
{
    public List<string> Validate(GetTeamsParameters parameters);
    public List<string> Validate(GetTeamStandingsParameters parameters);
    public List<string> Validate(GetTeamResultsParameters parameters);
}