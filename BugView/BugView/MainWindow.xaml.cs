using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using TicTacToe.DTO.Models;

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
        using var client = new HttpClient();
        string apiUrl = "http://192.168.1.197/api/Bug/getBugs";
        string jwtToken =
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiOCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwibmJmIjoxNzMxNTkyNTc4LCJleHAiOjE3MzE2Nzg5NzgsImlzcyI6Ik15SXNzdWVyIiwiYXVkIjoiTXlBdWRpZW5jZSJ9.D1blorujxlZoVRCzIeBgZK4Df-zoM5sUY9HfdTDzWz0";
            
        try
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);

            var bugs = await client.GetFromJsonAsync<List<object>>(apiUrl);

            var bugsDesetialize = JsonSerializer.Deserialize<List<BugModel>>(
            
            BugDataGrid.ItemsSource = bugsDesetialize;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading bugs: {ex.Message}");
        }
    }
}