using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooExpoOrg.Context.Entities;

namespace ZooExpoOrg.Services.JwtParse;

public interface IJwtParseService
{
    ClientEntity GetRequestClient(string jwtToken);
}
