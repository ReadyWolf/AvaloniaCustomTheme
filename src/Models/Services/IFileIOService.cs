using AvaloniaCustomTheme.Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaCustomTheme.Models.Services
{
    public interface IFileIOService
    {
        bool InitalizePaths();
        Task SaveAppSettingsToFileAsync(AppSettings appSettings);
        bool SaveAppSettingsToFile(AppSettings appSettings);
        AppSettings LoadAppSettingsFromFile();
        Task<AppSettings> LoadAppSettingsFromFileAsync();
        ThemeRoot LoadThemesFromFile();

    }
}

