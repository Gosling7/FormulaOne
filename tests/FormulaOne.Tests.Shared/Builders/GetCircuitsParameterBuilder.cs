using FormulaOne.Application.Constants;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Tests.Shared.Builders;

public class GetCircuitsParameterBuilder
{
    private GetCircuitsParameter _parameter = null!;

    public GetCircuitsParameterBuilder SetDefaultValues()
    {
        _parameter = new GetCircuitsParameter(
            Id: null,
            Name: null,
            Location: null,
            SortField: null,
            SortOrder: null,
            Page: QueryParameterConstant.DefaultPage,
            PageSize: QueryParameterConstant.DefaultPageSize);

        return this;
    }

    public GetCircuitsParameter Build()
    {
        return _parameter;
    }

    public GetCircuitsParameterBuilder SetId(string? id)
    {
        _parameter = _parameter with { Id = id };
        return this;
    }

    public GetCircuitsParameterBuilder SetName(string? name)
    {
        _parameter = _parameter with { Name = name };
        return this;
    }

    public GetCircuitsParameterBuilder SetLocation(string? location)
    {
        _parameter = _parameter with { Location = location };
        return this;
    }

    public GetCircuitsParameterBuilder SetSortField(string? sortField)
    {
        _parameter = _parameter with { SortField = sortField };
        return this;
    }

    public GetCircuitsParameterBuilder SetSortOrder(string? sortOrder)
    {
        _parameter = _parameter with { SortOrder = sortOrder };
        return this;
    }

    public GetCircuitsParameterBuilder SetPage(int page)
    {
        _parameter = _parameter with { Page = page };
        return this;
    }

    public GetCircuitsParameterBuilder SetPageSize(int pageSize)
    {
        _parameter = _parameter with { PageSize = pageSize };
        return this;
    }
}
