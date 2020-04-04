using System.Threading.Tasks;
using Xamarin.Forms;
using IEEE_COVID19.Services.NavigationService;
using IEEE_COVID19.ViewModels.Base;
using IEEE_COVID19.Notification;

namespace IEEE_COVID19
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();           
        }

        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
            DependencyService.Get<INotificationManager>().Initialize();
            return navigationService.InitializeAsync();
        }
        protected override async void OnStart()
        {
            // Handle when your app starts
            base.OnStart();

            if (Device.RuntimePlatform != Device.UWP)
            {
                await InitNavigation();
            }

            base.OnResume();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
