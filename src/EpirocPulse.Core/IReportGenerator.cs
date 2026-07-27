namespace EpirocPulse.Core;

/// <summary>
/// Defines the contract for generating diagnostic reports.
/// </summary>
/// <remarks>
/// Implementations should provide different output formats (Markdown, HTML, PDF, etc.).
/// </remarks>
public interface IReportGenerator
{
    /// <summary>
    /// Generates a report from the provided diagnostic data.
    /// </summary>
    /// <param name="report">The report data to generate.</param>
    /// <returns>The generated report content as a string.</returns>
    string GenerateReport(Report report);
}
