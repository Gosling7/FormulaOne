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

    public async Task<(int, IReadOnlyCollection<DriverDto>)> GetItemsAsync(
        GetDriversParameter parameters)
    {
        IQueryable<Driver> query = _context.Drivers;
        query = BuildQueryFilter(parameters, query);

        var queryDriverCount = await query.CountAsync();

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

        return (queryDriverCount, drivers);
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
            query = query.Where(d => parameter.Id.Contains(d.Id.ToString()));
        }

        if (!string.IsNullOrWhiteSpace(parameter.Nationality))
        {
            query = query.Where(d => parameter.Nationality.Contains(d.Nationality.ToString()));
        }

        return query;
    }

    private static IQueryable<Driver> ApplySorting(GetDriversParameter parameter, 
        IQueryable<Driver> query)
    {
        if (!string.IsNullOrWhiteSpace(parameter.SortField))
        {
            switch (parameter.SortField)
            {
                case RepositoryConstant.FirstNameField:
                    query = parameter.SortOrder == RepositoryConstant.DescendingOrder
                        ? query.OrderByDescending(d => d.FirstName)
                        : query.OrderBy(d => d.FirstName);
                    break;
                case RepositoryConstant.LastNameField:
                    query = parameter.SortOrder == RepositoryConstant.DescendingOrder
                        ? query.OrderByDescending(d => d.LastName)
                        : query.OrderBy(d => d.LastName);
                    break;
                case RepositoryConstant.NationalityField:
                    query = parameter.SortOrder == RepositoryConstant.DescendingOrder
                        ? query.OrderByDescending(d => d.Nationality)
                        : query.OrderBy(d => d.Nationality);
                    break;
                default:
                    break;
            }
        }

        return query;
    }
}
