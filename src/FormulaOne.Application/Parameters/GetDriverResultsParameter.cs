using FormulaOne.Application.Constants;
using FormulaOne.Application.Interfaces;

namespace FormulaOne.Application.Parameters;

public record GetDriverResultsParameter(
    string? Id,
    string? DriverId,
    string? DriverName,
    string? Year,
    string? SortField,
    string? SortOrder,
    int Page = QueryParameterConstant.DefaultPage,
    int PageSize = QueryParameterConstant.DefaultPageSize) : IQueryParameter;
