using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Infrastructure.Repositories;
public interface ICircuitRepository
{
    Task<(int, IReadOnlyCollection<CircuitDto>)> GetItemsAsync(GetCircuitsParameter parameters);
}