using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.Application.Parameters;

internal record GetTeamStandingsParameters(
    string Id,
    int Year,
    int Page,
    int PageSize,
    int MaxPageSize,
    string NameFilter,
    string Sort);