using FormulaOne.Application.Constants;
using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;
using FormulaOne.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Infrastructure.Repositories;

public class DriverStandingRepository : IDriverStandingRepository
{
    private readonly FormulaOneDbContext _context;

    public DriverStandingRepository(FormulaOneDbContext context)
    {
        _context = context;
    }

    public async Task<(int, IEnumerable<DriverStandingDto>)> GetItemsAsync(
        GetDriverStandingsParameter parameter)
    {
        IQueryable<DriverStanding> query = _context.DriverStandings;
        query = BuildQueryFilter(parameter, query);

        var queryDriverStandingCount = await query.CountAsync();

        query = query
            .Skip((parameter.Page - 1) * parameter.PageSize)
            .Take(parameter.PageSize);

        var teamStandings = await query
            .Include(ds => ds.Team)
            .Select(ds => new DriverStandingDto
            {
                Id = ds.Id.ToString(),
                Position = ds.Position,
                DriverId = ds.Driver.Id.ToString(),
                DriverName = ds.Driver.FirstName + " " + ds.Driver.LastName,
                Nationality = ds.Driver.Nationality,
                TeamId = ds.Team.Id.ToString(),
                TeamName = ds.Team.Name,
                Points = ds.Points,
                Year = ds.Year
            })
            .ToListAsync();

        return (queryDriverStandingCount, teamStandings);
    }

    private static IQueryable<DriverStanding> BuildQueryFilter(
        GetDriverStandingsParameter parameter,
        IQueryable<DriverStanding> query)
    {
        query = ApplyFilters(parameter, query);
        query = ApplySorting(parameter, query);

        return query;

        static IQueryable<DriverStanding> ApplyFilters(
            GetDriverStandingsParameter parameter,
            IQueryable<DriverStanding> query)
        {
            if (!string.IsNullOrWhiteSpace(parameter.Id))
            {
                query = query.Where(ds => parameter.Id.Contains(ds.Id.ToString()));
            }

            if (!string.IsNullOrWhiteSpace(parameter.DriverId))
            {
                query = query.Where(ds => parameter.DriverId.Contains(ds.Driver.Id.ToString()));
            }

            if (parameter.Year is not null)
            {
                var yearParts = parameter.Year.Split("-");
                var yearStart = int.Parse(yearParts[0]);
                if (yearParts.Length == 2)
                {
                    var yearEnd = int.Parse(yearParts[1]);
                    query = query.Where(ds => ds.Year >= yearStart && ds.Year <= yearEnd);
                }
                else
                {
                    query = query.Where(ds => ds.Year == yearStart);
                }
            }

            return query;
        }

        static IQueryable<DriverStanding> ApplySorting(
            GetDriverStandingsParameter parameters,
            IQueryable<DriverStanding> query)
        {
            if (!string.IsNullOrWhiteSpace(parameters.SortField))
            {
                switch (parameters.SortField)
                {
                    case QueryRepositoryConstant.YearField:
                        query = parameters.SortOrder == QueryRepositoryConstant.DescendingOrder
                            ? query.OrderByDescending(ds => ds.Year)
                            : query.OrderBy(ds => ds.Year);
                        break;
                    case QueryRepositoryConstant.PositionField:
                        query = parameters.SortOrder == QueryRepositoryConstant.DescendingOrder
                            ? query.OrderByDescending(ds => ds.Position)
                            : query.OrderBy(ds => ds.Position);
                        break;
                    case QueryRepositoryConstant.PointsField:
                        query = parameters.SortOrder == QueryRepositoryConstant.DescendingOrder
                            ? query.OrderByDescending(ds => ds.Points)
                            : query.OrderBy(ds => ds.Points);
                        break;
                    default:
                        break;
                }
            }

            return query;
        }
    }
}
