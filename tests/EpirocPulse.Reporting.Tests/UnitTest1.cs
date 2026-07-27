namespace EpirocPulse.Reporting.Tests;

using EpirocPulse.Core;

public class MarkdownReportGeneratorTests
{
    [Fact]
    public void GenerateReport_WithValidReport_ReturnsMarkdownString()
    {
        // Arrange
        var generator = new MarkdownReportGenerator();
        var deviceInfo = new DeviceInfo("TESTPC", "TestUser", "Windows 11");
        var report = new Report("Test Report", "1.0.0", deviceInfo);

        // Act
        var result = generator.GenerateReport(report);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Contains("# Test Report", result);
        Assert.Contains("Device Information", result);
        Assert.Contains("Summary", result);
    }

    [Fact]
    public void GenerateReport_WithNullReport_ThrowsArgumentNullException()
    {
        // Arrange
        var generator = new MarkdownReportGenerator();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => generator.GenerateReport(null!));
    }

    [Fact]
    public void GenerateReport_IncludesHeader_WithTitleDateAndVersion()
    {
        // Arrange
        var generator = new MarkdownReportGenerator();
        var deviceInfo = new DeviceInfo("TESTPC", "TestUser", "Windows 11");
        var report = new Report("Network Diagnostics", "2.5.0", deviceInfo);
        report.GeneratedAt = new DateTime(2026, 7, 27, 14, 30, 0, DateTimeKind.Utc);

        // Act
        var result = generator.GenerateReport(report);

        // Assert
        Assert.Contains("# Network Diagnostics", result);
        Assert.Contains("2026-07-27 14:30:00", result);
        Assert.Contains("Epiroc Pulse 2.5.0", result);
    }

    [Fact]
    public void GenerateReport_IncludesDeviceInformation_WithComputerNameUserAndOS()
    {
        // Arrange
        var generator = new MarkdownReportGenerator();
        var deviceInfo = new DeviceInfo("MY-COMPUTER", "john.doe", "Windows Server 2022");
        var report = new Report("Test", "1.0.0", deviceInfo);

        // Act
        var result = generator.GenerateReport(report);

        // Assert
        Assert.Contains("## Device Information", result);
        Assert.Contains("MY-COMPUTER", result);
        Assert.Contains("john.doe", result);
        Assert.Contains("Windows Server 2022", result);
    }

    [Fact]
    public void GenerateReport_IncludesDiagnosticResults_WithStatusAndDetails()
    {
        // Arrange
        var generator = new MarkdownReportGenerator();
        var deviceInfo = new DeviceInfo("PC", "User", "Windows 11");
        var report = new Report("Test", "1.0.0", deviceInfo);

        var diagnostic1 = new DiagnosticResult(
            DiagnosticStatus.Pass,
            "Network adapter connected",
            "Ethernet connection active, 100 Mbps speed",
            "No action required",
            DateTime.UtcNow);

        var diagnostic2 = new DiagnosticResult(
            DiagnosticStatus.Fail,
            "Gateway unreachable",
            "192.168.1.1 did not respond to ping",
            "Check network cable and verify gateway IP",
            DateTime.UtcNow);

        report.DiagnosticResults.Add(diagnostic1);
        report.DiagnosticResults.Add(diagnostic2);

        // Act
        var result = generator.GenerateReport(report);

        // Assert
        Assert.Contains("## Diagnostic Results", result);
        Assert.Contains("✅ Pass", result);
        Assert.Contains("❌ Fail", result);
        Assert.Contains("Network adapter connected", result);
        Assert.Contains("Gateway unreachable", result);
        Assert.Contains("No action required", result);
    }

    [Fact]
    public void GenerateReport_WithEmptyDiagnosticResults_ShowsEmptyMessage()
    {
        // Arrange
        var generator = new MarkdownReportGenerator();
        var deviceInfo = new DeviceInfo("PC", "User", "Windows 11");
        var report = new Report("Test", "1.0.0", deviceInfo);

        // Act
        var result = generator.GenerateReport(report);

        // Assert
        Assert.Contains("No diagnostic results available", result);
    }

    [Fact]
    public void GenerateReport_IncludesSummarySection_WithCorrectCounts()
    {
        // Arrange
        var generator = new MarkdownReportGenerator();
        var deviceInfo = new DeviceInfo("PC", "User", "Windows 11");
        var report = new Report("Test", "1.0.0", deviceInfo);

        report.DiagnosticResults.Add(new DiagnosticResult(DiagnosticStatus.Pass, "Pass", "", "", DateTime.UtcNow));
        report.DiagnosticResults.Add(new DiagnosticResult(DiagnosticStatus.Pass, "Pass", "", "", DateTime.UtcNow));
        report.DiagnosticResults.Add(new DiagnosticResult(DiagnosticStatus.Warning, "Warning", "", "", DateTime.UtcNow));
        report.DiagnosticResults.Add(new DiagnosticResult(DiagnosticStatus.Fail, "Fail", "", "", DateTime.UtcNow));
        report.DiagnosticResults.Add(new DiagnosticResult(DiagnosticStatus.Skipped, "Skipped", "", "", DateTime.UtcNow));

        // Act
        var result = generator.GenerateReport(report);

        // Assert
        Assert.Contains("## Summary", result);
        Assert.Contains("| Total Checks | 5 |", result);
        Assert.Contains("| ✅ Passed | 2 |", result);
        Assert.Contains("| ⚠️ Warnings | 1 |", result);
        Assert.Contains("| ❌ Failed | 1 |", result);
        Assert.Contains("| ⊘ Skipped | 1 |", result);
    }

    [Fact]
    public void GenerateReport_WithRawOutput_IncludesAppendix()
    {
        // Arrange
        var generator = new MarkdownReportGenerator();
        var deviceInfo = new DeviceInfo("PC", "User", "Windows 11");
        var report = new Report("Test", "1.0.0", deviceInfo);

        var rawOutput = "Pinging 192.168.1.1 with 32 bytes of data:\nReply from 192.168.1.1: bytes=32 time=5ms TTL=64";
        var diagnostic = new DiagnosticResult(
            DiagnosticStatus.Pass,
            "Ping successful",
            "Gateway responded in 5ms",
            "No action required",
            DateTime.UtcNow,
            rawOutput);

        report.DiagnosticResults.Add(diagnostic);

        // Act
        var result = generator.GenerateReport(report);

        // Assert
        Assert.Contains("## Appendix: Raw Diagnostic Output", result);
        Assert.Contains("```", result);
        Assert.Contains("Pinging 192.168.1.1", result);
    }

    [Fact]
    public void GenerateReport_WithoutRawOutput_OmitsAppendix()
    {
        // Arrange
        var generator = new MarkdownReportGenerator();
        var deviceInfo = new DeviceInfo("PC", "User", "Windows 11");
        var report = new Report("Test", "1.0.0", deviceInfo);

        var diagnostic = new DiagnosticResult(
            DiagnosticStatus.Pass,
            "Test passed",
            "Details",
            "Action",
            DateTime.UtcNow);

        report.DiagnosticResults.Add(diagnostic);

        // Act
        var result = generator.GenerateReport(report);

        // Assert
        Assert.DoesNotContain("## Appendix: Raw Diagnostic Output", result);
    }

    [Fact]
    public void GenerateReport_EscapesMarkdownSpecialCharacters()
    {
        // Arrange
        var generator = new MarkdownReportGenerator();
        var deviceInfo = new DeviceInfo("PC_Test*", "User[1]", "Windows|11");
        var report = new Report("Test", "1.0.0", deviceInfo);

        // Act
        var result = generator.GenerateReport(report);

        // Assert
        // Special characters should be escaped
        Assert.Contains("PC\\_Test\\*", result);
        Assert.Contains("User\\[1\\]", result);
        Assert.Contains("Windows\\|11", result);
    }

    [Fact]
    public void GenerateReport_AllStatusTypes_HaveCorrectBadges()
    {
        // Arrange
        var generator = new MarkdownReportGenerator();
        var deviceInfo = new DeviceInfo("PC", "User", "Windows 11");
        var report = new Report("Test", "1.0.0", deviceInfo);

        report.DiagnosticResults.Add(new DiagnosticResult(DiagnosticStatus.Pass, "Pass", "", "", DateTime.UtcNow));
        report.DiagnosticResults.Add(new DiagnosticResult(DiagnosticStatus.Warning, "Warning", "", "", DateTime.UtcNow));
        report.DiagnosticResults.Add(new DiagnosticResult(DiagnosticStatus.Fail, "Fail", "", "", DateTime.UtcNow));
        report.DiagnosticResults.Add(new DiagnosticResult(DiagnosticStatus.Info, "Info", "", "", DateTime.UtcNow));
        report.DiagnosticResults.Add(new DiagnosticResult(DiagnosticStatus.Skipped, "Skipped", "", "", DateTime.UtcNow));

        // Act
        var result = generator.GenerateReport(report);

        // Assert
        Assert.Contains("✅ Pass", result);
        Assert.Contains("⚠️ Warning", result);
        Assert.Contains("❌ Fail", result);
        Assert.Contains("ℹ️ Info", result);
        Assert.Contains("⊘ Skipped", result);
    }

    [Fact]
    public void GenerateReport_IncludesAllDiagnosticDetails()
    {
        // Arrange
        var generator = new MarkdownReportGenerator();
        var deviceInfo = new DeviceInfo("PC", "User", "Windows 11");
        var report = new Report("Test", "1.0.0", deviceInfo);

        var timestamp = new DateTime(2026, 7, 27, 10, 30, 45, DateTimeKind.Utc);
        var diagnostic = new DiagnosticResult(
            DiagnosticStatus.Warning,
            "DNS Server responding slowly",
            "Primary DNS: 8.8.8.8, Secondary DNS: 8.8.4.4, Response time: 250ms (expected <100ms)",
            "Contact network administrator or change DNS servers",
            timestamp);

        report.DiagnosticResults.Add(diagnostic);

        // Act
        var result = generator.GenerateReport(report);

        // Assert
        Assert.Contains("**Summary**", result);
        Assert.Contains("DNS Server responding slowly", result);
        Assert.Contains("**Technical Details**", result);
        Assert.Contains("Primary DNS: 8.8.8.8", result);
        Assert.Contains("**Suggested Action**", result);
        Assert.Contains("Contact network administrator", result);
        Assert.Contains("**Timestamp**", result);
        Assert.Contains("2026-07-27 10:30:45 UTC", result);
    }

    [Fact]
    public void GenerateReport_WithMultipleDiagnostics_NumbersThemCorrectly()
    {
        // Arrange
        var generator = new MarkdownReportGenerator();
        var deviceInfo = new DeviceInfo("PC", "User", "Windows 11");
        var report = new Report("Test", "1.0.0", deviceInfo);

        report.DiagnosticResults.Add(new DiagnosticResult(DiagnosticStatus.Pass, "First", "", "", DateTime.UtcNow));
        report.DiagnosticResults.Add(new DiagnosticResult(DiagnosticStatus.Pass, "Second", "", "", DateTime.UtcNow));
        report.DiagnosticResults.Add(new DiagnosticResult(DiagnosticStatus.Pass, "Third", "", "", DateTime.UtcNow));

        // Act
        var result = generator.GenerateReport(report);

        // Assert
        Assert.Contains("### Diagnostic 1:", result);
        Assert.Contains("### Diagnostic 2:", result);
        Assert.Contains("### Diagnostic 3:", result);
    }

    [Fact]
    public void GenerateReport_WithMultilineRawOutput_FormatsAsCodeBlock()
    {
        // Arrange
        var generator = new MarkdownReportGenerator();
        var deviceInfo = new DeviceInfo("PC", "User", "Windows 11");
        var report = new Report("Test", "1.0.0", deviceInfo);

        var rawOutput = "Line 1\nLine 2\nLine 3\nLine 4";
        var diagnostic = new DiagnosticResult(
            DiagnosticStatus.Info,
            "Multi-line output",
            "Details",
            "Action",
            DateTime.UtcNow,
            rawOutput);

        report.DiagnosticResults.Add(diagnostic);

        // Act
        var result = generator.GenerateReport(report);

        // Assert
        Assert.Contains("```", result);
        Assert.Contains("Line 1", result);
        Assert.Contains("Line 2", result);
        Assert.Contains("Line 3", result);
        Assert.Contains("Line 4", result);
    }

    [Fact]
    public void GenerateReport_ProducesValidMarkdownStructure()
    {
        // Arrange
        var generator = new MarkdownReportGenerator();
        var deviceInfo = new DeviceInfo("TESTPC", "TestUser", "Windows 11");
        var report = new Report("Complete Report", "1.0.0", deviceInfo);

        report.DiagnosticResults.Add(new DiagnosticResult(DiagnosticStatus.Pass, "Test 1", "Detail 1", "Action 1", DateTime.UtcNow));
        report.DiagnosticResults.Add(new DiagnosticResult(DiagnosticStatus.Fail, "Test 2", "Detail 2", "Action 2", DateTime.UtcNow, "Raw output"));

        // Act
        var result = generator.GenerateReport(report);

        // Assert
        // Verify markdown structure
        Assert.StartsWith("# Complete Report", result);
        var lines = result.Split(Environment.NewLine);
        Assert.NotEmpty(lines);
        
        // Verify we have all sections
        Assert.Contains("## Device Information", result);
        Assert.Contains("## Diagnostic Results", result);
        Assert.Contains("## Summary", result);
        Assert.Contains("## Appendix: Raw Diagnostic Output", result);
    }
}

