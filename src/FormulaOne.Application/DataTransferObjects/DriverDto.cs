namespace FormulaOne.Application.DataTransferObjects;

public record DriverDto
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string? Nationality { get; init; }
}
   