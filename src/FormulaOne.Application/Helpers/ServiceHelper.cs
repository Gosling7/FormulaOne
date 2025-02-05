using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Interfaces;

namespace FormulaOne.Application.Helpers;

internal class ServiceHelper
{
    public async Task<PagedResult<TDto>> ValidateAndReturnPagedResult<TDto, TQueryParameter>(
        TQueryParameter parameters,
        Func<TQueryParameter, Task<(int, IEnumerable<TDto>)>> fetchDataAsync,
        Func<TQueryParameter, IReadOnlyCollection<string>> validateQueryParmeters) 
        where TQueryParameter : IQueryParameter
    {
        var errors = validateQueryParmeters(parameters);
        if (errors.Any())
        {
            return new PagedResult<TDto>(
                CurrentPage: parameters.Page,
                TotalPages: 0,
                PageSize: parameters.PageSize,
                TotalResults: 0,
                Errors: errors,
                Items: new List<TDto>());
        }

        var (totalCount, items) = await fetchDataAsync(parameters);
        var totalPages = (int)MathF.Ceiling((float)totalCount / parameters.PageSize);

        return new PagedResult<TDto>(
            CurrentPage: parameters.Page,
            TotalPages: totalPages,
            PageSize: parameters.PageSize,
            TotalResults: totalCount,
            Errors: errors,
            Items: items);
    }
}
