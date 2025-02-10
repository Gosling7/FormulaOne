using FormulaOne.Application.Constants;

namespace FormulaOne.Tests.Unit.Helpers.ParameterValidatorHelper;

public class ValidatePaginationTests
{
    private readonly Application.Helpers.ParameterValidatorHelper _validator = new();
    private readonly List<string> _errors = [];

    [Fact]
    public void Should_not_add_errors_when_page_is_greater_than_0()
    {
        // Arrange
        var pageNumber = 1;

        // Act
        _validator.ValidatePagination(pageNumber, _errors);

        // Assert
        Assert.Empty(_errors);
    }

    [Fact]
    public void Should_add_errors_when_page_is_equal_to_0()
    {
        // Arrange
        var pageNumber = 0;

        // Act
        _validator.ValidatePagination(pageNumber, _errors);

        // Assert
        Assert.Single(_errors);
        Assert.Contains(ValidationMessage.InvalidPageNumber(pageNumber), _errors);
    }

    [Fact]
    public void Should_add_errors_when_page_is_less_than_0()
    {
        // Arrange
        var pageNumber = -1;

        // Act
        _validator.ValidatePagination(pageNumber, _errors);

        // Assert
        Assert.Single(_errors);
        Assert.Contains(ValidationMessage.InvalidPageNumber(pageNumber), _errors);
    }
}
