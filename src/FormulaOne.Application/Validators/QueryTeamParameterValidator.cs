using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;
using FormulaOne.Core.Entities;

namespace FormulaOne.Application.Validators;

internal class QueryTeamParameterValidator : IQueryTeamParameterValidator
{
    private readonly IParameterValidatorHelper _validatorHelper;

    private readonly List<string> _teamValidSortFields =
    [
        nameof(Team.Name).ToLower()
    ];
    private readonly List<string> _teamStandingValidSortFields =
    [
        nameof(TeamStanding.Year).ToLower(),
        nameof(TeamStanding.Position).ToLower(),
        nameof(TeamStanding.Points).ToLower(),
    ];

    public QueryTeamParameterValidator(IParameterValidatorHelper validatorHelper)
    {
        _validatorHelper = validatorHelper;
    }

    public List<string> Validate(GetTeamsParameter parameters)
    {
        var errors = new List<string>();

        _validatorHelper.ValidateId(parameters.Id, errors);
        _validatorHelper.ValidatePagination(parameters.Page, errors);
        _validatorHelper.ValidateSorting(parameters.SortField, parameters.SortOrder,
            _teamValidSortFields, errors);

        return errors;
    }

    public List<string> Validate(GetTeamStandingsParameter parameters)
    {
        var errors = new List<string>();

        _validatorHelper.ValidateYear(parameters.Year, errors, isTeamStanding: true);
        _validatorHelper.ValidateId(parameters.Id, errors);
        _validatorHelper.ValidatePagination(parameters.Page, errors);
        _validatorHelper.ValidateSorting(parameters.SortField, parameters.SortOrder,
            _teamStandingValidSortFields, errors);

        return errors;
    }

    public List<string> Validate(GetTeamResultsParameter parameters)
    {
        var errors = new List<string>();

        _validatorHelper.ValidateYear(parameters.Year, errors);
        _validatorHelper.ValidateId(parameters.Id, errors);
        _validatorHelper.ValidateId(parameters.TeamId, errors, nameOfIdParameter: nameof(parameters.TeamId));
        _validatorHelper.ValidatePagination(parameters.Page, errors);
        _validatorHelper.ValidateResultSorting(parameters.SortField, parameters.SortOrder, errors);

        return errors;
    }
}