using lab9.Services;
using lab9.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace lab9
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 1. Создаём коллекцию сервисов
            var services = new ServiceCollection();

            // 2. Регистрируем сервисы (Lifetime)
            // DialogService — Singleton(один экземпляр на всё приложение), так как он не хранит состояние пользователя.
            services.AddSingleton<IDialogService, DialogService>();

            // 3. MainViewModel — Transient(новый экземпляр при каждом запросе) (при навигации нам будут нужны новые экземпляры)
            services.AddTransient<MainViewModel>();

            // 4. MainWindow  — Singleton с явной передачей DataContext через лямбда-выражение
            // Окно создаётся один раз, DataContext получается из контейнера
            services.AddSingleton<MainWindow>(sp =>
            {
                var window = new MainWindow();
                window.DataContext = sp.GetRequiredService<MainViewModel>();
                return window;
            });

            // 5. Создаём контейнер (ServiceProvider)
            var serviceProvider = services.BuildServiceProvider();

            // 6. Получаем главное окно из контейнера и запускаем приложение
            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
