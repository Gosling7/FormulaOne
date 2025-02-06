using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Parameters;
using FormulaOne.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Infrastructure.Repositories;

public class CircuitRepository : ICircuitRepository
{
    private readonly FormulaOneDbContext _context;

    public CircuitRepository(FormulaOneDbContext context)
    {
        _context = context;
    }

    public async Task<(int, IEnumerable<CircuitDto>)> GetItemsAsync(GetCircuitsParameter parameters)
    {
        IQueryable<Circuit> query = _context.Circuits;
        query = BuildQueryFilter(parameters, query);

        var queryItemsCount = await query.CountAsync();

        query = query
            .Skip((parameters.Page - 1) * parameters.PageSize)
            .Take(parameters.PageSize);

        var drivers = await query
            .Select(c => new CircuitDto
            {
                Id = c.Id.ToString(),
                Name = c.Name,
                Location = c.Location,
            })
            .ToListAsync();

        return (queryItemsCount, drivers);
    }

    private static IQueryable<Circuit> BuildQueryFilter(GetCircuitsParameter parameters,
        IQueryable<Circuit> query)
    {
        query = ApplyFilters(parameters, query);
        query = ApplySorting(parameters, query);

        return query;
    }

    private static IQueryable<Circuit> ApplyFilters(GetCircuitsParameter parameters,
            IQueryable<Circuit> query)
    {
        if (!string.IsNullOrWhiteSpace(parameters.Name))
        {
            query = query.Where(c => c.Name.Contains(parameters.Name.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(parameters.Id))
        {
            query = query.Where(c => parameters.Id.Contains(c.Id.ToString()));
        }

        return query;
    }

    private static IQueryable<Circuit> ApplySorting(GetCircuitsParameter parameter, IQueryable<Circuit> query)
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
