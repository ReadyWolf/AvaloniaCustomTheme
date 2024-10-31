using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;
using AvaloniaCustomTheme.Models;
using AvaloniaCustomTheme.ViewModels;
using AvaloniaCustomTheme.Views;
using Avalonia.Media;
using Avalonia.Controls;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using AvaloniaCustomTheme.Models.DTOS;
using Avalonia.Threading;
using Splat;
using AvaloniaCustomTheme.Models.Services;

namespace AvaloniaCustomTheme
{
    public partial class App : Application
    {


        public void ApplyTheme(Theme theme, ThemeVariant variant, int transparency)
        {

            var applyTheme = new FluentTheme()
                {
                    Palettes =
                {
                    [ThemeVariant.Light] =  ThemeConverter.ToColorPaletteResources(theme.Light, transparency),
                    [ThemeVariant.Dark] = ThemeConverter.ToColorPaletteResources(theme.Dark, transparency)
                },
                };

                this.RequestedThemeVariant = variant; // can be ThemeVariant.Default, ThemeVariant.Dark ThemeVariant.Light;
                Styles.Clear();
                Styles.Add(applyTheme);

        }


        // This function breaks Avalonia. Had to do some codebehind the view hack to get arround it
        public void ApplyThemeRaw()
        {
                // Remove previously present style
                Styles.Clear();

                // Define new themes
                var lightPalette = new ColorPaletteResources
                {
                    Accent = Color.Parse("#ffdf992e"),
                    AltHigh = Colors.White,
                    AltLow = Colors.White,
                    AltMedium = Colors.White,
                    AltMediumHigh = Colors.White,
                    AltMediumLow = Colors.White,
                    BaseHigh = Colors.Black,
                    BaseLow = Color.Parse("#ff8a779b"),
                    BaseMedium = Color.Parse("#ff584271"),
                    BaseMediumHigh = Color.Parse("#ff361e55"),
                    BaseMediumLow = Color.Parse("#ff473063"),
                    ChromeAltLow = Color.Parse("#ff361e55"),
                    ChromeBlackHigh = Colors.Black,
                    ChromeBlackLow = Color.Parse("#ff8a779b"),
                    ChromeBlackMedium = Color.Parse("#ff361e55"),
                    ChromeBlackMediumLow = Color.Parse("#ff584271"),
                    ChromeDisabledHigh = Color.Parse("#ff8a779b"),
                    ChromeDisabledLow = Color.Parse("#ff584271"),
                    ChromeGray = Color.Parse("#ff473063"),
                    ChromeHigh = Color.Parse("#ff8a779b"),
                    ChromeLow = Color.Parse("#ffd8cbe3"),
                    ChromeMedium = Color.Parse("#ffc8bad5"),
                    ChromeMediumLow = Color.Parse("#ffd8cbe3"),
                    ChromeWhite = Colors.White,
                    ListLow = Color.Parse("#ffc8bad5"),
                    ListMedium = Color.Parse("#ff8a779b"),
                    RegionColor = Color.Parse("#ffdfe4ff")
                };

                var darkPalette = new ColorPaletteResources
                {
                    Accent = Color.Parse("#ffcfa12e"),
                    AltHigh = Colors.Black,
                    AltLow = Colors.Black,
                    AltMedium = Colors.Black,
                    AltMediumHigh = Colors.Black,
                    AltMediumLow = Colors.Black,
                    BaseHigh = Colors.White,
                    BaseLow = Color.Parse("#ff532e64"),
                    BaseMedium = Color.Parse("#ffad96b9"),
                    BaseMediumHigh = Color.Parse("#ffc3b0ce"),
                    BaseMediumLow = Color.Parse("#ff80628e"),
                    ChromeAltLow = Color.Parse("#ffc3b0ce"),
                    ChromeBlackHigh = Colors.Black,
                    ChromeBlackLow = Color.Parse("#ffc3b0ce"),
                    ChromeBlackMedium = Colors.Black,
                    ChromeBlackMediumLow = Colors.Black,
                    ChromeDisabledHigh = Color.Parse("#ff532e64"),
                    ChromeDisabledLow = Color.Parse("#ffad96b9"),
                    ChromeGray = Color.Parse("#ff967ca4"),
                    ChromeHigh = Color.Parse("#ff967ca4"),
                    ChromeLow = Color.Parse("#ff2a0c3c"),
                    ChromeMedium = Color.Parse("#ff341446"),
                    ChromeMediumLow = Color.Parse("#ff49255a"),
                    ChromeWhite = Colors.White,
                    ListLow = Color.Parse("#ff341446"),
                    ListMedium = Color.Parse("#ff532e64"),
                    RegionColor = Color.Parse("#ff061527")
                };

                // Apply the new theme
                var theme = new FluentTheme()
                {
                    Palettes =
                    {
                        { ThemeVariant.Light, lightPalette },
                        { ThemeVariant.Dark, darkPalette }
                    }
                };

                Styles.Add(theme);

                // Optionally set the requested theme variant
                this.RequestedThemeVariant = ThemeVariant.Default; // or ThemeVariant.Dark or ThemeVariant.Light or ThemeVariant.Default
 
                
         }



        Bootstrapper Bootstrapper;

        public override void Initialize()
        {

            AvaloniaXamlLoader.Load(this);
         
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {

                Bootstrapper = new Bootstrapper();
                // Add the App to the DI services
                Locator.CurrentMutable.RegisterLazySingleton(() => this, typeof(App));

               // MainWindowViewModel _MainWindowViewModel = DIUtils.GetRequiredService<MainWindowViewModel>();
                // Line below is needed to remove Avalonia data validation.
                // Without this line you will get duplicate validations from both Avalonia and CT
                // BindingPlugins.DataValidators.RemoveAt(0);
                desktop.MainWindow = new MainWindow
                {
                    DataContext = DIUtils.GetRequiredService<MainWindowViewModel>()
                };


                // Run extra operations on Shutdown
                desktop.ShutdownRequested += DesktopOnShutdownRequested;
            }

        base.OnFrameworkInitializationCompleted();
        }

        // We want to save our ToDoList before we actually shutdown the App. As File I/O is async, we need to wait until file is closed
        // before we can actually close this window

        private bool _canClose; // This flag is used to check if window is allowed to close
        private async void DesktopOnShutdownRequested(object? sender, ShutdownRequestedEventArgs e)
        {
            e.Cancel = !_canClose; // cancel closing event first time

            if (!_canClose)
            {
                IFileIOService _fileIOService = DIUtils.GetRequiredService<IFileIOService>();
                AppSettings appSettings = DIUtils.GetRequiredService < AppSettings>();

                await _fileIOService.SaveAppSettingsToFileAsync(appSettings);

                // Set _canClose to true and Close this Window again
                _canClose = true;
                if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    desktop.Shutdown();
                }
            }
        }



    }


}