using FormulaOne.Application.Interfaces;
using FormulaOne.Core.Entities;
using System.Reflection.Metadata;

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

    public void ValidateSorting(string? fieldParameter, string? orderParameter,
        List<string> errors)
    {
        var validSortFields = new[] { "id", "name" };

        if (!string.IsNullOrWhiteSpace(fieldParameter))
        {
            if (!validSortFields.Contains(fieldParameter))
            {
                errors.Add($"Invalid sorting field: {fieldParameter}. " +
                    $"Valid values: {string.Join(", ", validSortFields)}.");
            }
        }

        if (!string.IsNullOrWhiteSpace(orderParameter))
        {
            if (string.IsNullOrWhiteSpace(fieldParameter))
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

    public void ValidatePagination(int pageParameter, List<string> errors)
    {
        if (pageParameter <= 0)
        {
            errors.Add($"Invalid page number: {pageParameter}. " +
                $"Valid values are greater than 0.");
        }
    }

    public void ValidateYear(string? yearsParameter, List<string> errors,
        bool isTeamStanding = false)
    {
        if (yearsParameter is null)
        {
            return;
        }

        var yearParts = yearsParameter.Split("-");
        if (yearParts.Length != 2)
        {
            errors.Add($"Invalid year format: {yearsParameter}. " +
                GetValidYearsFormatMessagePart(yearsParameter));
            return;
        }

        if (!int.TryParse(yearParts[0], out var startYear))
        {
            errors.Add($"Start year is not a number: {yearsParameter}. " +
                GetValidYearsFormatMessagePart(yearsParameter));
        }
        if (!int.TryParse(yearParts[1], out var endYear))
        {
            errors.Add($"End year is not a number: {yearsParameter}. " +
                GetValidYearsFormatMessagePart(yearsParameter));
        }

        if (startYear == default || endYear == default)
        {
            return;
        }

        var validEndYear = DateTime.Now.Year;
        var validStartYear = isTeamStanding ? 1958 : 1950;
        if (startYear > endYear)
        {
            errors.Add(GetInvalidYearRangeMessagePart(yearsParameter) +
                $"The start year ({startYear}) cannot be greater than the end year ({endYear}). " +
                GetValidRangeMessagePart(validStartYear, validEndYear));
        }

        if (startYear < validStartYear)
        {
            errors.Add(GetInvalidYearRangeMessagePart(yearsParameter) +
                $"The start year ({startYear}) cannot be greater than {validStartYear}. " +
                GetValidRangeMessagePart(validStartYear, validEndYear));
        }
        if (endYear > validEndYear)
        {
            errors.Add(GetInvalidYearRangeMessagePart(yearsParameter) +
                $"The end year ({endYear}) cannot be less than the {validEndYear}). " +
                GetValidRangeMessagePart(validStartYear, validEndYear));
        }

        static string GetValidYearsFormatMessagePart(string yearsParameter) 
            => $"Valid format is: StartYear-EndYear, e.g., 2000-2024. ";

        static string GetInvalidYearRangeMessagePart(string yearsParameter) 
            => $"Invalid year range: {yearsParameter}. ";

        static string GetValidRangeMessagePart(int validStartYear, int validEndYear) 
            => $"Valid range is between {validStartYear} and {validEndYear}. ";
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
}
