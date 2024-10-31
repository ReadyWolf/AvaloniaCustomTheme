using AvaloniaCustomTheme.Models.DTOS;
using AvaloniaCustomTheme.Models.Services;
using DynamicData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaCustomTheme.Models
{
    // TODO move this to FileIOService
    public class ThemeController
    {

        public  ThemeRoot themes;
        public  ObservableCollection<string> themeNames { get; set; }

        public void LoadThemes(string filePath)
        {
            try
            {

                IFileIOService _fileIOService = DIUtils.GetRequiredService<IFileIOService>();
                themes = _fileIOService.LoadThemesFromFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading themes: {ex.Message}");
            }



        

            Console.WriteLine($"Done");
        }

        public ObservableCollection<string> GetThemeNames()
        {
            var themeNames = new ObservableCollection<string>();
            foreach (var themeName in themes.Themes.Keys)
            {
                themeNames.Add(themeName);
            }
            return themeNames;
        }

        public async Task AddNewThemeAndSave(string filePath, Theme newTheme, string themeName)
        {
            try
            {
                IJsonService _jsonService = DIUtils.GetRequiredService<IJsonService>();
                // Add new theme to the dictionary
                themes.Themes.Add(themeName, newTheme);

                // Save the updated themes
                await _jsonService.WriteJsonFileAsync(filePath, themes);
                Console.WriteLine($"{themeName} added and saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving {themeName} theme: {ex.Message}");
            }
        }


        public async Task SaveThemes(string filePath)
        {
            try
            {
                IJsonService _jsonService = DIUtils.GetRequiredService<IJsonService>();
                // Save the updated themes
                await _jsonService.WriteJsonFileAsync(filePath, themes);
                Console.WriteLine("Themes saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving new theme: {ex.Message}");
            }
        }
    }

}
