using FormulaOne.Application.Interfaces;
using FormulaOne.Core.Entities;

namespace FormulaOne.Application.Helpers;

internal class ParameterValidatorHelper : IParameterValidatorHelper
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

    public void ValidatePagination(int? pageParameter, List<string> errors)
    {
        if (pageParameter <= 0)
        {
            errors.Add($"Invalid page number: {pageParameter}. " +
                $"Valid values are greater than 0.");
        }
    }

    public void ValidateYear(string? yearString, List<string> errors,
        bool isTeamStanding = false)
    {
        // TODO: year w walidacji do poprawy
        if (yearString is null)
        {
            return;
        }

        if (!int.TryParse(yearString, out var year))
        {
            // TODO: error gdy nie uda się zparsować inta w YearParameter
        }

        if (isTeamStanding)
        {
            //if (year < 1958)
            //{
            //    errors.Add($"Invalid year: {yearString}," +
            //    $"Valid years are between 1958 and 2024.");
            //}
        }

        //if (year < 1950)
        //{
        //    errors.Add($"Invalid year: {yearString}," +
        //        $"Valid years are between 1950 and 2024.");
        //}
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
