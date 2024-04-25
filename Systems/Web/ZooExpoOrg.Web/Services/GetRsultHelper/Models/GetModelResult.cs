using ZooExpoOrg.Web.Services.GetRsultHelper.Models;

namespace ZooExpoOrg.Web.Services.GetRsultHelper;

public class GetModelResult<T> : RequestResult
{
    public T Result { get; set; }
}
