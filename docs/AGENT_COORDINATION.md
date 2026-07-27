\# Agent Coordination Model - Epiroc Pulse



\## Purpose



This document defines how AI agents collaborate on Epiroc Pulse.



Agents communicate through:



\- GitHub Issues

\- GitHub Pull Requests

\- `docs/AGENT\_STATUS\_BOARD.md`

\- `docs/BLOCKERS.md`

\- `docs/HANDOFFS.md`

\- `docs/DECISION\_LOG.md`

\- `CHANGELOG.md`



\## Core Principle



No agent should guess what another agent is doing.



Before starting a task, every agent must check:



1\. `docs/AGENT\_STATUS\_BOARD.md`

2\. `docs/BLOCKERS.md`

3\. `docs/HANDOFFS.md`

4\. `docs/DECISION\_LOG.md`

5\. Related GitHub Issues

6\. Related Pull Requests



\## Agent States



Each agent can be in one of these states:



\- Idle

\- Active

\- Blocked

\- Waiting for Review

\- Complete

\- Needs Merge

\- Needs Rebase



\## Handoff Process



If Agent A needs Agent B to do something:



1\. Agent A documents the issue in `docs/HANDOFFS.md`.

2\. Agent A adds a blocker in `docs/BLOCKERS.md` if the task cannot continue.

3\. Agent A creates or updates a GitHub Issue for Agent B.

4\. Agent A updates `docs/AGENT\_STATUS\_BOARD.md`.

5\. Agent B reads the handoff before starting.

6\. Agent B completes the work in its own branch.

7\. Agent B updates the handoff status.

8\. Agent A resumes after the dependency is complete.



\## Conflict Prevention



Agents must not edit the same files at the same time.



Each issue must define:



\- Assigned agent

\- Allowed files

\- Forbidden files

\- Expected output

\- Acceptance criteria

\- Dependencies

