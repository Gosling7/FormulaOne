namespace FormulaOne.Application.DataTransferObjects;

public record TeamDto
{
    public required string Id { get; init; }
    public required string Name { get; init; }
}
