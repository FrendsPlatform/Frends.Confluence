using System;

namespace Frends.Confluence.Request.Definitions;

/// <summary>
/// Error details returned when the task fails.
/// </summary>
public class Error
{
    /// <summary>
    /// Error message describing the failure.
    /// </summary>
    /// <example>Request to Confluence API failed with status 401</example>
    public string Message { get; set; }

    /// <summary>
    /// Additional information about the error, typically the original exception.
    /// </summary>
    /// <example>null</example>
    public Exception AdditionalInfo { get; set; }
}
