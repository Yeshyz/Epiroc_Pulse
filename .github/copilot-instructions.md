\# Epiroc Pulse - Global Copilot Instructions



\## Project Summary



Epiroc Pulse is a Windows desktop application for Epiroc field technicians.



Its primary purpose is to diagnose and troubleshoot network problems while connected to an Epiroc machine.



The app is being built from scratch in C#/.NET/WPF. A previous Electron/Node version exists only as reference material.



\## Tech Stack



\- C#

\- .NET

\- WPF

\- MVVM

\- xUnit

\- Markdown documentation

\- GitHub Issues, branches, and pull requests



\## Primary Goals



\- Provide technician-friendly network diagnostics.

\- Run natively on Windows.

\- Avoid third-party runtime dependencies where practical.

\- Provide clear pass, warning, fail, info, and skipped feedback.

\- Maintain strong documentation and changelog discipline.

\- Design for future packet capture, reporting, and advanced diagnostics.



\## Architecture Rules



\- Keep UI code in `src/EpirocPulse.App`.

\- Keep shared models in `src/EpirocPulse.Core`.

\- Keep diagnostic logic in `src/EpirocPulse.Diagnostics`.

\- Keep reporting logic in `src/EpirocPulse.Reporting`.

\- Keep OS/process/file abstractions in `src/EpirocPulse.Infrastructure`.

\- Do not put diagnostic logic directly in WPF code-behind.

\- Prefer interfaces for system-level operations so they can be tested.

\- Make network diagnostics explainable to a technician.



\## UX Rules



Every diagnostic result should include:



\- Status

\- Short technician-friendly summary

\- Technical detail

\- Suggested next action

\- Timestamp

\- Optional raw output



Use consistent status meaning:



\- Green = Pass

\- Yellow/Amber = Warning

\- Red = Fail

\- Blue = Info

\- Grey = Skipped or Unknown



\## Multi-Agent Collaboration Rules



This project uses multiple AI agents.



Agents communicate through:



\- GitHub Issues

\- GitHub Pull Requests

\- `docs/AGENT\_STATUS\_BOARD.md`

\- `docs/BLOCKERS.md`

\- `docs/HANDOFFS.md`

\- `docs/DECISION\_LOG.md`

\- `CHANGELOG.md`



Before starting work, every agent must read:



1\. `.github/copilot-instructions.md`

2\. Its own agent instruction file

3\. `docs/AGENT\_COORDINATION.md`

4\. `docs/AGENT\_STATUS\_BOARD.md`

5\. `docs/BLOCKERS.md`

6\. `docs/HANDOFFS.md`

7\. `docs/DECISION\_LOG.md`

8\. The assigned GitHub Issue



If an agent is blocked by another area:



1\. Stop work on the blocked portion.

2\. Add an entry in `docs/BLOCKERS.md`.

3\. Add an entry in `docs/HANDOFFS.md`.

4\. Update `docs/AGENT\_STATUS\_BOARD.md`.

5\. Create or update a GitHub Issue for the required agent.

6\. Do not make assumptions or edit another agent's files.



\## File Ownership Rules



Agents must not edit the same files at the same time.



Each GitHub Issue must define:



\- Assigned agent

\- Allowed files

\- Forbidden files

\- Dependencies

\- Acceptance criteria



\## Documentation Rules



Every meaningful change must update:



\- `CHANGELOG.md`

\- Relevant file in `docs/`

\- README if user-facing behavior changes



\## Old App Reference Rules



The old app in `reference/old-app/` is reference-only.



\- Do not copy old JavaScript/Node/Electron code directly.

\- Extract workflows, terminology, report ideas, and diagnostic concepts.

\- Reimplement cleanly in C#/.NET/WPF.

