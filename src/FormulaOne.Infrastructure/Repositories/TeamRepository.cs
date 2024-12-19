using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaOne.Domain.Entities;
using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;
using Microsoft.EntityFrameworkCore;
using FormulaOne.Application.DataTransferObjects;

namespace FormulaOne.Infrastructure.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly FormulaOneDbContext _context;

    public TeamRepository(FormulaOneDbContext context)
    {
        _context = context;
    }

    public Task<Team?> GetTeamById(Guid id)
    {
        return _context.Teams
            .SingleOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Team>> GetTeamsAsync(GetTeamsParameters parameters)
    {
        IQueryable<Team> query = _context.Teams;

        // Apply NameFilter if provided
        if (!string.IsNullOrWhiteSpace(parameters.NameFilter))
        {
            query = query.Where(d => d.Name.Contains(parameters.NameFilter, StringComparison.CurrentCultureIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(parameters.Id.ToString()))
        {
            query = query.Where(t => parameters.Id.Contains(t.Id.ToString());
        }

        // Apply Pagination
        query = query
            .OrderBy(t => t.Name) // Apply ordering to ensure consistent results
            .Skip((parameters.Page - 1) * parameters.PageSize)
            .Take(parameters.PageSize);

        return await query.ToListAsync();
    }

    public Task<IEnumerable<TeamStanding>> GetTeamAllStandingsByTeamId(Guid id)
    {
        throw new NotImplementedException();
    }    

    public Task<IEnumerable<RaceResult>> GetTeamRaceResultsByYear(Guid id, int year)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TeamStanding>> GetTeamStandings(int year)
    {
        throw new NotImplementedException();
    }

    public static Task<int> GetTeamsCountAsync(IQueryable<Team> query) 
        => query.CountAsync();
}
