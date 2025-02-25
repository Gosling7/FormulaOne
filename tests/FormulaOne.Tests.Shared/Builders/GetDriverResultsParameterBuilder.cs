using FormulaOne.Application.Constants;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Tests.Shared.Builders;

public class GetDriverResultsParameterBuilder
{
    private GetDriverResultsParameter _parameter = null!;

    public GetDriverResultsParameterBuilder SetDefaultValues()
    {
        _parameter = new GetDriverResultsParameter(
            Id: null,
            DriverId: null,
            DriverName: null,
            Year: null,
            SortField: null,
            SortOrder: null,
            Page: QueryParameterConstant.DefaultPage,
            PageSize: QueryParameterConstant.DefaultPageSize);

        return this;
    }

    public GetDriverResultsParameter Build()
    {
        return _parameter;
    }

    public GetDriverResultsParameterBuilder SetId(string? id)
    {
        _parameter = _parameter with { Id = id };
        return this;
    }

    public GetDriverResultsParameterBuilder SetDriverId(string? driverId)
    {
        _parameter = _parameter with { DriverId = driverId };
        return this;
    }

    public GetDriverResultsParameterBuilder SetDriverName(string? driverName)
    {
        _parameter = _parameter with { DriverName = driverName };
        return this;
    }

    public GetDriverResultsParameterBuilder SetYear(string? year)
    {
        _parameter = _parameter with { Year = year };
        return this;
    }

    public GetDriverResultsParameterBuilder SetSortField(string? sortField)
    {
        _parameter = _parameter with { SortField = sortField };
        return this;
    }

    public GetDriverResultsParameterBuilder SetSortOrder(string? sortOrder)
    {
        _parameter = _parameter with { SortOrder = sortOrder };
        return this;
    }

    public GetDriverResultsParameterBuilder SetPage(int page)
    {
        _parameter = _parameter with { Page = page };
        return this;
    }

    public GetDriverResultsParameterBuilder SetPageSize(int pageSize)
    {
        _parameter = _parameter with { PageSize = pageSize };
        return this;
    }
}
