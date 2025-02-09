using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;
using FormulaOne.Core.Entities;

namespace FormulaOne.Application.Validators;

internal class QueryCircuitParameterValidator : IQueryCircuitParameterValidator
{
    private readonly IParameterValidatorHelper _validatorHelper;

    private readonly List<string> _circuitValidSortFields =
    [
        nameof(Circuit.Name).ToLower(),
        nameof(Circuit.Location).ToLower()
    ];

    public QueryCircuitParameterValidator(IParameterValidatorHelper validatorHelper)
    {
        _validatorHelper = validatorHelper;
    }

    public List<string> Validate(GetCircuitsParameter parameters)
    {
        var errors = new List<string>();

        _validatorHelper.ValidateId(parameters.Id, errors);
        _validatorHelper.ValidatePagination(parameters.Page, errors);
        _validatorHelper.ValidateSorting(parameters.SortField, parameters.SortOrder,
            _circuitValidSortFields, errors);

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
}
