namespace FormulaOne.Application.Parameters;

public record GetRaceResultsParameters(
    string Id,
    int YearFilter,
    int Page,
    int PageSize,
    int MaxPageSize,
    string NameFilter,
    string Sort);
