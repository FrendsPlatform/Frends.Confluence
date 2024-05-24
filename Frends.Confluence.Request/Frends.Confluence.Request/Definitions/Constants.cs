namespace Frends.Confluence.Request.Definitions;

#pragma warning disable CS1591 // self explanatory
public static class Constants
{
    public enum ApiVersion
    {
        V1 = 1,
        V2 = 2
    }

    public const string ApiV1Uri = "/wiki/rest/api/";
    public const string ApiV2Uri = "/wiki/api/v2/";
}
#pragma warning restore CS1591 // self explanatory
