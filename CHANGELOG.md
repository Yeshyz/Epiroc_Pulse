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

