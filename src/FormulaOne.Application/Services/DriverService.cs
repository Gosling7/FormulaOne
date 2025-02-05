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
                fetchDataAsync: (param) => _driverRepository.GetDriversAsync(param),
                validateQueryParmeters: (param) => _validator.Validate(param));
    }

    public async Task<PagedResult<DriverStandingDto>> GetDriverStandings(
        GetDriverStandingsParameter parameters)
    {
        return await _serviceHelper
            .ValidateAndReturnPagedResult<DriverStandingDto, GetDriverStandingsParameter>(
                parameters: parameters,
                fetchDataAsync: (param) => _driverStandingRepository.GetDriverStandings(param),
                validateQueryParmeters: (param) => _validator.Validate(param));
    }

    public async Task<PagedResult<RaceResultDto>> GetDriverResults(
        GetDriverResultsParameter parameters)
    {
        return await _serviceHelper
            .ValidateAndReturnPagedResult<RaceResultDto, GetDriverResultsParameter>(
                parameters: parameters,
                fetchDataAsync: (param) => _raceResultRepository.GetRaceResultsAsync(param),
                validateQueryParmeters: (param) => _validator.Validate(param));
    }
}



//public async Task<PagedResult<DriverDto>> GetDrivers(GetDriversParameter parameters)
//    {
//        var errors = new List<string>();

//        errors = _validator.Validate(parameters);
//        if (errors.Count > 0)
//        {
//            return new PagedResult<DriverDto>(
//                CurrentPage: parameters.Page,
//                TotalPages: 0,
//                PageSize: parameters.PageSize,
//                TotalResults: 0,
//                Errors: errors,
//                Items: new List<DriverDto>());
//        }

//        var driversWithCount = await _driverRepository.GetDriversAsync(parameters);
//        var driverCount = driversWithCount.Item1;
//        var drivers = driversWithCount.Item2;
//        var totalPages = MathF.Ceiling(Convert.ToSingle(driverCount) / parameters.PageSize);

//        return new PagedResult<DriverDto>(
//            CurrentPage: parameters.Page,
//            TotalPages: (int)totalPages,
//            PageSize: parameters.PageSize,
//            TotalResults: driverCount,
//            Errors: errors,
//            Items: drivers);
//    }
//public async Task<PagedResult<DriverStandingDto>> GetDriverStandings(
//        GetDriverStandingsParameter parameters)
//    {
//        var errors = new List<string>();

//        errors = _validator.Validate(parameters);
//        if (errors.Count > 0)
//        {
//            return new PagedResult<DriverStandingDto>(
//                CurrentPage: parameters.Page,
//                TotalPages: 0,
//                PageSize: parameters.PageSize,
//                TotalResults: 0,
//                Errors: errors,
//                Items: new List<DriverStandingDto>());
//        }

//        var driverStandingsWithCount = await _driverStandingRepository.GetDriverStandings(parameters);
//        var driverStandingCount = driverStandingsWithCount.Item1;
//        var driverStandings = driverStandingsWithCount.Item2;
//        var totalPages = MathF.Ceiling(Convert.ToSingle(driverStandingCount) / parameters.PageSize);

//        return new PagedResult<DriverStandingDto>(
//            CurrentPage: parameters.Page,
//            TotalPages: (int)totalPages,
//            PageSize: parameters.PageSize,
//            TotalResults: driverStandingCount,
//            Errors: errors,
//            Items: driverStandings);
//    }
//public async Task<PagedResult<RaceResultDto>> GetDriverResults(
//        GetDriverResultsParameter parameters)
//    {
//        var errors = new List<string>();

//        errors = _validator.Validate(parameters);
//        if (errors.Count > 0)
//        {
//            return new PagedResult<RaceResultDto>(
//                CurrentPage: parameters.Page,
//                TotalPages: 0,
//                PageSize: parameters.PageSize,
//                TotalResults: 0,
//                Errors: errors,
//                Items: new List<RaceResultDto>());
//        }

//        var raceResultsWithCount = await _raceResultRepository.GetDriversRaceResultsAsync(parameters);
//        var raceResultCount = raceResultsWithCount.Item1;
//        var raceResults = raceResultsWithCount.Item2;
//        var totalPages = MathF.Ceiling(Convert.ToSingle(raceResultCount) / parameters.PageSize);

//        return new PagedResult<RaceResultDto>(
//            CurrentPage: parameters.Page,
//            TotalPages: (int)totalPages,
//            PageSize: parameters.PageSize,
//            TotalResults: raceResultCount,
//            Errors: errors,
//            Items: raceResults);
//    }