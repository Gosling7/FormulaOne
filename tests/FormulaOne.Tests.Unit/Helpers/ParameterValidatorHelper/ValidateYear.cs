using FormulaOne.Application.Constants;

namespace FormulaOne.Tests.Unit.Helpers.ParameterValidatorHelper;

public class ValidateYear
{
    private readonly Application.Helpers.ParameterValidatorHelper _validator = new();
    private readonly List<string> _errors = [];
    private readonly int ValidEndYear = DateTime.UtcNow.Year;
    private const int ValidStartYear = ValidationConstant.StartYear;
    private const int ValidTeamStandingStartYear = ValidationConstant.TeamStandingStartYear;

    [Fact]
    public void Should_not_add_errors_when_input_range_year_is_valid()
    {
        // Arrange
        var validYears = $"{ValidStartYear}-{ValidEndYear}";

        // Act
        _validator.ValidateYear(validYears, _errors, isTeamStanding: false);

        // Assert
        Assert.Empty(_errors);
    }

    [Fact]
    public void Should_not_add_errors_when_input_single_year_is_valid()
    {
        // Arrange
        var validYear = $"{ValidStartYear}";

        // Act
        _validator.ValidateYear(validYear, _errors, isTeamStanding: false);

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
        _validator.ValidateYear(nullYear, _errors, isTeamStanding: false);
        _validator.ValidateYear(emptyYear, _errors, isTeamStanding: false);
        _validator.ValidateYear(whiteSpaceYear, _errors, isTeamStanding: false);

        // Assert
        Assert.Empty(_errors);
    }

    [Fact]
    public void Should_add_error_when_input_year_format_is_neither_single_nor_range()
    {
        // Arrange
        var invalidYears = "1950-1951-1952";

        // Act
        _validator.ValidateYear(invalidYears, _errors, isTeamStanding: false);

        // Assert
        Assert.Single(_errors);
        Assert.Contains(ValidationMessage.InvalidYearFormat(invalidYears), _errors);
    }

    [Fact]
    public void Should_add_error_when_input_year_contains_non_numeric_character()
    {
        // Arrange
        var invalidYear = "199x";

        // Act
        _validator.ValidateYear(invalidYear, _errors, isTeamStanding: false);

        // Assert
        Assert.Single(_errors);
        Assert.Contains(ValidationMessage.YearFilterNotNumber(invalidYear), _errors);
    }

    [Fact]
    public void Should_add_error_when_input_single_year_is_less_than_valid_start_year()
    {
        // Arrange
        var invalidYear = $"{ValidStartYear - 1}";
        var isTeamStanding = false;

        // Act
        _validator.ValidateYear(invalidYear, _errors, isTeamStanding);

        // Assert
        Assert.Single(_errors);
        Assert.Contains(ValidationMessage.InvalidSingleYearFilter(invalidYear,
            ValidStartYear, ValidEndYear), _errors);
    }

    [Fact]
    public void Should_add_error_when_input_single_year_is_less_than_valid_start_year_for_team_standing()
    {
        // Arrange
        var invalidYear = $"{ValidTeamStandingStartYear - 1}";
        var isTeamStanding = true;

        // Act
        _validator.ValidateYear(invalidYear, _errors, isTeamStanding);

        // Assert
        Assert.Single(_errors);
        Assert.Contains(ValidationMessage.InvalidSingleYearFilter(invalidYear,
            ValidTeamStandingStartYear, ValidEndYear), _errors);
    }

    [Fact]
    public void Should_add_error_when_input_single_year_is_greater_than_valid_end_year()
    {
        // Arrange
        var invalidYear = $"{ValidEndYear + 1}";

        // Act
        _validator.ValidateYear(invalidYear, _errors, isTeamStanding: false);

        // Assert
        Assert.Single(_errors);
        Assert.Contains(ValidationMessage.InvalidSingleYearFilter(invalidYear,
            ValidStartYear, ValidEndYear), _errors);
    }

    [Fact]
    public void Should_add_error_when_input_range_year_start_has_non_numeric_values()
    {
        // Arrange
        var nonNumericYear = $"1x5x-{ValidEndYear}";

        // Act
        _validator.ValidateYear(nonNumericYear, _errors, isTeamStanding: false);

        // Assert
        Assert.Single(_errors);
        Assert.Contains(ValidationMessage.YearFilterNotNumber(nonNumericYear), _errors);
    }

    [Fact]
    public void Should_add_error_when_input_range_year_end_has_non_numeric_values()
    {
        // Arrange
        var nonNumericYear = $"{ValidStartYear}-20x0";

        // Act
        _validator.ValidateYear(nonNumericYear, _errors, isTeamStanding: false);

        // Assert
        Assert.Single(_errors);
        Assert.Contains(ValidationMessage.YearFilterNotNumber(nonNumericYear), _errors);
    }

    [Fact]
    public void Should_add_error_when_input_range_year_start_is_greater_than_end_year()
    {
        // Arrange
        var invalidYearRange = $"{ValidEndYear}-{ValidStartYear}";

        // Act
        _validator.ValidateYear(invalidYearRange, _errors, isTeamStanding: false);

        // Assert
        Assert.Single(_errors);

        var yearParts = invalidYearRange.Split("-");
        var startYear = int.Parse(yearParts[0]);
        var endYear = int.Parse(yearParts[1]);
        Assert.Contains(ValidationMessage.StartYearGreaterThanEndYear(startYear, endYear,
            ValidStartYear, ValidEndYear), _errors);
    }

    [Fact]
    public void Should_add_error_when_input_range_year_start_is_less_than_valid_start_year()
    {
        // Arrange
        var invalidYearRange = $"{ValidStartYear - 1}-{ValidEndYear}";

        // Act
        _validator.ValidateYear(invalidYearRange, _errors, isTeamStanding: false);

        // Assert
        Assert.Single(_errors);

        var yearParts = invalidYearRange.Split("-");
        var startYear = int.Parse(yearParts[0]);
        Assert.Contains(ValidationMessage.StartYearLessThanValidStartYear(startYear,
            ValidStartYear, ValidEndYear), _errors);
    }

    [Fact]
    public void Should_add_error_when_input_range_year_start_is_less_than_valid_start_year_for_team_standing()
    {
        // Arrange
        var invalidYearRange = $"{ValidTeamStandingStartYear - 1}-{ValidEndYear}";
        var isTeamStanding = true;

        // Act
        _validator.ValidateYear(invalidYearRange, _errors, isTeamStanding);

        // Assert
        Assert.Single(_errors);

        var yearParts = invalidYearRange.Split("-");
        var startYear = int.Parse(yearParts[0]);
        Assert.Contains(ValidationMessage.StartYearLessThanValidStartYear(startYear,
            ValidTeamStandingStartYear, ValidEndYear), _errors);
    }

    [Fact]
    public void Should_add_error_when_input_range_year_start_is_greater_than_valid_end_year()
    {
        // Arrange
        var invalidYearRange = $"{ValidEndYear + 1}-{ValidStartYear}";

        // Act
        _validator.ValidateYear(invalidYearRange, _errors, isTeamStanding: false);

        // Assert
        Assert.Equal(2, _errors.Count);

        var yearParts = invalidYearRange.Split("-");
        var startYear = int.Parse(yearParts[0]);
        var endYear = int.Parse(yearParts[1]);

        Assert.Contains(ValidationMessage.StartYearGreaterThanValidEndYear(startYear,
            ValidStartYear, ValidEndYear), _errors);
        Assert.Contains(ValidationMessage.StartYearGreaterThanEndYear(startYear, endYear,
            ValidStartYear, ValidEndYear), _errors);
    }

    [Fact]
    public void Should_add_error_when_input_range_year_end_is_greater_than_valid_end_year()
    {
        // Arrange
        var invalidYearRange = $"{ValidStartYear}-{ValidEndYear + 1}";

        // Act
        _validator.ValidateYear(invalidYearRange, _errors, isTeamStanding: false);

        // Assert
        Assert.Single(_errors);

        var yearParts = invalidYearRange.Split("-");
        var endYear = int.Parse(yearParts[1]);
        Assert.Contains(ValidationMessage.EndYearGreaterThanValidEndYear(endYear,
            ValidStartYear, ValidEndYear), _errors);
    }

    [Fact]
    public void Should_add_error_when_input_range_year_end_is_less_than_valid_start_year()
    {
        // Arrange
        var invalidYearRange = $"{ValidStartYear}-{ValidStartYear - 1}";

        // Act
        _validator.ValidateYear(invalidYearRange, _errors, isTeamStanding: false);

        // Assert
        Assert.Equal(2, _errors.Count);

        var yearParts = invalidYearRange.Split("-");
        var endYear = int.Parse(yearParts[1]);
        var startYear = int.Parse(yearParts[0]);

        Assert.Contains(ValidationMessage.EndYearLessThanValidStartYear(endYear,
            ValidStartYear, ValidEndYear), _errors);
        Assert.Contains(ValidationMessage.StartYearGreaterThanEndYear(startYear, endYear,
            ValidStartYear, ValidEndYear), _errors);
    }

    [Fact]
    public void Should_add_2_errors_when_input_range_year_is_longer_than_valid_range_on_both_sides()
    {
        // Arrange
        var tooLongYearRange = $"{ValidStartYear - 1}-{ValidEndYear + 1}";

        // Act
        _validator.ValidateYear(tooLongYearRange, _errors, isTeamStanding: false);

        // Assert
        Assert.Equal(2, _errors.Count);

        var yearParts = tooLongYearRange.Split("-");
        var startYear = int.Parse(yearParts[0]);
        var endYear = int.Parse(yearParts[1]);

        Assert.Contains(ValidationMessage.StartYearLessThanValidStartYear(startYear,
            ValidStartYear, ValidEndYear), _errors);
        Assert.Contains(ValidationMessage.EndYearGreaterThanValidEndYear(endYear,
            ValidStartYear, ValidEndYear), _errors);
    }
}