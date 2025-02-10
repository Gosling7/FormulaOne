using FormulaOne.Application.Constants;
using FormulaOne.Application.DataTransferObjects;

namespace FormulaOne.Tests.Unit.Builders;

internal class PagedResultBuilder<TDataType>
{
    private PagedResult<TDataType> _result = null!;

    public PagedResultBuilder<TDataType> SetDefaultValues()
    {
        _result = new PagedResult<TDataType>(
            CurrentPage: 0,
            TotalPages: 0,
            PageSize: QueryParameterConstant.DefaultPageSize,
            TotalResults: 0,
            Errors: [],
            Items: new List<TDataType>());

        return this;
    }

    public PagedResult<TDataType> Build()
    {
        return _result;
    }

    public PagedResultBuilder<TDataType> SetCurrentPage(int currentPage)
    {
        _result = _result with { CurrentPage = currentPage };
        return this;
    }

    public PagedResultBuilder<TDataType> SetTotalPages(int totalPages)
    {
        _result = _result with { TotalPages = totalPages };
        return this;
    }

    public PagedResultBuilder<TDataType> SetPageSize(int pageSize)
    {
        _result = _result with { PageSize = pageSize };
        return this;
    }

    public PagedResultBuilder<TDataType> SetTotalResults(int totalResults)
    {
        _result = _result with { TotalPages = totalResults };
        return this;
    }

    public PagedResultBuilder<TDataType> SetErrors(IReadOnlyCollection<string> errors)
    {
        _result = _result with { Errors = errors };
        return this;
    }

    public PagedResultBuilder<TDataType> SetItems(IEnumerable<TDataType> items)
    {
        _result = _result with { Items = items };
        return this;
    }
}
