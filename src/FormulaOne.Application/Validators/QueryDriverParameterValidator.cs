using FormulaOne.Application.Helpers;
using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;
using FormulaOne.Core.Entities;

namespace FormulaOne.Application.Validators;

internal class QueryDriverParameterValidator : IQueryDriverParameterValidator
{
    public List<string> Validate(GetDriversParameter parameter)
    {
        var errors = new List<string>();

        QueryParameterValidatorHelper.ValidateId(parameter.Id, errors);
        QueryParameterValidatorHelper.ValidatePagination(parameter.Page, errors);
        ValidateNationality(parameter.Nationality, errors);
        ValidateDriverSorting(parameter.SortField, parameter.SortOrder, errors);

        return errors;
    }

    public List<string> Validate(GetDriverStandingsParameter parameter)
    {
        var errors = new List<string>();

        QueryParameterValidatorHelper.ValidateYear(parameter.Year, errors);
        QueryParameterValidatorHelper.ValidateId(parameter.Id, errors);
        // TODO: dodać walidowanie DriverId i tym podobne w reszcie walidatorów
        QueryParameterValidatorHelper.ValidatePagination(parameter.Page, errors);
        ValidateDriverStandingsSorting(parameter.SortField, parameter.SortOrder, errors);

        return errors;
    }

    public List<string> Validate(GetDriverResultsParameter parameter)
    {
        var errors = new List<string>();

        QueryParameterValidatorHelper.ValidateYear(parameter.Year, errors);
        QueryParameterValidatorHelper.ValidateId(parameter.Id, errors);
        QueryParameterValidatorHelper.ValidatePagination(parameter.Page, errors);
        ValidateDriverRaceResultsSorting(parameter.SortField, parameter.SortOrder, errors);

        return errors;
    }

    private static void ValidateNationality(string? nationalityParameter, List<string> errors)
    {
        if (!string.IsNullOrWhiteSpace(nationalityParameter))
        {
            if (nationalityParameter.Length != 3)
            {
                errors.Add($"Invalid nationality filter: {nationalityParameter}. " +
                    $"Valid values are three-letter codes, e.g. POL.");
            }
        }
    }

    private static void ValidateDriverSorting(string? fieldParameter, string? orderParameter,
        List<string> errors)
    {
        var validSortFields = new[]
        {
            nameof(Driver.Id),
            nameof(Driver.Nationality),
            "name"
        };

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

    private static void ValidateDriverStandingsSorting(string? fieldParameter,
        string? orderParameter, List<string> errors)
    {
        var validSortFields = new[]
        {
            nameof(DriverStanding.Id),
            nameof(DriverStanding.Year),
            nameof(DriverStanding.Position),
            nameof(DriverStanding.Points),
        };

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

    private static void ValidateDriverRaceResultsSorting(string? fieldParameter, string? orderParameter,
        List<string> errors)
    {
        var validSortFields = new[]
        {
            nameof(RaceResult.Date).ToLower(),
            nameof(RaceResult.Position).ToLower(),
            nameof(RaceResult.Points).ToLower(),
        };

        if (!string.IsNullOrWhiteSpace(fieldParameter?.ToLower()))
        {
            if (!validSortFields.Contains(fieldParameter.ToLower()))
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
}
