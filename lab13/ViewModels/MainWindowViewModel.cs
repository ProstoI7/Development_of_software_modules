using lab9.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace lab9.ViewModels
{
    public class MainWindowViewModel
    {
        public INavigationService NavigationService { get; }

        public ICommand ShowContactsCommand { get; }
        public ICommand ShowAboutCommand { get; }

        public MainWindowViewModel(INavigationService navigation)
        {
            NavigationService = navigation;

            ShowContactsCommand = new RelayCommand(() => NavigationService.NavigateTo<ContactsListViewModel>());
            ShowAboutCommand = new RelayCommand(() => NavigationService.NavigateTo<AboutViewModel>());

            NavigationService.NavigateTo<ContactsListViewModel>();
        }
    }
}
