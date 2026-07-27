namespace EpirocPulse.Core.Tests;

/// <summary>
/// Unit tests for <see cref="DiagnosticStatus"/> enum.
/// </summary>
public class DiagnosticStatusTests
{
    [Fact]
    public void DiagnosticStatus_ShouldHaveFiveValues()
    {
        var values = Enum.GetValues(typeof(DiagnosticStatus)).Cast<int>().ToList();

        Assert.Equal(5, values.Count);
    }

    [Fact]
    public void DiagnosticStatus_Pass_ShouldEqual0()
    {
        Assert.Equal(0, (int)DiagnosticStatus.Pass);
    }

    [Fact]
    public void DiagnosticStatus_Warning_ShouldEqual1()
    {
        Assert.Equal(1, (int)DiagnosticStatus.Warning);
    }

    [Fact]
    public void DiagnosticStatus_Fail_ShouldEqual2()
    {
        Assert.Equal(2, (int)DiagnosticStatus.Fail);
    }

    [Fact]
    public void DiagnosticStatus_Info_ShouldEqual3()
    {
        Assert.Equal(3, (int)DiagnosticStatus.Info);
    }

    [Fact]
    public void DiagnosticStatus_Skipped_ShouldEqual4()
    {
        Assert.Equal(4, (int)DiagnosticStatus.Skipped);
    }

    [Theory]
    [InlineData(DiagnosticStatus.Pass)]
    [InlineData(DiagnosticStatus.Warning)]
    [InlineData(DiagnosticStatus.Fail)]
    [InlineData(DiagnosticStatus.Info)]
    [InlineData(DiagnosticStatus.Skipped)]
    public void DiagnosticStatus_AllValuesAreDefined(DiagnosticStatus status)
    {
        Assert.True(Enum.IsDefined(typeof(DiagnosticStatus), status));
    }

    [Fact]
    public void DiagnosticStatus_CanParse_PassString()
    {
        var result = Enum.TryParse<DiagnosticStatus>("Pass", out var status);

        Assert.True(result);
        Assert.Equal(DiagnosticStatus.Pass, status);
    }

    [Fact]
    public void DiagnosticStatus_CanParse_WarningString()
    {
        var result = Enum.TryParse<DiagnosticStatus>("Warning", out var status);

        Assert.True(result);
        Assert.Equal(DiagnosticStatus.Warning, status);
    }

    [Fact]
    public void DiagnosticStatus_CanParse_FailString()
    {
        var result = Enum.TryParse<DiagnosticStatus>("Fail", out var status);

        Assert.True(result);
        Assert.Equal(DiagnosticStatus.Fail, status);
    }

    [Fact]
    public void DiagnosticStatus_CanParse_InfoString()
    {
        var result = Enum.TryParse<DiagnosticStatus>("Info", out var status);

        Assert.True(result);
        Assert.Equal(DiagnosticStatus.Info, status);
    }

    [Fact]
    public void DiagnosticStatus_CanParse_SkippedString()
    {
        var result = Enum.TryParse<DiagnosticStatus>("Skipped", out var status);

        Assert.True(result);
        Assert.Equal(DiagnosticStatus.Skipped, status);
    }
}
