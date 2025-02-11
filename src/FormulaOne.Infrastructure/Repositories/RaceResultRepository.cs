using FormulaOne.Application.Constants;
using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;
using FormulaOne.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Infrastructure.Repositories;

public class RaceResultRepository : IRaceResultRepository
{
    private readonly FormulaOneDbContext _context;

    public RaceResultRepository(FormulaOneDbContext context)
    {
        _context = context;
    }

    public async Task<(int, IReadOnlyCollection<RaceResultDto>)> GetItemsAsync(
        GetTeamResultsParameter parameters)
    {
        IQueryable<RaceResult> query = _context.RaceResults;
        query = ApplyTeamFilters(parameters, query);
        query = ApplySorting(parameters, query);
        return await ExecuteQueryAsync(query, parameters.Page, parameters.PageSize);

        IQueryable<RaceResult> ApplyTeamFilters(GetTeamResultsParameter parameters,
            IQueryable<RaceResult> query)
        {
            if (!string.IsNullOrWhiteSpace(parameters.Id))
            {
                query = query.Where(rr => parameters.Id.Contains(rr.Id.ToString()));
            }
            if (!string.IsNullOrWhiteSpace(parameters.TeamId))
            {
                query = query.Where(rr => parameters.TeamId.Contains(rr.TeamId.ToString()));
            }
            if (!string.IsNullOrWhiteSpace(parameters.TeamName))
            {
                query = query.Where(rr => rr.Team.Name.Contains(parameters.TeamName));
            }
            if (!string.IsNullOrWhiteSpace(parameters.Year))
            {
                query = ApplyYearFilter(parameters.Year, query);
            }

            return query;
        }
    }

    public async Task<(int, IReadOnlyCollection<RaceResultDto>)> GetItemsAsync(
        GetDriverResultsParameter parameters)
    {
        IQueryable<RaceResult> query = _context.RaceResults;
        query = ApplyDriverFilters(parameters, query);
        query = ApplySorting(parameters, query);
        return await ExecuteQueryAsync(query, parameters.Page, parameters.PageSize);

        IQueryable<RaceResult> ApplyDriverFilters(GetDriverResultsParameter parameters,
            IQueryable<RaceResult> query)
        {
            if (!string.IsNullOrWhiteSpace(parameters.Id))
            {
                query = query.Where(rr => parameters.Id.Contains(rr.Id.ToString()));
            }
            if (!string.IsNullOrWhiteSpace(parameters.DriverId))
            {
                query = query.Where(rr => parameters.DriverId.Contains(rr.DriverId.ToString()));
            }
            if (!string.IsNullOrWhiteSpace(parameters.DriverName))
            {
                query = query.Where(rr => (rr.Driver.FirstName + " " + rr.Driver.LastName).Contains(parameters.DriverName));
            }
            if (!string.IsNullOrWhiteSpace(parameters.Year))
            {
                query = ApplyYearFilter(parameters.Year, query);
            }

            return query;
        }
    }

    public async Task<(int, IReadOnlyCollection<RaceResultDto>)> GetItemsAsync(
        GetCircuitResultsParameter parameters)
    {
        IQueryable<RaceResult> query = _context.RaceResults;
        query = ApplyCircuitFilters(parameters, query);
        query = ApplySorting(parameters, query);
        return await ExecuteQueryAsync(query, parameters.Page, parameters.PageSize);

        IQueryable<RaceResult> ApplyCircuitFilters(GetCircuitResultsParameter parameters,
            IQueryable<RaceResult> query)
        {
            if (!string.IsNullOrWhiteSpace(parameters.Id))
            {
                query = query.Where(c => parameters.Id.Contains(c.Id.ToString()));
            }
            if (!string.IsNullOrWhiteSpace(parameters.CircuitId))
            {
                query = query.Where(c => parameters.CircuitId.Contains(c.CircuitId.ToString()));
            }
            if (!string.IsNullOrWhiteSpace(parameters.CircuitName))
            {
                query = query.Where(c => c.Circuit.Name.Contains(parameters.CircuitName));
            }
            if (!string.IsNullOrWhiteSpace(parameters.CircuitLocation))
            {
                query = query.Where(c => c.Circuit.Location.Contains(parameters.CircuitLocation));
            }
            if (!string.IsNullOrWhiteSpace(parameters.Year))
            {
                query = ApplyYearFilter(parameters.Year, query);
            }

            return query;
        }
    }

    private static IQueryable<RaceResult> ApplyYearFilter(string yearParam,
        IQueryable<RaceResult> query)
    {
        var yearParts = yearParam.Split("-");
        var yearStart = int.Parse(yearParts[0]);
        if (yearParts.Length == 2)
        {
            var yearEnd = int.Parse(yearParts[1]);
            query = query.Where(rr => rr.Date.Year >= yearStart && rr.Date.Year <= yearEnd);
        }
        else
        {
            query = query.Where(rr => rr.Date.Year == yearStart);
        }

        return query;
    }

    private static IQueryable<RaceResult> ApplySorting(IQueryParameter parameters,
        IQueryable<RaceResult> query)
    {
        if (string.IsNullOrWhiteSpace(parameters.SortField))
        {
            return query;
        }

        switch (parameters.SortField)
        {
            case RepositoryConstant.DateField:
                query = parameters.SortOrder == RepositoryConstant.DescendingOrder
                    ? query.OrderByDescending(rr => rr.Date)
                    : query.OrderBy(rr => rr.Date);
                break;
            case RepositoryConstant.PointsField:
                query = parameters.SortOrder == RepositoryConstant.DescendingOrder
                    ? query.OrderByDescending(rr => rr.Points)
                    : query.OrderBy(rr => rr.Points);
                break;
            case RepositoryConstant.PositionField:
                query = parameters.SortOrder == RepositoryConstant.DescendingOrder
                    // Push Position==0 (DNFs, DNSs etc) towards the bottom
                    ? query.OrderByDescending(rr => rr.Position == 0)
                        .ThenByDescending(rr => rr.Position)
                    : query.OrderBy(rr => rr.Position == 0)
                        .ThenBy(rr => rr.Position);
                break;
            default:
                break;
        }

        return query;
    }

    private static async Task<(int, IReadOnlyCollection<RaceResultDto>)> ExecuteQueryAsync(
        IQueryable<RaceResult> query, int page, int pageSize)
    {
        var total = await query.CountAsync();

        query = query
            .Include(rr => rr.Driver)
            .Include(rr => rr.Team)
            .Include(rr => rr.Circuit)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        var results = await query
            .Select(rr => new RaceResultDto
            {
                Id = rr.Id.ToString(),
                Position = rr.Position,
                Date = rr.Date,
                CircuitId = rr.CircuitId.ToString(),
                CircuitName = rr.Circuit.Name,
                DriverId = rr.DriverId.ToString(),
                DriverName = rr.Driver.FirstName + " " + rr.Driver.LastName,
                TeamId = rr.TeamId.ToString(),
                TeamName = rr.Team.Name,
                Laps = rr.Laps,
                Time = rr.Time,
                Points = rr.Points
            })
            .ToListAsync();

        return (total, results);
    }
}
