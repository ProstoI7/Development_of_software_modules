using lab9.Models;
using lab9.Services;
using lab9.ViewModels;
using lab9.Views;
using Microsoft.EntityFrameworkCore;
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

            services.AddDbContext<PhoneBookDbЛуканина2307б2Context>(options => options.UseSqlServer("Data Source=.\\\\XZ;Initial Catalog=PhoneBookDB_Луканина_2307б2;Integrated Security=True;Trust Server Certificate=True"));

            // 2. Регистрируем сервисы (Lifetime)
            // DialogService — Singleton(один экземпляр на всё приложение), так как он не хранит состояние пользователя.
            services.AddSingleton<IDialogService, DialogService>();
            // NavigationService — Singleton(один экземпляр на всё приложение)
            services.AddSingleton<INavigationService, NavigationService>();

            // 3. Эти ViewModel — Transient(новый экземпляр при каждом запросе) (при навигации нам будут нужны новые экземпляры)
            services.AddTransient<AboutViewModel>();
            services.AddTransient<ContactEditViewModel>();
            services.AddTransient<ContactsListViewModel>();
            // MainWindowViewModel — Singleton(живёт всё время работы приложения)
            services.AddSingleton<MainWindowViewModel>();
            // 4. MainWindow  — Singleton с явной передачей DataContext через лямбда-выражение
            // Окно создаётся один раз, DataContext получается из контейнера
            services.AddSingleton<MainWindow>(sp =>
            {
                var window = new MainWindow();
                window.DataContext = sp.GetRequiredService<MainWindowViewModel>();
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
