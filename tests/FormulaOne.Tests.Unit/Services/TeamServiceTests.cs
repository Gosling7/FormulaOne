using FormulaOne.Application.Constants;
using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;
using FormulaOne.Application.Services;
using NSubstitute;
using Shouldly;

namespace FormulaOne.Tests.Unit.Services;

public class TeamServiceTests : ServiceTestsBase
{
    private readonly ITeamRepository _teamRepository = Substitute.For<ITeamRepository>();
    private readonly ITeamStandingRepository _teamStandingRepository =
        Substitute.For<ITeamStandingRepository>();

    private readonly IQueryTeamParameterValidator _validator =
        Substitute.For<IQueryTeamParameterValidator>();

    private readonly ITeamService _service;

    public TeamServiceTests()
    {
        _service = new TeamService(
            validator: _validator,
            teamRepository: _teamRepository,
            teamStandingRepository: _teamStandingRepository,
            raceResultRepository: _raceResultRepository,
            serviceHelper: new Application.Helpers.ServiceHelper());
    }

    [Fact]
    public async void GetTeams_should_return_paged_result_without_errors()
    {
        // Arrange 
        var parameters = new GetTeamsParameter(
            Id: null,
            Name: null,
            SortField: null,
            SortOrder: null,
            PageSize: QueryParameterConstant.DefaultPageSize,
            Page: QueryParameterConstant.DefaultPage);
        List<TeamDto> teams =
        [
            new TeamDto { Id = Guid.NewGuid().ToString(), Name = "name1" }
        ];

        _teamRepository.GetItemsAsync(parameters).Returns((1, teams));
        _validator.Validate(parameters).Returns([]);

        // Act
        var result = await _service.GetTeams(parameters);

        // Assert
        var expectedPagedResult = new PagedResult<TeamDto>(
            CurrentPage: parameters.Page,
            TotalPages: 1,
            PageSize: parameters.PageSize,
            TotalResults: 1,
            Errors: new List<string>(),
            Items: teams);
        result.ShouldBeEquivalentTo(expectedPagedResult);
    }

    [Fact]
    public async void GetTeams_should_return_paged_result_with_error_when_validation_fails()
    {
        // Arrange 
        var parameters = new GetTeamsParameter(
            Id: InvalidId,
            Name: null,
            SortField: null,
            SortOrder: null,
            PageSize: QueryParameterConstant.DefaultPageSize,
            Page: QueryParameterConstant.DefaultPage);

        List<string> errors = [ValidationMessage.InvalidGuid("Id", InvalidId)];
        _validator.Validate(parameters).Returns(errors);

        // Act
        var result = await _service.GetTeams(parameters);

        // Assert
        var expectedResult = new PagedResult<TeamDto>(
            CurrentPage: parameters.Page,
            TotalPages: 0,
            PageSize: parameters.PageSize,
            TotalResults: 0,
            Errors: errors,
            Items: new List<TeamDto>());
        result.ShouldBeEquivalentTo(expectedResult);
    }

    [Fact]
    public async void GetTeamStandings_should_return_paged_result_without_errors()
    {
        // Arrange 
        var parameters = new GetTeamStandingsParameter(
            Id: null,
            TeamId: null,
            TeamName: null,
            Year: null,
            SortField: null,
            SortOrder: null,
            PageSize: QueryParameterConstant.DefaultPageSize,
            Page: QueryParameterConstant.DefaultPage);
        List<TeamStandingDto> teamStandings =
        [
            new TeamStandingDto
            {
                Id = Guid.NewGuid().ToString(),
                Position = 1,
                Year = ValidationConstant.TeamStandingStartYear,
                TeamId = Guid.NewGuid().ToString(),
                TeamName = "team name",
                Points = 25
            }
        ];

        _teamStandingRepository.GetItemsAsync(parameters).Returns((1, teamStandings));
        _validator.Validate(parameters).Returns([]);

        // Act
        var result = await _service.GetTeamStandings(parameters);

        // Assert
        var expectedPagedResult = new PagedResult<TeamStandingDto>(
            CurrentPage: parameters.Page,
            TotalPages: 1,
            PageSize: parameters.PageSize,
            TotalResults: 1,
            Errors: new List<string>(),
            Items: teamStandings);
        result.ShouldBeEquivalentTo(expectedPagedResult);
    }

    [Fact]
    public async void GetTeamStandings_should_return_paged_result_with_error_when_validation_fails()
    {
        // Arrange 
        var parameters = new GetTeamStandingsParameter(
            Id: InvalidId,
            TeamId: null,
            TeamName: null,
            Year: null,
            SortField: null,
            SortOrder: null,
            PageSize: QueryParameterConstant.DefaultPageSize,
            Page: QueryParameterConstant.DefaultPage);

        List<string> errors = [ValidationMessage.InvalidGuid("Id", InvalidId)];
        _validator.Validate(parameters).Returns(errors);

        // Act
        var result = await _service.GetTeamStandings(parameters);

        // Assert
        var expectedPagedResult = new PagedResult<TeamStandingDto>(
            CurrentPage: parameters.Page,
            TotalPages: 0,
            PageSize: parameters.PageSize,
            TotalResults: 0,
            Errors: errors,
            Items: new List<TeamStandingDto>());
        result.ShouldBeEquivalentTo(expectedPagedResult);
    }

    [Fact]
    public async void GetTeamResults_should_return_paged_result_without_errors()
    {
        // Arrange 
        var parameters = new GetTeamResultsParameter(
            Id: null,
            TeamId: null,
            TeamName: null,
            Year: null,
            SortField: null,
            SortOrder: null,
            PageSize: QueryParameterConstant.DefaultPageSize,
            Page: QueryParameterConstant.DefaultPage);
        var teamResults = base.GetRaceResults();

        _raceResultRepository.GetItemsAsync(parameters).Returns((1, teamResults));
        _validator.Validate(parameters).Returns([]);

        // Act
        var result = await _service.GetTeamResults(parameters);

        // Assert
        var expectedPagedResult = new PagedResult<RaceResultDto>(
            CurrentPage: parameters.Page,
            TotalPages: 1,
            PageSize: parameters.PageSize,
            TotalResults: 1,
            Errors: new List<string>(),
            Items: teamResults);
        result.ShouldBeEquivalentTo(expectedPagedResult);
    }

    [Fact]
    public async void GetTeamResults_should_return_paged_result_with_error_when_validation_fails()
    {
        // Arrange 
        var parameters = new GetTeamResultsParameter(
            Id: InvalidId,
            TeamId: null,
            TeamName: null,
            Year: null,
            SortField: null,
            SortOrder: null,
            PageSize: QueryParameterConstant.DefaultPageSize,
            Page: QueryParameterConstant.DefaultPage);

        List<string> errors = [ValidationMessage.InvalidGuid("Id", InvalidId)];
        _validator.Validate(parameters).Returns(errors);

        // Act
        var result = await _service.GetTeamResults(parameters);

        // Assert
        var expectedPagedResult = new PagedResult<RaceResultDto>(
            CurrentPage: parameters.Page,
            TotalPages: 0,
            PageSize: parameters.PageSize,
            TotalResults: 0,
            Errors: errors,
            Items: new List<RaceResultDto>());
        result.ShouldBeEquivalentTo(expectedPagedResult);
    }
}
