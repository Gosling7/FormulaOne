﻿using FormulaOne.Application.Helpers;

namespace FormulaOne.Tests.Unit.Helpers.ParameterValidatorHelperTests;

public class ValidatePagination
{
    private readonly ParameterValidatorHelper _validator = new();
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
        Assert.Equal($"Invalid page number: {pageNumber}. " +
            $"Valid values are greater than 0.", _errors.First());
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
        Assert.Equal($"Invalid page number: {pageNumber}. " +
            $"Valid values are greater than 0.", _errors.First());
    }
}
