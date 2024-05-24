using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using static Frends.Confluence.Request.Definitions.Constants;

namespace Frends.Confluence.Request.Definitions;

/// <summary>
/// Input parameters.
/// </summary>
public class Input
{
    /// <summary>
    /// Username used to connect to confluence with Basic Auth.
    /// </summary>
    /// <example>john.doe@email.com</example>
    public string Username { get; init; }

    /// <summary>
    /// ApiToken used to connect to confluence with Basic Auth.
    /// </summary>
    /// <example>AKxDuOPvOWAb2g1-HtJmeqnlcztevlXMqDGtsNURL88=2C9A9D12</example>
    [PasswordPropertyText]
    public string ApiToken { get; init; }

    /// <summary>
    /// Define method of request.
    /// </summary>
    /// <example>HttpMethod.Get</example>
    public Constants.HttpMethod HttpMethod { get; init; }

    /// <summary>
    /// Api version that will be used in a request.
    /// </summary>
    /// <example>ApiVersion.V1</example>
    public ApiVersion ApiVersion { get; init; }

    /// <summary>
    /// Confluence name
    /// </summary>
    /// <example>example-name</example>
    public string ConfluenceDomainName { get; init; }

    /// <summary>
    /// Url suffix from of operation. All operations are described in Confluence Rest API documentation.
    /// </summary>
    /// <example>/audit/export</example>
    public string OperationSufix { get; init; }

    /// <summary>
    /// Body content of a request if needed in a JsonF format.
    /// </summary>
    [UIHint(nameof(HttpMethod), "", Constants.HttpMethod.POST)]
    [UIHint(nameof(HttpMethod), "", Constants.HttpMethod.PUT)]
    [UIHint(nameof(HttpMethod), "", Constants.HttpMethod.PATCH)]
    public string JsonBody { get; init; } = string.Empty;

    /// <summary>
    /// Query parameters from which query string will be constructed.
    /// </summary>
    /// <example>{ {page, 1}, {limit, 5} }</example>
    public Dictionary<string, string> QueryParameters { get; init; } =
        new Dictionary<string, string>();
}
