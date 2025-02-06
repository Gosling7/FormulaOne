using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Application.Interfaces;

public interface ICircuitService
{
    Task<PagedResult<RaceResultDto>> GetCircuitResults(GetCircuitResultsParameter parameters);
    Task<PagedResult<CircuitDto>> GetCircuits(GetCircuitsParameter parameters);
}