using FormulaOne.Application.Constants;
using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Parameters;
using FormulaOne.Infrastructure.Repositories;
using FormulaOne.Tests.Unit.Builders;
using Shouldly;

namespace FormulaOne.Tests.Integration.Repositories;

public class CircuitRepositoryTests : IClassFixture<DbContainerFixture>
{
    private readonly CircuitRepository _circuitRepository = null!;

    private const string CircuitId1 = TestConstant.CircuitId1;
    private const string CircuitName1 = TestConstant.CircuitName1;
    private const string CircuitLocation1 = TestConstant.CircuitLocation1;

    private const string CircuitId2 = TestConstant.CircuitId2;
    private const string CircuitName2 = TestConstant.CircuitName2;
    private const string CircuitLocation2 = TestConstant.CircuitLocation2;

    public CircuitRepositoryTests(DbContainerFixture dbFixture)
    {
        _circuitRepository = dbFixture.CircuitRepository;
    }

    [Theory]
    // Id filter
    [InlineData(nameof(GetCircuitsParameter.Id), CircuitId1, 1)]
    [InlineData(nameof(GetCircuitsParameter.Id), "incorrect id", 0)]
    
    // Name filter
    [InlineData(nameof(GetCircuitsParameter.Name), CircuitName1, 1)]
    [InlineData(nameof(GetCircuitsParameter.Name), "incorrect name", 0)]
    
    // Location filter
    [InlineData(nameof(GetCircuitsParameter.Location), CircuitLocation1, 1)]
    [InlineData(nameof(GetCircuitsParameter.Location), "incorrect location", 0)]
    public async Task GetItemsAsync_should_return_expected_circuits_based_on_filter(
        string filterType, string filterValue, int expectedCount)
    {
        // Arrange
        var builder = new GetCircuitsParameterBuilder().SetDefaultValues();

        switch (filterType)
        {
            case nameof(GetCircuitsParameter.Id):
                builder.SetId(filterValue);
                break;
            case nameof(GetCircuitsParameter.Name):
                builder.SetName(filterValue);
                break;
            case nameof(GetCircuitsParameter.Location):
                builder.SetLocation(filterValue);
                break;
        }

        var parameters = builder.Build();

        // Act
        var (resultCount, resultCircuits) = await _circuitRepository.GetItemsAsync(parameters);

        // Assert
        resultCount.ShouldBe(expectedCount);
        if (expectedCount > 0)
        {
            resultCircuits.ShouldContain(new CircuitDto
            {
                Id = CircuitId1,
                Name = CircuitName1,
                Location = CircuitLocation1
            });
        }
        else
        {
            resultCircuits.ShouldBeEmpty();
        }
    }

    [Theory]
    [InlineData(RepositoryConstant.NameField, RepositoryConstant.AscendingOrder)]
    [InlineData(RepositoryConstant.NameField, RepositoryConstant.DescendingOrder)]
    [InlineData(RepositoryConstant.LocationField, RepositoryConstant.AscendingOrder)]
    [InlineData(RepositoryConstant.LocationField, RepositoryConstant.DescendingOrder)]
    public async Task GetItemsAsync_should_return_circuits_sorted_based_on_filter(
        string sortField, string sortOrder)
    {
        // Arrange
        var parameters = new GetCircuitsParameterBuilder()
            .SetDefaultValues()
            .SetSortField(sortField)
            .SetSortOrder(sortOrder)
            .Build();

        // Act
        var (_, resultCircuits) = await _circuitRepository.GetItemsAsync(parameters);

        // Assert
        IOrderedEnumerable<CircuitDto> expectedOrder = null!;
        switch (sortField)
        {
            case RepositoryConstant.NameField:
                expectedOrder = sortOrder == RepositoryConstant.DescendingOrder
                    ? resultCircuits.OrderByDescending(c => c.Name)
                    : resultCircuits.OrderBy(c => c.Name);
                break;
            case RepositoryConstant.LocationField:
                expectedOrder = sortOrder == RepositoryConstant.DescendingOrder
                    ? resultCircuits.OrderByDescending(c => c.Location)
                    : resultCircuits.OrderBy(c => c.Location);
                break;
            default:
                throw new ArgumentException("Invalid sort field");
        }

        resultCircuits.ShouldBe(expectedOrder, ignoreOrder: false);
    }

    [Fact]
    public async Task GetItemsAsync_should_return_all_two_circuits_present_in_database()
    {
        //Arrange
        var parameters = new GetCircuitsParameterBuilder()
            .SetDefaultValues()
            .Build();

        var expectedCircuits = (IReadOnlyCollection<CircuitDto>)new List<CircuitDto>
        {
            new()
            {
                Id = CircuitId1,
                Name = CircuitName1,
                Location = CircuitLocation1
            },
            new()
            {
                Id = CircuitId2,
                Name = CircuitName2,
                Location = CircuitLocation2
            }
        };

        // Act
        var (resultCount, resultCircuits) = await _circuitRepository.GetItemsAsync(parameters);

        // Assert
        resultCount.ShouldBe(expectedCircuits.Count);
        resultCircuits.ShouldContainAll(expectedCircuits);
    }

    [Fact]
    public async Task GetItemsAsync_should_return_limited_number_of_circuits_per_page()
    {
        // Arrange
        var parameters = new GetCircuitsParameterBuilder()
            .SetDefaultValues()
            .SetPage(1)
            .SetPageSize(1)
            .Build();

        // Act
        var (resultCount, resultCircuits) = await _circuitRepository.GetItemsAsync(parameters);
         
        // Assert
        resultCount.ShouldBeGreaterThan(1);
        resultCircuits.Count.ShouldBe(1);
    }

    [Fact]
    public async Task GetItemsAsync_should_return_no_circuits_when_page_exceeds_total()
    {
        // Arrange
        var parameters = new GetCircuitsParameterBuilder()
            .SetDefaultValues()
            .SetPage(999) 
            .SetPageSize(1)
            .Build();

        // Act
        var (_, resultCircuits) = await _circuitRepository.GetItemsAsync(parameters);

        // Assert
        resultCircuits.ShouldBeEmpty();
    }
}
