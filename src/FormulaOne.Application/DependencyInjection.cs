﻿using Microsoft.Extensions.DependencyInjection;

namespace FormulaOne.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        return services;
    }
}