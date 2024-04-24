using System.Text.Json.Serialization;

namespace ZooExpoOrg.Web.Services.Accounts;

public class RegisterError
{
    public bool Successful { get; set; }

    [JsonPropertyName("fieldName")]
    public string FieldName { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }
}

