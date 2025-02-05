using FormulaOne.Application.Constants;
using FormulaOne.Application.Interfaces;

namespace FormulaOne.Application.Parameters;

public record GetDriverStandingsParameter(
    string? Id,
    string? DriverId,
    string? Year,
    string? SortField,
    string? SortOrder,
    int PageSize = QueryParameterConstant.DefaultPageSize,
    int Page = QueryParameterConstant.DefaultPage) : IParameters;
