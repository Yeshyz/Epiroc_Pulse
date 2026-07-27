\# Changelog



All notable changes to Epiroc Pulse will be documented in this file.



\## \[Unreleased]



\### Added



\- Initial project structure.

\- Initial multi-agent coordination model.

\- **Diagnostic Domain Models** (in `src/EpirocPulse.Core/`):
  \- `DiagnosticStatus` enum with five states: Pass, Warning, Fail, Info, Skipped
  \- `DiagnosticResult` model with structured properties for diagnostic results: Status, Summary, TechnicalDetails, SuggestedAction, Timestamp, and optional RawOutput
  \- Comprehensive XML documentation for all public types
  \- 37 unit tests covering all enum values, model construction, property initialization, and edge cases

\- **WPF Application Shell** (in `src/EpirocPulse.App/`):
  \- Main application window with modern technician-focused layout
  \- Left sidebar navigation with Dashboard, Diagnostics, Reports, Settings, and Help sections
  \- Color-coded status badge system: Pass (green), Warning (amber), Fail (red), Info (blue), Skipped (gray)
  \- Reusable XAML styles for badges, cards, buttons, and layouts
  \- Dashboard view with welcome section, application status, placeholder diagnostic cards, and recent activity
  \- Diagnostics view with list of available diagnostics and run options
  \- Reports view with template selection and report management placeholders
  \- Settings view with application configuration options and machine setup
  \- Help view with quick start guide, badge legend, common issues, and support information
  \- MVVM-friendly structure with view and code-behind separation

\- **Reporting V1** (in `src/EpirocPulse.Reporting/` and `src/EpirocPulse.Core/`):
  \- `Report` domain model with title, generation timestamp, application version, device info, and diagnostic results
  \- `DeviceInfo` model capturing computer name, user name, and operating system at report generation time
  \- `DiagnosticResultSummary` model aggregating diagnostic result statistics (total, passed, warnings, failed, skipped)
  \- `IReportGenerator` interface for dependency injection of report generation services
  \- `MarkdownReportGenerator` implementation supporting:
    \- Technician-friendly Markdown formatting with clear visual structure
    \- Header section with report title, generation date/time, and application version
    \- Device Information section showing computer name, user name, and OS
    \- Diagnostic Results section with numbered diagnostics, status badges, summary, technical details, suggested actions, and timestamps
    \- Summary section with aggregate statistics (total checks, passed, warnings, failed, skipped)
    \- Appendix with raw diagnostic output in code blocks (optional)
    \- Markdown special character escaping to prevent formatting conflicts
    \- Status badge emoji support (✅ Pass, ⚠️ Warning, ❌ Fail, ℹ️ Info, ⊘ Skipped)
  \- 15 comprehensive unit tests covering:
    \- Valid report generation
    \- All report sections and content
    \- Empty diagnostic results handling
    \- Markdown special character escaping
    \- Status badge rendering
    \- Raw output formatting
    \- Multi-line diagnostic results
    \- Markdown structure validation

