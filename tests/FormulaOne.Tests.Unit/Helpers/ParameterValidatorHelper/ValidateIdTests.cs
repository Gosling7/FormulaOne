using FormulaOne.Application.Constants;

namespace FormulaOne.Tests.Unit.Helpers.ParameterValidatorHelper;

public class ValidateIdTests
{
    private readonly Application.Helpers.ParameterValidatorHelper _validator = new();
    private readonly List<string> _errors = [];
    private const string IdParameterName = "Id";

    [Fact]
    public void Should_not_add_errors_when_id_is_valid()
    {
        // Arrange
        var validId = Guid.NewGuid().ToString();

        // Act
        _validator.ValidateId(validId, _errors);

        // Assert
        Assert.Empty(_errors);
    }

    [Fact]
    public void Should_not_add_errors_when_multiple_ids_are_valid()
    {
        // Arrange
        var validIds = $"{Guid.NewGuid()},{Guid.NewGuid()}";

        // Act
        _validator.ValidateId(validIds, _errors);

        // Assert
        Assert.Empty(_errors);
    }

    [Fact]
    public void Should_not_add_errors_when_id_is_null_or_whitespace()
    {
        // Arrange
        var whitespaceOnlyId = " ";
        var emptyId = string.Empty;
        string? nullId = null;

        // Act
        _validator.ValidateId(whitespaceOnlyId, _errors);
        _validator.ValidateId(emptyId, _errors);
        _validator.ValidateId(nullId, _errors);

        // Assert
        Assert.Empty(_errors);
    }

    [Fact]
    public void Should_add_errors_when_multiple_ids_are_invalid()
    {
        // Arrange
        var invalidId1 = "0";
        var invalidId2 = "a";
        var invalidIds = $"{invalidId1},{invalidId2}";

        // Act
        _validator.ValidateId(invalidIds, _errors);

        // Assert
        Assert.Equal(2, _errors.Count);
        Assert.Contains(ValidationMessage.InvalidGuid(IdParameterName, invalidId1), _errors);
        Assert.Contains(ValidationMessage.InvalidGuid(IdParameterName, invalidId2), _errors);
    }

    [Fact]
    public void Should_add_error_when_given_multiple_ids_one_id_is_invalid()
    {
        // Arrange
        var validId = Guid.NewGuid().ToString();
        var invalidId = "a";
        var invalidIds = $"{validId},{invalidId}";

        // Act
        _validator.ValidateId(invalidIds, _errors);

        // Assert
        Assert.Single(_errors);
        Assert.Contains(ValidationMessage.InvalidGuid(IdParameterName, invalidId), _errors);
    }

    [Fact]
    public void Should_add_error_with_correct_id_parameter_name_when_given_multiple_ids()
    {
        // Arrange
        var parameter1 = new { DriverId = "a" };
        var parameter2 = new { DriverId = Guid.NewGuid().ToString() };
        var mixedIds = $"{parameter1.DriverId},{parameter2.DriverId}";
        var parameterName = nameof(parameter1.DriverId);
        var invalidId = parameter1.DriverId;

        // Act
        _validator.ValidateId(mixedIds, _errors, parameterName);

        // Assert
        Assert.Single(_errors);
        Assert.Contains(ValidationMessage.InvalidGuid(parameterName, invalidId), _errors);
    }
}