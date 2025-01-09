namespace FormulaOne.Application.DataTransferObjects;

public record RaceResultDto(
    string Id,
    int Position,
    DateOnly Date,
    string CircuitName,
    string DriverName,
    string? TeamName,
    int Laps,
    string? Time,
    float Points);
