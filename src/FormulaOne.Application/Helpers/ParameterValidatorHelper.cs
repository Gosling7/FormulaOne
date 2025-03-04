﻿using FormulaOne.Application.Constants;
using FormulaOne.Application.Interfaces;
using FormulaOne.Core.Entities;

namespace FormulaOne.Application.Helpers;

internal class ParameterValidatorHelper : IParameterValidatorHelper
{
    public void ValidateId(string? idParameter, List<string> errors,
        string idParameterName = "Id")
    {
        if (string.IsNullOrWhiteSpace(idParameter))
        {
            return;
        }

        var ids = idParameter.Split(',');
        foreach (var id in ids)
        {
            if (!Guid.TryParse(id, out _))
            {
                errors.Add(ValidationMessage.InvalidGuid(idParameterName, id));
            }
        }
    }

    public void ValidatePagination(int pageParameter, List<string> errors)
    {
        if (pageParameter <= 0)
        {
            errors.Add(ValidationMessage.InvalidPageNumber(pageParameter));
        }
    }

    public void ValidateYear(string? yearParameter, List<string> errors,
        bool isTeamStanding = false)
    {
        if (string.IsNullOrWhiteSpace(yearParameter))
        {
            return;
        }

        var yearParts = yearParameter.Split("-");
        if (yearParts.Length > 2)
        {
            errors.Add(ValidationMessage.InvalidYearFormat(yearParameter));

            return;
        }

        var validEndYear = DateTime.Now.Year;
        var validStartYear = isTeamStanding 
            ? ValidationConstant.TeamStandingStartYear 
            : ValidationConstant.StartYear;

        if (yearParts.Length == 1)
        {
            ValidateSingleYear(yearParameter, errors, validEndYear, validStartYear, yearParts);

            return;
        }

        ValidateYearRange(yearParameter, errors, validEndYear, validStartYear, yearParts);
    }

    public void ValidateSorting(string? fieldParameter, string? orderParameter,
        IEnumerable<string> validSortFields, List<string> errors)
    {
        if (string.IsNullOrWhiteSpace(fieldParameter)
            && string.IsNullOrWhiteSpace(orderParameter))
        {
            return;
        }

        var field = fieldParameter?.ToLower();
        var order = orderParameter?.ToLower();

        if (!string.IsNullOrWhiteSpace(field) && !validSortFields.Contains(field))
        {
            errors.Add(ValidationMessage.InvalidSortField(field, validSortFields));
        }

        if (!string.IsNullOrWhiteSpace(order) && string.IsNullOrWhiteSpace(field))
        {
            errors.Add(ValidationMessage.ExplicitSortingRequiresSortField(validSortFields));
        }

        var validSortDirections = new[] 
        { 
            ValidationConstant.AscendingSortDirection, 
            ValidationConstant.DescendingSortDirection
        };
        if (!string.IsNullOrWhiteSpace(order) && !validSortDirections.Contains(order))
        {
            errors.Add(ValidationMessage.InvalidSortOrder(order, validSortDirections));
        }
    }

    public void ValidateResultSorting(string? fieldParameter,
        string? orderParameter, List<string> errors)
    {
        if (string.IsNullOrWhiteSpace(fieldParameter)
            && string.IsNullOrWhiteSpace(orderParameter))
        {
            return;
        }

        var validSortFields = new[]
        {
            nameof(RaceResult.Date).ToLower(),
            nameof(RaceResult.Position).ToLower(),
            nameof(RaceResult.Points).ToLower(),
        };
        ValidateSorting(fieldParameter, orderParameter, validSortFields, errors);
    }    

    private static void ValidateSingleYear(string yearParameter, List<string> errors,
        int validEndYear, int validStartYear, string[] yearParts)
    {
        if (!int.TryParse(yearParts[0], out var year))
        {
            errors.Add(ValidationMessage.YearFilterNotNumber(yearParameter));

            return;
        }

        if (year < validStartYear
            || year > validEndYear)
        {
            errors.Add(ValidationMessage.InvalidSingleYearFilter(yearParameter, 
                validStartYear, validEndYear));
        }
    }

    private static void ValidateYearRange(string yearParameter, List<string> errors,
        int validEndYear, int validStartYear, string[] yearParts)
    {
        if (!int.TryParse(yearParts[0], out var startYear))
        {
            errors.Add(ValidationMessage.YearFilterNotNumber(yearParameter));

            return;
        }
        if (!int.TryParse(yearParts[1], out var endYear))
        {
            errors.Add(ValidationMessage.YearFilterNotNumber(yearParameter));

            return;
        }

        if (startYear > endYear)
        {
            errors.Add(ValidationMessage.StartYearGreaterThanEndYear(startYear, endYear,
                validStartYear, validEndYear));
        }

        if (startYear < validStartYear)
        {
            errors.Add(ValidationMessage.StartYearLessThanValidStartYear(startYear,
                validStartYear, validEndYear));
        }
        if (startYear > validEndYear)
        {
            errors.Add(ValidationMessage.StartYearGreaterThanValidEndYear(startYear,
                validStartYear, validEndYear));
        }

        if (endYear > validEndYear)
        {
            errors.Add(ValidationMessage.EndYearGreaterThanValidEndYear(endYear,
                validStartYear, validEndYear));
        }
        if (endYear < validStartYear)
        {
            errors.Add(ValidationMessage.EndYearLessThanValidStartYear(endYear,
                validStartYear, validEndYear));
        }
    }
}
