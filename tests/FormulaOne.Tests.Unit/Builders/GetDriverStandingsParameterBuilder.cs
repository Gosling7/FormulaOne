using FormulaOne.Application.Constants;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Tests.Unit.Builders;

public class GetDriverStandingsParameterBuilder
{
    private GetDriverStandingsParameter _parameter = null!;

    public GetDriverStandingsParameterBuilder SetDefaultValues()
    {
        _parameter = new GetDriverStandingsParameter(
            Id: null,
            DriverId: null,
            Year: null,
            SortField: null,
            SortOrder: null,
            PageSize: QueryParameterConstant.DefaultPageSize,
            Page: QueryParameterConstant.DefaultPage);

        return this;
    }

    public GetDriverStandingsParameter Build()
    {
        return _parameter;
    }

    public GetDriverStandingsParameterBuilder SetId(string? id)
    {
        _parameter = _parameter with { Id = id };
        return this;
    }

    public GetDriverStandingsParameterBuilder SetDriverId(string? driverId)
    {
        _parameter = _parameter with { DriverId = driverId };
        return this;
    }

    public GetDriverStandingsParameterBuilder SetYear(string? year)
    {
        _parameter = _parameter with { Year = year };
        return this;
    }

    public GetDriverStandingsParameterBuilder SetSortField(string? sortField)
    {
        _parameter = _parameter with { SortField = sortField };
        return this;
    }

    public GetDriverStandingsParameterBuilder SetSortOrder(string? sortOrder)
    {
        _parameter = _parameter with { SortOrder = sortOrder };
        return this;
    }

    public GetDriverStandingsParameterBuilder SetPage(int page)
    {
        _parameter = _parameter with { Page = page };
        return this;
    }

    public GetDriverStandingsParameterBuilder SetPageSize(int pageSize)
    {
        _parameter = _parameter with { PageSize = pageSize };
        return this;
    }
}
