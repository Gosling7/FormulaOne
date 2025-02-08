using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;
using FormulaOne.Core.Entities;

namespace FormulaOne.Application.Validators;

internal class QueryCircuitParameterValidator : IQueryCircuitParameterValidator
{
    private readonly IParameterValidatorHelper _validatorHelper;

    public QueryCircuitParameterValidator(IParameterValidatorHelper validatorHelper)
    {
        _validatorHelper = validatorHelper;
    }

    public List<string> Validate(GetCircuitsParameter parameters)
    {
        var errors = new List<string>();

        _validatorHelper.ValidateId(parameters.Id, errors);
        _validatorHelper.ValidatePagination(parameters.Page, errors);
        ValidateCircuitSorting(parameters.SortField, parameters.SortOrder, errors);

        return errors;
    }

    public List<string> Validate(GetCircuitResultsParameter parameters)
    {
        var errors = new List<string>();

        _validatorHelper.ValidateYear(parameters.Year, errors);
        _validatorHelper.ValidateId(parameters.Id, errors);
        _validatorHelper.ValidateId(parameters.CircuitId, errors,
            nameof(GetCircuitResultsParameter.CircuitId));
        _validatorHelper.ValidatePagination(parameters.Page, errors);
        _validatorHelper.ValidateResultSorting(parameters.SortField, parameters.SortOrder, errors);

        return errors;
    }

    private static void ValidateCircuitSorting(string? fieldParameter, string? orderParameter,
        List<string> errors)
    {
        var validSortFields = new[]
        {
            nameof(Circuit.Name).ToLower(),
            nameof(Circuit.Location).ToLower()
        };

        var field = fieldParameter?.ToLower();
        var order = orderParameter?.ToLower();

        if (!string.IsNullOrWhiteSpace(field))
        {
            if (!validSortFields.Contains(field))
            {
                errors.Add($"Invalid sorting field: {field}. " +
                    $"Valid values: {string.Join(", ", validSortFields)}.");
            }
        }

        if (!string.IsNullOrWhiteSpace(order))
        {
            if (string.IsNullOrWhiteSpace(field))
            {
                errors.Add($"Explicit sorting direction requires sorting field. " +
                    $"Valid SortField values: {string.Join(", ", validSortFields)}.");
            }

            var validSortDirections = new[] { "asc", "desc" };
            if (!validSortDirections.Contains(order))
            {
                errors.Add($"Invalid sorting direction: {order}. " +
                    $"Valid values: {string.Join(", ", validSortDirections)}.");
            }
        }
    }
}
