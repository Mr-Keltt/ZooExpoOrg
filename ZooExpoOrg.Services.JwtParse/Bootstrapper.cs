using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooExpoOrg.Services.JwtParse.JwtParse;

namespace ZooExpoOrg.Services.JwtParse;

public static class Bootstrapper
{
    public static IServiceCollection AddAppLogger(this IServiceCollection services)
    {
        return services
            .AddSingleton<IJwtParseService, JwtParseService>();
    }
}
