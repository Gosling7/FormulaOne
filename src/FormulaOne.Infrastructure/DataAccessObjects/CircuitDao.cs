namespace FormulaOne.Infrastructure.DataAccessObjects;

public class CircuitDao
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public IEnumerable<RaceResultDao> RaceResults { get; set; }
}