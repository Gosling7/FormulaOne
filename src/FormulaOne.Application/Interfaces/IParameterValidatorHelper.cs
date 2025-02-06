namespace FormulaOne.Application.Interfaces;

internal interface IParameterValidatorHelper
{
    void ValidateId(string? idParameter, List<string> errors, string? nameOfIdParameter = "Id");
    void ValidatePagination(int pageParameter, List<string> errors);
    void ValidateResultSorting(string? fieldParameter, string? orderParameter, List<string> errors);
    void ValidateSorting(string? fieldParameter, string? orderParameter, List<string> errors);
    void ValidateYear(string? yearString, List<string> errors, bool isTeamStanding = false);
}