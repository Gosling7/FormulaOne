using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.Application.Validators;

internal class QueryParameterValidatorBase
{
    protected void ValidateId(string idParameter, List<string> errors)
    {
        if (!string.IsNullOrWhiteSpace(idParameter))
        {
            var ids = idParameter.Split(',');
            foreach (var id in ids)
            {
                if (!Guid.TryParse(id, out _))
                {
                    errors.Add($"Team Id is not a valid Guid: {id}.");
                }
            }
        }
    }

    protected void ValidateSorting(string sortParameter, List<string> errors)
    {
        if (!string.IsNullOrWhiteSpace(sortParameter))
        {
            var sortStringParts = sortParameter.Split(":");
            if (sortStringParts.Length != 2)
            {
                errors.Add($"Invalid sorting format: {sortParameter}. " +
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

    protected void ValidatePagination(int pageParameter, List<string> errors)
    {
        if (pageParameter <= 0)
        {
            errors.Add($"Invalid page number: {pageParameter}. " +
                $"Valid values are greater than 0.");
        }
    }

    protected void ValidateYear(int year, List<string> errors)
    {
        if (year < 1950)
        {
            // TODO: error do poprawy
            errors.Add($"Invalid year: {year}," +
                $"Valid years are between 1950 and 2024.");
        }
    }

    protected void ValidateSession(string sessionParameter, List<string> errors)
    {
        if (!string.IsNullOrWhiteSpace(sessionParameter))
        {
            var validSessions = new[]
            {
                "fp1", "fp2", "fp3", "qualifying", "race", "sprint", "sprintqualifying"
            };
            if (!validSessions.Contains(sessionParameter))
            {
                errors.Add($"Invalid session: {sessionParameter}. " +
                    $"Valid values: {string.Join(", ", validSessions)}.");
            }
        }
    }
}
