using FormulaOne.Application.Constants;
using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Parameters;
using FormulaOne.Infrastructure.Repositories;
using FormulaOne.Tests.Unit.Builders;
using Shouldly;

namespace FormulaOne.Tests.Integration.Repositories;

public class TeamStandingRepositoryTests : RepositoryTestsBase
{
    private readonly TeamStandingRepository _teamStandingRepository;

    private const string TeamStandingYearString1 = TestConstant.StandingYearString1;

    private const string TeamStandingId1 = TestConstant.TeamStandingId1;
    private const string TeamId1 = TestConstant.TeamId1;
    private const string TeamName1 = TestConstant.TeamName1;

    private readonly TeamStandingDto ExpectedTeamStanding1 = new()
    {
        Id = TeamStandingId1,
        Position = TestConstant.TeamStandingPosition1,
        TeamId = TestConstant.TeamId1,
        TeamName = TeamName1,
        Points = TestConstant.TeamStandingPoints1,
        Year = TestConstant.StandingYear1
    };
    private readonly TeamStandingDto ExpectedTeamStanding2 = new()
    {
        Id = TestConstant.TeamStandingId2,
        Position = TestConstant.TeamStandingPosition2,
        TeamId = TestConstant.TeamId2,
        TeamName = TestConstant.TeamName2,
        Points = TestConstant.TeamStandingPoints2,
        Year = TestConstant.StandingYear2
    };

    public TeamStandingRepositoryTests()
    {
        _teamStandingRepository = new TeamStandingRepository(base._dbContext);
    }

    [Theory]
    [InlineData(nameof(GetTeamStandingsParameter.Id), TeamStandingId1, 1)]
    [InlineData(nameof(GetTeamStandingsParameter.Id), "incorrect id", 0)]

    [InlineData(nameof(GetTeamStandingsParameter.TeamId), TeamId1, 1)]
    [InlineData(nameof(GetTeamStandingsParameter.TeamId), "incorrect team id", 0)]

    [InlineData(nameof(GetTeamStandingsParameter.TeamName), TeamName1, 1)]
    [InlineData(nameof(GetTeamStandingsParameter.TeamName), "incorrect team name", 0)]

    [InlineData(nameof(GetTeamStandingsParameter.Year), TeamStandingYearString1, 1)]
    [InlineData(nameof(GetTeamStandingsParameter.Year), "1999", 0)]
    public async Task GetItemsAsync_should_return_expected_team_standings_based_on_filter(
        string filterType, string filterValue, int expectedCount)
    {
        // Arrange
        var builder = new GetTeamStandingsParameterBuilder().SetDefaultValues();

        switch (filterType)
        {
            case nameof(GetTeamStandingsParameter.Id):
                builder.SetId(filterValue);
                break;
            case nameof(GetTeamStandingsParameter.TeamId):
                builder.SetTeamId(filterValue);
                break;
            case nameof(GetTeamStandingsParameter.TeamName):
                builder.SetTeamName(filterValue);
                break;
            case nameof(GetTeamStandingsParameter.Year):
                builder.SetYear(filterValue);
                break;
        }

        var parameters = builder.Build();

        // Act
        var (resultCount, resultTeamStandings) = await _teamStandingRepository.GetItemsAsync(
            parameters);

        // Assert
        resultCount.ShouldBe(expectedCount);
        if (expectedCount > 0)
        {
            resultTeamStandings.ShouldContain(ExpectedTeamStanding1);
        }
        else
        {
            resultTeamStandings.ShouldBeEmpty();
        }
    }

    [Theory]
    [InlineData(RepositoryConstant.YearField, RepositoryConstant.AscendingOrder)]
    [InlineData(RepositoryConstant.YearField, RepositoryConstant.DescendingOrder)]

    [InlineData(RepositoryConstant.PositionField, RepositoryConstant.AscendingOrder)]
    [InlineData(RepositoryConstant.PositionField, RepositoryConstant.DescendingOrder)]

    [InlineData(RepositoryConstant.PointsField, RepositoryConstant.AscendingOrder)]
    [InlineData(RepositoryConstant.PointsField, RepositoryConstant.DescendingOrder)]
    public async Task GetItemsAsync_should_return_team_standings_sorted_based_on_filter(
        string sortField, string sortOrder)
    {
        // Arrange
        var parameters = new GetTeamStandingsParameterBuilder()
            .SetDefaultValues()
            .SetSortField(sortField)
            .SetSortOrder(sortOrder)
            .Build();

        // Act
        var (_, resultTeamStandings) = await _teamStandingRepository.GetItemsAsync(parameters);

        // Assert
        IOrderedEnumerable<TeamStandingDto> expectedOrder = null!;
        switch (sortField)
        {
            case RepositoryConstant.YearField:
                expectedOrder = sortOrder == RepositoryConstant.DescendingOrder
                    ? resultTeamStandings.OrderByDescending(ts => ts.Year)
                    : resultTeamStandings.OrderBy(ts => ts.Year);
                break;
            case RepositoryConstant.PositionField:
                expectedOrder = sortOrder == RepositoryConstant.DescendingOrder
                    ? resultTeamStandings.OrderByDescending(ts => ts.Position)
                    : resultTeamStandings.OrderBy(ts => ts.Position);
                break;
            case RepositoryConstant.PointsField:
                expectedOrder = sortOrder == RepositoryConstant.DescendingOrder
                    ? resultTeamStandings.OrderByDescending(ts => ts.Points)
                    : resultTeamStandings.OrderBy(ts => ts.Points);
                break;
            default:
                throw new ArgumentException("Invalid sort field");
        }

        resultTeamStandings.ShouldBe(expectedOrder, ignoreOrder: false);
    }

    [Fact]
    public async Task GetItemsAsync_should_return_all_two_team_standings_present_in_database()
    {
        //Arrange
        var parameters = new GetTeamStandingsParameterBuilder()
            .SetDefaultValues()
            .Build();

        var expectedDriverStandings = (IReadOnlyCollection<TeamStandingDto>)
        [
            ExpectedTeamStanding1,
            ExpectedTeamStanding2
        ];

        // Act
        var (resultCount, resultTeamStandings) = await _teamStandingRepository.GetItemsAsync(
            parameters);

        // Assert
        resultCount.ShouldBe(expectedDriverStandings.Count);
        resultTeamStandings.ShouldContainAll(expectedDriverStandings);
    }

    [Fact]
    public async Task GetItemsAsync_should_return_limited_number_of_team_standings_per_page()
    {
        // Arrange
        var parameters = new GetTeamStandingsParameterBuilder()
            .SetDefaultValues()
            .SetPage(1)
            .SetPageSize(1)
            .Build();

        // Act
        var (resultCount, resultTeamStandings) = await _teamStandingRepository.GetItemsAsync(
            parameters);

        // Assert
        resultCount.ShouldBeGreaterThan(1);
        resultTeamStandings.Count.ShouldBe(1);
    }

    [Fact]
    public async Task GetItemsAsync_should_return_no_team_standing_when_page_exceeds_total()
    {
        // Arrange
        var parameters = new GetTeamStandingsParameterBuilder()
            .SetDefaultValues()
            .SetPage(999)
            .SetPageSize(1)
            .Build();

        // Act
        var (_, resultTeamStandings) = await _teamStandingRepository.GetItemsAsync(parameters);

        // Assert
        resultTeamStandings.ShouldBeEmpty();
    }
}
