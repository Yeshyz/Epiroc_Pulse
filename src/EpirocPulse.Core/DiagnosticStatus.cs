namespace EpirocPulse.Core;

/// <summary>
/// Represents the status of a diagnostic check.
/// </summary>
/// <remarks>
/// Status values correspond to visual indicators in the UI:
/// - Green = Pass
/// - Yellow/Amber = Warning
/// - Red = Fail
/// - Blue = Info
/// - Grey = Skipped
/// </remarks>
public enum DiagnosticStatus
{
    /// <summary>
    /// The diagnostic check passed.
    /// Represented as green in the UI.
    /// </summary>
    Pass = 0,

    /// <summary>
    /// The diagnostic check passed with warnings or potential issues.
    /// Represented as yellow/amber in the UI.
    /// </summary>
    Warning = 1,

    /// <summary>
    /// The diagnostic check failed.
    /// Represented as red in the UI.
    /// </summary>
    Fail = 2,

    /// <summary>
    /// The diagnostic check returned informational content.
    /// Represented as blue in the UI.
    /// </summary>
    Info = 3,

    /// <summary>
    /// The diagnostic check was skipped.
    /// Represented as grey in the UI.
    /// </summary>
    Skipped = 4
}
