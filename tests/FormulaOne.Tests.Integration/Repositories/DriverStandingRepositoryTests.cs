using FormulaOne.Application.Constants;
using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Parameters;
using FormulaOne.Infrastructure.Repositories;
using FormulaOne.Tests.Unit.Builders;
using Shouldly;

namespace FormulaOne.Tests.Integration.Repositories;

public class DriverStandingRepositoryTests : RepositoryTestsBase
{
    private readonly DriverStandingRepository _driverStandingRepository;

    private const string DriverStandingYearString1 = TestConstant.StandingYearString1;

    private const string DriverStandingId1 = TestConstant.DriverStandingId1;
    private const string DriverId1 = TestConstant.DriverId1;

    private readonly DriverStandingDto ExpectedDriverStanding1 = new()
    {
        Id = DriverStandingId1,
        Position = TestConstant.DriverStandingPosition1,
        DriverId = TestConstant.DriverId1,
        DriverName = $"{TestConstant.DriverFirstName1} {TestConstant.DriverLastName1}",
        Nationality = TestConstant.DriverNationality1,
        TeamId = TestConstant.TeamId1,
        TeamName = TestConstant.TeamName1,
        Points = TestConstant.DriverStandingPoints1,
        Year = TestConstant.StandingYear1
    };
    private readonly DriverStandingDto ExpectedDriverStanding2 = new()
    {
        Id = TestConstant.DriverStandingId2,
        Position = TestConstant.DriverStandingPosition2,
        DriverId = TestConstant.DriverId2,
        DriverName = $"{TestConstant.DriverFirstName2} {TestConstant.DriverLastName2}",
        Nationality = TestConstant.DriverNationality2,
        TeamId = TestConstant.TeamId2,
        TeamName = TestConstant.TeamName2,
        Points = TestConstant.DriverStandingPoints2,
        Year = TestConstant.StandingYear2
    };

    public DriverStandingRepositoryTests()
    {
        _driverStandingRepository = new DriverStandingRepository(base._dbContext);
    }

    [Theory]
    [InlineData(nameof(GetDriverStandingsParameter.Id), DriverStandingId1, 1)]
    [InlineData(nameof(GetDriverStandingsParameter.Id), "incorrect id", 0)]

    [InlineData(nameof(GetDriverStandingsParameter.DriverId), DriverId1, 1)]
    [InlineData(nameof(GetDriverStandingsParameter.DriverId), "incorrect driver id", 0)]

    [InlineData(nameof(GetDriverStandingsParameter.Year), DriverStandingYearString1, 1)]
    [InlineData(nameof(GetDriverStandingsParameter.Year), "1999", 0)]
    public async Task GetItemsAsync_should_return_expected_driver_standings_based_on_filter(
        string filterType, string filterValue, int expectedCount)
    {
        // Arrange
        var builder = new GetDriverStandingsParameterBuilder().SetDefaultValues();

        switch (filterType)
        {
            case nameof(GetDriversParameter.Id):
                builder.SetId(filterValue);
                break;
            case nameof(GetDriverStandingsParameter.DriverId):
                builder.SetDriverId(filterValue);
                break;
            case nameof(GetDriverStandingsParameter.Year):
                builder.SetYear(filterValue);
                break;
        }

        var parameters = builder.Build();

        // Act
        var (resultCount, resultDriverStandings) = await _driverStandingRepository.GetItemsAsync(
            parameters);

        // Assert
        resultCount.ShouldBe(expectedCount);
        if (expectedCount > 0)
        {
            resultDriverStandings.ShouldContain(ExpectedDriverStanding1);
        }
        else
        {
            resultDriverStandings.ShouldBeEmpty();
        }
    }

    [Theory]
    [InlineData(RepositoryConstant.YearField, RepositoryConstant.AscendingOrder)]
    [InlineData(RepositoryConstant.YearField, RepositoryConstant.DescendingOrder)]

    [InlineData(RepositoryConstant.PositionField, RepositoryConstant.AscendingOrder)]
    [InlineData(RepositoryConstant.PositionField, RepositoryConstant.DescendingOrder)]

    [InlineData(RepositoryConstant.PointsField, RepositoryConstant.AscendingOrder)]
    [InlineData(RepositoryConstant.PointsField, RepositoryConstant.DescendingOrder)]
    public async Task GetItemsAsync_should_return_driver_standings_sorted_based_on_filter(
        string sortField, string sortOrder)
    {
        // Arrange
        var parameters = new GetDriverStandingsParameterBuilder()
            .SetDefaultValues()
            .SetSortField(sortField)
            .SetSortOrder(sortOrder)
            .Build();

        // Act
        var (_, resultDriverStandings) = await _driverStandingRepository.GetItemsAsync(parameters);

        // Assert
        IOrderedEnumerable<DriverStandingDto> expectedOrder = null!;
        switch (sortField)
        {
            case RepositoryConstant.YearField:
                expectedOrder = sortOrder == RepositoryConstant.DescendingOrder
                    ? resultDriverStandings.OrderByDescending(ds => ds.Year)
                    : resultDriverStandings.OrderBy(d => d.Year);
                break;
            case RepositoryConstant.PositionField:
                expectedOrder = sortOrder == RepositoryConstant.DescendingOrder
                    ? resultDriverStandings.OrderByDescending(d => d.Position)
                    : resultDriverStandings.OrderBy(d => d.Position);
                break;
            case RepositoryConstant.PointsField:
                expectedOrder = sortOrder == RepositoryConstant.DescendingOrder
                    ? resultDriverStandings.OrderByDescending(d => d.Points)
                    : resultDriverStandings.OrderBy(d => d.Points);
                break;
            default:
                throw new ArgumentException("Invalid sort field");
        }

        resultDriverStandings.ShouldBe(expectedOrder, ignoreOrder: false);
    }

    [Fact]
    public async Task GetItemsAsync_should_return_all_two_driver_standings_present_in_database()
    {
        //Arrange
        var parameters = new GetDriverStandingsParameterBuilder()
            .SetDefaultValues()
            .Build();

        var expectedDriverStandings = (IReadOnlyCollection<DriverStandingDto>)
        [
            ExpectedDriverStanding1,
            ExpectedDriverStanding2
        ];

        // Act
        var (resultCount, resultDriverStandings) = await _driverStandingRepository.GetItemsAsync(
            parameters);

        // Assert
        resultCount.ShouldBe(expectedDriverStandings.Count);
        resultDriverStandings.ShouldContainAll(expectedDriverStandings);
    }

    [Fact]
    public async Task GetItemsAsync_should_return_limited_number_of_driver_standings_per_page()
    {
        // Arrange
        var parameters = new GetDriverStandingsParameterBuilder()
            .SetDefaultValues()
            .SetPage(1)
            .SetPageSize(1)
            .Build();

        // Act
        var (resultCount, resultDriverStandings) = await _driverStandingRepository.GetItemsAsync(
            parameters);

        // Assert
        resultCount.ShouldBeGreaterThan(1);
        resultDriverStandings.Count.ShouldBe(1);
    }

    [Fact]
    public async Task GetItemsAsync_should_return_no_driver_standing_when_page_exceeds_total()
    {
        // Arrange
        var parameters = new GetDriverStandingsParameterBuilder()
            .SetDefaultValues()
            .SetPage(999)
            .SetPageSize(1)
            .Build();

        // Act
        var (_, resultDriverStandings) = await _driverStandingRepository.GetItemsAsync(parameters);

        // Assert
        resultDriverStandings.ShouldBeEmpty();
    }
}
