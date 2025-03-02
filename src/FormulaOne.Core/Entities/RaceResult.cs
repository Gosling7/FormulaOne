namespace FormulaOne.Core.Entities;

public class RaceResult
{
    public Guid Id { get; set; }
    public int Position { get; set; }
    public Guid CircuitId { get; set; }
    public Circuit Circuit { get; set; }
    public DateOnly Date { get; set; }
    public Guid DriverId { get; set; }
    public Driver Driver { get; set; }
    public Guid? TeamId { get; set; }
    public Team? Team { get; set; }
    public int Laps { get; set; }
    public string? Time { get; set; }
    public float Points { get; set; }

    public RaceResult(Guid id, int position, Circuit circuit, DateOnly date, 
        Driver driver, Team? team, int laps, string? time, float points)
    {
        Id = id;
        Position = position;
        Circuit = circuit;
        Date = date;
        Driver = driver;
        Team = team;
        Laps = laps;
        Time = time;
        Points = points;
    }

    public RaceResult()
    {        
    }
}