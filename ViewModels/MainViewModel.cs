using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RecallFinanceApp.Views;

namespace RecallFinanceApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private object? _currentView;
        private decimal _balance = 1500.50m;

        public decimal Balance
        {
            get => _balance;
            set { _balance = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> Transactions { get; set; } = new();

        public object? CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            CurrentView = new DashboardView();
        }

        public void UpdateBalance()
        {
            Balance += 100.25m;
            Transactions.Insert(0, $"Sistem: Portföy Değeri Güncellendi - {DateTime.Now:HH:mm:ss}");
        }

        public void ShowSettings() => CurrentView = new SettingsView();
        public void ShowDashboard() => CurrentView = new DashboardView();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}