using FormulaOne.Application.Constants;
using FormulaOne.Application.Interfaces;

namespace FormulaOne.Application.Parameters;

public record GetDriverResultsParameter(
    string? Id,
    string? DriverId,
    string? DriverName,
    string? SortField,
    string? SortOrder,
    string? Year,
    int Page = QueryParameterConstant.DefaultPage,
    int PageSize = QueryParameterConstant.DefaultPageSize) : IParameters;
