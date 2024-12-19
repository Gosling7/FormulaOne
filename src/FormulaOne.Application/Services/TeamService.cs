using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.Application.Services;

public enum SortDirection
{
    Ascending,
    Descending
}

internal class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;

    public TeamService(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<PagedResult<TeamDto>> GetTeams(GetTeamsParameters parameters)
    {
        // TODO: walidacja parametrów
        var errors = new List<string>();

        // _parametersValidator.Validate(parameters);
        // if (errors.Count > 0)
        // {
        //     return new PagedResult<TeamDto>(
        //         CurrentPage: 0,
        //         TotalPages: 0,
        //         PageSize: 0,
        //         TotalResults: 0,
        //         Errors: errors,
        //         Items: new List<TeamDto>());
        // }

        /*
         * var errors = parametersValidator.Validate(parameters)
         * if (errors.Any())
         * {
         *     errors.Add("");
         * }
         * 
         * sortingValidator.Validate()
         * paginationValidator.Validate()
         * 
         * 
         */

        // Id
        ValidateId(parameters, errors);

        // Sort
        ValidateSorting(parameters, errors);

        // Pagination
        ValidatePagination(parameters, errors);


        // _sortingValidator.Validate()

        if (errors.Count > 0)
        {
            return new PagedResult<TeamDto>(
                CurrentPage: 0,
                TotalPages: 0,
                PageSize: 0,
                TotalResults: 0,
                Errors: errors,
                Items: new List<TeamDto>());
        }

        var totalResults = await _teamRepository.GetTeamsCountAsync();
        var pagedTeams = await _teamRepository.GetTeamsAsync(parameters);

        return new PagedResult<TeamDto>(
            CurrentPage: 0,
            TotalPages: 0,
            PageSize: 0,
            TotalResults: totalResults,
            Errors: errors,
            Items: pagedTeams.Select(team => new TeamDto(
                Id: team.Id.ToString(),
                Name: team.Name)));
    }

    private static void ValidatePagination(GetTeamsParameters parameters, List<string> errors)
    {
        if (parameters.Page <= 0)
        {
            errors.Add($"Invalid page number: {parameters.Page}. " +
                $"Valid values are greater than 0.");
        }
    }

    private static void ValidateSorting(GetTeamsParameters parameters, List<string> errors)
    {
        if (!string.IsNullOrWhiteSpace(parameters.Sort))
        {
            var sortStringParts = parameters.Sort.Split(":");
            if (sortStringParts.Length != 2)
            {
                errors.Add($"Invalid sorting format: {parameters.Sort}. " +
                   $"Valid format: field:direction (e.g., id:asc).");
            }
            else
            {
                var validSortFields = new[] { "id", "name" };
                var sortField = sortStringParts[0].ToLower();
                if (!validSortFields.Contains(sortField))
                {
                    errors.Add($"Invalid sorting field: {sortField}. " +
                        $"Valid values: {string.Join(", ", validSortFields)}.");
                }

                var validSortDirections = new[] { "asc", "desc" };
                var sortDirection = sortStringParts[1].ToLower();
                if (!validSortDirections.Contains(sortDirection))
                {
                    errors.Add($"Invalid sorting direction: {sortDirection}. " +
                        $"Valid values: {string.Join(", ", validSortDirections)}.");
                }
            }
        }
    }

    private static void ValidateId(GetTeamsParameters parameters, List<string> errors)
    {
        if (!string.IsNullOrWhiteSpace(parameters.Id))
        {
            var ids = parameters.Id.Split(',');
            foreach (var id in ids)
            {
                if (!Guid.TryParse(id, out _))
                {
                    errors.Add($"Team Id is not a valid Guid: {id}.");
                }
            }
        }
    }
}

//if (parameters.Id is not null)
//{
//    var ids = parameters.Id?.Split(',').ToList();
//    if (ids is not null) 
//    {
//        // jeden id
//        // ?id=guid1
//        if (ids.Count == 1)
//        {
//            if (!Guid.TryParse(parameters.Id, out _))
//            {
//                errors.Add($"Team Id is not a valid Guid: {parameters.Id}.");
//            }
//        }

//        // bulk fetch
//        // ?id=guid1,guid2
//        if (ids.Count > 1)
//        {
//            foreach (var id in ids)
//            {
//                if (!Guid.TryParse(parameters.Id, out _))
//                {
//                    errors.Add($"Team Id is not a valid Guid: {parameters.Id}.");
//                }
//            }
//        }
//    }
//}