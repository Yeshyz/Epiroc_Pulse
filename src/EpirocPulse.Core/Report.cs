namespace EpirocPulse.Core;

/// <summary>
/// Represents a complete diagnostic report.
/// </summary>
/// <remarks>
/// A diagnostic report contains header information, device details, diagnostic results, and summary statistics.
/// It is designed to be human-readable for technicians and support staff.
/// </remarks>
public class Report
{
    /// <summary>
    /// Gets or sets the report title.
    /// </summary>
    /// <remarks>
    /// Example: "Network Diagnostics Report", "Machine Connectivity Report", etc.
    /// </remarks>
    public string Title { get; set; } = "Diagnostic Report";

    /// <summary>
    /// Gets or sets the date and time when the report was generated.
    /// </summary>
    /// <remarks>
    /// Always in UTC for consistency across time zones.
    /// </remarks>
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the version of Epiroc Pulse that generated the report.
    /// </summary>
    /// <remarks>
    /// Example: "1.0.0", "0.1.0-alpha", etc.
    /// </remarks>
    public string ApplicationVersion { get; set; } = "1.0.0";

    /// <summary>
    /// Gets or sets the device information at the time of report generation.
    /// </summary>
    public DeviceInfo DeviceInfo { get; set; } = new();

    /// <summary>
    /// Gets or sets the collection of diagnostic results included in the report.
    /// </summary>
    public List<DiagnosticResult> DiagnosticResults { get; set; } = new();

    /// <summary>
    /// Gets or sets the summary statistics for the diagnostic results.
    /// </summary>
    /// <remarks>
    /// This is typically computed from DiagnosticResults when needed.
    /// </remarks>
    public DiagnosticResultSummary Summary
    {
        get => DiagnosticResultSummary.FromResults(DiagnosticResults);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Report"/> class.
    /// </summary>
    public Report()
    {
    }

    /// <summary>
    /// Creates a new Report with the specified properties.
    /// </summary>
    /// <param name="title">The report title.</param>
    /// <param name="applicationVersion">The version of Epiroc Pulse.</param>
    /// <param name="deviceInfo">The device information.</param>
    public Report(string title, string applicationVersion, DeviceInfo deviceInfo)
    {
        Title = title;
        ApplicationVersion = applicationVersion;
        DeviceInfo = deviceInfo;
        GeneratedAt = DateTime.UtcNow;
    }
}
