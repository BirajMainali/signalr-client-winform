using Microsoft.AspNetCore.SignalR.Client;

namespace win_form_client;

public partial class Home : Form
{
    HubConnection connection;

    public Home()
    {
        InitializeComponent();
        connection = new HubConnectionBuilder().WithUrl("https://localhost:7202/NotificationHub")
            .WithAutomaticReconnect()
            .Build();
        connection.Closed += async (error) =>
        {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await connection.StartAsync();
        };
        Load += OnFormLoad;
    }

    private async void OnFormLoad(object? sender, EventArgs e)
    {
        try
        {
            connection.On<string>("onOrder", (message) =>
            {
                // When onOrder is received from server -> do this.
                MessageBox.Show(message);
            });
            
            await connection.StartAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}