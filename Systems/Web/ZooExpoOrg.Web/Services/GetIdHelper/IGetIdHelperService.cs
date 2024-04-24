namespace ZooExpoOrg.Web.Services.GetIdHelper;

public interface IGetIdHelperService
{
    Task<Guid> GetUserId(CancellationToken cancellationToken = default);

    Task<Guid> GetClientId(CancellationToken cancellationToken = default);
}
