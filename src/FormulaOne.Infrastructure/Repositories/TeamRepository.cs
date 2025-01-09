using FormulaOne.Application.Constants;
using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;
using FormulaOne.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Infrastructure.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly FormulaOneDbContext _context;

    public TeamRepository(FormulaOneDbContext context)
    {
        _context = context;
    }

    public async Task<(int, IEnumerable<TeamDto>)> GetTeamsAsync(GetTeamsParameters parameters)
    {
        IQueryable<Team> query = _context.Teams;
        query = BuildQueryFilter(parameters, query);

        var queryTeamCount = await query.CountAsync();

        query = query
            .Skip((parameters.Page - 1) * parameters.PageSize)
            .Take(parameters.PageSize);

        var teams = await query
            .Select(t => new TeamDto(
                t.Id.ToString(),
                t.Name))
            .ToListAsync();

        return (queryTeamCount, teams);
    }

    private static IQueryable<Team> BuildQueryFilter(GetTeamsParameters parameters, IQueryable<Team> query)
    {
        query = ApplyFilters(parameters, query);
        query = ApplySorting(parameters, query);

        return query;

        static IQueryable<Team> ApplyFilters(GetTeamsParameters parameters, IQueryable<Team> query)
        {
            if (!string.IsNullOrWhiteSpace(parameters.Name))
            {
                query = query.Where(d => d.Name.Contains(parameters.Name.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Id))
            {
                query = query.Where(t => parameters.Id.Contains(t.Id.ToString()));
            }

            return query;
        }

        static IQueryable<Team> ApplySorting(GetTeamsParameters parameters, IQueryable<Team> query)
        {
            if (!string.IsNullOrWhiteSpace(parameters.SortField))
            {
                switch (parameters.SortField)
                {
                    case QueryRepositoryConstant.NameField:
                        query = parameters.SortOrder == QueryRepositoryConstant.DescendingOrder
                            ? query.OrderByDescending(t => t.Name)
                            : query.OrderBy(t => t.Name);
                        break;
                    default:
                        break;
                }
            }

            return query;
        }
    }
}
