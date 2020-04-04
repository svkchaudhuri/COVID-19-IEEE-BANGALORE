using System.Threading.Tasks;
using IEEE_COVID19.ViewModels.Base;

namespace IEEE_COVID19.Services.NavigationService
{
    public interface INavigationService
    {
        ViewModelBase PreviousPageViewModel { get; }

        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

        Task PushNewNavigation<TViewModel>() where TViewModel : ViewModelBase;

        Task PushNewNavigation<TViewModel>(object parameter) where TViewModel : ViewModelBase;

        Task RemoveLastFromBackStackAsync();

        Task RemoveBackStackAsync();
    }
}

