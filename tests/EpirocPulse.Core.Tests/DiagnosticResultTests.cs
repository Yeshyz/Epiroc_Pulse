namespace EpirocPulse.Core.Tests;

/// <summary>
/// Unit tests for <see cref="DiagnosticResult"/> model.
/// </summary>
public class DiagnosticResultTests
{
    private readonly DateTime _testTimestamp = new(2024, 1, 15, 10, 30, 0, DateTimeKind.Utc);

    [Fact]
    public void DiagnosticResult_Constructor_WithAllParameters_ShouldCreateInstanceSuccessfully()
    {
        var result = new DiagnosticResult(
            DiagnosticStatus.Pass,
            "Network adapter connected",
            "Ethernet adapter has IP 192.168.1.100",
            "Continue with next diagnostic",
            _testTimestamp,
            "some raw output");

        Assert.NotNull(result);
        Assert.Equal(DiagnosticStatus.Pass, result.Status);
        Assert.Equal("Network adapter connected", result.Summary);
        Assert.Equal("Ethernet adapter has IP 192.168.1.100", result.TechnicalDetails);
        Assert.Equal("Continue with next diagnostic", result.SuggestedAction);
        Assert.Equal(_testTimestamp, result.Timestamp);
        Assert.Equal("some raw output", result.RawOutput);
    }

    [Fact]
    public void DiagnosticResult_Constructor_WithoutRawOutput_ShouldHaveNullRawOutput()
    {
        var result = new DiagnosticResult(
            DiagnosticStatus.Pass,
            "Network adapter connected",
            "Ethernet adapter has IP 192.168.1.100",
            "Continue with next diagnostic",
            _testTimestamp);

        Assert.Null(result.RawOutput);
    }

    [Fact]
    public void DiagnosticResult_PropertyInitializer_ShouldAllowSetting()
    {
        var result = new DiagnosticResult
        {
            Status = DiagnosticStatus.Fail,
            Summary = "Gateway unreachable",
            TechnicalDetails = "Ping to 192.168.1.1 failed",
            SuggestedAction = "Check network cable",
            Timestamp = _testTimestamp,
            RawOutput = null
        };

        Assert.Equal(DiagnosticStatus.Fail, result.Status);
        Assert.Equal("Gateway unreachable", result.Summary);
        Assert.Equal("Ping to 192.168.1.1 failed", result.TechnicalDetails);
        Assert.Equal("Check network cable", result.SuggestedAction);
        Assert.Equal(_testTimestamp, result.Timestamp);
        Assert.Null(result.RawOutput);
    }

    [Theory]
    [InlineData(DiagnosticStatus.Pass)]
    [InlineData(DiagnosticStatus.Warning)]
    [InlineData(DiagnosticStatus.Fail)]
    [InlineData(DiagnosticStatus.Info)]
    [InlineData(DiagnosticStatus.Skipped)]
    public void DiagnosticResult_ShouldSupportAllStatuses(DiagnosticStatus status)
    {
        var result = new DiagnosticResult(
            status,
            "Test summary",
            "Test details",
            "Test action",
            _testTimestamp);

        Assert.Equal(status, result.Status);
    }

    [Fact]
    public void DiagnosticResult_PassResult_ShouldHaveCorrectProperties()
    {
        var result = new DiagnosticResult(
            DiagnosticStatus.Pass,
            "Network adapter connected",
            "Ethernet: 192.168.1.100/24, Gateway: 192.168.1.1, DNS: 8.8.8.8",
            "Proceed to next diagnostic check",
            _testTimestamp);

        Assert.Equal(DiagnosticStatus.Pass, result.Status);
        Assert.Contains("connected", result.Summary.ToLower());
    }

    [Fact]
    public void DiagnosticResult_WarningResult_ShouldHaveCorrectProperties()
    {
        var result = new DiagnosticResult(
            DiagnosticStatus.Warning,
            "Unexpected APIPA address detected",
            "Adapter is using autoconfigured IP 169.254.x.x instead of DHCP",
            "Verify DHCP server is reachable",
            _testTimestamp,
            "ipconfig output");

        Assert.Equal(DiagnosticStatus.Warning, result.Status);
        Assert.Contains("APIPA", result.Summary);
        Assert.NotNull(result.RawOutput);
    }

    [Fact]
    public void DiagnosticResult_FailResult_ShouldHaveCorrectProperties()
    {
        var result = new DiagnosticResult(
            DiagnosticStatus.Fail,
            "Gateway unreachable",
            "Ping to 192.168.1.1 timed out after 3 attempts",
            "Check network cable, switch port, or gateway status",
            _testTimestamp,
            "ping output");

        Assert.Equal(DiagnosticStatus.Fail, result.Status);
        Assert.Contains("unreachable", result.Summary.ToLower());
    }

    [Fact]
    public void DiagnosticResult_InfoResult_ShouldHaveCorrectProperties()
    {
        var result = new DiagnosticResult(
            DiagnosticStatus.Info,
            "Multiple network adapters detected",
            "Found 3 active adapters: Ethernet, Wi-Fi, VPN",
            "Review adapter configuration if unexpected",
            _testTimestamp);

        Assert.Equal(DiagnosticStatus.Info, result.Status);
        Assert.Contains("Multiple", result.Summary);
    }

    [Fact]
    public void DiagnosticResult_SkippedResult_ShouldHaveCorrectProperties()
    {
        var result = new DiagnosticResult(
            DiagnosticStatus.Skipped,
            "TCP port check skipped",
            "Port check skipped due to gateway unreachable",
            "Address gateway issue before retrying",
            _testTimestamp);

        Assert.Equal(DiagnosticStatus.Skipped, result.Status);
        Assert.Contains("skipped", result.Summary.ToLower());
    }

    [Fact]
    public void DiagnosticResult_Timestamp_ShouldBePersisted()
    {
        var specificTime = new DateTime(2024, 6, 15, 14, 30, 45, DateTimeKind.Utc);
        var result = new DiagnosticResult(
            DiagnosticStatus.Pass,
            "Test",
            "Test",
            "Test",
            specificTime);

        Assert.Equal(specificTime, result.Timestamp);
    }

    [Fact]
    public void DiagnosticResult_SummaryAndDetailsCanBeLong()
    {
        var longSummary = string.Join(" ", Enumerable.Repeat("word", 50));
        var longDetails = string.Join(" ", Enumerable.Repeat("detail", 100));

        var result = new DiagnosticResult(
            DiagnosticStatus.Pass,
            longSummary,
            longDetails,
            "Test action",
            _testTimestamp);

        Assert.Equal(longSummary, result.Summary);
        Assert.Equal(longDetails, result.TechnicalDetails);
    }

    [Fact]
    public void DiagnosticResult_RawOutputCanBeEmpty()
    {
        var result = new DiagnosticResult(
            DiagnosticStatus.Pass,
            "Test",
            "Test",
            "Test",
            _testTimestamp,
            string.Empty);

        Assert.NotNull(result.RawOutput);
        Assert.Empty(result.RawOutput);
    }

    [Fact]
    public void DiagnosticResult_SummaryCanBeEmpty()
    {
        var result = new DiagnosticResult(
            DiagnosticStatus.Pass,
            string.Empty,
            "Technical details",
            "Suggested action",
            _testTimestamp);

        Assert.Empty(result.Summary);
    }

    [Fact]
    public void DiagnosticResult_MultipleInstancesAreIndependent()
    {
        var result1 = new DiagnosticResult(
            DiagnosticStatus.Pass,
            "Result 1",
            "Details 1",
            "Action 1",
            _testTimestamp);

        var result2 = new DiagnosticResult(
            DiagnosticStatus.Fail,
            "Result 2",
            "Details 2",
            "Action 2",
            _testTimestamp.AddSeconds(10),
            "Raw 2");

        Assert.Equal(DiagnosticStatus.Pass, result1.Status);
        Assert.Equal(DiagnosticStatus.Fail, result2.Status);
        Assert.Equal("Result 1", result1.Summary);
        Assert.Equal("Result 2", result2.Summary);
        Assert.Null(result1.RawOutput);
        Assert.Equal("Raw 2", result2.RawOutput);
    }

    [Fact]
    public void DiagnosticResult_CanModifyPropertiesAfterCreation()
    {
        var result = new DiagnosticResult(
            DiagnosticStatus.Pass,
            "Initial summary",
            "Initial details",
            "Initial action",
            _testTimestamp);

        result.Status = DiagnosticStatus.Warning;
        result.Summary = "Modified summary";
        result.RawOutput = "New raw output";

        Assert.Equal(DiagnosticStatus.Warning, result.Status);
        Assert.Equal("Modified summary", result.Summary);
        Assert.Equal("New raw output", result.RawOutput);
    }

    [Fact]
    public void DiagnosticResult_DefaultConstructor_CreatesEmptyInstance()
    {
        var result = new DiagnosticResult();

        Assert.Null(result.RawOutput);
    }

    [Fact]
    public void DiagnosticResult_TimestampKindMatters()
    {
        var utcTime = new DateTime(2024, 6, 15, 12, 0, 0, DateTimeKind.Utc);
        var result = new DiagnosticResult(
            DiagnosticStatus.Pass,
            "Test",
            "Test",
            "Test",
            utcTime);

        Assert.Equal(DateTimeKind.Utc, result.Timestamp.Kind);
    }
}
