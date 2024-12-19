using FormulaOne.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FormulaOne.Application.Services.PaginationValidator;
namespace FormulaOne.Application.Services;

internal class PaginationValidator
{
    private const int MinYear = 1950;
    private const int MaxYear = 2024;

    public IReadOnlyCollection<string> Validate(GetTeamsParameters parameters)
    {

    }

    public IReadOnlyCollection<string> Validate(GetTeamStandingsParameters parameters)
    {

    }

    public List<string> ValidateIdsAsync(string ids)
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(ids))
        {
            errors.Add("Ids cannot be empty");
            return errors;
        }

        var idList = ids.Split(',').ToList();
        foreach (var id in idList)
        {
            if (!Guid.TryParse(id, out _))
            {
                errors.Add($"Invalid GUID: {id}");
            }
        }

        return errors;
    }

    public async Task<ValidationResult> ValidateNameAsync(string name)
    {
        var result = new ValidationResult();
        if (!string.IsNullOrWhiteSpace(name) && name.Length < 3)
        {
            result.IsValid = false;
            result.Errors.Add("Name must be at least 3 characters long.");
        }
        result.IsValid = result.Errors.Count == 0;
        return await Task.FromResult(result);
    }

    public async Task<ValidationResult> ValidatePageAsync(int page)
    {
        var result = new ValidationResult();
        if (page < 1)
        {
            result.IsValid = false;
            result.Errors.Add("Page number must be greater than 0.");
        }
        result.IsValid = result.Errors.Count == 0;
        return await Task.FromResult(result);
    }

    public async Task<ValidationResult> ValidatePageSizeAsync(int pageSize)
    {
        var result = new ValidationResult();
        if (pageSize < 1 || pageSize > 100)
        {
            result.IsValid = false;
            result.Errors.Add("Page size must be between 1 and 100.");
        }
        result.IsValid = result.Errors.Count == 0;
        return await Task.FromResult(result);
    }

    public async Task<ValidationResult> ValidateNationalityAsync(string nationality)
    {
        var result = new ValidationResult();
        if (string.IsNullOrWhiteSpace(nationality) || nationality.Length != 3)
        {
            result.IsValid = false;
            result.Errors.Add("Nationality must be a 3-letter code.");
        }
        result.IsValid = result.Errors.Count == 0;
        return await Task.FromResult(result);
    }

    public async Task<ValidationResult> ValidateSortAsync(string sort)
    {
        var result = new ValidationResult();
        if (!string.IsNullOrWhiteSpace(sort) && !sort.Contains(":"))
        {
            result.IsValid = false;
            result.Errors.Add("Sort must be in the format 'field:direction'.");
        }
        result.IsValid = result.Errors.Count == 0;
        return await Task.FromResult(result);
    }

    public async Task<ValidationResult> ValidateYearAsync(int? year, bool isTeamStandings = false)
    {
        var result = new ValidationResult();
        if (year == null || year < MinYear || year > MaxYear)
        {
            result.IsValid = false;
            result.Errors.Add($"Year must be between {MinYear} and {MaxYear}.");
        }

        if (isTeamStandings && (year < 1958 || year > 2024))
        {
            result.IsValid = false;
            result.Errors.Add("For team standings, the year must be between 1958 and 2024.");
        }

        result.IsValid = result.Errors.Count == 0;
        return await Task.FromResult(result);
    }

}
