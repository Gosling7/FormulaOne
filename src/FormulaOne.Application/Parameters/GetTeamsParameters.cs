namespace FormulaOne.Application.Parameters;

public record GetTeamsParameters(
    string Id, 
    int Page,
    int PageSize,
    int MaxPageSize,
    string NameFilter,
    string Sort);
