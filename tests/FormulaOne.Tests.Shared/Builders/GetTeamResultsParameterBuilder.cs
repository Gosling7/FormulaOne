using FormulaOne.Application.Constants;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Tests.Shared.Builders;

public class GetTeamResultsParameterBuilder
{
    private GetTeamResultsParameter _parameter = null!;

    public GetTeamResultsParameterBuilder SetDefaultValues()
    {
        _parameter = new GetTeamResultsParameter(
            Id: null,
            TeamId: null,
            TeamName: null,
            Year: null,
            SortField: null,
            SortOrder: null,
            Page: QueryParameterConstant.DefaultPage,
            PageSize: QueryParameterConstant.DefaultPageSize);

        return this;
    }

    public GetTeamResultsParameter Build()
    {
        return _parameter;
    }

    public GetTeamResultsParameterBuilder SetId(string? id)
    {
        _parameter = _parameter with { Id = id };
        return this;
    }

    public GetTeamResultsParameterBuilder SetTeamId(string? teamId)
    {
        _parameter = _parameter with { TeamId = teamId };
        return this;
    }

    public GetTeamResultsParameterBuilder SetTeamName(string? teamName)
    {
        _parameter = _parameter with { TeamName = teamName };
        return this;
    }

    public GetTeamResultsParameterBuilder SetYear(string? year)
    {
        _parameter = _parameter with { Year = year };
        return this;
    }

    public GetTeamResultsParameterBuilder SetSortField(string? sortField)
    {
        _parameter = _parameter with { SortField = sortField };
        return this;
    }

    public GetTeamResultsParameterBuilder SetSortOrder(string? sortOrder)
    {
        _parameter = _parameter with { SortOrder = sortOrder };
        return this;
    }

    public GetTeamResultsParameterBuilder SetPage(int page)
    {
        _parameter = _parameter with { Page = page };
        return this;
    }

    public GetTeamResultsParameterBuilder SetPageSize(int pageSize)
    {
        _parameter = _parameter with { PageSize = pageSize };
        return this;
    }
}
