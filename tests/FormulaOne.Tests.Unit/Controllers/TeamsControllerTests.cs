using FormulaOne.Api.Controllers;
using FormulaOne.Application.Constants;
using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Interfaces;
using FormulaOne.Tests.Shared.Builders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;

namespace FormulaOne.Tests.Unit.Controllers;

public class TeamsControllerTests
{
    private readonly ITeamService _teamService = Substitute.For<ITeamService>();
    private readonly TeamsController _controller;

    private const string InvalidId = "invalidguid";

    public TeamsControllerTests()
    {
        _controller = new TeamsController(_teamService);
    }

    [Fact]
    public async Task GetTeams_should_return_200OK_when_teams_found()
    {
        // Arrange
        var parameters = new GetTeamsParameterBuilder()
            .SetDefaultValues()
            .Build();

        List<TeamDto> teams =
        [
            new TeamDto
            {
                Id = Guid.NewGuid().ToString(),
                Name = "name1"
            }
        ];
        var pagedTeams = new PagedResultBuilder<TeamDto>()
            .SetDefaultValues()
            .SetItems(teams)
            .Build();

        _teamService.GetTeams(parameters).Returns(pagedTeams);

        // Act
        var result = await _controller.GetTeams(parameters);

        // Assert
        var objectResult = result.Result as OkObjectResult;
        objectResult.ShouldNotBeNull();
        objectResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task GetTeams_should_return_400BadRequest_when_paged_result_has_error()
    {
        // Arrange
        var parameters = new GetTeamsParameterBuilder()
            .SetDefaultValues()
            .SetId(InvalidId)
            .Build();

        var pagedTeams = new PagedResultBuilder<TeamDto>()
            .SetDefaultValues()
            .SetErrors([ValidationMessage.InvalidGuid("Id", InvalidId)])
            .Build();

        _teamService.GetTeams(parameters).Returns(pagedTeams);

        // Act
        var result = await _controller.GetTeams(parameters);

        // Assert
        var objectResult = result.Result as BadRequestObjectResult;
        objectResult.ShouldNotBeNull();
        objectResult.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task GetTeams_should_return_404NotFound_when_teams_not_found()
    {
        // Arrange
        var parameters = new GetTeamsParameterBuilder()
            .SetDefaultValues()
            .Build();

        var pagedTeams = new PagedResultBuilder<TeamDto>()
            .SetDefaultValues()
            .SetItems([])
            .Build();

        _teamService.GetTeams(parameters).Returns(pagedTeams);

        // Act
        var result = await _controller.GetTeams(parameters);

        // Assert
        var notFoundResult = result.Result as NotFoundObjectResult;
        notFoundResult.ShouldNotBeNull();
        notFoundResult.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task GetTeamStandings_should_return_200OK_when_standings_found()
    {
        // Arrange
        var parameters = new GetTeamStandingsParameterBuilder()
            .SetDefaultValues()
            .Build();

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
        var pagedTeamStandings = new PagedResultBuilder<TeamStandingDto>()
            .SetDefaultValues()
            .SetItems(teamStandings)
            .Build();

        _teamService.GetTeamStandings(parameters).Returns(pagedTeamStandings);

        // Act
        var result = await _controller.GetTeamStandings(parameters);

        // Assert
        var objectResult = result.Result as OkObjectResult;
        objectResult.ShouldNotBeNull();
        objectResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task GetTeamStandings_should_return_400BadRequest_when_paged_result_has_error()
    {
        // Arrange
        var parameters = new GetTeamStandingsParameterBuilder()
            .SetDefaultValues()
            .SetId(InvalidId)
            .Build();

        var pagedTeamStandings = new PagedResultBuilder<TeamStandingDto>()
            .SetDefaultValues()
            .SetErrors([ValidationMessage.InvalidGuid("Id", InvalidId)])
            .Build();

        _teamService.GetTeamStandings(parameters).Returns(pagedTeamStandings);

        // Act
        var result = await _controller.GetTeamStandings(parameters);

        // Assert
        var objectResult = result.Result as BadRequestObjectResult;
        objectResult.ShouldNotBeNull();
        objectResult.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task GetTeamStandings_should_return_404NotFound_when_standings_not_found()
    {
        // Arrange
        var parameters = new GetTeamStandingsParameterBuilder()
            .SetDefaultValues()
            .Build();

        var pagedTeamStandings = new PagedResultBuilder<TeamStandingDto>()
            .SetDefaultValues()
            .SetItems([])
            .Build();

        _teamService.GetTeamStandings(parameters).Returns(pagedTeamStandings);

        // Act
        var result = await _controller.GetTeamStandings(parameters);

        // Assert
        var notFoundResult = result.Result as NotFoundObjectResult;
        notFoundResult.ShouldNotBeNull();
        notFoundResult.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task GetTeamResults_should_return_200OK_when_results_found()
    {
        // Arrange
        var parameters = new GetTeamResultsParameterBuilder()
            .SetDefaultValues()
            .Build();

        var teamResult = new RaceResultDtoBuilder()
            .SetDefaultValues()
            .Build();

        var pagedTeamResults = new PagedResultBuilder<RaceResultDto>()
            .SetDefaultValues()
            .SetItems([teamResult])
            .Build();

        _teamService.GetTeamResults(parameters).Returns(pagedTeamResults);

        // Act
        var result = await _controller.GetTeamResults(parameters);

        // Assert
        var objectResult = result.Result as OkObjectResult;
        objectResult.ShouldNotBeNull();
        objectResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task GetTeamResults_should_return_400BadRequest_when_paged_result_has_error()
    {
        // Arrange
        var parameters = new GetTeamResultsParameterBuilder()
            .SetDefaultValues()
            .Build();

        var pagedTeamResults = new PagedResultBuilder<RaceResultDto>()
            .SetDefaultValues()
            .SetErrors([ValidationMessage.InvalidGuid("Id", InvalidId)])
            .Build();

        _teamService.GetTeamResults(parameters).Returns(pagedTeamResults);

        // Act
        var result = await _controller.GetTeamResults(parameters);

        // Assert
        var objectResult = result.Result as BadRequestObjectResult;
        objectResult.ShouldNotBeNull();
        objectResult.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task GetTeamResults_should_return_404NotFound_when_results_not_found()
    {
        // Arrange
        var parameters = new GetTeamResultsParameterBuilder()
            .SetDefaultValues()
            .Build();

        var pagedTeamResults = new PagedResultBuilder<RaceResultDto>()
            .SetDefaultValues()
            .SetItems([])
            .Build();

        _teamService.GetTeamResults(parameters).Returns(pagedTeamResults);

        // Act
        var result = await _controller.GetTeamResults(parameters);

        // Assert
        var notFoundResult = result.Result as NotFoundObjectResult;
        notFoundResult.ShouldNotBeNull();
        notFoundResult.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
    }
}
