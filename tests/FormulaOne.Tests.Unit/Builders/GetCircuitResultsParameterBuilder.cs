using FormulaOne.Application.Constants;
using FormulaOne.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.Tests.Unit.Builders;

public class GetCircuitResultsParameterBuilder
{
    private GetCircuitResultsParameter _parameter = null!;

    public GetCircuitResultsParameterBuilder SetDefaultValues()
    {
        _parameter = new GetCircuitResultsParameter(
            Id: null,
            CircuitId: null,
            CircuitName: null,
            CircuitLocation: null,
            Year: null,
            SortField: null,
            SortOrder: null,
            Page: QueryParameterConstant.DefaultPage,
            PageSize: QueryParameterConstant.DefaultPageSize);

        return this;
    }

    public GetCircuitResultsParameter Build()
    {
        return _parameter;
    }

    public GetCircuitResultsParameterBuilder SetId(string? id)
    {
        _parameter = _parameter with { Id = id };
        return this;
    }

    public GetCircuitResultsParameterBuilder SetCircuitId(string? circuitId)
    {
        _parameter = _parameter with { CircuitId = circuitId };
        return this;
    }

    public GetCircuitResultsParameterBuilder SetCircuitName(string? circuitName)
    {
        _parameter = _parameter with { CircuitName = circuitName };
        return this;
    }

    public GetCircuitResultsParameterBuilder SetCircuitLocation(string? circuitLocation)
    {
        _parameter = _parameter with { CircuitLocation = circuitLocation };
        return this;
    }

    public GetCircuitResultsParameterBuilder SetYear(string? year)
    {
        _parameter = _parameter with { Year = year };
        return this;
    }

    public GetCircuitResultsParameterBuilder SetSortField(string? sortField)
    {
        _parameter = _parameter with { SortField = sortField };
        return this;
    }

    public GetCircuitResultsParameterBuilder SetSortOrder(string? sortOrder)
    {
        _parameter = _parameter with { SortOrder = sortOrder };
        return this;
    }

    public GetCircuitResultsParameterBuilder SetPage(int page)
    {
        _parameter = _parameter with { Page = page };
        return this;
    }

    public GetCircuitResultsParameterBuilder SetPageSize(int pageSize)
    {
        _parameter = _parameter with { PageSize = pageSize };
        return this;
    }
}
