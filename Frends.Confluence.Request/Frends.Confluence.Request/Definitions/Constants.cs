using System.ComponentModel;

namespace Frends.Confluence.Request.Definitions;

/// <summary>
/// Constants used by the Confluence request task.
/// </summary>
public static class Constants
{
    /// <summary>
    /// HTTP method for the request.
    /// </summary>
    public enum HttpMethod
    {
        GET = 1,
        POST = 2,
        PUT = 3,
        PATCH = 4,
        DELETE = 5
    }

    /// <summary>
    /// Converts a Constants.HttpMethod to a System.Net.Http.HttpMethod.
    /// </summary>
    public static System.Net.Http.HttpMethod GetHttpMethod(HttpMethod method) =>
        method switch
        {
            HttpMethod.GET => System.Net.Http.HttpMethod.Get,
            HttpMethod.POST => System.Net.Http.HttpMethod.Post,
            HttpMethod.PUT => System.Net.Http.HttpMethod.Put,
            HttpMethod.PATCH => System.Net.Http.HttpMethod.Patch,
            HttpMethod.DELETE => System.Net.Http.HttpMethod.Delete,
            _ => throw new InvalidEnumArgumentException("This http method is not supported")
        };

    /// <summary>
    /// Confluence API version.
    /// </summary>
    public enum ApiVersion
    {
        V1 = 1,
        V2 = 2
    }

    /// <summary>
    /// URI path for Confluence REST API v1.
    /// </summary>
    public const string ApiV1Uri = "/wiki/rest/api/";

    /// <summary>
    /// URI path for Confluence API v2.
    /// </summary>
    public const string ApiV2Uri = "/wiki/api/v2/";
}
