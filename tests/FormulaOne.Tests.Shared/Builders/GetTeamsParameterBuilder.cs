using FormulaOne.Application.Constants;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Tests.Shared.Builders;

public class GetTeamsParameterBuilder
{
    private GetTeamsParameter _parameter = null!;

    public GetTeamsParameterBuilder SetDefaultValues()
    {
        _parameter = new GetTeamsParameter(
            Id: null,
            Name: null,
            SortField: null,
            SortOrder: null,
            PageSize: QueryParameterConstant.DefaultPageSize,
            Page: QueryParameterConstant.DefaultPage);

        return this;
    }

    public GetTeamsParameter Build()
    {
        return _parameter;
    }

    public GetTeamsParameterBuilder SetId(string? id)
    {
        _parameter = _parameter with { Id = id };
        return this;
    }

    public GetTeamsParameterBuilder SetName(string? name)
    {
        _parameter = _parameter with { Name = name };
        return this;
    }

    public GetTeamsParameterBuilder SetSortField(string? sortField)
    {
        _parameter = _parameter with { SortField = sortField };
        return this;
    }

    public GetTeamsParameterBuilder SetSortOrder(string? sortOrder)
    {
        _parameter = _parameter with { SortOrder = sortOrder };
        return this;
    }

    public GetTeamsParameterBuilder SetPage(int page)
    {
        _parameter = _parameter with { Page = page };
        return this;
    }

    public GetTeamsParameterBuilder SetPageSize(int pageSize)
    {
        _parameter = _parameter with { PageSize = pageSize };
        return this;
    }
}

