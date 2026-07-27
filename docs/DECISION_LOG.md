\# Decision Log - Epiroc Pulse

This file records technical and project decisions.

## Decisions

### DECISION-0001 - Five-Project Layered Architecture

Date: 2026-07-27  
Status: Accepted  
Owner: Architecture Agent

### Context

Epiroc Pulse needs to support multiple concurrent AI agents working on UI, diagnostics, reporting, and testing. The architecture must enable parallel development without conflicts or circular dependencies.

### Decision

Adopt a five-project layered architecture:

1. **EpirocPulse.App** – WPF UI and ViewModels
2. **EpirocPulse.Core** – Domain models and enums (no dependencies)
3. **EpirocPulse.Diagnostics** – Diagnostic services and orchestration
4. **EpirocPulse.Infrastructure** – System abstractions (networking, I/O, processes)
5. **EpirocPulse.Reporting** – Report generation and export

### Reasoning

- **Separation of Concerns:** Each layer has a single responsibility
- **Testability:** Infrastructure abstractions enable unit testing without OS dependencies
- **Parallel Development:** Agents can work on different layers simultaneously
- **Clear Ownership:** Each agent owns specific projects with clear boundaries
- **Extensibility:** New features (packet capture, advanced diagnostics) fit naturally into existing layers
- **Dependency Direction:** Strict DAG structure (no circular dependencies) enforces clean architecture

### Consequences

- Requires careful dependency review in code reviews
- Agents must not edit the same project simultaneously
- Each agent must understand the overall architecture before implementation
- Documentation overhead (ARCHITECTURE.md, DECISION_LOG.md, AGENT_COORDINATION.md)

---

### DECISION-0002 - MVVM Pattern for UI Layer

Date: 2026-07-27  
Status: Accepted  
Owner: Architecture Agent

### Context

WPF application needs to separate UI presentation from business logic to enable testing and maintainability.

### Decision

Strictly enforce MVVM pattern in EpirocPulse.App:

- **View** (XAML) – Pure presentation with data bindings
- **ViewModel** – Logic, state, and commands; implements INotifyPropertyChanged
- **Model** – Domain models from EpirocPulse.Core

### Reasoning

- **Industry Standard:** MVVM is the canonical pattern for WPF
- **Testability:** ViewModels can be unit tested without UI framework
- **Separation:** Presentation logic cleanly separated from business logic
- **Reusability:** ViewModels can be used by different view technologies

### Consequences

- ViewModels must not bypass abstractions (e.g., directly call Infrastructure)
- All diagnostic and reporting logic must flow through service interfaces
- Code-behind limited to XAML initialization only
- Requires clear binding conventions and documentation

---

### DECISION-0003 - Abstraction-Based Infrastructure Layer

Date: 2026-07-27  
Status: Accepted  
Owner: Architecture Agent

### Context

Diagnostics need to interact with OS-level APIs (networking, processes, file I/O) without coupling business logic to OS details or making diagnostics difficult to test.

### Decision

Create EpirocPulse.Infrastructure as an abstraction layer:

- Define interfaces (INetworkClient, IProcessExecutor, IFileService, etc.)
- Isolate P/Invoke and platform-specific code in implementations
- Provide mock implementations for testing
- Diagnostics depend on abstractions, not concrete implementations

### Reasoning

- **Testability:** Mocks enable unit testing without real network calls
- **Portability:** Could potentially support other platforms by swapping implementations
- **Clarity:** OS-specific code is centralized and visible
- **Isolation:** Reduces maintenance burden on diagnostic logic

### Consequences

- Infrastructure is purely abstraction-focused (no business logic)
- Implementations may need platform-specific handling (P/Invoke, etc.)
- Testing requires mock implementations
- Slight performance overhead from abstraction layers (acceptable for diagnostics)

---

### DECISION-0004 - Packet Capture as Future Extension

Date: 2026-07-27  
Status: Accepted  
Owner: Architecture Agent

### Context

Initial release focuses on network diagnostics (ping, TCP, adapter detection). Packet capture is a Phase 6 feature but must not force architectural changes.

### Decision

Define packet capture abstractions in advance (IPacketCaptureService in Infrastructure, IPacketCaptureOrchestrator in Diagnostics) but defer implementation to Phase 6.

### Reasoning

- **Forward Compatibility:** Architecture is ready for packet capture without refactoring
- **Windows Native First:** Avoid third-party packet capture libraries if possible
- **Incremental Delivery:** Core diagnostics ship without packet capture overhead
- **Risk Mitigation:** Phase 5 can research and prototype before committing

### Consequences

- Unused interfaces in early phases (Phase 0-5)
- Requires research into Windows native packet capture options
- Implementation strategy (netsh trace, WinPcap, etc.) TBD
- Phase 6 must integrate packet analysis results into existing diagnostic workflow

---

### DECISION-0005 - Core Project Zero Dependencies

Date: 2026-07-27  
Status: Accepted  
Owner: Architecture Agent

### Context

Core project (domain models, enums) must be referenced by all other projects without creating circular dependency risks.

### Decision

EpirocPulse.Core contains:

- Domain models (DiagnosticResult, NetworkAdapter, etc.)
- Enums (DiagnosticStatus, etc.)
- Interfaces for dependency injection contracts
- **No implementation logic**
- **No dependencies on other projects**

### Reasoning

- **Foundational:** All layers depend on Core; Core depends on nothing
- **Stability:** Changes to Core require careful review (affects all layers)
- **Clarity:** Core is the single source of truth for domain contracts
- **Testability:** Core classes are testable without any dependencies

### Consequences

- Core cannot contain validation logic or complex algorithms
- Interfaces in Core are contracts, not implementations
- Breaking changes to Core require coordinated updates across all projects
- Clear separation between contracts and implementations

---

### DECISION-0006 - Multi-Agent Coordination Model

Date: 2026-07-27  
Status: Accepted  
Owner: Architecture Agent

### Context

Epiroc Pulse is developed by multiple AI agents (Architecture, UI, Diagnostics, Infrastructure, Reporting, QA) working in parallel.

### Decision

Adopt a formal multi-agent coordination model:

- Each agent owns specific projects and files (documented in ARCHITECTURE.md)
- Agents communicate through documentation (DECISION_LOG.md, BLOCKERS.md, HANDOFFS.md, AGENT_STATUS_BOARD.md)
- No agent may edit another agent's project without explicit coordination
- Code reviews enforce architectural compliance

### Reasoning

- **Conflict Prevention:** Clear file ownership prevents merge conflicts
- **Visibility:** Handoff documentation prevents blocked dependencies
- **Accountability:** Each agent is responsible for their layer's quality
- **Scalability:** Process supports adding more agents in future

### Consequences

- Requires discipline and documentation overhead
- Handoff process adds latency (mitigated by clear coordination)
- Each agent must read coordination docs before starting work
- Changes affecting other agents require explicit approval

---

### DECISION-0007 - Diagnostic Result Structure

Date: 2026-07-27  
Status: Accepted  
Owner: Architecture Agent

### Context

Each diagnostic test must provide technicians with actionable information: status, summary, technical detail, and suggested next steps.

### Decision

Every diagnostic result includes:

- **Status** (DiagnosticStatus) – Pass, Fail, Warning, Info, Skipped, Unknown
- **Summary** – One-sentence technician-friendly description
- **TechnicalDetail** – Raw data, IPs, latencies, raw output
- **SuggestedAction** – What technician should do next
- **Timestamp** – When the test ran
- **RawOutput** – Optional (ping output, tracert, etc.)

### Reasoning

- **Usability:** Technicians get clear, actionable feedback
- **Debuggability:** Technical details enable support team to diagnose issues
- **Consistency:** All diagnostics follow same structure
- **Reporting:** Reports can easily aggregate and format results

### Consequences

- Service implementations must populate all fields carefully
- Documentation required for each diagnostic's expected output
- Reporting system built around this structure
- Status colors (Green/Yellow/Red/Blue/Grey) mapped to UI badges

---

### DECISION-0008 - Dependency Injection and Service Interfaces

Date: 2026-07-27  
Status: Accepted  
Owner: Architecture Agent

### Context

ViewModels, services, and tests need to work with abstractions rather than concrete implementations.

### Decision

All services are defined as interfaces in Core or their responsible layer. Dependencies are injected via constructor. No static dependencies or singletons.

### Reasoning

- **Testability:** Interfaces can be mocked for unit tests
- **Flexibility:** Implementations can be swapped without changing consumers
- **Clarity:** Dependencies are explicit and visible
- **Decoupling:** Services don't create their own dependencies

### Consequences

- Requires composition root in App layer for wiring dependencies
- Constructor parameters may grow for complex services
- Services must be stateless or thread-safe
- No global state or singletons

---

## Decision Template

### DECISION-XXXX - Title

Date: YYYY-MM-DD  
Status: Proposed / Accepted / Rejected / Superseded  
Owner: Architecture Agent / Orchestrator Agent / Human

### Context

Explain the issue or choice.

### Decision

Explain the decision made.

### Reasoning

Explain why.

### Consequences

Explain impact, risks, and follow-up tasks.



\## Decision Template



\### DECISION-0000 - Title



Date: YYYY-MM-DD  

Status: Proposed / Accepted / Rejected / Superseded  

Owner: Architecture Agent / Orchestrator Agent / Human  



\## Context



Explain the issue or choice.



\## Decision



Explain the decision made.



\## Reasoning



Explain why.



\## Consequences



Explain impact, risks, and follow-up tasks.

