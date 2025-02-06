using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;
using FormulaOne.Core.Entities;

namespace FormulaOne.Application.Validators;

internal class QueryTeamParameterValidator : IQueryTeamParameterValidator
{
    private readonly IParameterValidatorHelper _validatorHelper;

    public QueryTeamParameterValidator(IParameterValidatorHelper validatorHelper)
    {
        _validatorHelper = validatorHelper;
    }

    public List<string> Validate(GetTeamsParameter parameters)
    {
        var errors = new List<string>();

        _validatorHelper.ValidateId(parameters.Id, errors);
        _validatorHelper.ValidatePagination(parameters.Page, errors);
        ValidateTeamSorting(parameters.SortField, parameters.SortOrder, errors);

        return errors;
    }

    public List<string> Validate(GetTeamStandingsParameter parameters)
    {
        var errors = new List<string>();

        _validatorHelper.ValidateYear(parameters.Year, errors);
        _validatorHelper.ValidateId(parameters.Id, errors);
        _validatorHelper.ValidatePagination(parameters.Page, errors);
        ValidateTeamStandingSorting(parameters.SortField, parameters.SortOrder, errors);

        return errors;
    }

    public List<string> Validate(GetTeamResultsParameter parameters)
    {
        var errors = new List<string>();

        _validatorHelper.ValidateYear(parameters.Year, errors);
        _validatorHelper.ValidateId(parameters.Id, errors);
        _validatorHelper.ValidatePagination(parameters.Page, errors);
        _validatorHelper.ValidateResultSorting(parameters.SortField, parameters.SortOrder, errors);

        return errors;
    }

    private static void ValidateTeamSorting(string? fieldParameter, string? orderParameter,
        List<string> errors)
    {
        var validSortFields = new[]
        {
            nameof(TeamStanding.Id),
            nameof(TeamStanding.Year),
            nameof(TeamStanding.Position),
            nameof(TeamStanding.Points),
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

    private static void ValidateTeamStandingSorting(string? fieldParameter, string? orderParameter,
        List<string> errors)
    {
        var validSortFields = new[]
        {
            nameof(TeamStanding.Id),
            nameof(TeamStanding.Year),
            nameof(TeamStanding.Position),
            nameof(TeamStanding.Points),
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

    //private static void ValidateTeamRaceResultsSorting(string? fieldParameter, string? orderParameter,
    //    List<string> errors)
    //{
    //    var validSortFields = new[]
    //    {
    //        nameof(RaceResult.Date).ToLower(),
    //        nameof(TeamStanding.Position).ToLower(),
    //        nameof(TeamStanding.Points).ToLower(),
    //    };

    //    if (!string.IsNullOrWhiteSpace(fieldParameter?.ToLower()))
    //    {
    //        if (!validSortFields.Contains(fieldParameter.ToLower()))
    //        {
    //            errors.Add($"Invalid sorting field: {fieldParameter}. " +
    //                $"Valid values: {string.Join(", ", validSortFields)}.");
    //        }
    //    }

    //    if (!string.IsNullOrWhiteSpace(orderParameter))
    //    {
    //        if (string.IsNullOrWhiteSpace(fieldParameter))
    //        {
    //            errors.Add($"Explicit sorting direction requires sorting field. " +
    //                $"Valid SortField values: {string.Join(", ", validSortFields)}.");
    //        }

    //        var validSortDirections = new[] { "asc", "desc" };
    //        if (!validSortDirections.Contains(orderParameter))
    //        {
    //            errors.Add($"Invalid sorting direction: {orderParameter}. " +
    //                $"Valid values: {string.Join(", ", validSortDirections)}.");
    //        }
    //    }
    //}
}