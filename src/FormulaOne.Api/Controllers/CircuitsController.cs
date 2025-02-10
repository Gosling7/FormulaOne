using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Api.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CircuitsController : ControllerBase
{
    private readonly ICircuitService _circuitService;

    public CircuitsController(ICircuitService circuitService)
    {
        _circuitService = circuitService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PagedResult<DriverDto>>> GetCitcuits(
        [FromQuery] GetCircuitsParameter parameters)
    {
        var circuitsPagedResult = await _circuitService.GetCircuits(parameters);
        if (circuitsPagedResult.Errors.Count != 0)
        {
            return BadRequest(circuitsPagedResult);
        }

        if (!circuitsPagedResult.Items.Any())
        {
            return NotFound(circuitsPagedResult);
        }

        return Ok(circuitsPagedResult);
    }

    [HttpGet("Results")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedResult<TeamStandingDto>>> GetCircuitResults(
        [FromQuery] GetCircuitResultsParameter parameters)
    {
        var circuitResultsPagedResult = await _circuitService.GetCircuitResults(parameters);
        if (circuitResultsPagedResult.Errors.Count != 0)
        {
            return BadRequest(circuitResultsPagedResult);
        }

        if (!circuitResultsPagedResult.Items.Any())
        {
            return NotFound(circuitResultsPagedResult);
        }

        return Ok(circuitResultsPagedResult);
    }
}
