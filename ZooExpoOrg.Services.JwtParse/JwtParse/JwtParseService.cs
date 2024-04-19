using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooExpoOrg.Context;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Services.JwtParse;

public class JwtParseService : IJwtParseService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;

    public JwtParseService(IDbContextFactory<MainDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public ClientEntity GetRequestClient(string jwtToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = tokenHandler.ReadToken(jwtToken) as JwtSecurityToken;

        var userId = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;


    }
}
