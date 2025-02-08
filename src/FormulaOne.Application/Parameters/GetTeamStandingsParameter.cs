using FormulaOne.Application.Constants;
using FormulaOne.Application.Interfaces;

namespace FormulaOne.Application.Parameters;

public record GetTeamStandingsParameter(
    string? Id,
    string? TeamId,
    string? TeamName,
    string? Year,
    string? SortField,
    string? SortOrder,
    int Page = QueryParameterConstant.DefaultPage,
    int PageSize = QueryParameterConstant.DefaultPageSize) : IQueryParameter;