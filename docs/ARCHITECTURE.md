# Epiroc Pulse - Architecture Document

## Table of Contents

1. [Project Purpose](#project-purpose)
2. [Solution Structure](#solution-structure)
3. [Project Responsibilities](#project-responsibilities)
4. [MVVM Architecture](#mvvm-architecture)
5. [Dependency Rules](#dependency-rules)
6. [Diagnostic Workflow](#diagnostic-workflow)
7. [Reporting Workflow](#reporting-workflow)
8. [Packet Capture Architecture](#packet-capture-architecture)
9. [Agent Ownership Boundaries](#agent-ownership-boundaries)
10. [Risks and Assumptions](#risks-and-assumptions)

---

## Project Purpose

**Epiroc Pulse** is a Windows desktop troubleshooting application built for Epiroc field technicians.

### Primary Goals

1. **Network Diagnostics** – Detect and diagnose network connectivity issues between technician laptops and Epiroc machines.
2. **Network Troubleshooting** – Guide technicians through structured diagnostic workflows.
3. **Native Windows Operation** – Run natively on Windows without requiring external runtimes or plugins.
4. **Diagnostic Reporting** – Export diagnostic results in human-readable and machine-parseable formats.
5. **Future Packet Capture** – Extensible architecture to support native Windows packet capture (PCAP).
6. **Future Advanced Diagnostics** – Foundation for machine profiles, guided troubleshooting flows, and advanced analysis.

### Technology Stack

- **Language:** C#
- **Framework:** .NET
- **UI:** WPF (Windows Presentation Foundation)
- **Architecture Pattern:** MVVM (Model-View-ViewModel)
- **Testing:** xUnit
- **Documentation:** Markdown
- **Version Control:** Git with GitHub Issues and Pull Requests

---

## Solution Structure

The solution is organized into five core projects, each with a specific responsibility:

```
EpirocPulse.slnx (Solution file)
│
├── src/
│   ├── EpirocPulse.App/              (UI Layer - WPF)
│   ├── EpirocPulse.Core/             (Domain Models & Enums)
│   ├── EpirocPulse.Diagnostics/      (Diagnostic Logic)
│   ├── EpirocPulse.Infrastructure/   (OS/System Abstractions)
│   └── EpirocPulse.Reporting/        (Report Generation)
│
├── tests/                            (Test Projects)
│
├── docs/                             (Architecture & Coordination)
│
├── reference/                        (Old App - Reference Only)
│
├── README.md
├── CHANGELOG.md
└── EpirocPulse.slnx
```

---

## Project Responsibilities

### 1. EpirocPulse.App

**Layer:** Presentation (User Interface)

**Responsibility:** Provide the WPF user interface and coordinate diagnostic workflows.

**Key Responsibilities:**
- Render MainWindow and navigation
- Display diagnostic dashboards and results
- Manage ViewModel instances (MVVM pattern)
- Orchestrate user interactions
- Bind view state to domain models through ViewModels
- Present status badges (Green/Yellow/Red/Blue/Grey)
- Provide export and report UI

**Technologies:**
- WPF (XAML + C# code-behind)
- MVVM pattern

**What It Must NOT Do:**
- Contain diagnostic logic
- Make direct OS or network calls
- Depend on Infrastructure for direct calls (use Diagnostics abstractions instead)
- Bypass ViewModels to access data

**Dependencies:**
- `EpirocPulse.Core` (read-only access to models and enums)
- `EpirocPulse.Diagnostics` (diagnostic services via dependency injection)

---

### 2. EpirocPulse.Core

**Layer:** Domain Model

**Responsibility:** Define all shared models, enums, and contracts used across layers.

**Key Responsibilities:**
- Define `DiagnosticStatus` enum (Pass, Fail, Warning, Info, Skipped, Unknown)
- Define `DiagnosticResult` model with status, summary, detail, suggested action, timestamp
- Define network-related domain models (NetworkAdapter, IPAddress, Gateway, etc.)
- Define report structures and data contracts
- Define interfaces for dependency injection contracts

**Key Models:**
- `DiagnosticStatus` – Enumeration of result statuses
- `DiagnosticResult` – Single diagnostic test result with full context
- `DiagnosticSuite` – Collection of related diagnostic results
- `NetworkAdapter` – Represents a network interface
- `DiagnosticContext` – Input configuration for diagnostic runs
- `Report` – Base structure for reports

**What It Must NOT Do:**
- Contain implementation logic
- Perform I/O or system operations
- Depend on any other project

**Dependencies:**
- None (pure domain models)

---

### 3. EpirocPulse.Diagnostics

**Layer:** Business Logic

**Responsibility:** Implement all diagnostic workflows and detection logic.

**Key Responsibilities:**
- Implement diagnostic services (adapter detection, ping tests, TCP tests, etc.)
- Orchestrate multi-step diagnostic workflows
- Detect common network issues (APIPA, wrong subnet, multiple adapters, VPN, etc.)
- Provide results in `DiagnosticResult` format
- Use `Infrastructure` for OS operations through abstraction interfaces
- Implement status badge logic
- Provide suggested next actions

**Key Services:**
- `INetworkDiagnosticsService` – Orchestrates all network diagnostics
- `IAdapterDetectionService` – Detects and enumerates network adapters
- `IGatewayCheckService` – Tests gateway reachability
- `IDeviceReachabilityService` – Tests Epiroc machine reachability
- `ITcpPortCheckService` – Tests TCP connectivity to specific ports
- `INetworkIssueDetectionService` – Detects APIPA, routing, DNS issues

**What It Must NOT Do:**
- Make direct OS/network calls (use Infrastructure abstractions)
- Contain UI logic
- Contain reporting logic (delegate to Reporting)

**Dependencies:**
- `EpirocPulse.Core` (models and contracts)
- `EpirocPulse.Infrastructure` (system abstractions)

---

### 4. EpirocPulse.Infrastructure

**Layer:** System Abstractions

**Responsibility:** Provide testable abstractions for OS and system operations.

**Key Responsibilities:**
- Abstract network operations (ping, TCP connect, DNS)
- Abstract OS process execution
- Abstract file I/O for reports
- Abstract system information queries
- Provide mock implementations for testing
- Encapsulate P/Invoke and OS-specific code

**Key Abstractions:**
- `INetworkClient` – Abstracts ping, TCP connect, DNS queries
- `IProcessExecutor` – Abstracts running external processes
- `IFileService` – Abstracts file I/O
- `ISystemInformation` – Abstracts OS queries
- `IPacketCaptureService` – (Future) Abstracts packet capture operations

**What It Must NOT Do:**
- Contain business logic
- Make architectural decisions
- Bypass abstractions to call OS directly in Diagnostics

**Dependencies:**
- `EpirocPulse.Core` (models only)
- .NET Framework libraries
- Platform-specific APIs (via P/Invoke, carefully isolated)

---

### 5. EpirocPulse.Reporting

**Layer:** Report Generation

**Responsibility:** Convert diagnostic results into exportable reports.

**Key Responsibilities:**
- Generate Markdown reports from diagnostic suites
- Implement report templates
- Format diagnostic summaries for export
- Include suggested actions and next steps
- Support multiple output formats (Markdown initially)
- Prepare data for future advanced reporting

**Key Services:**
- `IReportGenerator` – Main report generation interface
- `IMarkdownReportFormatter` – Formats results as Markdown
- `IReportTemplate` – Report structure and layout

**What It Must NOT Do:**
- Modify diagnostic results
- Perform new diagnostics
- Make UI decisions
- Handle file I/O directly (delegate to Infrastructure)

**Dependencies:**
- `EpirocPulse.Core` (models)
- `EpirocPulse.Infrastructure` (for file I/O)

---

## MVVM Architecture

Epiroc Pulse follows the **Model-View-ViewModel** pattern strictly in the UI layer.

### Pattern Components

```
View (XAML)
    ↓ (Data Binding)
ViewModel (Logic & State)
    ↓ (Dependencies)
Model (Domain Model from Core)
    ↓ (Dependencies)
Services (Diagnostics, Reporting, Infrastructure)
```

### MVVM Layers

1. **View (XAML)**
   - Pure XAML presentation
   - Data bindings to ViewModel properties
   - Commands bound to ViewModel methods
   - No code-behind except XAML initialization

2. **ViewModel**
   - Implements `INotifyPropertyChanged`
   - Exposes properties for data binding
   - Implements commands for user actions
   - Coordinates with diagnostic services
   - Transforms Model data for presentation
   - Maintains view state (selected items, collapsed sections, etc.)

3. **Model (Domain Models)**
   - Defined in `EpirocPulse.Core`
   - Read-only or immutable where possible
   - Contains no presentation logic

### ViewModel Responsibilities

- **Separate Concerns:** ViewModels are the bridge between View and business logic
- **Testable UI Logic:** ViewModel logic can be unit tested without UI framework
- **State Management:** ViewModels maintain view-specific state
- **Command Handling:** ViewModels implement all user actions through commands
- **Binding Support:** Implement `INotifyPropertyChanged` for two-way binding

---

## Dependency Rules

### Allowed Dependencies

```
EpirocPulse.App
    ↓
    ├─→ EpirocPulse.Core (read-only models)
    └─→ EpirocPulse.Diagnostics (service interfaces)

EpirocPulse.Diagnostics
    ↓
    ├─→ EpirocPulse.Core (models and contracts)
    └─→ EpirocPulse.Infrastructure (system abstractions)

EpirocPulse.Reporting
    ↓
    ├─→ EpirocPulse.Core (models)
    └─→ EpirocPulse.Infrastructure (file I/O)

EpirocPulse.Infrastructure
    ↓
    └─→ EpirocPulse.Core (models only)

EpirocPulse.Core
    ↓
    └─→ (No dependencies)
```

### Forbidden Dependencies

- **No circular dependencies** – DAG (Directed Acyclic Graph) structure
- **App must NOT depend on Reporting** – Reports are generated on demand
- **Diagnostics must NOT depend on App** – Services are UI-agnostic
- **Infrastructure must NOT depend on Diagnostics** – Abstractions must be isolated
- **Core must NOT depend on anything** – Core is the foundation

### Dependency Injection

Services are registered in the App layer and injected into ViewModels:

```csharp
// In App.xaml.cs or composition root
var networkDiagnosticsService = new NetworkDiagnosticsService(
    adapterDetection,
    gatewayCheck,
    deviceReachability,
    tcpPortCheck,
    issueDetection
);

MainWindow.DataContext = new DashboardViewModel(
    networkDiagnosticsService,
    reportGenerator
);
```

---

## Diagnostic Workflow

### High-Level Flow

```
User Initiates Scan
    ↓
App calls DiagnosticsService.RunDiagnosticsAsync()
    ↓
Service orchestrates sequence:
    1. Detect adapters
    2. Analyze adapter configuration
    3. Test gateway reachability
    4. Test device reachability
    5. Test TCP ports
    6. Detect common issues
    ↓
Each step returns DiagnosticResult
    ↓
Results aggregated into DiagnosticSuite
    ↓
ViewModel updates UI with badges and details
    ↓
User can export or drill into results
```

### Diagnostic Services

1. **INetworkDiagnosticsService** (Orchestrator)
   - Entry point for all diagnostics
   - Coordinates lower-level services
   - Returns `DiagnosticSuite` containing all results
   - Handles error states gracefully

2. **IAdapterDetectionService**
   - Lists active network adapters
   - Returns adapter properties (IP, gateway, DNS, status)
   - Detects physical vs. virtual adapters

3. **IGatewayCheckService**
   - Pings the default gateway
   - Verifies gateway is reachable
   - Returns latency data

4. **IDeviceReachabilityService**
   - Pings the Epiroc machine IP
   - Tests if machine is on the network
   - Provides routing diagnostics

5. **ITcpPortCheckService**
   - Tests connectivity to specific ports
   - Used for PLC and service discovery
   - Returns success/failure and latency

6. **INetworkIssueDetectionService**
   - Detects APIPA addresses (169.254.x.x)
   - Detects wrong subnet configuration
   - Detects multiple active adapters
   - Detects VPN interference
   - Detects DNS issues

### Result Structure

Each step returns a `DiagnosticResult`:

```csharp
public class DiagnosticResult
{
    public string TestName { get; set; }
    public DiagnosticStatus Status { get; set; }        // Pass, Fail, Warning, etc.
    public string Summary { get; set; }                 // "Gateway reachable"
    public string TechnicalDetail { get; set; }         // "192.168.1.1 responded in 12ms"
    public string SuggestedAction { get; set; }         // "If gateway unreachable, check physical connection"
    public DateTime Timestamp { get; set; }
    public string RawOutput { get; set; }               // Optional raw ping/tracert output
}
```

---

## Reporting Workflow

### Report Generation Flow

```
User clicks "Export Report"
    ↓
App calls ReportGenerator.GenerateReportAsync(diagnosticSuite)
    ↓
Generator selects appropriate template
    ↓
Template formats results as Markdown:
    - Summary section
    - Detailed results table
    - Status badges
    - Suggested actions
    - Timestamps
    ↓
Report written to file via Infrastructure.FileService
    ↓
UI shows confirmation and file location
```

### Report Structure

```markdown
# Epiroc Pulse - Network Diagnostic Report

**Scan Date:** 2026-07-27 00:24:58  
**Technician:** [Optional]  
**Machine IP:** 192.168.1.100  

## Summary

| Test | Status | Summary |
|------|--------|---------|
| Network Adapter Detection | ✓ Pass | 2 adapters detected |
| Gateway Reachability | ✓ Pass | 192.168.1.1 responding |
| Device Reachability | ✓ Pass | Machine responding on 192.168.1.100 |
| TCP Port 502 | ⚠ Warning | Slow response (250ms) |

## Detailed Results

### Network Adapter Detection
**Status:** Pass  
**Summary:** 2 network adapters detected (Ethernet and WiFi)  
**Technical Detail:**  
- Adapter 1: Ethernet (192.168.1.50/24, Gateway 192.168.1.1)
- Adapter 2: WiFi (10.0.0.50/24, Gateway 10.0.0.1) - **Warning: Multiple adapters**

**Suggested Action:** Disable WiFi if not needed to reduce network confusion.

### Gateway Reachability
**Status:** Pass  
**Summary:** Default gateway is reachable  
**Technical Detail:** 192.168.1.1 responded in 12ms  
**Suggested Action:** Gateway is working correctly.

## Recommended Next Steps

1. [Ordered list of actions based on results]
2. [Links to troubleshooting guides]
3. [Contact information if needed]
```

### Report Services

- **IReportGenerator** – Main interface
- **IMarkdownReportFormatter** – Markdown-specific formatting
- **IReportTemplate** – Template selection and layout

---

## Packet Capture Architecture

### Current State (Phase 0-4)

Packet capture is **not yet implemented** but architecture must support it.

### Future Design (Phase 6)

```
┌─────────────────────────────────────────┐
│       User Initiates Packet Capture     │
└──────────────────┬──────────────────────┘
                   ↓
         ┌─────────────────────┐
         │  App ViewModel      │
         │ (UI Coordination)   │
         └──────────┬──────────┘
                    ↓
         ┌─────────────────────────────────────────┐
         │  IPacketCaptureOrchestrator             │
         │  (in EpirocPulse.Diagnostics)          │
         │  - Start/Stop capture                  │
         │  - Apply filters                       │
         │  - Manage capture files                │
         └──────────────────┬──────────────────────┘
                            ↓
         ┌─────────────────────────────────────────┐
         │  IPacketCaptureService                  │
         │  (in EpirocPulse.Infrastructure)        │
         │  - Native Windows implementation        │
         │  - netsh trace or WinPcap abstraction  │
         │  - Raw PCAP file handling              │
         └─────────────────────────────────────────┘
```

### Packet Capture Abstractions (TBD)

```csharp
// In EpirocPulse.Infrastructure
public interface IPacketCaptureService
{
    Task StartCaptureAsync(string outputFilePath, CaptureFilter filter);
    Task StopCaptureAsync();
    Task<bool> IsCaptureRunningAsync();
    Task<string> GetCaptureStatusAsync();
}

// In EpirocPulse.Core (domain model)
public class CaptureFilter
{
    public string Protocol { get; set; }         // "TCP", "UDP", "ICMP"
    public int? Port { get; set; }
    public string SourceIP { get; set; }
    public string DestinationIP { get; set; }
}

// In EpirocPulse.Diagnostics
public interface IPacketCaptureOrchestrator
{
    Task StartDiagnosticCaptureAsync(string deviceIP);
    Task StopCaptureAndAnalyzeAsync();
    DiagnosticResult AnalyzeCaptureFile(string pcapPath);
}
```

### Implementation Strategy

1. **Windows Native** – Use Windows native tools (netsh trace, WinPcap, or built-in tools)
2. **No External Runtimes** – Avoid third-party packet capture libraries that require separate installation
3. **Abstracted Interface** – Hide implementation details behind `IPacketCaptureService`
4. **Background Operation** – Capture runs asynchronously without blocking UI
5. **File Output** – PCAP or ETL format that can be analyzed locally or sent to Epiroc support

---

## Agent Ownership Boundaries

### Architecture Agent

**Owner:** Architecture Agent  
**Responsibilities:**
- Maintain ARCHITECTURE.md
- Maintain DECISION_LOG.md
- Define layer boundaries and module responsibilities
- Review cross-layer dependency violations
- Coordinate architectural decisions

**Files Owned:**
- `docs/ARCHITECTURE.md`
- `docs/DECISION_LOG.md`

**Files Off-Limits:**
- Implementation code (src/)
- UI files (XAML, code-behind)
- Tests (unless architectural)

---

### UI Agent

**Owner:** UI Agent (Future)  
**Responsibilities:**
- Implement MainWindow and views
- Implement ViewModels
- Manage WPF bindings and commands
- Implement navigation
- Render diagnostic badges and results

**Files Owned:**
- `src/EpirocPulse.App/**`
- `src/EpirocPulse.App/ViewModels/**`

**Dependencies:**
- Can depend on EpirocPulse.Core and EpirocPulse.Diagnostics
- Must use dependency injection
- Must not bypass abstractions

---

### Diagnostics Agent

**Owner:** Diagnostics Agent (Future)  
**Responsibilities:**
- Implement diagnostic services
- Orchestrate multi-step diagnostics
- Implement network issue detection logic
- Maintain diagnostic quality and accuracy

**Files Owned:**
- `src/EpirocPulse.Diagnostics/**`

**Dependencies:**
- Can depend on EpirocPulse.Core and EpirocPulse.Infrastructure
- Must use Infrastructure abstractions
- Must not depend on App or UI

---

### Infrastructure Agent

**Owner:** Infrastructure Agent (Future)  
**Responsibilities:**
- Implement system abstractions (networking, file I/O, processes)
- Handle platform-specific P/Invoke
- Provide mock implementations for testing
- Ensure testability of system operations

**Files Owned:**
- `src/EpirocPulse.Infrastructure/**`

**Constraints:**
- Must not contain business logic
- Must be focused on abstraction and isolation
- Can only depend on EpirocPulse.Core

---

### Reporting Agent

**Owner:** Reporting Agent (Future)  
**Responsibilities:**
- Implement report generation
- Create report templates
- Format diagnostic results for export
- Support multiple output formats

**Files Owned:**
- `src/EpirocPulse.Reporting/**`

**Dependencies:**
- Can depend on EpirocPulse.Core and EpirocPulse.Infrastructure
- Must not depend on App or UI logic

---

### QA Agent

**Owner:** QA Agent (Future)  
**Responsibilities:**
- Develop test suites
- Ensure diagnostic accuracy
- Validate cross-layer functionality
- Report quality metrics

**Files Owned:**
- `tests/**`

**Coordination:**
- Works with each implementation agent
- Reviews architectural compliance
- Identifies test gaps

---

## Risks and Assumptions

### Key Assumptions

1. **Windows Native Tooling Available**
   - Assumption: Windows provides sufficient native networking APIs and packet capture tools
   - Risk: If native tools are insufficient, may need third-party dependencies
   - Mitigation: Abstract packet capture early; evaluate tools during Phase 6

2. **MVVM Pattern Suitability**
   - Assumption: MVVM is appropriate for WPF diagnostic UI
   - Risk: Complex diagnostic workflows may reveal pattern limitations
   - Mitigation: Review pattern during Phase 2 UI implementation

3. **Technician Skill Level**
   - Assumption: Technicians are comfortable with Windows desktop applications
   - Risk: If diagnostics are too technical, adoption may be low
   - Mitigation: Extensive UX review before release; gather feedback during beta

4. **Network Stability During Diagnostics**
   - Assumption: Network remains stable during diagnostic run
   - Risk: Transient network issues may produce false positives
   - Mitigation: Add retry logic; document limitations in reports

5. **Epiroc Machine IP Known**
   - Assumption: Technician can provide or we can auto-detect machine IP
   - Risk: Auto-detection may be unreliable in complex networks
   - Mitigation: Support manual IP entry; validate before tests

### Key Risks

1. **Cross-Layer Circular Dependencies**
   - **Risk:** Accidental imports creating circular dependencies
   - **Mitigation:** Enforce dependency direction in code reviews; use static analysis

2. **Diagnostic False Positives**
   - **Risk:** Diagnostics incorrectly report issues due to transient conditions
   - **Mitigation:** Add statistical analysis (multiple runs, timeout handling)

3. **Packet Capture Implementation Unknown**
   - **Risk:** Windows native packet capture may not meet requirements
   - **Mitigation:** Research and prototype during Phase 5; adjust architecture if needed

4. **Performance Under Load**
   - **Risk:** Diagnostics may timeout on slow networks
   - **Mitigation:** Configurable timeouts; async/await throughout; progress feedback to user

5. **Multi-Agent Coordination Overhead**
   - **Risk:** Documentation burden may slow development
   - **Mitigation:** Automate handoff checks; maintain clear issue templates

6. **WPF Maintenance Risk**
   - **Risk:** WPF is a legacy framework; may be deprecated in future
   - **Mitigation:** Clean architecture isolates UI; could migrate to WinUI if needed

---

## Future Considerations

### Phase 5-6 Extensions

1. **Machine Profile Templates**
   - Define expected subnets, gateway IPs, PLC ports per machine model
   - Auto-validate against profile during diagnostics

2. **Guided Troubleshooting Flows**
   - Decision trees for common issues (wrong subnet, VPN, multiple adapters)
   - Step-by-step actions for technician

3. **Advanced Network Analysis**
   - Packet inspection and protocol-level diagnostics
   - Traffic analysis and bottleneck detection

4. **Report Integration**
   - Support Epiroc's existing report formats
   - Export to PDF or other formats

### Testing Strategy

- **Unit Tests:** Test each service in isolation with mocks
- **Integration Tests:** Test cross-service workflows
- **UI Tests:** Test ViewModel logic and bindings (no WPF framework)
- **E2E Tests:** Test full diagnostic flow (may require test machine)

---

## Summary

Epiroc Pulse is a cleanly architected Windows desktop application with clear separation of concerns:

- **App** – User interface and coordination
- **Core** – Shared domain models and contracts
- **Diagnostics** – Business logic and workflows
- **Infrastructure** – Testable system abstractions
- **Reporting** – Report generation and export

The architecture enforces a strict dependency hierarchy, enabling multiple agents to work in parallel while maintaining consistency and testability. Future phases (packet capture, advanced diagnostics) are designed with extensibility in mind, allowing new features to be added without restructuring existing layers.
