﻿namespace ZooExpoOrg.Common.Helpers;

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

public static class AutoMappersRegisterHelper
{
    public static void Register(IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(s => s.FullName != null && s.FullName.StartsWith("ZooExpoOrg."));

        services.AddAutoMapper(assemblies);
    }
}