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

