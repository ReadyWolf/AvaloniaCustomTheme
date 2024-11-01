using Avalonia.Controls.Shapes;
using AvaloniaCustomTheme.Models.DTOS;
using AvaloniaCustomTheme.Models.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AvaloniaCustomTheme.Models
{

    public class FileIOService : IFileIOService
    {





        private string _settingsPathFileName = @"{currentDirectory} Settings/Settings.json";
        private string _themesPathFileName = " ";

        /// <summary>
        /// Initializes the file paths for the application settings and themes.
        /// Ensures that the required directories exist before setting the paths.
        /// Logs any errors encountered during initialization.
        /// </summary>
        /// <returns>
        /// Returns `true` if the paths are successfully initialized, otherwise `false`.
        public bool InitalizePaths()
        {

            bool result = false;
            try
            {
                string baseDirectory = AppContext.BaseDirectory; // Where the EXE is
                string currentDirectory = Directory.GetCurrentDirectory(); // Where the Current Directory is

                // Ensure directories exist before setting the paths
                _settingsPathFileName = System.IO.Path.Combine(baseDirectory, "Settings", "Settings.json");
                if (!Directory.Exists(System.IO.Path.GetDirectoryName(_settingsPathFileName)))
                {
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(_settingsPathFileName));
                }


            

            _themesPathFileName = @"M:\DEV\Avalonia\AvaloniaCustomTheme\src\Assets\Themes.json";

                if (!Directory.Exists(System.IO.Path.GetDirectoryName(_themesPathFileName)))
                {
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(_themesPathFileName));
                }
                
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing paths: {ex.Message}");
            }

            return result;
        }
    

        


        public  async Task SaveAppSettingsToFileAsync(AppSettings appSettings)
        {
            IJsonService jsonService = DIUtils.GetRequiredService<IJsonService>();
            // Ensure all directories exists
            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(_settingsPathFileName)!);

            // We use a FileStream to write all items to disc
            await jsonService.WriteJsonFileAsync(_settingsPathFileName, appSettings);
        }


        public bool SaveAppSettingsToFile(AppSettings appSettings)
        {
            bool result = false;
            IJsonService jsonService = DIUtils.GetRequiredService<IJsonService>();
            // Ensure all directories exists
            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(_settingsPathFileName)!);

            // We use a FileStream to write all items to disc
            jsonService.WriteJsonFile(_settingsPathFileName, appSettings);

            result = true;
            return result;

        }

        public  AppSettings LoadAppSettingsFromFile()
        {
            AppSettings appSettings = new AppSettings();

            IJsonService jsonService = DIUtils.GetRequiredService<IJsonService>();
            try
            {
                IJsonService _jsonService = DIUtils.GetRequiredService<IJsonService>();


                appSettings = _jsonService.ReadJsonFile<AppSettings>(_settingsPathFileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading App Settings: {ex.Message}");
            }

            return appSettings;
        }

        public async Task<AppSettings> LoadAppSettingsFromFileAsync()
        {
            AppSettings appSettings = new AppSettings();
            IJsonService jsonService = DIUtils.GetRequiredService<IJsonService>();

            try
            {
                IJsonService _jsonService = DIUtils.GetRequiredService<IJsonService>();
                appSettings = await _jsonService.ReadJsonFileAsync<AppSettings>(_settingsPathFileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading App Settings: {ex.Message}");
            }

            return appSettings;
        }

        public ThemeRoot LoadThemesFromFile()
        {
            ThemeRoot themes = new ThemeRoot();
            try
            {
               
                IJsonService _jsonService = DIUtils.GetRequiredService<IJsonService>();
                themes = _jsonService.ReadJsonFile<ThemeRoot>(_themesPathFileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading themes: {ex.Message}");
            }

            return themes;
        }
    }
}
