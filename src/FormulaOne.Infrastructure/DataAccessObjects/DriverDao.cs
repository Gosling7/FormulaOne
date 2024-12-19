namespace FormulaOne.Infrastructure.DataAccessObjects;

public class DriverDao
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Nationality { get; set; }
    public IEnumerable<DriverStandingDao> DriverStandings { get; set; }
    public IEnumerable<RaceResultDao> RaceResults { get; set; }
}
