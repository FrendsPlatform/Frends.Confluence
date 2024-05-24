using System.Net;

namespace Frends.Confluence.Request.Definitions;

#pragma warning disable CS1591 // self explanatory
public class Result
{
    public HttpStatusCode StatusCode { get; init; }
    public string Content { get; init; }
}
#pragma warning restore CS1591 // self explanatory
