using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using static Frends.Confluence.Request.Definitions.Constants;

namespace Frends.Confluence.Request.Definitions;

/// <summary>
/// Represents different methods for interacting with Confluence.
/// </summary>
public enum ConfluenceMethod
{
    /// <summary>
    /// Generic request to Confluence.
    /// </summary>
    CustomRequest,

    /// <summary>
    /// Create a new page in Confluence.
    /// </summary>
    CreatePage,

    /// <summary>
    /// Update an existing page in Confluence.
    /// </summary>
    UpdatePage,

    /// <summary>
    /// Get a page from Confluence by its ID.
    /// </summary>
    GetPageById,

    /// <summary>
    /// Delete a page from Confluence by its ID.
    /// </summary>
    DeletePage,

    /// <summary>
    /// Return page ID f its title.
    /// </summary>
    GetPageByTitle,

    /// <summary>
    /// Create a new space in Confluence.
    /// </summary>
    CreateSpace,

    /// <summary>
    /// Delete a space in Confluence.
    /// </summary>
    DeleteSpace,

    /// <summary>
    /// Get a space in Confluence by its name.
    /// </summary>
    GetSpaceByName
}

/// <summary>
/// Input parameters.
/// </summary>
public class Input
{
    /// <summary>
    /// Chosen method for interacting with Confluence.
    /// </summary>
    public ConfluenceMethod ConfluenceMethod { get; set; }

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
    [UIHint(nameof(ConfluenceMethod), "", ConfluenceMethod.CustomRequest)]
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
    [UIHint(nameof(ConfluenceMethod), "", ConfluenceMethod.CustomRequest)]
    public string OperationSufix { get; init; }

    /// <summary>
    /// Body content of a request if needed in a JsonF format.
    /// </summary>
    [UIHint(nameof(HttpMethod), "", Constants.HttpMethod.POST, Constants.HttpMethod.PUT, Constants.HttpMethod.PATCH)]
    public string JsonBody { get; init; } = string.Empty;

    /// <summary>
    /// Query parameters from which query string will be constructed.
    /// </summary>
    /// <example>{ {page, 1}, {limit, 5} }</example>
    [UIHint(nameof(ConfluenceMethod), "", ConfluenceMethod.CustomRequest)]
    public Dictionary<string, string> QueryParameters { get; init; } =
        new Dictionary<string, string>();

    /// <summary>
    /// The space key where the page will be created or updated.
    /// </summary>
    /// <example>SPACEKEY</example>
    [UIHint(nameof(ConfluenceMethod), "", ConfluenceMethod.CreateSpace, ConfluenceMethod.DeleteSpace)]
    public string SpaceKey { get; init; }

    /// <summary>
    /// The title of the page.
    /// </summary>
    /// <example>My Page Title</example>
    [UIHint(nameof(ConfluenceMethod), "", ConfluenceMethod.CreatePage, ConfluenceMethod.UpdatePage, ConfluenceMethod.GetPageByTitle)]
    public string Title { get; init; }

    /// <summary>
    /// The body content of the page in storage format.
    /// </summary>
    /// <example>&lt;p&gt;This is a page body&lt;/p&gt;</example>
    [UIHint(nameof(ConfluenceMethod), "", ConfluenceMethod.CreatePage, ConfluenceMethod.UpdatePage)]
    public string Body { get; init; }

    /// <summary>
    /// The ID of the page to update or delete.
    /// </summary>
    /// <example>123456</example>
    [UIHint(nameof(ConfluenceMethod), "", ConfluenceMethod.UpdatePage, ConfluenceMethod.DeletePage, ConfluenceMethod.GetPageById)]
    public string PageId { get; init; }

    /// <summary>
    /// The new version number of the page for updates.
    /// </summary>
    /// <example>2</example>
    [UIHint(nameof(ConfluenceMethod), "", ConfluenceMethod.UpdatePage)]
    public int Version { get; init; }

    /// <summary>
    /// The space name for Confluence Space.
    /// </summary>
    /// <example>2</example>
    [UIHint(nameof(ConfluenceMethod), "", ConfluenceMethod.CreateSpace, ConfluenceMethod.GetSpaceByName)]
    public string SpaceName { get; init; }

    /// <summary>
    /// The ID of Confluence Space.
    /// </summary>
    /// <example>2</example>
    [UIHint(nameof(ConfluenceMethod), "", ConfluenceMethod.CreatePage)]
    public string SpaceId { get; init; }

}
