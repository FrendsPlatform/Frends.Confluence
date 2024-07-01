using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Frends.Confluence.Request.Definitions;
using static System.Net.Mime.MediaTypeNames;
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
                HttpMethod = Constants.HttpMethod.GET,
                ApiVersion = ApiVersion.V1,
                ConfluenceDomainName = domainName,
                OperationSufix = "/audit"
            },
            CancellationToken.None
        );
        Assert.AreEqual(200, result.StatusCode);
    }

    [TestMethod]
    public async Task RequestSimpleGetV2()
    {
        var result = await Confluence.Request(
            new Input
            {
                Username = username,
                ApiToken = apiToken,
                HttpMethod = Constants.HttpMethod.GET,
                ApiVersion = ApiVersion.V2,
                ConfluenceDomainName = domainName,
                OperationSufix = "/pages"
            },
            CancellationToken.None
        );
        Assert.AreEqual(200, result.StatusCode);
    }

    [TestMethod]
    public async Task AcceptPrecedingSlash()
    {
        var resultWithSlash = await Confluence.Request(
            new Input
            {
                Username = username,
                ApiToken = apiToken,
                HttpMethod = Constants.HttpMethod.GET,
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
                HttpMethod = Constants.HttpMethod.GET,
                ApiVersion = ApiVersion.V2,
                ConfluenceDomainName = domainName,
                OperationSufix = "pages"
            },
            CancellationToken.None
        );
        Assert.AreEqual(200, resultWithoutSlash.StatusCode);
        Assert.AreEqual(200, resultWithSlash.StatusCode);
    }

    [TestMethod]
    public async Task RequestWithQueryParameters()
    {
        var OnePageResult = await Confluence.Request(
            new Input
            {
                Username = username,
                ApiToken = apiToken,
                HttpMethod = Constants.HttpMethod.GET,
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
                HttpMethod = Constants.HttpMethod.GET,
                ApiVersion = ApiVersion.V2,
                ConfluenceDomainName = domainName,
                OperationSufix = "/pages",
            },
            CancellationToken.None
        );
        Assert.AreEqual(200, OnePageResult.StatusCode);
        Assert.AreEqual(200, ManyPageResult.StatusCode);
        Assert.IsTrue(
            OnePageResult.Content.ToString().Length < ManyPageResult.Content.ToString().Length
        );
    }

    [TestMethod]
    public async Task RequestWithPathParameters()
    {
        var result = await Confluence.Request(
            new Input
            {
                Username = username,
                ApiToken = apiToken,
                HttpMethod = Constants.HttpMethod.GET,
                ApiVersion = ApiVersion.V2,
                ConfluenceDomainName = domainName,
                OperationSufix = $"/spaces/{WorkSpaceId}",
            },
            CancellationToken.None
        );
        Assert.AreEqual(200, result.StatusCode);
    }

    [TestMethod]
    public async Task RequestWithBody()
    {
        var result = await Confluence.Request(
            new Input
            {
                Username = username,
                ApiToken = apiToken,
                HttpMethod = Constants.HttpMethod.POST,
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
        Assert.AreEqual(200, result.StatusCode);
    }

    [TestMethod]
    public async Task CreatePage()
    {
        var result = await Confluence.CreatePage(
            new Input
            {
                ConfluenceMethod = ConfluenceMethod.CreatePage,
                Username = username,
                ApiToken = apiToken,
                ApiVersion = ApiVersion.V2,
                ConfluenceDomainName = domainName,
                SpaceId = WorkSpaceId,
                Title = "TestPage3",
                Body = "This is a new page 3 created by the API"
            },
            CancellationToken.None
        );
        Assert.AreEqual(200, result.StatusCode);
    }

    [TestMethod]
    public async Task UpdatePage()
    {
        var result = await Confluence.UpdatePage(
            new Input
            {
                Title = "UpdatedTitle",
                PageId = PageId,
                Username = username,
                ApiToken = apiToken,
                ApiVersion = ApiVersion.V2,
                ConfluenceDomainName = domainName,
                Body = "Updated new page created by the API",
                Version = 2
            },
            CancellationToken.None
        );
        Assert.AreEqual(200, result.StatusCode);
    }

    [TestMethod]
    public async Task GetPageById()
    {
        var result = await Confluence.GetPageById(
            new Input
            {
                PageId = PageId,
                Username = username,
                ApiToken = apiToken,
                ApiVersion = ApiVersion.V2,
                ConfluenceDomainName = domainName
            },
            CancellationToken.None
        );
        Assert.AreEqual(200, result.StatusCode);
    }

    [TestMethod]
    public async Task DeletePage()
    {
        var result = await Confluence.DeletePage(
            new Input
            {
                PageId = PageId,
                Username = username,
                ApiToken = apiToken,
                ApiVersion = ApiVersion.V2,
                ConfluenceDomainName = domainName
            },
            CancellationToken.None
        );
        Assert.AreEqual(204, result.StatusCode);
    }

    [TestMethod]
    public async Task GetPageByTitle()
    {
        var result = await Confluence.GetPageByTitle(
            new Input
            {
                Title = "TestPage1",
                SpaceKey = "TEST",
                Username = username,
                ApiToken = apiToken,
                ApiVersion = ApiVersion.V2,
                ConfluenceDomainName = domainName
            },
            CancellationToken.None
        );
        Assert.AreEqual(200, result.StatusCode);
    }

    [TestMethod]
    public async Task CreateSpace()
    {
        var input = new Input
        {
            SpaceName = "Test123",
            SpaceKey = "TEST2",
            Username = username,
            ApiToken = apiToken,
            ApiVersion = ApiVersion.V1,
            ConfluenceDomainName = domainName
        };
        await Confluence.DeleteSpace(input, CancellationToken.None);
        var result = await Confluence.CreateSpace(input, CancellationToken.None);

        Assert.AreEqual(200, result.StatusCode);

        await Confluence.DeleteSpace(input, CancellationToken.None);
    }

    [TestMethod]
    public async Task DeleteSpace()
    {
        var input = new Input
        {
            SpaceKey = "TEST",
            Username = username,
            ApiToken = apiToken,
            ApiVersion = ApiVersion.V2,
            ConfluenceDomainName = domainName
        };
        var result = await Confluence.DeleteSpace(input, CancellationToken.None);

        Assert.AreEqual(202, result.StatusCode);
    }

    [TestMethod]
    public async Task GetSpaceByName()
    {
        var input = new Input
        {
            SpaceName = "Test123",
            Username = username,
            ApiToken = apiToken,
            ApiVersion = ApiVersion.V2,
            ConfluenceDomainName = domainName
        };
        await Confluence.CreateSpace(input, CancellationToken.None);
        var result = await Confluence.GetSpaceByName(input, CancellationToken.None);
        Assert.AreEqual(200, result.StatusCode);
    }
}
