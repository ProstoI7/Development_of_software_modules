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

            var services = new ServiceCollection();

            // 1. РЕГИСТРАЦИЯ ФАБРИКИ КОНТЕКСТОВ БАЗЫ ДАННЫХ (IDbContextFactory)
            // Вместо одиночного AddDbContext мы регистрируем фабрику, которая будет генерировать чистые короткоживущие контексты.
            services.AddDbContextFactory<PhoneBookDbЛуканина2307б2Context>(options =>
                options.UseSqlServer("Data Source=.\\XZ;Initial Catalog=PhoneBookDB_Луканина_2307б2;Integrated Security=True;Trust Server Certificate=True"));

            // 2. Регистрация глобальных инфраструктурных сервисов (Singleton)
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<INavigationService, NavigationService>();

            // 3. Регистрация ViewModels (Transient для дочерних экранов)
            services.AddTransient<AboutViewModel>();
            services.AddTransient<ContactEditViewModel>();
            services.AddTransient<ContactsListViewModel>(); // Фабрика автоматически залетит сюда через конструктор!

            // MainWindowViewModel и Главное окно Shell регистрируем как Singleton
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<MainWindow>(sp =>
            {
                var window = new MainWindow();
                window.DataContext = sp.GetRequiredService<MainWindowViewModel>();
                return window;
            });

            // 4. Инициализация провайдера служб и вывод Shell-окна на экран
            var serviceProvider = services.BuildServiceProvider();
            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
