﻿using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Application.Interfaces;

public interface IDriverStandingRepository
{
    Task<(int, IEnumerable<DriverStandingDto>)> GetDriverStandings(GetDriverStandingsParameter parameters);
}
