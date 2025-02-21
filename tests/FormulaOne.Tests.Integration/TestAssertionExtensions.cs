using FormulaOne.Application.DataTransferObjects;
using Shouldly;

namespace FormulaOne.Tests.Integration;

internal static class TestAssertionExtensions
{
    public static void ShouldContainAll(this IEnumerable<CircuitDto> actualCircuits,
        IEnumerable<CircuitDto> expectedCircuits)
    {
        foreach (var expectedCircuit in expectedCircuits)
        {
            actualCircuits.ShouldContain(expectedCircuit);
        }
    }

    public static void ShouldContainAll(this IEnumerable<TeamDto> actualTeams,
        IEnumerable<TeamDto> expectedTeams)
    {
        foreach (var expectedTeam in expectedTeams)
        {
            actualTeams.ShouldContain(expectedTeam);
        }
    }

    public static void ShouldContainAll(this IEnumerable<DriverDto> actualDrivers,
        IEnumerable<DriverDto> expectedDrivers)
    {
        foreach (var expectedDriver in expectedDrivers)
        {
            actualDrivers.ShouldContain(expectedDriver);
        }
    }

    public static void ShouldContainAll(this IEnumerable<DriverStandingDto> actualDriverStandings,
        IEnumerable<DriverStandingDto> expectedDriverStandings)
    {
        foreach (var expectedDriverStanding in expectedDriverStandings)
        {
            actualDriverStandings.ShouldContain(expectedDriverStanding);
        }
    }

    public static void ShouldContainAll(this IEnumerable<TeamStandingDto> actualTeamStandings,
        IEnumerable<TeamStandingDto> expectedTeamStandings)
    {
        foreach (var expectedTeamStanding in expectedTeamStandings)
        {
            actualTeamStandings.ShouldContain(expectedTeamStanding);
        }
    }
}
