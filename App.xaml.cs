using System.IO;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecallFinanceApp.Models;
using RecallFinanceApp.ViewModels;
using RecallFinanceApp.Views;

namespace RecallFinanceApp;

public partial class App : Application
{
    private readonly IHost _host;
    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json");
                config.AddUserSecrets<App>();
            })
            .ConfigureServices((context, services) =>
            {
                var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

                services.AddDbContext<RecallDbContext>(options =>
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

                services.AddSingleton<MainViewModel>();
                services.AddTransient<MainWindow>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        await InitializeApplicationAsync();
    }

    private async Task InitializeApplicationAsync()
    {
        await _host.StartAsync();

        using (var scope = _host.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<RecallDbContext>();
            try
            {
                bool canConnect = await dbContext.Database.CanConnectAsync();
                if (!canConnect)
                {
                    ShowErrorAndExit("Database connection failed! Check your MySQL service.");
                    return;
                }
            }
            catch (Exception ex)
            {
                ShowErrorAndExit($"Critical database error: {ex.Message}");
                return;
            }
        }

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        var mainVM = _host.Services.GetRequiredService<MainViewModel>();

        mainWindow.DataContext = mainVM;
        mainWindow.Show();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _host.StopAsync();
        _host.Dispose();
        base.OnExit(e);
    }

    private static void ShowErrorAndExit(string message)
    {
        MessageBox.Show(message, "Startup Error", MessageBoxButton.OK, MessageBoxImage.Error);
        Current.Shutdown();
    }
}