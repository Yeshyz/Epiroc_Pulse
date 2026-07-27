\# Agent Handoffs - Epiroc Pulse



This file records work that one agent needs from another.



\## Active Handoffs



### HANDOFF-0001 - Dashboard Adapter Display



Status: Open  
From Agent: Diagnostics  
To Agent: UI  
Related Issue: #TODO (Dashboard Adapter Integration)  
Related Branch: feature/dashboard-adapter-display  
Priority: High / Blocking  



## Request

Implement UI components to display network adapter information on the application dashboard after Network Adapter Detection service is merged.



## Reason

UI cannot proceed with adapter display until Diagnostics Agent completes and merges the Network Adapter Detection service, which provides the data model and detection logic.



## Acceptance Criteria

- [ ] Adapter list displays on dashboard with name, status, and IP information
- [ ] Adapter details view shows extended information (MAC address, speed, gateway)
- [ ] Adapter status updates reflect diagnostic results from service
- [ ] UI gracefully handles no adapters found scenario
- [ ] All UI tests pass



## Files Allowed

- `src/EpirocPulse.App/ViewModels/AdapterViewModel.cs`
- `src/EpirocPulse.App/Views/AdapterPanel.xaml`
- `src/EpirocPulse.App/Views/AdapterPanel.xaml.cs`
- `tests/EpirocPulse.App.Tests/ViewModels/AdapterViewModelTests.cs`
- `tests/EpirocPulse.App.Tests/Views/AdapterPanelTests.cs`



## Files Not Allowed

- `src/EpirocPulse.Diagnostics/`
- `src/EpirocPulse.Reporting/`
- `src/EpirocPulse.Infrastructure/`
- `docs/AGENT_STATUS_BOARD.md`
- `docs/BLOCKERS.md`
- `CHANGELOG.md`



## Completion Notes

Add notes when complete.



---



### HANDOFF-0002 - Adapter Report Integration



Status: Open  
From Agent: Diagnostics  
To Agent: Reporting  
Related Issue: #TODO (Adapter Report Integration)  
Related Branch: feature/adapter-reporting  
Priority: High / Blocking  



## Request

Integrate network adapter detection results into Markdown diagnostic reports after Network Adapter Detection service is merged.



## Reason

Reporting cannot include adapter diagnostics until Diagnostics Agent completes and merges the Network Adapter Detection service with its result models and detection output.



## Acceptance Criteria

- [ ] Adapter section included in diagnostic report template
- [ ] Report includes adapter name, status, IP address, and MAC address
- [ ] Report shows adapter diagnostic summary (pass/warning/fail status)
- [ ] Report includes raw adapter output in details section
- [ ] Report generation tests pass
- [ ] Generated reports validate against schema



## Files Allowed

- `src/EpirocPulse.Reporting/Templates/DiagnosticReportTemplate.md`
- `src/EpirocPulse.Reporting/Builders/AdapterReportBuilder.cs`
- `src/EpirocPulse.Reporting/Models/AdapterReportSection.cs`
- `tests/EpirocPulse.Reporting.Tests/Builders/AdapterReportBuilderTests.cs`
- `tests/EpirocPulse.Reporting.Tests/Models/AdapterReportSectionTests.cs`



## Files Not Allowed

- `src/EpirocPulse.Diagnostics/`
- `src/EpirocPulse.App/`
- `src/EpirocPulse.Infrastructure/`
- `docs/AGENT_STATUS_BOARD.md`
- `docs/BLOCKERS.md`
- `CHANGELOG.md`



## Completion Notes

Add notes when complete.



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

