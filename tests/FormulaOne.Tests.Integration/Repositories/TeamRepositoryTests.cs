using FormulaOne.Application.Constants;
using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Parameters;
using FormulaOne.Infrastructure.Repositories;
using FormulaOne.Tests.Unit.Builders;
using Shouldly;

namespace FormulaOne.Tests.Integration.Repositories;

public class TeamRepositoryTests : RepositoryTestsBase
{
    private readonly TeamRepository _teamRepository;

    private const string TeamId1 = TestConstant.TeamId1;
    private const string TeamName1 = TestConstant.TeamName1;

    private const string TeamId2 = TestConstant.TeamId2;
    private const string TeamName2 = TestConstant.TeamName2;

    public TeamRepositoryTests()
    {
        _teamRepository = new TeamRepository(base._dbContext);
    }

    [Theory]
    [InlineData(nameof(GetTeamsParameter.Id), TeamId1, 1)]
    [InlineData(nameof(GetTeamsParameter.Id), "incorrect id", 0)]

    [InlineData(nameof(GetTeamsParameter.Name), TeamName1, 1)]
    [InlineData(nameof(GetTeamsParameter.Name), "incorrect name", 0)]
    public async Task GetItemsAsync_should_return_expected_teams_based_on_filter(
        string filterType, string filterValue, int expectedCount)
    {
        // Arrange
        var builder = new GetTeamsParameterBuilder().SetDefaultValues();

        switch (filterType)
        {
            case nameof(GetTeamsParameter.Id):
                builder.SetId(filterValue);
                break;
            case nameof(GetTeamsParameter.Name):
                builder.SetName(filterValue);
                break;
        }

        var parameters = builder.Build();

        // Act
        var (resultCount, resultTeams) = await _teamRepository.GetItemsAsync(parameters);

        // Assert
        resultCount.ShouldBe(expectedCount);
        if (expectedCount > 0)
        {
            resultTeams.ShouldContain(new TeamDto
            {
                Id = TeamId1,
                Name = TeamName1,
            });
        }
        else
        {
            resultTeams.ShouldBeEmpty();
        }
    }

    [Theory]
    [InlineData(RepositoryConstant.NameField, RepositoryConstant.AscendingOrder)]
    [InlineData(RepositoryConstant.NameField, RepositoryConstant.DescendingOrder)]
    public async Task GetItemsAsync_should_return_teams_sorted_based_on_filter(
        string sortField, string sortOrder)
    {
        // Arrange
        var parameters = new GetTeamsParameterBuilder()
            .SetDefaultValues()
            .SetSortField(sortField)
            .SetSortOrder(sortOrder)
            .Build();

        // Act
        var (_, resultTeams) = await _teamRepository.GetItemsAsync(parameters);

        // Assert
        IOrderedEnumerable<TeamDto> expectedOrder = null!;
        switch (sortField)
        {
            case RepositoryConstant.NameField:
                expectedOrder = sortOrder == RepositoryConstant.DescendingOrder
                    ? resultTeams.OrderByDescending(c => c.Name)
                    : resultTeams.OrderBy(c => c.Name);
                break;
            default:
                throw new ArgumentException("Invalid sort field");
        }

        resultTeams.ShouldBe(expectedOrder, ignoreOrder: false);
    }

    [Fact]
    public async Task GetItemsAsync_should_return_all_two_teams_present_in_database()
    {
        //Arrange
        var parameters = new GetTeamsParameterBuilder()
            .SetDefaultValues()
            .Build();

        var expectedTeams = (IReadOnlyCollection<TeamDto>)new List<TeamDto>
        {
            new()
            {
                Id = TeamId1,
                Name = TeamName1,
            },
            new()
            {
                Id = TeamId2,
                Name = TeamName2,
            }
        };

        // Act
        var (resultCount, resultTeams) = await _teamRepository.GetItemsAsync(parameters);

        // Assert
        resultCount.ShouldBe(expectedTeams.Count);
        resultTeams.ShouldContainAll(expectedTeams);
    }

    [Fact]
    public async Task GetItemsAsync_should_return_limited_number_of_teams_per_page()
    {
        // Arrange
        var parameters = new GetTeamsParameterBuilder()
            .SetDefaultValues()
            .SetPage(1)
            .SetPageSize(1)
            .Build();

        // Act
        var (resultCount, resultTeams) = await _teamRepository.GetItemsAsync(parameters);

        // Assert
        resultCount.ShouldBeGreaterThan(1);
        resultTeams.Count.ShouldBe(1);
    }

    [Fact]
    public async Task GetItemsAsync_should_return_no_teams_when_page_exceeds_total()
    {
        // Arrange
        var parameters = new GetTeamsParameterBuilder()
            .SetDefaultValues()
            .SetPage(999)
            .SetPageSize(1)
            .Build();

        // Act
        var (_, resultTeams) = await _teamRepository.GetItemsAsync(parameters);

        // Assert
        resultTeams.ShouldBeEmpty();
    }
}
