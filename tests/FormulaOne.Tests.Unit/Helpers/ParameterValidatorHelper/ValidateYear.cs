﻿namespace FormulaOne.Tests.Unit.Helpers.ParameterValidatorHelper;

public class ValidateYear
{
    private readonly Application.Helpers.ParameterValidatorHelper _validator = new();
    private readonly List<string> _errors = [];

    [Fact]
    public void Should_not_add_errors_when_input_range_year_is_valid()
    {
        // Arrange
        var validYears = "2020-2024";

        // Act
        _validator.ValidateYear(validYears, _errors);

        // Assert
        Assert.Empty(_errors);
    }

    [Fact]
    public void Should_not_add_errors_when_input_single_year_is_valid()
    {
        // Arrange
        var validYears = "2020";

        // Act
        _validator.ValidateYear(validYears, _errors);

        // Assert
        Assert.Empty(_errors);
    }

    [Fact]
    public void Should_not_add_errors_when_input_year_is_null_or_whitespace()
    {
        // Arrange
        string? nullYear = null;
        string? emptyYear = "";
        string? whiteSpaceYear = " ";

        // Act
        _validator.ValidateYear(nullYear, _errors);
        _validator.ValidateYear(emptyYear, _errors);
        _validator.ValidateYear(whiteSpaceYear, _errors);

        // Assert
        Assert.Empty(_errors);
    }

    [Fact]
    public void Should_add_error_when_input_single_year_is_less_than_valid_start_year()
    {
        // Arrange
        var invalidYear = "1949";

        // Act
        _validator.ValidateYear(invalidYear, _errors);

        // Assert
        Assert.Single(_errors);

        var validStartYear = 1950;
        var message = $"Invalid year filer: {invalidYear}. " +
            $"The year cannot be less than {validStartYear}.";
        Assert.Contains(message, _errors);
    }

    [Fact]
    public void Should_add_error_when_input_single_year_is_greater_than_valid_end_year()
    {
        // Arrange
        var invalidYear = "3000";

        // Act
        _validator.ValidateYear(invalidYear, _errors);

        // Assert
        Assert.Single(_errors);

        var validEndYear = DateTime.UtcNow.Year;
        var message = $"Invalid year filer: {invalidYear}. " +
            $"The year cannot be greater than {validEndYear}.";
        Assert.Contains(message, _errors);
    }

    [Fact]
    public void Should_add_error_when_input_year_format_is_neither_single_nor_range()
    {
        // Arrange
        var invalidYearsFormat = "2022-2022-444";

        // Act
        _validator.ValidateYear(invalidYearsFormat, _errors);

        // Assert
        Assert.Single(_errors);

        var message = $"Invalid year format: {invalidYearsFormat}. " +
            $"Valid format is: StartYear-EndYear, e.g., 2000-2024. ";
        Assert.Contains(message, _errors);
    }

    [Fact]
    public void Should_add_error_when_input_range_year_start_year_has_non_numeric_values()
    {
        // Arrange
        var nonNumericYears = "1x5x-2000";

        // Act
        _validator.ValidateYear(nonNumericYears, _errors);

        // Assert
        Assert.Single(_errors);

        var message = $"Start year is not a number: {nonNumericYears}. " +
            $"Valid format is: StartYear-EndYear, e.g., 2000-2024. ";
        Assert.Contains(message, _errors);
    }

    [Fact]
    public void Should_add_error_when_input_range_year_end_year_has_non_numeric_values()
    {
        // Arrange
        var nonNumericYears = "2000-20x0";

        // Act
        _validator.ValidateYear(nonNumericYears, _errors);

        // Assert
        Assert.Single(_errors);

        var message = $"End year is not a number: {nonNumericYears}. " +
            $"Valid format is: StartYear-EndYear, e.g., 2000-2024. ";
        Assert.Equal(message, _errors.First());
    }

    [Fact]
    public void Should_add_2_errors_when_input_range_year_start_and_end_year_have_non_numeric_values()
    {
        // Arrange
        var nonNumericYears = "20xy-20x0";

        // Act
        _validator.ValidateYear(nonNumericYears, _errors);

        // Assert
        Assert.Equal(2, _errors.Count);

        var startYearMessage = $"Start year is not a number: {nonNumericYears}. " +
            $"Valid format is: StartYear-EndYear, e.g., 2000-2024. ";
        var endYearMessage = $"End year is not a number: {nonNumericYears}. " +
            $"Valid format is: StartYear-EndYear, e.g., 2000-2024. ";
        Assert.Contains(startYearMessage, _errors);
        Assert.Contains(endYearMessage, _errors);
    }

    [Fact]
    public void Should_add_error_when_input_range_year_start_is_greater_than_end_year()
    {
        // Arrange
        var yearsParameter = "2020-2000";
        var yearParts = yearsParameter.Split("-");
        var startYear = yearParts[0];
        var endYear = yearParts[1];

        // Act
        _validator.ValidateYear(yearsParameter, _errors);

        // Assert
        Assert.Single(_errors);

        var validStartYear = 1950;
        var validEndYear = DateTime.Now.Year;
        var message = $"Invalid year range: {yearsParameter}. " +
            $"The start year ({startYear}) cannot be greater than the end year ({endYear}). " +
            $"Valid range is between {validStartYear} and {validEndYear}. ";
        Assert.Contains(message, _errors);
    }

    [Fact]
    public void Should_add_error_when_input_range_year_start_year_is_less_than_valid_start_year()
    {
        // Arrange
        var yearsParameter = "1949-2000";

        // Act
        _validator.ValidateYear(yearsParameter, _errors);

        // Assert
        Assert.Single(_errors);

        var yearParts = yearsParameter.Split("-");
        var startYear = yearParts[0];
        var validStartYear = 1950;
        var validEndYear = DateTime.Now.Year;
        var message = $"Invalid year range: {yearsParameter}. " +
            $"The start year ({startYear}) cannot be greater than {validStartYear}. " +
            $"Valid range is between {validStartYear} and {validEndYear}. ";
        Assert.Contains(message, _errors);
    }

    [Fact]
    public void Should_add_error_when_input_range_year_end_is_greater_than_valid_end_year()
    {
        // Arrange
        var yearsParameter = "2000-3000";

        // Act
        _validator.ValidateYear(yearsParameter, _errors);

        // Assert
        Assert.Single(_errors);

        var yearParts = yearsParameter.Split("-");
        var endYear = yearParts[1];
        var validStartYear = 1950;
        var validEndYear = DateTime.Now.Year;
        var message = $"Invalid year range: {yearsParameter}. " +
            $"The end year ({endYear}) cannot be less than the {validEndYear}). " +
            $"Valid range is between {validStartYear} and {validEndYear}. ";
        Assert.Contains(message, _errors);
    }
}
