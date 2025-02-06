using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Helpers;
using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Application.Services;

internal class DriverService : IDriverService
{
    private readonly IQueryDriverParameterValidator _validator;
    private readonly IRaceResultRepository _raceResultRepository;
    private readonly IDriverRepository _driverRepository;
    private readonly IDriverStandingRepository _driverStandingRepository;
    private readonly ServiceHelper _serviceHelper;

    public DriverService(IQueryDriverParameterValidator validator,
        IRaceResultRepository raceResultRepository,
        IDriverRepository driverRepository,
        IDriverStandingRepository driverStandingRepository,
        ServiceHelper getDriversHelper)
    {
        _raceResultRepository = raceResultRepository;
        _driverRepository = driverRepository;
        _validator = validator;
        _driverStandingRepository = driverStandingRepository;
        _serviceHelper = getDriversHelper;
    }

    public async Task<PagedResult<DriverDto>> GetDrivers(GetDriversParameter parameters)
    {
        return await _serviceHelper
            .ValidateAndReturnPagedResult<DriverDto, GetDriversParameter>(
                parameters: parameters,
                fetchDataAsync: (param) => _driverRepository.GetItemsAsync(param),
                validateQueryParmeters: (param) => _validator.Validate(param));
    }

    public async Task<PagedResult<DriverStandingDto>> GetDriverStandings(
        GetDriverStandingsParameter parameters)
    {
        return await _serviceHelper
            .ValidateAndReturnPagedResult<DriverStandingDto, GetDriverStandingsParameter>(
                parameters: parameters,
                fetchDataAsync: (param) => _driverStandingRepository.GetItemsAsync(param),
                validateQueryParmeters: (param) => _validator.Validate(param));
    }

    public async Task<PagedResult<RaceResultDto>> GetDriverResults(
        GetDriverResultsParameter parameters)
    {
        return await _serviceHelper
            .ValidateAndReturnPagedResult<RaceResultDto, GetDriverResultsParameter>(
                parameters: parameters,
                fetchDataAsync: (param) => _raceResultRepository.GetItemsAsync(param),
                validateQueryParmeters: (param) => _validator.Validate(param));
    }
}