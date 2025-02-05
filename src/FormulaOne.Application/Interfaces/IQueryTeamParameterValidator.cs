using FormulaOne.Application.Parameters;

namespace FormulaOne.Application.Interfaces;

internal interface IQueryTeamParameterValidator
{
    public List<string> Validate(GetTeamsParameter parameters);
    public List<string> Validate(GetTeamStandingsParameter parameters);
    public List<string> Validate(GetTeamResultsParameter parameters);
}