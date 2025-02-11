using FormulaOne.Application.Constants;
using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;
using FormulaOne.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Infrastructure.Repositories;

public class TeamStandingRepository : ITeamStandingRepository
{
    private readonly FormulaOneDbContext _context;

    public TeamStandingRepository(FormulaOneDbContext context)
    {
        _context = context;
    }

    public async Task<(int, IReadOnlyCollection<TeamStandingDto>)> GetItemsAsync(
        GetTeamStandingsParameter parameters)
    {
        IQueryable<TeamStanding> query = _context.TeamStandings;
        query = BuildQueryFilter(parameters, query);

        var queryTeamStandingCount = await query.CountAsync();

        query = query
            .Skip((parameters.Page - 1) * parameters.PageSize)
            .Take(parameters.PageSize);

        var teamStandings = await query
            .Include(ts => ts.Team)
            .Select(ts => new TeamStandingDto
            {
                Id = ts.Id.ToString(),
                Year = ts.Year,
                Position = ts.Position,
                TeamId = ts.Team.Id.ToString(),
                TeamName = ts.Team.Name,
                Points = ts.Points
            })
            .ToListAsync();

        return (queryTeamStandingCount, teamStandings);
    }

    private static IQueryable<TeamStanding> BuildQueryFilter(GetTeamStandingsParameter parameters, 
        IQueryable<TeamStanding> query)
    {
        query = ApplyFilters(parameters, query);
        query = ApplySorting(parameters, query);

        return query;        
    }

    private static IQueryable<TeamStanding> ApplyFilters(GetTeamStandingsParameter parameters,
            IQueryable<TeamStanding> query)
    {
        if (!string.IsNullOrWhiteSpace(parameters.Id))
        {
            query = query.Where(ts => parameters.Id.Contains(ts.Id.ToString()));
        }

        if (!string.IsNullOrWhiteSpace(parameters.TeamId))
        {
            query = query.Where(ts => parameters.TeamId.Contains(ts.TeamId.ToString()));
        }

        if (!string.IsNullOrWhiteSpace(parameters.TeamName))
        {
            query = query.Where(ts => ts.Team.Name.Contains(parameters.TeamName));
        }

        if (parameters.Year is not null)
        {
            var yearParts = parameters.Year.Split("-");
            var yearStart = int.Parse(yearParts[0]);
            if (yearParts.Length == 2)
            {
                var yearEnd = int.Parse(yearParts[1]);
                query = query.Where(ts => ts.Year >= yearStart && ts.Year <= yearEnd);
            }
            else
            {
                query = query.Where(ts => ts.Year == yearStart);
            }
        }

        return query;
    }

    private static IQueryable<TeamStanding> ApplySorting(GetTeamStandingsParameter parameters,
        IQueryable<TeamStanding> query)
    {
        if (!string.IsNullOrWhiteSpace(parameters.SortField))
        {
            switch (parameters.SortField)
            {
                case RepositoryConstant.YearField:
                    query = parameters.SortOrder == RepositoryConstant.DescendingOrder
                        ? query.OrderByDescending(ts => ts.Year).ThenBy(ts => ts.Position)
                        : query.OrderBy(ts => ts.Year).ThenBy(ts => ts.Position);
                    break;
                case RepositoryConstant.PositionField:
                    query = parameters.SortOrder == RepositoryConstant.DescendingOrder
                        ? query.OrderByDescending(t => t.Position)
                        : query.OrderBy(ts => ts.Position);
                    break;
                case RepositoryConstant.PointsField:
                    query = parameters.SortOrder == RepositoryConstant.DescendingOrder
                        ? query.OrderByDescending(t => t.Points)
                        : query.OrderBy(ts => ts.Points);
                    break;
                default:
                    break;
            }
        }

        return query;
    }
}
