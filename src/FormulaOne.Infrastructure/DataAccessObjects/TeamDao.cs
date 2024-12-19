namespace FormulaOne.Infrastructure.DataAccessObjects;

public class TeamDao
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<TeamStandingDao> TeamStandings { get; set; }
    public IEnumerable<RaceResultDao> RaceResults { get; set; }
}