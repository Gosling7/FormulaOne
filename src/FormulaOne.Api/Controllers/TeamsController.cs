using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Api.Controllers;

[Route("api/[controller]")]
[ApiController]

public class TeamsController : ControllerBase
{
    private readonly ITeamService _teamService;

    public TeamsController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    // api/teams
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedResult<TeamDto>>> GetTeams(
        [FromQuery] GetTeamsParameter parameters)
    {
        var teamsPagedResult = await _teamService.GetTeams(parameters);
        if (teamsPagedResult.Errors.Count != 0)
        {
            return BadRequest(teamsPagedResult);
        }

        return Ok(teamsPagedResult);
    }

    // api/teams/standings
    [HttpGet("Standings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedResult<TeamStandingDto>>> GetTeamStandings(
        [FromQuery] GetTeamStandingsParameter parameters)
    {
        var teamStandingsPagedResult = await _teamService.GetTeamStandings(parameters);
        if (teamStandingsPagedResult.Errors.Count != 0)
        {
            return BadRequest(teamStandingsPagedResult);
        }

        return Ok(teamStandingsPagedResult);
    }

    [HttpGet("Results")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedResult<TeamStandingDto>>> GetTeamResults(
        [FromQuery] GetTeamResultsParameter parameters)
    {
        var teamResultsPagedResult = await _teamService.GetTeamResults(parameters);
        if (teamResultsPagedResult.Errors.Count != 0)
        {
            return BadRequest(teamResultsPagedResult);
        }

        return Ok(teamResultsPagedResult);
    }
}
