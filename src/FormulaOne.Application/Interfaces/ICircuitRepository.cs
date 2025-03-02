using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Application.Interfaces;

public interface ICircuitRepository
{
    Task<(int, IReadOnlyCollection<CircuitDto>)> GetItemsAsync(GetCircuitsParameter parameters);
}