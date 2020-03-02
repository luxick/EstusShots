using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using EstusShots.Avalonia.ViewModels;
using EstusShots.Avalonia.Views;
using EstusShots.Client;
using Microsoft.Extensions.DependencyInjection;

namespace EstusShots.Avalonia
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // The Application uses a single shared API client.
                var apiClient = new EstusShotsClient("http://localhost:5000/api/");
                    
                // Add all our services to the DI Container
                var serviceProvider = new ServiceCollection()
                    .AddSingleton(apiClient)
                    .AddViewModels()
                    .BuildServiceProvider();
                
                var main = new MainWindowViewModel(serviceProvider);
                Global.Navigator = new Navigator(main);
                Global.Navigator.GoTo<SeasonsViewModel>();

                desktop.MainWindow = new MainWindow
                {
                    DataContext = main,
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}