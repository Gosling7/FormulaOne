using FormulaOne.Application.Constants;

namespace FormulaOne.Tests.Unit.Helpers.ParameterValidatorHelper;

public class ValidateSorting
{
    private readonly Application.Helpers.ParameterValidatorHelper _validator = new();
    private readonly List<string> _errors = [];
    private readonly List<string> _validSortFields = ["validfield"];
    private readonly List<string> _validSortDirections =
    [ 
        ValidationConstant.AscendingSortDirection, 
        ValidationConstant.DescendingSortDirection
    ];

    [Fact]
    public void Should_not_add_errors_when_sort_field_and_sort_order_are_valid()
    {
        // Arrange
        var validSortField = _validSortFields.First();
        var validSortOrder = _validSortDirections.First();

        // Act
        _validator.ValidateSorting(validSortField, validSortOrder, _validSortFields, _errors);

        // Assert
        Assert.Empty(_errors);
    }

    [Fact]
    public void Should_not_add_errors_when_sort_field_is_valid_and_sort_order_is_not_given()
    {
        // Arrange
        var validSortField = _validSortFields.First();

        // Act
        _validator.ValidateSorting(validSortField, orderParameter: null, _validSortFields, _errors);

        // Assert
        Assert.Empty(_errors);
    }

    [Fact]
    public void Should_not_add_errors_when_sort_field_and_sort_order_are_null_or_whitespace()
    {
        // Arrange
        string? nullSortField = null;
        string? emptySortField = "";
        string? whiteSpaceSortField = " ";

        string? nullSortOrder = null;
        string? emptySortOrder = "";
        string? whiteSpaceSortOrder = " ";

        // Act
        _validator.ValidateSorting(nullSortField, nullSortOrder, _validSortFields, _errors);
        _validator.ValidateSorting(emptySortField, emptySortOrder, _validSortFields, _errors);
        _validator.ValidateSorting(whiteSpaceSortField, whiteSpaceSortOrder, _validSortFields, 
            _errors);

        // Assert
        Assert.Empty(_errors);
    }

    [Fact]
    public void Should_add_error_when_sort_field_is_invalid()
    {
        // Arrange
        var invalidSortField = "invalidfield";

        // Act
        _validator.ValidateSorting(invalidSortField, orderParameter: null, _validSortFields, _errors);

        // Assert
        Assert.Single(_errors);
        Assert.Contains(ValidationMessage.InvalidSortField(invalidSortField, _validSortFields),
            _errors);
    }

    [Fact]
    public void Should_add_error_when_order_valid_but_sort_field_is_invalid()
    {
        // Arrange
        var sortOrder = ValidationConstant.AscendingSortDirection;

        // Act
        _validator.ValidateSorting(fieldParameter: null, sortOrder, _validSortFields, _errors);

        // Assert
        Assert.Single(_errors);
        Assert.Contains(ValidationMessage.ExplicitSortingRequiresSortField(_validSortFields),
            _errors);
    }

    [Fact]
    public void Should_add_error_when_sort_order_is_invalid()
    {
        // Arrange
        var sortField = _validSortFields.First();
        var invalidSortOrder = "invalid sort order";

        // Act
        _validator.ValidateSorting(sortField, invalidSortOrder, _validSortFields, _errors);

        // Assert
        Assert.Single(_errors);
        Assert.Contains(ValidationMessage.InvalidSortOrder(invalidSortOrder, _validSortDirections),
            _errors);
    }
}
