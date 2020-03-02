using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace EstusShots.Avalonia
{
    public static class Extensions
    {
        public static IServiceCollection AddViewModels(this IServiceCollection @this)
        {
            var viewModels = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.Name.EndsWith("ViewModel"));
            viewModels.ForEach(x => @this.AddSingleton(x));
            return @this;
        }
    }
}