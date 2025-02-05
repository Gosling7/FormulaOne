using FormulaOne.Application.Constants;
using FormulaOne.Application.Interfaces;

namespace FormulaOne.Application.Parameters;

public record GetDriversParameter(
    string? Id,
    string? Name,
    string? Nationality,
    string? SortField,
    string? SortOrder,
    int PageSize = QueryParameterConstant.DefaultPageSize,
    int Page = QueryParameterConstant.DefaultPage) : IParameters;
