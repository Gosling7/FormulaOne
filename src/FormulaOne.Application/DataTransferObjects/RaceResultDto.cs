namespace FormulaOne.Application.DataTransferObjects;

public record RaceResultDto
{
    public required string Id { get; init; }
    public required int Position { get; init; }
    public required DateOnly Date { get; init; }
    public required string CircuitId { get; init; }
    public required string CircuitName { get; init; }
    public required string DriverId { get; init; }
    public required string DriverName { get; init; }
    public string? TeamId { get; init; } 
    public string? TeamName { get; init; } 
    public required int Laps { get; init; }
    public string? Time { get; init; } 
    public required float Points { get; init; }
}