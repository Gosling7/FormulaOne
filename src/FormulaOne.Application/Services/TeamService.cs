using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Helpers;
using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Application.Services;

public class TeamService : ITeamService
{
    private readonly IQueryTeamParameterValidator _validator;
    private readonly ITeamRepository _teamRepository;
    private readonly ITeamStandingRepository _teamStandingRepository;
    private readonly IRaceResultRepository _raceResultRepository;
    private readonly ServiceHelper _serviceHelper;

    public TeamService(IQueryTeamParameterValidator validator,
        ITeamRepository teamRepository,
        ITeamStandingRepository teamStandingRepository,
        IRaceResultRepository raceResultRepository,
        ServiceHelper serviceHelper)
    {
        _validator = validator;
        _teamRepository = teamRepository;
        _teamStandingRepository = teamStandingRepository;
        _raceResultRepository = raceResultRepository;
        _serviceHelper = serviceHelper;
    }

    public async Task<PagedResult<TeamDto>> GetTeams(GetTeamsParameter parameters)
    {
        return await _serviceHelper
            .ValidateAndReturnPagedResult<TeamDto, GetTeamsParameter>(
                parameters: parameters,
                fetchDataAsync: (param) => _teamRepository.GetItemsAsync(param),
                validateQueryParmeters: (param) => _validator.Validate(param));
    }

    public async Task<PagedResult<TeamStandingDto>> GetTeamStandings(
        GetTeamStandingsParameter parameters)
    {
        return await _serviceHelper
            .ValidateAndReturnPagedResult<TeamStandingDto, GetTeamStandingsParameter>(
                parameters: parameters,
                fetchDataAsync: (param) => _teamStandingRepository.GetItemsAsync(param),
                validateQueryParmeters: (param) => _validator.Validate(param));
    }

    public async Task<PagedResult<RaceResultDto>> GetTeamResults(
        GetTeamResultsParameter parameters)
    {
        return await _serviceHelper
            .ValidateAndReturnPagedResult<RaceResultDto, GetTeamResultsParameter>(
                parameters: parameters,
                fetchDataAsync: (param) => _raceResultRepository.GetItemsAsync(param),
                validateQueryParmeters: (param) => _validator.Validate(param));
    }
}