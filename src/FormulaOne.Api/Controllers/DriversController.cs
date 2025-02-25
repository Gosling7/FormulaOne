using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Api.Controllers;

[Route("api/[controller]")]
[ApiController]

public class DriversController : ControllerBase
{
    private readonly IDriverService _driverService;

    public DriversController(IDriverService driverService)
    {
        _driverService = driverService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PagedResult<DriverDto>>> GetDrivers(
        [FromQuery] GetDriversParameter parameters)
    {
        var driversPagedResult = await _driverService.GetDrivers(parameters);
        if (driversPagedResult.Errors.Count != 0)
        {
            return BadRequest(driversPagedResult);
        }

        if (!driversPagedResult.Items.Any())
        {
            return NotFound(driversPagedResult);
        }

        return Ok(driversPagedResult);
    }

    [HttpGet("Standings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PagedResult<DriverStandingDto>>> GetDriverStandings(
        [FromQuery] GetDriverStandingsParameter parameter)
    {
        var driverStandingsPagedResult = await _driverService.GetDriverStandings(parameter);
        if (driverStandingsPagedResult.Errors.Count != 0)
        {
            return BadRequest(driverStandingsPagedResult);
        }

        if (!driverStandingsPagedResult.Items.Any())
        {
            return NotFound(driverStandingsPagedResult);
        }

        return Ok(driverStandingsPagedResult);
    }

    [HttpGet("Results")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PagedResult<TeamStandingDto>>> GetDriverResults(
        [FromQuery] GetDriverResultsParameter parameters)
    {
        var driverResultsPagedResult = await _driverService.GetDriverResults(parameters);
        if (driverResultsPagedResult.Errors.Count != 0)
        {
            return BadRequest(driverResultsPagedResult);
        }

        if (!driverResultsPagedResult.Items.Any())
        {
            return NotFound(driverResultsPagedResult);
        }

        return Ok(driverResultsPagedResult);
    }
}
