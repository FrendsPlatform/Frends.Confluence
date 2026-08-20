namespace Frends.Confluence.Request.Definitions;

/// <summary>
/// Result of the Confluence request.
/// </summary>
public class Result
{
    /// <summary>
    /// Indicates whether the operation completed successfully.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; set; }

    /// <summary>
    /// Error details. Null when Success is true.
    /// </summary>
    /// <example>null</example>
    public Error Error { get; set; }

    /// <summary>
    /// Returned status code.
    /// </summary>
    /// <example>200</example>
    public int StatusCode { get; set; }

    /// <summary>
    /// Body of the response represented as JToken.
    /// </summary>
    /// <example>{ "id": 123 }</example>
    public dynamic Content { get; set; }
}
