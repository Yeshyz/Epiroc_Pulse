\# Blockers - Epiroc Pulse



This file tracks anything preventing an agent from continuing.



\## Active Blockers



### BLOCKER-0001 - UI Agent Blocked by Adapter Detection



Status: Open  
Blocking Agent: UI  
Required Agent: Diagnostics  
Related Issue: #TODO (Dashboard Adapter Integration)  
Related Branch: feature/adapter-detection  



## Problem

UI Agent cannot begin Dashboard Adapter Integration work because the Network Adapter Detection service has not been completed and merged, which is required to provide the data model and detection results.



## Required Resolution

Diagnostics Agent must complete and merge feature/adapter-detection into main, providing:
- Complete Network Adapter Detection service
- Adapter data models
- Detection result models
- Service interfaces for UI/Reporting consumption



## Files Expected

- `src/EpirocPulse.Diagnostics/Services/NetworkAdapterDetectionService.cs`
- `src/EpirocPulse.Core/Models/NetworkAdapter.cs`
- `src/EpirocPulse.Core/Models/DiagnosticResult.cs` (updated)



## Resolution Status

In Progress



---



### BLOCKER-0002 - Reporting Agent Blocked by Adapter Detection



Status: Open  
Blocking Agent: Reporting  
Required Agent: Diagnostics  
Related Issue: #TODO (Adapter Report Integration)  
Related Branch: feature/adapter-detection  



## Problem

Reporting Agent cannot begin Adapter Report Integration work because the Network Adapter Detection service has not been completed and merged, which is required to understand the adapter data structure and detection output format.



## Required Resolution

Diagnostics Agent must complete and merge feature/adapter-detection into main, providing:
- Complete Network Adapter Detection service
- Adapter data models
- Detection result models and output format
- Service interfaces for UI/Reporting consumption



## Files Expected

- `src/EpirocPulse.Diagnostics/Services/NetworkAdapterDetectionService.cs`
- `src/EpirocPulse.Core/Models/NetworkAdapter.cs`
- `src/EpirocPulse.Core/Models/DiagnosticResult.cs` (updated)



## Resolution Status

In Progress



---



### BLOCKER-0003 - QA Agent Blocked by Multiple Dependencies



Status: Open  
Blocking Agent: QA  
Required Agent: Diagnostics, UI, Reporting  
Related Issue: #TODO (Adapter Detection QA Validation)  
Related Branch: feature/adapter-detection, feature/dashboard-adapter-display, feature/adapter-reporting  



## Problem

QA Agent cannot begin Adapter Detection QA Validation because three dependent work items must be completed and merged first:
1. Network Adapter Detection service (Diagnostics)
2. Dashboard Adapter Display (UI)
3. Adapter Report Integration (Reporting)



## Required Resolution

All three agents must complete their work and merge to main:
1. Diagnostics Agent: Complete feature/adapter-detection
2. UI Agent: Complete feature/dashboard-adapter-display and merge
3. Reporting Agent: Complete feature/adapter-reporting and merge

QA can then proceed with comprehensive validation across all three components.



## Files Expected

- `src/EpirocPulse.Diagnostics/Services/NetworkAdapterDetectionService.cs`
- `src/EpirocPulse.App/ViewModels/AdapterViewModel.cs`
- `src/EpirocPulse.App/Views/AdapterPanel.xaml`
- `src/EpirocPulse.Reporting/Builders/AdapterReportBuilder.cs`
- QA test files across all three projects



## Resolution Status

Not Started



\## Blocker Template



\### BLOCKER-0000



Status: Open / Resolved  

Blocking Agent:  

Required Agent:  

Related Issue:  

Related Branch:  



\## Problem



Describe the issue.



\## Required Resolution



Describe what must happen.



\## Files Expected



List files or folders expected to change.



\## Resolution Status



Not Started / In Progress / Complete

