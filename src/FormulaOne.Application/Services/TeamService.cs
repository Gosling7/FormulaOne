using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Application.Services;

internal class TeamService : ITeamService
{
    private readonly IQueryTeamsParameterValidator _validator;
    private readonly ITeamRepository _teamRepository;
    private readonly ITeamStandingRepository _teamStandingRepository;
    private readonly IRaceResultRepository _raceResultRepository;

    public TeamService(IQueryTeamsParameterValidator validator,
        ITeamRepository teamRepository,
        ITeamStandingRepository teamStandingRepository,
        IRaceResultRepository raceResultRepository)
    {
        _teamRepository = teamRepository;
        _validator = validator;
        _teamStandingRepository = teamStandingRepository;
        _raceResultRepository = raceResultRepository;
    }

    public async Task<PagedResult<TeamDto>> GetTeams(GetTeamsParameters parameters)
    {
        var errors = new List<string>();

        errors = _validator.Validate(parameters);

        if (errors.Count > 0)
        {
            return new PagedResult<TeamDto>(
                CurrentPage: parameters.Page,
                TotalPages: 0,
                PageSize: parameters.PageSize,
                TotalResults: 0,
                Errors: errors,
                Items: new List<TeamDto>());
        }

        var teamsWithCount = await _teamRepository.GetTeamsAsync(parameters);
        var teamCount = teamsWithCount.Item1;
        var teams = teamsWithCount.Item2;
        var totalPages = MathF.Ceiling(Convert.ToSingle(teamCount) / parameters.PageSize);

        return new PagedResult<TeamDto>(
            CurrentPage: parameters.Page,
            TotalPages: (int)totalPages,
            PageSize: parameters.PageSize,
            TotalResults: teamCount,
            Errors: errors,
            Items: teams);
    }

    public async Task<PagedResult<TeamStandingDto>> GetTeamStandings(GetTeamStandingsParameters parameters)
    {
        var errors = new List<string>();

        errors = _validator.Validate(parameters);
        if (errors.Count > 0)
        {
            return new PagedResult<TeamStandingDto>(
                CurrentPage: parameters.Page,
                TotalPages: 0,
                PageSize: parameters.PageSize,
                TotalResults: 0,
                Errors: errors,
                Items: new List<TeamStandingDto>());
        }

        var teamStandingsWithCount = await _teamStandingRepository.GetTeamStandings(parameters);
        var teamStandingCount = teamStandingsWithCount.Item1;
        var teamStandings = teamStandingsWithCount.Item2;
        var totalPages = MathF.Ceiling(Convert.ToSingle(teamStandingCount) / parameters.PageSize);

        return new PagedResult<TeamStandingDto>(
            CurrentPage: parameters.Page,
            TotalPages: (int)totalPages,
            PageSize: parameters.PageSize,
            TotalResults: teamStandingCount,
            Errors: errors,
            Items: teamStandings);
    }

    public async Task<PagedResult<RaceResultDto>> GetTeamResults(GetTeamResultsParameters parameters)
    {
        var errors = new List<string>();

        errors = _validator.Validate(parameters);
        if (errors.Count > 0)
        {
            return new PagedResult<RaceResultDto>(
                CurrentPage: parameters.Page,
                TotalPages: 0,
                PageSize: parameters.PageSize,
                TotalResults: 0,
                Errors: errors,
                Items: new List<RaceResultDto>());
        }

        var raceResultsWithCount = await _raceResultRepository.GetRaceResultsAsync(parameters);
        var raceResultCount = raceResultsWithCount.Item1;
        var raceResults = raceResultsWithCount.Item2;
        var totalPages = MathF.Ceiling(Convert.ToSingle(raceResultCount) / parameters.PageSize);

        return new PagedResult<RaceResultDto>(
            CurrentPage: parameters.Page,
            TotalPages: (int)totalPages,
            PageSize: parameters.PageSize,
            TotalResults: raceResultCount,
            Errors: errors,
            Items: raceResults);
    }
}