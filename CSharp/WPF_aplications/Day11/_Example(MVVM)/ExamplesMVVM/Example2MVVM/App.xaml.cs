using System.Windows;
using MVVM_Sample.ViewModel;

namespace MVVM_Sample
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //MainWindow window = new MainWindow();
            //MainWindowViewModel viewModel = new MainWindowViewModel();
            //window.DataContext = viewModel;
            //window.Show();
        }
    }
}
