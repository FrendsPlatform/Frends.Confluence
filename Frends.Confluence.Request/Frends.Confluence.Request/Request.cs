﻿using System;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Frends.Confluence.Request.Definitions;
using Newtonsoft.Json.Linq;
using static Frends.Confluence.Request.Definitions.Constants;

namespace Frends.Confluence.Request;

/// <summary>
/// Confluence Task.
/// </summary>
public static class Confluence
{
    /// <summary>
    /// Make a request to Confluence.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.Confluence.Request)
    /// </summary>
    /// <param name="input">Input parameters</param>
    /// <param name="token">Token generated by Frends to stop this task.</param>
    /// <returns>Object { bool IsSuccess, List&lt;string&gt; DeletedFiles, string ErrorMessage }</returns>
    public static async Task<Result> Request([PropertyTab] Input input, CancellationToken token)
    {
        var client = GetAuthorizedClient(input);
        var uri = GetFullUri(input);
        var content = new StringContent(input.JsonBody, Encoding.UTF8, "application/json");

        var message = new HttpRequestMessage
        {
            Method = input.HttpMethod,
            RequestUri = uri,
            Content = content,
        };
        var response = await client.SendAsync(message, token);
        var responseContent = await response.Content.ReadAsStringAsync(token);
        var responseBody = JToken.Parse(responseContent);

        return new Result { StatusCode = (int)response.StatusCode, Content = responseBody };
    }

    private static HttpClient GetAuthorizedClient(Input input)
    {
        var client = new HttpClient();
        var authenticationString = $"{input.Username}:{input.ApiToken}";
        var base64EncodedAuthenticationString = Convert.ToBase64String(
            Encoding.ASCII.GetBytes(authenticationString)
        );
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Basic",
            base64EncodedAuthenticationString
        );
        return client;
    }

    private static Uri GetFullUri(Input input)
    {
        var versionPart = input.ApiVersion switch
        {
            ApiVersion.V1 => ApiV1Uri,
            ApiVersion.V2 => ApiV2Uri,
            _ => throw new InvalidEnumArgumentException(),
        };
        var baseUri = new Uri(
            $"https://{input.ConfluenceDomainName}.atlassian.net{versionPart}{input.OperationSufix.TrimStart('/')}"
        );

        var uriBuilder = new UriBuilder(baseUri);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        foreach (var param in input.QueryParameters)
        {
            query.Add(param.Key, param.Value);
        }
        uriBuilder.Query = query.ToString();
        return uriBuilder.Uri;
    }
}
