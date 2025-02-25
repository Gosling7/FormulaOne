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

public class DriversControllerTests
{
    private readonly IDriverService _driverService = Substitute.For<IDriverService>();
    private readonly DriversController _controller;

    private const string InvalidId = "invalidguid";

    public DriversControllerTests()
    {
        _controller = new DriversController(_driverService);
    }

    [Fact]
    public async Task GetDrivers_should_return_200OK_when_drivers_found()
    {
        // Arrange
        var parameters = new GetDriversParameterBuilder()
            .SetDefaultValues()
            .Build();

        List<DriverDto> drivers =
        [
            new DriverDto
            {
                Id = Guid.NewGuid().ToString(),
                Name = "name1"
            }
        ];
        var pagedDrivers = new PagedResultBuilder<DriverDto>()
            .SetDefaultValues()
            .SetItems(drivers)
            .Build();

        _driverService.GetDrivers(parameters).Returns(pagedDrivers);

        // Act
        var result = await _controller.GetDrivers(parameters);

        // Assert
        var objectResult = result.Result as OkObjectResult;
        objectResult.ShouldNotBeNull();
        objectResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task GetDrivers_should_return_400BadRequest_when_paged_result_has_error()
    {
        // Arrange
        var parameters = new GetDriversParameterBuilder()
            .SetDefaultValues()
            .SetId(InvalidId)
            .Build();

        var pagedDrivers = new PagedResultBuilder<DriverDto>()
            .SetDefaultValues()
            .SetErrors([ValidationMessage.InvalidGuid("Id", InvalidId)])
            .Build();

        _driverService.GetDrivers(parameters).Returns(pagedDrivers);

        // Act
        var result = await _controller.GetDrivers(parameters);

        // Assert
        var objectResult = result.Result as BadRequestObjectResult;
        objectResult.ShouldNotBeNull();
        objectResult.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task GetDrivers_should_return_404NotFound_when_drivers_not_found()
    {
        // Arrange
        var parameters = new GetDriversParameterBuilder()
            .SetDefaultValues()
            .Build();

        var pagedDrivers = new PagedResultBuilder<DriverDto>()
            .SetDefaultValues()
            .SetItems([])
            .Build();

        _driverService.GetDrivers(parameters).Returns(pagedDrivers);

        // Act
        var result = await _controller.GetDrivers(parameters);

        // Assert
        var notFoundResult = result.Result as NotFoundObjectResult;
        notFoundResult.ShouldNotBeNull();
        notFoundResult.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task GetDriverStandings_should_return_200OK_when_standings_found()
    {
        // Arrange
        var parameters = new GetDriverStandingsParameterBuilder()
            .SetDefaultValues()
            .Build();

        List<DriverStandingDto> driverStandings =
        [
            new DriverStandingDto
            {
                Id = Guid.NewGuid().ToString(),
                Position = 1,
                DriverId = Guid.NewGuid().ToString(),
                DriverName = "driver name",
                Nationality = "ITA",
                TeamId = Guid.NewGuid().ToString(),
                TeamName = "team name",
                Points = 25,
                Year = ValidationConstant.StartYear
            }
        ];
        var pagedDriverStandings = new PagedResultBuilder<DriverStandingDto>()
            .SetDefaultValues()
            .SetItems(driverStandings)
            .Build();

        _driverService.GetDriverStandings(parameters).Returns(pagedDriverStandings);

        // Act
        var result = await _controller.GetDriverStandings(parameters);

        // Assert
        var objectResult = result.Result as OkObjectResult;
        objectResult.ShouldNotBeNull();
        objectResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task GetDriverStandings_should_return_400BadRequest_when_paged_result_has_error()
    {
        // Arrange
        var parameters = new GetDriverStandingsParameterBuilder()
            .SetDefaultValues()
            .SetId(InvalidId)
            .Build();

        var pagedDriverStandings = new PagedResultBuilder<DriverStandingDto>()
            .SetDefaultValues()
            .SetErrors([ValidationMessage.InvalidGuid("Id", InvalidId)])
            .Build();

        _driverService.GetDriverStandings(parameters).Returns(pagedDriverStandings);

        // Act
        var result = await _controller.GetDriverStandings(parameters);

        // Assert
        var objectResult = result.Result as BadRequestObjectResult;
        objectResult.ShouldNotBeNull();
        objectResult.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task GetDriverStandings_should_return_404NotFound_when_standings_not_found()
    {
        // Arrange
        var parameters = new GetDriverStandingsParameterBuilder()
            .SetDefaultValues()
            .Build();

        var pagedDriverStandings = new PagedResultBuilder<DriverStandingDto>()
            .SetDefaultValues()
            .SetItems([])
            .Build();

        _driverService.GetDriverStandings(parameters).Returns(pagedDriverStandings);

        // Act
        var result = await _controller.GetDriverStandings(parameters);

        // Assert
        var notFoundResult = result.Result as NotFoundObjectResult;
        notFoundResult.ShouldNotBeNull();
        notFoundResult.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task GetDriverResults_should_return_200OK_when_results_found()
    {
        // Arrange
        var parameters = new GetDriverResultsParameterBuilder()
            .SetDefaultValues()
            .Build();

        var driverResult = new RaceResultDtoBuilder()
            .SetDefaultValues()
            .Build();

        var pagedDriverResults = new PagedResultBuilder<RaceResultDto>()
            .SetDefaultValues()
            .SetItems([driverResult])
            .Build();

        _driverService.GetDriverResults(parameters).Returns(pagedDriverResults);

        // Act
        var result = await _controller.GetDriverResults(parameters);

        // Assert
        var objectResult = result.Result as OkObjectResult;
        objectResult.ShouldNotBeNull();
        objectResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task GetDriverResults_should_return_400BadRequest_when_paged_result_has_error()
    {
        // Arrange
        var parameters = new GetDriverResultsParameterBuilder()
            .SetDefaultValues()
            .Build();

        var pagedDriverResults = new PagedResultBuilder<RaceResultDto>()
            .SetDefaultValues()
            .SetErrors([ValidationMessage.InvalidGuid("Id", InvalidId)])
            .Build();

        _driverService.GetDriverResults(parameters).Returns(pagedDriverResults);

        // Act
        var result = await _controller.GetDriverResults(parameters);

        // Assert
        var objectResult = result.Result as BadRequestObjectResult;
        objectResult.ShouldNotBeNull();
        objectResult.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task GetDriversResults_should_return_404NotFound_when_results_not_found()
    {
        // Arrange
        var parameters = new GetDriverResultsParameterBuilder()
            .SetDefaultValues()
            .Build();

        var pagedDriverResults = new PagedResultBuilder<RaceResultDto>()
            .SetDefaultValues()
            .SetItems([])
            .Build();

        _driverService.GetDriverResults(parameters).Returns(pagedDriverResults);

        // Act
        var result = await _controller.GetDriverResults(parameters);

        // Assert
        var notFoundResult = result.Result as NotFoundObjectResult;
        notFoundResult.ShouldNotBeNull();
        notFoundResult.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
    }
}
