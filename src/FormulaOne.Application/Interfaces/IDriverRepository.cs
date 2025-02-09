using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Application.Interfaces;

public interface IDriverRepository 
{
    Task<(int, IReadOnlyCollection<DriverDto>)> GetItemsAsync(GetDriversParameter parameters);
}
