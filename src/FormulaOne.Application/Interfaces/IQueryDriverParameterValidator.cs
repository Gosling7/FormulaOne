using FormulaOne.Application.Parameters;

namespace FormulaOne.Application.Interfaces;

internal interface IQueryDriverParameterValidator
{
    public List<string> Validate(GetDriversParameter parameters);
    public List<string> Validate(GetDriverStandingsParameter parameters);
    public List<string> Validate(GetDriverResultsParameter parameters);
}
