namespace FormulaOne.Core.Entities;

public class DriverStanding
{
    public Guid Id { get; private set; }
    public int Position { get; private set; }
    public Driver Driver { get; private set; }
    public Team Team { get; private set; }
    public float Points { get; private set; }
    public int Year { get; private set; }

    public DriverStanding(Guid id, int position, Driver driver, Team team, 
        float points, int year)
    {
        Id = id;
        Position = position;
        Driver = driver;
        Team = team;
        Points = points;
        Year = year;
    }

    // For integration tests.
    private DriverStanding() 
    { 
    }
}