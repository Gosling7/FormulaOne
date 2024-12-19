using FormulaOne.Application.Parameters;

namespace FormulaOne.Application.Validators;

// TODO: zrobić helpera: https://chatgpt.com/c/67671393-3770-8003-8173-b3b58b3a6daa
internal class QueryParameterValidator : QueryParameterValidatorBase
{
    public IEnumerable<string> Validate(GetTeamsParameters parameters)
    {
        var errors = new List<string>();

        base.ValidateId(parameters.Id, errors);
        base.ValidateSorting(parameters.Sort, errors);
        base.ValidatePagination(parameters.Page, errors);

        return errors;
    }

    public IEnumerable<string> Validate(GetTeamStandingsParameters parameters)
    {
        var errors = new List<string>();

        base.ValidateYear(parameters.Year, errors);
        base.ValidateId(parameters.Id, errors);
        base.ValidateSorting(parameters.Sort, errors);
        base.ValidatePagination(parameters.Page, errors);

        return errors;
    }

    public IEnumerable<string> Validate(GetTeamResultsParameters parameters)
    {
        var errors = new List<string>();

        base.ValidateYear(parameters.Year, errors);
        base.ValidateSession(parameters.Session, errors);
        base.ValidateId(parameters.Id, errors);
        base.ValidateSorting(parameters.Sort, errors);
        base.ValidatePagination(parameters.Page, errors);

        return errors;
    }
}