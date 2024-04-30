namespace ZooExpoOrg.Common.Helpers;

public static class DateHelper
{
    public static DateTime ConvertToUTC(DateTime date)
    {
        if (date.Kind != DateTimeKind.Utc)
        {
            return date.ToUniversalTime();
        }

        return date;
    }
}
