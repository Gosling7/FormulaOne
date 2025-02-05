using FormulaOne.Application.DataTransferObjects;
using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Parameters;

namespace FormulaOne.Application.Services;

internal class DriverService : IDriverService
{
    private readonly IQueryDriverParameterValidator _validator;
    private readonly IRaceResultRepository _raceResultRepository;
    private readonly IDriverRepository _driverRepository;
    //private readonly IDriverStandingRepository _driverStandingRepository;

    public DriverService(IQueryDriverParameterValidator validator,
        IRaceResultRepository raceResultRepository,
        IDriverRepository driverRepository
        )
    {
        _raceResultRepository = raceResultRepository;
        _driverRepository = driverRepository;
                _validator = validator;
    }

    public async Task<PagedResult<DriverDto>> GetDrivers(GetDriversParameter parameters)
    {
        var errors = new List<string>();

        errors = _validator.Validate(parameters);
        if (errors.Count > 0)
        {
            return new PagedResult<DriverDto>(
                CurrentPage: parameters.Page,
                TotalPages: 0,
                PageSize: parameters.PageSize,
                TotalResults: 0,
                Errors: errors,
                Items: new List<DriverDto>());
        }

        var driversWithCount = await _driverRepository.GetDriversAsync(parameters);
        var driverCount = driversWithCount.Item1;
        var drivers = driversWithCount.Item2;
        var totalPages = MathF.Ceiling(Convert.ToSingle(driverCount) / parameters.PageSize);

        return new PagedResult<DriverDto>(
            CurrentPage: parameters.Page,
            TotalPages: (int)totalPages,
            PageSize: parameters.PageSize,
            TotalResults: driverCount,
            Errors: errors,
            Items: drivers);
    }

    public async Task<PagedResult<DriverStandingDto>> GetDriverStandings(
        GetDriverStandingsParameter parameters)
    {
        var errors = new List<string>();

        errors = _validator.Validate(parameters);
        if (errors.Count > 0)
        {
            return new PagedResult<DriverStandingDto>(
                CurrentPage: parameters.Page,
                TotalPages: 0,
                PageSize: parameters.PageSize,
                TotalResults: 0,
                Errors: errors,
                Items: new List<DriverStandingDto>());
        }

        //var driverStandingsWithCount = await _driverStandingRepository.GetDriverStandings(parameters);
        //var driverStandingCount = driverStandingsWithCount.Item1;
        //var driverStandings = driverStandingsWithCount.Item2;
        //var totalPages = MathF.Ceiling(Convert.ToSingle(driverStandingCount) / parameters.PageSize);

        return new PagedResult<DriverStandingDto>(
            CurrentPage: parameters.Page,
            TotalPages: 0,
            PageSize: 0,
            TotalResults: 0,
            Errors: errors,
            Items: new List<DriverStandingDto>());
    }

    public async Task<PagedResult<RaceResultDto>> GetDriverResults(
        GetDriverResultsParameter parameters)
    {
        var errors = new List<string>();

        errors = _validator.Validate(parameters);
        if (errors.Count > 0)
        {
            return new PagedResult<RaceResultDto>(
                CurrentPage: parameters.Page,
                TotalPages: 0,
                PageSize: parameters.PageSize,
                TotalResults: 0,
                Errors: errors,
                Items: new List<RaceResultDto>());
        }

        var raceResultsWithCount = await _raceResultRepository.GetDriversRaceResultsAsync(parameters);
        var raceResultCount = raceResultsWithCount.Item1;
        var raceResults = raceResultsWithCount.Item2;
        var totalPages = MathF.Ceiling(Convert.ToSingle(raceResultCount) / parameters.PageSize);

        return new PagedResult<RaceResultDto>(
            CurrentPage: parameters.Page,
            TotalPages: (int)totalPages,
            PageSize: parameters.PageSize,
            TotalResults: raceResultCount,
            Errors: errors,
            Items: raceResults);
    }
}
