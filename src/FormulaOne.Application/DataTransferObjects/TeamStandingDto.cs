namespace FormulaOne.Application.DataTransferObjects;

public record TeamStandingDto(
    string Id,
    int Year,
    int Position,
    string TeamId,
    string TeamName,
    float Points);