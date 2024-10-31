using Avalonia.Themes.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;

namespace AvaloniaCustomTheme.Models.DTOS
{
    public class ThemeSettings
    {
        public string AccentColor { get; set; }
        public string AltHigh { get; set; }
        public string AltLow { get; set; }
        public string AltMedium { get; set; }
        public string AltMediumHigh { get; set; }
        public string AltMediumLow { get; set; }
        public string BaseHigh { get; set; }
        public string BaseLow { get; set; }
        public string BaseMedium { get; set; }
        public string BaseMediumHigh { get; set; }
        public string BaseMediumLow { get; set; }
        public string ChromeAltLow { get; set; }
        public string ChromeBlackHigh { get; set; }
        public string ChromeBlackLow { get; set; }
        public string ChromeBlackMedium { get; set; }
        public string ChromeBlackMediumLow { get; set; }
        public string ChromeDisabledHigh { get; set; }
        public string ChromeDisabledLow { get; set; }
        public string ChromeGray { get; set; }
        public string ChromeHigh { get; set; }
        public string ChromeLow { get; set; }
        public string ChromeMedium { get; set; }
        public string ChromeMediumLow { get; set; }
        public string ChromeWhite { get; set; }
        public string ListLow { get; set; }
        public string ListMedium { get; set; }
        public string RegionColor { get; set; }
    }

    public class Theme
    {
        public string       Name {  get; set; } 
        public ThemeSettings Dark  { get; set; } 
        public ThemeSettings Light { get; set; }
    }

    public class ThemeRoot
    {
        // This will allow for dynamic theme addition
        public Dictionary<string, Theme> Themes { get; set; }
    }




    public static class ThemeConverter
    {
        public static string GetRegionColor(ThemeRoot themeRoot, string themeName, string variant)
        {
            if (themeRoot == null || string.IsNullOrEmpty(themeName) || string.IsNullOrEmpty(variant))
            {
                throw new ArgumentException("ThemeRoot, theme name, and variant must be provided.");
            }

            if (themeRoot.Themes.TryGetValue(themeName, out var theme))
            {
                ThemeSettings settings = variant.ToLower() switch
                {
                    "light" => theme.Light,
                    "dark" => theme.Dark,
                    _ => theme.Dark
                };

                return settings?.RegionColor ?? throw new InvalidOperationException($"RegionColor not found for {themeName} theme with {variant} variant.");
            }
            else
            {
                throw new KeyNotFoundException($"Theme '{themeName}' not found in ThemeRoot.");
            }
        }


        public static ColorPaletteResources ToColorPaletteResources(ThemeSettings settings, int transparency = 0xFF)
        {

            Func<int, string> toHex = number => number.ToString("X2");
            string transvalue = toHex(transparency);
            settings.RegionColor = settings.RegionColor.Substring(0, 1) + transvalue + settings.RegionColor.Substring(3);


            return new ColorPaletteResources
            {
                Accent = Color.Parse(settings.AccentColor),
                AltHigh = Color.Parse(settings.AltHigh),
                AltLow = Color.Parse(settings.AltLow),
                AltMedium = Color.Parse(settings.AltMedium),
                AltMediumHigh = Color.Parse(settings.AltMediumHigh),
                AltMediumLow = Color.Parse(settings.AltMediumLow),
                BaseHigh = Color.Parse(settings.BaseHigh),
                BaseLow = Color.Parse(settings.BaseLow),
                BaseMedium = Color.Parse(settings.BaseMedium),
                BaseMediumHigh = Color.Parse(settings.BaseMediumHigh),
                BaseMediumLow = Color.Parse(settings.BaseMediumLow),
                ChromeAltLow = Color.Parse(settings.ChromeAltLow),
                ChromeBlackHigh = Color.Parse(settings.ChromeBlackHigh),
                ChromeBlackLow = Color.Parse(settings.ChromeBlackLow),
                ChromeBlackMedium = Color.Parse(settings.ChromeBlackMedium),
                ChromeBlackMediumLow = Color.Parse(settings.ChromeBlackMediumLow),
                ChromeDisabledHigh = Color.Parse(settings.ChromeDisabledHigh),
                ChromeDisabledLow = Color.Parse(settings.ChromeDisabledLow),
                ChromeGray = Color.Parse(settings.ChromeGray),
                ChromeHigh = Color.Parse(settings.ChromeHigh),
                ChromeLow = Color.Parse(settings.ChromeLow),
                ChromeMedium = Color.Parse(settings.ChromeMedium),
                ChromeMediumLow = Color.Parse(settings.ChromeMediumLow),
                ChromeWhite = Color.Parse(settings.ChromeWhite),
                ListLow = Color.Parse(settings.ListLow),
                ListMedium = Color.Parse(settings.ListMedium),
                RegionColor = Color.Parse(settings.RegionColor)
            };

        }
    }

}
