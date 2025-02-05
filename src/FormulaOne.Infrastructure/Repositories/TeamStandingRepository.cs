using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public async Task<(int, IEnumerable<TeamStandingDto>)> GetTeamStandings(GetTeamStandingsParameter parameters)
    {
        IQueryable<TeamStanding> query = _context.TeamStandings;
        query = BuildQueryFilter(parameters, query);

        var queryTeamStandingCount = await query.CountAsync();

        query = query
            .Skip((parameters.Page - 1) * parameters.PageSize)
            .Take(parameters.PageSize);

        var teamStandings = await query
            .Include(ts => ts.Team)
            .Select(ts => new TeamStandingDto(
                ts.Id.ToString(),
                ts.Year,
                ts.Position,
                ts.TeamId.ToString(),
                ts.Team.Name,
                ts.Points))
            .ToListAsync();

        return (queryTeamStandingCount, teamStandings);
    }

    private static IQueryable<TeamStanding> BuildQueryFilter(GetTeamStandingsParameter parameters, IQueryable<TeamStanding> query)
    {
        query = ApplyFilters(parameters, query);
        query = ApplySorting(parameters, query);

        return query;

        static IQueryable<TeamStanding> ApplyFilters(GetTeamStandingsParameter parameters, IQueryable<TeamStanding> query)
        {
            if (!string.IsNullOrWhiteSpace(parameters.Id))
            {
                query = query.Where(ts => parameters.Id.Contains(ts.Id.ToString()));
            }

            if (!string.IsNullOrWhiteSpace(parameters.TeamId))
            {
                query = query.Where(ts => parameters.TeamId.Contains(ts.TeamId.ToString()));
            }

            // Zwraca Teamy z mniej więcej podaną nazwą.
            if (!string.IsNullOrWhiteSpace(parameters.TeamName))
            {
                query = query.Where(ts => ts.Team.Name.Contains(parameters.TeamName));
            }

            // Zwraca Teamy z dokładnie podaną nazwą.
            //if (!string.IsNullOrWhiteSpace(parameters.TeamName))
            //{
            //    query = query.Where(ts => parameters.TeamName.Contains(ts.Team.Name));
            //}

            if (parameters.Year is not null)
            {
                var yearParts = parameters.Year.Split("-");
                var yearStart = int.Parse(yearParts[0]);
                if (yearParts.Length == 2)
                {
                    var yearEnd = int.Parse(yearParts[1]);
                    query = query.Where(ts => ts.Year >= yearStart && ts.Year <= yearEnd);
                }
                
                query = query.Where(ts => ts.Year == yearStart);
            }

            return query;
        }

        static IQueryable<TeamStanding> ApplySorting(GetTeamStandingsParameter parameters, IQueryable<TeamStanding> query)
        {
            if (!string.IsNullOrWhiteSpace(parameters.SortField))
            {
                switch (parameters.SortField)
                {
                    case QueryRepositoryConstant.YearField:
                        query = parameters.SortOrder == QueryRepositoryConstant.DescendingOrder
                            ? query.OrderByDescending(t => t.Year)
                            : query.OrderBy(t => t.Year);
                        break;
                    case QueryRepositoryConstant.PositionField:
                        query = parameters.SortOrder == QueryRepositoryConstant.DescendingOrder
                            ? query.OrderByDescending(t => t.Position)
                            : query.OrderBy(t => t.Position);
                        break;
                    case QueryRepositoryConstant.PointsField:
                        query = parameters.SortOrder == QueryRepositoryConstant.DescendingOrder
                            ? query.OrderByDescending(t => t.Points)
                            : query.OrderBy(t => t.Points);
                        break;
                    default:
                        break;
                }
            }

            return query;
        }
    }
}
