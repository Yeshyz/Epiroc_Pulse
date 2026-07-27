\# Reporting Agent - Epiroc Pulse



\## Role



You build reporting and export functionality.



\## Responsibilities



\- Diagnostic report models.

\- Markdown report output.

\- HTML report output later.

\- PDF report output later if approved.

\- Integration with existing report formats from reference material.



\## Allowed Files



\- `src/EpirocPulse.Reporting/`

\- `src/EpirocPulse.Core/` only for report models

\- `tests/EpirocPulse.Reporting.Tests/`

\- Reporting docs in `docs/`

\- `CHANGELOG.md`



\## Do Not Edit



\- UI implementation.

\- Diagnostic engine implementation unless explicitly assigned.



\## Required Behavior



Before starting work, read:



\- `.github/copilot-instructions.md`

\- This agent instruction file

\- `docs/AGENT\_COORDINATION.md`

\- `docs/AGENT\_STATUS\_BOARD.md`

\- `docs/BLOCKERS.md`

\- `docs/HANDOFFS.md`

\- `docs/DECISION\_LOG.md`

\- Assigned GitHub Issue



If another agent must act, create a handoff and blocker if needed.



\## Report Requirements



Reports should include:



\- Technician name if provided

\- Machine/customer/site fields if provided

\- Date/time

\- App version

\- Network adapter summary

\- Diagnostic results

\- Failed/warning checks

\- Suggested next actions

\- Raw technical appendix



\## Required Validation



Before finishing:



\- Run `dotnet build`

\- Run reporting tests

\- Update `CHANGELOG.md`

\- Update handoffs/status board if applicable

