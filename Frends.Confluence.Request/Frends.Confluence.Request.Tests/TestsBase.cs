using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using dotenv.net;
using Frends.Confluence.Request.Definitions;
using Newtonsoft.Json.Linq;
using static Frends.Confluence.Request.Definitions.Constants;

namespace Frends.Confluence.Request.Tests;

[TestClass]
public abstract class TestsBase
{
    protected static readonly string username = Environment.GetEnvironmentVariable(
        "CONFLUENCE_USERNAME"
    );
    protected static readonly string apiToken = Environment.GetEnvironmentVariable(
        "CONFLUENCE_API_TOKEN"
    );
    protected static readonly string domainName = Environment.GetEnvironmentVariable(
        "CONFLUENCE_DOMAIN_NAME"
    );

    protected static string WorkSpaceId { get; set; }
    private static readonly string WorkSpaceKey = "TEST";
    internal static string PageId { get; set; }

    [AssemblyInitialize]
    public static async Task AssemblyInit(TestContext context)
    {
        //load envs
        var root = Directory.GetCurrentDirectory();
        string projDir = Directory.GetParent(root).Parent.Parent.FullName;
        DotEnv.Load(
            options: new DotEnvOptions(
                envFilePaths: new[] { $"{projDir}{Path.DirectorySeparatorChar}.env.local" }
            )
        );

        await DeleteSpace();
        WorkSpaceId = await CreateSpace();
        await CreatePage(WorkSpaceId, "TestPage1");
        await CreatePage(WorkSpaceId, "TestPage2");
    }

    [AssemblyCleanup]
    public static async Task AssemblyCleanup()
    {
        await DeleteSpace();
    }

    private static async Task<string> CreateSpace()
    {
        var response = await Confluence.Request(
            new Input
            {
                Username = username,
                ApiToken = apiToken,
                HttpMethod = Constants.HttpMethod.POST,
                ApiVersion = ApiVersion.V1,
                ConfluenceDomainName = domainName,
                OperationSufix = "/space",
                JsonBody =
                    $@"{{
    ""key"": ""{WorkSpaceKey}"",
    ""name"": ""Test-{Guid.NewGuid()}""
}}"
            },
            CancellationToken.None
        );

        JToken jToken = response.Content;
        return jToken["id"].ToString();
    }


    private static async Task DeleteSpace()
    {
        await Confluence.Request(
            new Input
            {
                Username = username,
                ApiToken = apiToken,
                HttpMethod = Constants.HttpMethod.DELETE,
                ApiVersion = ApiVersion.V1,
                ConfluenceDomainName = domainName,
                OperationSufix = $"/space/{WorkSpaceKey}",
            },
            CancellationToken.None
        );
    }

    private static async Task CreatePage(string spaceId, string pageName)
    {
        var result = await Confluence.Request(
            new Input
            {
                ConfluenceMethod = ConfluenceMethod.CreatePage,
                Username = username,
                ApiToken = apiToken,
                ApiVersion = ApiVersion.V2,
                ConfluenceDomainName = domainName,
                SpaceId = spaceId,
                Title = pageName,
                Body = "This is a new page created by the API"
            },
            CancellationToken.None
        );
        if (result.StatusCode == 200 && result.Content is JObject jsonResponse && PageId == null)
        {
            PageId = jsonResponse["id"]?.ToString();
        }
    }
}
