using EpirocPulse.Core;
using System.Text;

namespace EpirocPulse.Reporting;

/// <summary>
/// Generates diagnostic reports in Markdown format.
/// </summary>
/// <remarks>
/// This implementation creates technician-friendly Markdown reports with clear sections for
/// headers, device information, diagnostics, summaries, and raw output appendix.
/// </remarks>
public class MarkdownReportGenerator : IReportGenerator
{
    /// <summary>
    /// Generates a Markdown report from the provided diagnostic data.
    /// </summary>
    /// <param name="report">The report data to generate.</param>
    /// <returns>The generated report content as Markdown.</returns>
    public string GenerateReport(Report report)
    {
        ArgumentNullException.ThrowIfNull(report);

        var sb = new StringBuilder();

        // Generate header section
        GenerateHeader(sb, report);

        // Generate device information section
        GenerateDeviceInformation(sb, report.DeviceInfo);

        // Generate diagnostics section
        GenerateDiagnosticsSection(sb, report.DiagnosticResults);

        // Generate summary section
        GenerateSummarySection(sb, report.Summary);

        // Generate appendix with raw output
        GenerateAppendix(sb, report.DiagnosticResults);

        return sb.ToString();
    }

    /// <summary>
    /// Generates the report header section.
    /// </summary>
    private static void GenerateHeader(StringBuilder sb, Report report)
    {
        sb.AppendLine("# " + report.Title);
        sb.AppendLine();

        sb.AppendLine("| Property | Value |");
        sb.AppendLine("|----------|-------|");
        sb.AppendLine($"| Generated | {FormatUtcDateTime(report.GeneratedAt)} UTC |");
        sb.AppendLine($"| Application | Epiroc Pulse {report.ApplicationVersion} |");
        sb.AppendLine();
    }

    /// <summary>
    /// Generates the device information section.
    /// </summary>
    private static void GenerateDeviceInformation(StringBuilder sb, DeviceInfo deviceInfo)
    {
        sb.AppendLine("## Device Information");
        sb.AppendLine();

        sb.AppendLine("| Property | Value |");
        sb.AppendLine("|----------|-------|");
        sb.AppendLine($"| Computer Name | {EscapeMarkdown(deviceInfo.ComputerName)} |");
        sb.AppendLine($"| User Name | {EscapeMarkdown(deviceInfo.UserName)} |");
        sb.AppendLine($"| Operating System | {EscapeMarkdown(deviceInfo.OperatingSystem)} |");
        sb.AppendLine();
    }

    /// <summary>
    /// Generates the diagnostics section with all diagnostic results.
    /// </summary>
    private static void GenerateDiagnosticsSection(StringBuilder sb, List<DiagnosticResult> results)
    {
        sb.AppendLine("## Diagnostic Results");
        sb.AppendLine();

        if (results.Count == 0)
        {
            sb.AppendLine("No diagnostic results available.");
            sb.AppendLine();
            return;
        }

        foreach (var (index, result) in results.Select((r, i) => (i + 1, r)))
        {
            GenerateDiagnosticResult(sb, index, result);
        }
    }

    /// <summary>
    /// Generates a single diagnostic result block.
    /// </summary>
    private static void GenerateDiagnosticResult(StringBuilder sb, int index, DiagnosticResult result)
    {
        var statusBadge = GetStatusBadge(result.Status);
        sb.AppendLine($"### Diagnostic {index}: {statusBadge}");
        sb.AppendLine();

        // Summary
        sb.AppendLine("**Summary**");
        sb.AppendLine();
        sb.AppendLine(EscapeMarkdown(result.Summary));
        sb.AppendLine();

        // Technical Details
        sb.AppendLine("**Technical Details**");
        sb.AppendLine();
        sb.AppendLine(EscapeMarkdown(result.TechnicalDetails));
        sb.AppendLine();

        // Suggested Action
        sb.AppendLine("**Suggested Action**");
        sb.AppendLine();
        sb.AppendLine(EscapeMarkdown(result.SuggestedAction));
        sb.AppendLine();

        // Timestamp
        sb.AppendLine("**Timestamp**");
        sb.AppendLine();
        sb.AppendLine(FormatUtcDateTime(result.Timestamp) + " UTC");
        sb.AppendLine();

        sb.AppendLine("---");
        sb.AppendLine();
    }

    /// <summary>
    /// Generates the summary section with aggregate statistics.
    /// </summary>
    private static void GenerateSummarySection(StringBuilder sb, DiagnosticResultSummary summary)
    {
        sb.AppendLine("## Summary");
        sb.AppendLine();

        sb.AppendLine("| Metric | Count |");
        sb.AppendLine("|--------|-------|");
        sb.AppendLine($"| Total Checks | {summary.TotalChecks} |");
        sb.AppendLine($"| ✅ Passed | {summary.Passed} |");
        sb.AppendLine($"| ⚠️ Warnings | {summary.Warnings} |");
        sb.AppendLine($"| ❌ Failed | {summary.Failed} |");
        sb.AppendLine($"| ⊘ Skipped | {summary.Skipped} |");
        sb.AppendLine();
    }

    /// <summary>
    /// Generates the appendix section with raw diagnostic output.
    /// </summary>
    private static void GenerateAppendix(StringBuilder sb, List<DiagnosticResult> results)
    {
        var resultsWithRawOutput = results.Where(r => !string.IsNullOrEmpty(r.RawOutput)).ToList();

        if (resultsWithRawOutput.Count == 0)
        {
            return;
        }

        sb.AppendLine("## Appendix: Raw Diagnostic Output");
        sb.AppendLine();

        foreach (var (index, result) in resultsWithRawOutput.Select((r, i) => (i + 1, r)))
        {
            sb.AppendLine($"### Raw Output - Diagnostic {index}");
            sb.AppendLine();
            sb.AppendLine("```");
            sb.AppendLine(result.RawOutput);
            sb.AppendLine("```");
            sb.AppendLine();
        }
    }

    /// <summary>
    /// Gets a human-readable status badge for the diagnostic result.
    /// </summary>
    private static string GetStatusBadge(DiagnosticStatus status)
    {
        return status switch
        {
            DiagnosticStatus.Pass => "✅ Pass",
            DiagnosticStatus.Warning => "⚠️ Warning",
            DiagnosticStatus.Fail => "❌ Fail",
            DiagnosticStatus.Info => "ℹ️ Info",
            DiagnosticStatus.Skipped => "⊘ Skipped",
            _ => "❓ Unknown"
        };
    }

    /// <summary>
    /// Formats a UTC DateTime for display in reports.
    /// </summary>
    private static string FormatUtcDateTime(DateTime utcDateTime)
    {
        return utcDateTime.ToString("yyyy-MM-dd HH:mm:ss");
    }

    /// <summary>
    /// Escapes special Markdown characters in text to prevent formatting issues.
    /// </summary>
    private static string EscapeMarkdown(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        // Escape only the characters that are special in Markdown table cells and general text
        return text
            .Replace("\\", "\\\\")  // Backslash (must be first)
            .Replace("|", "\\|")    // Pipe (special in tables)
            .Replace("*", "\\*")    // Asterisk (bold/italic)
            .Replace("_", "\\_")    // Underscore (bold/italic)
            .Replace("[", "\\[")    // Bracket (links)
            .Replace("]", "\\]")    // Bracket (links)
            .Replace("`", "\\`");   // Backtick (code)
    }
}
