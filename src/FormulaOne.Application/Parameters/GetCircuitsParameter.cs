using FormulaOne.Application.Constants;
using FormulaOne.Application.Interfaces;

namespace FormulaOne.Application.Parameters;

public record GetCircuitsParameter(
    string? Id,
    string? Name,
    string? Location,
    string? SortField,
    string? SortOrder,
    int Page = QueryParameterConstant.DefaultPage,
    int PageSize = QueryParameterConstant.DefaultPageSize) : IQueryParameter;
