using FormulaOne.Application.Constants;
using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Parameters;
using FormulaOne.Infrastructure.Repositories;
using FormulaOne.Tests.Unit.Builders;
using Shouldly;

namespace FormulaOne.Tests.Integration.Repositories;

public class DriverRepositoryTests : IClassFixture<DbContainerFixture>
{
    private readonly DriverRepository _driverRepository = null!;

    private const string DriverId1 = TestConstant.DriverId1;
    private const string DriverName1 = $"{TestConstant.DriverFirstName1} " +
        $"{TestConstant.DriverLastName1}";
    private const string DriverNationality1 = TestConstant.DriverNationality1;

    private const string DriverId2 = TestConstant.DriverId2;
    private const string DriverName2 = $"{TestConstant.DriverFirstName2} " +
        $"{TestConstant.DriverLastName2}";
    private const string DriverNationality2 = TestConstant.DriverNationality2;

    public DriverRepositoryTests(DbContainerFixture dbFixture)
    {
        _driverRepository = dbFixture.DriverRepository;
    }

    [Theory]
    // Id filter
    [InlineData(nameof(GetDriversParameter.Id), DriverId1, 1)]
    [InlineData(nameof(GetDriversParameter.Id), "incorrect id", 0)]

    // Name filter
    [InlineData(nameof(GetDriversParameter.Name), TestConstant.DriverFirstName1, 1)]
    [InlineData(nameof(GetDriversParameter.Name), "incorrect name", 0)]

    // Nationality filter
    [InlineData(nameof(GetDriversParameter.Nationality), DriverNationality1, 1)]
    [InlineData(nameof(GetDriversParameter.Nationality), "incorrect nationality", 0)]
    public async Task GetItemsAsync_should_return_expected_drivers_based_on_filter(
        string filterType, string filterValue, int expectedCount)
    {
        // Arrange
        var builder = new GetDriversParameterBuilder().SetDefaultValues();

        switch (filterType)
        {
            case nameof(GetDriversParameter.Id):
                builder.SetId(filterValue);
                break;
            case nameof(GetDriversParameter.Name):
                builder.SetName(filterValue);
                break;
            case nameof(GetDriversParameter.Nationality):
                builder.SetNationality(filterValue);
                break;
        }

        var parameters = builder.Build();

        // Act
        var (resultCount, resultDrivers) = await _driverRepository.GetItemsAsync(parameters);

        // Assert
        resultCount.ShouldBe(expectedCount);
        if (expectedCount > 0)
        {
            resultDrivers.ShouldContain(new DriverDto
            {
                Id = DriverId1,
                Name = DriverName1,
                Nationality = DriverNationality1
            });
        }
        else
        {
            resultDrivers.ShouldBeEmpty();
        }
    }

    [Theory]
    [InlineData(RepositoryConstant.FirstNameField, RepositoryConstant.AscendingOrder)]
    [InlineData(RepositoryConstant.FirstNameField, RepositoryConstant.DescendingOrder)]
    [InlineData(RepositoryConstant.LastNameField, RepositoryConstant.AscendingOrder)]
    [InlineData(RepositoryConstant.LastNameField, RepositoryConstant.DescendingOrder)]
    [InlineData(RepositoryConstant.NationalityField, RepositoryConstant.AscendingOrder)]
    [InlineData(RepositoryConstant.NationalityField, RepositoryConstant.DescendingOrder)]
    public async Task GetItemsAsync_should_return_drivers_sorted_based_on_filter(
        string sortField, string sortOrder)
    {
        // Arrange
        var parameters = new GetDriversParameterBuilder()
            .SetDefaultValues()
            .SetSortField(sortField)
            .SetSortOrder(sortOrder)
            .Build();

        // Act
        var (_, resultDrivers) = await _driverRepository.GetItemsAsync(parameters);

        // Assert
        IOrderedEnumerable<DriverDto> expectedOrder = null!;
        switch (sortField)
        {
            case RepositoryConstant.FirstNameField:
                expectedOrder = sortOrder == RepositoryConstant.DescendingOrder
                    ? resultDrivers.OrderByDescending(d => d.Name)
                    : resultDrivers.OrderBy(d => d.Name);
                break;
            case RepositoryConstant.LastNameField:
                expectedOrder = sortOrder == RepositoryConstant.DescendingOrder
                    ? resultDrivers.OrderByDescending(d => d.Name.Split(" ").Last())
                    : resultDrivers.OrderBy(d => d.Name.Split(" ").Last());
                break;
            case RepositoryConstant.NationalityField:
                expectedOrder = sortOrder == RepositoryConstant.DescendingOrder
                    ? resultDrivers.OrderByDescending(d => d.Nationality)
                    : resultDrivers.OrderBy(d => d.Nationality);
                break;
            default:
                throw new ArgumentException("Invalid sort field");
        }

        resultDrivers.ShouldBe(expectedOrder, ignoreOrder: false);
    }

    [Fact]
    public async Task GetItemsAsync_should_return_all_two_drivers_present_in_database()
    {
        //Arrange
        var parameters = new GetDriversParameterBuilder()
            .SetDefaultValues()
            .Build();

        var expectedDrivers = (IReadOnlyCollection<DriverDto>)new List<DriverDto>
        {
            new()
            {
                Id = DriverId1,
                Name = DriverName1,
                Nationality = DriverNationality1
            },
            new()
            {
                Id = DriverId2,
                Name = DriverName2,
                Nationality = DriverNationality2
            }
        };

        // Act
        var (resultCount, resultDrivers) = await _driverRepository.GetItemsAsync(parameters);

        // Assert
        resultCount.ShouldBe(expectedDrivers.Count);
        resultDrivers.ShouldContainAll(expectedDrivers);
    }

    [Fact]
    public async Task GetItemsAsync_should_return_limited_number_of_drivers_per_page()
    {
        // Arrange
        var parameters = new GetDriversParameterBuilder()
            .SetDefaultValues()
            .SetPage(1)
            .SetPageSize(1)
            .Build();

        // Act
        var (resultCount, resultDrivers) = await _driverRepository.GetItemsAsync(parameters);

        // Assert
        resultCount.ShouldBeGreaterThan(1);
        resultDrivers.Count.ShouldBe(1);
    }

    [Fact]
    public async Task GetItemsAsync_should_return_no_drivers_when_page_exceeds_total()
    {
        // Arrange
        var parameters = new GetDriversParameterBuilder()
            .SetDefaultValues()
            .SetPage(999)
            .SetPageSize(1)
            .Build();

        // Act
        var (_, resultDrivers) = await _driverRepository.GetItemsAsync(parameters);

        // Assert
        resultDrivers.ShouldBeEmpty();
    }
}

