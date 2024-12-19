using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.Domain.Entities;

public class TeamStanding
{
    public int TeamStandingId { get; private set; }
    public int Year { get; private set; }
    public int Position { get; private set; }
    public int TeamId { get; private set; }
    public Team Team { get; private set; }
    public float Points { get; private set; }
}
