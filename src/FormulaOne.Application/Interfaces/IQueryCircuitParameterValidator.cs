using FormulaOne.Application.Parameters;

namespace FormulaOne.Application.Interfaces;

public interface IQueryCircuitParameterValidator
{
    List<string> Validate(GetCircuitResultsParameter parameter);
    List<string> Validate(GetCircuitsParameter parameter);
}