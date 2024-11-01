
using Avalonia.Styling;
using ReactiveUI;
using System.Reactive;
using Avalonia.Themes.Fluent;
using AvaloniaCustomTheme.Models;
using Avalonia;
using AvaloniaCustomTheme.Models.DTOS;
using System.Threading.Tasks;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using Avalonia.Threading;
using Microsoft.VisualBasic;
using DynamicData;
using AvaloniaCustomTheme.Models.Services;
using Avalonia.Media;

namespace AvaloniaCustomTheme.ViewModels
{
 

    public partial class MainWindowViewModel : ViewModelBase
    {

        // SERVICES 

        // This should be a service and put into DI
        ThemeController _ThemeManager;
        App _AvaloniaApp;
        AppSettings _AppSettings;
        IFileIOService _FileIOService;
        // App Settings Should be a service

        public ViewModelActivator Activator { get; }

        public ReactiveCommand<Unit, Unit> ClickChangeThemeRaw { get; }
        public ReactiveCommand<Unit, Unit> ClickSaveSettings { get; }
        public ReactiveCommand<Unit, Unit> ClickLoadSettings { get; }


        public ObservableCollection<string> ThemeNames { get; set; }

        private string _selectedTheme;
        public string SelectedTheme
        {
            get => _selectedTheme;
            set => this.RaiseAndSetIfChanged(ref _selectedTheme, value);
        }


        public ObservableCollection<string> ThemeVariants { get; set; }

        private string _selectedThemeVariant;
        public string SelectedThemeVariant
        {
            get => _selectedThemeVariant;
            set => this.RaiseAndSetIfChanged(ref _selectedThemeVariant, value);
        }
        private int _transparencyValue;
        public int TransparencyValue
        {
            get => _transparencyValue;
            set => this.RaiseAndSetIfChanged(ref _transparencyValue, value);
        }
     
        private string _footerMessageText;

        public string FooterMessageText
        {
            get => _footerMessageText;
            set => this.RaiseAndSetIfChanged(ref _footerMessageText, value);

        }



        public MainWindowViewModel()
        {
            _ThemeManager = DIUtils.GetRequiredService<ThemeController>();
            _AvaloniaApp = DIUtils.GetRequiredService<App>();
            _AppSettings = DIUtils.GetRequiredService<AppSettings>();
            _FileIOService = DIUtils.GetRequiredService<IFileIOService>();

            ThemeNames = _ThemeManager.GetThemeNames();
             
            ThemeVariants = new ObservableCollection<string> { "Default", "Light", "Dark" };

            /* load in defaults here */
            RefreshCurrentValues();

            ClickChangeThemeRaw = ReactiveCommand.Create(() => UpdateThemeRaw());
            ClickSaveSettings = ReactiveCommand.Create(() => SaveThemeSettingsToFile());
            ClickLoadSettings = ReactiveCommand.Create(() => LoadThemeSettingsFromFile());

        }

        private void UpdateThemeRaw()
        {

            _AvaloniaApp.ApplyThemeRaw();
        }

        public void ApplyThemeFromView()
        {

            SetTheme(SelectedTheme, SelectedThemeVariant, TransparencyValue);
            // Update Settings
            _AppSettings.ThemeCurrentName = SelectedTheme;
            _AppSettings.ThemeVariant = SelectedThemeVariant;
            _AppSettings.ThemeTransparency = TransparencyValue;

        }

        private void RefreshCurrentValues()
        {
            int themeIndex = ThemeNames.IndexOf(_AppSettings.ThemeCurrentName) != -1 ? ThemeNames.IndexOf(_AppSettings.ThemeCurrentName) : 0;
            SelectedTheme = ThemeNames[themeIndex];
            int selectedThemeIndex = ThemeVariants.IndexOf(_AppSettings.ThemeVariant) != -1 ? ThemeVariants.IndexOf(_AppSettings.ThemeVariant) : 0;
            SelectedThemeVariant = ThemeVariants[selectedThemeIndex];
            TransparencyValue = _AppSettings.ThemeTransparency;

        }

        public void LoadThemeSettingsFromFile()
        {
            AppSettings loadedAppSettings = _FileIOService.LoadAppSettingsFromFile();

            _AppSettings.UpdateAppSettings(loadedAppSettings);
            RefreshCurrentValues();
            FooterMessageText = $"Settings Loaded";
          
        }

        private void SaveThemeSettingsToFile()
        {
            _FileIOService.SaveAppSettingsToFileAsync(_AppSettings);
            FooterMessageText = $"Settings Saved";
        }

        private void SetTheme(String themeName, String themeVariantName, int themeTransparencyValue)
        {
            
            ThemeVariant themeVariant = ThemeVariant.Default;

            switch (themeVariantName)
            {
                case null:
                case "Default":
                    themeVariant = ThemeVariant.Default; break;
                case "Dark":
                    themeVariant = ThemeVariant.Dark; break;
                case "Light":
                    themeVariant = ThemeVariant.Light; break;
            
            }

           
            try
            {
                  _AvaloniaApp.ApplyTheme(_ThemeManager.themes.Themes[themeName], themeVariant, themeTransparencyValue);
            }
            catch (NullReferenceException ex)
            {
                FooterMessageText = ("NullReferenceException: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                FooterMessageText = "An error occurred: " + ex.Message;
                throw new NullReferenceException("The theme or theme manager is null.", ex);


            }
            
            FooterMessageText = $"{themeName} with variant {themeVariantName} successfully applied";
        }

    }

 

}
