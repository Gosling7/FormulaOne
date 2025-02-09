namespace FormulaOne.Application.Constants;

public static class ValidationMessage
{
    public static string InvalidGuid(string parameterName, string value) =>
        $"{parameterName} is not a valid Guid: {value}.";

    public static string InvalidPageNumber(int pageNumber) =>
        $"Invalid page number: {pageNumber}. Valid values are greater than 0.";

    public static string InvalidYearFormat(string year) =>
        $"Invalid year format: {year}. {GetValidYearFormatExample()}";

    public static string InvalidSingleYearFilter(string year, 
        int validStartYear, int validEndYear) =>
        $"Invalid year filter: {year}. " +
        $"The year cannot be less than {validStartYear} or greater than {validEndYear}.";
    
    public static string YearFilterNotNumber(string year) =>
        $"Year filter cannot contain non-numeric values: {year}. {GetValidYearFormatExample()}";

    // Sorting
    public static string InvalidSortField(string field, IEnumerable<string> validFields) =>
        $"Invalid sorting field: {field}. Valid values: {string.Join(", ", validFields)}.";

    public static string ExplicitSortingRequiresSortField(IEnumerable<string> validFields) =>
        $"Explicit sorting order requires sorting field. " +
        $"Valid values: {string.Join(", ", validFields)}.";

    public static string InvalidSortOrder(string order, IEnumerable<string> validSortDirections) =>
        $"Invalid sorting direction: {order}. " +
        $"Valid values: {string.Join(", ", validSortDirections)}.";

    // Start year
    public static string StartYearGreaterThanEndYear(int startYear, int endYear, 
        int validStartYear, int validEndYear) =>
        $"The start year ({startYear}) cannot be greater than the end year ({endYear}). " +
        $"{GetValidRangeMessage(validStartYear, validEndYear)}";

    public static string StartYearGreaterThanValidEndYear(int startYear,
        int validStartYear, int validEndYear) =>
        $"The start year ({startYear}) cannot be greater than {validEndYear}. " +
        $"{GetValidRangeMessage(validStartYear, validEndYear)}";

    public static string StartYearLessThanValidStartYear(int startYear,
        int validStartYear, int validEndYear) =>
        $"The start year ({startYear}) cannot be less than {validStartYear}. " +
        $"{GetValidRangeMessage(validStartYear, validEndYear)}";

    // End year
    public static string EndYearGreaterThanValidEndYear(int endYear,
        int validStartYear, int validEndYear) =>
        $"The end year ({endYear}) cannot be greater than {validEndYear}. " +
        $"{GetValidRangeMessage(validStartYear, validEndYear)}";

    public static string EndYearLessThanValidStartYear(int endYear,
        int validStartYear, int validEndYear) =>
        $"The end year ({endYear}) cannot be less than {validStartYear}. " +
        $"{GetValidRangeMessage(validStartYear, validEndYear)}";

    private static string GetValidYearFormatExample() =>
        "Valid formats are: single year e.g., 2000; or year range e.g., 2000-2020.";

    private static string GetValidRangeMessage(int startYear, int endYear) =>
        $"Valid range is between {startYear} and {endYear}.";
}
