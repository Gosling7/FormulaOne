using FormulaOne.Application.Constants;
using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Parameters;
using FormulaOne.Infrastructure.Repositories;
using FormulaOne.Tests.Shared.Builders;
using Shouldly;

namespace FormulaOne.Tests.Integration.Repositories;

public class RaceResultRepositoryTests : RepositoryTestsBase
{
    private readonly RaceResultRepository _raceResultRepository;

    private const string RaceResultId1 = TestConstant.RaceResultId1;
    private const string TeamId1 = TestConstant.TeamId1;
    private const string TeamName1 = TestConstant.TeamName1;
    private const string CircuitId1 = TestConstant.CircuitId1;
    private const string CircuitName1 = TestConstant.CircuitName1;
    private const string CircuitLocation1 = TestConstant.CircuitLocation1;
    private const string DriverId1 = TestConstant.DriverId1;
    private const string DriverName1 = $"{TestConstant.DriverFirstName1} " +
        $"{TestConstant.DriverLastName1}";

    private const string RaceResultId2 = TestConstant.RaceResultId2;
    private const string DriverName2 = $"{TestConstant.DriverFirstName2} " +
        $"{TestConstant.DriverLastName2}";

    private const int RaceResultYear1 = TestConstant.RaceResultYear1;
    private const int RaceResultYear2 = TestConstant.RaceResultYear2;
    private const string RaceResultYearString = TestConstant.RaceResultYearString1;

    private static readonly DateOnly _raceResultDate1 = new(
        RaceResultYear1, TestConstant.RaceResultMonth1, TestConstant.RaceResultDay1);
    private static readonly DateOnly _raceResultDate2 = new(
        RaceResultYear2, TestConstant.RaceResultMonth2, TestConstant.RaceResultDay2);

    private readonly RaceResultDto ExpectedRaceResult1 = new()
    {
        Id = RaceResultId1,
        Position = TestConstant.TeamStandingPosition1,
        Date = _raceResultDate1,
        CircuitId = CircuitId1,
        CircuitName = CircuitName1,
        DriverId = DriverId1,
        DriverName = DriverName1,
        TeamId = TeamId1,
        TeamName = TeamName1,
        Laps = TestConstant.RaceResultLaps,
        Time = TestConstant.RaceResultTime1,
        Points = TestConstant.RaceResultPoints1
    };
    private readonly RaceResultDto ExpectedRaceResult2 = new()
    {
        Id = RaceResultId2,
        Position = TestConstant.TeamStandingPosition2,
        Date = _raceResultDate2,
        CircuitId = TestConstant.CircuitId2,
        CircuitName = TestConstant.CircuitName2,
        DriverId = TestConstant.DriverId2,
        DriverName = DriverName2,
        TeamId = TestConstant.TeamId2,
        TeamName = TestConstant.TeamName2,
        Laps = TestConstant.RaceResultLaps,
        Time = TestConstant.RaceResultTime2,
        Points = TestConstant.RaceResultPoints2
    };

    public RaceResultRepositoryTests()
    {
        _raceResultRepository = new RaceResultRepository(base._dbContext);
    }

    [Theory]
    [InlineData(nameof(GetTeamResultsParameter.Id), RaceResultId1, 1)]
    [InlineData(nameof(GetTeamResultsParameter.Id), "incorrect id", 0)]

    [InlineData(nameof(GetTeamResultsParameter.TeamId), TeamId1, 1)]
    [InlineData(nameof(GetTeamResultsParameter.TeamId), "incorrect team id", 0)]

    [InlineData(nameof(GetTeamResultsParameter.TeamName), TeamName1, 1)]
    [InlineData(nameof(GetTeamResultsParameter.TeamName), "incorrect team name", 0)]

    [InlineData(nameof(GetTeamResultsParameter.Year), RaceResultYearString, 1)]
    [InlineData(nameof(GetTeamResultsParameter.Year), "1999", 0)]
    public async Task GetItemsAsync_should_return_expected_race_results_based_on_team_filter(
        string filterType, string filterValue, int expectedCount)
    {
        // Arrange
        var builder = new GetTeamResultsParameterBuilder().SetDefaultValues();

        switch (filterType)
        {
            case nameof(GetTeamResultsParameter.Id):
                builder.SetId(filterValue);
                break;
            case nameof(GetTeamResultsParameter.TeamId):
                builder.SetTeamId(filterValue);
                break;
            case nameof(GetTeamResultsParameter.TeamName):
                builder.SetTeamName(filterValue);
                break;
            case nameof(GetTeamResultsParameter.Year):
                builder.SetYear(filterValue);
                break;
        }

        var parameters = builder.Build();

        // Act
        var (resultCount, resultRaceResults) = await _raceResultRepository.GetItemsAsync(
            parameters);

        // Assert
        resultCount.ShouldBe(expectedCount);
        if (expectedCount > 0)
        {
            resultRaceResults.ShouldContain(ExpectedRaceResult1);
        }
        else
        {
            resultRaceResults.ShouldBeEmpty();
        }
    }

    [Theory]
    [InlineData(nameof(GetDriverResultsParameter.Id), RaceResultId1, 1)]
    [InlineData(nameof(GetDriverResultsParameter.Id), "incorrect id", 0)]

    [InlineData(nameof(GetDriverResultsParameter.DriverId), DriverId1, 1)]
    [InlineData(nameof(GetDriverResultsParameter.DriverId), "incorrect driver id", 0)]

    [InlineData(nameof(GetDriverResultsParameter.DriverName), DriverName1, 1)]
    [InlineData(nameof(GetDriverResultsParameter.DriverName), "incorrect driver name", 0)]

    [InlineData(nameof(GetDriverResultsParameter.Year), RaceResultYearString, 1)]
    [InlineData(nameof(GetDriverResultsParameter.Year), "1999", 0)]
    public async Task GetItemsAsync_should_return_expected_race_results_based_on_driver_filter(
        string filterType, string filterValue, int expectedCount)
    {
        // Arrange
        var builder = new GetDriverResultsParameterBuilder().SetDefaultValues();

        switch (filterType)
        {
            case nameof(GetDriverResultsParameter.Id):
                builder.SetId(filterValue);
                break;
            case nameof(GetDriverResultsParameter.DriverId):
                builder.SetDriverId(filterValue);
                break;
            case nameof(GetDriverResultsParameter.DriverName):
                builder.SetDriverName(filterValue);
                break;
            case nameof(GetDriverResultsParameter.Year):
                builder.SetYear(filterValue);
                break;
        }

        var parameters = builder.Build();

        // Act
        var (resultCount, resultRaceResults) = await _raceResultRepository.GetItemsAsync(
            parameters);

        // Assert
        resultCount.ShouldBe(expectedCount);
        if (expectedCount > 0)
        {
            resultRaceResults.ShouldContain(ExpectedRaceResult1);
        }
        else
        {
            resultRaceResults.ShouldBeEmpty();
        }
    }

    [Theory]
    [InlineData(nameof(GetCircuitResultsParameter.Id), RaceResultId1, 1)]
    [InlineData(nameof(GetCircuitResultsParameter.Id), "incorrect id", 0)]

    [InlineData(nameof(GetCircuitResultsParameter.CircuitId), CircuitId1, 1)]
    [InlineData(nameof(GetCircuitResultsParameter.CircuitId), "incorrect circuit id", 0)]

    [InlineData(nameof(GetCircuitResultsParameter.CircuitName), CircuitName1, 1)]
    [InlineData(nameof(GetCircuitResultsParameter.CircuitName), "incorrect circuit name", 0)]

    [InlineData(nameof(GetCircuitResultsParameter.CircuitLocation), CircuitLocation1, 1)]
    [InlineData(nameof(GetCircuitResultsParameter.CircuitLocation), "incorrect circuit location", 0)]

    [InlineData(nameof(GetCircuitResultsParameter.Year), RaceResultYearString, 1)]
    [InlineData(nameof(GetCircuitResultsParameter.Year), "1999", 0)]
    public async Task GetItemsAsync_should_return_expected_race_results_based_on_circuit_filter(
        string filterType, string filterValue, int expectedCount)
    {
        // Arrange
        var builder = new GetCircuitResultsParameterBuilder().SetDefaultValues();

        switch (filterType)
        {
            case nameof(GetCircuitResultsParameter.Id):
                builder.SetId(filterValue);
                break;
            case nameof(GetCircuitResultsParameter.CircuitId):
                builder.SetCircuitId(filterValue);
                break;
            case nameof(GetCircuitResultsParameter.CircuitName):
                builder.SetCircuitName(filterValue);
                break;
            case nameof(GetCircuitResultsParameter.CircuitLocation):
                builder.SetCircuitLocation(filterValue);
                break;
            case nameof(GetCircuitResultsParameter.Year):
                builder.SetYear(filterValue);
                break;
        }

        var parameters = builder.Build();

        // Act
        var (resultCount, resultRaceResults) = await _raceResultRepository.GetItemsAsync(
            parameters);

        // Assert
        resultCount.ShouldBe(expectedCount);
        if (expectedCount > 0)
        {
            resultRaceResults.ShouldContain(ExpectedRaceResult1);
        }
        else
        {
            resultRaceResults.ShouldBeEmpty();
        }
    }

    [Theory]
    [InlineData(RepositoryConstant.DateField, RepositoryConstant.AscendingOrder)]
    [InlineData(RepositoryConstant.DateField, RepositoryConstant.DescendingOrder)]

    [InlineData(RepositoryConstant.PositionField, RepositoryConstant.AscendingOrder)]
    [InlineData(RepositoryConstant.PositionField, RepositoryConstant.DescendingOrder)]

    [InlineData(RepositoryConstant.PointsField, RepositoryConstant.AscendingOrder)]
    [InlineData(RepositoryConstant.PointsField, RepositoryConstant.DescendingOrder)]
    public async Task GetItemsAsync_should_return_race_results_sorted_based_on_filter(
        string sortField, string sortOrder)
    {
        // Arrange
        var parameters = new GetTeamResultsParameterBuilder()
            .SetDefaultValues()
            .SetSortField(sortField)
            .SetSortOrder(sortOrder)
            .Build();

        // Act
        var (_, resultRaceResults) = await _raceResultRepository.GetItemsAsync(parameters);

        // Assert
        IOrderedEnumerable<RaceResultDto> expectedOrder = null!;
        switch (sortField)
        {
            case RepositoryConstant.DateField:
                expectedOrder = sortOrder == RepositoryConstant.DescendingOrder
                    ? resultRaceResults.OrderByDescending(rr => rr.Date)
                    : resultRaceResults.OrderBy(rr => rr.Date);
                break;
            case RepositoryConstant.PositionField:
                expectedOrder = sortOrder == RepositoryConstant.DescendingOrder
                    ? resultRaceResults.OrderByDescending(rr => rr.Position)
                    : resultRaceResults.OrderBy(rr => rr.Position);
                break;
            case RepositoryConstant.PointsField:
                expectedOrder = sortOrder == RepositoryConstant.DescendingOrder
                    ? resultRaceResults.OrderByDescending(rr => rr.Points)
                    : resultRaceResults.OrderBy(rr => rr.Points);
                break;
            default:
                throw new ArgumentException("Invalid sort field");
        }

        resultRaceResults.ShouldBe(expectedOrder, ignoreOrder: false);
    }

    [Fact]
    public async Task GetItemsAsync_should_return_all_two_race_results_present_in_database()
    {
        //Arrange
        var parameters = new GetTeamResultsParameterBuilder()
            .SetDefaultValues()
            .Build();

        var expectedRaceResults = (IReadOnlyCollection<RaceResultDto>)
        [
            ExpectedRaceResult1,
            ExpectedRaceResult2
        ];

        // Act
        var (resultCount, resultRaceResults) = await _raceResultRepository.GetItemsAsync(
            parameters);

        // Assert
        resultCount.ShouldBe(expectedRaceResults.Count);
        resultRaceResults.ShouldContainAll(expectedRaceResults);
    }

    [Fact]
    public async Task GetItemsAsync_should_return_limited_number_of_race_results_per_page()
    {
        // Arrange
        var parameters = new GetTeamResultsParameterBuilder()
            .SetDefaultValues()
            .SetPage(1)
            .SetPageSize(1)
            .Build();

        // Act
        var (resultCount, resultRaceResults) = await _raceResultRepository.GetItemsAsync(
            parameters);

        // Assert
        resultCount.ShouldBeGreaterThan(1);
        resultRaceResults.Count.ShouldBe(1);
    }

    [Fact]
    public async Task GetItemsAsync_should_return_no_race_result_when_page_exceeds_total()
    {
        // Arrange
        var parameters = new GetTeamResultsParameterBuilder()
            .SetDefaultValues()
            .SetPage(999)
            .SetPageSize(1)
            .Build();

        // Act
        var (_, resultRaceResults) = await _raceResultRepository.GetItemsAsync(parameters);

        // Assert
        resultRaceResults.ShouldBeEmpty();
    }
}
