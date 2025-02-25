using FormulaOne.Application.Constants;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Tests.Shared.Builders;

public class GetTeamStandingsParameterBuilder
{
    private GetTeamStandingsParameter _parameter = null!;

    public GetTeamStandingsParameterBuilder SetDefaultValues()
    {
        _parameter = new GetTeamStandingsParameter(
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

    public GetTeamStandingsParameter Build()
    {
        return _parameter;
    }

    public GetTeamStandingsParameterBuilder SetId(string? id)
    {
        _parameter = _parameter with { Id = id };
        return this;
    }

    public GetTeamStandingsParameterBuilder SetTeamId(string? teamId)
    {
        _parameter = _parameter with { TeamId = teamId };
        return this;
    }

    public GetTeamStandingsParameterBuilder SetTeamName(string? teamName)
    {
        _parameter = _parameter with { TeamName = teamName };
        return this;
    }

    public GetTeamStandingsParameterBuilder SetYear(string? year)
    {
        _parameter = _parameter with { Year = year };
        return this;
    }

    public GetTeamStandingsParameterBuilder SetSortField(string? sortField)
    {
        _parameter = _parameter with { SortField = sortField };
        return this;
    }

    public GetTeamStandingsParameterBuilder SetSortOrder(string? sortOrder)
    {
        _parameter = _parameter with { SortOrder = sortOrder };
        return this;
    }

    public GetTeamStandingsParameterBuilder SetPage(int page)
    {
        _parameter = _parameter with { Page = page };
        return this;
    }

    public GetTeamStandingsParameterBuilder SetPageSize(int pageSize)
    {
        _parameter = _parameter with { PageSize = pageSize };
        return this;
    }
}
