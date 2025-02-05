using FormulaOne.Application.Constants;

namespace FormulaOne.Application.Interfaces;

public interface IQueryParameter
{
    string? SortField { get; }
    string? SortOrder { get; }
    int PageSize { get; }
    int Page { get; }
}
