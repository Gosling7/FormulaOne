using FormulaOne.Application.Constants;
using FormulaOne.Application.Interfaces;

namespace FormulaOne.Application.Parameters;

public record GetTeamStandingsParameter(
    string? Id,
    string? TeamId,
    string? TeamName,
    string? SortField,
    string? SortOrder,
    string? Year,
    int Page = QueryParameterConstant.DefaultPage,
    int PageSize = QueryParameterConstant.DefaultPageSize) : IParameters;