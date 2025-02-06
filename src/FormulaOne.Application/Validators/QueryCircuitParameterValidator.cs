using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.Application.Validators;

internal class QueryCircuitParameterValidator : IQueryCircuitParameterValidator
{
    private readonly IParameterValidatorHelper _validatorHelper;

    public QueryCircuitParameterValidator(IParameterValidatorHelper validatorHelper)
    {
        _validatorHelper = validatorHelper;
    }

    public List<string> Validate(GetCircuitsParameter parameter)
    {
        var errors = new List<string>();

        _validatorHelper.ValidateId(parameter.Id, errors);
        _validatorHelper.ValidatePagination(parameter.Page, errors);

        return errors;
    }

    public List<string> Validate(GetCircuitResultsParameter parameter)
    {
        var errors = new List<string>();

        _validatorHelper.ValidateYear(parameter.Year, errors);
        _validatorHelper.ValidateId(parameter.Id, errors);
        _validatorHelper.ValidateId(parameter.CircuitId, errors,
            nameof(GetCircuitResultsParameter.CircuitId));
        _validatorHelper.ValidatePagination(parameter.Page, errors);

        return errors; ;
    }
}
