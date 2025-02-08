using FormulaOne.Application.Interfaces;
using FormulaOne.Core.Entities;

namespace FormulaOne.Application.Helpers;

public class ParameterValidatorHelper : IParameterValidatorHelper
{
    public void ValidateId(string? idParameter, List<string> errors,
        string? nameOfIdParameter = "Id")
    {
        if (!string.IsNullOrWhiteSpace(idParameter))
        {
            var ids = idParameter.Split(',');
            foreach (var id in ids)
            {
                if (!Guid.TryParse(id, out _))
                {
                    errors.Add($"{nameOfIdParameter} is not a valid Guid: {id}.");
                }
            }
        }
    }

    public void ValidatePagination(int pageParameter, List<string> errors)
    {
        if (pageParameter <= 0)
        {
            errors.Add($"Invalid page number: {pageParameter}. " +
                $"Valid values are greater than 0.");
        }
    }

    public void ValidateYear(string? yearParameter, List<string> errors,
        bool isTeamStanding = false)
    {
        if (string.IsNullOrWhiteSpace(yearParameter))
        {
            return;
        }

        var yearParts = yearParameter.Split("-");
        if (yearParts.Length > 2)
        {
            errors.Add($"Invalid year format: {yearParameter}. " +
                GetValidYearsFormatMessagePart(yearParameter));
            return;
        }

        var validEndYear = DateTime.Now.Year;
        var validStartYear = isTeamStanding ? 1958 : 1950;

        if (yearParts.Length == 1)
        {
            ValidateSingleYear(yearParameter, errors, validEndYear, validStartYear, yearParts);
            return;
        }

        ValidateYearRange(yearParameter, errors, validEndYear, validStartYear, yearParts);
    }

    public void ValidateResultSorting(string? fieldParameter,
        string? orderParameter, List<string> errors)
    {
        var validSortFields = new[]
        {
            nameof(RaceResult.Date).ToLower(),
            nameof(RaceResult.Position).ToLower(),
            nameof(RaceResult.Points).ToLower(),
        };

        var field = fieldParameter?.ToLower();

        if (!string.IsNullOrWhiteSpace(field)
            && !validSortFields.Contains(field))
        {
            errors.Add($"Invalid sorting field: {field}. " +
                $"Valid values: {string.Join(", ", validSortFields)}.");
        }

        if (!string.IsNullOrWhiteSpace(orderParameter))
        {
            if (string.IsNullOrWhiteSpace(field))
            {
                errors.Add($"Explicit sorting direction requires sorting field. " +
                    $"Valid SortField values: {string.Join(", ", validSortFields)}.");
            }

            var validSortDirections = new[] { "asc", "desc" };
            if (!validSortDirections.Contains(orderParameter))
            {
                errors.Add($"Invalid sorting direction: {orderParameter}. " +
                    $"Valid values: {string.Join(", ", validSortDirections)}.");
            }
        }
    }

    private static void ValidateSingleYear(string? yearParameter, List<string> errors,
        int validEndYear, int validStartYear, string[] yearParts)
    {
        if (!int.TryParse(yearParts[0], out var year))
        {
            errors.Add($"Invalid year filer: {yearParameter}. " +
                $"The year cannot be less than {validStartYear}.");
        }

        if (year < validStartYear)
        {
            errors.Add($"Invalid year filer: {year}. " +
                $"The year cannot be less than {validStartYear}.");
        }
        if (year > validEndYear)
        {
            errors.Add($"Invalid year filer: {year}. " +
                $"The year cannot be greater than {validEndYear}.");
        }
    }

    private static void ValidateYearRange(string? yearParameter, List<string> errors,
        int validEndYear, int validStartYear, string[] yearParts)
    {
        if (!int.TryParse(yearParts[0], out var startYear))
        {
            errors.Add($"Start year is not a number: {yearParameter}. " +
                GetValidYearsFormatMessagePart(yearParameter!));
        }
        if (!int.TryParse(yearParts[1], out var endYear))
        {
            errors.Add($"End year is not a number: {yearParameter}. " +
                GetValidYearsFormatMessagePart(yearParameter!));
        }

        if (startYear == default || endYear == default)
        {
            return;
        }

        if (startYear > endYear)
        {
            errors.Add(GetInvalidYearRangeMessagePart(yearParameter!) +
                $"The start year ({startYear}) cannot be greater than the end year ({endYear}). " +
                GetValidRangeMessagePart(validStartYear, validEndYear));
        }

        if (startYear < validStartYear)
        {
            errors.Add(GetInvalidYearRangeMessagePart(yearParameter!) +
                $"The start year ({startYear}) cannot be greater than {validStartYear}. " +
                GetValidRangeMessagePart(validStartYear, validEndYear));
        }
        if (endYear > validEndYear)
        {
            errors.Add(GetInvalidYearRangeMessagePart(yearParameter!) +
                $"The end year ({endYear}) cannot be less than the {validEndYear}). " +
                GetValidRangeMessagePart(validStartYear, validEndYear));
        }
    }

    private static string GetValidYearsFormatMessagePart(string yearsParameter)
        => $"Valid format is: StartYear-EndYear, e.g., 2000-2024. ";

    private static string GetInvalidYearRangeMessagePart(string yearsParameter)
        => $"Invalid year range: {yearsParameter}. ";

    private static string GetValidRangeMessagePart(int validStartYear, int validEndYear)
        => $"Valid range is between {validStartYear} and {validEndYear}. ";
}
