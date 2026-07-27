namespace EpirocPulse.Core;

/// <summary>
/// Represents the structured result of a diagnostic check.
/// </summary>
/// <remarks>
/// A diagnostic result contains all information needed to present diagnostic feedback to a technician:
/// - The status of the check
/// - A brief, technician-friendly summary
/// - Detailed technical information
/// - Recommended next steps
/// - Optional raw output for advanced troubleshooting
/// - A timestamp indicating when the diagnostic was performed
/// </remarks>
public class DiagnosticResult
{
    /// <summary>
    /// Gets or sets the status of the diagnostic check.
    /// </summary>
    /// <remarks>
    /// This determines the visual indicator (green, yellow, red, blue, or grey).
    /// </remarks>
    public DiagnosticStatus Status { get; set; }

    /// <summary>
    /// Gets or sets a brief, technician-friendly summary of the diagnostic result.
    /// </summary>
    /// <remarks>
    /// This should be clear and actionable for field technicians.
    /// Examples: "Network adapter connected", "Gateway unreachable", "Unexpected APIPA address detected".
    /// </remarks>
    public string Summary { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the detailed technical information about the diagnostic result.
    /// </summary>
    /// <remarks>
    /// This provides in-depth information suitable for support teams or advanced troubleshooting.
    /// Examples: IP addresses, subnet masks, specific error codes, system configurations.
    /// </remarks>
    public string TechnicalDetails { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the suggested action or next step for the technician.
    /// </summary>
    /// <remarks>
    /// This should guide the technician on what to do next based on this result.
    /// Examples: "Verify network cable connection", "Check DHCP server", "Restart machine".
    /// </remarks>
    public string SuggestedAction { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the timestamp when the diagnostic was performed.
    /// </summary>
    /// <remarks>
    /// Always in UTC for consistency and reproducibility.
    /// </remarks>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets optional raw output from the diagnostic operation.
    /// </summary>
    /// <remarks>
    /// This may contain unprocessed command output, API responses, or other raw data.
    /// Useful for export, logging, and advanced analysis.
    /// Can be null if no raw output is available or applicable.
    /// </remarks>
    public string? RawOutput { get; set; }

    /// <summary>
    /// Creates a new DiagnosticResult with the specified properties.
    /// </summary>
    /// <param name="status">The status of the diagnostic check.</param>
    /// <param name="summary">A brief, technician-friendly summary.</param>
    /// <param name="technicalDetails">Detailed technical information.</param>
    /// <param name="suggestedAction">Suggested next step for the technician.</param>
    /// <param name="timestamp">The timestamp when the diagnostic was performed.</param>
    /// <param name="rawOutput">Optional raw output from the diagnostic operation.</param>
    public DiagnosticResult(
        DiagnosticStatus status,
        string summary,
        string technicalDetails,
        string suggestedAction,
        DateTime timestamp,
        string? rawOutput = null)
    {
        Status = status;
        Summary = summary;
        TechnicalDetails = technicalDetails;
        SuggestedAction = suggestedAction;
        Timestamp = timestamp;
        RawOutput = rawOutput;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DiagnosticResult"/> class.
    /// </summary>
    /// <remarks>
    /// This constructor is provided for deserialization and other scenarios where object initialization
    /// through the constructor is not possible.
    /// </remarks>
    public DiagnosticResult()
    {
        Status = DiagnosticStatus.Info;
        Summary = string.Empty;
        TechnicalDetails = string.Empty;
        SuggestedAction = string.Empty;
        Timestamp = DateTime.UtcNow;
    }
}
