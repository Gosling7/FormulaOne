namespace FormulaOne.Domain.Entities;

public class DriverStanding
{
    public string Id { get; set; }
    public int Position { get; set; }
    public Driver Driver { get; set; }
    public Team Team { get; set; }
    public float Points { get; set; }
    public int Year { get; set; }
}