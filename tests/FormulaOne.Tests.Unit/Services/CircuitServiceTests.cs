using FormulaOne.Application.Constants;
using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;
using FormulaOne.Application.Services;
using FormulaOne.Infrastructure.Repositories;
using FormulaOne.Tests.Unit.Builders;
using NSubstitute;
using Shouldly;

namespace FormulaOne.Tests.Unit.Services;

public class CircuitServiceTests : ServiceTestsBase
{
    private readonly ICircuitRepository _circuitRepository = Substitute.For<ICircuitRepository>();

    private readonly IQueryCircuitParameterValidator _validator =
        Substitute.For<IQueryCircuitParameterValidator>();

    private readonly ICircuitService _service;

    public CircuitServiceTests()
    {
        _service = new CircuitService(
            validator: _validator,
            circuitRepository: _circuitRepository,
            raceResultRepository: _raceResultRepository,
            getDriversHelper: new Application.Helpers.ServiceHelper());
    }

    [Fact]
    public async Task GetCircuits_should_return_paged_result_without_errors()
    {
        // Arrange 
        var parameters = new GetCircuitsParameterBuilder()
            .SetDefaultValues()
            .SetId(null)
            .SetName(null)
            .SetLocation(null)
            .SetSortField(null)
            .SetSortOrder(null)
            .SetPage(1)
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

        _circuitRepository.GetItemsAsync(parameters).Returns((1, circuits));
        _validator.Validate(parameters).Returns([]);

        // Act
        var result = await _service.GetCircuits(parameters);

        // Assert
        var expectedPagedResult = new PagedResult<CircuitDto>(
            CurrentPage: parameters.Page,
            TotalPages: 1,
            PageSize: parameters.PageSize,
            TotalResults: 1,
            Errors: new List<string>(),
            Items: circuits);
        result.ShouldBeEquivalentTo(expectedPagedResult);
    }

    [Fact]
    public async Task GetCircuits_should_return_paged_result_with_error_when_validation_fails()
    {
        // Arrange 
        var parameters = new GetCircuitsParameterBuilder()
            .SetDefaultValues()
            .SetId(InvalidId)
            .Build();

        List<string> errors = [ValidationMessage.InvalidGuid("Id", InvalidId)];
        _validator.Validate(parameters).Returns(errors);

        // Act
        var result = await _service.GetCircuits(parameters);

        // Assert
        var expectedPagedResult = new PagedResult<CircuitDto>(
            CurrentPage: parameters.Page,
            TotalPages: 0,
            PageSize: parameters.PageSize,
            TotalResults: 0,
            Errors: errors,
            Items: new List<CircuitDto>());
        result.ShouldBeEquivalentTo(expectedPagedResult);
    }

    [Fact]
    public async Task GetCircuitResults_should_return_paged_result_without_errors()
    {
        // Arrange 
        var parameters = new GetCircuitResultsParameter(
            Id: null,
            CircuitId: null,
            CircuitName: null,
            CircuitLocation: null,
            Year: null,
            SortField: null,
            SortOrder: null,
            PageSize: QueryParameterConstant.DefaultPageSize,
            Page: QueryParameterConstant.DefaultPage);
        var circuitsResults = GetRaceResults();

        _raceResultRepository.GetItemsAsync(parameters).Returns((1, circuitsResults));
        _validator.Validate(parameters).Returns([]);

        // Act
        var result = await _service.GetCircuitResults(parameters);

        // Assert
        var expectedPagedResult = GetExpectedRaceResultsWithoutErrors(
            parameters, circuitsResults);
        result.ShouldBeEquivalentTo(expectedPagedResult);
    }

    [Fact]
    public async Task GetCircuitResults_should_return_paged_result_with_error_when_validation_fails()
    {
        // Arrange 
        var parameters = new GetCircuitResultsParameter(
            Id: InvalidId,
            CircuitId: null,
            CircuitName: null,
            CircuitLocation: null,
            Year: null,
            SortField: null,
            SortOrder: null,
            PageSize: QueryParameterConstant.DefaultPageSize,
            Page: QueryParameterConstant.DefaultPage);

        List<string> errors = [ValidationMessage.InvalidGuid("Id", InvalidId)];
        _validator.Validate(parameters).Returns(errors);

        // Act
        var result = await _service.GetCircuitResults(parameters);

        // Assert
        var expectedPagedResult = GetExpectedRaceResultsWithErrors(parameters, errors);
        result.ShouldBeEquivalentTo(expectedPagedResult);
    }    
}
