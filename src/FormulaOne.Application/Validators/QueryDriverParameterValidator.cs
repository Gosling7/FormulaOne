using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;
using FormulaOne.Core.Entities;

namespace FormulaOne.Application.Validators;

internal class QueryDriverParameterValidator : IQueryDriverParameterValidator
{
    private readonly IParameterValidatorHelper _validatorHelper;

    public QueryDriverParameterValidator(IParameterValidatorHelper validatorHelper)
    {
        _validatorHelper = validatorHelper;
    }

    public List<string> Validate(GetDriversParameter parameters)
    {
        var errors = new List<string>();

        _validatorHelper.ValidateId(parameters.Id, errors);
        _validatorHelper.ValidatePagination(parameters.Page, errors);
        ValidateNationality(parameters.Nationality, errors);
        ValidateDriverSorting(parameters.SortField, parameters.SortOrder, errors);

        return errors;
    }

    public List<string> Validate(GetDriverStandingsParameter parameters)
    {
        var errors = new List<string>();

        _validatorHelper.ValidateYear(parameters.Year, errors);
        _validatorHelper.ValidateId(parameters.Id, errors);
        _validatorHelper.ValidateId(parameters.DriverId, errors, 
            nameof(GetDriverStandingsParameter.DriverId));
        _validatorHelper.ValidatePagination(parameters.Page, errors);
        ValidateDriverStandingsSorting(parameters.SortField, parameters.SortOrder, errors);

        return errors;
    }

    public List<string> Validate(GetDriverResultsParameter parameters)
    {
        var errors = new List<string>();

        _validatorHelper.ValidateYear(parameters.Year, errors);
        _validatorHelper.ValidateId(parameters.Id, errors);
        _validatorHelper.ValidatePagination(parameters.Page, errors);
        _validatorHelper.ValidateResultSorting(parameters.SortField, 
            parameters.SortOrder, errors);
        //ValidateDriverRaceResultsSorting(parameter.SortField, parameter.SortOrder, errors);

        return errors;
    }

    private static void ValidateNationality(string? nationalityParameter, List<string> errors)
    {
        if (!string.IsNullOrWhiteSpace(nationalityParameter))
        {
            var nationalities = nationalityParameter.Split(',');
            foreach (var nationality in nationalities)
            {
                if (nationality.Trim().Length != 3)
                {
                    errors.Add($"Invalid nationality filter: {nationality}. " +
                        $"Valid values are three-letter codes, e.g. POL.");
                }
            }            
        }
    }

    private static void ValidateDriverSorting(string? fieldParameter, string? orderParameter,
        List<string> errors)
    {
        var validSortFields = new[]
        {
            nameof(Driver.Nationality).ToLower(),
            nameof(Driver.FirstName).ToLower(),
            nameof(Driver.LastName).ToLower(),
        };

        var field = fieldParameter?.ToLower();

        if (!string.IsNullOrWhiteSpace(field))
        {
            if (!validSortFields.Contains(field))
            {
                errors.Add($"Invalid sorting field: {field}. " +
                    $"Valid values: {string.Join(", ", validSortFields)}.");
            }
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

    private static void ValidateDriverStandingsSorting(string? fieldParameter,
        string? orderParameter, List<string> errors)
    {
        var validSortFields = new[]
        {
            nameof(DriverStanding.Year).ToLower(),
            nameof(DriverStanding.Position).ToLower(),
            nameof(DriverStanding.Points).ToLower(),
        };

        var field = fieldParameter?.ToLower();

        if (!string.IsNullOrWhiteSpace(field))
        {
            if (!validSortFields.Contains(field))
            {
                errors.Add($"Invalid sorting field: {field}. " +
                    $"Valid values: {string.Join(", ", validSortFields)}.");
            }
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
