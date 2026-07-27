using System.Windows;
using EpirocPulse.App.Views;

namespace EpirocPulse.App;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        NavigateToDashboard(this, new RoutedEventArgs());
    }

    private void NavigateToDashboard(object sender, RoutedEventArgs e)
    {
        ContentFrame.Navigate(new DashboardView());
        UpdateNavButtonStates("Dashboard");
    }

    private void NavigateToDiagnostics(object sender, RoutedEventArgs e)
    {
        ContentFrame.Navigate(new DiagnosticsView());
        UpdateNavButtonStates("Diagnostics");
    }

    private void NavigateToReports(object sender, RoutedEventArgs e)
    {
        ContentFrame.Navigate(new ReportsView());
        UpdateNavButtonStates("Reports");
    }

    private void NavigateToSettings(object sender, RoutedEventArgs e)
    {
        ContentFrame.Navigate(new SettingsView());
        UpdateNavButtonStates("Settings");
    }

    private void NavigateToHelp(object sender, RoutedEventArgs e)
    {
        ContentFrame.Navigate(new HelpView());
        UpdateNavButtonStates("Help");
    }

    private void UpdateNavButtonStates(string activePage)
    {
        // Reset all buttons
        DashboardBtn.Foreground = (System.Windows.Media.Brush)FindResource("TextLightBrush");
        DashboardBtn.Background = System.Windows.Media.Brushes.Transparent;
        DiagnosticsBtn.Foreground = (System.Windows.Media.Brush)FindResource("TextLightBrush");
        DiagnosticsBtn.Background = System.Windows.Media.Brushes.Transparent;
        ReportsBtn.Foreground = (System.Windows.Media.Brush)FindResource("TextLightBrush");
        ReportsBtn.Background = System.Windows.Media.Brushes.Transparent;
        SettingsBtn.Foreground = (System.Windows.Media.Brush)FindResource("TextLightBrush");
        SettingsBtn.Background = System.Windows.Media.Brushes.Transparent;
        HelpBtn.Foreground = (System.Windows.Media.Brush)FindResource("TextLightBrush");
        HelpBtn.Background = System.Windows.Media.Brushes.Transparent;

        // Highlight active button
        var activeBtn = activePage switch
        {
            "Dashboard" => DashboardBtn,
            "Diagnostics" => DiagnosticsBtn,
            "Reports" => ReportsBtn,
            "Settings" => SettingsBtn,
            "Help" => HelpBtn,
            _ => DashboardBtn
        };

        activeBtn.Foreground = System.Windows.Media.Brushes.White;
        activeBtn.Background = (System.Windows.Media.Brush)FindResource("AccentBrush");
    }
}