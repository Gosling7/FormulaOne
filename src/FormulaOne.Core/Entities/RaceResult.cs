namespace FormulaOne.Core.Entities;

public class RaceResult
{
    public Guid Id { get; private set; }
    public int Position { get; private set; }
    public Guid CircuitId { get; private set; }
    public Circuit Circuit { get; private set; }
    public DateOnly Date { get; private set; }
    public Guid DriverId { get; private set; }
    public Driver Driver { get; private set; }
    public Guid? TeamId { get; private set; }
    public Team? Team { get; private set; }
    public int Laps { get; private set; }
    public string? Time { get; private set; }
    public float Points { get; private set; }
}