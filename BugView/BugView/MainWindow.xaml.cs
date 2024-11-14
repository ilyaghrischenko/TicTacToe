using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using TicTacToe.Domain.DbModels;

namespace BugView;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        LoadBugsAsync();
    }

    private async Task LoadBugsAsync()
    {
        using HttpClient client = new HttpClient();
        string apiUrl = "http://192.168.1.77/api/Bug/getBugs";
            
        try
        {
            var bugs = await client.GetFromJsonAsync<List<Bug>>(apiUrl);
            BugDataGrid.ItemsSource = bugs;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading bugs: {ex.Message}");
        }
    }
}