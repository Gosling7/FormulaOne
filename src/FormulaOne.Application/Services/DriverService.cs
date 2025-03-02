using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Helpers;
using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Application.Services;

public class DriverService : IDriverService
{
    private readonly IQueryDriverParameterValidator _validator;
    private readonly IRaceResultRepository _raceResultRepository;
    private readonly IDriverRepository _driverRepository;
    private readonly IDriverStandingRepository _driverStandingRepository;
    private readonly PagedQueryHelper _serviceHelper;

    public DriverService(IQueryDriverParameterValidator validator,
        IDriverRepository driverRepository,
        IDriverStandingRepository driverStandingRepository,
        IRaceResultRepository raceResultRepository,
        PagedQueryHelper serviceHelper)
    {
        _validator = validator;
        _driverRepository = driverRepository;
        _driverStandingRepository = driverStandingRepository;
        _raceResultRepository = raceResultRepository;
        _serviceHelper = serviceHelper;
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