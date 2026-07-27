namespace EpirocPulse.Core;

/// <summary>
/// Represents device information for a diagnostic report.
/// </summary>
/// <remarks>
/// This model captures system and user information at the time of diagnostic execution.
/// </remarks>
public class DeviceInfo
{
    /// <summary>
    /// Gets or sets the computer name.
    /// </summary>
    /// <remarks>
    /// The NetBIOS name of the computer running the diagnostics.
    /// </remarks>
    public string ComputerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user name.
    /// </summary>
    /// <remarks>
    /// The name of the user who initiated the diagnostics.
    /// </remarks>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the operating system description.
    /// </summary>
    /// <remarks>
    /// Example: "Windows 11 (22H2)", "Windows Server 2022", etc.
    /// </remarks>
    public string OperatingSystem { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeviceInfo"/> class.
    /// </summary>
    public DeviceInfo()
    {
    }

    /// <summary>
    /// Creates a new DeviceInfo with the specified properties.
    /// </summary>
    /// <param name="computerName">The computer name.</param>
    /// <param name="userName">The user name.</param>
    /// <param name="operatingSystem">The operating system description.</param>
    public DeviceInfo(string computerName, string userName, string operatingSystem)
    {
        ComputerName = computerName;
        UserName = userName;
        OperatingSystem = operatingSystem;
    }
}
