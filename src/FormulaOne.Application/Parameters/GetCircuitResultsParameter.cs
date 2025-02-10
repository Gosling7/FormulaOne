using FormulaOne.Application.Constants;
using FormulaOne.Application.Interfaces;

namespace FormulaOne.Application.Parameters;

public record GetCircuitResultsParameter(
    string? Id,
    string? CircuitId,
    string? CircuitName,
    string? CircuitLocation,
    string? Year,
    string? SortField,
    string? SortOrder,
    int Page = QueryParameterConstant.DefaultPage,
    int PageSize = QueryParameterConstant.DefaultPageSize) : IQueryParameter;