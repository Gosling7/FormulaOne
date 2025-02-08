namespace FormulaOne.Application.DataTransferObjects;

public record RaceResultDto(
    string Id,
    int Position,
    DateOnly Date,
    string CircuitId,
    string CircuitName,
    string DriverId,
    string DriverName,
    string? TeamId,
    string? TeamName,
    int Laps,
    string? Time,
    float Points);
