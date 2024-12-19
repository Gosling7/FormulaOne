namespace FormulaOne.Domain.Entities;

public class DriverStanding
{
    public string Id { get; private set; }
    public int Position { get; private set; }
    public Driver Driver { get; private set; }
    public Team Team { get; private set; }
    public float Points { get; private set; }
    public int Year { get; private set; }
}