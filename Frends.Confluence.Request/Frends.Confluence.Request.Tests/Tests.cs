using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Frends.Confluence.Request.Definitions;
using static Frends.Confluence.Request.Definitions.Constants;

namespace Frends.Confluence.Request.Tests;

[TestClass]
public class Tests : TestsBase
{
    [TestMethod]
    public async Task RequestSimpleGetV1()
    {
        var result = await Confluence.Request(
            new Input
            {
                Username = username,
                ApiToken = apiToken,
                HttpMethod = HttpMethod.Get,
                ApiVersion = ApiVersion.V1,
                ConfluenceDomainName = domainName,
                OperationSufix = "/audit"
            },
            CancellationToken.None
        );
        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
    }

    [TestMethod]
    public async Task RequestSimpleGetV2()
    {
        var result = await Confluence.Request(
            new Input
            {
                Username = username,
                ApiToken = apiToken,
                HttpMethod = HttpMethod.Get,
                ApiVersion = ApiVersion.V2,
                ConfluenceDomainName = domainName,
                OperationSufix = "/pages"
            },
            CancellationToken.None
        );
        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
    }

    [TestMethod]
    public async Task AcceptPrecedingSlash()
    {
        var resultWithSlash = await Confluence.Request(
            new Input
            {
                Username = username,
                ApiToken = apiToken,
                HttpMethod = HttpMethod.Get,
                ApiVersion = ApiVersion.V2,
                ConfluenceDomainName = domainName,
                OperationSufix = "/pages"
            },
            CancellationToken.None
        );

        var resultWithoutSlash = await Confluence.Request(
            new Input
            {
                Username = username,
                ApiToken = apiToken,
                HttpMethod = HttpMethod.Get,
                ApiVersion = ApiVersion.V2,
                ConfluenceDomainName = domainName,
                OperationSufix = "pages"
            },
            CancellationToken.None
        );
        Assert.AreEqual(HttpStatusCode.OK, resultWithoutSlash.StatusCode);
        Assert.AreEqual(HttpStatusCode.OK, resultWithSlash.StatusCode);
    }

    [TestMethod]
    public async Task RequestWithQueryParameters()
    {
        var OnePageResult = await Confluence.Request(
            new Input
            {
                Username = username,
                ApiToken = apiToken,
                HttpMethod = HttpMethod.Get,
                ApiVersion = ApiVersion.V2,
                ConfluenceDomainName = domainName,
                OperationSufix = "/pages",
                QueryParameters = new Dictionary<string, string> { { "limit", "1" } }
            },
            CancellationToken.None
        );

        var ManyPageResult = await Confluence.Request(
            new Input
            {
                Username = username,
                ApiToken = apiToken,
                HttpMethod = HttpMethod.Get,
                ApiVersion = ApiVersion.V2,
                ConfluenceDomainName = domainName,
                OperationSufix = "/pages",
            },
            CancellationToken.None
        );
        Assert.AreEqual(HttpStatusCode.OK, OnePageResult.StatusCode);
        Assert.AreEqual(HttpStatusCode.OK, ManyPageResult.StatusCode);
        Assert.IsTrue(OnePageResult.Content.Length < ManyPageResult.Content.Length);
    }

    [TestMethod]
    public async Task RequestWithPathParameters()
    {
        var result = await Confluence.Request(
            new Input
            {
                Username = username,
                ApiToken = apiToken,
                HttpMethod = HttpMethod.Get,
                ApiVersion = ApiVersion.V2,
                ConfluenceDomainName = domainName,
                OperationSufix = $"/spaces/{WorkSpaceId}",
            },
            CancellationToken.None
        );
        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
    }

    [TestMethod]
    public async Task RequestWithBody()
    {
        var result = await Confluence.Request(
            new Input
            {
                Username = username,
                ApiToken = apiToken,
                HttpMethod = HttpMethod.Post,
                ApiVersion = ApiVersion.V2,
                ConfluenceDomainName = domainName,
                OperationSufix = "/pages",
                JsonBody =
                    $@"{{
  ""spaceId"": ""{WorkSpaceId}"",
  ""status"": ""current"",
  ""title"": ""NewTestingPage-{Guid.NewGuid()}""
}}"
            },
            CancellationToken.None
        );
        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
    }
}
