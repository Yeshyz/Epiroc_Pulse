\# Agent Handoffs - Epiroc Pulse



This file records work that one agent needs from another.



\## Active Handoffs

### HANDOFF-0001

Status: Open  
From Agent: QA Agent  
To Agent: Any Agent (immediate action required)  
Related Issue: N/A  
Related Branch: Main  
Priority: Blocking  

## Request

Delete `src/EpirocPulse.Reporting/Class1.cs` to unblock build.

## Reason

Build currently fails with 11 compilation errors due to duplicate class definition. This blocks all development work until resolved.

## Acceptance Criteria

- \[ ] Class1.cs deleted from EpirocPulse.Reporting project
- \[ ] `dotnet build` completes successfully
- \[ ] No compilation errors
- \[ ] All test projects compile

## Files Allowed

- `src/EpirocPulse.Reporting/Class1.cs` (for deletion only)

## Files Not Allowed

- Any production source files
- Architecture or documentation files

## Completion Notes

(To be filled when task complete)

---

### HANDOFF-0002

Status: Open  
From Agent: QA Agent  
To Agent: Infrastructure Agent  
Related Issue: N/A  
Related Branch: Main  
Priority: High  

## Request

Implement Infrastructure layer abstractions and concrete implementations.

## Reason

Infrastructure abstractions are prerequisites for Diagnostics implementation. Currently placeholder only.

## Acceptance Criteria

- \[ ] INetworkClient interface defined
- \[ ] IProcessExecutor interface defined
- \[ ] IFileService interface defined
- \[ ] ISystemInformation interface defined
- \[ ] Windows implementations provided
- \[ ] Mock implementations for testing
- \[ ] 90%+ test coverage for Infrastructure layer
- \[ ] No compilation warnings

## Files Allowed

- `src/EpirocPulse.Infrastructure/**`
- `tests/EpirocPulse.Infrastructure.Tests/**` (to be created)

## Files Not Allowed

- Diagnostics layer files
- App layer files
- Core layer files

## Completion Notes

(To be filled when task complete)

---

### HANDOFF-0003

Status: Open  
From Agent: QA Agent  
To Agent: Diagnostics Agent  
Related Issue: N/A  
Related Branch: Main  
Priority: High  

## Request

Implement diagnostic services and orchestration logic.

## Reason

Core functionality of Epiroc Pulse. Diagnostics currently placeholder only.

## Acceptance Criteria

- \[ ] INetworkDiagnosticsService (orchestrator) implemented
- \[ ] IAdapterDetectionService implemented
- \[ ] IGatewayCheckService implemented
- \[ ] IDeviceReachabilityService implemented
- \[ ] ITcpPortCheckService implemented
- \[ ] INetworkIssueDetectionService implemented
- \[ ] All services use Infrastructure abstractions
- \[ ] 90%+ test coverage with mocked Infrastructure
- \[ ] All services return DiagnosticResult with proper fields

## Files Allowed

- `src/EpirocPulse.Diagnostics/**`
- `tests/EpirocPulse.Diagnostics.Tests/**`

## Files Not Allowed

- Infrastructure files
- App files
- Core files
- Reporting files

## Completion Notes

(To be filled when task complete)

---

### HANDOFF-0004

Status: Open  
From Agent: QA Agent  
To Agent: UI Agent  
Related Issue: N/A  
Related Branch: Main  
Priority: High  

## Request

Implement ViewModels and wire UI to diagnostic services.

## Reason

UI shell exists but is non-functional. Requires ViewModels and dependency injection.

## Acceptance Criteria

- \[ ] DashboardViewModel implemented with INotifyPropertyChanged
- \[ ] DiagnosticsViewModel with service integration
- \[ ] ReportsViewModel with export logic
- \[ ] SettingsViewModel with preferences
- \[ ] HelpViewModel with documentation
- \[ ] All ViewModels properly bound to Views
- \[ ] Command handling implemented
- \[ ] Status badges display correctly
- \[ ] No code-behind logic (MVVM compliant)

## Files Allowed

- `src/EpirocPulse.App/ViewModels/**` (to be created)
- `src/EpirocPulse.App/Views/**` (for view modifications)

## Files Not Allowed

- Other layer files
- Core files
- Infrastructure files

## Completion Notes

(To be filled when task complete)

---

### HANDOFF-0005

Status: Open  
From Agent: QA Agent  
To Agent: Architecture Agent  
Related Issue: N/A  
Related Branch: Main  
Priority: Medium  

## Request

Setup dependency injection container and composition root in App layer.

## Reason

Services need to be registered and injected into ViewModels. Currently no DI setup.

## Acceptance Criteria

- \[ ] DI container configured (Microsoft.Extensions.DependencyInjection or equivalent)
- \[ ] All services registered
- \[ ] ViewModels registered
- \[ ] Composition root in App.xaml.cs or startup
- \[ ] No circular dependencies
- \[ ] All references in architecture decision logs

## Files Allowed

- `src/EpirocPulse.App/App.xaml.cs`
- `src/EpirocPulse.App/App.xaml`
- Configuration files added as needed

## Files Not Allowed

- Production service files
- Core files

## Completion Notes

(To be filled when task complete)



\## Handoff Template



\### HANDOFF-0000



Status: Open / In Progress / Complete  

From Agent:  

To Agent:  

Related Issue:  

Related Branch:  

Priority: Low / Medium / High / Blocking  



\## Request



Describe what needs to be done.



\## Reason



Explain why this is needed.



\## Acceptance Criteria



\- \[ ] Requirement 1

\- \[ ] Requirement 2

\- \[ ] Requirement 3



\## Files Allowed



\- List files/folders



\## Files Not Allowed



\- List files/folders



\## Completion Notes



Add notes when complete.

