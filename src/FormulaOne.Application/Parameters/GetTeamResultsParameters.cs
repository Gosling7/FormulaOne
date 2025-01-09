using FormulaOne.Application.Constants;

namespace FormulaOne.Application.Parameters;

public record GetTeamResultsParameters(
    string? Id,
    string? TeamId,
    string? TeamName,
    string? SortField,
    string? SortOrder,
    string? Year,
    int Page = QueryParameterConstant.DefaultPage,
    int PageSize = QueryParameterConstant.DefaultPageSize);
