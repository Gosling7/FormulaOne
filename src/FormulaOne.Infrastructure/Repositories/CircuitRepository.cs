using FormulaOne.Application.Constants;
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

    public async Task<(int, IReadOnlyCollection<CircuitDto>)> GetItemsAsync(
        GetCircuitsParameter parameters)
    {
        IQueryable<Circuit> query = _context.Circuits;
        query = BuildQueryFilter(parameters, query);

        var queryItemsCount = await query.CountAsync();

        query = query
            .Skip((parameters.Page - 1) * parameters.PageSize)
            .Take(parameters.PageSize);

        var circuits = await query
            .Select(c => new CircuitDto
            {
                Id = c.Id.ToString(),
                Name = c.Name,
                Location = c.Location,
            })
            .ToListAsync();

        return (queryItemsCount, circuits);
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

        if (!string.IsNullOrWhiteSpace(parameters.Location))
        {
            query = query.Where(c => parameters.Location.Contains(c.Location));
        }

        return query;
    }

    private static IQueryable<Circuit> ApplySorting(GetCircuitsParameter parameter, 
        IQueryable<Circuit> query)
    {
        if (!string.IsNullOrWhiteSpace(parameter.SortField))
        {
            switch (parameter.SortField)
            {
                case RepositoryConstant.NameField:
                    query = parameter.SortOrder == RepositoryConstant.DescendingOrder
                        ? query.OrderByDescending(c => c.Name)
                        : query.OrderBy(c => c.Name);
                    break;
                case RepositoryConstant.LocationField:
                    query = parameter.SortOrder == RepositoryConstant.DescendingOrder
                        ? query.OrderByDescending(c => c.Location)
                        : query.OrderBy(c => c.Location);
                    break;
                default:
                    break;
            }
        }

        return query;
    }
}
