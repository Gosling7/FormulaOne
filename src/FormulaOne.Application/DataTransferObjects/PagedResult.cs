namespace FormulaOne.Application.DataTransferObjects;

public record PagedResult<TDataType>(
    int CurrentPage,
    int TotalPages,
    int PageSize,
    int TotalResults,
    IReadOnlyCollection<string> Errors,
    IEnumerable<TDataType> Items);