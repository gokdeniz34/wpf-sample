using System.Windows;
using RecallFinanceApp.ViewModels;

namespace RecallFinanceApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is MainViewModel vm)
            {
                vm.UpdateBalance();
            }
        }

        private void NavDashboard_Click(object sender, RoutedEventArgs e) => ((ViewModels.MainViewModel)DataContext).ShowDashboard();
        private void NavSettings_Click(object sender, RoutedEventArgs e) => ((ViewModels.MainViewModel)DataContext).ShowSettings();
    }
}