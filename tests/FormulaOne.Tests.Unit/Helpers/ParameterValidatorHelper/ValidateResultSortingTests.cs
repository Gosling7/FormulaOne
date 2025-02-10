namespace FormulaOne.Tests.Unit.Helpers.ParameterValidatorHelper;

public class ValidateResultSortingTests
{
    private readonly Application.Helpers.ParameterValidatorHelper _validator = new();
    private readonly List<string> _errors = [];

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
        _validator.ValidateResultSorting(nullSortField, nullSortOrder, _errors);
        _validator.ValidateResultSorting(emptySortField, emptySortOrder, _errors);
        _validator.ValidateResultSorting(whiteSpaceSortField, whiteSpaceSortOrder,
            _errors);

        // Assert
        Assert.Empty(_errors);
    }
}
