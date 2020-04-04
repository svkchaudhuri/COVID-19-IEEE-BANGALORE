using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using IEEE_COVID19.ViewModels;
using IEEE_COVID19.ViewModels.Base;
using IEEE_COVID19.Views;
using Xamarin.Forms;

namespace IEEE_COVID19.Services.NavigationService
{
    public class NavigationService : INavigationService
    {
       public ViewModelBase PreviousPageViewModel
        {
            get
            {
                var mainPage = (Application.Current.MainPage as MainView).Detail as NavigationPage;
                var viewModel = mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2].BindingContext;
                return viewModel as ViewModelBase;
            }
        }
        public Task InitializeAsync()
        {
           return NavigateToAsync<MainViewModel>();
        }
        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task RemoveLastFromBackStackAsync()
        {
            var mainPage = (Application.Current.MainPage as MainView).Detail as NavigationPage;

            if (mainPage != null)
            {
                mainPage.Navigation.RemovePage(
                    mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        public Task RemoveBackStackAsync()
        {
            var mainPage = (Application.Current.MainPage as MainView).Detail as NavigationPage;

            if (mainPage != null)
            {
                for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
                {
                    var page = mainPage.Navigation.NavigationStack[i];
                    mainPage.Navigation.RemovePage(page);
                }
            }
            return Task.FromResult(true);
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {

            var page = CreatePage(viewModelType, parameter);

            if (page is MainView)
            {
                Application.Current.MainPage = page;
            }
            else
            {
                if ((Application.Current.MainPage as MainView)?.Detail is NavigationPage navigationPage)
                {
                    await navigationPage.PushAsync(page);
                    ((MainView)Application.Current.MainPage).IsPresented = false;
                }
            }

            if (page.BindingContext != null)
                await ((ViewModelBase)page.BindingContext)?.Initialize(parameter);
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        private Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            return page;
        }

        public Task PushNewNavigation<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNewNavigationAsync(typeof(TViewModel), null);
        }

        public Task PushNewNavigation<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNewNavigationAsync(typeof(TViewModel), parameter);
        }

        private async Task InternalNewNavigationAsync(Type viewModelType, object parameter)
        {
            Page page = CreatePage(viewModelType, parameter);
            var mainPage = Application.Current.MainPage as MainView;
            if (mainPage != null)
            {
                mainPage.Detail = new NavigationPage(page);
                mainPage.IsPresented = false;
            }
            await (page.BindingContext as ViewModelBase)?.Initialize(parameter);
        }
    }
}

