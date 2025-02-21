namespace FormulaOne.Core.Entities;

public class TeamStanding
{
    public Guid Id { get; private set; }
    public int Year { get; private set; }
    public int Position { get; private set; }
    public float Points { get; private set; }
    public Guid TeamId { get; private set; }
    public Team Team { get; private set; }

    public TeamStanding(Guid id, int year, int position, float points, Team team)
    {
        Id = id;
        Year = year;
        Position = position;
        Points = points;
        Team = team;
    }

    public TeamStanding()
    {        
    }
}
