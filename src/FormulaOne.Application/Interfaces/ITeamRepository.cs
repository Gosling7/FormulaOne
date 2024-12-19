using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Parameters;
using FormulaOne.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.Application.Interfaces;

public interface ITeamRepository
{
    Task<IEnumerable<Team>> GetTeamsAsync(GetTeamsParameters parameters);

    Task<int> GetTeamsCountAsync();

    Task<Team?> GetTeamById(Guid id);

    Task<IEnumerable<TeamStanding>> GetTeamStandings(
        int year);

    Task<IEnumerable<TeamStanding>> GetTeamAllStandingsByTeamId(
        Guid id);

    Task<IEnumerable<RaceResult>> GetTeamRaceResultsByYear(
        Guid id, int year);
}
