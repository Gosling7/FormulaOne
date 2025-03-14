﻿using FormulaOne.Api.Controllers;
using FormulaOne.Application.Constants;
using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Interfaces;
using FormulaOne.Tests.Shared.Builders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;

namespace FormulaOne.Tests.Unit.Controllers;

public class CircuitsControllerTests
{
    private readonly ICircuitService _circuitService = Substitute.For<ICircuitService>();
    private readonly CircuitsController _controller;

    private const string InvalidId = "invalidguid";

    public CircuitsControllerTests()
    {
        _controller = new CircuitsController(_circuitService);
    }

    [Fact]
    public async Task GetCircuits_should_return_200OK_when_circuits_found()
    {
        // Arrange
        var parameters = new GetCircuitsParameterBuilder()
            .SetDefaultValues()
            .Build();

        List<CircuitDto> circuits =
        [
            new CircuitDto
            {
                Id = Guid.NewGuid().ToString(),
                Name = "name1",
                Location = "location"
            }
        ];
        var pagedCircuits = new PagedResultBuilder<CircuitDto>()
            .SetDefaultValues()
            .SetItems(circuits)
            .Build();

        _circuitService.GetCircuits(parameters).Returns(pagedCircuits);

        // Act
        var result = await _controller.GetCitcuits(parameters);

        // Assert
        var objectResult = result.Result as OkObjectResult;
        objectResult.ShouldNotBeNull();
        objectResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task GetCircuits_should_return_400BadRequest_when_paged_result_has_error()
    {
        // Arrange
        var parameters = new GetCircuitsParameterBuilder()
            .SetDefaultValues()
            .SetId(InvalidId)
            .Build();

        var pagedCircuits = new PagedResultBuilder<CircuitDto>()
            .SetDefaultValues()
            .SetErrors([ValidationMessage.InvalidGuid("Id", InvalidId)])
            .Build();

        _circuitService.GetCircuits(parameters).Returns(pagedCircuits);

        // Act
        var result = await _controller.GetCitcuits(parameters);

        // Assert
        var objectResult = result.Result as BadRequestObjectResult;
        objectResult.ShouldNotBeNull();
        objectResult.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task GetCircuits_should_return_404NotFound_when_circuits_not_found()
    {
        // Arrange
        var parameters = new GetCircuitsParameterBuilder()
            .SetDefaultValues()
            .Build();

        var pagedCircuits = new PagedResultBuilder<CircuitDto>()
            .SetDefaultValues()
            .SetItems([])
            .Build();

        _circuitService.GetCircuits(parameters).Returns(pagedCircuits);

        // Act
        var result = await _controller.GetCitcuits(parameters);

        // Assert
        var notFoundResult = result.Result as NotFoundObjectResult;
        notFoundResult.ShouldNotBeNull();
        notFoundResult.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task GetCircuitResults_should_return_200OK_when_results_found()
    {
        // Arrange
        var parameters = new GetCircuitResultsParameterBuilder()
            .SetDefaultValues()
            .Build();

        var circuitResult = new RaceResultDtoBuilder()
            .SetDefaultValues()
            .Build();

        var pagedCircuitResults = new PagedResultBuilder<RaceResultDto>()
            .SetDefaultValues()
            .SetItems([circuitResult])
            .Build();

        _circuitService.GetCircuitResults(parameters).Returns(pagedCircuitResults);

        // Act
        var result = await _controller.GetCircuitResults(parameters);

        // Assert
        var objectResult = result.Result as OkObjectResult;
        objectResult.ShouldNotBeNull();
        objectResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task GetCircuitResults_should_return_400BadRequest_when_paged_result_has_error()
    {
        // Arrange
        var parameters = new GetCircuitResultsParameterBuilder()
            .SetDefaultValues()
            .Build();

        var pagedCircuitResults = new PagedResultBuilder<RaceResultDto>()
            .SetDefaultValues()
            .SetErrors([ValidationMessage.InvalidGuid("Id", InvalidId)])
            .Build();

        _circuitService.GetCircuitResults(parameters).Returns(pagedCircuitResults);

        // Act
        var result = await _controller.GetCircuitResults(parameters);

        // Assert
        var objectResult = result.Result as BadRequestObjectResult;
        objectResult.ShouldNotBeNull();
        objectResult.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task GetCircuitResults_should_return_404NotFound_when_results_not_found()
    {
        // Arrange
        var parameters = new GetCircuitResultsParameterBuilder()
            .SetDefaultValues()
            .Build();

        var pagedCircuitResults = new PagedResultBuilder<RaceResultDto>()
            .SetDefaultValues()
            .SetItems([])
            .Build();

        _circuitService.GetCircuitResults(parameters).Returns(pagedCircuitResults);

        // Act
        var result = await _controller.GetCircuitResults(parameters);

        // Assert
        var notFoundResult = result.Result as NotFoundObjectResult;
        notFoundResult.ShouldNotBeNull();
        notFoundResult.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
    }
}
