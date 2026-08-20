using System;
using System.Threading;
using System.Threading.Tasks;
using Frends.Confluence.Request.Definitions;
using static Frends.Confluence.Request.Definitions.Constants;

namespace Frends.Confluence.Request.Tests;

[TestClass]
public class ErrorHandlerTests
{
    private const string CustomErrorMessage = "CustomErrorMessage";

    private static Input InvalidInput() =>
        new Input
        {
            Username = "invalid",
            ApiToken = "invalid",
            HttpMethod = Constants.HttpMethod.GET,
            ApiVersion = ApiVersion.V1,
            ConfluenceDomainName = "nonexistent-domain-that-does-not-exist",
            OperationSufix = "/audit",
        };

    [TestMethod]
    public async Task Should_Throw_Error_When_ThrowErrorOnFailure_Is_True()
    {
        var options = new Options { ThrowErrorOnFailure = true };
        var ex = await Assert.ThrowsExceptionAsync<Exception>(
            () => Confluence.Request(InvalidInput(), options, CancellationToken.None));
        Assert.IsNotNull(ex);
    }

    [TestMethod]
    public async Task Should_Return_Failed_Result_When_ThrowErrorOnFailure_Is_False()
    {
        var options = new Options { ThrowErrorOnFailure = false };
        var result = await Confluence.Request(InvalidInput(), options, CancellationToken.None);
        Assert.IsFalse(result.Success);
        Assert.IsNotNull(result.Error);
    }

    [TestMethod]
    public async Task Should_Use_Custom_ErrorMessageOnFailure()
    {
        var options = new Options { ThrowErrorOnFailure = true, ErrorMessageOnFailure = CustomErrorMessage };
        var ex = await Assert.ThrowsExceptionAsync<Exception>(
            () => Confluence.Request(InvalidInput(), options, CancellationToken.None));
        Assert.IsNotNull(ex);
        Assert.IsTrue(ex.Message.Contains(CustomErrorMessage));
    }
}
