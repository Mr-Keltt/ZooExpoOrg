using System.Text.Json.Serialization;

namespace ZooExpoOrg.Web.Services.GetRsultHelper;

public class ManageModelResult<T> : RequestResult
{
    public List<ManageModelError> FieldErrors { get; set; }
}