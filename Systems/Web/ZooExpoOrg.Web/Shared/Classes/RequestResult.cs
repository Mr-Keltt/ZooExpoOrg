namespace ZooExpoOrg.Web.Shared.Interfaces;

public abstract class RequestResult
{
    public bool Successful { get; set; }

    public string ErrorMesage { get; set; }
}
