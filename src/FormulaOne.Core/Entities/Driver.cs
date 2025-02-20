namespace FormulaOne.Core.Entities;

public class Driver
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string? Nationality { get; private set; }

    public ICollection<RaceResult> RaceResults { get; private set; } = [];

    public Driver(Guid id, string firstName, string lastName, string? nationality)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Nationality = nationality;
    }
}
