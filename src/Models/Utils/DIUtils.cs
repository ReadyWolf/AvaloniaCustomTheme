using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaCustomTheme.Models
{
    public static class DIUtils
    {

        public static TService GetRequiredService<TService>()
        {

            var service = Locator.Current.GetService<TService>();
            if (service is null)
            {
                throw new InvalidOperationException($"Failed to resolve object of type {typeof(TService)}");
            }

            return service;
        }

        public static object GetRequiredService(this IReadonlyDependencyResolver resolver, Type type)
        {
            var service = resolver.GetService(type);
            if (service is null)
            {
                throw new InvalidOperationException($"Failed to resolve object of type {type}");
            }

            return service;
        }
    }
}
