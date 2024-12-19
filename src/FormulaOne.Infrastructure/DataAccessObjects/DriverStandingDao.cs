namespace FormulaOne.Infrastructure.DataAccessObjects;

public class DriverStandingDao
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public int Position { get; set; }
    public Guid DriverId { get; set; }
    public DriverDao Driver { get; set; }
    public Guid TeamId { get; set; }
    public TeamDao Team { get; set; }
    public float Points { get; set; }
}
