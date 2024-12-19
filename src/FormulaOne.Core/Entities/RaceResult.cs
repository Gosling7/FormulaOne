using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.Domain.Entities;

public class RaceResult
{
    public int RaceResultId { get; private set; }
    public int Year { get; private set; }
    public int Position { get; private set; }
    public int CircuitId { get; private set; }
    //public Circuit Circuit { get; private set; }
    public DateOnly Date { get; private set; }
    public int DriverId { get; private set; }
    public Driver Driver { get; private set; }
    public int TeamId { get; private set; }
    public Team Team { get; private set; }
    public int Laps { get; private set; }
    public string Time { get; private set; }
    public float Points { get; private set; }
}