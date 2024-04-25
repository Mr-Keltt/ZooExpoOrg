namespace ZooExpoOrg.Web.Services.GetIdHelper;

public interface IGetIdHelperService
{
    Task<GetIdResult> GetAdminId();

    Task<GetIdResult> GetUserId(CancellationToken cancellationToken = default);

    Task<GetIdResult> GetClientId();
}
