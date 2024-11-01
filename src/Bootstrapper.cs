using AvaloniaCustomTheme.Models;
using AvaloniaCustomTheme.Models.DTOS;
using AvaloniaCustomTheme.Models.Services;
using AvaloniaCustomTheme.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaCustomTheme
{
    public class Bootstrapper
    {

        public Bootstrapper()
        {
            ConfigureServices();

            Initialize();
        }

        private static void ConfigureServices()
        {
            // add all your services here 

            var resolver = Locator.CurrentMutable;

            // Create a new Toaster any time someone asks
            //2) Register. Service registered with this name will be created each time requested:
            //Locator.CurrentMutable.Register(() => new Toaster(), typeof(IToaster));

            // Register a singleton instance
            //1) RegisterConstant allows you to inject some constant value of specific type.I use it for injecting configuration:
            //Locator.CurrentMutable.RegisterConstant(new ExtraGoodToaster(), typeof(IToaster));

            // Register a singleton which won't get created until the first user accesses it
            //3) RegisterLazySingleton. Once created, service instance will be reused in future so it acts like a singleton.
            //Locator.CurrentMutable.RegisterLazySingleton(() => new LazyToaster(), typeof(IToaster));

            resolver.RegisterLazySingleton(() => new JsonService(), typeof(IJsonService));
            resolver.RegisterLazySingleton(() => new ThemeController(), typeof(ThemeController));
            resolver.RegisterLazySingleton(() => new AppSettings(), typeof(AppSettings));
            resolver.RegisterLazySingleton(() => new FileIOService(), typeof(IFileIOService));
            resolver.RegisterLazySingleton(() => new MainWindowViewModel(), typeof(MainWindowViewModel));

            //            resolver.Register(() => new BusinessService(Locator.Current.GetService<IRepository>()), typeof(IBusinessService));

        }

        private static async void Initialize()
        {
            // Load in the App Settings
            AppSettings _AppSettings = DIUtils.GetRequiredService<AppSettings>();
            IFileIOService _FileIOService = DIUtils.GetRequiredService<IFileIOService>();

            _FileIOService.InitalizePaths();

            AppSettings loadedAppSettings = _FileIOService.LoadAppSettingsFromFile();

            _AppSettings.UpdateAppSettings(loadedAppSettings);
            
        


        }

    }
}