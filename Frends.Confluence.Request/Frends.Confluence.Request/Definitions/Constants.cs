using System.ComponentModel;

namespace Frends.Confluence.Request.Definitions;

#pragma warning disable CS1591 // self explanatory
public static class Constants
{
    public enum HttpMethod
    {
        GET = 1,
        POST = 2,
        PUT = 3,
        PATCH = 4,
        DELELTE = 5
    }

    public static System.Net.Http.HttpMethod GetHttpMethod(HttpMethod method) =>
        method switch
        {
            HttpMethod.GET => System.Net.Http.HttpMethod.Get,
            HttpMethod.POST => System.Net.Http.HttpMethod.Post,
            HttpMethod.PUT => System.Net.Http.HttpMethod.Put,
            HttpMethod.PATCH => System.Net.Http.HttpMethod.Patch,
            HttpMethod.DELELTE => System.Net.Http.HttpMethod.Delete,
            _ => throw new InvalidEnumArgumentException("This http method is not supported")
        };

    public enum ApiVersion
    {
        V1 = 1,
        V2 = 2
    }

    public const string ApiV1Uri = "/wiki/rest/api/";
    public const string ApiV2Uri = "/wiki/api/v2/";
}
#pragma warning restore CS1591 // self explanatory
