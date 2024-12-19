using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.Application.Parameters
{
    internal record GetTeamResultsParameters(
        string Id,
        int Year,
        string Session,
        int Page,
        int PageSize,
        int MaxPageSize,
        string NameFilter,
        string Sort);
}
