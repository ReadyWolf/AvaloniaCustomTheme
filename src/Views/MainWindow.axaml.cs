using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Platform;
using AvaloniaCustomTheme.Models;
using AvaloniaCustomTheme.ViewModels;
namespace AvaloniaCustomTheme.Views
{
    public partial class MainWindow : Window
    {
        MainWindowViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = DIUtils.GetRequiredService<MainWindowViewModel>();
            ApplyTheme(null, null);
        }

        // Hack - we have to apply themes from the code behind as we need to refresh the View. Which the ViewModel can't do without some issues...
        public void ApplyTheme(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MainWindowViewModel;
            viewModel?.ApplyThemeFromView();

            DataContext = DIUtils.GetRequiredService<MainWindowViewModel>();
            this.Content = null;

            InitializeComponent();
            DataContext = DIUtils.GetRequiredService<MainWindowViewModel>();


            this.ExtendClientAreaTitleBarHeightHint = -1;


        }

        public void LoadSettingsAndApplyTheme(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MainWindowViewModel;
            viewModel?.LoadThemeSettingsFromFile();
            ApplyTheme(sender, e);
        }

    }
}