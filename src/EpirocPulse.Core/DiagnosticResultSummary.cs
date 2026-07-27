namespace EpirocPulse.Core;

/// <summary>
/// Represents summary statistics for a collection of diagnostic results.
/// </summary>
/// <remarks>
/// This model aggregates diagnostic results to provide quick insight into overall test status.
/// It counts total checks, passed, warnings, failed, and skipped results.
/// </remarks>
public class DiagnosticResultSummary
{
    /// <summary>
    /// Gets or sets the total number of diagnostic checks.
    /// </summary>
    public int TotalChecks { get; set; }

    /// <summary>
    /// Gets or sets the number of passed checks.
    /// </summary>
    public int Passed { get; set; }

    /// <summary>
    /// Gets or sets the number of warning checks.
    /// </summary>
    public int Warnings { get; set; }

    /// <summary>
    /// Gets or sets the number of failed checks.
    /// </summary>
    public int Failed { get; set; }

    /// <summary>
    /// Gets or sets the number of skipped checks.
    /// </summary>
    public int Skipped { get; set; }

    /// <summary>
    /// Creates a new DiagnosticResultSummary from a collection of diagnostic results.
    /// </summary>
    /// <param name="results">The collection of diagnostic results to summarize.</param>
    /// <returns>A new DiagnosticResultSummary with aggregated statistics.</returns>
    public static DiagnosticResultSummary FromResults(IEnumerable<DiagnosticResult> results)
    {
        var resultList = results.ToList();
        
        return new DiagnosticResultSummary
        {
            TotalChecks = resultList.Count,
            Passed = resultList.Count(r => r.Status == DiagnosticStatus.Pass),
            Warnings = resultList.Count(r => r.Status == DiagnosticStatus.Warning),
            Failed = resultList.Count(r => r.Status == DiagnosticStatus.Fail),
            Skipped = resultList.Count(r => r.Status == DiagnosticStatus.Skipped)
        };
    }
}
