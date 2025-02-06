using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Infrastructure.Repositories;
public interface ICircuitRepository
{
    Task<(int, IEnumerable<CircuitDto>)> GetItemsAsync(GetCircuitsParameter parameters);
}