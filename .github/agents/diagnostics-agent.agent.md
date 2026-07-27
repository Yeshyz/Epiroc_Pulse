\# Diagnostics Agent - Epiroc Pulse



\## Role



You build the network diagnostic engine.



\## Responsibilities



\- Network adapter inspection.

\- IP/subnet/gateway validation.

\- Ping checks.

\- TCP port checks.

\- DNS checks.

\- Route table collection.

\- ARP table collection.

\- Future pktmon capture orchestration.



\## Allowed Files



\- `src/EpirocPulse.Diagnostics/`

\- `src/EpirocPulse.Core/` only for diagnostic models/interfaces

\- `src/EpirocPulse.Infrastructure/` only for required abstractions

\- `tests/EpirocPulse.Diagnostics.Tests/`

\- `tests/EpirocPulse.Core.Tests/` if model tests are required

\- `CHANGELOG.md`

\- Diagnostic docs in `docs/`



\## Do Not Edit



\- WPF UI files unless explicitly assigned.

\- Reporting implementation unless explicitly assigned.



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



\## Diagnostic Result Requirements



Every diagnostic method should return a structured result with:



\- Status

\- Summary

\- Technical details

\- Suggested action

\- Raw data when useful

\- Timestamp



\## Native Windows Preference



Prefer built-in Windows APIs or built-in Windows tools.



Do not introduce dependencies like Wireshark, Npcap, or external packet capture tools unless explicitly approved.



For future packet capture, prefer Windows built-in tools such as `pktmon` or `netsh trace`.



\## Required Validation



Before finishing:



\- Run `dotnet build`

\- Run related tests

\- Add or update tests for diagnostic logic

\- Update `CHANGELOG.md`

\- Update handoffs/status board if applicable

