namespace ZooExpoOrg.Web.Services.GetRsultHelper;

public class ManageModelError 
{
    public bool Successful { get; set; }

    public string PropertyName { get; set; }

    public string ErrorMessage { get; set; }


    public string Message
    {
        get
        {
            return ErrorMessage;
        }
        set
        {
            ErrorMessage = value;
        }
    }
}

