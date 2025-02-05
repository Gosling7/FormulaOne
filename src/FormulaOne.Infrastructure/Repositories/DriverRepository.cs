using FormulaOne.Application.Constants;
using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;
using FormulaOne.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Infrastructure.Repositories;

public class DriverRepository : IDriverRepository
{
    private readonly FormulaOneDbContext _context;

    public DriverRepository(FormulaOneDbContext context)
    {
        _context = context;
    }

    public async Task<(int, IEnumerable<DriverDto>)> GetDriversAsync(
        GetDriversParameter parameters)
    {
        IQueryable<Driver> query = _context.Drivers;
        query = BuildQueryFilter(parameters, query);

        var queryTeamCount = await query.CountAsync();

        query = query
            .Skip((parameters.Page - 1) * parameters.PageSize)
            .Take(parameters.PageSize);

        var drivers = await query
            .Select(d => new DriverDto
            {
                Id = d.Id.ToString(),
                Name = $"{d.FirstName} {d.LastName}",
                Nationality = d.Nationality,
            })
            .ToListAsync();

        return (queryTeamCount, drivers);
    }

    private static IQueryable<Driver> BuildQueryFilter(GetDriversParameter parameters,
        IQueryable<Driver> query)
    {
        query = ApplyFilters(parameters, query);
        query = ApplySorting(parameters, query);

        return query;
    }

    private static IQueryable<Driver> ApplyFilters(GetDriversParameter parameter,
            IQueryable<Driver> query)
    {
        if (!string.IsNullOrWhiteSpace(parameter.Name))
        {
            query = query.Where(d =>
                d.FirstName.ToLower().Contains(parameter.Name.ToLower()) ||
                d.LastName.ToLower().Contains(parameter.Name.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(parameter.Id))
        {
            query = query.Where(t => parameter.Id.Contains(t.Id.ToString()));
        }

        if (!string.IsNullOrWhiteSpace(parameter.Nationality))
        {
            query = query.Where(t => parameter.Nationality.Contains(t.Nationality.ToString()));
        }

        return query;
    }

    private static IQueryable<Driver> ApplySorting(GetDriversParameter parameter, IQueryable<Driver> query)
    {
        // TODO: jak zrobić sortowanie po Name, skoro w bazie jest FirstName i LastName
        if (!string.IsNullOrWhiteSpace(parameter.SortField))
        {
            //switch (parameter.SortField)
            //{
            //    case QueryRepositoryConstant.NameField:
            //        query = parameter.SortOrder == QueryRepositoryConstant.DescendingOrder
            //            ? query.OrderByDescending(t => t.Name)
            //            : query.OrderBy(t => t.Name);
            //        break;
            //    default:
            //        break;
            //}
        }

        return query;
    }
}
