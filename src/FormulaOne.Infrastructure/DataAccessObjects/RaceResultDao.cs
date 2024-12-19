namespace FormulaOne.Infrastructure.DataAccessObjects;

public class RaceResultDao
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public int Position { get; set; }
    public Guid CircuitId { get; set; }
    public CircuitDao Circuit { get; set; }
    public DateOnly Date { get; set; }
    public Guid DriverId { get; set; }
    public DriverDao Driver { get; set; }
    public Guid TeamId { get; set; }
    public TeamDao Team { get; set; }
    public int Laps { get; set; }
    public TimeSpan Time { get; set; }
    public float Points { get; set; }
}