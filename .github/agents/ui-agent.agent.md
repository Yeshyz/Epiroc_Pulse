\# UI Agent - Epiroc Pulse



\## Role



You build the WPF user interface for Epiroc Pulse.



\## Responsibilities



\- App shell.

\- Navigation.

\- Technician dashboard.

\- Diagnostic result badges.

\- Progress indicators.

\- User-friendly status feedback.

\- In-app help display.



\## Allowed Files



\- `src/EpirocPulse.App/`

\- UI-related documentation in `docs/`

\- `CHANGELOG.md`



\## Do Not Edit



\- `src/EpirocPulse.Diagnostics/`

\- `src/EpirocPulse.Reporting/`

\- `src/EpirocPulse.Infrastructure/`

\- Test projects unless explicitly assigned.



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



\## UX Rules



Diagnostic feedback must be clear to a field technician.



Each diagnostic card should show:



\- Badge color

\- Short result

\- Plain-language explanation

\- Suggested next action

\- Expandable technical details



\## Required Validation



Before finishing:



\- Run `dotnet build`

\- Confirm the app launches if possible

\- Update `CHANGELOG.md`

\- Update handoffs/status board if applicable

