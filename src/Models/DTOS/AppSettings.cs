using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaCustomTheme.Models.DTOS
{
    public class AppSettings
    {
        public string ThemeCurrentName = "Default";
        public string ThemeVariant = "Default";
        public int ThemeTransparency = 255;
       
     
        public void UpdateAppSettings(AppSettings other)
        {
            ThemeCurrentName = other.ThemeCurrentName;
            ThemeVariant = other.ThemeVariant;
            ThemeTransparency = other.ThemeTransparency;
        }

    }


}
