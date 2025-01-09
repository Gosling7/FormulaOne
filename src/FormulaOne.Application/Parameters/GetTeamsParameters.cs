using FormulaOne.Application.Constants;

namespace FormulaOne.Application.Parameters;

public record GetTeamsParameters(
    string? Id,
    string? Name,
    string? SortField,
    string? SortOrder,
    int PageSize = QueryParameterConstant.DefaultPageSize,
    int Page = QueryParameterConstant.DefaultPage);
