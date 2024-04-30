namespace ZooExpoOrg.Web.Services.GetIdHelper;

public interface IGetIdHelperService
{
    Task<GetIdResult> GetAdminId();

    Task<GetIdResult> GetCurrentUserId(CancellationToken cancellationToken = default);

    Task<GetIdResult> GetCurrentClientId();
}
