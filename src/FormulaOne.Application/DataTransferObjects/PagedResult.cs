namespace FormulaOne.Application.DataTransferObjects;

public record PagedResult<TDataType>(
    int CurrentPage,
    int TotalPages,
    int PageSize,
    int TotalResults,
    IReadOnlyCollection<string> Errors, // TODO: typ z FluentValidation?
    IEnumerable<TDataType> Items);