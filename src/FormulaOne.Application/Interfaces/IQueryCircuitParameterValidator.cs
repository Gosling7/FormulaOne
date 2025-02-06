using FormulaOne.Application.Parameters;

namespace FormulaOne.Application.Interfaces;
internal interface IQueryCircuitParameterValidator
{
    List<string> Validate(GetCircuitResultsParameter parameter);
    List<string> Validate(GetCircuitsParameter parameter);
}