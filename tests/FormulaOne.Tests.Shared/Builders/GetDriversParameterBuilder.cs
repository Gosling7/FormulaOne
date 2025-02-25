using FormulaOne.Application.Constants;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Tests.Shared.Builders;

public class GetDriversParameterBuilder
{
    private GetDriversParameter _parameter = null!;

    public GetDriversParameterBuilder SetDefaultValues()
    {
        _parameter = new GetDriversParameter(
            Id: null,
            Name: null,
            Nationality: null,
            SortField: null,
            SortOrder: null,
            PageSize: QueryParameterConstant.DefaultPageSize,
            Page: QueryParameterConstant.DefaultPage);

        return this;
    }

    public GetDriversParameter Build()
    {
        return _parameter;
    }

    public GetDriversParameterBuilder SetId(string? id)
    {
        _parameter = _parameter with { Id = id };
        return this;
    }

    public GetDriversParameterBuilder SetName(string? name)
    {
        _parameter = _parameter with { Name = name };
        return this;
    }

    public GetDriversParameterBuilder SetNationality(string? nationality)
    {
        _parameter = _parameter with { Nationality = nationality };
        return this;
    }

    public GetDriversParameterBuilder SetSortField(string? sortField)
    {
        _parameter = _parameter with { SortField = sortField };
        return this;
    }

    public GetDriversParameterBuilder SetSortOrder(string? sortOrder)
    {
        _parameter = _parameter with { SortOrder = sortOrder };
        return this;
    }

    public GetDriversParameterBuilder SetPage(int page)
    {
        _parameter = _parameter with { Page = page };
        return this;
    }

    public GetDriversParameterBuilder SetPageSize(int pageSize)
    {
        _parameter = _parameter with { PageSize = pageSize };
        return this;
    }
}
