using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Helpers;
using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Application.Services;

internal class TeamService : ITeamService
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
        ServiceHelper pagedResultHelper)
    {
        _teamRepository = teamRepository;
        _validator = validator;
        _teamStandingRepository = teamStandingRepository;
        _raceResultRepository = raceResultRepository;
        _serviceHelper = pagedResultHelper;
    }

    public async Task<PagedResult<TeamDto>> GetTeams(GetTeamsParameter parameters)
    {
        return await _serviceHelper
            .ValidateAndReturnPagedResult<TeamDto, GetTeamsParameter>(
                parameters: parameters,
                fetchDataAsync: (param) => _teamRepository.GetTeamsAsync(param),
                validateQueryParmeters: (param) => _validator.Validate(param));
    }

    public async Task<PagedResult<TeamStandingDto>> GetTeamStandings(
        GetTeamStandingsParameter parameters)
    {
        return await _serviceHelper
            .ValidateAndReturnPagedResult<TeamStandingDto, GetTeamStandingsParameter>(
                parameters: parameters,
                fetchDataAsync: (param) => _teamStandingRepository.GetStandingsAsync(param),
                validateQueryParmeters: (param) => _validator.Validate(param));
    }

    public async Task<PagedResult<RaceResultDto>> GetTeamResults(
        GetTeamResultsParameter parameters)
    {
        return await _serviceHelper
            .ValidateAndReturnPagedResult<RaceResultDto, GetTeamResultsParameter>(
                parameters: parameters,
                fetchDataAsync: (param) => _raceResultRepository.GetRaceResultsAsync(param),
                validateQueryParmeters: (param) => _validator.Validate(param));
    }
}



//public async Task<PagedResult<TeamDto>> GetTeams(GetTeamsParameter parameters)
//    {
//        var errors = new List<string>();

//        errors = _validator.Validate(parameters);
//        if (errors.Count > 0)
//        {
//            return new PagedResult<TeamDto>(
//                CurrentPage: parameters.Page,
//                TotalPages: 0,
//                PageSize: parameters.PageSize,
//                TotalResults: 0,
//                Errors: errors,
//                Items: new List<TeamDto>());
//        }

//        var teamsWithCount = await _teamRepository.GetTeamsAsync(parameters);
//        var teamCount = teamsWithCount.Item1;
//        var teams = teamsWithCount.Item2;
//        var totalPages = MathF.Ceiling(Convert.ToSingle(teamCount) / parameters.PageSize);

//        return new PagedResult<TeamDto>(
//            CurrentPage: parameters.Page,
//            TotalPages: (int)totalPages,
//            PageSize: parameters.PageSize,
//            TotalResults: teamCount,
//            Errors: errors,
//            Items: teams);
//    }

//    public async Task<PagedResult<TeamStandingDto>> GetTeamStandings(
//        GetTeamStandingsParameter parameters)
//    {
//        var errors = new List<string>();

//        errors = _validator.Validate(parameters);
//        if (errors.Count > 0)
//        {
//            return new PagedResult<TeamStandingDto>(
//                CurrentPage: parameters.Page,
//                TotalPages: 0,
//                PageSize: parameters.PageSize,
//                TotalResults: 0,
//                Errors: errors,
//                Items: new List<TeamStandingDto>());
//        }

//        var teamStandingsWithCount = await _teamStandingRepository.GetTeamStandings(parameters);
//        var teamStandingCount = teamStandingsWithCount.Item1;
//        var teamStandings = teamStandingsWithCount.Item2;
//        var totalPages = MathF.Ceiling(Convert.ToSingle(teamStandingCount) / parameters.PageSize);

//        return new PagedResult<TeamStandingDto>(
//            CurrentPage: parameters.Page,
//            TotalPages: (int)totalPages,
//            PageSize: parameters.PageSize,
//            TotalResults: teamStandingCount,
//            Errors: errors,
//            Items: teamStandings);
//    }

//    public async Task<PagedResult<RaceResultDto>> GetTeamResults(
//        GetTeamResultsParameter parameters)
//    {
//        var errors = new List<string>();

//        errors = _validator.Validate(parameters);
//        if (errors.Count > 0)
//        {
//            return new PagedResult<RaceResultDto>(
//                CurrentPage: parameters.Page,
//                TotalPages: 0,
//                PageSize: parameters.PageSize,
//                TotalResults: 0,
//                Errors: errors,
//                Items: new List<RaceResultDto>());
//        }

//        var raceResultsWithCount = await _raceResultRepository.GetRaceResultsAsync(parameters);
//        var raceResultCount = raceResultsWithCount.Item1;
//        var raceResults = raceResultsWithCount.Item2;
//        var totalPages = MathF.Ceiling(Convert.ToSingle(raceResultCount) / parameters.PageSize);

//        return new PagedResult<RaceResultDto>(
//            CurrentPage: parameters.Page,
//            TotalPages: (int)totalPages,
//            PageSize: parameters.PageSize,
//            TotalResults: raceResultCount,
//            Errors: errors,
//            Items: raceResults);
//    }
//}