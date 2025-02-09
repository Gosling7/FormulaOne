namespace FormulaOne.Application.DataTransferObjects;

public record TeamStandingDto
{
    public required string Id { get; init; }
    public required int Year { get; init; }
    public required int Position { get; init; }
    public required string TeamId { get; init; }
    public required string TeamName { get; init; }
    public required float Points { get; init; }
}

//public record TeamStandingDto(
//    string Id,
//    int Year,
//    int Position,
//    string TeamId,
//    string TeamName,
//    float Points);