namespace FormulaOne.Core.Entities;

public class TeamStanding
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public int Position { get; set; }
    public float Points { get; set; }
    public Guid TeamId { get; set; }
    public Team Team { get; set; }

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
