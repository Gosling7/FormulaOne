using FormulaOne.Application.Constants;
using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;
using FormulaOne.Application.Services;
using NSubstitute;
using Shouldly;

namespace FormulaOne.Tests.Unit.Services;

public class DriverServiceTests : ServiceTestsBase
{
    private readonly IDriverRepository _driverRepository = Substitute.For<IDriverRepository>();
    private readonly IDriverStandingRepository _driverStandingRepository =
        Substitute.For<IDriverStandingRepository>();

    private readonly IQueryDriverParameterValidator _validator =
        Substitute.For<IQueryDriverParameterValidator>();

    private readonly IDriverService _service;

    public DriverServiceTests()
    {
        _service = new DriverService(
            validator: _validator,
            driverRepository: _driverRepository,
            driverStandingRepository: _driverStandingRepository,
            raceResultRepository: _raceResultRepository,
            serviceHelper: new Application.Helpers.ServiceHelper());
    }

    [Fact]
    public async void GetDrivers_should_return_paged_result_without_errors()
    {
        // Arrange 
        var parameters = new GetDriversParameter(
            Id: null,
            Name: null,
            Nationality: null,
            SortField: null,
            SortOrder: null,
            PageSize: QueryParameterConstant.DefaultPageSize,
            Page: QueryParameterConstant.DefaultPage);
        List<DriverDto> drivers =
        [
            new DriverDto { Id = Guid.NewGuid().ToString(), Name = "name1" }
        ];

        _driverRepository.GetItemsAsync(parameters).Returns((1, drivers));
        _validator.Validate(parameters).Returns([]);

        // Act
        var result = await _service.GetDrivers(parameters);

        // Assert
        var expectedPagedResult = new PagedResult<DriverDto>(
            CurrentPage: parameters.Page,
            TotalPages: 1,
            PageSize: parameters.PageSize,
            TotalResults: 1,
            Errors: new List<string>(),
            Items: drivers);
        result.ShouldBeEquivalentTo(expectedPagedResult);
    }

    [Fact]
    public async void GetDrivers_should_return_paged_result_with_error_when_validation_fails()
    {
        // Arrange 
        var parameters = new GetDriversParameter(
            Id: InvalidId,
            Name: null,
            Nationality: null,
            SortField: null,
            SortOrder: null,
            PageSize: QueryParameterConstant.DefaultPageSize,
            Page: QueryParameterConstant.DefaultPage);

        List<string> errors = [ValidationMessage.InvalidGuid("Id", InvalidId)];
        _validator.Validate(parameters).Returns(errors);

        // Act
        var result = await _service.GetDrivers(parameters);

        // Assert
        var expectedPagedResult = new PagedResult<DriverDto>(
            CurrentPage: parameters.Page,
            TotalPages: 0,
            PageSize: parameters.PageSize,
            TotalResults: 0,
            Errors: errors,
            Items: new List<DriverDto>());
        result.ShouldBeEquivalentTo(expectedPagedResult);
    }

    [Fact]
    public async void GetDriverStandings_should_return_paged_result_without_errors()
    {
        // Arrange 
        var parameters = new GetDriverStandingsParameter(
            Id: null,
            DriverId: null,
            Year: null,
            SortField: null,
            SortOrder: null,
            PageSize: QueryParameterConstant.DefaultPageSize,
            Page: QueryParameterConstant.DefaultPage);
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

        _driverStandingRepository.GetItemsAsync(parameters).Returns((1, driverStandings));
        _validator.Validate(parameters).Returns([]);

        // Act
        var result = await _service.GetDriverStandings(parameters);

        // Assert
        var expectedPagedResult = new PagedResult<DriverStandingDto>(
            CurrentPage: parameters.Page,
            TotalPages: 1,
            PageSize: parameters.PageSize,
            TotalResults: 1,
            Errors: new List<string>(),
            Items: driverStandings);
        result.ShouldBeEquivalentTo(expectedPagedResult);
    }

    [Fact]
    public async void GetDriverStandings_should_return_paged_result_with_error_when_validation_fails()
    {
        // Arrange 
        var parameters = new GetDriverStandingsParameter(
            Id: InvalidId,
            DriverId: null,
            Year: null,
            SortField: null,
            SortOrder: null,
            PageSize: QueryParameterConstant.DefaultPageSize,
            Page: QueryParameterConstant.DefaultPage);

        List<string> errors = [ValidationMessage.InvalidGuid("Id", InvalidId)];
        _validator.Validate(parameters).Returns(errors);

        // Act
        var result = await _service.GetDriverStandings(parameters);

        // Assert
        var expectedPagedResult = new PagedResult<DriverStandingDto>(
            CurrentPage: parameters.Page,
            TotalPages: 0,
            PageSize: parameters.PageSize,
            TotalResults: 0,
            Errors: errors,
            Items: new List<DriverStandingDto>());
        result.ShouldBeEquivalentTo(expectedPagedResult);
    }

    [Fact]
    public async void GetDriverResults_should_return_paged_result_without_errors()
    {
        // Arrange 
        var parameters = new GetDriverResultsParameter(
            Id: null,
            DriverId: null,
            DriverName: null,
            Year: null,
            SortField: null,
            SortOrder: null,
            PageSize: QueryParameterConstant.DefaultPageSize,
            Page: QueryParameterConstant.DefaultPage);
        var driverResults = base.GetRaceResults();

        _raceResultRepository.GetItemsAsync(parameters).Returns((1, driverResults));
        _validator.Validate(parameters).Returns([]);

        // Act
        var result = await _service.GetDriverResults(parameters);

        // Assert
        var expectedPagedResult = new PagedResult<RaceResultDto>(
            CurrentPage: parameters.Page,
            TotalPages: 1,
            PageSize: parameters.PageSize,
            TotalResults: 1,
            Errors: new List<string>(),
            Items: driverResults);
        result.ShouldBeEquivalentTo(expectedPagedResult);
    }

    [Fact]
    public async void GetDriverResults_should_return_paged_result_with_error_when_validation_fails()
    {
        // Arrange 
        var parameters = new GetDriverResultsParameter(
            Id: InvalidId,
            DriverId: null,
            DriverName: null,
            Year: null,
            SortField: null,
            SortOrder: null,
            PageSize: QueryParameterConstant.DefaultPageSize,
            Page: QueryParameterConstant.DefaultPage);

        List<string> errors = [ValidationMessage.InvalidGuid("Id", InvalidId)];
        _validator.Validate(parameters).Returns(errors);

        // Act
        var result = await _service.GetDriverResults(parameters);

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
