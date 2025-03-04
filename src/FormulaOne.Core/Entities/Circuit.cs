﻿namespace FormulaOne.Core.Entities;

public class Circuit
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Location { get; private set; } = string.Empty;

    public ICollection<RaceResult> RaceResults { get; private set; } = [];

    public Circuit(Guid id, string name, string location)
    {
        Id = id;
        Name = name;
        Location = location;
    }
}
