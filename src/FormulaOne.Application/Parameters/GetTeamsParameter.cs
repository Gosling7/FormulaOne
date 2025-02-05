using FormulaOne.Application.Constants;
using FormulaOne.Application.Interfaces;

namespace FormulaOne.Application.Parameters;

public record GetTeamsParameter(
    string? Id,
    string? Name,
    string? SortField,
    string? SortOrder,
    int PageSize = QueryParameterConstant.DefaultPageSize,
    int Page = QueryParameterConstant.DefaultPage) : IQueryParameter;
