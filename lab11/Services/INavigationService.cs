using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab9.Services
{
    // Интерфейс сервиса навигации
    public interface INavigationService
    {
        object? CurrentViewModel { get; }
        void NavigateTo<TViewModel>(object? parameter = null) where TViewModel : class;
    }

    // Интерфейс для передачи параметров при навигации (например, выбранного контакта)
    public interface INavigationAware
    {
        void OnNavigatedTo(object? parameter);
    }
}
