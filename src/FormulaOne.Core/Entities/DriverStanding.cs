namespace FormulaOne.Core.Entities;

public class DriverStanding
{
    public Guid Id { get; set; }
    public int Position { get; set; }
    public Driver Driver { get; set; }
    public Team Team { get; set; }
    public float Points { get; set; }
    public int Year { get; set; }

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

    public DriverStanding() 
    { 
    }
}