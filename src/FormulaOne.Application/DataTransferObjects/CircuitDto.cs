﻿namespace FormulaOne.Application.DataTransferObjects;

public record CircuitDto
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Location { get; init; }
}
