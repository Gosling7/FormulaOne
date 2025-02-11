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
}
