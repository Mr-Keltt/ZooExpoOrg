using System.Text.Json.Serialization;
using ZooExpoOrg.Web.Shared.Interfaces;

namespace ZooExpoOrg.Web.Services.Accounts;

public class RegisterResult : RequestResult
{
    [JsonPropertyName("fieldErrors")]
    public List<RegisterError> FieldErrors { get; set; }
}

//{
//"code":"","message":"","fieldErrors":
//    [
//        {"code":"","fieldName":"email","message":"Email is required."},
//        { "code":"","fieldName":"password","message":"Password is required."},
//        { "code":"","fieldName":"userName","message":"User name is required."}
//    ]
//}