using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Helpers;
using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;
using FormulaOne.Infrastructure.Repositories;

namespace FormulaOne.Application.Services;

internal class CircuitService : ICircuitService
{
    private readonly IQueryCircuitParameterValidator _validator;
    private readonly IRaceResultRepository _raceResultRepository;
    private readonly ICircuitRepository _circuitRepository;
    private readonly ServiceHelper _serviceHelper;

    public CircuitService(IQueryCircuitParameterValidator validator,
        IRaceResultRepository raceResultRepository,
        IDriverRepository driverRepository,
        IDriverStandingRepository driverStandingRepository,
        ICircuitRepository circuitRepository,
        ServiceHelper getDriversHelper)
    {
        _raceResultRepository = raceResultRepository;
        _validator = validator;
        _circuitRepository = circuitRepository;
        _serviceHelper = getDriversHelper;
    }

    public async Task<PagedResult<CircuitDto>> GetCircuits(GetCircuitsParameter parameters)
    {
        return await _serviceHelper
            .ValidateAndReturnPagedResult<CircuitDto, GetCircuitsParameter>(
                parameters: parameters,
                fetchDataAsync: (param) => _circuitRepository.GetItemsAsync(param),
                validateQueryParmeters: (param) => _validator.Validate(param));
    }

    public async Task<PagedResult<RaceResultDto>> GetCircuitResults(
        GetCircuitResultsParameter parameters)
    {
        return await _serviceHelper
            .ValidateAndReturnPagedResult<RaceResultDto, GetCircuitResultsParameter>(
                parameters: parameters,
                fetchDataAsync: (param) => _raceResultRepository.GetItemsAsync(param),
                validateQueryParmeters: (param) => _validator.Validate(param));
    }
}
