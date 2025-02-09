using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;
using FormulaOne.Core.Entities;

namespace FormulaOne.Application.Validators;

internal class QueryDriverParameterValidator : IQueryDriverParameterValidator
{
    private readonly IParameterValidatorHelper _validatorHelper;

    private readonly List<string> _driverValidSortFields =
    [
        nameof(Driver.Nationality).ToLower(),
        nameof(Driver.FirstName).ToLower(),
        nameof(Driver.LastName).ToLower(),
    ];
    private readonly List<string> _driverStandingValidSortFields =
    [
        nameof(DriverStanding.Year).ToLower(),
        nameof(DriverStanding.Position).ToLower(),
        nameof(DriverStanding.Points).ToLower(),
    ];

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
        _validatorHelper.ValidateSorting(parameters.SortField, parameters.SortOrder,
            _driverValidSortFields, errors);

        return errors;
    }

    public List<string> Validate(GetDriverStandingsParameter parameters)
    {
        var errors = new List<string>();

        _validatorHelper.ValidateYear(parameters.Year, errors);
        _validatorHelper.ValidateId(parameters.Id, errors);
        _validatorHelper.ValidateId(parameters.DriverId, errors, 
            nameOfIdParameter: nameof(GetDriverStandingsParameter.DriverId));
        _validatorHelper.ValidatePagination(parameters.Page, errors);
        _validatorHelper.ValidateSorting(parameters.SortField, parameters.SortOrder,
            _driverStandingValidSortFields, errors);

        return errors;
    }

    public List<string> Validate(GetDriverResultsParameter parameters)
    {
        var errors = new List<string>();

        _validatorHelper.ValidateYear(parameters.Year, errors);
        _validatorHelper.ValidateId(parameters.Id, errors);
        _validatorHelper.ValidatePagination(parameters.Page, errors);
        _validatorHelper.ValidateResultSorting(parameters.SortField, parameters.SortOrder, errors);

        return errors;
    }

    private static void ValidateNationality(string? nationalityParameter, List<string> errors)
    {
        if (string.IsNullOrWhiteSpace(nationalityParameter))
        {
            return;
        }

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
