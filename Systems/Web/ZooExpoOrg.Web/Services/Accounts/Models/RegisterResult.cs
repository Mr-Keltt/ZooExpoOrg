using System.Text.Json.Serialization;

namespace ZooExpoOrg.Web.Services.Accounts;

public class RegisterResult
{
    public bool Successful { get; set; }

    [JsonPropertyName("fieldErrors")]
    public List<RegisterError> Errors { get; set; }
}

//{
//"code":"","message":"","fieldErrors":
//    [
//        {"code":"","fieldName":"email","message":"Email is required."},
//        { "code":"","fieldName":"password","message":"Password is required."},
//        { "code":"","fieldName":"userName","message":"User name is required."}
//    ]
//}