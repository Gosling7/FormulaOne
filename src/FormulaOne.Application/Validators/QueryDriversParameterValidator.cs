using FormulaOne.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.Application.Validators;

internal class QueryDriversParameterValidator : QueryParameterValidatorBase
{
    public IEnumerable<string> Validate(GetDriversParameters parameters)
    {
        var errors = new List<string>();

        base.ValidateId(parameters.Id, errors);
        base.ValidateSorting(parameters.Sort, errors);
        base.ValidatePagination(parameters.Page, errors);

        return errors;
    }

    public IEnumerable<string> Validate(GetDriverStandingsParameters parameters)
    {
        var errors = new List<string>();

        //ValidateId(parameters.Id, errors);
        //ValidateName(parameters.Name, errors);
        //ValidateSorting(parameters.Sort, errors);
        //ValidatePagination(parameters.Page, errors);

        return errors;
    }

    public IEnumerable<string> Validate(GetDriverResultsParameters parameters)
    {
        var errors = new List<string>();

        return errors;
    }
}
