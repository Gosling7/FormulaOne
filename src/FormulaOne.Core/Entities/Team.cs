﻿namespace FormulaOne.Core.Entities;

public class Team
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    public ICollection<TeamStanding> TeamStandings { get; private set; } = [];
    public ICollection<RaceResult> RaceResults { get; private set; } = [];
}
