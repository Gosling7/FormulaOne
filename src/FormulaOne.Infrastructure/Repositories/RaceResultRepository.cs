﻿using FormulaOne.Application.Constants;
using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;
using FormulaOne.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.Infrastructure.Repositories;

public enum RaceResultFilterType
{
    Driver,
    Team,
    Circuit
}

public class RaceResultRepository : IRaceResultRepository
{
    private readonly FormulaOneDbContext _context;

    public RaceResultRepository(FormulaOneDbContext context)
    {
        _context = context;
    }

    public async Task<(int, IEnumerable<RaceResultDto>)> GetRaceResultsAsync(
        GetTeamResultsParameters parameters)
    {
        IQueryable<RaceResult> query = _context.RaceResults;
        query = BuildQueryFilter(parameters, query);

        var queryRaceResultCount = await query.CountAsync();

        query = query
            .Include(rr => rr.Driver)
            .Include(rr => rr.Team)
            .Include(rr => rr.Circuit)
            .Skip((parameters.Page - 1) * parameters.PageSize)
            .Take(parameters.PageSize);

        // TODO: ogarnąć null w TeamRaceResult
        var raceResults = await query
            .Select(rr => new RaceResultDto(
                rr.Id.ToString(),
                rr.Position,
                rr.Date,
                rr.Circuit.Name,
                rr.Driver.FirstName + " " + rr.Driver.LastName,
                rr.Team.Name,
                rr.Laps,
                rr.Time,
                rr.Points))
            .ToListAsync();

        return (queryRaceResultCount, raceResults);
    }

    private static IQueryable<RaceResult> BuildQueryFilter(GetTeamResultsParameters parameters, IQueryable<RaceResult> query)
    {
        query = ApplyFilters(parameters, query);
        query = ApplySorting(parameters, query);

        return query;        
    }

    private static IQueryable<RaceResult> ApplyFilters(GetTeamResultsParameters parameters, IQueryable<RaceResult> query)
    {
        if (!string.IsNullOrWhiteSpace(parameters.Id))
        {
            query = query.Where(rr => parameters.Id.Contains(rr.Id.ToString()));
        }

        if (!string.IsNullOrWhiteSpace(parameters.TeamId))
        {
            query = query.Where(rr => parameters.TeamId.Contains(rr.TeamId.ToString()));
        }

        // Zwraca Teamy z mniej więcej podaną nazwą.
        if (!string.IsNullOrWhiteSpace(parameters.TeamName))
        {
            query = query.Where(rr => rr.Team.Name.Contains(parameters.TeamName));
        }

        if (parameters.Year is not null)
        {
            var yearParts = parameters.Year.Split("-");
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
        }

        return query;
    }

    private static IQueryable<RaceResult> ApplySorting(GetTeamResultsParameters parameters, IQueryable<RaceResult> query)
    {
        if (!string.IsNullOrWhiteSpace(parameters.SortField))
        {
            switch (parameters.SortField)
            {
                case QueryRepositoryConstant.DateField:
                    query = parameters.SortOrder == QueryRepositoryConstant.DescendingOrder
                        ? query.OrderByDescending(rr => rr.Date)
                        : query.OrderBy(rr => rr.Date);
                    break;
                case QueryRepositoryConstant.PointsField:
                    query = parameters.SortOrder == QueryRepositoryConstant.DescendingOrder
                        ? query.OrderByDescending(rr => rr.Points)
                        : query.OrderBy(rr => rr.Points);
                    break;
                case QueryRepositoryConstant.PositionField:
                    query = parameters.SortOrder == QueryRepositoryConstant.DescendingOrder
                        ? query.OrderByDescending(rr => rr.Position)
                        : query.OrderBy(rr => rr.Position);
                    break;
                default:
                    break;
            }
        }

        return query;
    }
}
