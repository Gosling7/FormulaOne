﻿using Microsoft.Extensions.DependencyInjection;

namespace FormulaOne.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainLayer(this IServiceCollection services)
    {
        return services;
    }
}
