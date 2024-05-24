using System;
using System.IO;
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
    private static readonly string SpaceKey = "TEST";

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
        var guid = Guid.NewGuid();
        var name = $"Test-{guid}";
        var response = await Confluence.Request(
            new Input
            {
                Username = username,
                ApiToken = apiToken,
                HttpMethod = HttpMethod.Post,
                ApiVersion = ApiVersion.V1,
                ConfluenceDomainName = domainName,
                OperationSufix = "/space",
                JsonBody =
                    $@"{{
  ""key"": ""{SpaceKey}"",
  ""name"": ""Test-{name}""
}}"
            },
            CancellationToken.None
        );

        var json = JObject.Parse(response.Content);
        return json["id"].ToString();
    }

    private static async Task DeleteSpace()
    {
        await Confluence.Request(
            new Input
            {
                Username = username,
                ApiToken = apiToken,
                HttpMethod = HttpMethod.Delete,
                ApiVersion = ApiVersion.V1,
                ConfluenceDomainName = domainName,
                OperationSufix = $"/space/{SpaceKey}",
            },
            CancellationToken.None
        );
    }

    private static async Task CreatePage(string spaceId, string pageName)
    {
        await Confluence.Request(
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
  ""spaceId"": ""{SpaceKey}"",
  ""status"": ""current"",
    ""spaceId"": ""Test-Page-{Guid.NewGuid()}""

}}"
            },
            CancellationToken.None
        );
    }
}
