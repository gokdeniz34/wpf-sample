using System.Windows;
using System.Windows.Controls;
using RecallFinanceApp.ViewModels;

namespace RecallFinanceApp.Views
{
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            // Dashboard, MainWindow içinde yaşadığı için Window üzerinden ViewModel'e ulaşıyoruz
            var mainWindow = Window.GetWindow(this);
            if (mainWindow?.DataContext is MainViewModel vm)
            {
                vm.UpdateBalance();
            }
        }
    }
}