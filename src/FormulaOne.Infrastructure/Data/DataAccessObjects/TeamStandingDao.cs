namespace FormulaOne.Infrastructure.Data.DataAccessObjects;

public class TeamStandingDao
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public int Position { get; set; }
    public Guid TeamId { get; set; }
    public TeamDao Team { get; set; }
    public float Points { get; set; }
}