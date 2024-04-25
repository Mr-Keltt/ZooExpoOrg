namespace ZooExpoOrg.Web.Services.GetIdHelper;

public class GetIdResult
{
    public bool Successful { get; set; }

    public Guid Id { get; set; }

    public string ErrorMesage { get; set; }
}
